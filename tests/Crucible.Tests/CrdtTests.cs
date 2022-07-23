namespace Crucible.Tests;

using Crucible.Abstractions;
using Crucible.Faults;
using Crucible.Protocols;
using Crucible.Protocols.Crdt;
using Crucible.Runtime;
using Xunit;

public class CrdtTests
{
    [Theory]
    [InlineData("x", "y")]
    [InlineData("apple", "banana")]
    [InlineData("k1", "k2")]
    public void OrSet_basic_merge(string val1, string val2)
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), val1);
        b.ApplyAdd(new OrSetTag("n1", 1), val2);
        a.Merge(b);

        var read = a.Read();
        Assert.Contains(val1, read);
        Assert.Contains(val2, read);
        Assert.Equal(2, read.Count);
    }

    [Fact]
    public void OrSet_observed_remove_semantics()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();

        var tagA = new OrSetTag("n0", 1);
        a.ApplyAdd(tagA, "item");

        // Sync a to b
        b.Merge(a);

        // a removes item (tagA is observed)
        a.ApplyRemove(tagA, "item");

        // b concurrently adds item with new tagB
        var tagB = new OrSetTag("n1", 2);
        b.ApplyAdd(tagB, "item");

        // Merge a and b: item must be present because tagB was not observed by the remove!
        a.Merge(b);
        b.Merge(a);

        Assert.Contains("item", a.Read());
        Assert.Contains("item", b.Read());
    }

    [Fact]
    public void OrSet_merge_is_commutative_and_idempotent()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();

        a.ApplyAdd(new OrSetTag("n0", 1), "alpha");
        b.ApplyAdd(new OrSetTag("n1", 1), "beta");
        b.ApplyAdd(new OrSetTag("n1", 2), "gamma");

        var doc1 = new OrSetDocument();
        doc1.Merge(a);
        doc1.Merge(b);

        var doc2 = new OrSetDocument();
        doc2.Merge(b);
        doc2.Merge(a);

        // Commutativity: a + b == b + a
        Assert.True(doc1.Read().SetEquals(doc2.Read()));

        // Idempotence: a + a == a
        doc1.Merge(a);
        Assert.True(doc1.Read().SetEquals(doc2.Read()));
    }

    [Fact]
    public void LwwRegister_resolves_concurrent_writes_by_timestamp_and_tiebreaker()
    {
        var reg = new LwwRegister();

        // Later timestamp wins
        reg.Merge(new LwwValue(10, "n0", "first"));
        reg.Merge(new LwwValue(20, "n1", "second"));
        Assert.Equal("second", reg.Read());

        // Same timestamp: tie-break by node ID lexicographically
        reg.Merge(new LwwValue(20, "n2", "third"));
        Assert.Equal("third", reg.Read()); // "n2" > "n1"
    }

    [Fact]
    public void Rga_reconstructs_text_deterministically()
    {
        var docA = new RgaDocument();
        var docB = new RgaDocument();

        var op1 = new RgaInsert(new RgaId(0, 1), null, 'H');
        var op2 = new RgaInsert(new RgaId(0, 2), new RgaId(0, 1), 'i');

        docA.Apply(op1);
        docA.Apply(op2);

        docB.Merge(docA);
        Assert.Equal("Hi", docB.Materialize());
    }

    [Fact]
    public void OrSet_cluster_simulation_converges_under_chaos()
    {
        var options = new WorkloadOptions { NodeCount = 5, ClientOpLimit = 5 };
        var (runtime, invariants) = ScenarioHost.Build("or-set", 42, options, chaos: ChaosProfile.Mild);
        runtime.Run(500);

        var violations = runtime.CheckAll(invariants);
        Assert.Empty(violations);
    }
}