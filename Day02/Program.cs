// See https://aka.ms/new-console-template for more information

using System.Numerics;
using static System.IO.File;

var data = ReadLines(@"./input.txt").ToArray();
var badPos = data.Aggregate(Vector2.Zero, (current, inst) => current + Movement.DecodeInstructions(inst));

Console.WriteLine($"Naive Result: {badPos.X * badPos.Y}");

var mvmt = new Movement();
var pos = data.Aggregate(Vector2.Zero, (current, inst) => current + mvmt.DecodeInstructionsP2(inst));

Console.WriteLine($"Result: {(long) pos.X * (long) pos.Y}");

public class Movement
{
    private int aim;

    public Movement()
    {
        aim = 0;
    }

    public static Vector2 DecodeInstructions(string inst)
    {
        var result = Vector2.Zero;
        var splitInst = inst.Split(" ");
        switch (splitInst[0])
        {
            case "forward":
                result.X += int.Parse(splitInst[1]);
                break;
            case "up":
                result.Y -= int.Parse(splitInst[1]);
                break;
            case "down":
                result.Y += int.Parse(splitInst[1]);
                break;
        }

        return result;
    }

    public Vector2 DecodeInstructionsP2(string inst)
    {
        var result = Vector2.Zero;
        var splitInst = inst.Split(" ");
        switch (splitInst[0])
        {
            case "forward":
                result.X += int.Parse(splitInst[1]);
                result.Y += int.Parse(splitInst[1]) * aim;
                break;
            case "up":
                aim -= int.Parse(splitInst[1]);
                break;
            case "down":
                aim += int.Parse(splitInst[1]);
                break;
        }

        return result;
    }
}