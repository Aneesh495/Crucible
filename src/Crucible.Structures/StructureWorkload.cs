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

    public IReadOnlyList<ISimProcess> CreateProcesses(int nodeCount, WorkloadOptions options)
    {
        var workers = new List<ISimProcess>();
        for (var i = 0; i < nodeCount; i++)
        {
            var id = new NodeId(i);
            workers.Add(new StackWorker(id, () => _memory!));
        }
        return workers;
    }

    public IReadOnlyList<IInvariant> GlobalInvariants =>
        new IInvariant[] { new StackDepthInvariant(() => _memory) };

    public void DriveClient(ISimContext clientContext, IReadOnlyList<NodeId> nodes, int step)
    {
        // Client is wired externally after memory is bound.
    }

    public void BindMemory(SimSharedMemory memory) => _memory = memory;

    private sealed class StackWorker : ISimProcess
    {
        private readonly Func<SimSharedMemory> _memory;
        private TreiberStack? _stack;

        public StackWorker(NodeId id, Func<SimSharedMemory> memory)
        {
            Id = id;
            _memory = memory;
        }

        public NodeId Id { get; }
        public ProcessState State { get; private set; } = ProcessState.Stopped;

        public void Start(ISimContext context)
        {
            State = ProcessState.Running;
            _stack = new TreiberStack(_memory());
        }

        public void OnMessage(MessageEnvelope envelope) { }

        public void OnTimer(string name, long generation)
        {
            if (_stack is null) return;
            var pid = Id.Value;
            if (generation % 2 == 0)
                _stack.Push(pid, generation);
            else
                _stack.TryPop(pid, out _);
        }

        public void OnCrash() => State = ProcessState.Crashed;
        public void OnRestart(ISimContext context) => State = ProcessState.Running;
        public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();
    }

    private sealed class StackDepthInvariant : IInvariant
    {
        private readonly Func<SimSharedMemory?> _memory;
        public StackDepthInvariant(Func<SimSharedMemory?> memory) { _memory = memory; Name = "stack-history-recorded"; }
        public string Name { get; }
        public IEnumerable<InvariantViolation> Check(IClusterView cluster)
        {
            if (cluster.EventLog.Count < 10)
                yield break;
            if (_memory()?.History.Invocations.Count == 0)
                yield return new InvariantViolation { Name = Name, Detail = "no shared ops recorded" };
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
