namespace Crucible.Concurrency;

using System.Text.Json;
using Crucible.Abstractions;
using Crucible.Runtime;

/// <summary>Result of a single exploration attempt.</summary>
public sealed class ExplorationResult
{
    public required int Seed { get; init; }
    public required string Workload { get; init; }
    public required long Steps { get; init; }
    public required bool Success { get; init; }
    public IReadOnlyList<InvariantViolation> Violations { get; init; } = Array.Empty<InvariantViolation>();
    public ScheduleTrace? Trace { get; init; }
    public string? Summary { get; init; }

    public override string ToString() =>
        Success
            ? $"OK seed={Seed} steps={Steps}"
            : $"FAIL seed={Seed} steps={Steps}: {Summary}";
}

/// <summary>Configuration for explorers.</summary>
public sealed class ExplorerOptions
{
    public int MaxSteps { get; init; } = 5_000;
    public int MaxSchedules { get; init; } = 100;
    public int DepthBound { get; init; } = 200;
    public int PctDepth { get; init; } = 10;
    public bool StopOnFirstFailure { get; init; } = true;
    public string? ScheduleOutputDirectory { get; init; }
}

/// <summary>Factory callback that builds a fresh runtime + workload for one schedule.</summary>
public delegate void ScenarioBuilder(DeterministicRuntime runtime, WorkloadOptions workloadOptions);

/// <summary>Serializes and deserializes schedule traces for replay.</summary>
public static class ScheduleSerializer
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static string ToJson(ScheduleTrace trace) =>
        JsonSerializer.Serialize(ToDto(trace), JsonOptions);

    public static void WriteToFile(ScheduleTrace trace, string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(path))!);
        File.WriteAllText(path, ToJson(trace));
    }

    public static ScheduleTrace FromJson(string json)
    {
        var dto = JsonSerializer.Deserialize<ScheduleTraceDto>(json, JsonOptions)
                  ?? throw new InvalidOperationException("Invalid schedule JSON.");
        return FromDto(dto);
    }

    public static ScheduleTrace FromFile(string path) => FromJson(File.ReadAllText(path));

    private static ScheduleTraceDto ToDto(ScheduleTrace t) => new()
    {
        Seed = t.Seed,
        Workload = t.Workload,
        StepsExecuted = t.StepsExecuted,
        FailureSummary = t.FailureSummary,
        Choices = t.Choices.Select(c => new ScheduleChoiceDto
        {
            Step = c.Step,
            Kind = c.Kind.ToString(),
            ChosenIndex = c.ChosenIndex,
            CandidateCount = c.CandidateCount,
            Actor = c.Actor?.Value,
            Label = c.Label
        }).ToList(),
        Faults = t.Faults.Select(f => new FaultEventDto
        {
            Kind = f.Kind.ToString(),
            AtStep = f.AtStep,
            AtTime = f.AtTime?.Ticks,
            Target = f.Target?.Value,
            Secondary = f.Secondary?.Value,
            PartitionA = f.PartitionA?.Select(n => n.Value).ToList(),
            PartitionB = f.PartitionB?.Select(n => n.Value).ToList(),
            DelayTicks = f.DelayTicks,
            Label = f.Label
        }).ToList()
    };

    private static ScheduleTrace FromDto(ScheduleTraceDto d) => new()
    {
        Seed = d.Seed,
        Workload = d.Workload,
        StepsExecuted = d.StepsExecuted,
        FailureSummary = d.FailureSummary,
        Choices = d.Choices.Select(c => new ScheduleChoice
        {
            Step = c.Step,
            Kind = Enum.Parse<SchedulingPointKind>(c.Kind),
            ChosenIndex = c.ChosenIndex,
            CandidateCount = c.CandidateCount,
            Actor = c.Actor is int a ? new NodeId(a) : null,
            Label = c.Label
        }).ToArray(),
        Faults = d.Faults.Select(f => new FaultEvent
        {
            Kind = Enum.Parse<FaultKind>(f.Kind),
            AtStep = f.AtStep,
            AtTime = f.AtTime is long t ? new SimTime(t) : null,
            Target = f.Target is int x ? new NodeId(x) : null,
            Secondary = f.Secondary is int y ? new NodeId(y) : null,
            PartitionA = f.PartitionA?.Select(n => new NodeId(n)).ToArray(),
            PartitionB = f.PartitionB?.Select(n => new NodeId(n)).ToArray(),
            DelayTicks = f.DelayTicks,
            Label = f.Label
        }).ToArray()
    };

    private sealed class ScheduleTraceDto
    {
        public int Seed { get; set; }
        public string Workload { get; set; } = "";
        public long StepsExecuted { get; set; }
        public string? FailureSummary { get; set; }
        public List<ScheduleChoiceDto> Choices { get; set; } = new();
        public List<FaultEventDto> Faults { get; set; } = new();
    }

    private sealed class ScheduleChoiceDto
    {
        public int Step { get; set; }
        public string Kind { get; set; } = "";
        public int ChosenIndex { get; set; }
        public int CandidateCount { get; set; }
        public int? Actor { get; set; }
        public string? Label { get; set; }
    }

    private sealed class FaultEventDto
    {
        public string Kind { get; set; } = "";
        public int? AtStep { get; set; }
        public long? AtTime { get; set; }
        public int? Target { get; set; }
        public int? Secondary { get; set; }
        public List<int>? PartitionA { get; set; }
        public List<int>? PartitionB { get; set; }
        public long? DelayTicks { get; set; }
        public string? Label { get; set; }
    }
}
