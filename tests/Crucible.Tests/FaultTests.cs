
using Crucible.Abstractions;
using Crucible.Faults;
using Crucible.Protocols;
using Crucible.Runtime;
using FluentAssertions;

namespace Crucible.Tests;

public class FaultTests
{
    [Fact]
    public void Partition_blocks_delivery()
    {
        var clock = new VirtualClock();
        var rng = new SeededRng(1);
        var net = new SimNetwork(clock, rng);
        net.SetPartition(new[] { new NodeId(0) }, new[] { new NodeId(1) });
        net.CanCommunicate(new NodeId(0), new NodeId(1)).Should().BeFalse();
    }

    [Fact]
    public void Split_brain_schedule_registers_faults()
    {
        var nodes = new[] { new NodeId(0), new NodeId(1), new NodeId(2) };
        var faults = PartitionGenerator.SplitBrain(nodes, 10, 20);
        faults.Should().NotBeEmpty();
    }

    [Fact]
    public void Raft_runs_under_mild_chaos()
    {
        var (rt, inv) = ScenarioHost.Build("raft", 7, new WorkloadOptions { NodeCount = 5 }, chaos: ChaosProfile.Mild);
        rt.Run(800);
        rt.CheckAll(inv).Should().BeEmpty();
    }
}
