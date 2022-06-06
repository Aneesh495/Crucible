namespace Crucible.Abstractions;

/// <summary>Severity of a property failure.</summary>
public enum ViolationSeverity
{
    Warning,
    Error,
    Fatal
}

/// <summary>A single invariant or linearizability failure.</summary>
public sealed class InvariantViolation
{
    public string Name { get; init; } = "";
    public string Detail { get; init; } = "";
    public ViolationSeverity Severity { get; init; } = ViolationSeverity.Error;
    public SimTime? At { get; init; }
    public NodeId? Node { get; init; }

    public override string ToString() =>
        $"[{Severity}] {Name}: {Detail}" +
        (Node is { } n ? $" @ {n}" : "") +
        (At is { } t ? $" t={t}" : "");
}

/// <summary>Cluster-wide safety property checked after steps or at the end.</summary>
public interface IInvariant
{
    string Name { get; }
    IEnumerable<InvariantViolation> Check(IClusterView cluster);
}

/// <summary>Read-only snapshot of cluster state for checkers.</summary>
public interface IClusterView
{
    SimTime Now { get; }
    IReadOnlyList<NodeId> Nodes { get; }
    ProcessState GetState(NodeId id);
    T? GetProcess<T>(NodeId id) where T : class, ISimProcess;
    IReadOnlyList<ISimProcess> AllProcesses { get; }
    IReadOnlyList<string> EventLog { get; }
}

/// <summary>Sequential specification used by the linearizability checker.</summary>
public interface ISequentialSpec<TState>
{
    string Name { get; }
    TState InitialState { get; }
    bool TryApply(TState state, HistoryInvocation invocation, out TState next, out object? result);
}

/// <summary>One call in a concurrent history.</summary>
public sealed class HistoryInvocation
{
    public long Id { get; init; }
    public string Method { get; init; } = "";
    public object? Argument { get; init; }
    public object? Result { get; set; }
    public bool HasResult { get; set; }
    public SimTime CallTime { get; init; }
    public SimTime? ReturnTime { get; set; }
    public int ProcessId { get; init; }
    public bool IsPending => !HasResult;

    public override string ToString() =>
        $"#{Id} p{ProcessId} {Method}({Argument}) -> {(HasResult ? Result : "pending")}";
}
