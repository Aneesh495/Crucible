namespace Crucible.Concurrency;

using Crucible.Abstractions;
using Crucible.Runtime;

/// <summary>
/// Probabilistic Concurrency Testing (PCT) explorer.
/// Assigns stable priorities to schedulable actors, and drops priority at d-1 randomly
/// chosen priority change points, systematically finding concurrency bugs.
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
        var oracle = new PctOracle(rng, _options.PctDepth, _options.MaxSteps);
        var runtime = new DeterministicRuntime(seed, oracle);

        foreach (var inv in invariants)
            runtime.RegisterInvariant(inv);

        builder(runtime, workloadOptions);
        var classification = runtime.Run(_options.MaxSteps);

        var violations = runtime.Violations.Count > 0 ? runtime.Violations : runtime.CheckAll(invariants);
        var success = violations.Count == 0 && classification != RunClassification.StepBoundExhausted;

        var trace = new ScheduleTrace
        {
            Seed = seed,
            Workload = workloadName,
            Options = workloadOptions,
            Choices = oracle.Choices.ToArray(),
            Faults = Array.Empty<FaultEvent>(),
            StepsExecuted = runtime.StepsExecuted,
            FailureSummary = violations.Count == 0 ? null : string.Join("; ", violations.Select(v => v.ToString()))
        };

        if (violations.Count > 0 && _options.ScheduleOutputDirectory is { } dir)
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
            Classification = classification,
            Violations = violations,
            Trace = trace,
            Summary = violations.Count > 0 ? trace.FailureSummary : (classification == RunClassification.StepBoundExhausted ? "step bound exhausted" : "ok"),
            Metrics = runtime.Metrics
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
/// Genuine Burckhardt PCT Oracle:
/// - Assigns stable priorities to actors.
/// - Randomly chooses d-1 priority change points across the step bound.
/// - At change points, drops the active highest-priority actor below initial priorities.
/// - Schedules the enabled candidate with the highest actor priority.
/// </summary>
public sealed class PctOracle : IScheduleOracle
{
    private readonly ISimRandom _random;
    private readonly int _depth;
    private readonly int _maxSteps;
    private readonly List<int> _changePoints;
    private readonly Dictionary<string, int> _actorPriorities = new(StringComparer.Ordinal);
    private readonly List<ScheduleChoice> _choices = new();
    private int _step;
    private int _nextInitialPriority;

    public PctOracle(ISimRandom random, int depth, int maxSteps = 10_000)
    {
        _random = random;
        _depth = Math.Max(2, depth);
        _maxSteps = Math.Max(10, maxSteps);
        _nextInitialPriority = _depth + 100;

        // Sample d - 1 distinct change points uniformly from [1, maxSteps]
        var points = new HashSet<int>();
        var targetPoints = _depth - 1;
        var attempts = 0;
        while (points.Count < targetPoints && attempts++ < 1000)
        {
            var pt = random.Next(1, _maxSteps + 1);
            points.Add(pt);
        }
        _changePoints = points.OrderBy(p => p).ToList();
    }

    public IReadOnlyList<ScheduleChoice> Choices => _choices;
    public IReadOnlyList<int> ChangePoints => _changePoints;

    private int GetPriority(string actorKey)
    {
        if (!_actorPriorities.TryGetValue(actorKey, out var p))
        {
            p = _nextInitialPriority + _random.Next(0, 1000);
            _actorPriorities[actorKey] = p;
        }
        return p;
    }

    private static string GetActorKey(NodeId? actor, string? transitionKey, int index)
    {
        if (actor.HasValue) return $"node:{actor.Value.Value}";
        if (!string.IsNullOrEmpty(transitionKey))
        {
            var colon = transitionKey.IndexOf(':');
            return colon > 0 ? transitionKey[..colon] : transitionKey;
        }
        return $"cand:{index}";
    }

    public int Choose(
        SchedulingPointKind kind,
        int candidateCount,
        NodeId? actor,
        string? label,
        string? transitionKey = null,
        IReadOnlyList<string>? candidateKeys = null)
    {
        if (candidateCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(candidateCount));

        if (candidateCount == 1)
        {
            var chosenKey1 = candidateKeys is not null && candidateKeys.Count > 0 ? candidateKeys[0] : transitionKey;
            var choice1 = new ScheduleChoice
            {
                Step = _step++,
                Kind = kind,
                ChosenIndex = 0,
                CandidateCount = candidateCount,
                Actor = actor,
                Label = label,
                TransitionKey = chosenKey1,
                CandidateKeys = candidateKeys
            };
            _choices.Add(choice1);
            return 0;
        }

        // Apply priority change points
        var changeIdx = _changePoints.IndexOf(_step);
        if (changeIdx >= 0)
        {
            if (_actorPriorities.Count > 0)
            {
                var highest = _actorPriorities.OrderByDescending(kv => kv.Value).First();
                _actorPriorities[highest.Key] = changeIdx + 1;
            }
        }

        // Pick enabled candidate with highest actor priority
        var bestIndex = 0;
        var bestPriority = int.MinValue;

        for (var i = 0; i < candidateCount; i++)
        {
            var cKey = candidateKeys is not null && i < candidateKeys.Count ? candidateKeys[i] : transitionKey;
            var aKey = GetActorKey(actor, cKey, i);
            var pri = GetPriority(aKey);

            if (pri > bestPriority)
            {
                bestPriority = pri;
                bestIndex = i;
            }
        }

        var chosenKey = candidateKeys is not null && bestIndex < candidateKeys.Count ? candidateKeys[bestIndex] : transitionKey;
        var choice = new ScheduleChoice
        {
            Step = _step++,
            Kind = kind,
            ChosenIndex = bestIndex,
            CandidateCount = candidateCount,
            Actor = actor,
            Label = label,
            TransitionKey = chosenKey,
            CandidateKeys = candidateKeys
        };
        _choices.Add(choice);
        return bestIndex;
    }

    public void Record(ScheduleChoice choice) => _choices.Add(choice);
}
