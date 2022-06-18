using Crucible.Runtime;
using FluentAssertions;
namespace Crucible.Tests;
public class RuntimeDeepTests
{

    [Fact]
    public void Rng_fork_0_differs_from_base()
    {
        var a = new SeededRng(0);
        var b = new SeededRng(0).Fork("label-0");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_1_differs_from_base()
    {
        var a = new SeededRng(1);
        var b = new SeededRng(1).Fork("label-1");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_2_differs_from_base()
    {
        var a = new SeededRng(2);
        var b = new SeededRng(2).Fork("label-2");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_3_differs_from_base()
    {
        var a = new SeededRng(3);
        var b = new SeededRng(3).Fork("label-3");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_4_differs_from_base()
    {
        var a = new SeededRng(4);
        var b = new SeededRng(4).Fork("label-4");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_5_differs_from_base()
    {
        var a = new SeededRng(5);
        var b = new SeededRng(5).Fork("label-5");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_6_differs_from_base()
    {
        var a = new SeededRng(6);
        var b = new SeededRng(6).Fork("label-6");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_7_differs_from_base()
    {
        var a = new SeededRng(7);
        var b = new SeededRng(7).Fork("label-7");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_8_differs_from_base()
    {
        var a = new SeededRng(8);
        var b = new SeededRng(8).Fork("label-8");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_9_differs_from_base()
    {
        var a = new SeededRng(9);
        var b = new SeededRng(9).Fork("label-9");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_10_differs_from_base()
    {
        var a = new SeededRng(10);
        var b = new SeededRng(10).Fork("label-10");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_11_differs_from_base()
    {
        var a = new SeededRng(11);
        var b = new SeededRng(11).Fork("label-11");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_12_differs_from_base()
    {
        var a = new SeededRng(12);
        var b = new SeededRng(12).Fork("label-12");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_13_differs_from_base()
    {
        var a = new SeededRng(13);
        var b = new SeededRng(13).Fork("label-13");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_14_differs_from_base()
    {
        var a = new SeededRng(14);
        var b = new SeededRng(14).Fork("label-14");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_15_differs_from_base()
    {
        var a = new SeededRng(15);
        var b = new SeededRng(15).Fork("label-15");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_16_differs_from_base()
    {
        var a = new SeededRng(16);
        var b = new SeededRng(16).Fork("label-16");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_17_differs_from_base()
    {
        var a = new SeededRng(17);
        var b = new SeededRng(17).Fork("label-17");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_18_differs_from_base()
    {
        var a = new SeededRng(18);
        var b = new SeededRng(18).Fork("label-18");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_19_differs_from_base()
    {
        var a = new SeededRng(19);
        var b = new SeededRng(19).Fork("label-19");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_20_differs_from_base()
    {
        var a = new SeededRng(20);
        var b = new SeededRng(20).Fork("label-20");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_21_differs_from_base()
    {
        var a = new SeededRng(21);
        var b = new SeededRng(21).Fork("label-21");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_22_differs_from_base()
    {
        var a = new SeededRng(22);
        var b = new SeededRng(22).Fork("label-22");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_23_differs_from_base()
    {
        var a = new SeededRng(23);
        var b = new SeededRng(23).Fork("label-23");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_24_differs_from_base()
    {
        var a = new SeededRng(24);
        var b = new SeededRng(24).Fork("label-24");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_25_differs_from_base()
    {
        var a = new SeededRng(25);
        var b = new SeededRng(25).Fork("label-25");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_26_differs_from_base()
    {
        var a = new SeededRng(26);
        var b = new SeededRng(26).Fork("label-26");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_27_differs_from_base()
    {
        var a = new SeededRng(27);
        var b = new SeededRng(27).Fork("label-27");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_28_differs_from_base()
    {
        var a = new SeededRng(28);
        var b = new SeededRng(28).Fork("label-28");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_29_differs_from_base()
    {
        var a = new SeededRng(29);
        var b = new SeededRng(29).Fork("label-29");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_30_differs_from_base()
    {
        var a = new SeededRng(30);
        var b = new SeededRng(30).Fork("label-30");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_31_differs_from_base()
    {
        var a = new SeededRng(31);
        var b = new SeededRng(31).Fork("label-31");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_32_differs_from_base()
    {
        var a = new SeededRng(32);
        var b = new SeededRng(32).Fork("label-32");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_33_differs_from_base()
    {
        var a = new SeededRng(33);
        var b = new SeededRng(33).Fork("label-33");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_34_differs_from_base()
    {
        var a = new SeededRng(34);
        var b = new SeededRng(34).Fork("label-34");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_35_differs_from_base()
    {
        var a = new SeededRng(35);
        var b = new SeededRng(35).Fork("label-35");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_36_differs_from_base()
    {
        var a = new SeededRng(36);
        var b = new SeededRng(36).Fork("label-36");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_37_differs_from_base()
    {
        var a = new SeededRng(37);
        var b = new SeededRng(37).Fork("label-37");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_38_differs_from_base()
    {
        var a = new SeededRng(38);
        var b = new SeededRng(38).Fork("label-38");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_39_differs_from_base()
    {
        var a = new SeededRng(39);
        var b = new SeededRng(39).Fork("label-39");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_40_differs_from_base()
    {
        var a = new SeededRng(40);
        var b = new SeededRng(40).Fork("label-40");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_41_differs_from_base()
    {
        var a = new SeededRng(41);
        var b = new SeededRng(41).Fork("label-41");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_42_differs_from_base()
    {
        var a = new SeededRng(42);
        var b = new SeededRng(42).Fork("label-42");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_43_differs_from_base()
    {
        var a = new SeededRng(43);
        var b = new SeededRng(43).Fork("label-43");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_44_differs_from_base()
    {
        var a = new SeededRng(44);
        var b = new SeededRng(44).Fork("label-44");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_45_differs_from_base()
    {
        var a = new SeededRng(45);
        var b = new SeededRng(45).Fork("label-45");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_46_differs_from_base()
    {
        var a = new SeededRng(46);
        var b = new SeededRng(46).Fork("label-46");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_47_differs_from_base()
    {
        var a = new SeededRng(47);
        var b = new SeededRng(47).Fork("label-47");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_48_differs_from_base()
    {
        var a = new SeededRng(48);
        var b = new SeededRng(48).Fork("label-48");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_49_differs_from_base()
    {
        var a = new SeededRng(49);
        var b = new SeededRng(49).Fork("label-49");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_50_differs_from_base()
    {
        var a = new SeededRng(50);
        var b = new SeededRng(50).Fork("label-50");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_51_differs_from_base()
    {
        var a = new SeededRng(51);
        var b = new SeededRng(51).Fork("label-51");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_52_differs_from_base()
    {
        var a = new SeededRng(52);
        var b = new SeededRng(52).Fork("label-52");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_53_differs_from_base()
    {
        var a = new SeededRng(53);
        var b = new SeededRng(53).Fork("label-53");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_54_differs_from_base()
    {
        var a = new SeededRng(54);
        var b = new SeededRng(54).Fork("label-54");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_55_differs_from_base()
    {
        var a = new SeededRng(55);
        var b = new SeededRng(55).Fork("label-55");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_56_differs_from_base()
    {
        var a = new SeededRng(56);
        var b = new SeededRng(56).Fork("label-56");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_57_differs_from_base()
    {
        var a = new SeededRng(57);
        var b = new SeededRng(57).Fork("label-57");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_58_differs_from_base()
    {
        var a = new SeededRng(58);
        var b = new SeededRng(58).Fork("label-58");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_59_differs_from_base()
    {
        var a = new SeededRng(59);
        var b = new SeededRng(59).Fork("label-59");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_60_differs_from_base()
    {
        var a = new SeededRng(60);
        var b = new SeededRng(60).Fork("label-60");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_61_differs_from_base()
    {
        var a = new SeededRng(61);
        var b = new SeededRng(61).Fork("label-61");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_62_differs_from_base()
    {
        var a = new SeededRng(62);
        var b = new SeededRng(62).Fork("label-62");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_63_differs_from_base()
    {
        var a = new SeededRng(63);
        var b = new SeededRng(63).Fork("label-63");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_64_differs_from_base()
    {
        var a = new SeededRng(64);
        var b = new SeededRng(64).Fork("label-64");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_65_differs_from_base()
    {
        var a = new SeededRng(65);
        var b = new SeededRng(65).Fork("label-65");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_66_differs_from_base()
    {
        var a = new SeededRng(66);
        var b = new SeededRng(66).Fork("label-66");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_67_differs_from_base()
    {
        var a = new SeededRng(67);
        var b = new SeededRng(67).Fork("label-67");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_68_differs_from_base()
    {
        var a = new SeededRng(68);
        var b = new SeededRng(68).Fork("label-68");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_69_differs_from_base()
    {
        var a = new SeededRng(69);
        var b = new SeededRng(69).Fork("label-69");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_70_differs_from_base()
    {
        var a = new SeededRng(70);
        var b = new SeededRng(70).Fork("label-70");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_71_differs_from_base()
    {
        var a = new SeededRng(71);
        var b = new SeededRng(71).Fork("label-71");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_72_differs_from_base()
    {
        var a = new SeededRng(72);
        var b = new SeededRng(72).Fork("label-72");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_73_differs_from_base()
    {
        var a = new SeededRng(73);
        var b = new SeededRng(73).Fork("label-73");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_74_differs_from_base()
    {
        var a = new SeededRng(74);
        var b = new SeededRng(74).Fork("label-74");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_75_differs_from_base()
    {
        var a = new SeededRng(75);
        var b = new SeededRng(75).Fork("label-75");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_76_differs_from_base()
    {
        var a = new SeededRng(76);
        var b = new SeededRng(76).Fork("label-76");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_77_differs_from_base()
    {
        var a = new SeededRng(77);
        var b = new SeededRng(77).Fork("label-77");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_78_differs_from_base()
    {
        var a = new SeededRng(78);
        var b = new SeededRng(78).Fork("label-78");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_79_differs_from_base()
    {
        var a = new SeededRng(79);
        var b = new SeededRng(79).Fork("label-79");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_80_differs_from_base()
    {
        var a = new SeededRng(80);
        var b = new SeededRng(80).Fork("label-80");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_81_differs_from_base()
    {
        var a = new SeededRng(81);
        var b = new SeededRng(81).Fork("label-81");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_82_differs_from_base()
    {
        var a = new SeededRng(82);
        var b = new SeededRng(82).Fork("label-82");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_83_differs_from_base()
    {
        var a = new SeededRng(83);
        var b = new SeededRng(83).Fork("label-83");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_84_differs_from_base()
    {
        var a = new SeededRng(84);
        var b = new SeededRng(84).Fork("label-84");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_85_differs_from_base()
    {
        var a = new SeededRng(85);
        var b = new SeededRng(85).Fork("label-85");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_86_differs_from_base()
    {
        var a = new SeededRng(86);
        var b = new SeededRng(86).Fork("label-86");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_87_differs_from_base()
    {
        var a = new SeededRng(87);
        var b = new SeededRng(87).Fork("label-87");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_88_differs_from_base()
    {
        var a = new SeededRng(88);
        var b = new SeededRng(88).Fork("label-88");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_89_differs_from_base()
    {
        var a = new SeededRng(89);
        var b = new SeededRng(89).Fork("label-89");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_90_differs_from_base()
    {
        var a = new SeededRng(90);
        var b = new SeededRng(90).Fork("label-90");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_91_differs_from_base()
    {
        var a = new SeededRng(91);
        var b = new SeededRng(91).Fork("label-91");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_92_differs_from_base()
    {
        var a = new SeededRng(92);
        var b = new SeededRng(92).Fork("label-92");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_93_differs_from_base()
    {
        var a = new SeededRng(93);
        var b = new SeededRng(93).Fork("label-93");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_94_differs_from_base()
    {
        var a = new SeededRng(94);
        var b = new SeededRng(94).Fork("label-94");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_95_differs_from_base()
    {
        var a = new SeededRng(95);
        var b = new SeededRng(95).Fork("label-95");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_96_differs_from_base()
    {
        var a = new SeededRng(96);
        var b = new SeededRng(96).Fork("label-96");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_97_differs_from_base()
    {
        var a = new SeededRng(97);
        var b = new SeededRng(97).Fork("label-97");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_98_differs_from_base()
    {
        var a = new SeededRng(98);
        var b = new SeededRng(98).Fork("label-98");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_99_differs_from_base()
    {
        var a = new SeededRng(99);
        var b = new SeededRng(99).Fork("label-99");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_100_differs_from_base()
    {
        var a = new SeededRng(100);
        var b = new SeededRng(100).Fork("label-100");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_101_differs_from_base()
    {
        var a = new SeededRng(101);
        var b = new SeededRng(101).Fork("label-101");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_102_differs_from_base()
    {
        var a = new SeededRng(102);
        var b = new SeededRng(102).Fork("label-102");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_103_differs_from_base()
    {
        var a = new SeededRng(103);
        var b = new SeededRng(103).Fork("label-103");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_104_differs_from_base()
    {
        var a = new SeededRng(104);
        var b = new SeededRng(104).Fork("label-104");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_105_differs_from_base()
    {
        var a = new SeededRng(105);
        var b = new SeededRng(105).Fork("label-105");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_106_differs_from_base()
    {
        var a = new SeededRng(106);
        var b = new SeededRng(106).Fork("label-106");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_107_differs_from_base()
    {
        var a = new SeededRng(107);
        var b = new SeededRng(107).Fork("label-107");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_108_differs_from_base()
    {
        var a = new SeededRng(108);
        var b = new SeededRng(108).Fork("label-108");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_109_differs_from_base()
    {
        var a = new SeededRng(109);
        var b = new SeededRng(109).Fork("label-109");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_110_differs_from_base()
    {
        var a = new SeededRng(110);
        var b = new SeededRng(110).Fork("label-110");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_111_differs_from_base()
    {
        var a = new SeededRng(111);
        var b = new SeededRng(111).Fork("label-111");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_112_differs_from_base()
    {
        var a = new SeededRng(112);
        var b = new SeededRng(112).Fork("label-112");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_113_differs_from_base()
    {
        var a = new SeededRng(113);
        var b = new SeededRng(113).Fork("label-113");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_114_differs_from_base()
    {
        var a = new SeededRng(114);
        var b = new SeededRng(114).Fork("label-114");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_115_differs_from_base()
    {
        var a = new SeededRng(115);
        var b = new SeededRng(115).Fork("label-115");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_116_differs_from_base()
    {
        var a = new SeededRng(116);
        var b = new SeededRng(116).Fork("label-116");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_117_differs_from_base()
    {
        var a = new SeededRng(117);
        var b = new SeededRng(117).Fork("label-117");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_118_differs_from_base()
    {
        var a = new SeededRng(118);
        var b = new SeededRng(118).Fork("label-118");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_119_differs_from_base()
    {
        var a = new SeededRng(119);
        var b = new SeededRng(119).Fork("label-119");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_120_differs_from_base()
    {
        var a = new SeededRng(120);
        var b = new SeededRng(120).Fork("label-120");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_121_differs_from_base()
    {
        var a = new SeededRng(121);
        var b = new SeededRng(121).Fork("label-121");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_122_differs_from_base()
    {
        var a = new SeededRng(122);
        var b = new SeededRng(122).Fork("label-122");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_123_differs_from_base()
    {
        var a = new SeededRng(123);
        var b = new SeededRng(123).Fork("label-123");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_124_differs_from_base()
    {
        var a = new SeededRng(124);
        var b = new SeededRng(124).Fork("label-124");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_125_differs_from_base()
    {
        var a = new SeededRng(125);
        var b = new SeededRng(125).Fork("label-125");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_126_differs_from_base()
    {
        var a = new SeededRng(126);
        var b = new SeededRng(126).Fork("label-126");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_127_differs_from_base()
    {
        var a = new SeededRng(127);
        var b = new SeededRng(127).Fork("label-127");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_128_differs_from_base()
    {
        var a = new SeededRng(128);
        var b = new SeededRng(128).Fork("label-128");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_129_differs_from_base()
    {
        var a = new SeededRng(129);
        var b = new SeededRng(129).Fork("label-129");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_130_differs_from_base()
    {
        var a = new SeededRng(130);
        var b = new SeededRng(130).Fork("label-130");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_131_differs_from_base()
    {
        var a = new SeededRng(131);
        var b = new SeededRng(131).Fork("label-131");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_132_differs_from_base()
    {
        var a = new SeededRng(132);
        var b = new SeededRng(132).Fork("label-132");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_133_differs_from_base()
    {
        var a = new SeededRng(133);
        var b = new SeededRng(133).Fork("label-133");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_134_differs_from_base()
    {
        var a = new SeededRng(134);
        var b = new SeededRng(134).Fork("label-134");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_135_differs_from_base()
    {
        var a = new SeededRng(135);
        var b = new SeededRng(135).Fork("label-135");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_136_differs_from_base()
    {
        var a = new SeededRng(136);
        var b = new SeededRng(136).Fork("label-136");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_137_differs_from_base()
    {
        var a = new SeededRng(137);
        var b = new SeededRng(137).Fork("label-137");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_138_differs_from_base()
    {
        var a = new SeededRng(138);
        var b = new SeededRng(138).Fork("label-138");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_139_differs_from_base()
    {
        var a = new SeededRng(139);
        var b = new SeededRng(139).Fork("label-139");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_140_differs_from_base()
    {
        var a = new SeededRng(140);
        var b = new SeededRng(140).Fork("label-140");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_141_differs_from_base()
    {
        var a = new SeededRng(141);
        var b = new SeededRng(141).Fork("label-141");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_142_differs_from_base()
    {
        var a = new SeededRng(142);
        var b = new SeededRng(142).Fork("label-142");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_143_differs_from_base()
    {
        var a = new SeededRng(143);
        var b = new SeededRng(143).Fork("label-143");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_144_differs_from_base()
    {
        var a = new SeededRng(144);
        var b = new SeededRng(144).Fork("label-144");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_145_differs_from_base()
    {
        var a = new SeededRng(145);
        var b = new SeededRng(145).Fork("label-145");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_146_differs_from_base()
    {
        var a = new SeededRng(146);
        var b = new SeededRng(146).Fork("label-146");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_147_differs_from_base()
    {
        var a = new SeededRng(147);
        var b = new SeededRng(147).Fork("label-147");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_148_differs_from_base()
    {
        var a = new SeededRng(148);
        var b = new SeededRng(148).Fork("label-148");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_149_differs_from_base()
    {
        var a = new SeededRng(149);
        var b = new SeededRng(149).Fork("label-149");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_150_differs_from_base()
    {
        var a = new SeededRng(150);
        var b = new SeededRng(150).Fork("label-150");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_151_differs_from_base()
    {
        var a = new SeededRng(151);
        var b = new SeededRng(151).Fork("label-151");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_152_differs_from_base()
    {
        var a = new SeededRng(152);
        var b = new SeededRng(152).Fork("label-152");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_153_differs_from_base()
    {
        var a = new SeededRng(153);
        var b = new SeededRng(153).Fork("label-153");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_154_differs_from_base()
    {
        var a = new SeededRng(154);
        var b = new SeededRng(154).Fork("label-154");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_155_differs_from_base()
    {
        var a = new SeededRng(155);
        var b = new SeededRng(155).Fork("label-155");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_156_differs_from_base()
    {
        var a = new SeededRng(156);
        var b = new SeededRng(156).Fork("label-156");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_157_differs_from_base()
    {
        var a = new SeededRng(157);
        var b = new SeededRng(157).Fork("label-157");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_158_differs_from_base()
    {
        var a = new SeededRng(158);
        var b = new SeededRng(158).Fork("label-158");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_159_differs_from_base()
    {
        var a = new SeededRng(159);
        var b = new SeededRng(159).Fork("label-159");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_160_differs_from_base()
    {
        var a = new SeededRng(160);
        var b = new SeededRng(160).Fork("label-160");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_161_differs_from_base()
    {
        var a = new SeededRng(161);
        var b = new SeededRng(161).Fork("label-161");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_162_differs_from_base()
    {
        var a = new SeededRng(162);
        var b = new SeededRng(162).Fork("label-162");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_163_differs_from_base()
    {
        var a = new SeededRng(163);
        var b = new SeededRng(163).Fork("label-163");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_164_differs_from_base()
    {
        var a = new SeededRng(164);
        var b = new SeededRng(164).Fork("label-164");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_165_differs_from_base()
    {
        var a = new SeededRng(165);
        var b = new SeededRng(165).Fork("label-165");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_166_differs_from_base()
    {
        var a = new SeededRng(166);
        var b = new SeededRng(166).Fork("label-166");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_167_differs_from_base()
    {
        var a = new SeededRng(167);
        var b = new SeededRng(167).Fork("label-167");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_168_differs_from_base()
    {
        var a = new SeededRng(168);
        var b = new SeededRng(168).Fork("label-168");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_169_differs_from_base()
    {
        var a = new SeededRng(169);
        var b = new SeededRng(169).Fork("label-169");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_170_differs_from_base()
    {
        var a = new SeededRng(170);
        var b = new SeededRng(170).Fork("label-170");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_171_differs_from_base()
    {
        var a = new SeededRng(171);
        var b = new SeededRng(171).Fork("label-171");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_172_differs_from_base()
    {
        var a = new SeededRng(172);
        var b = new SeededRng(172).Fork("label-172");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_173_differs_from_base()
    {
        var a = new SeededRng(173);
        var b = new SeededRng(173).Fork("label-173");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_174_differs_from_base()
    {
        var a = new SeededRng(174);
        var b = new SeededRng(174).Fork("label-174");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_175_differs_from_base()
    {
        var a = new SeededRng(175);
        var b = new SeededRng(175).Fork("label-175");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_176_differs_from_base()
    {
        var a = new SeededRng(176);
        var b = new SeededRng(176).Fork("label-176");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_177_differs_from_base()
    {
        var a = new SeededRng(177);
        var b = new SeededRng(177).Fork("label-177");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_178_differs_from_base()
    {
        var a = new SeededRng(178);
        var b = new SeededRng(178).Fork("label-178");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_179_differs_from_base()
    {
        var a = new SeededRng(179);
        var b = new SeededRng(179).Fork("label-179");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_180_differs_from_base()
    {
        var a = new SeededRng(180);
        var b = new SeededRng(180).Fork("label-180");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_181_differs_from_base()
    {
        var a = new SeededRng(181);
        var b = new SeededRng(181).Fork("label-181");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_182_differs_from_base()
    {
        var a = new SeededRng(182);
        var b = new SeededRng(182).Fork("label-182");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_183_differs_from_base()
    {
        var a = new SeededRng(183);
        var b = new SeededRng(183).Fork("label-183");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_184_differs_from_base()
    {
        var a = new SeededRng(184);
        var b = new SeededRng(184).Fork("label-184");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_185_differs_from_base()
    {
        var a = new SeededRng(185);
        var b = new SeededRng(185).Fork("label-185");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_186_differs_from_base()
    {
        var a = new SeededRng(186);
        var b = new SeededRng(186).Fork("label-186");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_187_differs_from_base()
    {
        var a = new SeededRng(187);
        var b = new SeededRng(187).Fork("label-187");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_188_differs_from_base()
    {
        var a = new SeededRng(188);
        var b = new SeededRng(188).Fork("label-188");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_189_differs_from_base()
    {
        var a = new SeededRng(189);
        var b = new SeededRng(189).Fork("label-189");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_190_differs_from_base()
    {
        var a = new SeededRng(190);
        var b = new SeededRng(190).Fork("label-190");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_191_differs_from_base()
    {
        var a = new SeededRng(191);
        var b = new SeededRng(191).Fork("label-191");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_192_differs_from_base()
    {
        var a = new SeededRng(192);
        var b = new SeededRng(192).Fork("label-192");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_193_differs_from_base()
    {
        var a = new SeededRng(193);
        var b = new SeededRng(193).Fork("label-193");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_194_differs_from_base()
    {
        var a = new SeededRng(194);
        var b = new SeededRng(194).Fork("label-194");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_195_differs_from_base()
    {
        var a = new SeededRng(195);
        var b = new SeededRng(195).Fork("label-195");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_196_differs_from_base()
    {
        var a = new SeededRng(196);
        var b = new SeededRng(196).Fork("label-196");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_197_differs_from_base()
    {
        var a = new SeededRng(197);
        var b = new SeededRng(197).Fork("label-197");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_198_differs_from_base()
    {
        var a = new SeededRng(198);
        var b = new SeededRng(198).Fork("label-198");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_199_differs_from_base()
    {
        var a = new SeededRng(199);
        var b = new SeededRng(199).Fork("label-199");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_200_differs_from_base()
    {
        var a = new SeededRng(200);
        var b = new SeededRng(200).Fork("label-200");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_201_differs_from_base()
    {
        var a = new SeededRng(201);
        var b = new SeededRng(201).Fork("label-201");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_202_differs_from_base()
    {
        var a = new SeededRng(202);
        var b = new SeededRng(202).Fork("label-202");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_203_differs_from_base()
    {
        var a = new SeededRng(203);
        var b = new SeededRng(203).Fork("label-203");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_204_differs_from_base()
    {
        var a = new SeededRng(204);
        var b = new SeededRng(204).Fork("label-204");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_205_differs_from_base()
    {
        var a = new SeededRng(205);
        var b = new SeededRng(205).Fork("label-205");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_206_differs_from_base()
    {
        var a = new SeededRng(206);
        var b = new SeededRng(206).Fork("label-206");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_207_differs_from_base()
    {
        var a = new SeededRng(207);
        var b = new SeededRng(207).Fork("label-207");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_208_differs_from_base()
    {
        var a = new SeededRng(208);
        var b = new SeededRng(208).Fork("label-208");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_209_differs_from_base()
    {
        var a = new SeededRng(209);
        var b = new SeededRng(209).Fork("label-209");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_210_differs_from_base()
    {
        var a = new SeededRng(210);
        var b = new SeededRng(210).Fork("label-210");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_211_differs_from_base()
    {
        var a = new SeededRng(211);
        var b = new SeededRng(211).Fork("label-211");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_212_differs_from_base()
    {
        var a = new SeededRng(212);
        var b = new SeededRng(212).Fork("label-212");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_213_differs_from_base()
    {
        var a = new SeededRng(213);
        var b = new SeededRng(213).Fork("label-213");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_214_differs_from_base()
    {
        var a = new SeededRng(214);
        var b = new SeededRng(214).Fork("label-214");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_215_differs_from_base()
    {
        var a = new SeededRng(215);
        var b = new SeededRng(215).Fork("label-215");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_216_differs_from_base()
    {
        var a = new SeededRng(216);
        var b = new SeededRng(216).Fork("label-216");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_217_differs_from_base()
    {
        var a = new SeededRng(217);
        var b = new SeededRng(217).Fork("label-217");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_218_differs_from_base()
    {
        var a = new SeededRng(218);
        var b = new SeededRng(218).Fork("label-218");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_219_differs_from_base()
    {
        var a = new SeededRng(219);
        var b = new SeededRng(219).Fork("label-219");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_220_differs_from_base()
    {
        var a = new SeededRng(220);
        var b = new SeededRng(220).Fork("label-220");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_221_differs_from_base()
    {
        var a = new SeededRng(221);
        var b = new SeededRng(221).Fork("label-221");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_222_differs_from_base()
    {
        var a = new SeededRng(222);
        var b = new SeededRng(222).Fork("label-222");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_223_differs_from_base()
    {
        var a = new SeededRng(223);
        var b = new SeededRng(223).Fork("label-223");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_224_differs_from_base()
    {
        var a = new SeededRng(224);
        var b = new SeededRng(224).Fork("label-224");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_225_differs_from_base()
    {
        var a = new SeededRng(225);
        var b = new SeededRng(225).Fork("label-225");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_226_differs_from_base()
    {
        var a = new SeededRng(226);
        var b = new SeededRng(226).Fork("label-226");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_227_differs_from_base()
    {
        var a = new SeededRng(227);
        var b = new SeededRng(227).Fork("label-227");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_228_differs_from_base()
    {
        var a = new SeededRng(228);
        var b = new SeededRng(228).Fork("label-228");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_229_differs_from_base()
    {
        var a = new SeededRng(229);
        var b = new SeededRng(229).Fork("label-229");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_230_differs_from_base()
    {
        var a = new SeededRng(230);
        var b = new SeededRng(230).Fork("label-230");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_231_differs_from_base()
    {
        var a = new SeededRng(231);
        var b = new SeededRng(231).Fork("label-231");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_232_differs_from_base()
    {
        var a = new SeededRng(232);
        var b = new SeededRng(232).Fork("label-232");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_233_differs_from_base()
    {
        var a = new SeededRng(233);
        var b = new SeededRng(233).Fork("label-233");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_234_differs_from_base()
    {
        var a = new SeededRng(234);
        var b = new SeededRng(234).Fork("label-234");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_235_differs_from_base()
    {
        var a = new SeededRng(235);
        var b = new SeededRng(235).Fork("label-235");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_236_differs_from_base()
    {
        var a = new SeededRng(236);
        var b = new SeededRng(236).Fork("label-236");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_237_differs_from_base()
    {
        var a = new SeededRng(237);
        var b = new SeededRng(237).Fork("label-237");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_238_differs_from_base()
    {
        var a = new SeededRng(238);
        var b = new SeededRng(238).Fork("label-238");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_239_differs_from_base()
    {
        var a = new SeededRng(239);
        var b = new SeededRng(239).Fork("label-239");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_240_differs_from_base()
    {
        var a = new SeededRng(240);
        var b = new SeededRng(240).Fork("label-240");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_241_differs_from_base()
    {
        var a = new SeededRng(241);
        var b = new SeededRng(241).Fork("label-241");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_242_differs_from_base()
    {
        var a = new SeededRng(242);
        var b = new SeededRng(242).Fork("label-242");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_243_differs_from_base()
    {
        var a = new SeededRng(243);
        var b = new SeededRng(243).Fork("label-243");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_244_differs_from_base()
    {
        var a = new SeededRng(244);
        var b = new SeededRng(244).Fork("label-244");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_245_differs_from_base()
    {
        var a = new SeededRng(245);
        var b = new SeededRng(245).Fork("label-245");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_246_differs_from_base()
    {
        var a = new SeededRng(246);
        var b = new SeededRng(246).Fork("label-246");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_247_differs_from_base()
    {
        var a = new SeededRng(247);
        var b = new SeededRng(247).Fork("label-247");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_248_differs_from_base()
    {
        var a = new SeededRng(248);
        var b = new SeededRng(248).Fork("label-248");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_249_differs_from_base()
    {
        var a = new SeededRng(249);
        var b = new SeededRng(249).Fork("label-249");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_250_differs_from_base()
    {
        var a = new SeededRng(250);
        var b = new SeededRng(250).Fork("label-250");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_251_differs_from_base()
    {
        var a = new SeededRng(251);
        var b = new SeededRng(251).Fork("label-251");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_252_differs_from_base()
    {
        var a = new SeededRng(252);
        var b = new SeededRng(252).Fork("label-252");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_253_differs_from_base()
    {
        var a = new SeededRng(253);
        var b = new SeededRng(253).Fork("label-253");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_254_differs_from_base()
    {
        var a = new SeededRng(254);
        var b = new SeededRng(254).Fork("label-254");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_255_differs_from_base()
    {
        var a = new SeededRng(255);
        var b = new SeededRng(255).Fork("label-255");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_256_differs_from_base()
    {
        var a = new SeededRng(256);
        var b = new SeededRng(256).Fork("label-256");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_257_differs_from_base()
    {
        var a = new SeededRng(257);
        var b = new SeededRng(257).Fork("label-257");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_258_differs_from_base()
    {
        var a = new SeededRng(258);
        var b = new SeededRng(258).Fork("label-258");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_259_differs_from_base()
    {
        var a = new SeededRng(259);
        var b = new SeededRng(259).Fork("label-259");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_260_differs_from_base()
    {
        var a = new SeededRng(260);
        var b = new SeededRng(260).Fork("label-260");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_261_differs_from_base()
    {
        var a = new SeededRng(261);
        var b = new SeededRng(261).Fork("label-261");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_262_differs_from_base()
    {
        var a = new SeededRng(262);
        var b = new SeededRng(262).Fork("label-262");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_263_differs_from_base()
    {
        var a = new SeededRng(263);
        var b = new SeededRng(263).Fork("label-263");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_264_differs_from_base()
    {
        var a = new SeededRng(264);
        var b = new SeededRng(264).Fork("label-264");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_265_differs_from_base()
    {
        var a = new SeededRng(265);
        var b = new SeededRng(265).Fork("label-265");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_266_differs_from_base()
    {
        var a = new SeededRng(266);
        var b = new SeededRng(266).Fork("label-266");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_267_differs_from_base()
    {
        var a = new SeededRng(267);
        var b = new SeededRng(267).Fork("label-267");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_268_differs_from_base()
    {
        var a = new SeededRng(268);
        var b = new SeededRng(268).Fork("label-268");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_269_differs_from_base()
    {
        var a = new SeededRng(269);
        var b = new SeededRng(269).Fork("label-269");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_270_differs_from_base()
    {
        var a = new SeededRng(270);
        var b = new SeededRng(270).Fork("label-270");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_271_differs_from_base()
    {
        var a = new SeededRng(271);
        var b = new SeededRng(271).Fork("label-271");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_272_differs_from_base()
    {
        var a = new SeededRng(272);
        var b = new SeededRng(272).Fork("label-272");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_273_differs_from_base()
    {
        var a = new SeededRng(273);
        var b = new SeededRng(273).Fork("label-273");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_274_differs_from_base()
    {
        var a = new SeededRng(274);
        var b = new SeededRng(274).Fork("label-274");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_275_differs_from_base()
    {
        var a = new SeededRng(275);
        var b = new SeededRng(275).Fork("label-275");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_276_differs_from_base()
    {
        var a = new SeededRng(276);
        var b = new SeededRng(276).Fork("label-276");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_277_differs_from_base()
    {
        var a = new SeededRng(277);
        var b = new SeededRng(277).Fork("label-277");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_278_differs_from_base()
    {
        var a = new SeededRng(278);
        var b = new SeededRng(278).Fork("label-278");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_279_differs_from_base()
    {
        var a = new SeededRng(279);
        var b = new SeededRng(279).Fork("label-279");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_280_differs_from_base()
    {
        var a = new SeededRng(280);
        var b = new SeededRng(280).Fork("label-280");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_281_differs_from_base()
    {
        var a = new SeededRng(281);
        var b = new SeededRng(281).Fork("label-281");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_282_differs_from_base()
    {
        var a = new SeededRng(282);
        var b = new SeededRng(282).Fork("label-282");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_283_differs_from_base()
    {
        var a = new SeededRng(283);
        var b = new SeededRng(283).Fork("label-283");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_284_differs_from_base()
    {
        var a = new SeededRng(284);
        var b = new SeededRng(284).Fork("label-284");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_285_differs_from_base()
    {
        var a = new SeededRng(285);
        var b = new SeededRng(285).Fork("label-285");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_286_differs_from_base()
    {
        var a = new SeededRng(286);
        var b = new SeededRng(286).Fork("label-286");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_287_differs_from_base()
    {
        var a = new SeededRng(287);
        var b = new SeededRng(287).Fork("label-287");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_288_differs_from_base()
    {
        var a = new SeededRng(288);
        var b = new SeededRng(288).Fork("label-288");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_289_differs_from_base()
    {
        var a = new SeededRng(289);
        var b = new SeededRng(289).Fork("label-289");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_290_differs_from_base()
    {
        var a = new SeededRng(290);
        var b = new SeededRng(290).Fork("label-290");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_291_differs_from_base()
    {
        var a = new SeededRng(291);
        var b = new SeededRng(291).Fork("label-291");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_292_differs_from_base()
    {
        var a = new SeededRng(292);
        var b = new SeededRng(292).Fork("label-292");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_293_differs_from_base()
    {
        var a = new SeededRng(293);
        var b = new SeededRng(293).Fork("label-293");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_294_differs_from_base()
    {
        var a = new SeededRng(294);
        var b = new SeededRng(294).Fork("label-294");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_295_differs_from_base()
    {
        var a = new SeededRng(295);
        var b = new SeededRng(295).Fork("label-295");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_296_differs_from_base()
    {
        var a = new SeededRng(296);
        var b = new SeededRng(296).Fork("label-296");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_297_differs_from_base()
    {
        var a = new SeededRng(297);
        var b = new SeededRng(297).Fork("label-297");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_298_differs_from_base()
    {
        var a = new SeededRng(298);
        var b = new SeededRng(298).Fork("label-298");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_299_differs_from_base()
    {
        var a = new SeededRng(299);
        var b = new SeededRng(299).Fork("label-299");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_300_differs_from_base()
    {
        var a = new SeededRng(300);
        var b = new SeededRng(300).Fork("label-300");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_301_differs_from_base()
    {
        var a = new SeededRng(301);
        var b = new SeededRng(301).Fork("label-301");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_302_differs_from_base()
    {
        var a = new SeededRng(302);
        var b = new SeededRng(302).Fork("label-302");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_303_differs_from_base()
    {
        var a = new SeededRng(303);
        var b = new SeededRng(303).Fork("label-303");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_304_differs_from_base()
    {
        var a = new SeededRng(304);
        var b = new SeededRng(304).Fork("label-304");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_305_differs_from_base()
    {
        var a = new SeededRng(305);
        var b = new SeededRng(305).Fork("label-305");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_306_differs_from_base()
    {
        var a = new SeededRng(306);
        var b = new SeededRng(306).Fork("label-306");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_307_differs_from_base()
    {
        var a = new SeededRng(307);
        var b = new SeededRng(307).Fork("label-307");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_308_differs_from_base()
    {
        var a = new SeededRng(308);
        var b = new SeededRng(308).Fork("label-308");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_309_differs_from_base()
    {
        var a = new SeededRng(309);
        var b = new SeededRng(309).Fork("label-309");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_310_differs_from_base()
    {
        var a = new SeededRng(310);
        var b = new SeededRng(310).Fork("label-310");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_311_differs_from_base()
    {
        var a = new SeededRng(311);
        var b = new SeededRng(311).Fork("label-311");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_312_differs_from_base()
    {
        var a = new SeededRng(312);
        var b = new SeededRng(312).Fork("label-312");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_313_differs_from_base()
    {
        var a = new SeededRng(313);
        var b = new SeededRng(313).Fork("label-313");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_314_differs_from_base()
    {
        var a = new SeededRng(314);
        var b = new SeededRng(314).Fork("label-314");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_315_differs_from_base()
    {
        var a = new SeededRng(315);
        var b = new SeededRng(315).Fork("label-315");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_316_differs_from_base()
    {
        var a = new SeededRng(316);
        var b = new SeededRng(316).Fork("label-316");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_317_differs_from_base()
    {
        var a = new SeededRng(317);
        var b = new SeededRng(317).Fork("label-317");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_318_differs_from_base()
    {
        var a = new SeededRng(318);
        var b = new SeededRng(318).Fork("label-318");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_319_differs_from_base()
    {
        var a = new SeededRng(319);
        var b = new SeededRng(319).Fork("label-319");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_320_differs_from_base()
    {
        var a = new SeededRng(320);
        var b = new SeededRng(320).Fork("label-320");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_321_differs_from_base()
    {
        var a = new SeededRng(321);
        var b = new SeededRng(321).Fork("label-321");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_322_differs_from_base()
    {
        var a = new SeededRng(322);
        var b = new SeededRng(322).Fork("label-322");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_323_differs_from_base()
    {
        var a = new SeededRng(323);
        var b = new SeededRng(323).Fork("label-323");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_324_differs_from_base()
    {
        var a = new SeededRng(324);
        var b = new SeededRng(324).Fork("label-324");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_325_differs_from_base()
    {
        var a = new SeededRng(325);
        var b = new SeededRng(325).Fork("label-325");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_326_differs_from_base()
    {
        var a = new SeededRng(326);
        var b = new SeededRng(326).Fork("label-326");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_327_differs_from_base()
    {
        var a = new SeededRng(327);
        var b = new SeededRng(327).Fork("label-327");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_328_differs_from_base()
    {
        var a = new SeededRng(328);
        var b = new SeededRng(328).Fork("label-328");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_329_differs_from_base()
    {
        var a = new SeededRng(329);
        var b = new SeededRng(329).Fork("label-329");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_330_differs_from_base()
    {
        var a = new SeededRng(330);
        var b = new SeededRng(330).Fork("label-330");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_331_differs_from_base()
    {
        var a = new SeededRng(331);
        var b = new SeededRng(331).Fork("label-331");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_332_differs_from_base()
    {
        var a = new SeededRng(332);
        var b = new SeededRng(332).Fork("label-332");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_333_differs_from_base()
    {
        var a = new SeededRng(333);
        var b = new SeededRng(333).Fork("label-333");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_334_differs_from_base()
    {
        var a = new SeededRng(334);
        var b = new SeededRng(334).Fork("label-334");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_335_differs_from_base()
    {
        var a = new SeededRng(335);
        var b = new SeededRng(335).Fork("label-335");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_336_differs_from_base()
    {
        var a = new SeededRng(336);
        var b = new SeededRng(336).Fork("label-336");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_337_differs_from_base()
    {
        var a = new SeededRng(337);
        var b = new SeededRng(337).Fork("label-337");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_338_differs_from_base()
    {
        var a = new SeededRng(338);
        var b = new SeededRng(338).Fork("label-338");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_339_differs_from_base()
    {
        var a = new SeededRng(339);
        var b = new SeededRng(339).Fork("label-339");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_340_differs_from_base()
    {
        var a = new SeededRng(340);
        var b = new SeededRng(340).Fork("label-340");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_341_differs_from_base()
    {
        var a = new SeededRng(341);
        var b = new SeededRng(341).Fork("label-341");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_342_differs_from_base()
    {
        var a = new SeededRng(342);
        var b = new SeededRng(342).Fork("label-342");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_343_differs_from_base()
    {
        var a = new SeededRng(343);
        var b = new SeededRng(343).Fork("label-343");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_344_differs_from_base()
    {
        var a = new SeededRng(344);
        var b = new SeededRng(344).Fork("label-344");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_345_differs_from_base()
    {
        var a = new SeededRng(345);
        var b = new SeededRng(345).Fork("label-345");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_346_differs_from_base()
    {
        var a = new SeededRng(346);
        var b = new SeededRng(346).Fork("label-346");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_347_differs_from_base()
    {
        var a = new SeededRng(347);
        var b = new SeededRng(347).Fork("label-347");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_348_differs_from_base()
    {
        var a = new SeededRng(348);
        var b = new SeededRng(348).Fork("label-348");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_349_differs_from_base()
    {
        var a = new SeededRng(349);
        var b = new SeededRng(349).Fork("label-349");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_350_differs_from_base()
    {
        var a = new SeededRng(350);
        var b = new SeededRng(350).Fork("label-350");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_351_differs_from_base()
    {
        var a = new SeededRng(351);
        var b = new SeededRng(351).Fork("label-351");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_352_differs_from_base()
    {
        var a = new SeededRng(352);
        var b = new SeededRng(352).Fork("label-352");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_353_differs_from_base()
    {
        var a = new SeededRng(353);
        var b = new SeededRng(353).Fork("label-353");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_354_differs_from_base()
    {
        var a = new SeededRng(354);
        var b = new SeededRng(354).Fork("label-354");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_355_differs_from_base()
    {
        var a = new SeededRng(355);
        var b = new SeededRng(355).Fork("label-355");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_356_differs_from_base()
    {
        var a = new SeededRng(356);
        var b = new SeededRng(356).Fork("label-356");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_357_differs_from_base()
    {
        var a = new SeededRng(357);
        var b = new SeededRng(357).Fork("label-357");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_358_differs_from_base()
    {
        var a = new SeededRng(358);
        var b = new SeededRng(358).Fork("label-358");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_359_differs_from_base()
    {
        var a = new SeededRng(359);
        var b = new SeededRng(359).Fork("label-359");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_360_differs_from_base()
    {
        var a = new SeededRng(360);
        var b = new SeededRng(360).Fork("label-360");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_361_differs_from_base()
    {
        var a = new SeededRng(361);
        var b = new SeededRng(361).Fork("label-361");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_362_differs_from_base()
    {
        var a = new SeededRng(362);
        var b = new SeededRng(362).Fork("label-362");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_363_differs_from_base()
    {
        var a = new SeededRng(363);
        var b = new SeededRng(363).Fork("label-363");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_364_differs_from_base()
    {
        var a = new SeededRng(364);
        var b = new SeededRng(364).Fork("label-364");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_365_differs_from_base()
    {
        var a = new SeededRng(365);
        var b = new SeededRng(365).Fork("label-365");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_366_differs_from_base()
    {
        var a = new SeededRng(366);
        var b = new SeededRng(366).Fork("label-366");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_367_differs_from_base()
    {
        var a = new SeededRng(367);
        var b = new SeededRng(367).Fork("label-367");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_368_differs_from_base()
    {
        var a = new SeededRng(368);
        var b = new SeededRng(368).Fork("label-368");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_369_differs_from_base()
    {
        var a = new SeededRng(369);
        var b = new SeededRng(369).Fork("label-369");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_370_differs_from_base()
    {
        var a = new SeededRng(370);
        var b = new SeededRng(370).Fork("label-370");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_371_differs_from_base()
    {
        var a = new SeededRng(371);
        var b = new SeededRng(371).Fork("label-371");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_372_differs_from_base()
    {
        var a = new SeededRng(372);
        var b = new SeededRng(372).Fork("label-372");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_373_differs_from_base()
    {
        var a = new SeededRng(373);
        var b = new SeededRng(373).Fork("label-373");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_374_differs_from_base()
    {
        var a = new SeededRng(374);
        var b = new SeededRng(374).Fork("label-374");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_375_differs_from_base()
    {
        var a = new SeededRng(375);
        var b = new SeededRng(375).Fork("label-375");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_376_differs_from_base()
    {
        var a = new SeededRng(376);
        var b = new SeededRng(376).Fork("label-376");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_377_differs_from_base()
    {
        var a = new SeededRng(377);
        var b = new SeededRng(377).Fork("label-377");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_378_differs_from_base()
    {
        var a = new SeededRng(378);
        var b = new SeededRng(378).Fork("label-378");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_379_differs_from_base()
    {
        var a = new SeededRng(379);
        var b = new SeededRng(379).Fork("label-379");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_380_differs_from_base()
    {
        var a = new SeededRng(380);
        var b = new SeededRng(380).Fork("label-380");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_381_differs_from_base()
    {
        var a = new SeededRng(381);
        var b = new SeededRng(381).Fork("label-381");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_382_differs_from_base()
    {
        var a = new SeededRng(382);
        var b = new SeededRng(382).Fork("label-382");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_383_differs_from_base()
    {
        var a = new SeededRng(383);
        var b = new SeededRng(383).Fork("label-383");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_384_differs_from_base()
    {
        var a = new SeededRng(384);
        var b = new SeededRng(384).Fork("label-384");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_385_differs_from_base()
    {
        var a = new SeededRng(385);
        var b = new SeededRng(385).Fork("label-385");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_386_differs_from_base()
    {
        var a = new SeededRng(386);
        var b = new SeededRng(386).Fork("label-386");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_387_differs_from_base()
    {
        var a = new SeededRng(387);
        var b = new SeededRng(387).Fork("label-387");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_388_differs_from_base()
    {
        var a = new SeededRng(388);
        var b = new SeededRng(388).Fork("label-388");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_389_differs_from_base()
    {
        var a = new SeededRng(389);
        var b = new SeededRng(389).Fork("label-389");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_390_differs_from_base()
    {
        var a = new SeededRng(390);
        var b = new SeededRng(390).Fork("label-390");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_391_differs_from_base()
    {
        var a = new SeededRng(391);
        var b = new SeededRng(391).Fork("label-391");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_392_differs_from_base()
    {
        var a = new SeededRng(392);
        var b = new SeededRng(392).Fork("label-392");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_393_differs_from_base()
    {
        var a = new SeededRng(393);
        var b = new SeededRng(393).Fork("label-393");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_394_differs_from_base()
    {
        var a = new SeededRng(394);
        var b = new SeededRng(394).Fork("label-394");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_395_differs_from_base()
    {
        var a = new SeededRng(395);
        var b = new SeededRng(395).Fork("label-395");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_396_differs_from_base()
    {
        var a = new SeededRng(396);
        var b = new SeededRng(396).Fork("label-396");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_397_differs_from_base()
    {
        var a = new SeededRng(397);
        var b = new SeededRng(397).Fork("label-397");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_398_differs_from_base()
    {
        var a = new SeededRng(398);
        var b = new SeededRng(398).Fork("label-398");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_399_differs_from_base()
    {
        var a = new SeededRng(399);
        var b = new SeededRng(399).Fork("label-399");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_400_differs_from_base()
    {
        var a = new SeededRng(400);
        var b = new SeededRng(400).Fork("label-400");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_401_differs_from_base()
    {
        var a = new SeededRng(401);
        var b = new SeededRng(401).Fork("label-401");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_402_differs_from_base()
    {
        var a = new SeededRng(402);
        var b = new SeededRng(402).Fork("label-402");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_403_differs_from_base()
    {
        var a = new SeededRng(403);
        var b = new SeededRng(403).Fork("label-403");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_404_differs_from_base()
    {
        var a = new SeededRng(404);
        var b = new SeededRng(404).Fork("label-404");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_405_differs_from_base()
    {
        var a = new SeededRng(405);
        var b = new SeededRng(405).Fork("label-405");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_406_differs_from_base()
    {
        var a = new SeededRng(406);
        var b = new SeededRng(406).Fork("label-406");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_407_differs_from_base()
    {
        var a = new SeededRng(407);
        var b = new SeededRng(407).Fork("label-407");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_408_differs_from_base()
    {
        var a = new SeededRng(408);
        var b = new SeededRng(408).Fork("label-408");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_409_differs_from_base()
    {
        var a = new SeededRng(409);
        var b = new SeededRng(409).Fork("label-409");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_410_differs_from_base()
    {
        var a = new SeededRng(410);
        var b = new SeededRng(410).Fork("label-410");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_411_differs_from_base()
    {
        var a = new SeededRng(411);
        var b = new SeededRng(411).Fork("label-411");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_412_differs_from_base()
    {
        var a = new SeededRng(412);
        var b = new SeededRng(412).Fork("label-412");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_413_differs_from_base()
    {
        var a = new SeededRng(413);
        var b = new SeededRng(413).Fork("label-413");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_414_differs_from_base()
    {
        var a = new SeededRng(414);
        var b = new SeededRng(414).Fork("label-414");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_415_differs_from_base()
    {
        var a = new SeededRng(415);
        var b = new SeededRng(415).Fork("label-415");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_416_differs_from_base()
    {
        var a = new SeededRng(416);
        var b = new SeededRng(416).Fork("label-416");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_417_differs_from_base()
    {
        var a = new SeededRng(417);
        var b = new SeededRng(417).Fork("label-417");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_418_differs_from_base()
    {
        var a = new SeededRng(418);
        var b = new SeededRng(418).Fork("label-418");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_419_differs_from_base()
    {
        var a = new SeededRng(419);
        var b = new SeededRng(419).Fork("label-419");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_420_differs_from_base()
    {
        var a = new SeededRng(420);
        var b = new SeededRng(420).Fork("label-420");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_421_differs_from_base()
    {
        var a = new SeededRng(421);
        var b = new SeededRng(421).Fork("label-421");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_422_differs_from_base()
    {
        var a = new SeededRng(422);
        var b = new SeededRng(422).Fork("label-422");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_423_differs_from_base()
    {
        var a = new SeededRng(423);
        var b = new SeededRng(423).Fork("label-423");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_424_differs_from_base()
    {
        var a = new SeededRng(424);
        var b = new SeededRng(424).Fork("label-424");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_425_differs_from_base()
    {
        var a = new SeededRng(425);
        var b = new SeededRng(425).Fork("label-425");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_426_differs_from_base()
    {
        var a = new SeededRng(426);
        var b = new SeededRng(426).Fork("label-426");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_427_differs_from_base()
    {
        var a = new SeededRng(427);
        var b = new SeededRng(427).Fork("label-427");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_428_differs_from_base()
    {
        var a = new SeededRng(428);
        var b = new SeededRng(428).Fork("label-428");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_429_differs_from_base()
    {
        var a = new SeededRng(429);
        var b = new SeededRng(429).Fork("label-429");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_430_differs_from_base()
    {
        var a = new SeededRng(430);
        var b = new SeededRng(430).Fork("label-430");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_431_differs_from_base()
    {
        var a = new SeededRng(431);
        var b = new SeededRng(431).Fork("label-431");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_432_differs_from_base()
    {
        var a = new SeededRng(432);
        var b = new SeededRng(432).Fork("label-432");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_433_differs_from_base()
    {
        var a = new SeededRng(433);
        var b = new SeededRng(433).Fork("label-433");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_434_differs_from_base()
    {
        var a = new SeededRng(434);
        var b = new SeededRng(434).Fork("label-434");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_435_differs_from_base()
    {
        var a = new SeededRng(435);
        var b = new SeededRng(435).Fork("label-435");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_436_differs_from_base()
    {
        var a = new SeededRng(436);
        var b = new SeededRng(436).Fork("label-436");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_437_differs_from_base()
    {
        var a = new SeededRng(437);
        var b = new SeededRng(437).Fork("label-437");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_438_differs_from_base()
    {
        var a = new SeededRng(438);
        var b = new SeededRng(438).Fork("label-438");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_439_differs_from_base()
    {
        var a = new SeededRng(439);
        var b = new SeededRng(439).Fork("label-439");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_440_differs_from_base()
    {
        var a = new SeededRng(440);
        var b = new SeededRng(440).Fork("label-440");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_441_differs_from_base()
    {
        var a = new SeededRng(441);
        var b = new SeededRng(441).Fork("label-441");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_442_differs_from_base()
    {
        var a = new SeededRng(442);
        var b = new SeededRng(442).Fork("label-442");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_443_differs_from_base()
    {
        var a = new SeededRng(443);
        var b = new SeededRng(443).Fork("label-443");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_444_differs_from_base()
    {
        var a = new SeededRng(444);
        var b = new SeededRng(444).Fork("label-444");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_445_differs_from_base()
    {
        var a = new SeededRng(445);
        var b = new SeededRng(445).Fork("label-445");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_446_differs_from_base()
    {
        var a = new SeededRng(446);
        var b = new SeededRng(446).Fork("label-446");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_447_differs_from_base()
    {
        var a = new SeededRng(447);
        var b = new SeededRng(447).Fork("label-447");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_448_differs_from_base()
    {
        var a = new SeededRng(448);
        var b = new SeededRng(448).Fork("label-448");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_449_differs_from_base()
    {
        var a = new SeededRng(449);
        var b = new SeededRng(449).Fork("label-449");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_450_differs_from_base()
    {
        var a = new SeededRng(450);
        var b = new SeededRng(450).Fork("label-450");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_451_differs_from_base()
    {
        var a = new SeededRng(451);
        var b = new SeededRng(451).Fork("label-451");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_452_differs_from_base()
    {
        var a = new SeededRng(452);
        var b = new SeededRng(452).Fork("label-452");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_453_differs_from_base()
    {
        var a = new SeededRng(453);
        var b = new SeededRng(453).Fork("label-453");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_454_differs_from_base()
    {
        var a = new SeededRng(454);
        var b = new SeededRng(454).Fork("label-454");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_455_differs_from_base()
    {
        var a = new SeededRng(455);
        var b = new SeededRng(455).Fork("label-455");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_456_differs_from_base()
    {
        var a = new SeededRng(456);
        var b = new SeededRng(456).Fork("label-456");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_457_differs_from_base()
    {
        var a = new SeededRng(457);
        var b = new SeededRng(457).Fork("label-457");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_458_differs_from_base()
    {
        var a = new SeededRng(458);
        var b = new SeededRng(458).Fork("label-458");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_459_differs_from_base()
    {
        var a = new SeededRng(459);
        var b = new SeededRng(459).Fork("label-459");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_460_differs_from_base()
    {
        var a = new SeededRng(460);
        var b = new SeededRng(460).Fork("label-460");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_461_differs_from_base()
    {
        var a = new SeededRng(461);
        var b = new SeededRng(461).Fork("label-461");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_462_differs_from_base()
    {
        var a = new SeededRng(462);
        var b = new SeededRng(462).Fork("label-462");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_463_differs_from_base()
    {
        var a = new SeededRng(463);
        var b = new SeededRng(463).Fork("label-463");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_464_differs_from_base()
    {
        var a = new SeededRng(464);
        var b = new SeededRng(464).Fork("label-464");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_465_differs_from_base()
    {
        var a = new SeededRng(465);
        var b = new SeededRng(465).Fork("label-465");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_466_differs_from_base()
    {
        var a = new SeededRng(466);
        var b = new SeededRng(466).Fork("label-466");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_467_differs_from_base()
    {
        var a = new SeededRng(467);
        var b = new SeededRng(467).Fork("label-467");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_468_differs_from_base()
    {
        var a = new SeededRng(468);
        var b = new SeededRng(468).Fork("label-468");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_469_differs_from_base()
    {
        var a = new SeededRng(469);
        var b = new SeededRng(469).Fork("label-469");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_470_differs_from_base()
    {
        var a = new SeededRng(470);
        var b = new SeededRng(470).Fork("label-470");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_471_differs_from_base()
    {
        var a = new SeededRng(471);
        var b = new SeededRng(471).Fork("label-471");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_472_differs_from_base()
    {
        var a = new SeededRng(472);
        var b = new SeededRng(472).Fork("label-472");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_473_differs_from_base()
    {
        var a = new SeededRng(473);
        var b = new SeededRng(473).Fork("label-473");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_474_differs_from_base()
    {
        var a = new SeededRng(474);
        var b = new SeededRng(474).Fork("label-474");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_475_differs_from_base()
    {
        var a = new SeededRng(475);
        var b = new SeededRng(475).Fork("label-475");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_476_differs_from_base()
    {
        var a = new SeededRng(476);
        var b = new SeededRng(476).Fork("label-476");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_477_differs_from_base()
    {
        var a = new SeededRng(477);
        var b = new SeededRng(477).Fork("label-477");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_478_differs_from_base()
    {
        var a = new SeededRng(478);
        var b = new SeededRng(478).Fork("label-478");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_479_differs_from_base()
    {
        var a = new SeededRng(479);
        var b = new SeededRng(479).Fork("label-479");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_480_differs_from_base()
    {
        var a = new SeededRng(480);
        var b = new SeededRng(480).Fork("label-480");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_481_differs_from_base()
    {
        var a = new SeededRng(481);
        var b = new SeededRng(481).Fork("label-481");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_482_differs_from_base()
    {
        var a = new SeededRng(482);
        var b = new SeededRng(482).Fork("label-482");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_483_differs_from_base()
    {
        var a = new SeededRng(483);
        var b = new SeededRng(483).Fork("label-483");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_484_differs_from_base()
    {
        var a = new SeededRng(484);
        var b = new SeededRng(484).Fork("label-484");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_485_differs_from_base()
    {
        var a = new SeededRng(485);
        var b = new SeededRng(485).Fork("label-485");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_486_differs_from_base()
    {
        var a = new SeededRng(486);
        var b = new SeededRng(486).Fork("label-486");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_487_differs_from_base()
    {
        var a = new SeededRng(487);
        var b = new SeededRng(487).Fork("label-487");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_488_differs_from_base()
    {
        var a = new SeededRng(488);
        var b = new SeededRng(488).Fork("label-488");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_489_differs_from_base()
    {
        var a = new SeededRng(489);
        var b = new SeededRng(489).Fork("label-489");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_490_differs_from_base()
    {
        var a = new SeededRng(490);
        var b = new SeededRng(490).Fork("label-490");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_491_differs_from_base()
    {
        var a = new SeededRng(491);
        var b = new SeededRng(491).Fork("label-491");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_492_differs_from_base()
    {
        var a = new SeededRng(492);
        var b = new SeededRng(492).Fork("label-492");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_493_differs_from_base()
    {
        var a = new SeededRng(493);
        var b = new SeededRng(493).Fork("label-493");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_494_differs_from_base()
    {
        var a = new SeededRng(494);
        var b = new SeededRng(494).Fork("label-494");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_495_differs_from_base()
    {
        var a = new SeededRng(495);
        var b = new SeededRng(495).Fork("label-495");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_496_differs_from_base()
    {
        var a = new SeededRng(496);
        var b = new SeededRng(496).Fork("label-496");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_497_differs_from_base()
    {
        var a = new SeededRng(497);
        var b = new SeededRng(497).Fork("label-497");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_498_differs_from_base()
    {
        var a = new SeededRng(498);
        var b = new SeededRng(498).Fork("label-498");
        a.Next().Should().NotBe(b.Next());
    }

    [Fact]
    public void Rng_fork_499_differs_from_base()
    {
        var a = new SeededRng(499);
        var b = new SeededRng(499).Fork("label-499");
        a.Next().Should().NotBe(b.Next());
    }
}
