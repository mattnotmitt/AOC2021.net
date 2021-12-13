using System.Collections.Immutable;
using System.Security.Cryptography;

namespace Day12;

using static File;

public class Program
{
    public static void Main(string[] args)
    {
        var logic = new OrigamiLogic(ReadAllText(@"./input.txt"));
        Console.WriteLine(logic.Part1Answer());
        Console.WriteLine(logic.Part2Answer());
    }
}

public class OrigamiLogic
{

    private bool[][] GenerateMap(string[] inp)
    {
        var (maxX, maxY) = (0, 0);
        var points = (from point in inp
            let parts = point.Split(",")
            let x = int.Parse(parts[0])
            let y = int.Parse(parts[1])
            select (x, y)).ToArray();
        foreach (var x in points.Select(obj => obj.x)) if (x > maxX) maxX = x;
        foreach (var y in points.Select(obj => obj.y)) if (y > maxY) maxY = y;
        return new bool[][] {};
    }

    private (string, int)[] ReadFolds(string[] inp)
    {
        return (from fold in inp
            let parts = fold[11..].Split("=")
            let axis = parts[0]
            let ind = int.Parse(parts[1])
            select (axis, ind)).ToArray();
    }

    private bool[][] unfoldedDotMap;
    private (string, int)[] folds;
    public OrigamiLogic(string inp)
    {
        var breakInput = inp.Split("\n\n");
        unfoldedDotMap = GenerateMap(breakInput[0].Split("\n"));
        folds = ReadFolds(breakInput[1].Split("\n"));
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