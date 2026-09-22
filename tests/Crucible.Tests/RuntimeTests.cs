
using Crucible.Abstractions;
using Crucible.Runtime;
using FluentAssertions;

namespace Crucible.Tests;

public class RuntimeTests
{
    [Fact]
    public void VirtualClock_never_moves_backward()
    {
        var clock = new VirtualClock();
        clock.AdvanceBy(10);
        clock.Now.Ticks.Should().Be(10);
        clock.Invoking(c => c.AdvanceTo(SimTime.Zero)).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void SeededRng_is_deterministic()
    {
        var a = new SeededRng(7);
        var b = new SeededRng(7);
        Enumerable.Range(0, 20).Select(_ => a.Next()).Should().Equal(Enumerable.Range(0, 20).Select(_ => b.Next()));
    }

    [Fact]
    public void SimNetwork_delivers_with_latency()
    {
        var clock = new VirtualClock();
        var rng = new SeededRng(1);
        var net = new SimNetwork(clock, rng);
        net.Send(new NodeId(0), new NodeId(1), new PingMessage(1));
        net.PendingCount(new NodeId(1)).Should().Be(0);
        clock.AdvanceBy(100);
        net.DeliverDue().Should().BeGreaterThan(0);
        net.TryReceive(new NodeId(1), out var env).Should().BeTrue();
        env!.Payload.Should().BeOfType<PingMessage>();
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
        Run(99).Should().Be(Run(99));
    }

    private sealed class EchoProcess : ISimProcess
    {
        public EchoProcess(NodeId id) => Id = id;
        public NodeId Id { get; }
        public ProcessState State { get; private set; } = ProcessState.Stopped;
        public void Start(ISimContext context) { State = ProcessState.Running; context.SetTimer("t", 1); }
        public void OnMessage(MessageEnvelope envelope) { }
        public void OnTimer(string name, long generation) { }
        public void OnCrash() => State = ProcessState.Crashed;
        public void OnRestart(ISimContext context) => State = ProcessState.Running;
        public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();
    }
}
