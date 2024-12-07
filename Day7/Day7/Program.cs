// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

Console.WriteLine("Day7");
var regex = new Regex(@"\d+");
var input = File.ReadAllLines("input.txt").Select(item => regex.Matches(item).Select(m => long.Parse(m.Value)).ToArray()).ToList();

var result = 0L;

foreach (var line in input)
{
    var sum = line[0];
    var numbers = line.Skip(1).ToArray();

    if (IsValid(sum, numbers, 1, numbers[0], false) || IsValid(sum, numbers, 1, numbers[0], true))
    {
        result += sum;
    }
}

Console.WriteLine($"Part 1 : {result}");

result = 0L;

foreach (var line in input)
{
    var sum = line[0];
    var numbers = line.Skip(1).ToArray();

    if (IsValidMoreOperation(sum, numbers, 1, numbers[0], '+') || 
        IsValidMoreOperation(sum, numbers, 1, numbers[0], '*') ||
        IsValidMoreOperation(sum, numbers, 1, numbers[0], '|'))
    {
        result += sum;
    }
}
Console.WriteLine($"Part 2 : {result}");
bool IsValid(long total, long[] numbers, int index, long currentResult, bool multiple)
{
    if (index == numbers.Length)
    {
        return false;
    }

    var nextResult = multiple ? currentResult * numbers[index] : currentResult + numbers[index];
    if (nextResult > total)
    {
        return false;
    }

    if (nextResult == total && index == numbers.Length - 1)
    {
        return true;
    }

    return IsValid(total, numbers, index + 1, nextResult, false) || IsValid(total, numbers, index + 1, nextResult, true);
}

bool IsValidMoreOperation(long total, long[] numbers, int index, long currentResult, char operation)
{
    if (index == numbers.Length)
    {
        return false;
    }

    var nextResult = operation switch
    {
        '*' => currentResult * numbers[index],
        '+' => currentResult + numbers[index],
        '|' => long.Parse($"{currentResult}{numbers[index]}")
    };
    if (nextResult > total)
    {
        return false;
    }

    if (nextResult == total && index == numbers.Length - 1)
    {
        return true;
    }

    return IsValidMoreOperation(total, numbers, index + 1, nextResult, '+') ||
           IsValidMoreOperation(total, numbers, index + 1, nextResult, '*') ||
           IsValidMoreOperation(total, numbers, index + 1, nextResult, '|');
}