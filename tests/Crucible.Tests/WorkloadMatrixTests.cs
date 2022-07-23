namespace Crucible.Tests;

using Crucible.Abstractions;
using Crucible.Protocols;
using Crucible.Runtime;
using Xunit;

public class WorkloadMatrixTests
{
    [Theory]
    [InlineData("raft", 10)]
    [InlineData("raft", 42)]
    [InlineData("or-set", 10)]
    [InlineData("or-set", 25)]
    [InlineData("lww-register", 15)]
    [InlineData("lww-register", 30)]
    [InlineData("rga", 20)]
    [InlineData("rga", 50)]
    [InlineData("two-phase-commit", 10)]
    [InlineData("two-phase-commit", 42)]
    [InlineData("multi-paxos", 10)]
    [InlineData("paxos", 42)]
    [InlineData("gossip", 10)]
    [InlineData("gossip", 42)]
    [InlineData("treiber-stack", 10)]
    [InlineData("treiber-stack", 42)]
    public void Workload_runs_cleanly_without_invariant_violations(string workload, int seed)
    {
        var options = new WorkloadOptions { NodeCount = 5, ClientOpLimit = 5 };
        var (runtime, invariants) = ScenarioHost.Build(workload, seed, options);
        runtime.Run(300);

        var violations = runtime.CheckAll(invariants);
        Assert.Empty(violations);
        Assert.True(runtime.StepsExecuted > 0, $"Workload {workload} should execute steps");
    }

    [Theory]
    [InlineData("raft")]
    [InlineData("or-set")]
    [InlineData("two-phase-commit")]
    public void Workload_progresses_virtual_time_and_delivers_traffic(string workload)
    {
        var options = new WorkloadOptions { NodeCount = 5, ClientOpLimit = 5 };
        var (runtime, _) = ScenarioHost.Build(workload, 42, options);
        runtime.Run(200);

        Assert.True(runtime.Clock.Now.Ticks > 0, $"Workload {workload} should advance virtual time");
        Assert.True(runtime.Network.MessagesDelivered > 0, $"Workload {workload} should deliver messages");
    }
}