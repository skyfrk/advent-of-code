using System;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt")[0].Split(',').Select(int.Parse).ToArray();

var targetNumber = 2020;
var spokenNumbers = input.ToList();

for(int i = input.Length; i < targetNumber; i++)
{
    var prevNum = spokenNumbers[i - 1];

    var lastIdx = spokenNumbers.SkipLast(1).ToList().LastIndexOf(prevNum);

    if(lastIdx == -1)
    {
        spokenNumbers.Add(0);
    }
    else
    {
        spokenNumbers.Add(i - 1 - lastIdx);
    }
}

Console.WriteLine($"Part 1: {spokenNumbers[targetNumber - 1]}");