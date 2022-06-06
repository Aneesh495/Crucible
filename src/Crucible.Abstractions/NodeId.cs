namespace Crucible.Abstractions;

/// <summary>Stable identity for a simulated process or node.</summary>
public readonly struct NodeId : IEquatable<NodeId>, IComparable<NodeId>
{
    public int Value { get; }

    public NodeId(int value)
    {
        if (value < 0)
            throw new ArgumentOutOfRangeException(nameof(value), "NodeId must be non-negative.");
        Value = value;
    }

    public bool Equals(NodeId other) => Value == other.Value;
    public override bool Equals(object? obj) => obj is NodeId other && Equals(other);
    public override int GetHashCode() => Value;
    public int CompareTo(NodeId other) => Value.CompareTo(other.Value);
    public override string ToString() => $"n{Value}";

    public static bool operator ==(NodeId left, NodeId right) => left.Equals(right);
    public static bool operator !=(NodeId left, NodeId right) => !left.Equals(right);
    public static implicit operator int(NodeId id) => id.Value;
    public static explicit operator NodeId(int value) => new(value);
}
