namespace Crucible.Protocols.Crdt;

using Crucible.Abstractions;

public sealed record RgaId(long Origin, long Counter);

public sealed record RgaInsert(RgaId Id, RgaId? After, char Char) : IMessage
{
    public string TypeName => nameof(RgaInsert);
}

public sealed record RgaState(List<RgaInsert> Ops) : IMessage { public string TypeName => nameof(RgaState); }

public sealed class RgaDocument
{
    private readonly List<RgaInsert> _ops = new();

    public void Apply(RgaInsert op)
    {
        if (_ops.Any(o => o.Id.Equals(op.Id))) return;
        _ops.Add(op);
    }

    public void Merge(RgaDocument other)
    {
        foreach (var op in other._ops) Apply(op);
    }

    public string Materialize()
    {
        var sorted = _ops.OrderBy(o => o.Id.Origin).ThenBy(o => o.Id.Counter).ToList();
        return new string(sorted.Select(o => o.Char).ToArray());
    }
}

public sealed class RgaNode : ISimProcess
{
    private readonly int _peers;
    private ISimContext? _ctx;
    private readonly RgaDocument _doc = new();
    private long _counter;

    public RgaNode(NodeId id, int peers) { Id = id; _peers = peers; }
    public NodeId Id { get; }
    public ProcessState State { get; private set; } = ProcessState.Stopped;
    public RgaDocument Document => _doc;

    public void Start(ISimContext context) { _ctx = context; State = ProcessState.Running; }
    public void OnCrash() => State = ProcessState.Crashed;
    public void OnRestart(ISimContext context) { State = ProcessState.Running; _ctx = context; }

    public void OnMessage(MessageEnvelope envelope)
    {
        if (envelope.Payload is RgaInsert ins) _doc.Apply(ins);
        else if (envelope.Payload is RgaState st)
        {
            var other = new RgaDocument();
            foreach (var op in st.Ops) other.Apply(op);
            _doc.Merge(other);
        }
    }

    public void OnTimer(string name, long generation) { }

    public void Insert(char c)
    {
        if (_ctx is null) return;
        var id = new RgaId(Id.Value, ++_counter);
        var op = new RgaInsert(id, null, c);
        _doc.Apply(op);
        for (var i = 0; i < _peers; i++)
            if (i != Id.Value) _ctx.Network.Send(Id, new NodeId(i), op);
    }

    public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();
}

public sealed class RgaConvergenceInvariant : IInvariant
{
    public string Name => "rga-convergence";
    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        string? reference = null;
        foreach (var p in cluster.AllProcesses.OfType<RgaNode>())
        {
            if (cluster.GetState(p.Id) != ProcessState.Running) continue;
            var s = p.Document.Materialize();
            reference ??= s;
            if (s != reference)
                yield return new InvariantViolation { Name = Name, Detail = $"text mismatch on {p.Id}", At = cluster.Now };
        }
    }
}

public sealed class RgaWorkload : IWorkload
{
    public string Name => "rga";
    public IReadOnlyList<ISimProcess> CreateProcesses(int nodeCount, WorkloadOptions options)
    {
        var list = new List<ISimProcess>();
        for (var i = 0; i < nodeCount; i++) list.Add(new RgaNode(new NodeId(i), nodeCount));
        return list;
    }
    public IReadOnlyList<IInvariant> GlobalInvariants { get; } = new IInvariant[] { new RgaConvergenceInvariant() };
    public void DriveClient(ISimContext ctx, IReadOnlyList<NodeId> nodes, int step)
    {
        if (nodes.Count == 0) return;
        var id = new RgaId(999, step);
        ctx.Network.Send(new NodeId(999), nodes[step % nodes.Count], new RgaInsert(id, null, (char)('a' + step % 26)));
    }
}
