using System.Linq;
using Day3;
using NUnit.Framework;

namespace Day3Test;

public class Tests
{
    private static readonly string testData =
        "00100\n11110\n10110\n10111\n10101\n01111\n00111\n11100\n10000\n11001\n00010\n01010";

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var input = testData.Split("\n").Select(x => x.ToCharArray().Select(y => y == '1').ToArray()).ToArray();
        Assert.AreEqual(23, LifeSupport.GetRating(input, true));
        Assert.AreEqual(10, LifeSupport.GetRating(input, false));
        Assert.Pass();
    }
}