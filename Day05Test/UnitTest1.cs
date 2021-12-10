using Day05;
using NUnit.Framework;

namespace Day05Test;

public class Tests
{
    private static readonly string testInput =
        "0,9 -> 5,9\n8,0 -> 0,8\n9,4 -> 3,4\n2,2 -> 2,1\n7,0 -> 7,4\n6,4 -> 2,0\n0,9 -> 2,9\n3,4 -> 1,4\n0,0 -> 8,8\n5,5 -> 8,2";

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var logic = new VentLogic();
        logic.LoadInput(testInput.Split("\n"));
        Assert.AreEqual(5, logic.Part1Answer());
        Assert.AreEqual(12, logic.Part2Answer());
    }
}