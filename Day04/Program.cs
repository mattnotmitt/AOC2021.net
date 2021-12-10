// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using MoreLinq;
using static System.IO.File;

namespace Day04;
public class Program
{
    public static void Main(string[] args)
    {
        var logic = new BingoLogic();
        logic.LoadInput(ReadLines(@"./input.txt").ToArray());
        Console.WriteLine(logic.Part1Answer());
        Console.WriteLine(logic.Part2Answer());
    }
}

public class BingoLogic
{
    private readonly List<int[][]> boards = new();
    private int[] callouts = Array.Empty<int>();
    private readonly List<int> winOrder = new();
    private readonly List<int> winState = new();

    public void LoadInput(string[] input)
    {
        callouts = input[0].Split(",").Select(int.Parse).ToArray();
        // Loop over the rest of the input to get boards
        var board = new List<int[]>();
        foreach (var line in input.Skip(2).Select(x => x.ToCharArray().Batch(3).ToArray()).ToArray())
        {
            if (line.Length == 0)
            {
                boards.Add(board.ToArray());
                board = new List<int[]>();
                continue;
            }

            var boardLine = line.Select(entry => int.Parse(new string(entry.Slice(0, 2).ToArray()))).ToArray();
            board.Add(boardLine);
        }
    }

    private void CalcWinOrder()
    {
        var boardState = Enumerable.Range(0, boards.Count).Select(
            i => Enumerable.Range(0, 5).Select(
                j => Enumerable.Repeat(false, 5).ToArray()
            ).ToArray()
        ).ToArray();
        var flattenedBoards = boards.Select(x => x.Flatten().OfType<int>().ToArray()).ToArray();
        var culledBoards = new List<int[][]>(boards);
        foreach (var call in callouts)
        foreach (var (board, i) in flattenedBoards.Select((x, i) => (x, i)).Where(obj => !winOrder.Contains(obj.i)))
        {
            foreach (var (num, j) in board.Select((x, j) => (x, j)).Where(obj => obj.x == call))
            {
                var x = (int) Math.Floor(j / 5.0);
                Debug.Assert(x is >= 0 and < 5);
                var y = j % 5;
                Debug.Assert(y is >= 0 and < 5);
                boardState[i][x].SetValue(true, y);
                culledBoards[i][x].SetValue(0, y);
            }

            if (!IsWon(boardState[i]) || winOrder.Contains(i)) continue;
            winState.Add(culledBoards[i].Flatten().OfType<int>().Sum() * call);
            winOrder.Add(i);
        }
    }

    public int Part1Answer()
    {
        if (winState.Count == 0) CalcWinOrder();
        return winState.First();
    }

    public int Part2Answer()
    {
        if (winState.Count == 0) CalcWinOrder();
        return winState.Last();
    }

    private static bool IsWon(bool[][] board)
    {
        var rotBoard = Enumerable.Range(0, 5).Select(x => Enumerable.Range(0, 5).Select(y => board[y][x]).ToArray())
            .ToArray();
        return board.Any(line => line.Count(i => i) == 5) || rotBoard.Any(line => line.Count(i => i) == 5);
    }
}