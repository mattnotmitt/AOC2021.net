using Day9;
using NUnit.Framework;

namespace Day9Test;

public class Tests
{
    private static string[] testData =
    {
        "2199943210",
        "3987894921",
        "9856789892",
        "8767896789",
        "9899965678"
    };
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var logic = new SmokeLogic(testData);
        Assert.AreEqual(15, logic.Part1Answer());
        Assert.AreEqual(1134, logic.Part2Answer());
    }
}