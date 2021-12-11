using NUnit.Framework;
using Day10;

namespace Day10Test;

public class Tests
{
    private string[] testData =
    {
        "[({(<(())[]>[[{[]{<()<>>",
        "[(()[<>])]({[<{<<[]>>(",
        "{([(<{}[<>[]}>{[]{[(<()>",
        "(((({<>}<{<{<>}{[]{[]{}",
        "[[<[([]))<([[{}[[()]]]",
        "[{[{({}]{}}([{[{{{}}([]",
        "{<[[]]>}<{[{[{[]{()[[[]",
        "[<(<(<(<{}))><([]([]()",
        "<{([([[(<>()){}]>(<<{{",
        "<{([{{}}[<[[[<>{}]]]>[]]"
    };

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var logic = new SyntaxLogic(testData);
        Assert.AreEqual(26397, logic.Part1Answer());
        Assert.AreEqual(288957L, logic.Part2Answer());
    }
}