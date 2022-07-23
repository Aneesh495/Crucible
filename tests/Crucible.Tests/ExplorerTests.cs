namespace Crucible.Tests;

using Crucible.Abstractions;
using Crucible.Concurrency;
using Crucible.Protocols;
using Crucible.Runtime;
using Xunit;

public class ExplorerTests
{
    [Fact]
    public void ScheduleSerializer_roundtrip()
    {
        var trace = new ScheduleTrace
        {
            Seed = 1,
            Workload = "or-set",
            Choices = new[]
            {
                new ScheduleChoice
                {
                    Step = 0,
                    Kind = SchedulingPointKind.TaskResume,
                    ChosenIndex = 0,
                    CandidateCount = 2,
                    TransitionKey = "process:n0",
                    CandidateKeys = new[] { "process:n0", "process:n1" }
                }
            },
            StepsExecuted = 10,
            Options = new WorkloadOptions { NodeCount = 3 }
        };

        var json = ScheduleSerializer.ToJson(trace);
        var back = ScheduleSerializer.FromJson(json);

        Assert.Equal(1, back.Seed);
        Assert.Equal("or-set", back.Workload);
        Assert.Single(back.Choices);
        Assert.Equal("process:n0", back.Choices[0].TransitionKey);
        Assert.Equal(3, back.Options?.NodeCount);
    }

    [Fact]
    public void Pct_explorer_runs_or_set()
    {
        var entry = ScenarioHost.RegistryView.Get("or-set");
        ScenarioBuilder builder = (rt, wo) => ScenarioHost.ConfigureWorkload(rt, entry, wo);
        var result = new PctExplorer(new ExplorerOptions { MaxSteps = 500 }).Run(
            "or-set", 42, builder, new WorkloadOptions { NodeCount = 3 }, entry.Workload.GlobalInvariants);

        Assert.True(result.Steps > 0);
        Assert.NotNull(result.Trace);
    }

    [Fact]
    public void Dfs_explorer_explores_multiple_schedules()
    {
        var entry = ScenarioHost.RegistryView.Get("or-set");
        ScenarioBuilder builder = (rt, wo) => ScenarioHost.ConfigureWorkload(rt, entry, wo);
        var explorer = new DfsExplorer(new ExplorerOptions { MaxSteps = 100, MaxSchedules = 3 });

        var results = explorer.Explore(
            "or-set",
            seed: 42,
            builder,
            new WorkloadOptions { NodeCount = 3 },
            entry.Workload.GlobalInvariants);

        Assert.InRange(results.Count, 1, 3);
        Assert.All(results, r => Assert.True(r.Steps > 0));
    }

    [Fact]
    public void Replay_divergence_is_detected_on_corrupted_schedule()
    {
        var entry = ScenarioHost.RegistryView.Get("or-set");
        ScenarioBuilder builder = (rt, wo) => ScenarioHost.ConfigureWorkload(rt, entry, wo);

        // Run one normal schedule to get a trace
        var normalResult = new PctExplorer(new ExplorerOptions { MaxSteps = 50 }).Run(
            "or-set", 42, builder, new WorkloadOptions { NodeCount = 3 }, entry.Workload.GlobalInvariants);

        Assert.NotNull(normalResult.Trace);
        Assert.NotEmpty(normalResult.Trace.Choices);

        // Corrupt the trace with an invalid transition key
        var corruptedChoices = normalResult.Trace.Choices.Select((c, idx) =>
            idx == 0
                ? new ScheduleChoice
                {
                    Step = c.Step,
                    Kind = c.Kind,
                    ChosenIndex = c.ChosenIndex,
                    CandidateCount = c.CandidateCount,
                    TransitionKey = "bogus_transition_key_that_does_not_exist",
                    CandidateKeys = c.CandidateKeys
                }
                : c).ToArray();

        var corruptedTrace = new ScheduleTrace
        {
            Seed = normalResult.Trace.Seed,
            Workload = normalResult.Trace.Workload,
            Choices = corruptedChoices,
            StepsExecuted = normalResult.Trace.StepsExecuted,
            Options = normalResult.Trace.Options
        };

        // Replay must throw ReplayDivergenceException and not silently clamp
        Assert.Throws<ReplayDivergenceException>(() =>
        {
            new ReplayExplorer().Replay(
                corruptedTrace,
                builder,
                corruptedTrace.Options ?? new WorkloadOptions { NodeCount = 3 },
                entry.Workload.GlobalInvariants);
        });
    }

    [Fact]
    public void Counterexample_minimizer_preserves_valid_trace()
    {
        var entry = ScenarioHost.RegistryView.Get("or-set");
        ScenarioBuilder builder = (rt, wo) => ScenarioHost.ConfigureWorkload(rt, entry, wo);
        var trace = new ScheduleTrace
        {
            Seed = 42,
            Workload = "or-set",
            Choices = Enumerable.Range(0, 10).Select(i => new ScheduleChoice
            {
                Step = i,
                ChosenIndex = 0,
                CandidateCount = 1
            }).ToArray(),
            StepsExecuted = 10,
            Options = new WorkloadOptions { NodeCount = 3 }
        };

        var minimized = CounterexampleMinimizer.Minimize(trace, builder, entry.Workload.GlobalInvariants);
        Assert.NotNull(minimized);
        Assert.True(minimized.Choices.Count <= trace.Choices.Count);
    }
}
