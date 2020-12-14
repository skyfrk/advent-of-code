using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var maskRegex = new Regex(@"mask = ([01X]+)");
var memRegex = new Regex(@"mem\[(\d+)\] = (\d+)");

string currentMask = null;
Dictionary<string, long> memory = new();

foreach (var line in input)
{
    if (maskRegex.IsMatch(line)) currentMask = maskRegex.Match(line).Groups[1].Value;

    if (memRegex.IsMatch(line))
    {
        var captureGroups = memRegex.Match(line).Groups;

        var address = captureGroups[1].Value;
        var value = uint.Parse(captureGroups[2].Value);

        var valueBitArray = Convert.ToString(value, 2).PadLeft(36, '0').ToArray();

        for(int i = 0; i < currentMask.Length; i++)
        {
            valueBitArray[i] = currentMask[i] == 'X' ? valueBitArray[i] : currentMask[i];
        }

        var maskedValue = Convert.ToInt64(new string(valueBitArray), 2);

        memory[address] = maskedValue;
    }
}

Console.WriteLine($"Part 1: {memory.Sum(x => x.Value)}");