namespace Crucible.Faults;

using Crucible.Abstractions;
using Crucible.Runtime;

/// <summary>Fluent builder for fault schedules injected into the runtime.</summary>
public sealed class FaultScheduleBuilder
{
    private readonly List<FaultEvent> _events = new();

    public FaultScheduleBuilder Crash(NodeId target, int atStep) =>
        Add(new FaultEvent { Kind = FaultKind.Crash, Target = target, AtStep = atStep });

    public FaultScheduleBuilder Restart(NodeId target, int atStep) =>
        Add(new FaultEvent { Kind = FaultKind.Restart, Target = target, AtStep = atStep });

    public FaultScheduleBuilder Partition(
        IReadOnlyList<NodeId> groupA,
        IReadOnlyList<NodeId> groupB,
        int atStep) =>
        Add(new FaultEvent
        {
            Kind = FaultKind.Partition,
            PartitionA = groupA,
            PartitionB = groupB,
            AtStep = atStep
        });

    public FaultScheduleBuilder Heal(int atStep) =>
        Add(new FaultEvent { Kind = FaultKind.Heal, AtStep = atStep });

    public FaultScheduleBuilder DropInFlight(int atStep) =>
        Add(new FaultEvent { Kind = FaultKind.DropMessage, AtStep = atStep });

    public FaultScheduleBuilder ClockSkew(NodeId target, long ticks, int atStep) =>
        Add(new FaultEvent
        {
            Kind = FaultKind.ClockSkew,
            Target = target,
            DelayTicks = ticks,
            AtStep = atStep
        });

    public FaultScheduleBuilder AtTime(SimTime time, FaultEvent fault) =>
        Add(new FaultEvent
        {
            Kind = fault.Kind,
            AtTime = time,
            AtStep = fault.AtStep,
            Target = fault.Target,
            Secondary = fault.Secondary,
            PartitionA = fault.PartitionA,
            PartitionB = fault.PartitionB,
            DelayTicks = fault.DelayTicks,
            Label = fault.Label
        });

    private FaultScheduleBuilder Add(FaultEvent fault)
    {
        _events.Add(fault);
        return this;
    }

    public IReadOnlyList<FaultEvent> Build() => _events.ToArray();

    public void ApplyTo(DeterministicRuntime runtime)
    {
        foreach (var e in _events)
            runtime.RegisterFault(e);
    }
}
