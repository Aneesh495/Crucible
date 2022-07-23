namespace Crucible.Concurrency;

using Crucible.Abstractions;
using Crucible.Runtime;

/// <summary>
/// Systematic bounded DFS explorer over discrete scheduling choices.
/// Enforces strict prefix replay, canonical choices beyond frontier, and systematic branching.
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

        var visitedDigests = new HashSet<string>(StringComparer.Ordinal);
        var schedules = 0;

        while (stack.Count > 0 && schedules < _options.MaxSchedules)
        {
            var frontier = stack.Pop();
            if (frontier.Depth > _options.DepthBound)
                continue;

            var oracle = new FrontierOracle(
                frontier.Prefix,
                frontier.Prefix.Count,
                frontier.ForcedIndex);

            var runtime = new DeterministicRuntime(seed, oracle);
            foreach (var inv in invariants)
                runtime.RegisterInvariant(inv);

            builder(runtime, workloadOptions);

            RunClassification classification;
            try
            {
                classification = runtime.Run(_options.MaxSteps);
            }
            catch (ReplayDivergenceException div)
            {
                results.Add(new ExplorationResult
                {
                    Seed = seed,
                    Workload = workloadName,
                    Steps = runtime.StepsExecuted,
                    Success = false,
                    Classification = RunClassification.ReplayDivergence,
                    Summary = div.Message,
                    Metrics = runtime.Metrics
                });
                continue;
            }

            schedules++;
            var digest = runtime.ComputeStateDigest();
            visitedDigests.Add(digest);

            var violations = runtime.Violations.Count > 0 ? runtime.Violations : runtime.CheckAll(invariants);
            var choices = oracle.Choices.ToArray();
            var success = violations.Count == 0 && classification != RunClassification.StepBoundExhausted;

            var trace = new ScheduleTrace
            {
                Seed = seed,
                Workload = workloadName,
                Options = workloadOptions,
                Choices = choices,
                Faults = Array.Empty<FaultEvent>(),
                StepsExecuted = runtime.StepsExecuted,
                FailureSummary = violations.Count == 0 ? null : string.Join("; ", violations.Select(v => v.ToString()))
            };

            var result = new ExplorationResult
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

            // Enumerate alternate branching choices beyond prefix
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

/// <summary>Replays a previously recorded schedule and strictly verifies reproducibility.</summary>
public sealed class ReplayExplorer
{
    public ExplorationResult Replay(
        ScheduleTrace trace,
        ScenarioBuilder builder,
        WorkloadOptions? workloadOptions = null,
        IReadOnlyList<IInvariant>? invariants = null,
        bool throwOnDivergence = true)
    {
        workloadOptions ??= trace.Options ?? new WorkloadOptions();
        invariants ??= Array.Empty<IInvariant>();

        var oracle = new ReplayOracle(trace.Choices);
        var runtime = new DeterministicRuntime(trace.Seed, oracle);

        foreach (var fault in trace.Faults)
            runtime.RegisterFault(fault);

        foreach (var inv in invariants)
            runtime.RegisterInvariant(inv);

        builder(runtime, workloadOptions);

        RunClassification classification;
        try
        {
            classification = runtime.Run(checked((int)Math.Max(trace.StepsExecuted + 10, trace.StepsExecuted)));
        }
        catch (ReplayDivergenceException div)
        {
            if (throwOnDivergence)
                throw;

            return new ExplorationResult
            {
                Seed = trace.Seed,
                Workload = trace.Workload,
                Steps = runtime.StepsExecuted,
                Success = false,
                Classification = RunClassification.ReplayDivergence,
                Summary = div.Message,
                Metrics = runtime.Metrics
            };
        }

        var violations = runtime.Violations.Count > 0 ? runtime.Violations : runtime.CheckAll(invariants);
        var success = violations.Count == 0;

        return new ExplorationResult
        {
            Seed = trace.Seed,
            Workload = trace.Workload,
            Steps = runtime.StepsExecuted,
            Success = success,
            Classification = classification,
            Violations = violations,
            Trace = trace,
            Summary = violations.Count == 0
                ? "replay ok"
                : string.Join("; ", violations.Select(v => v.ToString())),
            Metrics = runtime.Metrics
        };
    }
}

/// <summary>Random-walk explorer: explores seeded execution paths with recording oracle.</summary>
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
            var runtime = new DeterministicRuntime(seed);

            foreach (var inv in invariants)
                runtime.RegisterInvariant(inv);

            builder(runtime, workloadOptions);
            var classification = runtime.Run(_options.MaxSteps);

            var violations = runtime.Violations.Count > 0 ? runtime.Violations : runtime.CheckAll(invariants);
            var success = violations.Count == 0 && classification != RunClassification.StepBoundExhausted;

            var oracle = (RecordingOracle)runtime.Oracle;
            var trace = new ScheduleTrace
            {
                Seed = seed,
                Workload = workloadName,
                Options = workloadOptions,
                Choices = oracle.Choices.ToArray(),
                StepsExecuted = runtime.StepsExecuted,
                FailureSummary = violations.Count == 0 ? null : string.Join("; ", violations.Select(v => v.ToString()))
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
                Classification = classification,
                Violations = violations,
                Trace = trace,
                Summary = violations.Count > 0 ? trace.FailureSummary : (classification == RunClassification.StepBoundExhausted ? "step bound exhausted" : "ok"),
                Metrics = runtime.Metrics
            });

            if (!success && _options.StopOnFirstFailure)
                break;
        }

        return results;
    }
}

/// <summary>
/// Deterministic delta-debugging counterexample minimizer.
/// Reduces schedule choices while preserving the invariant failure.
/// </summary>
public static class CounterexampleMinimizer
{
    public static ScheduleTrace Minimize(
        ScheduleTrace trace,
        ScenarioBuilder builder,
        IReadOnlyList<IInvariant> invariants)
    {
        var replay = new ReplayExplorer();
        var baseline = replay.Replay(trace, builder, trace.Options, invariants, throwOnDivergence: false);
        if (baseline.Success)
            return trace; // Trace doesn't fail; cannot minimize

        // Binary search prefix truncation: find minimal prefix that still triggers the violation
        var choices = trace.Choices.ToList();
        var low = 1;
        var high = choices.Count;
        var bestCount = choices.Count;

        while (low <= high)
        {
            var mid = low + (high - low) / 2;
            var candidateTrace = new ScheduleTrace
            {
                Seed = trace.Seed,
                Workload = trace.Workload,
                Options = trace.Options,
                ChaosProfile = trace.ChaosProfile,
                Choices = choices.Take(mid).ToArray(),
                Faults = trace.Faults,
                StepsExecuted = mid,
                FailureSummary = trace.FailureSummary
            };

            var testResult = replay.Replay(candidateTrace, builder, trace.Options, invariants, throwOnDivergence: false);
            if (!testResult.Success && testResult.Classification == RunClassification.SafetyViolation)
            {
                bestCount = mid;
                high = mid - 1; // Try shorter
            }
            else
            {
                low = mid + 1; // Need longer
            }
        }

        return new ScheduleTrace
        {
            Seed = trace.Seed,
            Workload = trace.Workload,
            Options = trace.Options,
            ChaosProfile = trace.ChaosProfile,
            Choices = choices.Take(bestCount).ToArray(),
            Faults = trace.Faults,
            StepsExecuted = bestCount,
            FailureSummary = trace.FailureSummary
        };
    }
}
