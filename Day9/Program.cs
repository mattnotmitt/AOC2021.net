// See https://aka.ms/new-console-template for more information

namespace Day9;

using static System.IO.File;

public class Program
{
    public static void Main(string[] args)
    {
        var logic = new SmokeLogic(ReadAllLines(@"./input.txt"));
        Console.WriteLine(logic.Part1Answer());
        Console.WriteLine(logic.Part2Answer());
    }
}

public class SmokeLogic
{
    private int[][] heightMap;
    // private int[][] rotHeightMap;

    private List<(int, int)> lowPoints = new List<(int, int)>();

    public SmokeLogic(string[] inp)
    {
        heightMap = LoadInput(inp);
        // rotHeightMap = Enumerable.Range(0, heightMap[0].Length)
        //     .Select(x => Enumerable.Range(0, heightMap.Length).Select(y => heightMap[y][x]).ToArray()).ToArray();
    }

    private int[][] LoadInput(string[] inp)
    {
        return inp.Select(x => x.ToCharArray().Select(x => (int) char.GetNumericValue(x)).ToArray()).ToArray();
    }

    private bool IsLow(int h, int x, int y)
    {
        return (x == 0 || heightMap[x - 1][y] > h) &&
               (y == 0 || heightMap[x][y - 1] > h) &&
               (x == heightMap.Length - 1 || heightMap[x + 1][y] > h) &&
               (y == heightMap[0].Length - 1 || heightMap[x][y + 1] > h);
    }

    private bool lowPointsFound = false;
    private int GetLowPoints()
    {
        var dangerSum = 0;
        foreach (var (r, x) in heightMap.Select((r, x) => (r, x)))
        foreach (var (h, y) in r.Select((h, y) => (h, y)))
        {
            if (!IsLow(h, x, y)) continue;
            lowPoints.Add((x, y));
            dangerSum += h + 1;
        }

        lowPointsFound = true;
        return dangerSum;
    }

    public int Part1Answer()
    {
        return GetLowPoints();
    }

    private Dictionary<(int,int), List<(int,int)>> basins;

    private void FindLowPoint(int h, int x, int y)
    {
        var lowPath = new List<(int, int)>(); 
        while (!lowPoints.Contains((x, y)))
        {
            lowPath.Add((x, y));
            var left = x == 0 ? (9, 0, 0) : (heightMap[x - 1][y], x-1, y);
            var right = x == heightMap.Length - 1 ? (9, 0, 0) : (heightMap[x + 1][y], x+1, y);
            var up = y == 0 ? (9, 0, 0) : (heightMap[x][y - 1], x, y-1);
            var down = y == heightMap[0].Length - 1 ? (9, 0, 0) : (heightMap[x][y + 1], x, y+1);
            var adjList = new[] {left, right, up, down};
            Array.Sort(adjList, (a, b) => a.Item1.CompareTo(b.Item1));
            if (adjList[0].Item1 < h) (h, x, y) = adjList[0];
        }

        basins[(x, y)].AddRange(lowPath);
        basins[(x, y)] = basins[(x, y)].Distinct().ToList();
    }
    
    public int Part2Answer()
    {
        if (!lowPointsFound) GetLowPoints();
        basins = lowPoints.ToDictionary(x => x, x => new List<(int, int)> {x});
        
        foreach (var (r, x) in heightMap.Select((r, x) => (r, x)))
        foreach (var (h, y) in r.Select((h, y) => (h, y)))
        {
            if (h == 9 || basins.Values.Any(k => k.Contains((x, y)))) continue;
            FindLowPoint(h, x, y);
        }

        var basinSizes = basins.Values.ToArray().Select(x => x.Count).ToArray();
        Array.Sort(basinSizes, (a, b) => b.CompareTo(a));
        return basinSizes[0] * basinSizes[1] * basinSizes[2];
    }
}