namespace Crucible.Protocols.Crdt;

using Crucible.Abstractions;

public sealed record LwwValue(long Timestamp, string Node, string Value);

public sealed record LwwUpdate(LwwValue Value) : IMessage { public string TypeName => nameof(LwwUpdate); }

public sealed class LwwRegister
{
    private LwwValue _value = new(0, "", "");

    public void Merge(LwwValue incoming)
    {
        if (incoming.Timestamp > _value.Timestamp ||
            (incoming.Timestamp == _value.Timestamp && string.CompareOrdinal(incoming.Node, _value.Node) > 0))
            _value = incoming;
    }

    public string Read() => _value.Value;
    public LwwValue Snapshot() => _value;
}

public sealed class LwwNode : ISimProcess
{
    private readonly int _peers;
    private ISimContext? _ctx;
    private readonly LwwRegister _reg = new();
    private long _clock;

    public LwwNode(NodeId id, int peers) { Id = id; _peers = peers; }
    public NodeId Id { get; }
    public ProcessState State { get; private set; } = ProcessState.Stopped;
    public LwwRegister Register => _reg;

    public void Start(ISimContext context) { _ctx = context; State = ProcessState.Running; }
    public void OnCrash() => State = ProcessState.Crashed;
    public void OnRestart(ISimContext context) { State = ProcessState.Running; _ctx = context; }

    public void OnMessage(MessageEnvelope envelope)
    {
        if (envelope.Payload is LwwUpdate u) _reg.Merge(u.Value);
    }

    public void OnTimer(string name, long generation) { }

    public void Write(string value)
    {
        if (_ctx is null) return;
        var v = new LwwValue(++_clock, Id.ToString(), value);
        _reg.Merge(v);
        var msg = new LwwUpdate(v);
        for (var i = 0; i < _peers; i++)
            if (i != Id.Value) _ctx.Network.Send(Id, new NodeId(i), msg);
    }

    public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();
}

public sealed class LwwConvergenceInvariant : IInvariant
{
    public string Name => "lww-convergence";
    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        string? reference = null;
        foreach (var p in cluster.AllProcesses.OfType<LwwNode>())
        {
            if (cluster.GetState(p.Id) != ProcessState.Running) continue;
            var v = p.Register.Read();
            reference ??= v;
            if (v != reference)
                yield return new InvariantViolation { Name = Name, Detail = $"value mismatch on {p.Id}", At = cluster.Now };
        }
    }
}

public sealed class LwwRegisterWorkload : IWorkload
{
    public string Name => "lww-register";
    public IReadOnlyList<ISimProcess> CreateProcesses(int nodeCount, WorkloadOptions options)
    {
        var list = new List<ISimProcess>();
        for (var i = 0; i < nodeCount; i++) list.Add(new LwwNode(new NodeId(i), nodeCount));
        return list;
    }
    public IReadOnlyList<IInvariant> GlobalInvariants { get; } = new IInvariant[] { new LwwConvergenceInvariant() };
    public void DriveClient(ISimContext ctx, IReadOnlyList<NodeId> nodes, int step)
    {
        if (nodes.Count == 0) return;
        ctx.Network.Send(new NodeId(999), nodes[step % nodes.Count], new LwwUpdate(new LwwValue(step, "client", $"v{step}")));
    }
}
