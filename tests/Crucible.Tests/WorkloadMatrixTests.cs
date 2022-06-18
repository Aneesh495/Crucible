using Crucible.Abstractions;
using Crucible.Protocols;
using Crucible.Runtime;
using FluentAssertions;

namespace Crucible.Tests;

public class WorkloadMatrixTests
{

    [Fact]
    public void or_set_seed_10_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("or-set", 10, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=10");
    }

    [Fact]
    public void or_set_seed_15_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("or-set", 15, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=15");
    }

    [Fact]
    public void or_set_seed_20_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("or-set", 20, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=20");
    }

    [Fact]
    public void or_set_seed_25_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("or-set", 25, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=25");
    }

    [Fact]
    public void or_set_seed_30_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("or-set", 30, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=30");
    }

    [Fact]
    public void or_set_seed_35_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("or-set", 35, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=35");
    }

    [Fact]
    public void or_set_seed_40_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("or-set", 40, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=40");
    }

    [Fact]
    public void or_set_seed_45_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("or-set", 45, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=45");
    }

    [Fact]
    public void or_set_seed_50_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("or-set", 50, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=50");
    }

    [Fact]
    public void or_set_seed_55_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("or-set", 55, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=55");
    }

    [Fact]
    public void lww_register_seed_10_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("lww-register", 10, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=10");
    }

    [Fact]
    public void lww_register_seed_15_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("lww-register", 15, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=15");
    }

    [Fact]
    public void lww_register_seed_20_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("lww-register", 20, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=20");
    }

    [Fact]
    public void lww_register_seed_25_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("lww-register", 25, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=25");
    }

    [Fact]
    public void lww_register_seed_30_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("lww-register", 30, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=30");
    }

    [Fact]
    public void lww_register_seed_35_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("lww-register", 35, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=35");
    }

    [Fact]
    public void lww_register_seed_40_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("lww-register", 40, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=40");
    }

    [Fact]
    public void lww_register_seed_45_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("lww-register", 45, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=45");
    }

    [Fact]
    public void lww_register_seed_50_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("lww-register", 50, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=50");
    }

    [Fact]
    public void lww_register_seed_55_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("lww-register", 55, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=55");
    }

    [Fact]
    public void gossip_seed_10_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("gossip", 10, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=10");
    }

    [Fact]
    public void gossip_seed_15_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("gossip", 15, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=15");
    }

    [Fact]
    public void gossip_seed_20_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("gossip", 20, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=20");
    }

    [Fact]
    public void gossip_seed_25_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("gossip", 25, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=25");
    }

    [Fact]
    public void gossip_seed_30_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("gossip", 30, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=30");
    }

    [Fact]
    public void gossip_seed_35_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("gossip", 35, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=35");
    }

    [Fact]
    public void gossip_seed_40_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("gossip", 40, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=40");
    }

    [Fact]
    public void gossip_seed_45_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("gossip", 45, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=45");
    }

    [Fact]
    public void gossip_seed_50_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("gossip", 50, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=50");
    }

    [Fact]
    public void gossip_seed_55_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("gossip", 55, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=55");
    }

    [Fact]
    public void two_phase_commit_seed_10_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("two-phase-commit", 10, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=10");
    }

    [Fact]
    public void two_phase_commit_seed_15_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("two-phase-commit", 15, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=15");
    }

    [Fact]
    public void two_phase_commit_seed_20_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("two-phase-commit", 20, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=20");
    }

    [Fact]
    public void two_phase_commit_seed_25_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("two-phase-commit", 25, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=25");
    }

    [Fact]
    public void two_phase_commit_seed_30_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("two-phase-commit", 30, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=30");
    }

    [Fact]
    public void two_phase_commit_seed_35_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("two-phase-commit", 35, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=35");
    }

    [Fact]
    public void two_phase_commit_seed_40_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("two-phase-commit", 40, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=40");
    }

    [Fact]
    public void two_phase_commit_seed_45_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("two-phase-commit", 45, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=45");
    }

    [Fact]
    public void two_phase_commit_seed_50_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("two-phase-commit", 50, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=50");
    }

    [Fact]
    public void two_phase_commit_seed_55_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("two-phase-commit", 55, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=55");
    }

    [Fact]
    public void raft_seed_10_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 10, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=10");
    }

    [Fact]
    public void raft_seed_15_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 15, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=15");
    }

    [Fact]
    public void raft_seed_20_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 20, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=20");
    }

    [Fact]
    public void raft_seed_25_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 25, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=25");
    }

    [Fact]
    public void raft_seed_30_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 30, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=30");
    }

    [Fact]
    public void raft_seed_35_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 35, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=35");
    }

    [Fact]
    public void raft_seed_40_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 40, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=40");
    }

    [Fact]
    public void raft_seed_45_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 45, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=45");
    }

    [Fact]
    public void raft_seed_50_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 50, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=50");
    }

    [Fact]
    public void raft_seed_55_runs()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 55, options);
        runtime.Run(300);
        var violations = runtime.CheckAll(invariants);
        violations.Should().BeEmpty("workload={wl} seed=55");
    }
}