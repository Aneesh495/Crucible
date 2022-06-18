namespace Crucible.Protocols.Raft;

using System.Text.Json;
using Crucible.Abstractions;

/// <summary>Durable Raft state persisted through simulated storage.</summary>
public sealed class RaftPersistentState
{
    public long CurrentTerm { get; set; }
    public long? VotedFor { get; set; }
    public List<LogEntry> Log { get; } = new();
    public Dictionary<string, string> StateMachine { get; } = new(StringComparer.Ordinal);
    public long CommitIndex { get; set; }
    public long LastApplied { get; set; }

    public long LastLogIndex => Log.Count;
    public long LastLogTerm => Log.Count == 0 ? 0 : Log[^1].Term;

    public void Persist(ISimStorage storage, NodeId node)
    {
        var dto = new RaftPersistDto
        {
            CurrentTerm = CurrentTerm,
            VotedFor = VotedFor,
            Log = Log.Select(e => new LogEntryDto(e.Term, e.Index, e.Command)).ToList(),
            CommitIndex = CommitIndex,
            LastApplied = LastApplied,
            StateMachine = new Dictionary<string, string>(StateMachine, StringComparer.Ordinal)
        };
        var bytes = JsonSerializer.SerializeToUtf8Bytes(dto);
        storage.Write(node, "raft.state", bytes);
    }

    public static RaftPersistentState Load(ISimStorage storage, NodeId node)
    {
        var state = new RaftPersistentState();
        if (!storage.TryRead(node, "raft.state", out var bytes))
            return state;

        var dto = JsonSerializer.Deserialize<RaftPersistDto>(bytes);
        if (dto is null) return state;
        state.CurrentTerm = dto.CurrentTerm;
        state.VotedFor = dto.VotedFor;
        state.Log.Clear();
        foreach (var e in dto.Log)
            state.Log.Add(new LogEntry(e.Term, e.Index, e.Command));
        state.CommitIndex = dto.CommitIndex;
        state.LastApplied = dto.LastApplied;
        state.StateMachine.Clear();
        foreach (var kv in dto.StateMachine)
            state.StateMachine[kv.Key] = kv.Value;
        return state;
    }

    public void AppendEntry(LogEntry entry) => Log.Add(entry);

    public bool TryGetEntry(long index, out LogEntry entry)
    {
        if (index <= 0 || index > Log.Count)
        {
            entry = null!;
            return false;
        }
        entry = Log[(int)(index - 1)];
        return true;
    }

    public void TruncateFrom(long index)
    {
        if (index <= 0)
        {
            Log.Clear();
            return;
        }
        while (Log.Count >= index)
            Log.RemoveAt(Log.Count - 1);
    }

    public void ApplyCommitted()
    {
        while (LastApplied < CommitIndex)
        {
            LastApplied++;
            if (!TryGetEntry(LastApplied, out var entry))
                continue;
            ApplyCommand(entry.Command);
        }
    }

    public void ApplyCommand(string command)
    {
        // Simple KV: "set key value" / "del key"
        var parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) return;
        switch (parts[0])
        {
            case "set" when parts.Length >= 3:
                StateMachine[parts[1]] = parts[2];
                break;
            case "del" when parts.Length >= 2:
                StateMachine.Remove(parts[1]);
                break;
        }
    }

    private sealed class RaftPersistDto
    {
        public long CurrentTerm { get; set; }
        public long? VotedFor { get; set; }
        public List<LogEntryDto> Log { get; set; } = new();
        public long CommitIndex { get; set; }
        public long LastApplied { get; set; }
        public Dictionary<string, string> StateMachine { get; set; } = new();
    }

    private sealed record LogEntryDto(long Term, long Index, string Command);
}
