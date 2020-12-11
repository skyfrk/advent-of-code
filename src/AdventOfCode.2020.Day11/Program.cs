using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

void SolvePart1()
{
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

                if (input[row][column] == 'L' && numberOfOccupiedAdjacentSeats == 0)
                {
                    changes.Add((row, column, '#'));
                    somethingChanged = true;
                    continue;
                }

                if (input[row][column] == '#' && numberOfOccupiedAdjacentSeats >= 4)
                {
                    changes.Add((row, column, 'L'));
                    somethingChanged = true;
                }
            }
        }

        foreach (var (row, column, value) in changes)
        {
            input[row][column] = value;
        }
    }

    var count = 0;
    foreach (var row in input)
    {
        foreach (var seat in row)
        {
            if (seat == '#') count++;
        }
    }

    Console.WriteLine($"Part 1: {count}");

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
}

void SolvePart2()
{
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

                var occupiedSeatsInSightCount = GetNumberOfOccupiedSeatsInSight(row, column);

                if (input[row][column] == 'L' && occupiedSeatsInSightCount == 0)
                {
                    changes.Add((row, column, '#'));
                    somethingChanged = true;
                    continue;
                }

                if (input[row][column] == '#' && occupiedSeatsInSightCount >= 5)
                {
                    changes.Add((row, column, 'L'));
                    somethingChanged = true;
                }
            }
        }

        foreach (var (row, column, value) in changes)
        {
            input[row][column] = value;
        }

    }

    var count = 0;
    foreach (var row in input)
    {
        foreach (var seat in row)
        {
            if (seat == '#') count++;
        }
    }

    Console.WriteLine($"Part 2: {count}");

    uint GetNumberOfOccupiedSeatsInSight(int startRow, int startColumn)
    {
        uint count = 0;

        var directionPatterns = new List<(int RowOffset, int ColumnOffset)>
        {
            (1,0),
            (0,1),
            (-1, 0),
            (0, -1),
            (-1, 1),
            (1, -1),
            (1, 1),
            (-1, -1),
        };

        foreach (var (rowOffset, columnOffset) in directionPatterns)
        {
            var row = startRow + rowOffset;
            var column = startColumn + columnOffset;

            while (row >= 0 && column >= 0 && row < input.Length && column < input[0].Length)
            {
                if (input[row][column] == '#')
                {
                    count++;
                    break;
                }

                if (input[row][column] == 'L') break;

                row += rowOffset;
                column += columnOffset;
            }
        }

        return count;
    }
}

SolvePart1();
SolvePart2();