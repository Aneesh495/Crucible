namespace Crucible.Runtime;

using Crucible.Abstractions;

/// <summary>A pending timer registered by a process.</summary>
public sealed class SimTimer
{
    public required string Name { get; init; }
    public required NodeId Owner { get; init; }
    public required SimTime FireAt { get; set; }
    public required long Generation { get; init; }
    public bool Cancelled { get; set; }
}

/// <summary>Min-heap of timers keyed by FireAt, then generation.</summary>
public sealed class TimerWheel
{
    private readonly List<SimTimer> _heap = new();
    private readonly Dictionary<(NodeId, string), SimTimer> _byName = new();
    private long _generation;

    public int Count => _heap.Count;

    public SimTimer Set(NodeId owner, string name, SimTime fireAt)
    {
        Cancel(owner, name);
        var timer = new SimTimer
        {
            Name = name,
            Owner = owner,
            FireAt = fireAt,
            Generation = ++_generation
        };
        _byName[(owner, name)] = timer;
        _heap.Add(timer);
        SiftUp(_heap.Count - 1);
        return timer;
    }

    public bool Cancel(NodeId owner, string name)
    {
        if (!_byName.Remove((owner, name), out var existing))
            return false;
        existing.Cancelled = true;
        return true;
    }

    public SimTime? NextFireTime()
    {
        Compact();
        return _heap.Count == 0 ? null : _heap[0].FireAt;
    }

    public List<SimTimer> PopDue(SimTime now)
    {
        var due = new List<SimTimer>();
        Compact();
        while (_heap.Count > 0 && _heap[0].FireAt <= now)
        {
            var t = PopMin();
            if (t.Cancelled) continue;
            _byName.Remove((t.Owner, t.Name));
            due.Add(t);
        }
        return due;
    }

    public void ClearNode(NodeId node)
    {
        var keys = _byName.Keys.Where(k => k.Item1 == node).ToArray();
        foreach (var k in keys)
        {
            _byName[k].Cancelled = true;
            _byName.Remove(k);
        }
        Compact();
    }

    public void Reset()
    {
        _heap.Clear();
        _byName.Clear();
        _generation = 0;
    }

    private void Compact()
    {
        while (_heap.Count > 0 && _heap[0].Cancelled)
            PopMin();
    }

    private SimTimer PopMin()
    {
        var min = _heap[0];
        var last = _heap[^1];
        _heap.RemoveAt(_heap.Count - 1);
        if (_heap.Count > 0)
        {
            _heap[0] = last;
            SiftDown(0);
        }
        return min;
    }

    private void SiftUp(int i)
    {
        while (i > 0)
        {
            var p = (i - 1) / 2;
            if (Compare(_heap[i], _heap[p]) >= 0) break;
            (_heap[i], _heap[p]) = (_heap[p], _heap[i]);
            i = p;
        }
    }

    private void SiftDown(int i)
    {
        while (true)
        {
            var l = 2 * i + 1;
            var r = 2 * i + 2;
            var s = i;
            if (l < _heap.Count && Compare(_heap[l], _heap[s]) < 0) s = l;
            if (r < _heap.Count && Compare(_heap[r], _heap[s]) < 0) s = r;
            if (s == i) break;
            (_heap[i], _heap[s]) = (_heap[s], _heap[i]);
            i = s;
        }
    }

    private static int Compare(SimTimer a, SimTimer b)
    {
        var c = a.FireAt.CompareTo(b.FireAt);
        return c != 0 ? c : a.Generation.CompareTo(b.Generation);
    }
}
