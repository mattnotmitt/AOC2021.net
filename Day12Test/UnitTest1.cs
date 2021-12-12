using Day12;
using NUnit.Framework;

namespace Day12Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var testData = new[]{"start-A","start-b","A-c","A-b","b-d","A-end","b-end"};
        var logic = new PathLogic(testData);
        Assert.AreEqual(10, logic.Part1Answer());
        Assert.AreEqual(36, logic.Part2Answer());
    }

    [Test]
    public void Test2()
    {
        var testData = new[]
            {"dc-end", "HN-start", "start-kj", "dc-start", "dc-HN", "LN-dc", "HN-end", "kj-sa", "kj-HN", "kj-dc"};
        var logic = new PathLogic(testData);
        Assert.AreEqual(19, logic.Part1Answer());
        Assert.AreEqual(103, logic.Part2Answer());
    }
    
    [Test]
    public void Test3()
    {
        var testData = new[]
            {
                "fs-end", "he-DX", "fs-he", "start-DX", "pj-DX", "end-zg", "zg-sl", "zg-pj", "pj-he", "RW-he", "fs-DX",
                "pj-RW", "zg-RW", "start-pj", "he-WI", "zg-he", "pj-fs", "start-RW"
            };
        var logic = new PathLogic(testData);
        Assert.AreEqual(226, logic.Part1Answer());
        Assert.AreEqual(3509, logic.Part2Answer());
    }
}