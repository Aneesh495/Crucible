namespace Crucible.Tests;

using Crucible.Abstractions;
using Crucible.Checking;
using Xunit;

public class CheckingTests
{
    [Fact]
    public void Register_history_is_linearizable()
    {
        var history = new ExecutionHistory();
        var t0 = SimTime.Zero;
        var w1 = history.Begin(0, "write", 42, t0);
        history.Complete(w1, true, t0.Add(1));
        var r1 = history.Begin(1, "read", null, t0.Add(2));
        history.Complete(r1, 42, t0.Add(3));
        var checker = new LinearizabilityChecker<int?>(new RegisterSpec());
        Assert.True(checker.Check(history.Snapshot()).IsLinearizable);
    }

    [Fact]
    public void Register_violation_detected()
    {
        var history = new ExecutionHistory();
        var t0 = SimTime.Zero;
        var w1 = history.Begin(0, "write", 1, t0);
        history.Complete(w1, true, t0.Add(1));
        var r1 = history.Begin(1, "read", null, t0.Add(2));
        history.Complete(r1, 99, t0.Add(3));
        var checker = new LinearizabilityChecker<int?>(new RegisterSpec());
        Assert.False(checker.Check(history.Snapshot()).IsLinearizable);
    }

    [Fact]
    public void Queue_spec_accepts_fifo()
    {
        var history = new ExecutionHistory();
        var t = SimTime.Zero;
        var e1 = history.Begin(0, "enqueue", 1, t);
        history.Complete(e1, true, t.Add(1));
        var e2 = history.Begin(0, "enqueue", 2, t.Add(2));
        history.Complete(e2, true, t.Add(3));
        var d1 = history.Begin(1, "dequeue", null, t.Add(4));
        history.Complete(d1, 1, t.Add(5));
        var checker = new LinearizabilityChecker<List<int>>(new QueueSpec());
        Assert.True(checker.Check(history.Snapshot()).IsLinearizable);
    }

    [Fact]
    public void Stack_spec_accepts_lifo()
    {
        var history = new ExecutionHistory();
        var t = SimTime.Zero;
        var p1 = history.Begin(0, "push", 10L, t);
        history.Complete(p1, true, t.Add(1));
        var p2 = history.Begin(0, "push", 20L, t.Add(2));
        history.Complete(p2, true, t.Add(3));
        var pop1 = history.Begin(1, "pop", null, t.Add(4));
        history.Complete(pop1, 20L, t.Add(5));
        var pop2 = history.Begin(1, "pop", null, t.Add(6));
        history.Complete(pop2, 10L, t.Add(7));
        var checker = new LinearizabilityChecker<List<long>>(new StackSpec());
        Assert.True(checker.Check(history.Snapshot()).IsLinearizable);
    }

    [Fact]
    public void Stack_spec_rejects_fifo()
    {
        var history = new ExecutionHistory();
        var t = SimTime.Zero;
        var p1 = history.Begin(0, "push", 10L, t);
        history.Complete(p1, true, t.Add(1));
        var p2 = history.Begin(0, "push", 20L, t.Add(2));
        history.Complete(p2, true, t.Add(3));
        var pop1 = history.Begin(1, "pop", null, t.Add(4));
        // Returning 10 first instead of 20 violates LIFO:
        history.Complete(pop1, 10L, t.Add(5));
        var checker = new LinearizabilityChecker<List<long>>(new StackSpec());
        Assert.False(checker.Check(history.Snapshot()).IsLinearizable);
    }
}
