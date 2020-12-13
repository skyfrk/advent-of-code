using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

void SolvePart1()
{
    var earliestLeaveTimestamp = int.Parse(input[0]);
    var busIds = new Regex(@"(\d)+").Matches(input[1]).Select(m => int.Parse(m.Value));

    for (int currentTimestamp = earliestLeaveTimestamp; true; currentTimestamp++)
    {
        var foundSolution = false;

        foreach (var busId in busIds)
        {
            if (currentTimestamp % busId == 0)
            {
                var waitTime = currentTimestamp - earliestLeaveTimestamp;
                Console.WriteLine($"Part 1: {waitTime * busId}");
                foundSolution = true;
                break;
            }
        }
        if (foundSolution) break;
    }
}

void SolvePart2()
{
    var allBusIds = input[1].Split(',');

    List<(int id, int offset)> busIdsWithOffset = new ();

    for (int i = 0; i < allBusIds.Length; i++)
    {
        if (allBusIds[i] != "x")
        {
            busIdsWithOffset.Add((int.Parse(allBusIds[i]), i));
        }
    }

    long num = 1;
    long increment = 1;

    foreach (var (id, offset) in busIdsWithOffset)
    {
        while ((num + offset) % id != 0) num += increment;
        increment *= id;
    }

    Console.WriteLine($"Part 2: {num}");
}

SolvePart1();
SolvePart2();
