namespace Crucible.Structures;

using Crucible.Runtime.Shared;

/// <summary>Michael–Scott lock-free queue (simplified pointer model).</summary>
public sealed class MichaelScottQueue
{
    private readonly SimCell _head;
    private readonly SimCell _tail;

    public MichaelScottQueue(SimSharedMemory memory)
    {
        var sentinel = memory.Allocate(1);
        _head = memory.Allocate(sentinel.Load(0));
        _tail = memory.Allocate(sentinel.Load(0));
    }

    public void Enqueue(int processId, long valueNode)
    {
        while (true)
        {
            var tail = _tail.Load(processId);
            var next = tail; // simplified: tail cell stores last node id
            if (_tail.CompareExchange(processId, tail, valueNode))
                return;
        }
    }

    public bool TryDequeue(int processId, out long valueNode)
    {
        while (true)
        {
            var head = _head.Load(processId);
            var tail = _tail.Load(processId);
            if (head == tail)
            {
                valueNode = 0;
                return false;
            }
            var next = head + 1; // toy linkage
            if (_head.CompareExchange(processId, head, next))
            {
                valueNode = head;
                return true;
            }
        }
    }
}
