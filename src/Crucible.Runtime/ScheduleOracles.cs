namespace Crucible.Runtime;

using Crucible.Abstractions;

/// <summary>Exception thrown when replay diverges from the recorded schedule trace.</summary>
public sealed class ReplayDivergenceException : InvalidOperationException
{
    public int Step { get; }
    public ScheduleChoice Expected { get; }
    public SchedulingPointKind ActualKind { get; }
    public int ActualCandidateCount { get; }
    public NodeId? ActualActor { get; }
    public string? ActualTransitionKey { get; }
    public IReadOnlyList<string>? ActualCandidateKeys { get; }

    public ReplayDivergenceException(
        int step,
        ScheduleChoice expected,
        SchedulingPointKind actualKind,
        int actualCandidateCount,
        NodeId? actualActor,
        string? actualTransitionKey,
        IReadOnlyList<string>? actualCandidateKeys)
        : base($"Replay divergence at step #{step}: expected {expected.Kind} on actor {expected.Actor} pick={expected.ChosenIndex}/{expected.CandidateCount} key={expected.TransitionKey}, but got {actualKind} on actor {actualActor} (candidates={actualCandidateCount}) key={actualTransitionKey}. Actual candidates: [{string.Join(", ", actualCandidateKeys ?? Array.Empty<string>())}]")
    {
        Step = step;
        Expected = expected;
        ActualKind = actualKind;
        ActualCandidateCount = actualCandidateCount;
        ActualActor = actualActor;
        ActualTransitionKey = actualTransitionKey;
        ActualCandidateKeys = actualCandidateKeys;
    }
}

/// <summary>Default schedule oracle: RNG picks uniformly among candidates and records choices.</summary>
public sealed class RecordingOracle : IScheduleOracle
{
    private readonly ISimRandom _random;
    private readonly List<ScheduleChoice> _choices = new();
    private int _step;

    public RecordingOracle(ISimRandom random) => _random = random;

    public IReadOnlyList<ScheduleChoice> Choices => _choices;

    public int Choose(
        SchedulingPointKind kind,
        int candidateCount,
        NodeId? actor,
        string? label,
        string? transitionKey = null,
        IReadOnlyList<string>? candidateKeys = null)
    {
        if (candidateCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(candidateCount));
        var pick = candidateCount == 1 ? 0 : _random.Next(candidateCount);
        var chosenKey = candidateKeys is not null && pick < candidateKeys.Count ? candidateKeys[pick] : transitionKey;
        var choice = new ScheduleChoice
        {
            Step = _step++,
            Kind = kind,
            ChosenIndex = pick,
            CandidateCount = candidateCount,
            Actor = actor,
            Label = label,
            TransitionKey = chosenKey,
            CandidateKeys = candidateKeys
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

/// <summary>Replay oracle: consumes a fixed list of choices; fails strictly if the trace diverges.</summary>
public sealed class ReplayOracle : IScheduleOracle
{
    private readonly IReadOnlyList<ScheduleChoice> _trace;
    private int _index;
    private readonly List<ScheduleChoice> _consumed = new();

    public ReplayOracle(IReadOnlyList<ScheduleChoice> trace) => _trace = trace;

    public IReadOnlyList<ScheduleChoice> Consumed => _consumed;
    public bool Exhausted => _index >= _trace.Count;

    public int Choose(
        SchedulingPointKind kind,
        int candidateCount,
        NodeId? actor,
        string? label,
        string? transitionKey = null,
        IReadOnlyList<string>? candidateKeys = null)
    {
        if (_index >= _trace.Count)
        {
            throw new InvalidOperationException(
                $"Replay exhausted at step {_index} requesting {kind} ({candidateCount} candidates).");
        }

        var expected = _trace[_index++];

        bool divergence = expected.Kind != kind || expected.CandidateCount != candidateCount;
        if (expected.Actor is not null && actor is not null && expected.Actor != actor)
            divergence = true;

        var actualChosenKey = candidateKeys is not null && expected.ChosenIndex >= 0 && expected.ChosenIndex < candidateKeys.Count
            ? candidateKeys[expected.ChosenIndex]
            : transitionKey;

        if (!string.IsNullOrEmpty(expected.TransitionKey) && !string.IsNullOrEmpty(actualChosenKey) &&
            !string.Equals(expected.TransitionKey, actualChosenKey, StringComparison.Ordinal))
        {
            divergence = true;
        }

        if (expected.ChosenIndex < 0 || expected.ChosenIndex >= candidateCount)
            divergence = true;

        if (divergence)
        {
            throw new ReplayDivergenceException(
                expected.Step,
                expected,
                kind,
                candidateCount,
                actor,
                actualChosenKey,
                candidateKeys);
        }

        _consumed.Add(expected);
        return expected.ChosenIndex;
    }

    public void Record(ScheduleChoice choice) { /* replay is authoritative */ }
}

/// <summary>
/// Scripted oracle used by DFS explorers: at the frontier step, force a specific index;
/// before that, strictly replay a prefix; after that, choose canonically.
/// </summary>
public sealed class FrontierOracle : IScheduleOracle
{
    private readonly IReadOnlyList<ScheduleChoice> _prefix;
    private readonly int _frontierStep;
    private readonly int _forcedIndex;
    private readonly ISimRandom? _fallback;
    private readonly List<ScheduleChoice> _choices = new();
    private int _step;

    public FrontierOracle(
        IReadOnlyList<ScheduleChoice> prefix,
        int frontierStep,
        int forcedIndex,
        ISimRandom? fallback = null)
    {
        _prefix = prefix;
        _frontierStep = frontierStep;
        _forcedIndex = forcedIndex;
        _fallback = fallback;
    }

    public IReadOnlyList<ScheduleChoice> Choices => _choices;

    public int Choose(
        SchedulingPointKind kind,
        int candidateCount,
        NodeId? actor,
        string? label,
        string? transitionKey = null,
        IReadOnlyList<string>? candidateKeys = null)
    {
        if (candidateCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(candidateCount));

        int pick;
        if (_step < _prefix.Count)
        {
            var expected = _prefix[_step];
            bool divergence = expected.Kind != kind || expected.CandidateCount != candidateCount;
            if (expected.Actor is not null && actor is not null && expected.Actor != actor)
                divergence = true;
            if (expected.ChosenIndex < 0 || expected.ChosenIndex >= candidateCount)
                divergence = true;

            if (divergence)
            {
                throw new ReplayDivergenceException(
                    expected.Step,
                    expected,
                    kind,
                    candidateCount,
                    actor,
                    transitionKey,
                    candidateKeys);
            }

            pick = expected.ChosenIndex;
        }
        else if (_step == _frontierStep)
        {
            if (_forcedIndex < 0 || _forcedIndex >= candidateCount)
            {
                throw new InvalidOperationException(
                    $"Frontier forced choice {_forcedIndex} out of range for {candidateCount} candidates at step {_step}.");
            }
            pick = _forcedIndex;
        }
        else
        {
            // Deterministic canonical choice beyond frontier: always pick 0
            pick = 0;
        }

        var chosenKey = candidateKeys is not null && pick < candidateKeys.Count ? candidateKeys[pick] : transitionKey;
        var choice = new ScheduleChoice
        {
            Step = _step++,
            Kind = kind,
            ChosenIndex = pick,
            CandidateCount = candidateCount,
            Actor = actor,
            Label = label,
            TransitionKey = chosenKey,
            CandidateKeys = candidateKeys
        };
        _choices.Add(choice);
        return pick;
    }

    public void Record(ScheduleChoice choice) => _choices.Add(choice);
}
