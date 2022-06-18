namespace Crucible.Faults;

using Crucible.Abstractions;
using Crucible.Runtime;

/// <summary>Named chaos profiles applied before simulation starts.</summary>
public sealed class ChaosProfile
{
    public string Name { get; init; } = "default";
    public double DropProbability { get; init; }
    public double DuplicateProbability { get; init; }
    public long MinLatency { get; init; } = 1;
    public long MaxLatency { get; init; } = 8;
    public DeliveryOrder Order { get; init; } = DeliveryOrder.Reorderable;
    public IReadOnlyList<FaultEvent> Scripted { get; init; } = Array.Empty<FaultEvent>();

    public void ConfigureNetwork(SimNetwork network, IReadOnlyList<NodeId> nodes)
    {
        var link = new LinkConfig
        {
            DropProbability = DropProbability,
            DuplicateProbability = DuplicateProbability,
            MinLatencyTicks = MinLatency,
            MaxLatencyTicks = MaxLatency,
            Order = Order
        };
        network.SetDefaultLink(link);
        foreach (var a in nodes)
        foreach (var b in nodes)
        {
            if (a != b)
                network.ConfigureLink(a, b, link);
        }
    }

    public void ApplyScripted(DeterministicRuntime runtime)
    {
        foreach (var f in Scripted)
            runtime.RegisterFault(f);
    }

    public static ChaosProfile Mild => new()
    {
        Name = "mild",
        MinLatency = 2,
        MaxLatency = 6,
        Order = DeliveryOrder.Fifo
    };

    public static ChaosProfile Adversarial => new()
    {
        Name = "adversarial",
        DropProbability = 0.05,
        DuplicateProbability = 0.03,
        MinLatency = 1,
        MaxLatency = 20,
        Order = DeliveryOrder.Reorderable
    };
}
