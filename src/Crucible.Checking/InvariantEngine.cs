namespace Crucible.Checking;

using Crucible.Abstractions;

/// <summary>Composable invariant predicates over cluster views and histories.</summary>
public sealed class InvariantEngine
{
    private readonly List<IInvariant> _invariants = new();

    public InvariantEngine Add(IInvariant invariant)
    {
        _invariants.Add(invariant);
        return this;
    }

    public InvariantEngine AddRange(IEnumerable<IInvariant> invariants)
    {
        _invariants.AddRange(invariants);
        return this;
    }

    public IReadOnlyList<IInvariant> All => _invariants;

    public IReadOnlyList<InvariantViolation> Check(IClusterView cluster)
    {
        var violations = new List<InvariantViolation>();
        foreach (var inv in _invariants)
            violations.AddRange(inv.Check(cluster));
        return violations;
    }
}

public sealed class PredicateInvariant : IInvariant
{
    private readonly Func<IClusterView, IEnumerable<InvariantViolation>> _check;

    public PredicateInvariant(string name, Func<IClusterView, IEnumerable<InvariantViolation>> check)
    {
        Name = name;
        _check = check;
    }

    public string Name { get; }

    public IEnumerable<InvariantViolation> Check(IClusterView cluster) => _check(cluster);
}

public sealed class LinearizableHistoryInvariant<TState> : IInvariant
{
    private readonly LinearizabilityChecker<TState> _checker;
    private readonly Func<IClusterView, ExecutionHistory?> _history;

    public LinearizableHistoryInvariant(
        ISequentialSpec<TState> spec,
        Func<IClusterView, ExecutionHistory?> history)
    {
        Name = $"linearizable:{spec.Name}";
        _checker = new LinearizabilityChecker<TState>(spec);
        _history = history;
    }

    public string Name { get; }

    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        var history = _history(cluster);
        if (history is null)
            yield break;

        var result = _checker.Check(history.Snapshot());
        if (!result.IsLinearizable)
        {
            yield return new InvariantViolation
            {
                Name = Name,
                Detail = result.Detail ?? "not linearizable",
                Severity = ViolationSeverity.Fatal,
                At = cluster.Now
            };
        }
    }
}

public sealed class CounterexampleReport
{
    public required string Workload { get; init; }
    public required int Seed { get; init; }
    public required long Steps { get; init; }
    public required IReadOnlyList<InvariantViolation> Violations { get; init; }
    public ScheduleTrace? Trace { get; init; }
    public HistorySnapshot? History { get; init; }
    public string? RuntimeDump { get; init; }

    public string Render()
    {
        var lines = new List<string>
        {
            $"Counterexample: workload={Workload} seed={Seed} steps={Steps}",
            "--- violations ---"
        };
        foreach (var v in Violations)
            lines.Add(v.ToString());
        if (History is not null)
        {
            lines.Add("--- history ---");
            foreach (var h in History.Invocations)
                lines.Add(h.ToString());
        }
        if (RuntimeDump is not null)
        {
            lines.Add("--- runtime ---");
            lines.Add(RuntimeDump);
        }
        return string.Join(Environment.NewLine, lines);
    }
}
