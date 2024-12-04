// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

Console.WriteLine("Day 3");

var input = File.ReadAllText("input.txt");

var regex = new Regex("mul\\(\\d{1,3},\\d{1,3}\\)");
var numRegex = new Regex("\\d{1,3}");
var matches = regex.Matches(input);

var result = 0;
foreach (var match in matches.AsEnumerable())
{
    var numbers = numRegex.Matches(match.Value);
    var num1 = int.Parse(numbers[0].Value);
    var num2 = int.Parse(numbers[1].Value);
    result += num1 * num2;
}

Console.WriteLine($"Part 1: {result}");

result = 0;

const string count = "don't()";
const string notCount = "do()";
var splitWord = count;
var split = new[] { "", input };
do
{
    split = split[1].Split(splitWord, 2);

    if (splitWord == count)
    {
        matches = regex.Matches(split[0]);

        foreach (var match in matches.AsEnumerable())
        {
            var numbers = numRegex.Matches(match.Value);
            var num1 = int.Parse(numbers[0].Value);
            var num2 = int.Parse(numbers[1].Value);
            result += num1 * num2;
        }

        splitWord = notCount;
    }
    else
    {
        splitWord = count;
    }
} while (split.Length == 2);

if (splitWord == count)
{
    matches = regex.Matches(split[0]);

    foreach (var match in matches.AsEnumerable())
    {
        var numbers = numRegex.Matches(match.Value);
        var num1 = int.Parse(numbers[0].Value);
        var num2 = int.Parse(numbers[1].Value);
        result += num1 * num2;
    }
}

Console.WriteLine($"Part 2: {result}");