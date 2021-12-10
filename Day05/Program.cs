// See https://aka.ms/new-console-template for more information

using System.Drawing;
using MoreLinq;
using static System.IO.File;

namespace Day05;

public class Program
{
    public static void Main(string[] args)
    {
        var logic = new VentLogic();
        logic.LoadInput(ReadLines(@"./input.txt").ToArray());
        Console.WriteLine(logic.Part1Answer());
        Console.WriteLine(logic.Part2Answer());
    }
}

public class VentLogic
{
    private int[,]? map;
    private readonly List<Tuple<Point, Point>> ventLines = new();
    private bool vertHoriDone;
    private bool diagDone;

    public void LoadInput(string[] inp)
    {
        var (minX, maxX, minY, maxY) = (int.MaxValue, 0, int.MaxValue, 0);
        foreach (var line in inp)
        {
            var coords = line.Split(" -> ")
                .Select(x => x.Split(',')
                    .Select(int.Parse))
                .Flatten()
                .OfType<int>()
                .ToArray();
            ventLines.Add(new Tuple<Point, Point>(new Point(coords[0], coords[1]), new Point(coords[2], coords[3])));
            if (coords[0] > maxX) maxX = coords[0];
            else if (coords[0] < minX) minX = coords[0];
            if (coords[1] > maxY) maxY = coords[1];
            else if (coords[1] < minY) minY = coords[1];
            if (coords[2] > maxX) maxX = coords[2];
            else if (coords[2] < minX) minX = coords[2];
            if (coords[3] > maxY) maxY = coords[3];
            else if (coords[3] < minY) minY = coords[3];
        }

        map = new int[maxX + 1, maxY + 1];
        foreach (var x in Enumerable.Range(minX, maxX - minX))
        foreach (var y in Enumerable.Range(minY, maxY - minY))
            map[x, y] = 0;
    }

    private void MapVertHori()
    {
        var horizontalLines = ventLines.Where(t => t.Item1.Y == t.Item2.Y);
        foreach (var (item1, item2) in horizontalLines)
        {
            var (xPoint1, xPoint2) = item1.X > item2.X ? (item2.X, item1.X) : (item1.X, item2.X);
            foreach (var xPoint in Enumerable.Range(xPoint1, (xPoint2 - xPoint1)+1)) map![xPoint, item1.Y]++;
        }

        var verticalLines = ventLines.Where(t => t.Item1.X == t.Item2.X);
        foreach (var (item1, item2) in verticalLines)
        {
            var (yPoint1, yPoint2) = item1.Y > item2.Y ? (item2.Y, item1.Y) : (item1.Y, item2.Y);
            foreach (var yPoint in Enumerable.Range(yPoint1, (yPoint2 - yPoint1)+1)) map![item1.X, yPoint]++;
        }

        vertHoriDone = true;
    }

    private void MapDiag()
    {
        var diagLines = ventLines.Where(t => !(t.Item1.Y == t.Item2.Y || t.Item1.X == t.Item2.X));
        foreach (var (item1, item2) in diagLines)
        {
            var diff = item2 - (Size) item1;
            var step = new Size(1, 1);
            if (diff.X > 0 && diff.Y < 0) step = new Size(1, -1);
            else if (diff.X < 0 && diff.Y < 0) step = new Size(-1, -1);
            else if (diff.X < 0 && diff.Y > 0) step = new Size(-1, 1);
            for (var tmp = item1; tmp != item2 + step; tmp += step)
            {
                map![tmp.X, tmp.Y]++;
            }
        }
    }

    public int Part1Answer()
    {
        if (!vertHoriDone) MapVertHori();
        return map.Flatten().OfType<int>().Count(x => x > 1);
    }

    public int Part2Answer()
    {
        if (!vertHoriDone) MapVertHori();
        if (!diagDone) MapDiag();
        return map.Flatten().OfType<int>().Count(x => x > 1);
    }
}