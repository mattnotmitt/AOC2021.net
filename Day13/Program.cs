using MoreLinq;

namespace Day13;

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
        foreach (var (x, y) in points)
        {
            if (x > maxX) maxX = x;
            if (y > maxY) maxY = y;
        }

        var map = new bool[maxY + 1][];
        foreach (var i in Enumerable.Range(0, maxY+1))
        {
            map[i] = new bool[maxX + 1];
        }
        foreach (var (x, y) in points)
        {
            map[y][x] = true;
        }
        return map;
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

    private bool[][] tmpMap;
    
    private bool[][] DoFold(bool [][] map, (string axis, int ind) fold)
    {
        if (fold.Item1 == "x")
        {
            var splitMap = new bool[2][][];
            foreach (var i in Enumerable.Range(0, 2))
            {
                splitMap[i] = new bool[map.Length][]; 
                foreach (var j in Enumerable.Range(0, map.Length))
                {
                    splitMap[i][j] = new bool[(map[0].Length - 1) / 2];
                }
            }

            foreach (var (l, i) in map.Select((l, i) => (l, i)))
            foreach (var (c, j) in l.Select((c, j) => (c, j)))
            {
                if (j == fold.ind) continue;
                var a = j > fold.ind ? 1 : 0;
                var b = (j - a) % fold.ind;
                splitMap[a][i][b] = c;
            }

            return splitMap[0].Zip(splitMap[1], (a, b) => a.Zip(b.Reverse(), (c, d) => c || d).ToArray()).ToArray();
        }
        else
        {
            var splitMap = new bool[2][][];
            splitMap[0] = new bool[(map.Length - 1) / 2][];
            splitMap[1] = new bool[(map.Length - 1) / 2][];

            foreach (var (l, i) in map.Select((l, i) => (l, i)))
            {
                if (i == fold.ind) continue;
                var a = i > fold.ind ? 1 : 0;
                var b = (i - a) % fold.ind;
                splitMap[a][b] = l;
            }

            return splitMap[0].Zip(splitMap[1].Reverse(), (a, b) => a.Zip(b, (c, d) => c || d).ToArray()).ToArray();
        }
    }

    public int Part1Answer()
    {
        tmpMap = DoFold(unfoldedDotMap, folds[0]);
        return tmpMap.Flatten().OfType<bool>().Count(x => x);
    }
    
    public string Part2Answer()
    {
        var map = unfoldedDotMap;
        foreach (var fold in folds)
        {
            map = DoFold(map, fold);
        }

        var outString = "";
        foreach (var l in map)
        {
            var tmpString = "";
            foreach (var c in l)
            {
                tmpString += c ? "# " : ". ";
            }

            outString += tmpString + "\n";
        }

        return outString;
    }
}