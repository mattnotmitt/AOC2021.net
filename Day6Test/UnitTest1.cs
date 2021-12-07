using System.Numerics;
using NUnit.Framework;
using Day6;

namespace Day6Test;

public class Tests
{
    
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Day6Test1()
    {
        var logic = new LanternfishLogic();
        logic.LoadInput("3,4,3,1,2");
        Assert.AreEqual(5934L, logic.Part1Answer());
        Assert.AreEqual(26984457539L, logic.Part2Answer());
    }
}