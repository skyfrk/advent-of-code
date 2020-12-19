using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var messageRegex = new Regex(@"^([ab]+)$");
var ruleLineRegex = new Regex(@"^(\d+): (.*)");

List<string> messages = new();
Dictionary<int, string> rules = new();

foreach (var line in input)
{
    if (messageRegex.IsMatch(line)) messages.Add(line);
    if (ruleLineRegex.IsMatch(line))
    {
        var match = ruleLineRegex.Match(line);
        rules.Add(int.Parse(match.Groups[1].Value), match.Groups[2].Value);
    }
}

var firstRuleRegex = new Regex($"^{BuildRegex(rules[0])}$");

Console.WriteLine($"Part 1: {messages.Count(m => firstRuleRegex.IsMatch(m))}");

string BuildRegex(string inputRule)
{
    if (inputRule.Contains("\"")) return inputRule.Replace("\"", string.Empty);

    List<string> subrules = new();

    if (inputRule.Contains("|"))
    {
        subrules.AddRange(inputRule.Split("|").Select(s => s.Trim()));
    }
    else
    {
        subrules.Add(inputRule);
    }

    List<string> subruleRegexList = new();

    foreach (var subrule in subrules)
    {
        var subruleRegex = string.Empty;
        foreach (var ruleIdStr in subrule.Split(" "))
        {
            var ruleId = int.Parse(ruleIdStr);
            subruleRegex += BuildRegex(rules[ruleId]);
        }
        subruleRegexList.Add(subruleRegex);
    }

    return $"({string.Join('|', subruleRegexList)})";
}
