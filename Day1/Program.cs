// See https://aka.ms/new-console-template for more information

using System;
using System.Linq;
using MoreLinq;
using static System.IO.File;

var input = ReadLines(@"./input.txt").Select(int.Parse).ToArray();
var lastReading = input[0];
var ctr = 0;

foreach (var reading in input.Skip(1))
{
    if (reading > lastReading) ctr++;
    lastReading = reading;
}  

Console.WriteLine($"Single Increases: {ctr}");

var windows = input.Window(3).Select(x => x.Sum()).ToArray();
var lastWindow = windows[0];
ctr = 0;

foreach (var window in windows.Skip(1))
{
    if (window > lastWindow) ctr++;
    lastWindow = window;
}

Console.WriteLine($"Window Increases: {ctr}");