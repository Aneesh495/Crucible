namespace Crucible.Runtime;

using Crucible.Abstractions;

/// <summary>Monotonic virtual clock. Never jumps backward.</summary>
public sealed class VirtualClock : ISimClock
{
    public SimTime Now { get; private set; } = SimTime.Zero;

    public void AdvanceTo(SimTime time)
    {
        if (time < Now)
            throw new InvalidOperationException($"Cannot move clock backward from {Now} to {time}.");
        Now = time;
    }

    public void AdvanceBy(long ticks)
    {
        if (ticks < 0)
            throw new ArgumentOutOfRangeException(nameof(ticks));
        Now = Now.Add(ticks);
    }

    public void Reset() => Now = SimTime.Zero;
}
