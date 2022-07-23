namespace Crucible.Tests;

using Crucible.Abstractions;
using Crucible.Concurrency;
using Crucible.Runtime;
using Xunit;

// --- Messages for LostUpdate Workload ---
public sealed record ReadBalanceReq : IMessage { public string TypeName => nameof(ReadBalanceReq); }
public sealed record ReadBalanceResp(int Balance) : IMessage { public string TypeName => nameof(ReadBalanceResp); }
public sealed record WriteBalanceReq(int NewBalance) : IMessage { public string TypeName => nameof(WriteBalanceReq); }
public sealed record TxDone : IMessage { public string TypeName => nameof(TxDone); }

// --- Bank Coordinator Node (holds shared state) ---
public sealed class BankCoordinatorNode : ISimProcess
{
    private ISimContext? _ctx;
    public NodeId Id { get; } = new(0);
    public ProcessState State { get; private set; } = ProcessState.Stopped;
    public int Balance { get; private set; } = 0;
    public int CompletedTransactions { get; private set; } = 0;

    public void Start(ISimContext context) { _ctx = context; State = ProcessState.Running; }
    public void OnCrash() => State = ProcessState.Crashed;
    public void OnRestart(ISimContext context) { _ctx = context; State = ProcessState.Running; }
    public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();

    public void OnMessage(MessageEnvelope envelope)
    {
        if (_ctx is null) return;
        switch (envelope.Payload)
        {
            case ReadBalanceReq:
                _ctx.Network.Send(Id, envelope.From, new ReadBalanceResp(Balance));
                break;
            case WriteBalanceReq w:
                Balance = w.NewBalance;
                CompletedTransactions++;
                _ctx.Network.Send(Id, envelope.From, new TxDone());
                break;
        }
    }

    public void OnTimer(string name, long generation) { }
}

// --- Client Transactor Node (performs read-modify-write) ---
public sealed class TransactorNode : ISimProcess
{
    private ISimContext? _ctx;
    private int _localRead;

    public TransactorNode(NodeId id) => Id = id;
    public NodeId Id { get; }
    public ProcessState State { get; private set; } = ProcessState.Stopped;
    public bool Finished { get; private set; }

    public void Start(ISimContext context) { _ctx = context; State = ProcessState.Running; }
    public void OnCrash() => State = ProcessState.Crashed;
    public void OnRestart(ISimContext context) { _ctx = context; State = ProcessState.Running; }
    public IEnumerable<InvariantViolation> CheckLocalInvariants() => Array.Empty<InvariantViolation>();

    public void InitiateTx()
    {
        _ctx?.Network.Send(Id, new NodeId(0), new ReadBalanceReq());
    }

    public void OnMessage(MessageEnvelope envelope)
    {
        if (_ctx is null) return;
        switch (envelope.Payload)
        {
            case ReadBalanceResp r:
                _localRead = r.Balance;
                // Yield scheduling point between read and write:
                _ctx.Yield("tx-gap");
                _ctx.Network.Send(Id, new NodeId(0), new WriteBalanceReq(_localRead + 10));
                break;
            case TxDone:
                Finished = true;
                break;
        }
    }

    public void OnTimer(string name, long generation) { }
}

// --- Safety Invariant: No Lost Updates ---
public sealed class LostUpdateInvariant : IInvariant
{
    public string Name => "lost-update-safety";

    public IEnumerable<InvariantViolation> Check(IClusterView cluster)
    {
        var bank = cluster.AllProcesses.OfType<BankCoordinatorNode>().FirstOrDefault();
        if (bank is null) yield break;

        // If both transactions completed, the balance must reflect both increments (+10 each = 20)
        if (bank.CompletedTransactions >= 2 && bank.Balance < 20)
        {
            yield return new InvariantViolation
            {
                Name = Name,
                Detail = $"Lost update detected! Final balance={bank.Balance}, expected=20 (completed tx={bank.CompletedTransactions})",
                Severity = ViolationSeverity.Fatal,
                At = cluster.Now
            };
        }
    }
}

// --- LostUpdate Workload ---
public sealed class LostUpdateWorkload : IWorkload
{
    public string Name => "lost-update";

    public IReadOnlyList<ISimProcess> CreateProcesses(int nodeCount, WorkloadOptions options)
    {
        return new ISimProcess[]
        {
            new BankCoordinatorNode(),
            new TransactorNode(new NodeId(1)),
            new TransactorNode(new NodeId(2))
        };
    }

    public IReadOnlyList<IInvariant> GlobalInvariants => new IInvariant[] { new LostUpdateInvariant() };

    public void DriveClient(ISimContext clientContext, IReadOnlyList<NodeId> nodes, int step)
    {
        // Step 0 triggers Transactor 1; Step 1 triggers Transactor 2
        if (step == 0)
            clientContext.Network.Send(new NodeId(999), new NodeId(1), new ReadBalanceReq());
        else if (step == 1)
            clientContext.Network.Send(new NodeId(999), new NodeId(2), new ReadBalanceReq());
    }
}

public class ConcurrencyBugTests
{
    private static void Configure(DeterministicRuntime runtime, WorkloadOptions options)
    {
        var workload = new LostUpdateWorkload();
        foreach (var p in workload.CreateProcesses(3, options))
            runtime.Register(p);

        // When transactor receives start trigger from client, initiate transaction
        runtime.SetClientDriver((ctx, nodes, step) =>
        {
            if (step == 0)
            {
                var t1 = runtime.GetProcess<TransactorNode>(new NodeId(1));
                t1?.InitiateTx();
            }
            else if (step == 1)
            {
                var t2 = runtime.GetProcess<TransactorNode>(new NodeId(2));
                t2?.InitiateTx();
            }
        }, opLimit: 2, opInterval: 1);
    }

    [Fact]
    public void Ordinary_execution_can_miss_the_concurrency_bug()
    {
        // Under default sequential execution where tx 1 completes before tx 2,
        // both increments succeed: 0 -> 10 -> 20.
        var runtime = new DeterministicRuntime(42);
        Configure(runtime, new WorkloadOptions());
        var inv = new LostUpdateWorkload().GlobalInvariants;
        runtime.Run(100);

        var violations = runtime.CheckAll(inv);
        Assert.Empty(violations);

        var bank = runtime.AllProcesses.OfType<BankCoordinatorNode>().Single();
        Assert.Equal(20, bank.Balance);
    }

    [Fact]
    public void Exploration_detects_lost_update_and_replays_exactly()
    {
        var workload = new LostUpdateWorkload();
        var options = new ExplorerOptions { MaxSteps = 50, MaxSchedules = 20 };
        var dfs = new DfsExplorer(options);

        // DFS explores interleavings and finds the lost update schedule
        var results = dfs.Explore(
            workload.Name,
            seed: 1,
            Configure,
            new WorkloadOptions(),
            workload.GlobalInvariants);

        var failingResult = results.FirstOrDefault(r => !r.Success);
        Assert.NotNull(failingResult);
        Assert.NotNull(failingResult.Trace);
        Assert.NotEmpty(failingResult.Violations);
        Assert.Contains(failingResult.Violations, v => v.Name == "lost-update-safety");

        // Serialize the counterexample trace to JSON and back
        var json = ScheduleSerializer.ToJson(failingResult.Trace);
        var reloadedTrace = ScheduleSerializer.FromJson(json);

        // Exact replay reproduces the same violation at the exact same step!
        var replayer = new ReplayExplorer();
        var replayResult = replayer.Replay(
            reloadedTrace,
            Configure,
            new WorkloadOptions(),
            workload.GlobalInvariants);

        Assert.False(replayResult.Success);
        Assert.Equal(failingResult.Steps, replayResult.Steps);
        Assert.Contains(replayResult.Violations, v => v.Name == "lost-update-safety");
    }

    [Fact]
    public void Different_seeds_explore_deterministically()
    {
        var workload = new LostUpdateWorkload();
        var options = new ExplorerOptions { MaxSteps = 50, MaxSchedules = 5 };

        var r1 = new DfsExplorer(options).Explore(workload.Name, 10, Configure, new WorkloadOptions(), workload.GlobalInvariants);
        var r2 = new DfsExplorer(options).Explore(workload.Name, 10, Configure, new WorkloadOptions(), workload.GlobalInvariants);
        var r3 = new DfsExplorer(options).Explore(workload.Name, 99, Configure, new WorkloadOptions(), workload.GlobalInvariants);

        // Identical seed reproduces identical schedule count and step results
        Assert.Equal(r1.Count, r2.Count);
        for (var i = 0; i < r1.Count; i++)
        {
            Assert.Equal(r1[i].Steps, r2[i].Steps);
            Assert.Equal(r1[i].Success, r2[i].Success);
        }
    }
}
