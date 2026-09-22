
using Crucible.Abstractions;
using Crucible.Concurrency;
using Crucible.Protocols;
using FluentAssertions;

namespace Crucible.Tests;

public class ExplorerTests
{
    [Fact]
    public void ScheduleSerializer_roundtrip()
    {
        var trace = new ScheduleTrace
        {
            Seed = 1,
            Workload = "or-set",
            Choices = new[] { new ScheduleChoice { Step = 0, Kind = SchedulingPointKind.TaskResume, ChosenIndex = 0, CandidateCount = 2 } },
            StepsExecuted = 10
        };
        var json = ScheduleSerializer.ToJson(trace);
        var back = ScheduleSerializer.FromJson(json);
        back.Seed.Should().Be(1);
        back.Choices.Should().HaveCount(1);
    }

    [Fact]
    public void Pct_explorer_runs_or_set()
    {
        var entry = ScenarioHost.RegistryView.Get("or-set");
        ScenarioBuilder builder = (rt, wo) => ScenarioHost.ConfigureWorkload(rt, entry, wo);
        var result = new PctExplorer(new ExplorerOptions { MaxSteps = 500 }).Run(
            "or-set", 42, builder, new WorkloadOptions { NodeCount = 3 }, entry.Workload.GlobalInvariants);
        result.Steps.Should().BeGreaterThan(0);
    }
}
