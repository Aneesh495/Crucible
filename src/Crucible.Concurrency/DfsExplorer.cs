namespace Crucible.Concurrency;

using Crucible.Abstractions;
using Crucible.Runtime;

/// <summary>
/// Bounded DFS over scheduling choices. At each frontier where candidateCount &gt; 1,
/// enqueue alternate indices. Prefix of the schedule is replayed via FrontierOracle.
/// </summary>
public sealed class DfsExplorer
{
    private readonly ExplorerOptions _options;

    public DfsExplorer(ExplorerOptions? options = null) =>
        _options = options ?? new ExplorerOptions();

    public IReadOnlyList<ExplorationResult> Explore(
        string workloadName,
        int seed,
        ScenarioBuilder builder,
        WorkloadOptions? workloadOptions = null,
        IReadOnlyList<IInvariant>? invariants = null)
    {
        workloadOptions ??= new WorkloadOptions();
        invariants ??= Array.Empty<IInvariant>();

        var results = new List<ExplorationResult>();
        var stack = new Stack<Frontier>();
        stack.Push(new Frontier(Array.Empty<ScheduleChoice>(), 0, 0));

        var schedules = 0;
        while (stack.Count > 0 && schedules < _options.MaxSchedules)
        {
            var frontier = stack.Pop();
            if (frontier.Depth > _options.DepthBound)
                continue;

            var fallback = new SeededRng(seed ^ (schedules * 397) ^ frontier.Depth);
            var oracle = new FrontierOracle(
                frontier.Prefix,
                frontier.Prefix.Count,
                frontier.ForcedIndex,
                fallback);

            var runtime = new DeterministicRuntime(seed, oracle);
            builder(runtime, workloadOptions);
            runtime.Run(_options.MaxSteps);

            schedules++;
            var violations = runtime.CheckAll(invariants);
            var choices = oracle.Choices.ToArray();
            var success = violations.Count == 0;
            var trace = new ScheduleTrace
            {
                Seed = seed,
                Workload = workloadName,
                Choices = choices,
                StepsExecuted = runtime.StepsExecuted,
                FailureSummary = success ? null : string.Join("; ", violations.Select(v => v.ToString()))
            };

            var result = new ExplorationResult
            {
                Seed = seed,
                Workload = workloadName,
                Steps = runtime.StepsExecuted,
                Success = success,
                Violations = violations,
                Trace = trace,
                Summary = trace.FailureSummary
            };
            results.Add(result);

            if (!success)
            {
                if (_options.ScheduleOutputDirectory is { } dir)
                {
                    Directory.CreateDirectory(dir);
                    ScheduleSerializer.WriteToFile(
                        trace,
                        Path.Combine(dir, $"dfs-{seed}-{schedules}.schedule.json"));
                }
                if (_options.StopOnFirstFailure)
                    break;
            }

            // Expand new frontiers: for each step with multiple candidates beyond the prefix,
            // push alternate choices.
            for (var i = frontier.Prefix.Count; i < choices.Length; i++)
            {
                var c = choices[i];
                if (c.CandidateCount <= 1) continue;
                for (var alt = 0; alt < c.CandidateCount; alt++)
                {
                    if (alt == c.ChosenIndex) continue;
                    var prefix = choices.Take(i).ToArray();
                    stack.Push(new Frontier(prefix, alt, frontier.Depth + 1));
                }
                // Only expand the first branching point per schedule to keep the tree manageable.
                break;
            }
        }

        return results;
    }

    private readonly record struct Frontier(
        IReadOnlyList<ScheduleChoice> Prefix,
        int ForcedIndex,
        int Depth);
}

/// <summary>Replays a previously recorded schedule and re-checks invariants.</summary>
public sealed class ReplayExplorer
{
    public ExplorationResult Replay(
        ScheduleTrace trace,
        ScenarioBuilder builder,
        WorkloadOptions? workloadOptions = null,
        IReadOnlyList<IInvariant>? invariants = null)
    {
        workloadOptions ??= new WorkloadOptions();
        invariants ??= Array.Empty<IInvariant>();

        var oracle = new ReplayOracle(trace.Choices);
        var runtime = new DeterministicRuntime(trace.Seed, oracle);
        foreach (var fault in trace.Faults)
            runtime.RegisterFault(fault);

        builder(runtime, workloadOptions);
        runtime.Run(checked((int)Math.Max(trace.StepsExecuted + 100, trace.StepsExecuted)));

        var violations = runtime.CheckAll(invariants);
        return new ExplorationResult
        {
            Seed = trace.Seed,
            Workload = trace.Workload,
            Steps = runtime.StepsExecuted,
            Success = violations.Count == 0,
            Violations = violations,
            Trace = trace,
            Summary = violations.Count == 0
                ? "replay ok"
                : string.Join("; ", violations.Select(v => v.ToString()))
        };
    }
}

/// <summary>Random-walk explorer: many seeds, recording oracle each time.</summary>
public sealed class RandomExplorer
{
    private readonly ExplorerOptions _options;

    public RandomExplorer(ExplorerOptions? options = null) =>
        _options = options ?? new ExplorerOptions();

    public IReadOnlyList<ExplorationResult> Run(
        string workloadName,
        int baseSeed,
        int count,
        ScenarioBuilder builder,
        WorkloadOptions? workloadOptions = null,
        IReadOnlyList<IInvariant>? invariants = null)
    {
        workloadOptions ??= new WorkloadOptions();
        invariants ??= Array.Empty<IInvariant>();
        var results = new List<ExplorationResult>();

        for (var i = 0; i < count; i++)
        {
            var seed = baseSeed + i;
            var rng = new SeededRng(seed);
            var oracle = new RecordingOracle(rng);
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
                StepsExecuted = runtime.StepsExecuted,
                FailureSummary = success ? null : string.Join("; ", violations.Select(v => v.ToString()))
            };

            if (!success && _options.ScheduleOutputDirectory is { } dir)
            {
                Directory.CreateDirectory(dir);
                ScheduleSerializer.WriteToFile(trace, Path.Combine(dir, $"rnd-{seed}.schedule.json"));
            }

            results.Add(new ExplorationResult
            {
                Seed = seed,
                Workload = workloadName,
                Steps = runtime.StepsExecuted,
                Success = success,
                Violations = violations,
                Trace = trace,
                Summary = trace.FailureSummary
            });

            if (!success && _options.StopOnFirstFailure)
                break;
        }

        return results;
    }
}
