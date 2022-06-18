namespace Crucible.Faults;

using Crucible.Abstractions;
using Crucible.Runtime;

/// <summary>Generates partition/heal sequences for n-node clusters.</summary>
public static class PartitionGenerator
{
    public static IReadOnlyList<FaultEvent> SplitBrain(
        IReadOnlyList<NodeId> nodes,
        int partitionStep,
        int healStep)
    {
        if (nodes.Count < 3)
            throw new ArgumentException("Split-brain scenario needs at least 3 nodes.");

        var mid = nodes.Count / 2;
        var a = nodes.Take(mid).ToArray();
        var b = nodes.Skip(mid).ToArray();
        return new FaultScheduleBuilder()
            .Partition(a, b, partitionStep)
            .Heal(healStep)
            .Build();
    }

    public static IReadOnlyList<FaultEvent> IsolatedMinority(
        NodeId isolated,
        IReadOnlyList<NodeId> majority,
        int partitionStep,
        int healStep)
    {
        var builder = new FaultScheduleBuilder();
        foreach (var m in majority)
            builder.Partition(new[] { isolated }, new[] { m }, partitionStep);
        builder.Heal(healStep);
        return builder.Build();
    }

    public static IReadOnlyList<FaultEvent> RandomPartitions(
        ISimRandom random,
        IReadOnlyList<NodeId> nodes,
        int count,
        int stepStride)
    {
        var events = new List<FaultEvent>();
        for (var i = 0; i < count; i++)
        {
            var step = i * stepStride + 10;
            var shuffled = nodes.ToList();
            random.Shuffle(shuffled);
            var cut = random.Next(1, shuffled.Count);
            var a = shuffled.Take(cut).ToArray();
            var b = shuffled.Skip(cut).ToArray();
            if (a.Length == 0 || b.Length == 0) continue;
            events.Add(new FaultEvent
            {
                Kind = FaultKind.Partition,
                AtStep = step,
                PartitionA = a,
                PartitionB = b,
                Label = $"random-partition-{i}"
            });
            events.Add(new FaultEvent { Kind = FaultKind.Heal, AtStep = step + stepStride / 2 });
        }
        return events;
    }
}
