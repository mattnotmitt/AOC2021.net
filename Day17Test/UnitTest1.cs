using NUnit.Framework;
using Day17;

namespace Day17Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var logic = new ProbeLogic("target area: x=20..30, y=-10..-5");
        Assert.AreEqual(45, logic.Part1Answer());
        Assert.AreEqual(112, logic.Part2Answer());
    }
}