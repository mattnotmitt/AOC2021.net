// See https://aka.ms/new-console-template for more information

using System.Text;

namespace Day10;

using static File;

public class Program
{
    public static void Main(string[] args)
    {
        var logic = new SyntaxLogic(ReadAllLines(@"./input.txt"));
        Console.WriteLine(logic.Part1Answer());
        Console.WriteLine(logic.Part2Answer());
    }
}

public class SyntaxLogic
{
    private readonly string[] navSubSys;

    public SyntaxLogic(string[] inp)
    {
        navSubSys = inp;
    }

    private Stack<char> ParseLine(string line)
    {
        var matches = new Dictionary<char, char> {{'}', '{'}, {')', '('}, {'>', '<'}, {']', '['}};
        var stack = new Stack<char>();

        foreach (var c in line)
            if (c is '{' or '[' or '<' or '(')
            {
                stack.Push(c);
            }
            else
            {
                var p = stack.Pop();
                if (matches[c] != p)
                    throw new Exception(c.ToString());
            }

        return stack;
    }

    private char CheckForCorruptedLine(string line)
    {
        try
        {
            ParseLine(line);
            return '\0';
        }
        catch (Exception ex)
        {
            return ex.Message[0];
        }
    }

    private string CompleteLine(string line)
    {
        var matches = new Dictionary<char, char> {{'{', '}'}, {'(', ')'}, {'<', '>'}, {'[', ']'}};
        var closers = new StringBuilder();
        try
        {
            var stack = ParseLine(line);
            // We should have only unmatched symbols left            
            while (stack.Count > 0)
                closers.Append(matches[stack.Pop()]);
        }
        catch (Exception)
        {
            return "error";
        }

        return closers.ToString();
    }

    private static long ScorePart2(string s)
    {
        var values = new Dictionary<char, long> {{'}', 3}, {')', 1}, {'>', 4}, {']', 2}};
        return s.Aggregate(0, (long a, char b) => a * 5 + values[b]);
    }

    public int Part1Answer()
    {
        var values = new Dictionary<char, int> {{'}', 1197}, {')', 3}, {'>', 25137}, {']', 57}, {'\0', 0}};
        var total = navSubSys.Select(l => values[CheckForCorruptedLine(l)]).Sum();

        return total;
    }

    public long Part2Answer()
    {
        var scores = navSubSys.Select(CompleteLine)
            .Where(l => l != "error")
            .Select(ScorePart2)
            .OrderBy(s => s).ToList();
        return scores[scores.Count / 2];
    }
}