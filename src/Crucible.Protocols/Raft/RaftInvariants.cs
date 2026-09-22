namespace Crucible.Protocols.Raft;

using Crucible.Abstractions;

public sealed class RaftElectionSafetyInvariant : IInvariant
{
    public string Name => "raft-election-safety";

    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        var leaders = new Dictionary<long, List<NodeId>>();
        foreach (var p in cluster.AllProcesses)
        {
            if (p is not RaftNode rn || cluster.GetState(p.Id) != ProcessState.Running)
                continue;
            if (rn.Role == RaftRole.Leader)
            {
                if (!leaders.TryGetValue(rn.Persistent.CurrentTerm, out var list))
                {
                    list = new List<NodeId>();
                    leaders[rn.Persistent.CurrentTerm] = list;
                }
                list.Add(p.Id);
            }
        }
        foreach (var (term, nodes) in leaders)
        {
            if (nodes.Count > 1)
            {
                yield return new InvariantViolation
                {
                    Name = Name,
                    Detail = $"multiple leaders in term {term}: {string.Join(",", nodes)}",
                    Severity = ViolationSeverity.Fatal,
                    At = cluster.Now
                };
            }
        }
    }
}

public sealed class RaftLogMatchingInvariant : IInvariant
{
    public string Name => "raft-log-matching";

    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        var logs = cluster.AllProcesses
            .OfType<RaftNode>()
            .Where(p => cluster.GetState(p.Id) == ProcessState.Running)
            .Select(p => (p.Id, p.Persistent.Log))
            .ToArray();
        if (logs.Length < 2) yield break;

        var maxLen = logs.Max(l => l.Log.Count);
        for (var idx = 1; idx <= maxLen; idx++)
        {
            long? term = null;
            string? cmd = null;
            foreach (var (_, log) in logs)
            {
                if (idx > log.Count) continue;
                var e = log[idx - 1];
                if (term is null)
                {
                    term = e.Term;
                    cmd = e.Command;
                }
                else if (e.Term != term || e.Command != cmd)
                {
                    yield return new InvariantViolation
                    {
                        Name = Name,
                        Detail = $"log diverged at index {idx}",
                        Severity = ViolationSeverity.Fatal,
                        At = cluster.Now
                    };
                    yield break;
                }
            }
        }
    }
}

public sealed class RaftStateMachineSafetyInvariant : IInvariant
{
    public string Name => "raft-state-machine-safety";

    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        Dictionary<string, string>? reference = null;
        foreach (var p in cluster.AllProcesses.OfType<RaftNode>())
        {
            if (cluster.GetState(p.Id) != ProcessState.Running) continue;
            var sm = p.Persistent.StateMachine;
            if (reference is null)
            {
                reference = new Dictionary<string, string>(sm, StringComparer.Ordinal);
                continue;
            }
            foreach (var kv in reference)
            {
                if (!sm.TryGetValue(kv.Key, out var v) || v != kv.Value)
                {
                    yield return new InvariantViolation
                    {
                        Name = Name,
                        Detail = $"key {kv.Key} differs on {p.Id}",
                        Severity = ViolationSeverity.Fatal,
                        At = cluster.Now
                    };
                }
            }
        }
    }
}
