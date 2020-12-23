using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var cups = File.ReadAllLines("input.txt")[0].Select(c => int.Parse(c.ToString())).ToList();

var maxCupLabel = cups.Max();
var minCupLabel = cups.Min();

var currentCup = cups[0];

for(int i = 0; i < 100; i++)
{
    var nextThree = Enumerable.Range(0, 3).Select(x => GetNextAfter(cups, cups.IndexOf(currentCup), true)).ToArray();

    var destinationCup = GetDestinationCup(cups, currentCup, nextThree, maxCupLabel, minCupLabel);

    var destinationCupIdx = cups.IndexOf(destinationCup);

    cups.InsertRange(destinationCupIdx + 1, nextThree);

    currentCup = GetNextAfter(cups, cups.IndexOf(currentCup));
}

var solution = string.Empty;

var nextIdx = cups.IndexOf(1);

for(int i = 0; i < cups.Count - 1; i++)
{
    var next = GetNextAfter(cups, nextIdx, false);
    nextIdx = cups.IndexOf(next);
    solution += $"{next}";
}

Console.WriteLine($"Part 1: {solution}");

int GetDestinationCup(List<int> cups, int currentCup, int[] nextThree, int maxCupLabel, int minCupLabel)
{
    var destinationCup = currentCup;

    do
    {
        destinationCup--;

        if (destinationCup < minCupLabel) destinationCup += maxCupLabel;
    }
    while (nextThree.Contains(destinationCup));

    return destinationCup;
}

int GetNextAfter(List<int> cups, int idx, bool remove = false)
{
    var nextIdx = (idx + 1) % cups.Count;
    var nextItem = cups[nextIdx];
    if(remove) cups.RemoveAt(nextIdx);
    return nextItem;
}
