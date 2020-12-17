using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadAllLines("sample.input.txt");

var currentState = new List<Cube>();

for(int row = 0; row < input.Length; row++)
{
    for(int column = 0; column < input[0].Length; column++)
    {
        currentState.Add(new Cube(IsActive(input[row][column]), column, row, 1));
    }
}

for (int iteration = 0; iteration < 6; iteration++)
{
    var newState = new List<Cube>();

    // TODO get top left and lower right corner of the current cube and add inactive cubes in the new layer around the cube
}


Console.WriteLine($"Part 1: {currentState.Count(c => c.State == CubeState.Active)}");

CubeState IsActive(char v) => v switch
{
    '#' => CubeState.Active,
    '.' => CubeState.Inactive,
    _ => throw new InvalidOperationException($"Invalid state: {v}")
};

record Cube(CubeState State, long X, long Y, long Z)
{
    public Cube AdjustStateTo(IEnumerable<Cube> currentState)
    {
        var neighbors = GetNeighbors(currentState);

        if (State == CubeState.Active)
        {
            var activeNeighborCount = neighbors.Count(n => n.State == CubeState.Active);
            if(activeNeighborCount is 2 or 3)
            {
                return this;
            } else
            {
                return new Cube(CubeState.Inactive, X, Y, Z);
            }
        }
        else if (State == CubeState.Inactive)
        {
            // TODO
        }

        throw new InvalidOperationException();
    }

    IEnumerable<Cube> GetNeighbors(IEnumerable<Cube> currentState)
    {
        return currentState.Where(c =>
            c.X >= X - 1 && c.X <= X + 1 &&
            c.Y >= Y - 1 && c.Y <= Y + 1 &&
            c.Z >= Z - 1 && c.Z <= Z + 1
        );
    }
}

enum CubeState
{
    Active,
    Inactive
}