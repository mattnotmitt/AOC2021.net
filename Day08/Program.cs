// See https://aka.ms/new-console-template for more information

namespace Day08;

using static File;

public class Program
{
    public static void Main(string[] args)
    {
        var logic = new SegmentLogic();
        logic.LoadInput(ReadAllLines(@"./input.txt"));
        Console.WriteLine(logic.Part1Answer());
        Console.WriteLine(logic.Part2Answer());
    }
}

public class SegmentLogic
{
    public List<SevSegDisp> displays = new();
    private bool segsIdentified;

    public void LoadInput(string[] inp)
    {
        foreach (var sepLine in inp.Select(x => x.Split(" | ").Select(y => y.Split(" ").ToArray()).ToArray()))
        {
            var disp = new SevSegDisp();
            foreach (var patt in sepLine[0])
            {
                disp.unorderedPatt.Add(patt);
                switch (patt.Length)
                {
                    case 2:
                        disp.identPatt[1] = patt;
                        break;
                    case 3:
                        disp.identPatt[7] = patt;
                        break;
                    case 4:
                        disp.identPatt[4] = patt;
                        break;
                    case 7:
                        disp.identPatt[8] = patt;
                        break;
                }
            }

            disp.outputPatt = sepLine[1];
            displays.Add(disp);
        }
    }

    public int Part1Answer()
    {
        return displays.Select(
            d => d.outputPatt.Count(
                p => d.identPatt.Any(
                    i => i != null && i.Length == p.Length &&
                         i.ToCharArray().Intersect(p.ToCharArray()).ToList().Count == i.Length
                )
            )
        ).Sum();
    }

    public void IdentSegments()
    {
        var newDisplays = new List<SevSegDisp>();
        foreach (var (d, i) in displays.Select((d, i) => (d, i)))
        {
            var orderedPatt = d.unorderedPatt.ToArray();
            // order should now be 1, 7, 4, [2,3,5], [0,6,9], 8
            Array.Sort(orderedPatt, (a, b) => a.Length.CompareTo(b.Length));
            // Start with the 6 length numbers
            foreach (var p in orderedPatt.Skip(6).SkipLast(1))
            {
                var normC = d.identPatt[1].Except(p).ToArray();
                var normD = d.identPatt[4].Except(p).ToArray();
                if (normC.Length == 1)
                {
                    d.identPatt[6] = p;
                    d.seg[2] = normC[0];
                    d.seg[5] = d.identPatt[1].Except(normC).ToArray()[0];
                }
                else if (normD.Length == 1)
                {
                    d.identPatt[0] = p;
                    d.seg[3] = normD[0];
                    d.seg[1] = d.identPatt[4].Except(normD).Except(d.identPatt[1]).ToArray()[0];
                }
                else
                {
                    d.identPatt[9] = p;
                    d.seg[4] = "abcdefg".Except(p).ToArray()[0];
                }
            }

            // Fill in missing segments
            d.seg[0] = d.identPatt[7].Except(d.seg).ToArray()[0];
            d.seg[6] = d.identPatt[9].Except(d.seg).ToArray()[0];
            // Fill in missing patterns
            d.identPatt[2] = $"{d.seg[0]}{d.seg[2]}{d.seg[3]}{d.seg[4]}{d.seg[6]}";
            d.identPatt[3] = $"{d.seg[0]}{d.seg[2]}{d.seg[3]}{d.seg[5]}{d.seg[6]}";
            d.identPatt[5] = $"{d.seg[0]}{d.seg[1]}{d.seg[3]}{d.seg[5]}{d.seg[6]}";
            newDisplays.Add(d);
        }

        displays = newDisplays;
        segsIdentified = true;
    }

    public int Part2Answer()
    {
        if (!segsIdentified) IdentSegments();

        var readings = displays.Select(
            d => d.outputPatt.Select((p, i) =>
            {
                foreach (var (ip, j) in d.identPatt.Select((ip, j) => (ip, j)))
                    if (ip != null && ip.Length == p.Length &&
                        ip.ToCharArray().Intersect(p.ToCharArray()).ToList().Count == ip.Length)
                        return (int) (j * Math.Pow(10, 3 - i));
                return 0;
            }).Sum()
        );
        return readings.Sum();
    }

    public struct SevSegDisp
    {
        public List<string> unorderedPatt = new();
        public string[] identPatt = new string[10];
        public string[] outputPatt = new string[4];
        public char[] seg = new char[7];
    }
}