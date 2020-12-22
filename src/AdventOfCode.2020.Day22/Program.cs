using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var input = File.ReadAllText("input.txt");

void SolvePart1()
{
    Queue<int> player1Queue = new();
    Queue<int> player2Queue = new();

    foreach (var num in input.Split("\r\n\r\n")[0].Replace("Player 1:\r\n", string.Empty).Split("\r\n").Select(int.Parse))
    {
        player1Queue.Enqueue(num);
    }

    foreach (var num in input.Split("\r\n\r\n")[1].Replace("Player 2:\r\n", string.Empty).Split("\r\n").Select(int.Parse))
    {
        player2Queue.Enqueue(num);
    }

    while (player1Queue.Count != 0 && player2Queue.Count != 0)
    {
        var player1num = player1Queue.Dequeue();
        var player2num = player2Queue.Dequeue();

        if (player1num > player2num)
        {
            player1Queue.Enqueue(player1num);
            player1Queue.Enqueue(player2num);
        }

        if (player1num < player2num)
        {
            player2Queue.Enqueue(player2num);
            player2Queue.Enqueue(player1num);
        }

        // discard the numbers if equal?
    }


    long score = -1;

    if (player2Queue.Count == 0)
    {
        score = GetScore(player1Queue);
    }

    if (player1Queue.Count == 0)
    {
        score = GetScore(player2Queue);
    }

    Console.WriteLine($"Part 1: {score}");
}

void SolvePart2()
{
    Queue<int> initialP1Deck = new();
    Queue<int> initialP2Deck = new();

    foreach (var num in input.Split("\r\n\r\n")[0].Replace("Player 1:\r\n", string.Empty).Split("\r\n").Select(int.Parse))
    {
        initialP1Deck.Enqueue(num);
    }

    foreach (var num in input.Split("\r\n\r\n")[1].Replace("Player 2:\r\n", string.Empty).Split("\r\n").Select(int.Parse))
    {
        initialP2Deck.Enqueue(num);
    }

    Result result = RecursiveGame(initialP1Deck, initialP2Deck);

    long score = -1;

    if(result == Result.Player1Victory)
    {
        score = GetScore(initialP1Deck);
    }

    if (result == Result.Player2Victory)
    {
        score = GetScore(initialP2Deck);
    }

    Console.WriteLine($"Part 2: {score}");

    Result RecursiveGame(Queue<int> p1Deck, Queue<int> p2Deck)
    {
        // dont remember the complete game state but each players hands...
        HashSet<int> deck1Memory = new();
        HashSet<int> deck2Memory = new();

        while (true)
        {
            var deck1hash = string.Join(',', p1Deck.ToArray().Select(c => c.ToString())).GetHashCode();
            var deck2hash = string.Join(',', p2Deck.ToArray().Select(c => c.ToString())).GetHashCode();

            if (deck1Memory.Contains(deck1hash) || deck2Memory.Contains(deck2hash))
            {
                return Result.Player1Victory;
            }

            deck1Memory.Add(deck1hash);
            deck2Memory.Add(deck2hash);

            var p1Card = p1Deck.Dequeue();
            var p2Card = p2Deck.Dequeue();

            Result result = default;

            if (p1Card <= p1Deck.Count && p2Card <= p2Deck.Count)
            {
                result = RecursiveGame(new Queue<int>(p1Deck.ToArray()[0..p1Card]), new Queue<int>(p2Deck.ToArray()[0..p2Card]));
            }
            else
            {
                if (p1Card > p2Card) result = Result.Player1Victory;
                if (p2Card > p1Card) result = Result.Player2Victory;
            }

            if (result == Result.Player1Victory)
            {
                p1Deck.Enqueue(p1Card);
                p1Deck.Enqueue(p2Card);
            }

            if (result == Result.Player2Victory)
            {
                p2Deck.Enqueue(p2Card);
                p2Deck.Enqueue(p1Card);
            }

            if(p1Deck.Count == 0 || p2Deck.Count == 0)
            {
                return result;
            }
        }

        throw new InvalidOperationException();
    }
}

long GetScore(Queue<int> deck)
{
    long score = 0;

    for (int pos = deck.Count; pos > 0; pos--)
    {
        score += pos * deck.Dequeue();
    }

    return score;
}

SolvePart1();
SolvePart2();

enum Result
{
    Tie,
    Player1Victory,
    Player2Victory,
}