using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

void SolvePart1()
{
    long sum = 0;

    foreach (var line in input)
    {
        var tokens = GetTokens(line);
        var tokenTree = ParseTokens(tokens);
        var value = tokenTree.GetValue();
        sum += value;
    }

    Console.WriteLine($"Part 1: {sum}");

    TokenTree ParseTokens(Token[] tokens)
    {
        if (tokens.Length == 1)
        {
            return new TokenTree(tokens[0], null, null);
        }

        if (tokens.Length < 3) throw new InvalidOperationException("Can not parse a token tree. Token count invalid.");

        var remainingTokens = tokens.Select(t => t).ToList();

        TokenTree leftTree = null;
        TokenTree nextTree = null;
        Token operatorToken = null;

        while (remainingTokens.Count > 0)
        {
            if (remainingTokens[0].Type == TokenType.Number)
            {
                var numberTokens = new List<Token>();
                var tokenToBeRemovedCount = 0;

                for (int i = 0; i < remainingTokens.Count; i++)
                {
                    if (remainingTokens[i].Type == TokenType.Number)
                    {
                        numberTokens.Add(remainingTokens[i]);
                        tokenToBeRemovedCount++;
                    }
                    else break;
                }

                remainingTokens.RemoveRange(0, tokenToBeRemovedCount);

                var number = int.Parse(string.Concat(numberTokens.Select(t => t.Value.ToString())));
                var numberToken = new Token(TokenType.Number, number);
                nextTree = new TokenTree(numberToken, null, null);
            }
            else if (remainingTokens[0].Type == TokenType.BracketOpen)
            {
                var openBracketCount = 1;
                var tokensInBrackets = new List<Token>();
                var tokenIndicesToBeRemovedCount = 1;

                for (int i = 1; i < remainingTokens.Count; i++)
                {
                    if (remainingTokens[i].Type == TokenType.BracketOpen) openBracketCount++;
                    if (remainingTokens[i].Type == TokenType.BracketClose) openBracketCount--;

                    tokenIndicesToBeRemovedCount++;

                    if (openBracketCount == 0) break;

                    tokensInBrackets.Add(remainingTokens[i]);
                }

                remainingTokens.RemoveRange(0, tokenIndicesToBeRemovedCount);

                nextTree = ParseTokens(tokensInBrackets.ToArray());
            }
            else if (remainingTokens[0].Type is TokenType.Multiplication or TokenType.Addition)
            {
                operatorToken = remainingTokens[0];
                remainingTokens.RemoveAt(0);
            }

            if (nextTree != null && operatorToken != null)
            {
                if (leftTree == null)
                {
                    leftTree = nextTree;
                    nextTree = null;
                    continue;
                }

                leftTree = new TokenTree(operatorToken, leftTree, nextTree);
                nextTree = null;
                operatorToken = null;
            }
        }

        return leftTree;
    }

    Token[] GetTokens(string line)
    {
        var tokens = new List<Token>();

        foreach (var value in line)
        {
            if (value == ' ') continue;

            var tokenType = value switch
            {
                '+' => TokenType.Addition,
                '*' => TokenType.Multiplication,
                '(' => TokenType.BracketOpen,
                ')' => TokenType.BracketClose,
                '0' => TokenType.Number,
                '1' => TokenType.Number,
                '2' => TokenType.Number,
                '3' => TokenType.Number,
                '4' => TokenType.Number,
                '5' => TokenType.Number,
                '6' => TokenType.Number,
                '7' => TokenType.Number,
                '8' => TokenType.Number,
                '9' => TokenType.Number,
                _ => throw new InvalidOperationException("Invalid token.")
            };

            if (tokenType == TokenType.Number)
            {
                tokens.Add(new Token(tokenType, (int)char.GetNumericValue(value)));
            }
            else
            {
                tokens.Add(new Token(tokenType, 0));
            }
        }

        return tokens.ToArray();
    }
}

void SolvePart2()
{
    long solution = 0;

    foreach (var rawLine in input)
    {
        solution += Evaluate(rawLine.Replace(" ", string.Empty));
    }

    Console.WriteLine($"Part 2: {solution}");

    long Evaluate(string line)
    {
        var additionRegex = new Regex(@"(\d+)\+(\d+)");
        var multiplicationRegex = new Regex(@"(\d+)\*(\d+)");

        while (TryFindBracketPair(line, out var position))
        {
            var bracketPairString = line.Substring(position.startIdx + 1, position.length - 2);
            var bracketPairResult = Evaluate(bracketPairString);

            line = line.Remove(position.startIdx, position.length);
            line = line.Insert(position.startIdx, bracketPairResult.ToString());
        }

        while (additionRegex.IsMatch(line))
        {
            var firstMatch = additionRegex.Match(line);
            var sum = long.Parse(firstMatch.Groups[1].Value) + long.Parse(firstMatch.Groups[2].Value);
            line = line.Remove(firstMatch.Index, firstMatch.Length);
            line = line.Insert(firstMatch.Index, sum.ToString());
        }

        while (multiplicationRegex.IsMatch(line))
        {
            var firstMatch = multiplicationRegex.Match(line);
            var product = long.Parse(firstMatch.Groups[1].Value) * long.Parse(firstMatch.Groups[2].Value);
            line = line.Remove(firstMatch.Index, firstMatch.Length);
            line = line.Insert(firstMatch.Index, product.ToString());
        }

        return long.Parse(line);
    }

    bool TryFindBracketPair(string input, out (int startIdx, int length) position)
    {
        var openBracketCount = 0;
        var openBracketIdx = 0;
        var length = 0;
        var foundOpenBracket = false;

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '(')
            {
                if (!foundOpenBracket)
                {
                    openBracketIdx = i;
                    foundOpenBracket = true;
                }
                openBracketCount++;
            }

            if (input[i] == ')' && foundOpenBracket) openBracketCount--;

            if (foundOpenBracket && openBracketCount == 0)
            {
                length = i - openBracketIdx + 1;
                break;
            }
        }

        position = (openBracketIdx, length);

        return foundOpenBracket;
    }
}

SolvePart1();
SolvePart2();

enum TokenType
{
    Number,
    Addition,
    Multiplication,
    BracketOpen,
    BracketClose
}

record Token(TokenType Type, int Value);

record TokenTree(Token Token, TokenTree LeftChild, TokenTree RightChild)
{
    public long GetValue() => Token.Type switch
    {
        TokenType.Number => Token.Value,
        TokenType.Addition => LeftChild.GetValue() + RightChild.GetValue(),
        TokenType.Multiplication => LeftChild.GetValue() * RightChild.GetValue(),
        _ => throw new InvalidOperationException("Can not get value for given token type.")
    };
}