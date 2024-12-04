// See https://aka.ms/new-console-template for more information

Console.WriteLine("Day 1");

var input = File.ReadAllLines("input.txt").Select(item => item.Split("   ")).ToList();
var leftIndexes = input.Select(item => int.Parse(item[0])).ToArray();
var rightIndexes = input.Select(item => int.Parse(item[1])).ToArray();

Array.Sort(leftIndexes);
Array.Sort(rightIndexes);

var sum = 0;

for (var i = 0; i < leftIndexes.Length; i++)
{
    sum += Math.Abs(leftIndexes[i] - rightIndexes[i]);
}

Console.WriteLine($"part 1 result: {sum}");

var idDictionary = new Dictionary<int, int>();

foreach (var index in rightIndexes)
{
    if (!idDictionary.TryAdd(index, 1))
    {
        idDictionary[index]++;
    }
}

sum = 0;
foreach (var index in leftIndexes)
{
    var multiplier = idDictionary.GetValueOrDefault(index, 0);
    sum += index * multiplier;
}

Console.WriteLine($"part 2 result: {sum}");