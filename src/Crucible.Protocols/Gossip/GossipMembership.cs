namespace Crucible.Protocols.Gossip;

using Crucible.Abstractions;

public sealed record GossipPing(IReadOnlyList<string> Membership, long Epoch) : IMessage
{
    public string TypeName => nameof(GossipPing);
}

public sealed record GossipAck(IReadOnlyList<string> Membership, long Epoch) : IMessage
{
    public string TypeName => nameof(GossipAck);
}

public sealed class GossipNode : ISimProcess
{
    private readonly int _peers;
    private ISimContext? _ctx;
    private readonly HashSet<string> _members = new(StringComparer.Ordinal);
    private long _epoch;

    public GossipNode(NodeId id, int peers)
    {
        Id = id;
        _peers = peers;
        _members.Add(id.ToString());
    }

    public NodeId Id { get; }
    public ProcessState State { get; private set; } = ProcessState.Stopped;
    public IReadOnlyCollection<string> Members => _members;

    public void Start(ISimContext context)
    {
        _ctx = context;
        State = ProcessState.Running;
        context.SetTimer("gossip", 4);
    }

    public void OnCrash()
    {
        State = ProcessState.Crashed;
        _members.Remove(Id.ToString());
    }

    public void OnRestart(ISimContext context)
    {
        State = ProcessState.Running;
        _ctx = context;
        _members.Add(Id.ToString());
    }

    public void OnMessage(MessageEnvelope envelope)
    {
        switch (envelope.Payload)
        {
            case GossipPing ping:
                Merge(ping.Membership, ping.Epoch);
                _ctx?.Network.Send(Id, envelope.From, new GossipAck(_members.ToArray(), _epoch));
                break;
            case GossipAck ack:
                Merge(ack.Membership, ack.Epoch);
                break;
        }
    }

    public void OnTimer(string name, long generation)
    {
        if (_ctx is null || name != "gossip") return;
        var target = _ctx.Random.Next(_peers);
        if (target == Id.Value) target = (target + 1) % _peers;
        _ctx.Network.Send(Id, new NodeId(target), new GossipPing(_members.ToArray(), _epoch));
        _ctx.SetTimer("gossip", 4 + _ctx.Random.NextLong(0, 2));
    }

    private void Merge(IReadOnlyList<string> incoming, long epoch)
    {
        if (epoch < _epoch) return;
        if (epoch > _epoch)
        {
            _members.Clear();
            _epoch = epoch;
        }
        foreach (var m in incoming) _members.Add(m);
    }

    public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();
}

public sealed class GossipMonotonicMembershipInvariant : IInvariant
{
    public string Name => "gossip-no-ghost-self";
    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        foreach (var p in cluster.AllProcesses.OfType<GossipNode>())
        {
            if (cluster.GetState(p.Id) == ProcessState.Running && !p.Members.Contains(p.Id.ToString()))
            {
                yield return new InvariantViolation
                {
                    Name = Name,
                    Detail = $"running node {p.Id} not in its own membership view",
                    Node = p.Id,
                    At = cluster.Now
                };
            }
        }
    }
}

public sealed class GossipWorkload : IWorkload
{
    public string Name => "gossip";
    public IReadOnlyList<ISimProcess> CreateProcesses(int nodeCount, WorkloadOptions options)
    {
        var list = new List<ISimProcess>();
        for (var i = 0; i < nodeCount; i++) list.Add(new GossipNode(new NodeId(i), nodeCount));
        return list;
    }
    public IReadOnlyList<IInvariant> GlobalInvariants { get; } = new IInvariant[] { new GossipMonotonicMembershipInvariant() };
    public void DriveClient(ISimContext ctx, IReadOnlyList<NodeId> nodes, int step) { }
}
