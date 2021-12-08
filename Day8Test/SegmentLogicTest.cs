using NUnit.Framework;
using Day8;

namespace Day8Test;

public class Tests
{
    private static string[] testData = {
        "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe",
        "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc", 
        "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg",
        "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb",
        "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea",
        "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb",
        "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe",
        "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef",
        "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb",
        "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce"
    };

    private static string[] testData2 = {"acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf"};
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SegmentLogicTest1()
    {
        var logic = new SegmentLogic();
        logic.LoadInput(testData);
        Assert.AreEqual(26, logic.Part1Answer());
        Assert.AreEqual(61229, logic.Part2Answer());
    }
    
    [Test]
    public void SegmentLogicTest2()
    {
        var logic = new SegmentLogic();
        logic.LoadInput(testData2);
        logic.IdentSegments();
        var d = logic.displays[0];
        Assert.AreEqual('a', d.seg[2]);
        Assert.AreEqual('b', d.seg[5]);
        Assert.AreEqual('c', d.seg[6]);
        Assert.AreEqual('d', d.seg[0]);
        Assert.AreEqual('e', d.seg[1]);
        Assert.AreEqual('f', d.seg[3]);
        Assert.AreEqual('g', d.seg[4]);
    }
}