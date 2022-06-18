namespace Crucible.Protocols.TwoPhaseCommit;

using Crucible.Abstractions;

public enum TwoPcPhase { Idle, Prepared, Committed, Aborted }

public sealed record TwoPcPrepare(string TxId) : IMessage { public string TypeName => nameof(TwoPcPrepare); }
public sealed record TwoPcPrepared(string TxId) : IMessage { public string TypeName => nameof(TwoPcPrepared); }
public sealed record TwoPcAbort(string TxId) : IMessage { public string TypeName => nameof(TwoPcAbort); }
public sealed record TwoPcCommit(string TxId) : IMessage { public string TypeName => nameof(TwoPcCommit); }

public sealed class TwoPcCoordinator : ISimProcess
{
    private readonly int _participants;
    private ISimContext? _ctx;
    private readonly HashSet<string> _prepared = new(StringComparer.Ordinal);
    private string? _activeTx;

    public TwoPcCoordinator(NodeId id, int participants) { Id = id; _participants = participants; }
    public NodeId Id { get; }
    public ProcessState State { get; private set; } = ProcessState.Stopped;
    public TwoPcPhase Phase { get; private set; } = TwoPcPhase.Idle;

    public void Start(ISimContext context) { _ctx = context; State = ProcessState.Running; }
    public void OnCrash() { State = ProcessState.Crashed; Phase = TwoPcPhase.Aborted; }
    public void OnRestart(ISimContext context) { State = ProcessState.Running; _ctx = context; }

    public void BeginTransaction(string txId)
    {
        if (_ctx is null) return;
        _activeTx = txId;
        _prepared.Clear();
        Phase = TwoPcPhase.Prepared;
        for (var i = 1; i <= _participants; i++)
            _ctx.Network.Send(Id, new NodeId(i), new TwoPcPrepare(txId));
    }

    public void OnMessage(MessageEnvelope envelope)
    {
        if (_ctx is null || _activeTx is null) return;
        switch (envelope.Payload)
        {
            case TwoPcPrepared p when p.TxId == _activeTx:
                _prepared.Add(envelope.From.ToString());
                if (_prepared.Count >= _participants)
                {
                    Phase = TwoPcPhase.Committed;
                    for (var i = 1; i <= _participants; i++)
                        _ctx.Network.Send(Id, new NodeId(i), new TwoPcCommit(_activeTx));
                }
                break;
            case TwoPcAbort a when a.TxId == _activeTx:
                Phase = TwoPcPhase.Aborted;
                for (var i = 1; i <= _participants; i++)
                    _ctx.Network.Send(Id, new NodeId(i), new TwoPcAbort(_activeTx));
                break;
        }
    }

    public void OnTimer(string name, long generation) { }
    public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();
}

public sealed class TwoPcParticipant : ISimProcess
{
    private ISimContext? _ctx;
    public NodeId Id { get; }
    public ProcessState State { get; private set; } = ProcessState.Stopped;
    public TwoPcPhase Phase { get; private set; } = TwoPcPhase.Idle;
    public string? TxId { get; private set; }

    public TwoPcParticipant(NodeId id) => Id = id;

    public void Start(ISimContext context) { _ctx = context; State = ProcessState.Running; }
    public void OnCrash() { State = ProcessState.Crashed; }
    public void OnRestart(ISimContext context) { State = ProcessState.Running; _ctx = context; }

    public void OnMessage(MessageEnvelope envelope)
    {
        if (_ctx is null) return;
        switch (envelope.Payload)
        {
            case TwoPcPrepare prep:
                TxId = prep.TxId;
                Phase = TwoPcPhase.Prepared;
                _ctx.Network.Send(Id, new NodeId(0), new TwoPcPrepared(prep.TxId));
                break;
            case TwoPcCommit commit when commit.TxId == TxId:
                Phase = TwoPcPhase.Committed;
                break;
            case TwoPcAbort abort:
                Phase = TwoPcPhase.Aborted;
                TxId = abort.TxId;
                break;
        }
    }

    public void OnTimer(string name, long generation) { }
    public IEnumerable<InvariantViolation> CheckLocalInvariants()
    {
        if (Phase == TwoPcPhase.Committed && TxId is null)
            yield return new InvariantViolation { Name = "2pc-committed-without-tx", Node = Id };
    }
}

public sealed class TwoPcAtomicityInvariant : IInvariant
{
    public string Name => "2pc-atomicity";
    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        var parts = cluster.AllProcesses.OfType<TwoPcParticipant>().ToArray();
        if (parts.Length == 0) yield break;
        var committed = parts.Count(p => p.Phase == TwoPcPhase.Committed);
        var aborted = parts.Count(p => p.Phase == TwoPcPhase.Aborted);
        if (committed > 0 && committed < parts.Length && aborted == 0)
        {
            yield return new InvariantViolation
            {
                Name = Name,
                Detail = $"partial commit: {committed}/{parts.Length}",
                Severity = ViolationSeverity.Fatal,
                At = cluster.Now
            };
        }
    }
}

public sealed class TwoPcWorkload : IWorkload
{
    public string Name => "two-phase-commit";
    public IReadOnlyList<ISimProcess> CreateProcesses(int nodeCount, WorkloadOptions options)
    {
        var list = new List<ISimProcess> { new TwoPcCoordinator(new NodeId(0), nodeCount - 1) };
        for (var i = 1; i < nodeCount; i++) list.Add(new TwoPcParticipant(new NodeId(i)));
        return list;
    }
    public IReadOnlyList<IInvariant> GlobalInvariants { get; } = new IInvariant[] { new TwoPcAtomicityInvariant() };
    public void DriveClient(ISimContext ctx, IReadOnlyList<NodeId> nodes, int step)
    {
        ctx.Network.Send(new NodeId(999), new NodeId(0), new TwoPcPrepare($"tx-{step}"));
    }
}
