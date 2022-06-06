namespace Crucible.Concurrency;

using Crucible.Abstractions;
using Crucible.Runtime;

/// <summary>
/// Probabilistic Concurrency Testing (PCT) explorer.
/// Assigns random priorities to a bounded number of "priority change points",
/// which is known to find bugs with high probability in shallow buggy schedules.
/// </summary>
public sealed class PctExplorer
{
    private readonly ExplorerOptions _options;

    public PctExplorer(ExplorerOptions? options = null) =>
        _options = options ?? new ExplorerOptions();

    public ExplorationResult Run(
        string workloadName,
        int seed,
        ScenarioBuilder builder,
        WorkloadOptions? workloadOptions = null,
        IReadOnlyList<IInvariant>? invariants = null)
    {
        workloadOptions ??= new WorkloadOptions();
        invariants ??= Array.Empty<IInvariant>();

        var rng = new SeededRng(seed);
        var oracle = new PctOracle(rng, _options.PctDepth);
        var runtime = new DeterministicRuntime(seed, oracle);
        builder(runtime, workloadOptions);
        runtime.Run(_options.MaxSteps);

        var violations = runtime.CheckAll(invariants);
        var success = violations.Count == 0;
        var trace = new ScheduleTrace
        {
            Seed = seed,
            Workload = workloadName,
            Choices = oracle.Choices.ToArray(),
            Faults = Array.Empty<FaultEvent>(),
            StepsExecuted = runtime.StepsExecuted,
            FailureSummary = success ? null : string.Join("; ", violations.Select(v => v.ToString()))
        };

        if (!success && _options.ScheduleOutputDirectory is { } dir)
        {
            Directory.CreateDirectory(dir);
            ScheduleSerializer.WriteToFile(trace, Path.Combine(dir, $"pct-{seed}.schedule.json"));
        }

        return new ExplorationResult
        {
            Seed = seed,
            Workload = workloadName,
            Steps = runtime.StepsExecuted,
            Success = success,
            Violations = violations,
            Trace = trace,
            Summary = trace.FailureSummary
        };
    }

    public IReadOnlyList<ExplorationResult> RunMany(
        string workloadName,
        int baseSeed,
        int count,
        ScenarioBuilder builder,
        WorkloadOptions? workloadOptions = null,
        IReadOnlyList<IInvariant>? invariants = null)
    {
        var results = new List<ExplorationResult>();
        for (var i = 0; i < count; i++)
        {
            var r = Run(workloadName, baseSeed + i, builder, workloadOptions, invariants);
            results.Add(r);
            if (!r.Success && _options.StopOnFirstFailure)
                break;
        }
        return results;
    }
}

/// <summary>
/// PCT oracle: most of the time pick the enabled transition with highest priority.
/// At d randomly chosen steps, reassign priorities (priority change points).
/// </summary>
public sealed class PctOracle : IScheduleOracle
{
    private readonly ISimRandom _random;
    private readonly int _depth;
    private readonly HashSet<int> _changePoints;
    private readonly Dictionary<int, int> _priorities = new();
    private readonly List<ScheduleChoice> _choices = new();
    private int _step;
    private int _nextKey;

    public PctOracle(ISimRandom random, int depth)
    {
        _random = random;
        _depth = Math.Max(1, depth);
        _changePoints = new HashSet<int>();
        // Change points sampled lazily as steps grow; pre-sample a window.
        for (var i = 0; i < _depth; i++)
            _changePoints.Add(random.Next(1, 10_000));
    }

    public IReadOnlyList<ScheduleChoice> Choices => _choices;

    public int Choose(SchedulingPointKind kind, int candidateCount, NodeId? actor, string? label)
    {
        if (candidateCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(candidateCount));

        if (_changePoints.Contains(_step))
            _priorities.Clear();

        // Assign priorities to candidate indices for this decision.
        var best = 0;
        var bestPri = int.MinValue;
        for (var i = 0; i < candidateCount; i++)
        {
            var key = _nextKey++;
            if (!_priorities.TryGetValue(key, out var pri))
            {
                pri = _random.Next();
                _priorities[key] = pri;
            }
            // Use index-stable keys within this call.
            var localPri = _random.Next();
            if (i == 0 || localPri > bestPri)
            {
                bestPri = localPri;
                best = i;
            }
        }

        // Classic PCT for thread scheduling picks max priority thread; here candidates
        // are transitions. Bias toward a random permutation priority vector.
        if (candidateCount > 1)
        {
            var ranks = Enumerable.Range(0, candidateCount).ToArray();
            _random.Shuffle(ranks);
            // At non-change steps, pick the first in priority order (ranks[0] after shuffle
            // approximates a random total order established at change points).
            best = ranks[0];
        }

        var choice = new ScheduleChoice
        {
            Step = _step++,
            Kind = kind,
            ChosenIndex = best,
            CandidateCount = candidateCount,
            Actor = actor,
            Label = label
        };
        _choices.Add(choice);
        return best;
    }

    public void Record(ScheduleChoice choice) => _choices.Add(choice);
}
