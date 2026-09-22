namespace Crucible.Protocols.Crdt;

using Crucible.Abstractions;

public sealed record OrSetTag(string Node, long Counter);

public sealed record OrSetAdd(OrSetTag Tag, string Element) : IMessage
{
    public string TypeName => nameof(OrSetAdd);
}

public sealed record OrSetRemove(OrSetTag Tag, string Element) : IMessage
{
    public string TypeName => nameof(OrSetRemove);
}

public sealed record OrSetState(Dictionary<string, HashSet<OrSetTag>> Added, HashSet<(string, OrSetTag)> Removed) : IMessage
{
    public string TypeName => nameof(OrSetState);
}

public sealed class OrSetDocument
{
    private readonly Dictionary<string, HashSet<OrSetTag>> _added = new(StringComparer.Ordinal);
    private readonly HashSet<(string E, OrSetTag T)> _removed = new();

    public void ApplyAdd(OrSetTag tag, string element)
    {
        if (!_added.TryGetValue(element, out var set))
        {
            set = new HashSet<OrSetTag>();
            _added[element] = set;
        }
        set.Add(tag);
    }

    public void ApplyRemove(OrSetTag tag, string element)
    {
        _removed.Add((element, tag));
    }

    public void Merge(OrSetDocument other)
    {
        foreach (var (e, tags) in other._added)
        {
            if (!_added.TryGetValue(e, out var set))
            {
                set = new HashSet<OrSetTag>();
                _added[e] = set;
            }
            foreach (var t in tags) set.Add(t);
        }
        foreach (var r in other._removed) _removed.Add(r);
    }

    public HashSet<string> Read()
    {
        var result = new HashSet<string>(StringComparer.Ordinal);
        foreach (var (e, tags) in _added)
        {
            if (tags.Any(t => !_removed.Contains((e, t))))
                result.Add(e);
        }
        return result;
    }

    public OrSetState ExportState()
    {
        var added = _added.ToDictionary(
            kv => kv.Key,
            kv => new HashSet<OrSetTag>(kv.Value),
            StringComparer.Ordinal);
        return new OrSetState(added, new HashSet<(string, OrSetTag)>(_removed));
    }

    public void ImportState(OrSetState state)
    {
        _added.Clear();
        foreach (var (k, v) in state.Added)
            _added[k] = new HashSet<OrSetTag>(v);
        _removed.Clear();
        foreach (var r in state.Removed) _removed.Add(r);
    }
}

public sealed class OrSetNode : ISimProcess
{
    private readonly int _peers;
    private ISimContext? _ctx;
    private readonly OrSetDocument _doc = new();
    private long _counter;

    public OrSetNode(NodeId id, int peers)
    {
        Id = id;
        _peers = peers;
    }

    public NodeId Id { get; }
    public ProcessState State { get; private set; } = ProcessState.Stopped;
    public OrSetDocument Document => _doc;

    public void Start(ISimContext context)
    {
        _ctx = context;
        State = ProcessState.Running;
        context.SetTimer("sync", 5);
    }

    public void OnCrash() => State = ProcessState.Crashed;
    public void OnRestart(ISimContext context)
    {
        State = ProcessState.Running;
        _ctx = context;
    }

    public void OnMessage(MessageEnvelope envelope)
    {
        switch (envelope.Payload)
        {
            case OrSetAdd add:
                _doc.ApplyAdd(add.Tag, add.Element);
                break;
            case OrSetRemove rem:
                _doc.ApplyRemove(rem.Tag, rem.Element);
                break;
            case OrSetState st:
                var other = new OrSetDocument();
                other.ImportState(st);
                _doc.Merge(other);
                break;
        }
    }

    public void OnTimer(string name, long generation)
    {
        if (_ctx is null || name != "sync") return;
        BroadcastState();
        _ctx.SetTimer("sync", 5 + _ctx.Random.NextLong(0, 3));
    }

    public void AddLocal(string element)
    {
        if (_ctx is null) return;
        var tag = new OrSetTag(Id.ToString(), ++_counter);
        _doc.ApplyAdd(tag, element);
        var msg = new OrSetAdd(tag, element);
        for (var i = 0; i < _peers; i++)
        {
            if (i == Id.Value) continue;
            _ctx.Network.Send(Id, new NodeId(i), msg);
        }
    }

    private void BroadcastState()
    {
        if (_ctx is null) return;
        var st = _doc.ExportState();
        for (var i = 0; i < _peers; i++)
        {
            if (i == Id.Value) continue;
            _ctx.Network.Send(Id, new NodeId(i), st);
        }
    }

    public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();
}

public sealed class OrSetConvergenceInvariant : IInvariant
{
    public string Name => "or-set-convergence";
    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        HashSet<string>? reference = null;
        foreach (var p in cluster.AllProcesses.OfType<OrSetNode>())
        {
            if (cluster.GetState(p.Id) != ProcessState.Running) continue;
            var view = p.Document.Read();
            if (reference is null) reference = view;
            else if (!reference.SetEquals(view))
            {
                yield return new InvariantViolation
                {
                    Name = Name,
                    Detail = $"divergent read on {p.Id}",
                    Severity = ViolationSeverity.Fatal,
                    At = cluster.Now
                };
            }
        }
    }
}

public sealed class OrSetWorkload : IWorkload
{
    public string Name => "or-set";
    public IReadOnlyList<ISimProcess> CreateProcesses(int nodeCount, WorkloadOptions options)
    {
        var list = new List<ISimProcess>();
        for (var i = 0; i < nodeCount; i++)
            list.Add(new OrSetNode(new NodeId(i), nodeCount));
        return list;
    }

    public IReadOnlyList<IInvariant> GlobalInvariants { get; } = new IInvariant[] { new OrSetConvergenceInvariant() };

    public void DriveClient(ISimContext ctx, IReadOnlyList<NodeId> nodes, int step)
    {
        if (nodes.Count == 0) return;
        var n = nodes[step % nodes.Count];
        ctx.Network.Send(new NodeId(999), n, new OrSetAdd(new OrSetTag("client", step), $"e{step % 7}"));
    }
}
