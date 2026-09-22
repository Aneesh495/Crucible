namespace Crucible.Abstractions;

/// <summary>Lifecycle state of a simulated process.</summary>
public enum ProcessState
{
    Running,
    Crashed,
    Stopped
}

/// <summary>A process that participates in the simulation.</summary>
public interface ISimProcess
{
    NodeId Id { get; }
    ProcessState State { get; }
    void Start(ISimContext context);
    void OnMessage(MessageEnvelope envelope);
    void OnTimer(string name, long generation);
    void OnCrash();
    void OnRestart(ISimContext context);
    IEnumerable<InvariantViolation> CheckLocalInvariants();
}

/// <summary>Services available to a process while it is running.</summary>
public interface ISimContext
{
    NodeId Self { get; }
    ISimClock Clock { get; }
    ISimRandom Random { get; }
    ISimNetwork Network { get; }
    ISimStorage Storage { get; }
    void SetTimer(string name, long delayTicks);
    void CancelTimer(string name);
    void Yield(string? label = null);
    int Choose(int candidateCount, string? label = null);
    void Log(string message);
    void RecordEvent(string category, string detail);
}

/// <summary>Factory for spawning a cluster of processes.</summary>
public interface IWorkload
{
    string Name { get; }
    IReadOnlyList<ISimProcess> CreateProcesses(int nodeCount, WorkloadOptions options);
    IReadOnlyList<IInvariant> GlobalInvariants { get; }
    void DriveClient(ISimContext clientContext, IReadOnlyList<NodeId> nodes, int step);
}

public sealed class WorkloadOptions
{
    public int NodeCount { get; init; } = 3;
    public long ElectionTimeoutMin { get; init; } = 50;
    public long ElectionTimeoutMax { get; init; } = 100;
    public long HeartbeatInterval { get; init; } = 20;
    public bool EnableSnapshots { get; init; } = true;
    public IReadOnlyDictionary<string, string> Extra { get; init; } =
        new Dictionary<string, string>();
}
