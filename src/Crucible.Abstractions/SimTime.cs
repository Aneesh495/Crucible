namespace Crucible.Abstractions;

/// <summary>Logical time in the simulation. Never wall-clock.</summary>
public readonly struct SimTime : IEquatable<SimTime>, IComparable<SimTime>
{
    public long Ticks { get; }

    public SimTime(long ticks)
    {
        if (ticks < 0)
            throw new ArgumentOutOfRangeException(nameof(ticks));
        Ticks = ticks;
    }

    public static SimTime Zero => new(0);
    public static SimTime FromMilliseconds(long ms) => new(ms);
    public TimeSpan AsTimeSpan() => TimeSpan.FromMilliseconds(Ticks);

    public SimTime Add(long deltaTicks)
    {
        if (deltaTicks < 0)
            throw new ArgumentOutOfRangeException(nameof(deltaTicks));
        return new SimTime(checked(Ticks + deltaTicks));
    }

    public SimTime Add(SimTime other) => Add(other.Ticks);

    public long DistanceTo(SimTime other) => Math.Abs(other.Ticks - Ticks);

    public bool Equals(SimTime other) => Ticks == other.Ticks;
    public override bool Equals(object? obj) => obj is SimTime other && Equals(other);
    public override int GetHashCode() => Ticks.GetHashCode();
    public int CompareTo(SimTime other) => Ticks.CompareTo(other.Ticks);
    public override string ToString() => $"{Ticks}t";

    public static bool operator ==(SimTime a, SimTime b) => a.Equals(b);
    public static bool operator !=(SimTime a, SimTime b) => !a.Equals(b);
    public static bool operator <(SimTime a, SimTime b) => a.Ticks < b.Ticks;
    public static bool operator >(SimTime a, SimTime b) => a.Ticks > b.Ticks;
    public static bool operator <=(SimTime a, SimTime b) => a.Ticks <= b.Ticks;
    public static bool operator >=(SimTime a, SimTime b) => a.Ticks >= b.Ticks;
    public static SimTime operator +(SimTime a, long delta) => a.Add(delta);
}
