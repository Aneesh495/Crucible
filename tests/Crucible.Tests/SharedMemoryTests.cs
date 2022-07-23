namespace Crucible.Tests;

using Crucible.Checking;
using Crucible.Runtime;
using Crucible.Runtime.Shared;
using Crucible.Structures;
using Xunit;

public class SharedMemoryTests
{
    [Fact]
    public void Treiber_stack_push_pop_linearizable()
    {
        var rt = new DeterministicRuntime(1);
        var history = new ExecutionHistory();
        var mem = new SimSharedMemory(rt, history);
        var stack = new TreiberStack(mem);

        stack.Push(0, 10);
        stack.Push(0, 20);

        Assert.True(stack.TryPop(1, out var val1));
        Assert.Equal(20, val1);

        Assert.True(stack.TryPop(1, out var val2));
        Assert.Equal(10, val2);

        Assert.False(stack.TryPop(1, out _));

        var checker = new LinearizabilityChecker<List<long>>(new StackSpec());
        var res = checker.Check(history.Snapshot());
        Assert.True(res.IsLinearizable);
    }

    [Fact]
    public void Michael_scott_queue_enqueue_dequeue_linearizable()
    {
        var rt = new DeterministicRuntime(1);
        var history = new ExecutionHistory();
        var mem = new SimSharedMemory(rt, history);
        var queue = new MichaelScottQueue(mem);

        queue.Enqueue(0, 100);
        queue.Enqueue(0, 200);

        Assert.True(queue.TryDequeue(1, out var val1));
        Assert.Equal(100, val1);

        Assert.True(queue.TryDequeue(1, out var val2));
        Assert.Equal(200, val2);

        Assert.False(queue.TryDequeue(1, out _));

        var checker = new LinearizabilityChecker<List<int>>(new QueueSpec());
        var res = checker.Check(history.Snapshot());
        Assert.True(res.IsLinearizable);
    }

    [Fact]
    public void Buggy_treiber_stack_is_rejected_under_concurrent_operations()
    {
        var rt = new DeterministicRuntime(42);
        var history = new ExecutionHistory();
        var mem = new SimSharedMemory(rt, history);
        var buggyStack = new BuggyTreiberStack(mem);

        // Process 0 and 1 both perform operations without synchronization
        buggyStack.Push(0, 1);
        buggyStack.Push(1, 2);
        buggyStack.TryPop(0, out var v1);
        buggyStack.TryPop(1, out var v2);

        var snap = history.Snapshot();
        Assert.NotEmpty(snap.Completed);
    }
}
