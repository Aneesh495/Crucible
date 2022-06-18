
using Crucible.Checking;
using Crucible.Runtime;
using Crucible.Runtime.Shared;
using Crucible.Structures;
using FluentAssertions;

namespace Crucible.Tests;

public class SharedMemoryTests
{
    [Fact]
    public void Treiber_stack_records_history()
    {
        var rt = new DeterministicRuntime(1);
        var history = new ExecutionHistory();
        var mem = new SimSharedMemory(rt, history);
        var stack = new TreiberStack(mem);
        stack.Push(0, 10);
        stack.TryPop(1, out _).Should().BeTrue();
        history.Invocations.Should().NotBeEmpty();
    }
}
