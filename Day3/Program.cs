// See https://aka.ms/new-console-template for more information

using Day3;
using static System.IO.File;

var input = ReadLines(@"./input.txt").Select(x => x.ToCharArray().Select(y => y == '1').ToArray()).ToArray();
var rowCount = input.Length;
var colCount = input[0].Length;

var rotInput = Enumerable.Range(0, colCount)
    .Select(x => Enumerable.Range(0, rowCount).Select(y => input[y][x]).ToArray()).ToArray();
var p1GammaStr = "";
foreach (var col in rotInput)
{
    var onCount = col.Count(read => read);
    p1GammaStr += onCount > rowCount - onCount ? "1" : "0";
}

var p1Gamma = Convert.ToInt32(p1GammaStr, 2);
var p1Epsilon = p1Gamma ^ 0b111111111111;

Console.WriteLine($"Part 1: {p1Gamma * p1Epsilon}");

Console.WriteLine($"Part 2: {LifeSupport.GetRating(input, true) * LifeSupport.GetRating(input, false)}");

namespace Day3
{
    public class LifeSupport
    {
        public static int GetRating(bool[][] input, bool mostCommon)
        {
            var cullInput = input;
            foreach (var i in Enumerable.Range(0, input.Length))
            {
                var rotInput = Enumerable.Range(0, cullInput[0].Length).Select(x =>
                    Enumerable.Range(0, cullInput.Length).Select(y => cullInput[y][x]).ToArray()).ToArray();
                var mostCommonCount = rotInput[i].Count(x => x);
                var state = mostCommonCount > cullInput.Length - mostCommonCount;
                var search = mostCommon ? state : !state;
                if (mostCommonCount * 2 == cullInput.Length) search = mostCommon;

                cullInput = cullInput.Where(x => x[i] == search).ToArray();
                if (cullInput.Length == 1) break;
            }

            var val = 0;
            foreach (var (flag, i) in cullInput[0].Select((f, i) => (f, i)))
                val |= (flag ? (int) Math.Pow(2, cullInput[0].Length - 1) : 0) >> i;

            return val;
        }
    }
}