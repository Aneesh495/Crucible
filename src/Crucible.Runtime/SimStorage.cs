namespace Crucible.Runtime;

using Crucible.Abstractions;

/// <summary>In-memory per-node key/value store with optional durability journaling.</summary>
public sealed class SimStorage : ISimStorage
{
    private readonly Dictionary<NodeId, Dictionary<string, byte[]>> _nodes = new();
    private readonly List<StorageOp> _journal = new();
    private readonly object _gate = new();

    public IReadOnlyList<StorageOp> Journal
    {
        get { lock (_gate) return _journal.ToArray(); }
    }

    public void Write(NodeId node, string key, ReadOnlySpan<byte> data)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        var copy = data.ToArray();
        lock (_gate)
        {
            if (!_nodes.TryGetValue(node, out var bag))
            {
                bag = new Dictionary<string, byte[]>(StringComparer.Ordinal);
                _nodes[node] = bag;
            }
            bag[key] = copy;
            _journal.Add(new StorageOp(StorageOpKind.Write, node, key, copy.Length));
        }
    }

    public bool TryRead(NodeId node, string key, out byte[] data)
    {
        lock (_gate)
        {
            if (_nodes.TryGetValue(node, out var bag) && bag.TryGetValue(key, out var found))
            {
                data = found.ToArray();
                return true;
            }
            data = Array.Empty<byte>();
            return false;
        }
    }

    public bool Delete(NodeId node, string key)
    {
        lock (_gate)
        {
            if (_nodes.TryGetValue(node, out var bag) && bag.Remove(key))
            {
                _journal.Add(new StorageOp(StorageOpKind.Delete, node, key, 0));
                return true;
            }
            return false;
        }
    }

    public IEnumerable<string> Keys(NodeId node)
    {
        lock (_gate)
        {
            if (!_nodes.TryGetValue(node, out var bag))
                return Array.Empty<string>();
            return bag.Keys.OrderBy(k => k, StringComparer.Ordinal).ToArray();
        }
    }

    public void Clear(NodeId node)
    {
        lock (_gate)
        {
            if (_nodes.Remove(node))
                _journal.Add(new StorageOp(StorageOpKind.Clear, node, "*", 0));
        }
    }

    public void Reset()
    {
        lock (_gate)
        {
            _nodes.Clear();
            _journal.Clear();
        }
    }
}

public enum StorageOpKind { Write, Delete, Clear }

public readonly record struct StorageOp(StorageOpKind Kind, NodeId Node, string Key, int ByteCount);
