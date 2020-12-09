using System;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt").Select(line => long.Parse(line)).ToArray();

for (int i = 25; i < input.Length; i++)
{
    var prevNums = input[(i - 25)..i];
    if (prevNums.SelectMany(num => prevNums.Where(x => x != num).Select(num2 => num2 + num)).Contains(input[i])) continue;
    Console.WriteLine($"Part 1: {input[i]}");
    break;
}

for (int i = 0; i < input.Length; i++)
{
    long sum = input[i];
    for (int j = i + 1; j < input.Length; j++)
    {
        sum += input[j];
        if (sum == 1930745883)
        {
            var range = input[i..(j + 1)];
            Console.WriteLine($"Part 2: {range.Min() + range.Max()}");
            break;
        }
        if (sum > 1930745883) break;
    }
}