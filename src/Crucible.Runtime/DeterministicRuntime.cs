namespace Crucible.Runtime;

using System.Security.Cryptography;
using System.Text;
using Crucible.Abstractions;

/// <summary>Per-process clock view accounting for local clock skew.</summary>
public sealed class NodeClockView : ISimClock
{
    private readonly DeterministicRuntime _runtime;
    private readonly NodeId _node;

    public NodeClockView(DeterministicRuntime runtime, NodeId node)
    {
        _runtime = runtime;
        _node = node;
    }

    public SimTime Now => _runtime.GetNodeTime(_node);
    public void AdvanceTo(SimTime time) => _runtime.Clock.AdvanceTo(time);
    public void AdvanceBy(long ticks) => _runtime.Clock.AdvanceBy(ticks);
}

/// <summary>Per-process context bound to the ambient runtime.</summary>
public sealed class SimContext : ISimContext
{
    private readonly DeterministicRuntime _runtime;
    private readonly NodeClockView _clockView;

    public SimContext(DeterministicRuntime runtime, NodeId self)
    {
        _runtime = runtime;
        Self = self;
        _clockView = new NodeClockView(runtime, self);
    }

    public NodeId Self { get; }
    public ISimClock Clock => _clockView;
    public ISimRandom Random => _runtime.ProtocolRandom;
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

/// <summary>Enabled atomic transition kind.</summary>
public enum TransitionKind
{
    ApplyFault,
    DeliverMessage,
    ProcessMessage,
    FireTimer,
    DriveClient
}

/// <summary>Enabled transition the scheduler can select on a step.</summary>
public sealed class EnabledTransition
{
    public required TransitionKind Kind { get; init; }
    public required NodeId? Node { get; init; }
    public required string Key { get; init; }
    public required string Label { get; init; }
    public required Action Execute { get; init; }
}

/// <summary>Classification of simulation run completion.</summary>
public enum RunClassification
{
    Completed,
    Quiescent,
    SafetyViolation,
    StepBoundExhausted,
    ReplayDivergence
}

/// <summary>Comprehensive metrics collected during execution.</summary>
public sealed class ExecutionMetrics
{
    public long VirtualTimeAdvanced { get; set; }
    public long ClientOpsAttempted { get; set; }
    public long ClientOpsCompleted { get; set; }
    public long MessagesSent { get; set; }
    public long MessagesDelivered { get; set; }
    public long MessagesDropped { get; set; }
    public long MessagesDuplicated { get; set; }
    public int MessagesPending { get; set; }
    public long TimersFired { get; set; }
    public long FaultsApplied { get; set; }
    public long ProcessesCrashed { get; set; }
    public long ProcessesRestarted { get; set; }
    public long SchedulesExplored { get; set; }
    public long InvariantChecksExecuted { get; set; }
}

/// <summary>
/// The deterministic runtime: one cluster, one clock, separate deterministic RNG streams,
/// discrete atomic transitions, and per-transition safety checking.
/// </summary>
public sealed class DeterministicRuntime : IClusterView
{
    private readonly List<ISimProcess> _processes = new();
    private readonly Dictionary<NodeId, ISimProcess> _byId = new();
    private readonly Dictionary<NodeId, ProcessState> _states = new();
    private readonly Dictionary<NodeId, SimContext> _contexts = new();
    private readonly Dictionary<NodeId, long> _clockSkews = new();
    private readonly List<string> _eventLog = new();
    private readonly TimerWheel _timers = new();
    private readonly List<FaultEvent> _pendingFaults = new();
    private readonly List<IInvariant> _invariants = new();
    private readonly List<InvariantViolation> _violations = new();
    private readonly ExecutionMetrics _metrics = new();

    private readonly SeededRng _protocolRandom;
    private readonly SeededRng _networkRandom;
    private readonly SeededRng _scheduleRandom;

    private IScheduleOracle _oracle;
    private Action<ISimContext, IReadOnlyList<NodeId>, int>? _clientDriver;
    private int _clientStep;
    private int _clientOpLimit = 10;
    private long _clientOpInterval = 10;
    private SimTime _clientNextTime = SimTime.Zero;
    private bool _running;
    private long _nextFaultId = 1;

    public DeterministicRuntime(int seed, IScheduleOracle? oracle = null)
    {
        Seed = seed;
        Clock = new VirtualClock();
        Random = new SeededRng(seed);
        _protocolRandom = Random.Fork("protocol");
        _networkRandom = Random.Fork("network");
        _scheduleRandom = Random.Fork("schedule");

        _oracle = oracle ?? new RecordingOracle(_scheduleRandom);
        Network = new SimNetwork(Clock, _networkRandom, _oracle);
        Storage = new SimStorage();
    }

    public int Seed { get; }
    public VirtualClock Clock { get; }
    public SeededRng Random { get; }
    public SeededRng ProtocolRandom => _protocolRandom;
    public SeededRng NetworkRandom => _networkRandom;
    public SeededRng ScheduleRandom => _scheduleRandom;
    public SimNetwork Network { get; }
    public SimStorage Storage { get; }
    public long StepsExecuted { get; private set; }
    public IScheduleOracle Oracle => _oracle;
    public ExecutionMetrics Metrics => _metrics;
    public IReadOnlyList<InvariantViolation> Violations => _violations;

    public SimTime Now => Clock.Now;
    public IReadOnlyList<NodeId> Nodes => _processes.Select(p => p.Id).ToArray();
    public IReadOnlyList<ISimProcess> AllProcesses => _processes;
    public IReadOnlyList<string> EventLog => _eventLog;
    public bool IsQuiescent => Network.InFlight.Count == 0 && _processes.All(p => Network.PendingCount(p.Id) == 0);

    public ProcessState GetState(NodeId id) =>
        _states.TryGetValue(id, out var s) ? s : ProcessState.Stopped;

    public T? GetProcess<T>(NodeId id) where T : class, ISimProcess =>
        _byId.TryGetValue(id, out var p) ? p as T : null;

    public long GetClockSkew(NodeId node) => _clockSkews.GetValueOrDefault(node);
    public SimTime GetNodeTime(NodeId node) => Clock.Now.Add(GetClockSkew(node));

    public void SetOracle(IScheduleOracle oracle)
    {
        _oracle = oracle;
        Network.Oracle = oracle;
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

    public void RegisterInvariant(IInvariant invariant) => _invariants.Add(invariant);

    public void RegisterFault(FaultEvent fault)
    {
        var assigned = fault.Id == 0
            ? new FaultEvent
            {
                Id = _nextFaultId++,
                Kind = fault.Kind,
                AtTime = fault.AtTime,
                AtStep = fault.AtStep,
                Target = fault.Target,
                Secondary = fault.Secondary,
                PartitionA = fault.PartitionA,
                PartitionB = fault.PartitionB,
                DelayTicks = fault.DelayTicks,
                Label = fault.Label
            }
            : fault;
        _pendingFaults.Add(assigned);
    }

    public void SetClientDriver(
        Action<ISimContext, IReadOnlyList<NodeId>, int> driver,
        int opLimit = 10,
        long opInterval = 10)
    {
        _clientDriver = driver;
        _clientOpLimit = opLimit;
        _clientOpInterval = opInterval;
        _clientStep = 0;
        _clientNextTime = Clock.Now;
    }

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
        // Node local clock view: fire time in global clock
        var fireAt = Clock.Now.Add(delayTicks);
        _timers.Set(owner, name, fireAt);
    }

    public void CancelTimer(NodeId owner, string name) => _timers.Cancel(owner, name);

    public void FireTimer(NodeId owner, string name, long generation = 0)
    {
        if (_byId.TryGetValue(owner, out var proc) && _states.TryGetValue(owner, out var st) && st == ProcessState.Running)
        {
            proc.OnTimer(name, generation);
            _metrics.TimersFired++;
        }
    }

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
        _metrics.ProcessesCrashed++;
    }

    public void Restart(NodeId node)
    {
        if (GetState(node) != ProcessState.Crashed) return;
        _states[node] = ProcessState.Running;
        _byId[node].OnRestart(_contexts[node]);
        RecordEvent(node, "lifecycle", "restart");
        _metrics.ProcessesRestarted++;
    }

    /// <summary>Collect atomic transitions currently enabled at the current virtual time.</summary>
    public List<EnabledTransition> CollectEnabled()
    {
        var enabled = new List<EnabledTransition>();

        // 1. Ready faults
        foreach (var fault in _pendingFaults)
        {
            bool ready = (fault.AtStep is int s && StepsExecuted >= s) ||
                         (fault.AtTime is SimTime t && Clock.Now >= t);
            if (ready)
            {
                var f = fault;
                enabled.Add(new EnabledTransition
                {
                    Kind = TransitionKind.ApplyFault,
                    Node = f.Target,
                    Key = $"fault:{f.Id}:{f.Kind}",
                    Label = $"fault:{f.Kind}",
                    Execute = () => ApplyFault(f)
                });
            }
        }

        // 2. In-flight messages due for delivery
        var dueMsgs = Network.GetDueInFlight(Clock.Now);
        foreach (var m in dueMsgs)
        {
            var msg = m;
            enabled.Add(new EnabledTransition
            {
                Kind = TransitionKind.DeliverMessage,
                Node = msg.To,
                Key = $"msg:{msg.MessageId}",
                Label = $"deliver:{msg.From}->{msg.To}:{msg.Payload.TypeName}",
                Execute = () => Network.DeliverOneMessage(msg.MessageId)
            });
        }

        // 3. Process steps: each running process with a message in its inbox
        foreach (var p in _processes)
        {
            if (_states[p.Id] != ProcessState.Running) continue;
            if (Network.PendingCount(p.Id) > 0)
            {
                var id = p.Id;
                var proc = p;
                enabled.Add(new EnabledTransition
                {
                    Kind = TransitionKind.ProcessMessage,
                    Node = id,
                    Key = $"process:{id}",
                    Label = $"process:{id}",
                    Execute = () =>
                    {
                        if (Network.TryReceive(id, out var env) && env is not null)
                            proc.OnMessage(env);
                    }
                });
            }
        }

        // 4. Timers due at current virtual time
        var dueTimers = _timers.GetDue(Clock.Now);
        foreach (var t in dueTimers)
        {
            if (_states.TryGetValue(t.Owner, out var st) && st == ProcessState.Running)
            {
                var timer = t;
                enabled.Add(new EnabledTransition
                {
                    Kind = TransitionKind.FireTimer,
                    Node = timer.Owner,
                    Key = $"timer:{timer.Owner}:{timer.Name}:{timer.Generation}",
                    Label = $"timer:{timer.Owner}:{timer.Name}",
                    Execute = () =>
                    {
                        if (_timers.ConsumeTimer(timer.Owner, timer.Name, timer.Generation, out var consumed) && consumed is not null)
                        {
                            _byId[consumed.Owner].OnTimer(consumed.Name, consumed.Generation);
                            _metrics.TimersFired++;
                        }
                    }
                });
            }
        }

        // 5. Finite or rate-limited client driver
        if (_clientDriver is not null && _clientStep < _clientOpLimit && _clientNextTime <= Clock.Now)
        {
            var step = _clientStep;
            enabled.Add(new EnabledTransition
            {
                Kind = TransitionKind.DriveClient,
                Node = new NodeId(999),
                Key = $"client:{step}",
                Label = $"client:{step}",
                Execute = () =>
                {
                    var ctx = _contexts[_processes[0].Id];
                    _clientStep++;
                    _metrics.ClientOpsAttempted++;
                    _clientDriver(ctx, Nodes.ToArray(), step);
                    _clientNextTime = Clock.Now.Add(_clientOpInterval);
                }
            });
        }

        // Deterministic candidate ordering: sort by Kind, then Node, then Key
        enabled.Sort((a, b) =>
        {
            var c = a.Kind.CompareTo(b.Kind);
            if (c != 0) return c;
            var an = a.Node?.Value ?? -1;
            var bn = b.Node?.Value ?? -1;
            c = an.CompareTo(bn);
            if (c != 0) return c;
            return string.CompareOrdinal(a.Key, b.Key);
        });

        return enabled;
    }

    /// <summary>
    /// Executes exactly one atomic transition. If nothing is enabled at Now,
    /// advances virtual time to the earliest future event.
    /// Invariants are checked immediately after each transition.
    /// </summary>
    public bool StepOnce()
    {
        if (!_running) return false;
        if (_violations.Count > 0) return false;

        var enabled = CollectEnabled();
        if (enabled.Count == 0)
        {
            var nextMsg = Network.NextDeliveryTime();
            var nextTimer = _timers.NextFireTime();
            SimTime? nextFault = null;
            foreach (var f in _pendingFaults)
            {
                if (f.AtTime is SimTime ft && ft > Clock.Now)
                {
                    if (nextFault is null || ft < nextFault.Value)
                        nextFault = ft;
                }
            }

            SimTime? next = null;
            if (nextMsg is { } m && m > Clock.Now) next = m;
            if (nextTimer is { } t && t > Clock.Now)
                next = next is { } n ? (t < n ? t : n) : t;
            if (nextFault is { } fTime)
                next = next is { } n2 ? (fTime < n2 ? fTime : n2) : fTime;

            if (_clientDriver is not null && _clientStep < _clientOpLimit && _clientNextTime > Clock.Now)
            {
                next = next is { } n3 ? (_clientNextTime < n3 ? _clientNextTime : n3) : _clientNextTime;
            }

            if (next is null || next <= Clock.Now)
                return false; // Quiescent!

            Clock.AdvanceTo(next.Value);
            _metrics.VirtualTimeAdvanced = Clock.Now.Ticks;

            enabled = CollectEnabled();
            if (enabled.Count == 0)
                return false;
        }

        var candidateKeys = enabled.Select(e => e.Key).ToArray();
        var pick = _oracle.Choose(
            ToSchedulingPointKind(enabled[0].Kind),
            enabled.Count,
            enabled[0].Node,
            enabled[0].Label,
            enabled[0].Key,
            candidateKeys);

        if (pick < 0 || pick >= enabled.Count)
            throw new InvalidOperationException($"Oracle pick {pick} out of range for {enabled.Count} candidates.");

        var transition = enabled[pick];
        transition.Execute();
        StepsExecuted++;

        // Sync network metrics
        _metrics.MessagesSent = Network.MessagesSent;
        _metrics.MessagesDelivered = Network.MessagesDelivered;
        _metrics.MessagesDropped = Network.MessagesDropped;
        _metrics.MessagesDuplicated = Network.MessagesDuplicated;
        _metrics.MessagesPending = Network.InFlight.Count;

        // Verify safety invariants after every transition
        var violations = CheckAll(_invariants);
        if (violations.Count > 0)
        {
            _violations.AddRange(violations);
            return false;
        }

        return true;
    }

    /// <summary>Runs the simulation up to maxSteps or until a violation or quiescence.</summary>
    public RunClassification Run(int maxSteps)
    {
        StartAll();
        for (var i = 0; i < maxSteps; i++)
        {
            if (_violations.Count > 0)
                return RunClassification.SafetyViolation;

            if (!StepOnce())
            {
                return _violations.Count > 0
                    ? RunClassification.SafetyViolation
                    : RunClassification.Quiescent;
            }
        }

        return _violations.Count > 0
            ? RunClassification.SafetyViolation
            : RunClassification.StepBoundExhausted;
    }

    public IReadOnlyList<InvariantViolation> CheckAll(IEnumerable<IInvariant> globals)
    {
        _metrics.InvariantChecksExecuted++;
        var list = new List<InvariantViolation>();
        foreach (var p in _processes)
            list.AddRange(p.CheckLocalInvariants());
        foreach (var inv in globals)
            list.AddRange(inv.Check(this));
        return list;
    }

    public string ComputeStateDigest()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"t={Clock.Now.Ticks},steps={StepsExecuted}");
        foreach (var p in _processes.OrderBy(p => p.Id.Value))
        {
            sb.Append($"{p.Id}:st={_states[p.Id]}:inbox={Network.PendingCount(p.Id)}");
            if (p is IStateDigestProvider sdp && sdp.GetStateDigest() is { } digest)
            {
                sb.Append($":{digest}");
            }
            sb.AppendLine();
        }
        sb.AppendLine($"net:sent={Network.MessagesSent}:deliv={Network.MessagesDelivered}:inflight={Network.InFlight.Count}");
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
        return Convert.ToHexString(bytes)[..16].ToLowerInvariant();
    }

    public void Reset()
    {
        _processes.Clear();
        _byId.Clear();
        _states.Clear();
        _contexts.Clear();
        _clockSkews.Clear();
        _eventLog.Clear();
        _timers.Reset();
        _pendingFaults.Clear();
        _invariants.Clear();
        _violations.Clear();
        Network.Reset();
        Storage.Reset();
        Clock.Reset();
        StepsExecuted = 0;
        _clientStep = 0;
        _clientDriver = null;
        _running = false;
        _nextFaultId = 1;
        if (_oracle is RecordingOracle rec)
            rec.Reset();
    }

    public string DumpState()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"seed={Seed} time={Clock.Now} steps={StepsExecuted} digest={ComputeStateDigest()}");
        sb.AppendLine($"net: sent={Network.MessagesSent} dropped={Network.MessagesDropped} delivered={Network.MessagesDelivered} inFlight={Network.InFlight.Count}");
        foreach (var p in _processes)
            sb.AppendLine($"  {p.Id} state={_states[p.Id]} inbox={Network.PendingCount(p.Id)} skew={GetClockSkew(p.Id)}");
        return sb.ToString();
    }

    private void ApplyFault(FaultEvent fault)
    {
        _pendingFaults.Remove(fault);
        _metrics.FaultsApplied++;
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
                RecordEvent(fault.Target ?? new NodeId(0), "fault", $"partition {string.Join(",", fault.PartitionA)} || {string.Join(",", fault.PartitionB)}");
                break;
            case FaultKind.Heal:
                Network.HealPartitions();
                RecordEvent(new NodeId(0), "fault", "heal");
                break;
            case FaultKind.DropMessage:
                Network.DropMessages(fault.Target, fault.Secondary);
                RecordEvent(fault.Target ?? new NodeId(0), "fault", "drop");
                break;
            case FaultKind.ClockSkew when fault.Target is { } target && fault.DelayTicks is long d:
                _clockSkews[target] = _clockSkews.GetValueOrDefault(target) + d;
                RecordEvent(target, "fault", $"clock-skew {d} total={_clockSkews[target]}");
                break;
            default:
                RecordEvent(fault.Target ?? new NodeId(0), "fault", fault.Kind.ToString());
                break;
        }
    }

    private static SchedulingPointKind ToSchedulingPointKind(TransitionKind kind) => kind switch
    {
        TransitionKind.ApplyFault => SchedulingPointKind.Fault,
        TransitionKind.DeliverMessage => SchedulingPointKind.MessageDeliver,
        TransitionKind.ProcessMessage => SchedulingPointKind.MessageProcess,
        TransitionKind.FireTimer => SchedulingPointKind.TimerFire,
        TransitionKind.DriveClient => SchedulingPointKind.ClientOperation,
        _ => SchedulingPointKind.TaskResume
    };
}
