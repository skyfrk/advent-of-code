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

SolvePart1();