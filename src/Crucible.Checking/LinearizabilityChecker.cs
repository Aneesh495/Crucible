namespace Crucible.Checking;

using Crucible.Abstractions;

/// <summary>
/// Wing–Gong linearizability checker for finite histories.
/// Searches for a total order of completed invocations consistent with the sequential spec
/// and real-time precedence (if A returns before B begins, A must precede B in the order).
/// </summary>
public sealed class LinearizabilityChecker<TState>
{
    private readonly ISequentialSpec<TState> _spec;

    public LinearizabilityChecker(ISequentialSpec<TState> spec) => _spec = spec;

    public LinearizabilityResult Check(HistorySnapshot history)
    {
        var completed = history.Completed.OrderBy(i => i.Id).ToArray();
        if (completed.Length == 0)
            return LinearizabilityResult.Ok(_spec.Name);

        var pending = history.Pending.ToArray();
        if (pending.Length > 0)
        {
            return LinearizabilityResult.Fail(
                _spec.Name,
                $"{pending.Length} invocation(s) still pending.");
        }

        var n = completed.Length;
        var order = new int[n];
        for (var i = 0; i < n; i++) order[i] = i;

        // Build precedence constraints: i must come before j if completed[i] returns before completed[j] starts.
        var mustPrecede = new List<(int before, int after)>();
        for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
        {
            if (i == j) continue;
            var a = completed[i];
            var b = completed[j];
            if (a.ReturnTime is SimTime ra && ra <= b.CallTime)
                mustPrecede.Add((i, j));
        }

        var perm = new int[n];
        var used = new bool[n];
        var ok = Search(completed, mustPrecede, perm, used, 0, _spec.InitialState, out var witness);
        return ok
            ? LinearizabilityResult.Ok(_spec.Name, witness)
            : LinearizabilityResult.Fail(_spec.Name, "No linearization exists for this history.");
    }

    private static bool ResultsMatch(object? actual, object? expected)
    {
        if (actual is null && expected is null) return true;
        if (actual is null || expected is null) return false;
        return actual.Equals(expected);
    }

    private bool Search(
        HistoryInvocation[] completed,
        List<(int before, int after)> precedence,
        int[] perm,
        bool[] used,
        int depth,
        TState state,
        out List<long>? witness)
    {
        if (depth == completed.Length)
        {
            witness = perm.Select(i => completed[i].Id).ToList();
            return true;
        }

        for (var i = 0; i < completed.Length; i++)
        {
            if (used[i]) continue;

            // Check precedence: all predecessors of i must already be placed.
            if (precedence.Any(p => p.after == i && !used[p.before]))
                continue;

            if (!_spec.TryApply(state, completed[i], out var next, out var specResult))
                continue;
            if (!ResultsMatch(completed[i].Result, specResult))
                continue;

            used[i] = true;
            perm[depth] = i;
            if (Search(completed, precedence, perm, used, depth + 1, next, out witness))
                return true;
            used[i] = false;
        }

        witness = null;
        return false;
    }
}

public sealed class LinearizabilityResult
{
    public string SpecName { get; init; } = "";
    public bool IsLinearizable { get; init; }
    public string? Detail { get; init; }
    public IReadOnlyList<long>? WitnessOrder { get; init; }

    public static LinearizabilityResult Ok(string name, IReadOnlyList<long>? witness = null) =>
        new() { SpecName = name, IsLinearizable = true, WitnessOrder = witness };

    public static LinearizabilityResult Fail(string name, string detail) =>
        new() { SpecName = name, IsLinearizable = false, Detail = detail };
}
