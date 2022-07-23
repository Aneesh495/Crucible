namespace Crucible.Protocols.Raft;

using Crucible.Abstractions;

/// <summary>
/// Election Safety (§5.2):
/// At most one leader can be elected in a given term across the entire history of the simulation.
/// </summary>
public sealed class RaftElectionSafetyInvariant : IInvariant
{
    private readonly Dictionary<long, HashSet<NodeId>> _leadersByTerm = new();

    public string Name => "raft-election-safety";

    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        foreach (var p in cluster.AllProcesses)
        {
            if (p is not RaftNode rn) continue;
            if (rn.Role == RaftRole.Leader)
            {
                var term = rn.Persistent.CurrentTerm;
                if (!_leadersByTerm.TryGetValue(term, out var set))
                {
                    set = new HashSet<NodeId>();
                    _leadersByTerm[term] = set;
                }
                set.Add(rn.Id);
                if (set.Count > 1)
                {
                    yield return new InvariantViolation
                    {
                        Name = Name,
                        Detail = $"multiple leaders elected in term {term}: {string.Join(",", set)}",
                        Severity = ViolationSeverity.Fatal,
                        At = cluster.Now
                    };
                }
            }
        }
    }
}

/// <summary>
/// Log Matching (§5.3):
/// If two logs contain an entry with the same index and term,
/// then the logs are identical in all entries up through the given index.
/// </summary>
public sealed class RaftLogMatchingInvariant : IInvariant
{
    public string Name => "raft-log-matching";

    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        var nodes = cluster.AllProcesses.OfType<RaftNode>().ToArray();
        for (var i = 0; i < nodes.Length; i++)
            for (var j = i + 1; j < nodes.Length; j++)
            {
                var logA = nodes[i].Persistent.Log;
                var logB = nodes[j].Persistent.Log;
                var minLen = Math.Min(logA.Count, logB.Count);

                for (var k = 1; k <= minLen; k++)
                {
                    var eA = logA[k - 1];
                    var eB = logB[k - 1];
                    if (eA.Index == eB.Index && eA.Term == eB.Term)
                    {
                        // Entries match at index k; all prior entries 1..k-1 must be identical
                        for (var m = 1; m < k; m++)
                        {
                            var prevA = logA[m - 1];
                            var prevB = logB[m - 1];
                            if (prevA.Term != prevB.Term || prevA.Command != prevB.Command)
                            {
                                yield return new InvariantViolation
                                {
                                    Name = Name,
                                    Detail = $"Log matching violated between {nodes[i].Id} and {nodes[j].Id}: matched at index {k} term {eA.Term}, but differed at prefix index {m}",
                                    Severity = ViolationSeverity.Fatal,
                                    At = cluster.Now
                                };
                                yield break;
                            }
                        }
                    }
                }
            }
    }
}

/// <summary>
/// State Machine Safety (§5.4.3):
/// If a server has applied a log entry at a given index to its state machine,
/// no other server will ever apply a different log entry for the same index.
/// </summary>
public sealed class RaftStateMachineSafetyInvariant : IInvariant
{
    private readonly Dictionary<long, (long term, string command)> _appliedByIndex = new();

    public string Name => "raft-state-machine-safety";

    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        foreach (var rn in cluster.AllProcesses.OfType<RaftNode>())
        {
            var log = rn.Persistent.Log;
            var applied = rn.Persistent.LastApplied;
            for (var idx = 1; idx <= applied; idx++)
            {
                if (rn.Persistent.TryGetEntry(idx, out var entry))
                {
                    if (_appliedByIndex.TryGetValue(idx, out var existing))
                    {
                        if (existing.term != entry.Term || existing.command != entry.Command)
                        {
                            yield return new InvariantViolation
                            {
                                Name = Name,
                                Detail = $"State machine safety violated at index {idx}: {rn.Id} applied (term {entry.Term}, '{entry.Command}') but earlier node applied (term {existing.term}, '{existing.command}')",
                                Severity = ViolationSeverity.Fatal,
                                At = cluster.Now
                            };
                        }
                    }
                    else
                    {
                        _appliedByIndex[idx] = (entry.Term, entry.Command);
                    }
                }
            }
        }
    }
}

/// <summary>
/// Leader Completeness (§5.4):
/// If a log entry is committed in a given term, then that entry will be present in the logs
/// of the leaders for all higher-numbered terms.
/// </summary>
public sealed class RaftLeaderCompletenessInvariant : IInvariant
{
    private readonly Dictionary<long, (long term, string command)> _committedEntries = new();

    public string Name => "raft-leader-completeness";

    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        // 1. Record newly committed entries
        foreach (var rn in cluster.AllProcesses.OfType<RaftNode>())
        {
            var commit = rn.Persistent.CommitIndex;
            for (var idx = 1; idx <= commit; idx++)
            {
                if (rn.Persistent.TryGetEntry(idx, out var entry))
                {
                    if (!_committedEntries.ContainsKey(idx))
                        _committedEntries[idx] = (entry.Term, entry.Command);
                }
            }
        }

        // 2. Check each leader: it must contain all committed entries from lower terms
        foreach (var rn in cluster.AllProcesses.OfType<RaftNode>())
        {
            if (rn.Role != RaftRole.Leader) continue;
            var leaderTerm = rn.Persistent.CurrentTerm;

            foreach (var (idx, committed) in _committedEntries)
            {
                if (committed.term < leaderTerm)
                {
                    if (!rn.Persistent.TryGetEntry(idx, out var leaderEntry) ||
                        leaderEntry.Term != committed.term ||
                        leaderEntry.Command != committed.command)
                    {
                        yield return new InvariantViolation
                        {
                            Name = Name,
                            Detail = $"Leader completeness violated: leader {rn.Id} in term {leaderTerm} missing committed entry at index {idx} (committed in term {committed.term})",
                            Severity = ViolationSeverity.Fatal,
                            At = cluster.Now
                        };
                    }
                }
            }
        }
    }
}
