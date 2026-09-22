using Crucible.Protocols.Crdt;
using Crucible.Abstractions;
using Crucible.Runtime;
using FluentAssertions;

namespace Crucible.Tests;

public class CrdtTests
{

    [Fact]
    public void OrSet_merge_seed_1()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_2()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_3()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_4()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_5()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_6()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_7()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_8()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_9()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_10()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_11()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_12()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_13()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_14()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_15()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_16()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_17()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_18()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_19()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_20()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_21()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_22()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_23()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_24()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_25()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_26()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_27()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_28()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_29()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_30()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_31()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_32()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_33()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_34()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_35()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_36()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_37()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_38()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_39()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_40()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_41()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_42()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_43()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_44()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_45()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_46()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_47()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_48()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_49()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_50()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_51()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_52()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_53()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_54()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_55()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_56()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_57()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_58()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_59()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_60()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_61()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_62()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_63()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_64()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_65()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_66()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_67()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_68()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_69()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_70()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_71()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_72()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_73()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_74()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_75()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_76()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_77()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_78()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }

    [Fact]
    public void OrSet_merge_seed_79()
    {
        var a = new OrSetDocument();
        var b = new OrSetDocument();
        a.ApplyAdd(new OrSetTag("n0", 1), "x");
        b.ApplyAdd(new OrSetTag("n1", 1), "y");
        a.Merge(b);
        a.Read().Should().BeEquivalentTo(new[] { "x", "y" });
    }
}