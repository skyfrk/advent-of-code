using System;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("input.txt").Select(line => (action: line[0], value: int.Parse(line[1..])));

uint orientation = 90; // east
int posNorthSouth = 0;
int posEastWest = 0;

foreach (var (action, value) in input)
{
    Console.WriteLine($"Orientation: {orientation} NorthSouth: {posNorthSouth} EastWest: {posEastWest} Action: {action}{value}");

    if (action == 'N')
    {
        posNorthSouth += value;
        continue;
    }

    if (action == 'S')
    {
        posNorthSouth -= value;
        continue;
    }

    if (action == 'E')
    {
        posEastWest += value;
        continue;
    }

    if (action == 'W')
    {
        posEastWest -= value;
        continue;
    }

    if (action == 'R' || action == 'L')
    {
        // Console.WriteLine($"Orientation before: {orientation} Action: {action}{value}");
        orientation = GetOrientation(orientation, value, action);
        // Console.WriteLine($"Orientation after: {orientation}");
        continue;
    }

    if (action == 'F')
    {
        if (orientation == 0) posNorthSouth += value;
        if (orientation == 90) posEastWest += value;
        if (orientation == 180) posNorthSouth -= value;
        if (orientation == 270) posEastWest -= value;
    }
}

Console.WriteLine($"Orientation: {orientation} NorthSouth: {posNorthSouth} EastWest: {posEastWest}");

Console.WriteLine($"Part 1: {Math.Abs(posNorthSouth) + Math.Abs(posEastWest)}");

static uint GetOrientation(uint currentOrientation, int turnValue, char turnDirection)
{
    if (turnValue == 0) return currentOrientation;

    if (turnDirection == 'R')
    {
        return (currentOrientation + (uint)turnValue) % 360;
    }
    else
    {
        return (360 + (currentOrientation - (uint)turnValue)) % 360;
    }
}