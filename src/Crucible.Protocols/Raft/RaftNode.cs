namespace Crucible.Protocols.Raft;

using Crucible.Abstractions;

public sealed class RaftNode : ISimProcess, IStateDigestProvider
{
    private readonly WorkloadOptions _options;
    private readonly int _clusterSize;
    private ISimContext? _ctx;
    private RaftPersistentState _state = new();
    private RaftRole _role = RaftRole.Follower;
    private long _leaderId = -1;
    private long _heartbeatInterval;
    private readonly Dictionary<long, long> _nextIndex = new();
    private readonly Dictionary<long, long> _matchIndex = new();
    private readonly Dictionary<(long clientId, long seq), bool> _clientDedup = new();
    private readonly Dictionary<long, (NodeId from, ClientRequest req)> _pendingClientRequests = new();
    private readonly HashSet<NodeId> _grantedVotes = new();

    public RaftNode(NodeId id, int clusterSize, WorkloadOptions options)
    {
        Id = id;
        _clusterSize = clusterSize;
        _options = options;
    }

    public NodeId Id { get; }
    public ProcessState State { get; private set; } = ProcessState.Stopped;
    public RaftRole Role => _role;
    public RaftPersistentState Persistent => _state;

    public string? GetStateDigest() =>
        $"term={Persistent.CurrentTerm}:role={Role}:log={Persistent.LastLogIndex}:commit={Persistent.CommitIndex}";
    public long LeaderId => _leaderId;
    public IReadOnlyDictionary<string, string> StateMachine => _state.StateMachine;

    public void TakeSnapshot(long snapshotIndex, Dictionary<string, string> stateMachineData)
    {
        var term = _state.GetEntryTerm(snapshotIndex);
        _state.InstallSnapshotData(snapshotIndex, term, stateMachineData);
    }

    public void Start(ISimContext context)
    {
        _ctx = context;
        State = ProcessState.Running;
        _state = RaftPersistentState.Load(context.Storage, Id);
        BecomeFollower(context, _state.CurrentTerm);
        ResetElectionTimer(context);
    }

    public void OnCrash()
    {
        State = ProcessState.Crashed;
        _ctx?.CancelTimer("election");
        _ctx?.CancelTimer("heartbeat");
        _role = RaftRole.Follower;
        _grantedVotes.Clear();
        _pendingClientRequests.Clear();
    }

    public void OnRestart(ISimContext context)
    {
        State = ProcessState.Running;
        _ctx = context;
        _state = RaftPersistentState.Load(context.Storage, Id);
        _role = RaftRole.Follower;
        _grantedVotes.Clear();
        _pendingClientRequests.Clear();

        // Replay committed log onto state machine
        _state.ApplyCommitted();

        BecomeFollower(context, _state.CurrentTerm);
        ResetElectionTimer(context);
        context.RecordEvent("raft", "restarted");
    }

    public void OnMessage(MessageEnvelope envelope)
    {
        if (_ctx is null || State != ProcessState.Running) return;
        switch (envelope.Payload)
        {
            case VoteRequest vr:
                HandleVoteRequest(envelope.From, vr);
                break;
            case VoteResponse vresp:
                HandleVoteResponse(envelope.From, vresp);
                break;
            case AppendEntriesRequest ae:
                HandleAppendEntries(envelope.From, ae);
                break;
            case AppendEntriesResponse aer:
                HandleAppendEntriesResponse(envelope.From, aer);
                break;
            case ClientRequest cr:
                HandleClientRequest(envelope.From, cr);
                break;
            case InstallSnapshot snap:
                HandleSnapshot(envelope.From, snap);
                break;
        }
    }

    public void OnTimer(string name, long generation)
    {
        if (_ctx is null || State != ProcessState.Running) return;
        if (name == "election")
        {
            if (_role != RaftRole.Leader)
                StartElection();
            ResetElectionTimer(_ctx);
        }
        else if (name == "heartbeat" && _role == RaftRole.Leader)
        {
            BroadcastAppendEntries();
            _ctx.SetTimer("heartbeat", _heartbeatInterval);
        }
    }

    public IEnumerable<InvariantViolation> CheckLocalInvariants()
    {
        if (_state.LastApplied > _state.CommitIndex)
        {
            yield return new InvariantViolation
            {
                Name = "raft-applied-le-commit",
                Detail = $"applied={_state.LastApplied} commit={_state.CommitIndex}",
                Node = Id
            };
        }
        if (_state.CommitIndex > _state.LastLogIndex)
        {
            yield return new InvariantViolation
            {
                Name = "raft-commit-le-log",
                Detail = $"commit={_state.CommitIndex} log={_state.LastLogIndex}",
                Node = Id
            };
        }
    }

    private void BecomeFollower(ISimContext ctx, long term)
    {
        _role = RaftRole.Follower;
        _leaderId = -1;
        _grantedVotes.Clear();
        if (term > _state.CurrentTerm)
        {
            _state.CurrentTerm = term;
            _state.VotedFor = null;
            _state.Persist(ctx.Storage, Id);
        }
        ctx.CancelTimer("heartbeat");
    }

    private void BecomeCandidate(ISimContext ctx)
    {
        _role = RaftRole.Candidate;
        _state.CurrentTerm++;
        _state.VotedFor = Id.Value;
        _state.Persist(ctx.Storage, Id);
        _leaderId = -1;
        _grantedVotes.Clear();
        _grantedVotes.Add(Id); // vote for self
        ctx.RecordEvent("raft", $"became candidate term={_state.CurrentTerm}");
    }

    private void BecomeLeader(ISimContext ctx)
    {
        _role = RaftRole.Leader;
        _leaderId = Id.Value;
        _nextIndex.Clear();
        _matchIndex.Clear();
        for (var i = 0; i < _clusterSize; i++)
        {
            _nextIndex[i] = _state.LastLogIndex + 1;
            _matchIndex[i] = 0;
        }
        ctx.SetTimer("heartbeat", _heartbeatInterval);
        BroadcastAppendEntries();
        ctx.RecordEvent("raft", $"became leader term={_state.CurrentTerm}");
    }

    private void ResetElectionTimer(ISimContext ctx)
    {
        var min = _options.ElectionTimeoutMin;
        var max = _options.ElectionTimeoutMax;
        var timeout = ctx.Random.NextLong(min, max + 1);
        _heartbeatInterval = _options.HeartbeatInterval;
        ctx.SetTimer("election", timeout);
    }

    private void StartElection()
    {
        if (_ctx is null) return;
        BecomeCandidate(_ctx);
        var lastIdx = _state.LastLogIndex;
        var lastTerm = _state.LastLogTerm;
        for (var i = 0; i < _clusterSize; i++)
        {
            if (i == Id.Value) continue;
            var req = new VoteRequest(_state.CurrentTerm, Id.Value, lastIdx, lastTerm);
            _ctx.Network.Send(Id, new NodeId(i), req);
        }
    }

    private void HandleVoteRequest(NodeId from, VoteRequest req)
    {
        if (_ctx is null) return;
        if (req.Term > _state.CurrentTerm)
            BecomeFollower(_ctx, req.Term);

        var grant = false;
        if (req.Term == _state.CurrentTerm)
        {
            var logOk = req.LastLogTerm > _state.LastLogTerm ||
                        (req.LastLogTerm == _state.LastLogTerm && req.LastLogIndex >= _state.LastLogIndex);

            if ((_state.VotedFor is null || _state.VotedFor == req.CandidateId) && logOk)
            {
                _state.VotedFor = req.CandidateId;
                _state.Persist(_ctx.Storage, Id);
                grant = true;
                ResetElectionTimer(_ctx);
            }
        }

        SendVoteResponse(from, _state.CurrentTerm, grant);
    }

    private void SendVoteResponse(NodeId to, long term, bool granted)
    {
        _ctx?.Network.Send(Id, to, new VoteResponse(term, granted));
    }

    private void HandleVoteResponse(NodeId from, VoteResponse resp)
    {
        if (_ctx is null || _role != RaftRole.Candidate) return;

        if (resp.Term > _state.CurrentTerm)
        {
            BecomeFollower(_ctx, resp.Term);
            return;
        }

        if (resp.Term < _state.CurrentTerm || !resp.VoteGranted) return;

        // Deduplicate votes by voter node ID:
        _grantedVotes.Add(from);

        if (_grantedVotes.Count >= _clusterSize / 2 + 1)
            BecomeLeader(_ctx);
    }

    private void HandleAppendEntries(NodeId from, AppendEntriesRequest req)
    {
        if (_ctx is null) return;

        if (req.Term > _state.CurrentTerm)
            BecomeFollower(_ctx, req.Term);

        if (req.Term < _state.CurrentTerm)
        {
            SendAppendEntriesResponse(from, _state.CurrentTerm, false, _state.LastLogIndex);
            return;
        }

        _role = RaftRole.Follower;
        _leaderId = req.LeaderId;
        ResetElectionTimer(_ctx);

        // Verify PrevLogIndex and PrevLogTerm
        if (req.PrevLogIndex > 0)
        {
            if (!_state.TryGetEntry(req.PrevLogIndex, out var prev) || prev.Term != req.PrevLogTerm)
            {
                SendAppendEntriesResponse(from, _state.CurrentTerm, false, _state.LastLogIndex);
                return;
            }
        }

        // Append new entries and resolve conflicts
        foreach (var entry in req.Entries)
        {
            if (_state.TryGetEntry(entry.Index, out var existing))
            {
                if (existing.Term != entry.Term)
                {
                    _state.TruncateFrom(entry.Index);
                    _state.AppendEntry(entry);
                }
            }
            else
            {
                _state.AppendEntry(entry);
            }
        }

        if (req.LeaderCommit > _state.CommitIndex)
        {
            _state.CommitIndex = Math.Min(req.LeaderCommit, _state.LastLogIndex);
            _state.ApplyCommitted();
        }

        _state.Persist(_ctx.Storage, Id);
        SendAppendEntriesResponse(from, _state.CurrentTerm, true, _state.LastLogIndex);
    }

    private void SendAppendEntriesResponse(NodeId to, long term, bool success, long matchIndex)
    {
        _ctx?.Network.Send(Id, to, new AppendEntriesResponse(term, success, matchIndex));
    }

    private void HandleAppendEntriesResponse(NodeId from, AppendEntriesResponse resp)
    {
        if (_ctx is null || _role != RaftRole.Leader) return;

        if (resp.Term > _state.CurrentTerm)
        {
            BecomeFollower(_ctx, resp.Term);
            return;
        }

        if (resp.Term < _state.CurrentTerm) return;

        if (!resp.Success)
        {
            // Backtrack nextIndex for this follower and retry
            var currentNext = _nextIndex.GetValueOrDefault(from.Value, _state.LastLogIndex + 1);
            _nextIndex[from.Value] = Math.Max(1, currentNext - 1);
            SendAppendEntriesTo(from);
            return;
        }

        _matchIndex[from.Value] = Math.Max(_matchIndex.GetValueOrDefault(from.Value, 0), resp.MatchIndex);
        _nextIndex[from.Value] = resp.MatchIndex + 1;
        AdvanceCommitIndex();
    }

    private void AdvanceCommitIndex()
    {
        for (var n = _state.LastLogIndex; n > _state.CommitIndex; n--)
        {
            if (_state.TryGetEntry(n, out var e) && e.Term == _state.CurrentTerm)
            {
                var count = 1; // leader itself
                for (var i = 0; i < _clusterSize; i++)
                {
                    if (i == Id.Value) continue;
                    if (_matchIndex.TryGetValue(i, out var m) && m >= n)
                        count++;
                }
                if (count >= _clusterSize / 2 + 1)
                {
                    _state.CommitIndex = n;
                    _state.ApplyCommitted();
                    _state.Persist(_ctx!.Storage, Id);
                    NotifyCommittedClients();
                    break;
                }
            }
        }
    }

    private void NotifyCommittedClients()
    {
        var committedIndices = _pendingClientRequests.Keys
            .Where(idx => idx <= _state.CommitIndex)
            .OrderBy(idx => idx)
            .ToList();

        foreach (var idx in committedIndices)
        {
            var (clientNode, req) = _pendingClientRequests[idx];
            _clientDedup[(req.ClientId, req.Seq)] = true;
            ReplyClient(clientNode, req, true, null);
            _pendingClientRequests.Remove(idx);
        }
    }

    private void BroadcastAppendEntries()
    {
        if (_ctx is null) return;
        for (var i = 0; i < _clusterSize; i++)
        {
            if (i == Id.Value) continue;
            SendAppendEntriesTo(new NodeId(i));
        }
    }

    private void SendAppendEntriesTo(NodeId peer)
    {
        if (_ctx is null) return;
        var next = _nextIndex.GetValueOrDefault(peer.Value, _state.LastLogIndex + 1);

        // If follower is behind snapshot, send InstallSnapshot
        if (_state.SnapshotIndex > 0 && next <= _state.SnapshotIndex)
        {
            var snap = new InstallSnapshot(
                _state.CurrentTerm,
                Id.Value,
                _state.SnapshotIndex,
                _state.SnapshotTerm,
                new Dictionary<string, string>(_state.StateMachine));
            _ctx.Network.Send(Id, peer, snap);
            return;
        }

        var prevIdx = next - 1;
        long prevTerm = 0;
        if (prevIdx > 0 && _state.TryGetEntry(prevIdx, out var prev))
            prevTerm = prev.Term;

        var entries = new List<LogEntry>();
        for (var idx = next; idx <= _state.LastLogIndex; idx++)
        {
            if (_state.TryGetEntry(idx, out var e))
                entries.Add(e);
        }

        var req = new AppendEntriesRequest(
            _state.CurrentTerm,
            Id.Value,
            prevIdx,
            prevTerm,
            entries,
            _state.CommitIndex);
        _ctx.Network.Send(Id, peer, req);
    }

    private void HandleClientRequest(NodeId from, ClientRequest req)
    {
        if (_ctx is null) return;
        if (_role != RaftRole.Leader)
        {
            if (_leaderId >= 0)
                _ctx.Network.Send(Id, new NodeId((int)_leaderId), req);
            return;
        }

        var key = (req.ClientId, req.Seq);
        if (_clientDedup.ContainsKey(key))
        {
            ReplyClient(from, req, true, null);
            return;
        }

        var entry = new LogEntry(_state.CurrentTerm, _state.LastLogIndex + 1, req.Command);
        _state.AppendEntry(entry);
        _state.Persist(_ctx.Storage, Id);
        _pendingClientRequests[entry.Index] = (from, req);

        BroadcastAppendEntries();
        // NOTE: Client is NOT replied to here. Reply is sent only in NotifyCommittedClients()
        // once the entry has been replicated to a quorum and committed.
    }

    private void ReplyClient(NodeId to, ClientRequest req, bool ok, string? error)
    {
        _ctx?.Network.Send(Id, to, new ClientResponse(req.Command, req.ClientId, req.Seq, ok, error));
    }

    private void HandleSnapshot(NodeId from, InstallSnapshot snap)
    {
        if (_ctx is null) return;
        if (snap.Term > _state.CurrentTerm)
            BecomeFollower(_ctx, snap.Term);

        if (snap.Term >= _state.CurrentTerm)
        {
            _state.InstallSnapshotData(snap.LastIncludedIndex, snap.LastIncludedTerm, snap.StateMachineData);
            _state.Persist(_ctx.Storage, Id);
            _nextIndex[from.Value] = _state.LastLogIndex + 1;
        }
    }
}
