namespace Crucible.Protocols.Raft;

using System.Text.Json;
using Crucible.Abstractions;

/// <summary>
/// Raft durable state model.
/// Per the Raft consensus specification (Ongaro &amp; Ousterhout, §5.2, Figure 2):
/// - Durable state (persisted to stable storage before responding to RPCs):
///     * CurrentTerm: latest term server has seen
///     * VotedFor: candidateId that received vote in current term (or null)
///     * Log: log entries (each with Term, Index, Command)
///     * SnapshotIndex / SnapshotTerm / SnapshotData: compaction snapshot if taken
/// - Volatile state on all servers (not persisted across crashes):
///     * CommitIndex: initialized to 0 (or SnapshotIndex) on reboot
///     * LastApplied: initialized to 0 (or SnapshotIndex) on reboot
///     * StateMachine: re-applied from durable log (and snapshot) on recovery
/// </summary>
public sealed class RaftPersistentState
{
    public long CurrentTerm { get; set; }
    public long? VotedFor { get; set; }
    public List<LogEntry> Log { get; } = new();
    public Dictionary<string, string> StateMachine { get; } = new(StringComparer.Ordinal);

    // Volatile state:
    public long CommitIndex { get; set; }
    public long LastApplied { get; set; }

    // Snapshot state:
    public long SnapshotIndex { get; set; }
    public long SnapshotTerm { get; set; }
    public long LastIncludedIndex => SnapshotIndex;
    public long LastIncludedTerm => SnapshotTerm;

    public long LastLogIndex => Log.Count > 0 ? Log[^1].Index : SnapshotIndex;
    public long LastLogTerm => Log.Count > 0 ? Log[^1].Term : SnapshotTerm;

    public void Persist(ISimStorage storage, NodeId node)
    {
        var dto = new RaftPersistDto
        {
            CurrentTerm = CurrentTerm,
            VotedFor = VotedFor,
            Log = Log.Select(e => new LogEntryDto(e.Term, e.Index, e.Command)).ToList(),
            SnapshotIndex = SnapshotIndex,
            SnapshotTerm = SnapshotTerm,
            SnapshotData = new Dictionary<string, string>(StateMachine, StringComparer.Ordinal)
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
        state.SnapshotIndex = dto.SnapshotIndex;
        state.SnapshotTerm = dto.SnapshotTerm;

        state.Log.Clear();
        foreach (var e in dto.Log)
            state.Log.Add(new LogEntry(e.Term, e.Index, e.Command));

        // Restore snapshot into state machine
        state.StateMachine.Clear();
        if (dto.SnapshotData is not null)
        {
            foreach (var kv in dto.SnapshotData)
                state.StateMachine[kv.Key] = kv.Value;
        }

        // CommitIndex and LastApplied reset to snapshot baseline on crash/restart
        state.CommitIndex = state.SnapshotIndex;
        state.LastApplied = state.SnapshotIndex;

        return state;
    }

    public void AppendEntry(LogEntry entry) => Log.Add(entry);

    public bool TryGetEntry(long index, out LogEntry entry)
    {
        if (index <= SnapshotIndex)
        {
            entry = null!;
            return false;
        }

        var offset = (int)(index - SnapshotIndex - 1);
        if (offset < 0 || offset >= Log.Count)
        {
            entry = null!;
            return false;
        }

        entry = Log[offset];
        return true;
    }

    public long GetEntryTerm(long index)
    {
        if (index == 0) return 0;
        if (index <= SnapshotIndex) return SnapshotTerm;
        var offset = (int)(index - SnapshotIndex - 1);
        return (offset >= 0 && offset < Log.Count) ? Log[offset].Term : 0;
    }

    public void TruncateFrom(long index)
    {
        if (index <= SnapshotIndex + 1)
        {
            Log.Clear();
            return;
        }

        var offset = (int)(index - SnapshotIndex - 1);
        while (Log.Count > offset)
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

    public void InstallSnapshotData(long lastIndex, long lastTerm, Dictionary<string, string> data)
    {
        SnapshotIndex = lastIndex;
        SnapshotTerm = lastTerm;

        // Truncate any entries covered by snapshot
        TruncateFrom(lastIndex + 1);

        StateMachine.Clear();
        foreach (var kv in data)
            StateMachine[kv.Key] = kv.Value;

        if (CommitIndex < lastIndex) CommitIndex = lastIndex;
        if (LastApplied < lastIndex) LastApplied = lastIndex;
    }

    private sealed class RaftPersistDto
    {
        public long CurrentTerm { get; set; }
        public long? VotedFor { get; set; }
        public List<LogEntryDto> Log { get; set; } = new();
        public long SnapshotIndex { get; set; }
        public long SnapshotTerm { get; set; }
        public Dictionary<string, string>? SnapshotData { get; set; }
    }

    private sealed record LogEntryDto(long Term, long Index, string Command);
}
