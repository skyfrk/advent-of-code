using System;
using System.IO;
using System.Linq;
using System.Numerics;

var input = File.ReadAllLines("input.txt").Select(line => (action: line[0], value: int.Parse(line[1..])));

void SolvePart1()
{
    uint orientation = 90; // east
    int posNorthSouth = 0;
    int posEastWest = 0;

    foreach (var (action, value) in input)
    {
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
            orientation = GetOrientation(orientation, value, action);
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
}

void SolvePart2()
{
    // x-axis: east west
    // y-axis: north south
    var waypointPosition = (x: 10, y: 1);
    var shipPosition = (x: 0, y: 0);

    foreach (var (action, value) in input)
    {
        switch (action)
        {
            case 'N':
                waypointPosition.y += value;
                break;
            case 'S':
                waypointPosition.y -= value;
                break;
            case 'E':
                waypointPosition.x += value;
                break;
            case 'W':
                waypointPosition.x -= value;
                break;
            case 'R':
                RotateWaypoint(value);
                break;
            case 'L':
                RotateWaypoint(-value);
                break;
            case 'F':
                shipPosition.x += value * waypointPosition.x;
                shipPosition.y += value * waypointPosition.y;
                break;
            default:
                throw new InvalidOperationException();
        };
    }

    Console.WriteLine($"Part 2: {Math.Abs(shipPosition.x) + Math.Abs(shipPosition.y)}");

    void RotateWaypoint(int value)
    {
        for (int i = 0; i < Math.Abs(value / 90); i++)
        {
            if (value < 0)
            {
                var oldX = waypointPosition.x;
                waypointPosition.x = -waypointPosition.y;
                waypointPosition.y = oldX;
            } 
            else
            {
                var oldX = waypointPosition.x;
                waypointPosition.x = waypointPosition.y;
                waypointPosition.y = -oldX;
            }
        }
    }
}

SolvePart1();
SolvePart2();
