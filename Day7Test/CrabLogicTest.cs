using NUnit.Framework;
using Day7;

namespace Day7Test;

public class Tests
{
    private static string testInput = "16,1,2,0,4,2,7,1,2,14";
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Day7Test1()
    {
        var logic = new CrabLogic();
        logic.LoadInputs(testInput);
        Assert.AreEqual(37, logic.Part1Answer());
        Assert.AreEqual(168, logic.Part2Answer());
    }
}