using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt").Select(uint.Parse);

void SolvePart1()
{
    var deviceJoltage = input.Max() + 3;
    var allDevices = input.OrderBy(d => d).Append(deviceJoltage);

    uint prev = 0;
    List<uint> diffs = new();
    foreach (var device in allDevices)
    {
        uint diff = device - prev;
        prev = device;
        if (diff > 3) throw new InvalidOperationException();
        diffs.Add(diff);
    }

    var oneDiffCount = diffs.Where(d => d == 1).Count();
    var threeDiffCount = diffs.Where(d => d == 3).Count();

    Console.WriteLine($"Part 1: {oneDiffCount * threeDiffCount}");
}

void SolvePart2WithUnlimitedRAMAndTimeLol()
{
    var allDevices = input.ToList();
    allDevices.Add(0);
    allDevices.Add(allDevices.Max() + 3);
    var allDevicesSorted = allDevices.OrderBy(d => d).ToArray();

    var solutions = GetRemainingSolutions(new(), allDevicesSorted);

    Console.WriteLine($"Part 2: {solutions.Count}");

    List<List<uint>> GetRemainingSolutions(List<uint> solution, uint[] remainingDevices)
    {
        if (remainingDevices.Length == 0) return new List<List<uint>> { solution };

        solution.Add(remainingDevices.First());

        List<List<uint>> solutions = new();

        for (int i = 1; i < remainingDevices.Length; i++)
        {
            if (remainingDevices[i] - remainingDevices[0] <= 3)
            {
                var remainingSolutions = GetRemainingSolutions(solution, remainingDevices[(i + 1)..]);
                solutions.AddRange(remainingSolutions);
            }
        }

        return solutions;
    }
}

void SolvePart2()
{
    var allDevices = input.ToList();
    allDevices.Add(0);
    allDevices.Add(allDevices.Max() + 3);
    var allDevicesSorted = allDevices.OrderBy(d => d).ToArray();

    var possibleSolutionCounts = new long[allDevicesSorted.Length];

    for (int i = 0; i < possibleSolutionCounts.Length; i++)
    {
        if (i == 0) possibleSolutionCounts[i] = 1;
        else
        {
            possibleSolutionCounts[i] = 0;

            var lookBackOffset = 1;
            while(i - lookBackOffset >= 0 && allDevicesSorted[i] - allDevicesSorted[i - lookBackOffset] <= 3)
            {
                possibleSolutionCounts[i] += possibleSolutionCounts[i - lookBackOffset];
                lookBackOffset++;
            }
        }
    }

    Console.WriteLine($"Part 2: {possibleSolutionCounts[possibleSolutionCounts.Length - 1]}");
}

SolvePart1();
SolvePart2();