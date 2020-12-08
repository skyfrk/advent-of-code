using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var instructionRegex = new Regex(@"^(nop|acc|jmp){1} ([\+|-]{1}\d+)$");

var instructions = input
    .Select(line =>
    {
        var match = instructionRegex.Match(line);
        var operation = match.Groups[1].Value;
        var value = int.Parse(match.Groups[2].Value);
        return (Operation: operation, Value: value, ExecutedCount: 0);
    })
    .ToArray();

var accumulator = 0;

for (int i = 0; i < instructions.Length; i++)
{
    var (operation, value, executedCount) = instructions[i];

    if (executedCount > 0) break;

    instructions[i].ExecutedCount++;

    if (operation == "jmp")
    {
        i += value - 1;
    }

    if (operation == "acc")
    {
        accumulator += value;
    }
}

Console.WriteLine($"Part 1: {accumulator}");