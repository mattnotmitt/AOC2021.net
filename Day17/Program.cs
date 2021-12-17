// See https://aka.ms/new-console-template for more information
namespace Day17;

public record Bound(int min, int max);

public class Program
{
    public static void Main(string[] args)
    {
        var logic = new ProbeLogic("target area: x=201..230, y=-99..-65");
        Console.WriteLine(logic.Part1Answer());
        Console.WriteLine(logic.Part2Answer());
    }
}

public class ProbeLogic
{
    private (Bound x, Bound y) target;

    private (Bound x, Bound y) GetTarget(string inp)
    {
        var preRangeSplit = inp[13..];
        var ranges = preRangeSplit.Split(", ");
        var xRange = ranges[0][2..].Split("..");
        var yRange = ranges[1][2..].Split("..");
        return (x: new Bound(int.Parse(xRange[0]), int.Parse(xRange[1])),
            y: new Bound(int.Parse(yRange[0]), int.Parse(yRange[1])));
    }
    
    public ProbeLogic(string inp)
    {
        target = GetTarget(inp);
    }
    
    private record struct ProbeState ((int x, int y) vel, (int x, int y) pos);

    private ProbeState DoProbeStep(ProbeState ps)
    {
        var nps = new ProbeState(ps.vel, ps.pos);
        // Increase x pos by vel
        // Increase y pos by vel
        nps.pos = nps.pos with
        {
            x = nps.pos.x + nps.vel.x,
            y = nps.pos.y + nps.vel.y
        };
        // Decrease x vel by 1
        nps.vel = nps.vel with
        {
            x = nps.vel.x - Math.Clamp(nps.vel.x, -1, 1),
            y = nps.vel.y - 1
        };
        // Decrease y vel by 1
        return nps;
    }

    private bool CanReachTarget(ProbeState ps)
    {
        return ps.pos.x <= target.x.max && ps.pos.y >= target.y.min && (ps.vel.x != 0 || ps.pos.x >= target.x.min);
    }

    private bool InTarget(ProbeState ps)
    {
        return ps.pos.x >= target.x.min && ps.pos.x <= target.x.max && ps.pos.y >= target.y.min &&
               ps.pos.y <= target.y.max;
    }
    
    /// <returns>Maximum height above the origin achieved, or -1 if does not reach target area</returns>
    private int MaxHeightOfArc((int x, int y) vel)
    {
        var maxY = 0;
        var ps = new ProbeState(vel, (0, 0));
        while (CanReachTarget(ps))
        {
            ps = DoProbeStep(ps);
            if (ps.pos.y > maxY) maxY = ps.pos.y;
            if (InTarget(ps)) return maxY;
        }
        return -1;
    }

    public int Part1Answer()
    {
        var maxHeights = new List<int>();
        foreach (var x in Enumerable.Range(0, target.x.max/2))
        foreach (var y in Enumerable.Range(0, target.x.max/2))
        {
            maxHeights.Add(MaxHeightOfArc((x, y)));
        }

        return maxHeights.Max();
    }

    public int Part2Answer()
    {
        var maxHeights = new List<int>();
        foreach (var x in Enumerable.Range(0, target.x.max*2))
        foreach (var y in Enumerable.Range(-target.x.max/2, target.x.max))
        {
            var res = MaxHeightOfArc((x, y));
            if (res > -1) maxHeights.Add(res);
        }
        return maxHeights.Count;
    }
}