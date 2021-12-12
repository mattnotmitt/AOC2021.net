using System.Collections.Immutable;
using System.Security.Cryptography;

namespace Day12;

using static File;

public class Program
{
    public static void Main(string[] args)
    {
        var logic = new PathLogic(ReadAllLines(@"./input.txt"));
        Console.WriteLine(logic.Part1Answer());
        Console.WriteLine(logic.Part2Answer());
    }
}

public class PathLogic
{
    private Dictionary<string, string[]> map;

    private static Dictionary<string, string[]> GetMap(string[] inp)
    {
        var conns =
            from line in inp
            let parts = line.Split("-")
            let a = parts[0]
            let b = parts[1]
            from conn in new[] {(From: a, To: b), (From: b, To: a)}
            select conn;

        return (from p in conns group p by p.From into g select g).ToDictionary(g => g.Key,
            g => g.Select(conn => conn.To).ToArray());
    }
    
    public PathLogic(string[] inp)
    {
        map = GetMap(inp);
    }

    int countPathsRec(string curr, ImmutableHashSet<string> visCaves, bool visSmallTwice)
    {
        // If this is the end of a path, add one to the rec count
        if (curr == "end") return 1;

        var paths = 0;
        foreach (var dest in map[curr])
        {
            var isBig = dest.ToUpper() == dest;
            var seen = visCaves.Contains(dest);
            if (!seen || isBig)
            {
                paths += countPathsRec(dest, visCaves.Add(dest), visSmallTwice);
            }
            else if (!isBig && dest != "start" && !visSmallTwice)
            {
                paths += countPathsRec(dest, visCaves.Add(dest), true);
            }
        }

        return paths;
    }
    
    public int Part1Answer()
    {
        return countPathsRec("start", ImmutableHashSet.Create("start"), true);
    }
    
    public int Part2Answer()
    {
        return countPathsRec("start", ImmutableHashSet.Create("start"), false);
    }
}
