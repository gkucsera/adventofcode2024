// See https://aka.ms/new-console-template for more information

Console.WriteLine("Day 11");
var input = File.ReadAllText("input.txt").Split(' ').Select(int.Parse).ToArray();

var stones = input.ToList();
var totalTransformCache = new Dictionary<long, Dictionary<int, long>>();

var result = 0L;

foreach (var stone in stones)
{
    result += GetTransformCount(stone, 25);
}

Console.WriteLine($"Part 1 : {result}");

result = 0L;

foreach (var stone in stones)
{
    result += GetTransformCount(stone, 75);
}

Console.WriteLine($"Part 1 : {result}");
return;

long GetTransformCount(long stone, int day)
{
    if (day == 1)
    {
        return GetNextDay(stone).two == -1 ? 1 : 2;
    }

    if (totalTransformCache.TryGetValue(stone, out var numberTransforms))
    {
        if (numberTransforms.TryGetValue(day, out var transform))
        {
            return transform;
        }
    }

    var next = GetNextDay(stone);
    var currentResult = 0L;
    currentResult += GetTransformCount(next.one, day - 1);
    if (next.two != -1)
    {
        currentResult += GetTransformCount(next.two, day - 1);
    }

    totalTransformCache.TryAdd(stone, new Dictionary<int, long>());
    totalTransformCache[stone][day] = currentResult;
    return currentResult;
}

(long one, long two) GetNextDay(long stone)
{
    if (stone == 0)
    {
        return (1, -1);
    }

    var stringNumber = stone.ToString();
    if (stringNumber.Length % 2 == 0)
    {
        var first = stringNumber.Substring(0, stringNumber.Length / 2);
        var second = stringNumber.Substring(stringNumber.Length / 2);
        return (int.Parse(first), int.Parse(second));
    }

    return (stone * 2024, -1);
}