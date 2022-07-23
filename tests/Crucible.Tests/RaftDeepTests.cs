namespace Crucible.Tests;

using Crucible.Abstractions;
using Crucible.Faults;
using Crucible.Protocols;
using Crucible.Protocols.Raft;
using Crucible.Runtime;
using Xunit;

public class RaftDeepTests
{
    [Theory]
    [InlineData(100)]
    [InlineData(101)]
    [InlineData(102)]
    [InlineData(103)]
    [InlineData(104)]
    public void Raft_election_safety_under_chaos(int seed)
    {
        var options = new WorkloadOptions
        {
            NodeCount = 5,
            ElectionTimeoutMin = 20,
            ElectionTimeoutMax = 40,
            HeartbeatInterval = 10,
            ClientOpLimit = 5
        };
        var (runtime, invariants) = ScenarioHost.Build("raft", seed, options, chaos: ChaosProfile.Mild);
        runtime.Run(600);
        var violations = runtime.CheckAll(invariants);
        Assert.Empty(violations);
    }

    [Fact]
    public void Raft_duplicate_vote_rpc_does_not_fabricate_quorum()
    {
        var options = new WorkloadOptions { NodeCount = 5 };
        var rt = new DeterministicRuntime(42);
        var candidate = new RaftNode(new NodeId(0), 5, options);
        rt.Register(candidate);
        rt.StartAll();

        // Start election -> candidate votes for self (count = 1)
        rt.FireTimer(new NodeId(0), "election", 1);
        Assert.Equal(RaftRole.Candidate, candidate.Role);

        // Deliver ONE vote response from node 1
        var voteFrom1 = new MessageEnvelope(new NodeId(1), new NodeId(0), new VoteResponse(candidate.Persistent.CurrentTerm, true), SimTime.Zero);
        candidate.OnMessage(voteFrom1);
        // Still Candidate because votes = 2 (self + n1), quorum is 3
        Assert.Equal(RaftRole.Candidate, candidate.Role);

        // Malicious or duplicated vote response from node 1 arrives again:
        candidate.OnMessage(voteFrom1);
        candidate.OnMessage(voteFrom1);

        // Candidate MUST NOT become leader from duplicate votes! Quorum is unique voters.
        Assert.Equal(RaftRole.Candidate, candidate.Role);

        // Now deliver genuine vote from node 2 -> votes = 3 (self + n1 + n2) -> Quorum reached!
        var voteFrom2 = new MessageEnvelope(new NodeId(2), new NodeId(0), new VoteResponse(candidate.Persistent.CurrentTerm, true), SimTime.Zero);
        candidate.OnMessage(voteFrom2);
        Assert.Equal(RaftRole.Leader, candidate.Role);
    }

    [Fact]
    public void Raft_leader_does_not_ack_client_before_quorum_commit()
    {
        var options = new WorkloadOptions { NodeCount = 3 };
        var rt = new DeterministicRuntime(42);
        var n0 = new RaftNode(new NodeId(0), 3, options);
        var n1 = new RaftNode(new NodeId(1), 3, options);
        var n2 = new RaftNode(new NodeId(2), 3, options);
        rt.Register(n0);
        rt.Register(n1);
        rt.Register(n2);
        rt.StartAll();

        // Elect n0 as leader
        rt.FireTimer(new NodeId(0), "election", 1);
        var term = n0.Persistent.CurrentTerm;
        n0.OnMessage(new MessageEnvelope(new NodeId(1), new NodeId(0), new VoteResponse(term, true), SimTime.Zero));
        Assert.Equal(RaftRole.Leader, n0.Role);

        // Send client command to leader
        var clientReq = new ClientRequest("set x 100", 1, 1);
        n0.OnMessage(new MessageEnvelope(new NodeId(999), new NodeId(0), clientReq, SimTime.Zero));

        // Client response must NOT be sent before quorum commit!
        Assert.DoesNotContain(rt.Network.InFlight, m => m.To == new NodeId(999));

        // Follower n1 receives AppendEntries and responds with success
        var aeMsg = rt.Network.InFlight.LastOrDefault(m => m.To == new NodeId(1) && m.Payload is AppendEntriesRequest);
        Assert.NotNull(aeMsg);
        n1.OnMessage(aeMsg);

        // Deliver AE response from n1 to leader n0 (satisfying quorum: n0 + n1)
        var aeRespMsg = rt.Network.InFlight.LastOrDefault(m => m.To == new NodeId(0) && m.Payload is AppendEntriesResponse);
        Assert.NotNull(aeRespMsg);
        n0.OnMessage(aeRespMsg);

        // Now entry is committed and applied, client response should be sent!
        Assert.True(n0.Persistent.CommitIndex >= 1);
        Assert.Contains(rt.Network.InFlight, m => m.To == new NodeId(999) && m.Payload is ClientResponse resp && resp.Ok);
    }

    [Fact]
    public void Raft_follower_log_conflict_backtracks_nextIndex()
    {
        var options = new WorkloadOptions { NodeCount = 3 };
        var rt = new DeterministicRuntime(42);
        var leader = new RaftNode(new NodeId(0), 3, options);
        var follower = new RaftNode(new NodeId(1), 3, options);
        rt.Register(leader);
        rt.Register(follower);
        rt.StartAll();

        // Follower has conflicting uncommitted entry at index 1 with term 1
        follower.Persistent.CurrentTerm = 1;
        follower.Persistent.AppendEntry(new LogEntry(1, 1, "conflicting-cmd"));
        follower.Persistent.Persist(rt.Storage, new NodeId(1));

        // Leader is in term 2 and has entry at index 1 with term 2
        leader.Persistent.CurrentTerm = 2;
        leader.Persistent.AppendEntry(new LogEntry(1, 2, "authoritative-cmd"));
        leader.Persistent.AppendEntry(new LogEntry(2, 2, "second-cmd"));
        leader.Persistent.Persist(rt.Storage, new NodeId(0));

        // Make n0 become leader in term 2
        leader.OnMessage(new MessageEnvelope(new NodeId(1), new NodeId(0), new VoteResponse(2, true), SimTime.Zero));

        // Step simulation to deliver AppendEntries and handle response
        rt.Run(50);

        // Follower must have overwritten conflicting entry and matched leader
        Assert.Equal(leader.Persistent.LastLogIndex, follower.Persistent.LastLogIndex);
        Assert.Equal(leader.Persistent.LastLogTerm, follower.Persistent.LastLogTerm);
    }

    [Fact]
    public void Raft_snapshot_compacts_log_and_installs_on_slow_follower()
    {
        var options = new WorkloadOptions { NodeCount = 3 };
        var rt = new DeterministicRuntime(42);
        var leader = new RaftNode(new NodeId(0), 3, options);
        var follower = new RaftNode(new NodeId(1), 3, options);
        rt.Register(leader);
        rt.Register(follower);
        rt.StartAll();

        leader.Persistent.CurrentTerm = 3;
        leader.Persistent.AppendEntry(new LogEntry(1, 1, "k1=v1"));
        leader.Persistent.AppendEntry(new LogEntry(2, 2, "k2=v2"));
        leader.Persistent.CommitIndex = 2;
        leader.Persistent.LastApplied = 2;

        // Leader creates snapshot up to index 2
        var snapData = new Dictionary<string, string> { ["k1"] = "v1", ["k2"] = "v2" };
        leader.TakeSnapshot(2, snapData);

        Assert.Equal(2, leader.Persistent.LastIncludedIndex);
        Assert.Empty(leader.Persistent.Log); // Log compacted

        // Install snapshot onto follower
        var snapMsg = new InstallSnapshot(3, 0, 2, 2, snapData);
        follower.OnMessage(new MessageEnvelope(new NodeId(0), new NodeId(1), snapMsg, SimTime.Zero));

        Assert.Equal(2, follower.Persistent.LastIncludedIndex);
        Assert.Equal(2, follower.Persistent.CommitIndex);
        Assert.Equal(2, follower.Persistent.LastApplied);
        Assert.Equal("v1", follower.StateMachine["k1"]);
        Assert.Equal("v2", follower.StateMachine["k2"]);
    }
}
