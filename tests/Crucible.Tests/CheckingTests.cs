
using Crucible.Abstractions;
using Crucible.Checking;
using FluentAssertions;

namespace Crucible.Tests;

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
        checker.Check(history.Snapshot()).IsLinearizable.Should().BeTrue();
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
        checker.Check(history.Snapshot()).IsLinearizable.Should().BeFalse();
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
        new LinearizabilityChecker<List<int>>(new QueueSpec()).Check(history.Snapshot()).IsLinearizable.Should().BeTrue();
    }
}
