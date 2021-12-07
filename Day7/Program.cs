// See https://aka.ms/new-console-template for more information
namespace Day7;

using static System.IO.File;

public class Program
{
    public static void Main(string[] args)
    {
        var logic = new CrabLogic();
        logic.LoadInputs(ReadAllText(@"./input.txt"));
        Console.WriteLine(logic.Part1Answer());
        Console.WriteLine(logic.Part2Answer());
    }
}

public class CrabLogic
{
    private List<int> baseCrabMariners;
    public void LoadInputs(string inp)
    {
        baseCrabMariners = inp.Split(",").Select(int.Parse).ToList();
    }

    public long Part1Answer()
    {
        var crabMariners = new List<int>(baseCrabMariners);
        crabMariners.Sort();
        var median = crabMariners[(int) Math.Floor(crabMariners.Count / 2F)];
        return crabMariners.Sum(crab => Math.Abs(crab - median));
    }

    public long Part2Answer()
    {
        var crabMariners = new List<int>(baseCrabMariners);
        crabMariners.Sort();
        var minFuelUsed = int.MaxValue;
        // It's gonna be in the middle third.
        for (var pos = crabMariners[0]; pos < crabMariners.Last(); pos++)
        {
            var fuelUsed = 0;
            foreach (var crab in crabMariners)
            {
                var diff = Math.Abs(pos - crab);
                fuelUsed += (int) Math.Ceiling((diff/2F) * (1+diff));
            }

            if (fuelUsed < minFuelUsed) minFuelUsed = fuelUsed;
        }

        return minFuelUsed;
    }
}

