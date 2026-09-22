namespace Crucible.Structures;

using System.Collections.Concurrent;
using Crucible.Abstractions;

/// <summary>In-process actor mailbox (logical; used by protocol actors).</summary>
public sealed class ActorMailbox<T>
{
    private readonly Queue<T> _queue = new();
    private readonly object _gate = new();

    public void Post(T message)
    {
        lock (_gate) _queue.Enqueue(message);
    }

    public bool TryReceive(out T? message)
    {
        lock (_gate)
        {
            if (_queue.Count == 0)
            {
                message = default;
                return false;
            }
            message = _queue.Dequeue();
            return true;
        }
    }

    public int Count
    {
        get { lock (_gate) return _queue.Count; }
    }
}

public sealed class ActorSystem
{
    private readonly Dictionary<NodeId, Action<object>> _handlers = new();

    public void Register(NodeId id, Action<object> handler) => _handlers[id] = handler;

    public void Dispatch(NodeId id, object message)
    {
        if (_handlers.TryGetValue(id, out var h))
            h(message);
    }
}
