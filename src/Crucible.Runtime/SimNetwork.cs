namespace Crucible.Runtime;

using Crucible.Abstractions;

/// <summary>
/// Simulated network with per-link latency, drop/dup, partitions, and delivery queues.
/// All time is logical; delivery happens when the scheduler drains due messages.
/// </summary>
public sealed class SimNetwork : ISimNetwork
{
    private readonly ISimClock _clock;
    private readonly ISimRandom _random;
    private readonly IScheduleOracle? _oracle;
    private readonly Dictionary<(NodeId, NodeId), LinkConfig> _links = new();
    private readonly Dictionary<NodeId, List<MessageEnvelope>> _inboxes = new();
    private readonly List<MessageEnvelope> _inFlight = new();
    private readonly HashSet<(NodeId, NodeId)> _blocked = new();
    private long _nextMessageId = 1;
    private LinkConfig _defaultLink = new();

    public SimNetwork(ISimClock clock, ISimRandom random, IScheduleOracle? oracle = null)
    {
        _clock = clock;
        _random = random;
        _oracle = oracle;
    }

    public IReadOnlyList<MessageEnvelope> InFlight => _inFlight.ToArray();
    public long MessagesSent { get; private set; }
    public long MessagesDropped { get; private set; }
    public long MessagesDuplicated { get; private set; }
    public long MessagesDelivered { get; private set; }

    public void SetDefaultLink(LinkConfig config) => _defaultLink = config;

    public LinkConfig GetLink(NodeId a, NodeId b)
    {
        if (_links.TryGetValue((a, b), out var cfg))
            return cfg;
        return _defaultLink;
    }

    public void ConfigureLink(NodeId a, NodeId b, LinkConfig config) => _links[(a, b)] = config;

    public bool CanCommunicate(NodeId a, NodeId b)
    {
        if (a == b) return true;
        return !_blocked.Contains((a, b));
    }

    public void SetPartition(IReadOnlyCollection<NodeId> groupA, IReadOnlyCollection<NodeId> groupB)
    {
        foreach (var a in groupA)
        foreach (var b in groupB)
        {
            _blocked.Add((a, b));
            _blocked.Add((b, a));
        }
    }

    public void HealPartitions() => _blocked.Clear();

    public void Send(NodeId from, NodeId to, IMessage payload, string? correlationId = null)
    {
        ArgumentNullException.ThrowIfNull(payload);
        MessagesSent++;

        if (!CanCommunicate(from, to))
        {
            MessagesDropped++;
            return;
        }

        var link = GetLink(from, to);
        if (link.DropProbability > 0 && _random.NextBool(link.DropProbability))
        {
            MessagesDropped++;
            return;
        }

        var latency = link.MinLatencyTicks == link.MaxLatencyTicks
            ? link.MinLatencyTicks
            : _random.NextLong(link.MinLatencyTicks, link.MaxLatencyTicks + 1);

        var envelope = new MessageEnvelope
        {
            MessageId = _nextMessageId++,
            From = from,
            To = to,
            Payload = payload,
            SentAt = _clock.Now,
            DeliverAt = _clock.Now.Add(latency),
            CorrelationId = correlationId
        };
        _inFlight.Add(envelope);

        if (link.DuplicateProbability > 0 && _random.NextBool(link.DuplicateProbability))
        {
            MessagesDuplicated++;
            _inFlight.Add(new MessageEnvelope
            {
                MessageId = _nextMessageId++,
                From = from,
                To = to,
                Payload = payload,
                SentAt = envelope.SentAt,
                DeliverAt = envelope.DeliverAt.Add(_random.NextLong(0, 5)),
                IsDuplicate = true,
                CorrelationId = correlationId
            });
        }
    }

    public void Broadcast(NodeId from, IEnumerable<NodeId> recipients, IMessage payload)
    {
        foreach (var to in recipients)
        {
            if (to != from)
                Send(from, to, payload);
        }
    }

    /// <summary>
    /// Move all due in-flight messages into destination inboxes.
    /// When reorderable, the oracle (or RNG) picks delivery order among due messages to a node.
    /// </summary>
    public int DeliverDue()
    {
        var due = new List<MessageEnvelope>();
        for (var i = _inFlight.Count - 1; i >= 0; i--)
        {
            if (_inFlight[i].DeliverAt <= _clock.Now)
            {
                due.Add(_inFlight[i]);
                _inFlight.RemoveAt(i);
            }
        }

        if (due.Count == 0)
            return 0;

        // Group by destination for ordering decisions.
        var byDest = due.GroupBy(m => m.To);
        var delivered = 0;
        foreach (var group in byDest)
        {
            var list = group.ToList();
            var link = list.Count > 0 ? GetLink(list[0].From, group.Key) : _defaultLink;
            if (link.Order == DeliveryOrder.Reorderable && list.Count > 1)
            {
                // Explorer may permute; default shuffles with RNG.
                if (_oracle is not null)
                {
                    for (var i = list.Count - 1; i > 0; i--)
                    {
                        var pick = _oracle.Choose(SchedulingPointKind.MessageDeliver, i + 1, group.Key, "reorder");
                        (list[i], list[pick]) = (list[pick], list[i]);
                    }
                }
                else
                {
                    _random.Shuffle(list);
                }
            }
            else
            {
                list.Sort((a, b) =>
                {
                    var c = a.DeliverAt.CompareTo(b.DeliverAt);
                    return c != 0 ? c : a.MessageId.CompareTo(b.MessageId);
                });
            }

            if (!_inboxes.TryGetValue(group.Key, out var inbox))
            {
                inbox = new List<MessageEnvelope>();
                _inboxes[group.Key] = inbox;
            }
            foreach (var msg in list)
            {
                if (!CanCommunicate(msg.From, msg.To))
                {
                    MessagesDropped++;
                    continue;
                }
                inbox.Add(msg);
                MessagesDelivered++;
                delivered++;
            }
        }
        return delivered;
    }

    public bool TryReceive(NodeId node, out MessageEnvelope? envelope)
    {
        if (_inboxes.TryGetValue(node, out var inbox) && inbox.Count > 0)
        {
            envelope = inbox[0];
            inbox.RemoveAt(0);
            return true;
        }
        envelope = null;
        return false;
    }

    public IReadOnlyList<MessageEnvelope> Drain(NodeId node)
    {
        if (!_inboxes.TryGetValue(node, out var inbox) || inbox.Count == 0)
            return Array.Empty<MessageEnvelope>();
        var all = inbox.ToArray();
        inbox.Clear();
        return all;
    }

    public int PendingCount(NodeId node) =>
        _inboxes.TryGetValue(node, out var inbox) ? inbox.Count : 0;

    public SimTime? NextDeliveryTime()
    {
        if (_inFlight.Count == 0) return null;
        var min = _inFlight[0].DeliverAt;
        for (var i = 1; i < _inFlight.Count; i++)
            if (_inFlight[i].DeliverAt < min)
                min = _inFlight[i].DeliverAt;
        return min;
    }

    public void DropAllInFlight()
    {
        MessagesDropped += _inFlight.Count;
        _inFlight.Clear();
    }

    public void Reset()
    {
        _inFlight.Clear();
        _inboxes.Clear();
        _blocked.Clear();
        _links.Clear();
        _nextMessageId = 1;
        MessagesSent = MessagesDropped = MessagesDuplicated = MessagesDelivered = 0;
    }
}
