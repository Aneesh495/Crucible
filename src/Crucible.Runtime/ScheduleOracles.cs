namespace Crucible.Runtime;

using Crucible.Abstractions;

/// <summary>Default schedule oracle: RNG picks uniformly among candidates and records choices.</summary>
public sealed class RecordingOracle : IScheduleOracle
{
    private readonly ISimRandom _random;
    private readonly List<ScheduleChoice> _choices = new();
    private int _step;

    public RecordingOracle(ISimRandom random) => _random = random;

    public IReadOnlyList<ScheduleChoice> Choices => _choices;

    public int Choose(SchedulingPointKind kind, int candidateCount, NodeId? actor, string? label)
    {
        if (candidateCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(candidateCount));
        var pick = candidateCount == 1 ? 0 : _random.Next(candidateCount);
        var choice = new ScheduleChoice
        {
            Step = _step++,
            Kind = kind,
            ChosenIndex = pick,
            CandidateCount = candidateCount,
            Actor = actor,
            Label = label
        };
        _choices.Add(choice);
        return pick;
    }

    public void Record(ScheduleChoice choice) => _choices.Add(choice);

    public void Reset()
    {
        _choices.Clear();
        _step = 0;
    }
}

/// <summary>Replay oracle: consumes a fixed list of choices; fails if the trace diverges.</summary>
public sealed class ReplayOracle : IScheduleOracle
{
    private readonly IReadOnlyList<ScheduleChoice> _trace;
    private int _index;
    private readonly List<ScheduleChoice> _consumed = new();

    public ReplayOracle(IReadOnlyList<ScheduleChoice> trace) => _trace = trace;

    public IReadOnlyList<ScheduleChoice> Consumed => _consumed;
    public bool Exhausted => _index >= _trace.Count;

    public int Choose(SchedulingPointKind kind, int candidateCount, NodeId? actor, string? label)
    {
        if (_index >= _trace.Count)
            throw new InvalidOperationException(
                $"Replay exhausted at step {_index} requesting {kind} ({candidateCount} candidates).");

        var expected = _trace[_index++];
        if (expected.Kind != kind)
            throw new InvalidOperationException(
                $"Replay divergence at #{expected.Step}: expected {expected.Kind}, got {kind}.");
        if (expected.CandidateCount != candidateCount)
            throw new InvalidOperationException(
                $"Replay divergence at #{expected.Step}: expected {expected.CandidateCount} candidates, got {candidateCount}.");
        if (expected.ChosenIndex >= candidateCount)
            throw new InvalidOperationException(
                $"Replay choice {expected.ChosenIndex} out of range for {candidateCount} candidates.");

        _consumed.Add(expected);
        return expected.ChosenIndex;
    }

    public void Record(ScheduleChoice choice) { /* replay is authoritative */ }
}

/// <summary>
/// Scripted oracle used by DFS explorers: at the frontier step, force a specific index;
/// before that, follow a prefix; after that, fall back to RNG.
/// </summary>
public sealed class FrontierOracle : IScheduleOracle
{
    private readonly IReadOnlyList<ScheduleChoice> _prefix;
    private readonly int _frontierStep;
    private readonly int _forcedIndex;
    private readonly ISimRandom _fallback;
    private readonly List<ScheduleChoice> _choices = new();
    private int _step;

    public FrontierOracle(
        IReadOnlyList<ScheduleChoice> prefix,
        int frontierStep,
        int forcedIndex,
        ISimRandom fallback)
    {
        _prefix = prefix;
        _frontierStep = frontierStep;
        _forcedIndex = forcedIndex;
        _fallback = fallback;
    }

    public IReadOnlyList<ScheduleChoice> Choices => _choices;

    public int Choose(SchedulingPointKind kind, int candidateCount, NodeId? actor, string? label)
    {
        int pick;
        if (_step < _prefix.Count)
        {
            var expected = _prefix[_step];
            pick = expected.ChosenIndex;
            if (pick >= candidateCount)
                pick = candidateCount - 1;
        }
        else if (_step == _frontierStep)
        {
            pick = Math.Clamp(_forcedIndex, 0, candidateCount - 1);
        }
        else
        {
            pick = candidateCount == 1 ? 0 : _fallback.Next(candidateCount);
        }

        var choice = new ScheduleChoice
        {
            Step = _step++,
            Kind = kind,
            ChosenIndex = pick,
            CandidateCount = candidateCount,
            Actor = actor,
            Label = label
        };
        _choices.Add(choice);
        return pick;
    }

    public void Record(ScheduleChoice choice) => _choices.Add(choice);
}
