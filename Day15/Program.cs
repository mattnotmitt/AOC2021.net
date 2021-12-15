// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;

namespace Day15;

using System.Drawing;
using MapT = Dictionary<System.Drawing.Point, int>;
using static File;

public class Program
{
    public static void Main(string[] args)
    {
        var logic = new ChitonLogic(ReadAllLines(@"./input.txt"));
        Console.WriteLine(logic.Part1Answer());
        Console.WriteLine(logic.Part2Answer());
    }
}


public class ChitonLogic
{
    private MapT caveMap;

    private MapT LoadData(string[] inp)
    {
        return new MapT(
            from y in Enumerable.Range(0, inp[0].Length)
            from x in Enumerable.Range(0, inp.Length)
            select new KeyValuePair<Point, int>(new Point(x, y), inp[y][x] - '0'));
    }
    
    public ChitonLogic(string[] inp)
    {
        caveMap = LoadData(inp);
    }

    private Point[] Neighbours(Point pt, (int x, int y) max)
    {
        return new[]
        {
            pt with {X = pt.X - 1},
            pt with {X = pt.X + 1},
            pt with {Y = pt.Y - 1},
            pt with {Y = pt.Y + 1}
        }.Where(p => p.X >= 0 && p.X <= max.x && p.Y >= 0 && p.Y <= max.y).ToArray();
    }

    private (int x, int y) MapMax(MapT map)
    {
        return (x: map.Keys.MaxBy(p => p.X).X, y: map.Keys.MaxBy(p => p.Y).Y);
    }

    /**
     * I promise this is not dijkstra
     */
    private int ShortestPath(MapT map)
    {
        var origin = new Point(0, 0);
        var max = MapMax(map);
        var end = new Point(max.x, max.y);
        var visited = new HashSet<Point>
        {
            origin
        };
        var totalMap = new MapT(
            from k in map.Keys 
            select new KeyValuePair<Point, int>(k, int.MaxValue))
        {
            [origin] = 0
        };
        var q = new PriorityQueue<Point, int>();
        q.Enqueue(origin, 0);
        
        while (q.Count > 0)
        {
            var p = q.Dequeue();
            if (p == end) break;
            foreach (var n in Neighbours(p, max).Where(n => !visited.Contains(n)))
            {
                var total = totalMap[p] + map[n];
                var oldTotal = totalMap[n];
                if (total < oldTotal)
                {
                    totalMap[n] = total;
                    q.Enqueue(n, total);
                }
            }

            visited.Add(p);
        }
        return totalMap[end];
    }

    public int Part1Answer()
    {
        return ShortestPath(caveMap);
    }

    private MapT ExpandedMap(MapT map)
    {
        var max = MapMax(map);
        return new MapT(
            from x in Enumerable.Range(0, (max.x + 1)*5)
            from y in Enumerable.Range(0, (max.y + 1)*5)
            let lx = x % (max.x + 1)
            let ly = y % (max.y + 1)
                
            let initRisk = map[new Point(lx, ly)]
            let dist = (int) (Math.Floor(y/(float) (max.y + 1)) + Math.Floor(x/(float) (max.x + 1)))
            let risk = (initRisk + dist-1) % 9 + 1
            select new KeyValuePair<Point, int>(new Point(x, y), risk)
        );
    }

    public int Part2Answer()
    {
        return ShortestPath(ExpandedMap(caveMap));
    }
}