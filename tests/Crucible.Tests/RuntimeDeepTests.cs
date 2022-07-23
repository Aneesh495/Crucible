namespace Crucible.Tests;

using Crucible.Runtime;
using Xunit;

public class RuntimeDeepTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(42)]
    [InlineData(100)]
    [InlineData(999)]
    public void Rng_fork_differs_from_base(int seed)
    {
        var a = new SeededRng(seed);
        var b = new SeededRng(seed).Fork($"label-{seed}");
        Assert.NotEqual(a.Next(), b.Next());
    }

    [Fact]
    public void Rng_fork_property_across_broad_seed_range()
    {
        for (var seed = 0; seed < 50; seed++)
        {
            var a = new SeededRng(seed);
            var b = new SeededRng(seed).Fork($"label-{seed}");
            Assert.NotEqual(a.Next(), b.Next());
        }
    }

    [Fact]
    public void Rng_fork_deterministic_for_identical_label()
    {
        var a1 = new SeededRng(42).Fork("network");
        var a2 = new SeededRng(42).Fork("network");
        for (var i = 0; i < 20; i++)
            Assert.Equal(a1.Next(), a2.Next());
    }

    [Fact]
    public void Rng_independent_streams_dont_interfere()
    {
        var baseRng = new SeededRng(42);
        var s1 = baseRng.Fork("stream1");
        var s2 = baseRng.Fork("stream2");

        var v1 = s1.Next();
        var v2 = s2.Next();
        Assert.NotEqual(v1, v2);
    }
}
