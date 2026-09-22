namespace Crucible.Abstractions;

/// <summary>Fault kinds the injector can schedule.</summary>
public enum FaultKind
{
    None,
    Crash,
    Restart,
    DropMessage,
    DuplicateMessage,
    DelayMessage,
    Reorder,
    Partition,
    Heal,
    ClockSkew
}

/// <summary>A planned fault relative to simulation time or step count.</summary>
public sealed class FaultEvent
{
    public FaultKind Kind { get; init; }
    public SimTime? AtTime { get; init; }
    public int? AtStep { get; init; }
    public NodeId? Target { get; init; }
    public NodeId? Secondary { get; init; }
    public IReadOnlyList<NodeId>? PartitionA { get; init; }
    public IReadOnlyList<NodeId>? PartitionB { get; init; }
    public long? DelayTicks { get; init; }
    public string? Label { get; init; }

    public override string ToString() =>
        $"{Kind} step={AtStep} time={AtTime} target={Target} {Label}";
}

/// <summary>Opaque handle for a recorded schedule ready for replay.</summary>
public sealed class ScheduleTrace
{
    public int Seed { get; init; }
    public string Workload { get; init; } = "";
    public IReadOnlyList<ScheduleChoice> Choices { get; init; } = Array.Empty<ScheduleChoice>();
    public IReadOnlyList<FaultEvent> Faults { get; init; } = Array.Empty<FaultEvent>();
    public string? FailureSummary { get; init; }
    public long StepsExecuted { get; init; }
}
