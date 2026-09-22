namespace Crucible.Abstractions;

/// <summary>Delivery semantics for a simulated network link.</summary>
public enum DeliveryOrder
{
    /// <summary>Per-sender FIFO.</summary>
    Fifo,
    /// <summary>Messages may be reordered arbitrarily (subject to faults).</summary>
    Reorderable
}

/// <summary>Link-level configuration between two nodes.</summary>
public sealed class LinkConfig
{
    public long MinLatencyTicks { get; init; } = 1;
    public long MaxLatencyTicks { get; init; } = 10;
    public double DropProbability { get; init; }
    public double DuplicateProbability { get; init; }
    public DeliveryOrder Order { get; init; } = DeliveryOrder.Fifo;
}

/// <summary>Simulated network surface used by protocols.</summary>
public interface ISimNetwork
{
    void Send(NodeId from, NodeId to, IMessage payload, string? correlationId = null);
    void Broadcast(NodeId from, IEnumerable<NodeId> recipients, IMessage payload);
    bool TryReceive(NodeId node, out MessageEnvelope? envelope);
    IReadOnlyList<MessageEnvelope> Drain(NodeId node);
    int PendingCount(NodeId node);
    void SetPartition(IReadOnlyCollection<NodeId> groupA, IReadOnlyCollection<NodeId> groupB);
    void HealPartitions();
    bool CanCommunicate(NodeId a, NodeId b);
    LinkConfig GetLink(NodeId a, NodeId b);
    void ConfigureLink(NodeId a, NodeId b, LinkConfig config);
}

/// <summary>Byte-addressable durable storage per node (simulated).</summary>
public interface ISimStorage
{
    void Write(NodeId node, string key, ReadOnlySpan<byte> data);
    bool TryRead(NodeId node, string key, out byte[] data);
    bool Delete(NodeId node, string key);
    IEnumerable<string> Keys(NodeId node);
    void Clear(NodeId node);
}
