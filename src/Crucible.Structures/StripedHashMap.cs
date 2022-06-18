namespace Crucible.Structures;

using Crucible.Runtime.Shared;

/// <summary>Striped hash map with per-bucket CAS over simulated memory.</summary>
public sealed class StripedHashMap
{
    private readonly SimCell[] _buckets;
    private readonly int _mask;

    public StripedHashMap(SimSharedMemory memory, int stripeCount)
    {
        if (stripeCount <= 0 || (stripeCount & (stripeCount - 1)) != 0)
            throw new ArgumentException("stripeCount must be power of two");
        _mask = stripeCount - 1;
        _buckets = new SimCell[stripeCount];
        for (var i = 0; i < stripeCount; i++)
            _buckets[i] = memory.Allocate(0);
    }

    public bool TryPut(int processId, int key, long value)
    {
        var bucket = _buckets[key & _mask];
        while (true)
        {
            var current = bucket.Load(processId);
            if (current == 0 || current == value)
            {
                if (bucket.CompareExchange(processId, current, value))
                    return true;
            }
            else if (bucket.CompareExchange(processId, current, value))
                return true;
        }
    }

    public bool TryGet(int processId, int key, out long value)
    {
        value = _buckets[key & _mask].Load(processId);
        return value != 0;
    }
}
