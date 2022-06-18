namespace Crucible.Protocols.Raft;

using Crucible.Abstractions;

public enum RaftRole { Follower, Candidate, Leader }

public sealed record LogEntry(long Term, long Index, string Command) : IMessage
{
    public string TypeName => nameof(LogEntry);
}

public sealed record VoteRequest(long Term, long CandidateId, long LastLogIndex, long LastLogTerm) : IMessage
{
    public string TypeName => nameof(VoteRequest);
}

public sealed record VoteResponse(long Term, bool VoteGranted) : IMessage
{
    public string TypeName => nameof(VoteResponse);
}

public sealed record AppendEntriesRequest(
    long Term,
    long LeaderId,
    long PrevLogIndex,
    long PrevLogTerm,
    IReadOnlyList<LogEntry> Entries,
    long LeaderCommit) : IMessage
{
    public string TypeName => nameof(AppendEntriesRequest);
}

public sealed record AppendEntriesResponse(long Term, bool Success, long MatchIndex) : IMessage
{
    public string TypeName => nameof(AppendEntriesResponse);
}

public sealed record ClientRequest(string Command, long ClientId, long Seq) : IMessage
{
    public string TypeName => nameof(ClientRequest);
}

public sealed record ClientResponse(string Command, long ClientId, long Seq, bool Ok, string? Error) : IMessage
{
    public string TypeName => nameof(ClientResponse);
}

public sealed record InstallSnapshot(long Term, long LeaderId, long LastIncludedIndex, long LastIncludedTerm, string StateMachineKey) : IMessage
{
    public string TypeName => nameof(InstallSnapshot);
}
