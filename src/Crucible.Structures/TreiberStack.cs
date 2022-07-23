namespace Crucible.Structures;

using Crucible.Abstractions;
using Crucible.Runtime.Shared;

/// <summary>Faithful lock-free Treiber stack over simulated shared memory.</summary>
public sealed class TreiberStack
{
    private readonly SimSharedMemory _memory;
    private readonly SimCell _top;

    public TreiberStack(SimSharedMemory memory)
    {
        _memory = memory;
        _top = memory.Allocate(0); // 0 = null pointer
    }

    public SimSharedMemory Memory => _memory;
    public SimCell TopCell => _top;

    public void Push(int processId, long value)
    {
        var callId = _memory.History.Begin(processId, "push", value, SimTime.Zero);
        var valCell = _memory.Allocate(value);
        var nextCell = _memory.Allocate(0);
        var nodePtr = valCell.Id;

        while (true)
        {
            var oldTop = _top.Load(processId);
            nextCell.Store(processId, oldTop);
            if (_top.CompareExchange(processId, oldTop, nodePtr))
            {
                _memory.History.Complete(callId, true, SimTime.Zero);
                return;
            }
        }
    }

    public bool TryPop(int processId, out long value)
    {
        var callId = _memory.History.Begin(processId, "pop", null, SimTime.Zero);
        while (true)
        {
            var oldTop = _top.Load(processId);
            if (oldTop == 0)
            {
                value = 0;
                _memory.History.Complete(callId, null, SimTime.Zero);
                return false;
            }
            var nextCell = _memory.GetCell(oldTop + 1);
            var newTop = nextCell.Load(processId);
            if (_top.CompareExchange(processId, oldTop, newTop))
            {
                var valCell = _memory.GetCell(oldTop);
                value = valCell.Load(processId);
                _memory.History.Complete(callId, value, SimTime.Zero);
                return true;
            }
        }
    }
}

/// <summary>Deliberately buggy concurrent stack for linearizability checker testing.</summary>
public sealed class BuggyTreiberStack
{
    private readonly SimSharedMemory _memory;
    private readonly SimCell _top;

    public BuggyTreiberStack(SimSharedMemory memory)
    {
        _memory = memory;
        _top = memory.Allocate(0);
    }

    public SimSharedMemory Memory => _memory;

    public void Push(int processId, long value)
    {
        var callId = _memory.History.Begin(processId, "push", value, SimTime.Zero);
        var valCell = _memory.Allocate(value);
        var nextCell = _memory.Allocate(0);
        var nodePtr = valCell.Id;

        // BUG: Unsynchronized store instead of CAS loop creates lost update race condition
        var oldTop = _top.Load(processId);
        nextCell.Store(processId, oldTop);
        _top.Store(processId, nodePtr);
        _memory.History.Complete(callId, true, SimTime.Zero);
    }

    public bool TryPop(int processId, out long value)
    {
        var callId = _memory.History.Begin(processId, "pop", null, SimTime.Zero);
        var oldTop = _top.Load(processId);
        if (oldTop == 0)
        {
            value = 0;
            _memory.History.Complete(callId, null, SimTime.Zero);
            return false;
        }
        var nextCell = _memory.GetCell(oldTop + 1);
        var newTop = nextCell.Load(processId);
        _top.Store(processId, newTop);
        var valCell = _memory.GetCell(oldTop);
        value = valCell.Load(processId);
        _memory.History.Complete(callId, value, SimTime.Zero);
        return true;
    }
}
