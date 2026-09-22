namespace Crucible.Checking;

using Crucible.Abstractions;

/// <summary>Records concurrent invocations for linearizability and audit.</summary>
public sealed class ExecutionHistory
{
    private long _nextId = 1;
    private readonly List<HistoryInvocation> _invocations = new();
    private readonly object _gate = new();

    public IReadOnlyList<HistoryInvocation> Invocations
    {
        get { lock (_gate) return _invocations.ToArray(); }
    }

    public long Begin(int processId, string method, object? argument, SimTime callTime)
    {
        lock (_gate)
        {
            var id = _nextId++;
            _invocations.Add(new HistoryInvocation
            {
                Id = id,
                ProcessId = processId,
                Method = method,
                Argument = argument,
                CallTime = callTime,
                HasResult = false
            });
            return id;
        }
    }

    public void Complete(long id, object? result, SimTime returnTime)
    {
        lock (_gate)
        {
            var inv = _invocations.FirstOrDefault(i => i.Id == id)
                      ?? throw new InvalidOperationException($"Unknown invocation {id}.");
            inv.Result = result;
            inv.ReturnTime = returnTime;
            inv.HasResult = true;
        }
    }

    public void Reset()
    {
        lock (_gate)
        {
            _invocations.Clear();
            _nextId = 1;
        }
    }

    public HistorySnapshot Snapshot()
    {
        lock (_gate)
        {
            return new HistorySnapshot(_invocations.Select(Clone).ToArray());
        }
    }

    private static HistoryInvocation Clone(HistoryInvocation i) => new()
    {
        Id = i.Id,
        ProcessId = i.ProcessId,
        Method = i.Method,
        Argument = i.Argument,
        Result = i.Result,
        HasResult = i.HasResult,
        CallTime = i.CallTime,
        ReturnTime = i.ReturnTime
    };
}

public sealed class HistorySnapshot
{
    public HistorySnapshot(IReadOnlyList<HistoryInvocation> invocations) =>
        Invocations = invocations;

    public IReadOnlyList<HistoryInvocation> Invocations { get; }

    public IEnumerable<HistoryInvocation> Pending =>
        Invocations.Where(i => !i.HasResult);

    public IEnumerable<HistoryInvocation> Completed =>
        Invocations.Where(i => i.HasResult);
}
