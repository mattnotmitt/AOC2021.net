using NUnit.Framework;
using Day15;

namespace Day15Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var testData = new[] {"1163751742","1381373672","2136511328","3694931569","7463417111","1319128137","1359912421","3125421639","1293138521","2311944581"};
        var logic = new ChitonLogic(testData);
        Assert.AreEqual(40, logic.Part1Answer());
        Assert.AreEqual(315, logic.Part2Answer());
    }
}