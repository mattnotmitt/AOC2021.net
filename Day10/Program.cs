// See https://aka.ms/new-console-template for more information

using System.Numerics;

namespace Day10;

using static System.IO.File;

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
    private string[] navSubSys;
    private readonly Dictionary<char, char> pairs = new() {{'[', ']'}, {'<','>'}, {'(', ')'}, {'{', '}'}};
    private readonly Dictionary<char, int> corrScores = new() {{']', 57}, {'>',25137}, {')', 3}, {'}', 1197}};

    private int synScore = 0;
    public SyntaxLogic(string[] inp)
    {
        navSubSys = inp;
    }

    private int RecursiveChunkage(string substr, char closing)
    {
        for (var i = 1; i < substr.Length;)
        {
            var c = substr[i];
            var ni = i+1;
            if (c == closing) return ni;
            if (pairs.ContainsKey(c))
            {
                var chunkLen = RecursiveChunkage(substr[i..], pairs[c]);
                if (chunkLen == -1) return -1;
                i += chunkLen;
            }
            else
            {
                synScore += corrScores[c];
                return -1;
            }
        }

        return 1;
    }
    
    private List<string> unfinishedLines = new List<string>();
    private bool foundUnfinished = false;

    private void FindUnfinished()
    {
        foreach (var line in navSubSys)
        {
            if (RecursiveChunkage(line, pairs[line[0]]) != -1) unfinishedLines.Add(line);
        }

        foundUnfinished = true;
    }

    public int Part1Answer()
    {
        if (!foundUnfinished) FindUnfinished();
        return synScore;
    }

    private List<long> finishScores = new();
    private bool linesHaveBeenFinished = false;
    private readonly Dictionary<char, int> finScores = new() {{')', 1}, {']', 2}, {'}', 3}, {'>', 4}};
    
    private int RecursiveFinish(int index, ref string line, ref string end, int bi, char closing)
    {
        int i;
        for ( i = bi; i < line.Length;)
        {
            var c = line[i];
            var ni = i+1;
            if (c == closing)
            {
                if (bi != 1) return ni - bi + 1;
                i++;
                closing = pairs[line[ni]];
                continue;
            }

            if (!pairs.ContainsKey(c)) continue;
            var chunkLen = RecursiveFinish(index, ref line, ref end, ni, pairs[c]);
            i += chunkLen;
            
            if (bi == 1 && i == line.Length && (line.Length + end.Length) % 2 == 0) return 1;
        }

        end += closing;
        finishScores[index] = finishScores[index]*5L + finScores[closing];
        return i+1-bi;
    }
    
    private void FinishLines()
    {
        foreach (var (line, i) in unfinishedLines.Select((l,i) => (l,i)))
        { 
            finishScores.Add(0L);
            var s = line;
            var c = "";
            RecursiveFinish(i, ref s, ref c, 1, pairs[s[0]]);
        }

        linesHaveBeenFinished = true;
    }

    public BigInteger Part2Answer()
    {
        if (!linesHaveBeenFinished) FinishLines();
        finishScores.Sort();
        return finishScores[(int) Math.Ceiling(finishScores.Count/2F)];
    }
}