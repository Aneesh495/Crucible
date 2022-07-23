namespace Crucible.Structures;

using Crucible.Abstractions;
using Crucible.Runtime.Shared;

/// <summary>Faithful Michael–Scott lock-free queue over simulated shared memory.</summary>
public sealed class MichaelScottQueue
{
    private readonly SimSharedMemory _memory;
    private readonly SimCell _head;
    private readonly SimCell _tail;

    public MichaelScottQueue(SimSharedMemory memory)
    {
        _memory = memory;
        // Allocate sentinel node (value cell at S, next pointer cell at S+1)
        var sentinelVal = memory.Allocate(0);
        var sentinelNext = memory.Allocate(0);
        _head = memory.Allocate(sentinelVal.Id);
        _tail = memory.Allocate(sentinelVal.Id);
    }

    public SimSharedMemory Memory => _memory;

    public void Enqueue(int processId, long value)
    {
        var callId = _memory.History.Begin(processId, "enqueue", value, SimTime.Zero);
        var nodeVal = _memory.Allocate(value);
        var nodeNext = _memory.Allocate(0);
        var nodePtr = nodeVal.Id;

        while (true)
        {
            var curTail = _tail.Load(processId);
            var tailNextCell = _memory.GetCell(curTail + 1);
            var next = tailNextCell.Load(processId);

            if (curTail == _tail.Load(processId))
            {
                if (next == 0)
                {
                    if (tailNextCell.CompareExchange(processId, 0, nodePtr))
                    {
                        _tail.CompareExchange(processId, curTail, nodePtr);
                        _memory.History.Complete(callId, true, SimTime.Zero);
                        return;
                    }
                }
                else
                {
                    _tail.CompareExchange(processId, curTail, next);
                }
            }
        }
    }

    public bool TryDequeue(int processId, out long value)
    {
        var callId = _memory.History.Begin(processId, "dequeue", null, SimTime.Zero);
        while (true)
        {
            var curHead = _head.Load(processId);
            var curTail = _tail.Load(processId);
            var headNextCell = _memory.GetCell(curHead + 1);
            var next = headNextCell.Load(processId);

            if (curHead == _head.Load(processId))
            {
                if (curHead == curTail)
                {
                    if (next == 0)
                    {
                        value = 0;
                        _memory.History.Complete(callId, null, SimTime.Zero);
                        return false;
                    }
                    _tail.CompareExchange(processId, curTail, next);
                }
                else
                {
                    if (next != 0)
                    {
                        var valCell = _memory.GetCell(next);
                        var v = valCell.Load(processId);
                        if (_head.CompareExchange(processId, curHead, next))
                        {
                            value = v;
                            _memory.History.Complete(callId, value, SimTime.Zero);
                            return true;
                        }
                    }
                }
            }
        }
    }
}

/// <summary>Deliberately buggy concurrent queue for linearizability checker testing.</summary>
public sealed class BuggyMichaelScottQueue
{
    private readonly SimSharedMemory _memory;
    private readonly SimCell _head;
    private readonly SimCell _tail;

    public BuggyMichaelScottQueue(SimSharedMemory memory)
    {
        _memory = memory;
        var sentinelVal = memory.Allocate(0);
        memory.Allocate(0);
        _head = memory.Allocate(sentinelVal.Id);
        _tail = memory.Allocate(sentinelVal.Id);
    }

    public void Enqueue(int processId, long value)
    {
        var callId = _memory.History.Begin(processId, "enqueue", value, SimTime.Zero);
        var nodeVal = _memory.Allocate(value);
        _memory.Allocate(0);
        var nodePtr = nodeVal.Id;

        // BUG: Stores tail directly without linking tail->next, causing concurrent enqueues to lose nodes
        var curTail = _tail.Load(processId);
        var tailNextCell = _memory.GetCell(curTail + 1);
        tailNextCell.Store(processId, nodePtr);
        _tail.Store(processId, nodePtr);
        _memory.History.Complete(callId, true, SimTime.Zero);
    }

    public bool TryDequeue(int processId, out long value)
    {
        var callId = _memory.History.Begin(processId, "dequeue", null, SimTime.Zero);
        var curHead = _head.Load(processId);
        var curTail = _tail.Load(processId);
        var headNextCell = _memory.GetCell(curHead + 1);
        var next = headNextCell.Load(processId);

        if (curHead == curTail || next == 0)
        {
            value = 0;
            _memory.History.Complete(callId, null, SimTime.Zero);
            return false;
        }

        var valCell = _memory.GetCell(next);
        value = valCell.Load(processId);
        _head.Store(processId, next);
        _memory.History.Complete(callId, value, SimTime.Zero);
        return true;
    }
}
