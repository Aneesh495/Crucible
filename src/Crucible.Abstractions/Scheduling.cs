namespace Crucible.Abstractions;

/// <summary>Deterministic pseudo-random source owned by the runtime.</summary>
public interface ISimRandom
{
    int Seed { get; }
    int Next();
    int Next(int maxExclusive);
    int Next(int minInclusive, int maxExclusive);
    double NextDouble();
    bool NextBool(double probability = 0.5);
    void Shuffle<T>(IList<T> list);
    T Choose<T>(IReadOnlyList<T> items);
    long NextLong(long minInclusive, long maxExclusive);
}

/// <summary>Virtual clock. Advances only when the scheduler decides.</summary>
public interface ISimClock
{
    SimTime Now { get; }
    void AdvanceTo(SimTime time);
    void AdvanceBy(long ticks);
}

/// <summary>Points where the explorer may insert a scheduling decision.</summary>
public enum SchedulingPointKind
{
    TaskSpawn,
    TaskResume,
    MessageDeliver,
    TimerFire,
    LockAcquire,
    Yield,
    NondeterministicChoice,
    Crash,
    Restart
}

/// <summary>A recorded choice the explorer made (or will replay).</summary>
public readonly struct ScheduleChoice
{
    public int Step { get; init; }
    public SchedulingPointKind Kind { get; init; }
    public int ChosenIndex { get; init; }
    public int CandidateCount { get; init; }
    public NodeId? Actor { get; init; }
    public string? Label { get; init; }

    public override string ToString() =>
        $"#{Step} {Kind} pick={ChosenIndex}/{CandidateCount} {Actor} {Label}";
}

/// <summary>Callback the runtime uses to ask the explorer which enabled transition to take.</summary>
public interface IScheduleOracle
{
    int Choose(SchedulingPointKind kind, int candidateCount, NodeId? actor, string? label);
    void Record(ScheduleChoice choice);
}
