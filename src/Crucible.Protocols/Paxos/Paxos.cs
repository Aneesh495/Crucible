namespace Crucible.Protocols.Paxos;

using Crucible.Abstractions;

public sealed record PaxosPrepare(long Ballot) : IMessage { public string TypeName => nameof(PaxosPrepare); }
public sealed record PaxosPromise(long Ballot, long AcceptedBallot, string? AcceptedValue) : IMessage
{
    public string TypeName => nameof(PaxosPromise);
}
public sealed record PaxosAccept(long Ballot, string Value) : IMessage { public string TypeName => nameof(PaxosAccept); }
public sealed record PaxosAccepted(long Ballot, string Value) : IMessage { public string TypeName => nameof(PaxosAccepted); }
public sealed record PaxosLearn(string Value) : IMessage { public string TypeName => nameof(PaxosLearn); }

public sealed class PaxosAcceptor : ISimProcess
{
    private ISimContext? _ctx;
    public NodeId Id { get; }
    public ProcessState State { get; private set; } = ProcessState.Stopped;
    public long PromisedBallot { get; private set; }
    public long AcceptedBallot { get; private set; }
    public string? AcceptedValue { get; private set; }

    public PaxosAcceptor(NodeId id) => Id = id;

    public void Start(ISimContext context) { _ctx = context; State = ProcessState.Running; }
    public void OnCrash() => State = ProcessState.Crashed;
    public void OnRestart(ISimContext context) { State = ProcessState.Running; _ctx = context; }

    public void OnMessage(MessageEnvelope envelope)
    {
        if (_ctx is null) return;
        switch (envelope.Payload)
        {
            case PaxosPrepare prep when prep.Ballot >= PromisedBallot:
                PromisedBallot = prep.Ballot;
                _ctx.Network.Send(Id, envelope.From, new PaxosPromise(prep.Ballot, AcceptedBallot, AcceptedValue));
                break;
            case PaxosAccept acc when acc.Ballot >= PromisedBallot:
                PromisedBallot = acc.Ballot;
                AcceptedBallot = acc.Ballot;
                AcceptedValue = acc.Value;
                _ctx.Network.Send(Id, envelope.From, new PaxosAccepted(acc.Ballot, acc.Value));
                break;
        }
    }

    public void OnTimer(string name, long generation) { }
    public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();
}

public sealed class PaxosProposer : ISimProcess
{
    private readonly int _acceptors;
    private ISimContext? _ctx;
    private long _ballotCounter;

    public PaxosProposer(NodeId id, int acceptors) { Id = id; _acceptors = acceptors; }
    public NodeId Id { get; }
    public ProcessState State { get; private set; } = ProcessState.Stopped;
    public string? ChosenValue { get; private set; }

    public void Start(ISimContext context)
    {
        _ctx = context;
        State = ProcessState.Running;
        context.SetTimer("propose", 3);
    }

    public void OnCrash() => State = ProcessState.Crashed;
    public void OnRestart(ISimContext context) { State = ProcessState.Running; _ctx = context; }

    public void Propose(string value)
    {
        if (_ctx is null) return;
        var ballot = ++_ballotCounter * 100 + Id.Value;
        for (var i = 0; i < _acceptors; i++)
            _ctx.Network.Send(Id, new NodeId(i), new PaxosPrepare(ballot));
        _pendingValue = value;
        _pendingBallot = ballot;
    }

    private string? _pendingValue;
    private long _pendingBallot;
    private int _promises;
    private int _accepts;

    public void OnMessage(MessageEnvelope envelope)
    {
        if (_ctx is null) return;
        switch (envelope.Payload)
        {
            case PaxosPromise prom when prom.Ballot == _pendingBallot:
                _promises++;
                if (_promises >= _acceptors / 2 + 1)
                {
                    var value = prom.AcceptedValue ?? _pendingValue ?? "default";
                    for (var i = 0; i < _acceptors; i++)
                        _ctx.Network.Send(Id, new NodeId(i), new PaxosAccept(_pendingBallot, value));
                }
                break;
            case PaxosAccepted acc when acc.Ballot == _pendingBallot:
                _accepts++;
                if (_accepts >= _acceptors / 2 + 1)
                {
                    ChosenValue = acc.Value;
                    for (var i = 0; i < _acceptors; i++)
                        _ctx.Network.Send(Id, new NodeId(i), new PaxosLearn(acc.Value));
                }
                break;
        }
    }

    public void OnTimer(string name, long generation)
    {
        if (_ctx is null || name != "propose") return;
        Propose($"v{generation}");
        _ctx.SetTimer("propose", 10);
    }

    public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();
}

public sealed class PaxosLearner : ISimProcess
{
    public NodeId Id { get; }
    public ProcessState State { get; private set; } = ProcessState.Stopped;
    public string? Learned { get; private set; }

    public PaxosLearner(NodeId id) => Id = id;
    public void Start(ISimContext context) => State = ProcessState.Running;
    public void OnCrash() => State = ProcessState.Crashed;
    public void OnRestart(ISimContext context) => State = ProcessState.Running;
    public void OnMessage(MessageEnvelope envelope)
    {
        if (envelope.Payload is PaxosLearn l) Learned = l.Value;
    }
    public void OnTimer(string name, long generation) { }
    public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();
}

public sealed class PaxosAgreementInvariant : IInvariant
{
    public string Name => "paxos-agreement";
    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        string? chosen = null;
        foreach (var a in cluster.AllProcesses.OfType<PaxosAcceptor>())
        {
            if (a.AcceptedValue is null) continue;
            chosen ??= a.AcceptedValue;
            if (a.AcceptedValue != chosen)
                yield return new InvariantViolation { Name = Name, Detail = "acceptors disagree", At = cluster.Now };
        }
    }
}

public sealed class MultiPaxosWorkload : IWorkload
{
    public string Name => "multi-paxos";
    public IReadOnlyList<ISimProcess> CreateProcesses(int nodeCount, WorkloadOptions options)
    {
        var list = new List<ISimProcess>();
        for (var i = 0; i < nodeCount; i++) list.Add(new PaxosAcceptor(new NodeId(i)));
        list.Add(new PaxosProposer(new NodeId(nodeCount), nodeCount));
        list.Add(new PaxosLearner(new NodeId(nodeCount + 1)));
        return list;
    }
    public IReadOnlyList<IInvariant> GlobalInvariants { get; } = new IInvariant[] { new PaxosAgreementInvariant() };
    public void DriveClient(ISimContext ctx, IReadOnlyList<NodeId> nodes, int step) { }
}
