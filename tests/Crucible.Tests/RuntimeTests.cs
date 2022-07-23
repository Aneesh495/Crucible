namespace Crucible.Tests;

using Crucible.Abstractions;
using Crucible.Runtime;
using Xunit;

public class RuntimeTests
{
    [Fact]
    public void VirtualClock_never_moves_backward()
    {
        var clock = new VirtualClock();
        clock.AdvanceBy(10);
        Assert.Equal(10, clock.Now.Ticks);
        Assert.Throws<InvalidOperationException>(() => clock.AdvanceTo(SimTime.Zero));
    }

    [Fact]
    public void SeededRng_is_deterministic()
    {
        var a = new SeededRng(7);
        var b = new SeededRng(7);
        var seqA = Enumerable.Range(0, 20).Select(_ => a.Next()).ToArray();
        var seqB = Enumerable.Range(0, 20).Select(_ => b.Next()).ToArray();
        Assert.Equal(seqA, seqB);
    }

    [Fact]
    public void SimNetwork_delivers_with_latency()
    {
        var clock = new VirtualClock();
        var rng = new SeededRng(1);
        var net = new SimNetwork(clock, rng);
        net.Send(new NodeId(0), new NodeId(1), new PingMessage(1));
        Assert.Equal(0, net.PendingCount(new NodeId(1)));

        clock.AdvanceBy(100);
        Assert.True(net.DeliverDue() > 0);
        Assert.True(net.TryReceive(new NodeId(1), out var env));
        Assert.IsType<PingMessage>(env!.Payload);
    }

    [Fact]
    public void DeterministicRuntime_replays_same_seed()
    {
        static long Run(int seed)
        {
            var rt = new DeterministicRuntime(seed);
            rt.Register(new EchoProcess(new NodeId(0)));
            rt.StartAll();
            for (var i = 0; i < 50; i++) rt.StepOnce();
            return rt.StepsExecuted;
        }
        Assert.Equal(Run(99), Run(99));
    }

    [Fact]
    public void DeterministicRuntime_advances_virtual_time_when_idle()
    {
        var rt = new DeterministicRuntime(42);
        var n0 = new NodeId(0);
        var proc = new EchoProcess(n0);
        rt.Register(proc);
        rt.StartAll();

        // Initially at time 0
        Assert.Equal(0, rt.Clock.Now.Ticks);

        // Process scheduled a timer for delay = 50 ticks
        // Calling StepOnce should advance virtual time to 50 and fire the timer!
        Assert.True(rt.StepOnce());
        Assert.Equal(50, rt.Clock.Now.Ticks);
        Assert.Equal(1, rt.Metrics.TimersFired);
    }

    [Fact]
    public void Transitions_are_discrete_and_atomic()
    {
        var rt = new DeterministicRuntime(42);
        var n0 = new NodeId(0);
        var n1 = new NodeId(1);
        var proc0 = new EchoProcess(n0);
        var proc1 = new EchoProcess(n1);
        rt.Register(proc0);
        rt.Register(proc1);
        rt.StartAll();

        rt.Network.ConfigureLink(n0, n1, new LinkConfig { MinLatencyTicks = 10, MaxLatencyTicks = 10 });

        // Send a message with latency
        rt.Network.Send(n0, n1, new PingMessage(1));

        // Step: advances to t=10 and delivers message
        rt.StepOnce();
        Assert.Equal(10, rt.Clock.Now.Ticks);
        Assert.Equal(1, rt.Network.MessagesDelivered);

        // Message is now in n1's inbox, not yet processed
        Assert.Equal(1, rt.Network.PendingCount(n1));

        // Next transition: n1 processes message
        rt.StepOnce();
        Assert.Equal(0, rt.Network.PendingCount(n1));
    }

    private sealed class EchoProcess : ISimProcess
    {
        public EchoProcess(NodeId id) => Id = id;
        public NodeId Id { get; }
        public ProcessState State { get; private set; } = ProcessState.Stopped;
        public void Start(ISimContext context)
        {
            State = ProcessState.Running;
            context.SetTimer("delayed-action", 50);
        }
        public void OnMessage(MessageEnvelope envelope) { }
        public void OnTimer(string name, long generation) { }
        public void OnCrash() => State = ProcessState.Crashed;
        public void OnRestart(ISimContext context) => State = ProcessState.Running;
        public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();
    }
}
