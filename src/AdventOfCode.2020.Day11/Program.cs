using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt").Select(s => s.ToCharArray()).ToArray();

var somethingChanged = true;

while (somethingChanged)
{
    somethingChanged = false;
    List<(int Row, int Column, char Value)> changes = new();

    for (int row = 0; row < input.Length; row++)
    {
        for (int column = 0; column < input[0].Length; column++)
        {
            if (input[row][column] == '.') continue;

            var numberOfOccupiedAdjacentSeats = GetNumberOfOccupiedAdjacentSeats(row, column);

            if (input[row][column] == 'L')
            {
                if (numberOfOccupiedAdjacentSeats == 0)
                {
                    changes.Add((row, column, '#'));
                    somethingChanged = true;
                }
                continue;
            }

            if (input[row][column] == '#')
            {
                if (numberOfOccupiedAdjacentSeats >= 4)
                {
                    changes.Add((row, column, 'L'));
                    somethingChanged = true;
                }
            }
        }
    }


    foreach (var (row, column, value) in changes)
    {
        input[row][column] = value;
    }
}

Console.WriteLine($"Part 1: {GetTotalNumberOfOccupiedSeats()}");

int GetTotalNumberOfOccupiedSeats()
{
    var count = 0;
    foreach(var row in input)
    {
        foreach(var seat in row)
        {
            if (seat == '#') count++;
        }
    }
    return count;
}

uint GetNumberOfOccupiedAdjacentSeats(int row, int column)
{
    uint count = 0;
    if (IsOccupied(row + 1, column)) count++;
    if (IsOccupied(row - 1, column)) count++;
    if (IsOccupied(row, column + 1)) count++;
    if (IsOccupied(row, column - 1)) count++;
    if (IsOccupied(row + 1, column + 1)) count++;
    if (IsOccupied(row - 1, column - 1)) count++;
    if (IsOccupied(row + 1, column - 1)) count++;
    if (IsOccupied(row - 1, column + 1)) count++;
    return count;
}

bool IsOccupied(int row, int column)
{
    if (row < 0 || column < 0 || row >= input.Length || column >= input[0].Length || input[row][column] == '.' || input[row][column] == 'L') return false;
    return true;
}