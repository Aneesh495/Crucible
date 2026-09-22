namespace Crucible.Runtime.Shared;

using Crucible.Abstractions;
using Crucible.Checking;

/// <summary>
/// Sequentially consistent shared memory for lock-free structure tests.
/// Every load/store/CAS is a scheduling point: the runtime picks interleaving order.
/// </summary>
public sealed class SimSharedMemory
{
    private readonly DeterministicRuntime _runtime;
    private readonly ExecutionHistory _history;
    private readonly Dictionary<long, long> _cells = new();
    private long _nextCellId = 1;

    public SimSharedMemory(DeterministicRuntime runtime, ExecutionHistory history)
    {
        _runtime = runtime;
        _history = history;
    }

    public ExecutionHistory History => _history;

    public SimCell Allocate(long initial = 0)
    {
        var id = _nextCellId++;
        _cells[id] = initial;
        return new SimCell(this, id);
    }

    internal long Load(int processId, long cellId)
    {
        _runtime.Choose(new NodeId(processId), 1, "shared-load");
        var id = _history.Begin(processId, "read", cellId, _runtime.Clock.Now);
        var value = _cells.TryGetValue(cellId, out var v) ? v : 0;
        _history.Complete(id, value, _runtime.Clock.Now);
        return value;
    }

    internal void Store(int processId, long cellId, long value)
    {
        _runtime.Choose(new NodeId(processId), 1, "shared-store");
        var id = _history.Begin(processId, "write", (cellId, value), _runtime.Clock.Now);
        _cells[cellId] = value;
        _history.Complete(id, true, _runtime.Clock.Now);
    }

    internal bool CompareExchange(int processId, long cellId, long expected, long update)
    {
        _runtime.Choose(new NodeId(processId), 1, "shared-cas");
        var id = _history.Begin(processId, "compareAndSwap", (expected, update), _runtime.Clock.Now);
        var current = _cells.TryGetValue(cellId, out var v) ? v : 0;
        var success = current == expected;
        if (success)
            _cells[cellId] = update;
        _history.Complete(id, success, _runtime.Clock.Now);
        return success;
    }

    internal long FetchAdd(int processId, long cellId, long delta)
    {
        _runtime.Choose(new NodeId(processId), 1, "shared-faa");
        var id = _history.Begin(processId, "fetchAdd", delta, _runtime.Clock.Now);
        var current = _cells.TryGetValue(cellId, out var v) ? v : 0;
        var next = checked(current + delta);
        _cells[cellId] = next;
        _history.Complete(id, current, _runtime.Clock.Now);
        return current;
    }
}

public sealed class SimCell
{
    private readonly SimSharedMemory _mem;
    private readonly long _id;

    internal SimCell(SimSharedMemory mem, long id)
    {
        _mem = mem;
        _id = id;
    }

    public long Load(int processId) => _mem.Load(processId, _id);
    public void Store(int processId, long value) => _mem.Store(processId, _id, value);
    public bool CompareExchange(int processId, long expected, long update) =>
        _mem.CompareExchange(processId, _id, expected, update);
    public long FetchAdd(int processId, long delta) => _mem.FetchAdd(processId, _id, delta);
}
