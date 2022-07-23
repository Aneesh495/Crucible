namespace Crucible.Tests;

using Crucible.Abstractions;
using Crucible.Faults;
using Crucible.Protocols;
using Crucible.Protocols.Raft;
using Crucible.Runtime;
using Xunit;

public class FaultTests
{
    [Fact]
    public void Partition_blocks_delivery_and_heal_restores_it()
    {
        var clock = new VirtualClock();
        var rng = new SeededRng(1);
        var net = new SimNetwork(clock, rng);
        var n0 = new NodeId(0);
        var n1 = new NodeId(1);

        net.SetPartition(new[] { n0 }, new[] { n1 });
        Assert.False(net.CanCommunicate(n0, n1));

        net.Send(n0, n1, new PingMessage(1));
        clock.AdvanceBy(100);
        net.DeliverDue();
        Assert.Equal(0, net.PendingCount(n1)); // dropped by partition

        net.HealPartitions();
        Assert.True(net.CanCommunicate(n0, n1));

        net.Send(n0, n1, new PingMessage(2));
        clock.AdvanceBy(100);
        net.DeliverDue();
        Assert.Equal(1, net.PendingCount(n1)); // delivered after healing
    }

    [Fact]
    public void Split_brain_schedule_registers_faults()
    {
        var nodes = new[] { new NodeId(0), new NodeId(1), new NodeId(2) };
        var faults = PartitionGenerator.SplitBrain(nodes, 10, 20);
        Assert.NotEmpty(faults);
        Assert.Contains(faults, f => f.Kind == FaultKind.Partition);
        Assert.Contains(faults, f => f.Kind == FaultKind.Heal);
    }

    [Fact]
    public void Targeted_drop_only_affects_targeted_nodes()
    {
        var clock = new VirtualClock();
        var rng = new SeededRng(1);
        var net = new SimNetwork(clock, rng);
        var n0 = new NodeId(0);
        var n1 = new NodeId(1);
        var n2 = new NodeId(2);

        net.Send(n0, n1, new PingMessage(1));
        net.Send(n0, n2, new PingMessage(2));

        // Drop traffic specifically targeting n1
        net.DropMessages(target: n1);

        clock.AdvanceBy(100);
        net.DeliverDue();

        Assert.Equal(0, net.PendingCount(n1)); // dropped
        Assert.Equal(1, net.PendingCount(n2)); // preserved
    }

    [Fact]
    public void Clock_skew_shifts_node_perceived_time()
    {
        var rt = new DeterministicRuntime(42);
        var n0 = new NodeId(0);
        rt.Register(new EchoProcess(n0));
        rt.StartAll();

        Assert.Equal(0, rt.GetClockSkew(n0));
        Assert.Equal(rt.Clock.Now, rt.GetNodeTime(n0));

        // Inject clock skew fault of +500 ticks
        rt.RegisterFault(new FaultEvent
        {
            Kind = FaultKind.ClockSkew,
            Target = n0,
            DelayTicks = 500,
            AtStep = 0
        });

        rt.StepOnce();
        Assert.Equal(500, rt.GetClockSkew(n0));
        Assert.Equal(rt.Clock.Now.Add(500), rt.GetNodeTime(n0));
    }

    [Fact]
    public void Crash_and_restart_persists_raft_state()
    {
        var rt = new DeterministicRuntime(42);
        var n0 = new NodeId(0);
        var options = new WorkloadOptions { NodeCount = 3 };
        var raftNode = new RaftNode(n0, 3, options);
        rt.Register(raftNode);
        rt.StartAll();

        // Simulate node becoming candidate and updating term/votedFor
        raftNode.Persistent.CurrentTerm = 5;
        raftNode.Persistent.VotedFor = 0;
        raftNode.Persistent.AppendEntry(new LogEntry(Term: 5, Index: 1, Command: "cmd-1"));
        raftNode.Persistent.Persist(rt.Storage, n0);

        // Crash node
        rt.Crash(n0);
        Assert.Equal(ProcessState.Crashed, raftNode.State);

        // Restart node: must reload state from storage
        rt.Restart(n0);
        Assert.Equal(ProcessState.Running, raftNode.State);
        Assert.Equal(5, raftNode.Persistent.CurrentTerm);
        Assert.Equal(0, raftNode.Persistent.VotedFor);
        Assert.Equal(1, raftNode.Persistent.LastLogIndex);
    }

    [Fact]
    public void Raft_runs_under_mild_chaos()
    {
        var (rt, inv) = ScenarioHost.Build("raft", 7, new WorkloadOptions { NodeCount = 5 }, chaos: ChaosProfile.Mild);
        rt.Run(800);
        var violations = rt.CheckAll(inv);
        Assert.Empty(violations);
    }

    private sealed class EchoProcess : ISimProcess
    {
        public EchoProcess(NodeId id) => Id = id;
        public NodeId Id { get; }
        public ProcessState State { get; private set; } = ProcessState.Stopped;
        public void Start(ISimContext context) { State = ProcessState.Running; }
        public void OnMessage(MessageEnvelope envelope) { }
        public void OnTimer(string name, long generation) { }
        public void OnCrash() => State = ProcessState.Crashed;
        public void OnRestart(ISimContext context) => State = ProcessState.Running;
        public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();
    }
}
