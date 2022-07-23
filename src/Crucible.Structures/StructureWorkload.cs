namespace Crucible.Structures;

using Crucible.Abstractions;
using Crucible.Checking;
using Crucible.Runtime;
using Crucible.Runtime.Shared;

/// <summary>Stress Treiber stack with multiple worker processes under the forge.</summary>
public sealed class StackWorkload : IWorkload
{
    public string Name => "treiber-stack";
    private SimSharedMemory? _memory;
    private TreiberStack? _stack;

    public IReadOnlyList<ISimProcess> CreateProcesses(int nodeCount, WorkloadOptions options)
    {
        var workers = new List<ISimProcess>();
        for (var i = 0; i < nodeCount; i++)
        {
            var id = new NodeId(i);
            workers.Add(new StackWorker(id, () => _stack));
        }
        return workers;
    }

    public IReadOnlyList<IInvariant> GlobalInvariants =>
        new IInvariant[] { new StackLinearizabilityInvariant(() => _memory) };

    public void DriveClient(ISimContext clientContext, IReadOnlyList<NodeId> nodes, int step)
    {
        // Client is wired externally after memory is bound.
    }

    public void BindMemory(SimSharedMemory memory)
    {
        _memory = memory;
        _stack = new TreiberStack(memory);
    }

    private sealed class StackWorker : ISimProcess
    {
        private readonly Func<TreiberStack?> _getStack;

        public StackWorker(NodeId id, Func<TreiberStack?> getStack)
        {
            Id = id;
            _getStack = getStack;
        }

        public NodeId Id { get; }
        public ProcessState State { get; private set; } = ProcessState.Stopped;

        public void Start(ISimContext context)
        {
            State = ProcessState.Running;
        }

        public void OnMessage(MessageEnvelope envelope) { }

        public void OnTimer(string name, long generation)
        {
            var stack = _getStack();
            if (stack is null) return;
            var pid = (int)Id.Value;
            if (generation % 2 == 0)
                stack.Push(pid, pid * 100 + generation);
            else
                stack.TryPop(pid, out _);
        }

        public void OnCrash() => State = ProcessState.Crashed;
        public void OnRestart(ISimContext context) => State = ProcessState.Running;
        public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();
    }

    private sealed class StackLinearizabilityInvariant : IInvariant
    {
        private readonly Func<SimSharedMemory?> _memory;
        public StackLinearizabilityInvariant(Func<SimSharedMemory?> memory) { _memory = memory; Name = "stack-linearizable"; }
        public string Name { get; }
        public IEnumerable<InvariantViolation> Check(IClusterView cluster)
        {
            if (!cluster.IsQuiescent) yield break;
            var mem = _memory();
            if (mem is null) yield break;
            var snap = mem.History.Snapshot();
            if (!snap.Completed.Any()) yield break;
            var checker = new LinearizabilityChecker<List<long>>(new StackSpec());
            var res = checker.Check(snap);
            if (!res.IsLinearizable)
            {
                yield return new InvariantViolation
                {
                    Name = Name,
                    Detail = res.Detail ?? "Stack history is not linearizable",
                    Severity = ViolationSeverity.Fatal,
                    At = cluster.Now
                };
            }
        }
    }
}

public static class StructureScenario
{
    public static void Configure(Crucible.Runtime.DeterministicRuntime runtime, WorkloadOptions options)
    {
        var memory = new SimSharedMemory(runtime, new ExecutionHistory());
        var workload = new StackWorkload();
        workload.BindMemory(memory);
        foreach (var p in workload.CreateProcesses(options.NodeCount, options))
        {
            runtime.Register(p);
            runtime.SetTimer(p.Id, "tick", 1 + p.Id.Value);
        }
        runtime.SetClientDriver((ctx, nodes, step) =>
        {
            foreach (var n in nodes)
                ctx.Network.Send(new NodeId(999), n, new PingMessage(step));
        });
    }
}
