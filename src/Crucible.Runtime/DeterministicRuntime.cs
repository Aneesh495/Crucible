namespace Crucible.Runtime;

using System.Text;
using Crucible.Abstractions;

/// <summary>Per-process context bound to the ambient runtime.</summary>
public sealed class SimContext : ISimContext
{
    private readonly DeterministicRuntime _runtime;

    public SimContext(DeterministicRuntime runtime, NodeId self)
    {
        _runtime = runtime;
        Self = self;
    }

    public NodeId Self { get; }
    public ISimClock Clock => _runtime.Clock;
    public ISimRandom Random => _runtime.Random;
    public ISimNetwork Network => _runtime.Network;
    public ISimStorage Storage => _runtime.Storage;

    public void SetTimer(string name, long delayTicks) =>
        _runtime.SetTimer(Self, name, delayTicks);

    public void CancelTimer(string name) => _runtime.CancelTimer(Self, name);

    public void Yield(string? label = null) => _runtime.Yield(Self, label);

    public int Choose(int candidateCount, string? label = null) =>
        _runtime.Choose(Self, candidateCount, label);

    public void Log(string message) => _runtime.Log(Self, message);

    public void RecordEvent(string category, string detail) =>
        _runtime.RecordEvent(Self, category, detail);
}

/// <summary>Enabled transition the scheduler can take on a step.</summary>
public enum TransitionKind
{
    DeliverMessages,
    FireTimers,
    RunProcessStep,
    DriveClient,
    ApplyFault
}

public sealed class EnabledTransition
{
    public TransitionKind Kind { get; init; }
    public NodeId? Node { get; init; }
    public string? Label { get; init; }
    public Action Execute { get; init; } = () => { };
}

/// <summary>
/// The heart of Crucible: one cluster, one clock, one RNG, one schedule oracle.
/// Steps are discrete. Within a step the oracle picks which enabled transition fires.
/// </summary>
public sealed class DeterministicRuntime : IClusterView
{
    private readonly List<ISimProcess> _processes = new();
    private readonly Dictionary<NodeId, ISimProcess> _byId = new();
    private readonly Dictionary<NodeId, ProcessState> _states = new();
    private readonly Dictionary<NodeId, SimContext> _contexts = new();
    private readonly List<string> _eventLog = new();
    private readonly TimerWheel _timers = new();
    private readonly List<FaultEvent> _pendingFaults = new();
    private IScheduleOracle _oracle;
    private Action<ISimContext, IReadOnlyList<NodeId>, int>? _clientDriver;
    private int _clientStep;
    private bool _running;

    public DeterministicRuntime(int seed, IScheduleOracle? oracle = null)
    {
        Seed = seed;
        Clock = new VirtualClock();
        Random = new SeededRng(seed);
        _oracle = oracle ?? new RecordingOracle(Random);
        Network = new SimNetwork(Clock, Random, _oracle);
        Storage = new SimStorage();
    }

    public int Seed { get; }
    public VirtualClock Clock { get; }
    public SeededRng Random { get; }
    public SimNetwork Network { get; }
    public SimStorage Storage { get; }
    public long StepsExecuted { get; private set; }
    public IScheduleOracle Oracle => _oracle;

    public SimTime Now => Clock.Now;
    public IReadOnlyList<NodeId> Nodes => _processes.Select(p => p.Id).ToArray();
    public IReadOnlyList<ISimProcess> AllProcesses => _processes;
    public IReadOnlyList<string> EventLog => _eventLog;

    public ProcessState GetState(NodeId id) =>
        _states.TryGetValue(id, out var s) ? s : ProcessState.Stopped;

    public T? GetProcess<T>(NodeId id) where T : class, ISimProcess =>
        _byId.TryGetValue(id, out var p) ? p as T : null;

    public void SetOracle(IScheduleOracle oracle)
    {
        _oracle = oracle;
        // Network was constructed with the old oracle reference; rebuild delivery-side hook
        // by assigning through a shim — SimNetwork holds oracle only for reorder choices.
        // For simplicity, reorder will use the runtime Choose path via explicit calls.
    }

    public void Register(ISimProcess process)
    {
        if (_byId.ContainsKey(process.Id))
            throw new InvalidOperationException($"Duplicate process {process.Id}");
        _processes.Add(process);
        _byId[process.Id] = process;
        _states[process.Id] = ProcessState.Stopped;
        _contexts[process.Id] = new SimContext(this, process.Id);
    }

    public void RegisterFault(FaultEvent fault) => _pendingFaults.Add(fault);

    public void SetClientDriver(Action<ISimContext, IReadOnlyList<NodeId>, int> driver) =>
        _clientDriver = driver;

    public void StartAll()
    {
        foreach (var p in _processes)
        {
            _states[p.Id] = ProcessState.Running;
            p.Start(_contexts[p.Id]);
            RecordEvent(p.Id, "lifecycle", "start");
        }
        _running = true;
    }

    public void SetTimer(NodeId owner, string name, long delayTicks)
    {
        if (delayTicks < 0) throw new ArgumentOutOfRangeException(nameof(delayTicks));
        _timers.Set(owner, name, Clock.Now.Add(delayTicks));
    }

    public void CancelTimer(NodeId owner, string name) => _timers.Cancel(owner, name);

    public void Yield(NodeId actor, string? label) =>
        _oracle.Choose(SchedulingPointKind.Yield, 1, actor, label ?? "yield");

    public int Choose(NodeId actor, int candidateCount, string? label) =>
        _oracle.Choose(SchedulingPointKind.NondeterministicChoice, candidateCount, actor, label);

    public void Log(NodeId node, string message) =>
        _eventLog.Add($"{Clock.Now} {node}: {message}");

    public void RecordEvent(NodeId node, string category, string detail) =>
        _eventLog.Add($"{Clock.Now} [{category}] {node}: {detail}");

    public void Crash(NodeId node)
    {
        if (GetState(node) != ProcessState.Running) return;
        _states[node] = ProcessState.Crashed;
        _timers.ClearNode(node);
        _byId[node].OnCrash();
        RecordEvent(node, "lifecycle", "crash");
    }

    public void Restart(NodeId node)
    {
        if (GetState(node) != ProcessState.Crashed) return;
        _states[node] = ProcessState.Running;
        _byId[node].OnRestart(_contexts[node]);
        RecordEvent(node, "lifecycle", "restart");
    }

    /// <summary>Collect currently enabled transitions.</summary>
    public List<EnabledTransition> CollectEnabled()
    {
        var enabled = new List<EnabledTransition>();

        // Faults due by step or time.
        foreach (var fault in _pendingFaults.Where(f =>
                     (f.AtStep is int s && s == StepsExecuted) ||
                     (f.AtTime is SimTime t && t <= Clock.Now)).ToArray())
        {
            var captured = fault;
            enabled.Add(new EnabledTransition
            {
                Kind = TransitionKind.ApplyFault,
                Node = fault.Target,
                Label = fault.Kind.ToString(),
                Execute = () => ApplyFault(captured)
            });
        }

        var nextMsg = Network.NextDeliveryTime();
        var nextTimer = _timers.NextFireTime();

        if (nextMsg is SimTime mt && mt <= Clock.Now)
        {
            enabled.Add(new EnabledTransition
            {
                Kind = TransitionKind.DeliverMessages,
                Label = "deliver",
                Execute = () =>
                {
                    Network.DeliverDue();
                    DispatchInboxes();
                }
            });
        }

        if (nextTimer is SimTime tt && tt <= Clock.Now)
        {
            enabled.Add(new EnabledTransition
            {
                Kind = TransitionKind.FireTimers,
                Label = "timers",
                Execute = FireDueTimers
            });
        }

        foreach (var p in _processes)
        {
            if (_states[p.Id] != ProcessState.Running) continue;
            if (Network.PendingCount(p.Id) > 0)
            {
                var id = p.Id;
                enabled.Add(new EnabledTransition
                {
                    Kind = TransitionKind.RunProcessStep,
                    Node = id,
                    Label = "inbox",
                    Execute = () => DispatchNode(id)
                });
            }
        }

        if (_clientDriver is not null)
        {
            enabled.Add(new EnabledTransition
            {
                Kind = TransitionKind.DriveClient,
                Label = "client",
                Execute = () =>
                {
                    // Client uses node 0's context as a convenience handle for network/clock.
                    var ctx = _contexts[_processes[0].Id];
                    _clientDriver(ctx, Nodes.ToArray(), _clientStep++);
                }
            });
        }

        return enabled;
    }

    /// <summary>
    /// Advance time to the next interesting event if nothing is enabled at Now,
    /// then pick and execute one transition.
    /// </summary>
    public bool StepOnce()
    {
        if (!_running) return false;

        var enabled = CollectEnabled();
        if (enabled.Count == 0)
        {
            var nextMsg = Network.NextDeliveryTime();
            var nextTimer = _timers.NextFireTime();
            SimTime? next = null;
            if (nextMsg is { } m) next = m;
            if (nextTimer is { } t)
                next = next is { } n ? (t < n ? t : n) : t;

            if (next is null || next <= Clock.Now)
                return false;

            Clock.AdvanceTo(next.Value);
            enabled = CollectEnabled();
            if (enabled.Count == 0)
                return false;
        }

        var pick = _oracle.Choose(
            SchedulingPointKind.TaskResume,
            enabled.Count,
            enabled[0].Node,
            "transition");
        enabled[pick].Execute();
        StepsExecuted++;

        // Opportunistically deliver/fire if time already matches after the transition.
        if (Network.NextDeliveryTime() is SimTime dm && dm <= Clock.Now)
        {
            Network.DeliverDue();
            DispatchInboxes();
        }
        if (_timers.NextFireTime() is SimTime tm && tm <= Clock.Now)
            FireDueTimers();

        return true;
    }

    public void Run(int maxSteps)
    {
        StartAll();
        for (var i = 0; i < maxSteps; i++)
        {
            if (!StepOnce())
                break;
        }
    }

    public IReadOnlyList<InvariantViolation> CheckAll(IEnumerable<IInvariant> globals)
    {
        var violations = new List<InvariantViolation>();
        foreach (var p in _processes)
            violations.AddRange(p.CheckLocalInvariants());
        foreach (var inv in globals)
            violations.AddRange(inv.Check(this));
        return violations;
    }

    public void Reset()
    {
        _processes.Clear();
        _byId.Clear();
        _states.Clear();
        _contexts.Clear();
        _eventLog.Clear();
        _timers.Reset();
        _pendingFaults.Clear();
        Network.Reset();
        Storage.Reset();
        Clock.Reset();
        StepsExecuted = 0;
        _clientStep = 0;
        _clientDriver = null;
        _running = false;
        if (_oracle is RecordingOracle rec)
            rec.Reset();
    }

    public string DumpState()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"seed={Seed} time={Clock.Now} steps={StepsExecuted}");
        sb.AppendLine($"net: sent={Network.MessagesSent} dropped={Network.MessagesDropped} delivered={Network.MessagesDelivered} inFlight={Network.InFlight.Count}");
        foreach (var p in _processes)
            sb.AppendLine($"  {p.Id} state={_states[p.Id]} inbox={Network.PendingCount(p.Id)}");
        return sb.ToString();
    }

    private void DispatchInboxes()
    {
        foreach (var p in _processes)
        {
            if (_states[p.Id] != ProcessState.Running) continue;
            DispatchNode(p.Id);
        }
    }

    private void DispatchNode(NodeId id)
    {
        var process = _byId[id];
        while (Network.TryReceive(id, out var env) && env is not null)
            process.OnMessage(env);
    }

    private void FireDueTimers()
    {
        var due = _timers.PopDue(Clock.Now);
        foreach (var t in due)
        {
            if (_states.TryGetValue(t.Owner, out var st) && st == ProcessState.Running)
                _byId[t.Owner].OnTimer(t.Name, t.Generation);
        }
    }

    private void ApplyFault(FaultEvent fault)
    {
        _pendingFaults.Remove(fault);
        switch (fault.Kind)
        {
            case FaultKind.Crash when fault.Target is { } c:
                Crash(c);
                break;
            case FaultKind.Restart when fault.Target is { } r:
                Restart(r);
                break;
            case FaultKind.Partition when fault.PartitionA is not null && fault.PartitionB is not null:
                Network.SetPartition(fault.PartitionA, fault.PartitionB);
                RecordEvent(fault.Target ?? new NodeId(0), "fault", "partition");
                break;
            case FaultKind.Heal:
                Network.HealPartitions();
                RecordEvent(new NodeId(0), "fault", "heal");
                break;
            case FaultKind.DropMessage:
                Network.DropAllInFlight();
                break;
            case FaultKind.ClockSkew when fault.Target is { } && fault.DelayTicks is long d:
                // Logical skew is modelled as delaying that node's timers.
                RecordEvent(fault.Target.Value, "fault", $"clock-skew {d}");
                break;
            default:
                RecordEvent(fault.Target ?? new NodeId(0), "fault", fault.Kind.ToString());
                break;
        }
    }
}
