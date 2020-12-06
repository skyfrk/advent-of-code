using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt");

void SolvePart1()
{
    var currentStr = string.Empty;
    var totalSum = 0;

    foreach (var line in input)
    {
        if (line == string.Empty)
        {
            totalSum += currentStr.Distinct().Count();
            currentStr = string.Empty;
        }
        else
        {
            currentStr += line;
        }
    }

    totalSum += currentStr.Distinct().Count();

    Console.WriteLine($"Part 1: {totalSum}");
}

void SolvePart2()
{
    List<List<char>> currentGroup = new();

    var totalSum = 0;

    foreach (var line in input)
    {
        if (line == string.Empty)
        {
            totalSum += currentGroup.Aggregate((acc, x) => acc.Intersect(x).ToList()).Count;
            currentGroup = new();
        }
        else
        {
            currentGroup.Add(line.ToList());
        }
    }

    totalSum += currentGroup.Aggregate((acc, x) => acc.Intersect(x).ToList()).Count;

    Console.WriteLine($"Part 2: {totalSum}");
}


SolvePart1();
SolvePart2();