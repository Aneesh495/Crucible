namespace Crucible.Abstractions;

/// <summary>Opaque message payload carried across the simulated network.</summary>
public interface IMessage
{
    string TypeName { get; }
}

/// <summary>Envelope wrapping a payload with routing and delivery metadata.</summary>
public sealed class MessageEnvelope
{
    public long MessageId { get; init; }
    public NodeId From { get; init; }
    public NodeId To { get; init; }
    public IMessage Payload { get; init; } = null!;
    public SimTime SentAt { get; init; }
    public SimTime DeliverAt { get; set; }
    public int HopCount { get; set; }
    public bool IsDuplicate { get; init; }
    public string? CorrelationId { get; init; }

    public MessageEnvelope Clone() => new()
    {
        MessageId = MessageId,
        From = From,
        To = To,
        Payload = Payload,
        SentAt = SentAt,
        DeliverAt = DeliverAt,
        HopCount = HopCount,
        IsDuplicate = IsDuplicate,
        CorrelationId = CorrelationId
    };

    public override string ToString() =>
        $"[{MessageId}] {From}->{To} {Payload.TypeName} @ {DeliverAt}";
}

/// <summary>Typed helper for constructing common control messages.</summary>
public sealed record PingMessage(long Nonce) : IMessage
{
    public string TypeName => nameof(PingMessage);
}

public sealed record PongMessage(long Nonce) : IMessage
{
    public string TypeName => nameof(PongMessage);
}

public sealed record TimerFiredMessage(string TimerName, long Generation) : IMessage
{
    public string TypeName => nameof(TimerFiredMessage);
}
