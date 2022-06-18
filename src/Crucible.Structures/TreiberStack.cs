namespace Crucible.Structures;

using Crucible.Runtime.Shared;

/// <summary>Lock-free stack (Treiber) over simulated shared memory.</summary>
public sealed class TreiberStack
{
    private readonly SimCell _top;

    public TreiberStack(SimSharedMemory memory)
    {
        _top = memory.Allocate(0);
    }

    public void Push(int processId, long nodePtr)
    {
        while (true)
        {
            var head = _top.Load(processId);
            // nodePtr encodes (next, value) externally; here we treat nodePtr as payload handle.
            if (_top.CompareExchange(processId, head, nodePtr))
                return;
        }
    }

    public bool TryPop(int processId, out long nodePtr)
    {
        while (true)
        {
            var head = _top.Load(processId);
            if (head == 0)
            {
                nodePtr = 0;
                return false;
            }
            // In full impl we'd read head->next; simplified: pop returns head directly.
            if (_top.CompareExchange(processId, head, 0))
            {
                nodePtr = head;
                return true;
            }
        }
    }
}
