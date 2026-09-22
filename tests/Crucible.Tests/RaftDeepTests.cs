using Crucible.Abstractions;
using Crucible.Faults;
using Crucible.Protocols;
using Crucible.Protocols.Raft;
using Crucible.Runtime;
using FluentAssertions;

namespace Crucible.Tests;

public class RaftDeepTests
{

    [Fact]
    public void Raft_election_safety_seed_100()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 100, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_101()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 101, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_102()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 102, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_103()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 103, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_104()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 104, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_105()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 105, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_106()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 106, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_107()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 107, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_108()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 108, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_109()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 109, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_110()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 110, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_111()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 111, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_112()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 112, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_113()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 113, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_114()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 114, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_115()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 115, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_116()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 116, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_117()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 117, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_118()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 118, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_119()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 119, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_120()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 120, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_121()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 121, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_122()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 122, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_123()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 123, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_124()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 124, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_125()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 125, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_126()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 126, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_127()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 127, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_128()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 128, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_129()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 129, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_130()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 130, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_131()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 131, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_132()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 132, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_133()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 133, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_134()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 134, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_135()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 135, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_136()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 136, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_137()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 137, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_138()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 138, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_139()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 139, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_140()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 140, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_141()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 141, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_142()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 142, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_143()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 143, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_144()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 144, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_145()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 145, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_146()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 146, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_147()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 147, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_148()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 148, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_149()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 149, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_150()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 150, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_151()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 151, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_152()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 152, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_153()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 153, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_154()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 154, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_155()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 155, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_156()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 156, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_157()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 157, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_158()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 158, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_159()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 159, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_160()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 160, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_161()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 161, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_162()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 162, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_163()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 163, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_164()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 164, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_165()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 165, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_166()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 166, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_167()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 167, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_168()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 168, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_169()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 169, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_170()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 170, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_171()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 171, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_172()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 172, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_173()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 173, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_174()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 174, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_175()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 175, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_176()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 176, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_177()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 177, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_178()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 178, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_179()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 179, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_180()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 180, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_181()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 181, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_182()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 182, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_183()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 183, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_184()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 184, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_185()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 185, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_186()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 186, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_187()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 187, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_188()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 188, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_189()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 189, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_190()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 190, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_191()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 191, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_192()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 192, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_193()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 193, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_194()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 194, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_195()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 195, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_196()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 196, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_197()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 197, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_198()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 198, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_199()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 199, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_200()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 200, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_201()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 201, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_202()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 202, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_203()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 203, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_204()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 204, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_205()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 205, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_206()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 206, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_207()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 207, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_208()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 208, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_209()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 209, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_210()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 210, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_211()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 211, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_212()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 212, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_213()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 213, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_214()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 214, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_215()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 215, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_216()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 216, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_217()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 217, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_218()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 218, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_219()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 219, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_220()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 220, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_221()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 221, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_222()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 222, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_223()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 223, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_224()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 224, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_225()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 225, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_226()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 226, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_227()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 227, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_228()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 228, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_229()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 229, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_230()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 230, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_231()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 231, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_232()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 232, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_233()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 233, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_234()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 234, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_235()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 235, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_236()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 236, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_237()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 237, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_238()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 238, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_239()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 239, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_240()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 240, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_241()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 241, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_242()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 242, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_243()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 243, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_244()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 244, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_245()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 245, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_246()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 246, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_247()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 247, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_248()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 248, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_249()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 249, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_250()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 250, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_251()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 251, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_252()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 252, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_253()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 253, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_254()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 254, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_255()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 255, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_256()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 256, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_257()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 257, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_258()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 258, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_259()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 259, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_260()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 260, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_261()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 261, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_262()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 262, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_263()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 263, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_264()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 264, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_265()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 265, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_266()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 266, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_267()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 267, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_268()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 268, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_269()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 269, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_270()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 270, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_271()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 271, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_272()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 272, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_273()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 273, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_274()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 274, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_275()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 275, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_276()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 276, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_277()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 277, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_278()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 278, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_279()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 279, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_280()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 280, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_281()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 281, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_282()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 282, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_283()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 283, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_284()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 284, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_285()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 285, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_286()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 286, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_287()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 287, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_288()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 288, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_289()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 289, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_290()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 290, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_291()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 291, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_292()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 292, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_293()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 293, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_294()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 294, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_295()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 295, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_296()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 296, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_297()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 297, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_298()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 298, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }

    [Fact]
    public void Raft_election_safety_seed_299()
    {
        var options = new WorkloadOptions { NodeCount = 5, ElectionTimeoutMin = 20, ElectionTimeoutMax = 40, HeartbeatInterval = 10 };
        var (runtime, invariants) = ScenarioHost.Build("raft", 299, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        runtime.CheckAll(invariants).Should().BeEmpty();
    }
}
