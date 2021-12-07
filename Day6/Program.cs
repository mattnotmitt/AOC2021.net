// See https://aka.ms/new-console-template for more information

using System.Numerics;
using static System.IO.File;

namespace Day6;

public class Program
{
    public static void Main(string[] args)
    {
        var logic = new LanternfishLogic();
        logic.LoadInput(ReadAllText(@"./input.txt"));
        Console.WriteLine(logic.Part1Answer());
        Console.WriteLine(logic.Part2Answer());
    } 
}

public class LanternfishLogic
{
    private List<long> maturity =  Enumerable.Repeat(0L, 9).ToList();
    private List<long> baseMaturity =  Enumerable.Repeat(0L, 9).ToList();

    public void LoadInput(string inp)
    {
        var splitInp = inp.Split(",").Select(int.Parse).ToList();
        for (var i = 0; i <= 8; i++)
        {
            baseMaturity[i] += splitInp.Count(x => x == i);
        }
    }

    private void SimulateGrowth(int days)
    {
        for (var day = 0; day < days; day++)
        {
            maturity[7] += maturity[0];
            maturity.Add(maturity[0]);
            maturity.RemoveAt(0);
        }
    }

    public long Part1Answer()
    {
        maturity = new List<long>(baseMaturity);
        SimulateGrowth(80);
        return maturity.Sum();
    }

    public long Part2Answer()
    {
        maturity = new List<long>(baseMaturity);
        SimulateGrowth(256);
        return maturity.Sum();
    }
}