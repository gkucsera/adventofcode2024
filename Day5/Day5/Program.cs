// See https://aka.ms/new-console-template for more information

Console.WriteLine("Day 5");

var input = File.ReadAllLines("input.txt");

var lines = input.Where(item => item.Contains(',')).Select(item => item.Split(',').Select(int.Parse).ToArray()).ToList();
var rules = new Dictionary<int, List<int>>();

foreach (var rule in input.Where(item => item.Contains('|')))
{
    var split = rule.Split("|").Select(int.Parse).ToArray();
    if (rules.ContainsKey(split[0]))
    {
        rules[split[0]].Add(split[1]);
    }
    else
    {
        rules.Add(split[0], [split[1]]);
    }
}

var goodResult = 0;
var reorderedResult = 0;

foreach (var line in lines)
{
    var isGoodOrder = true;
    for (var i = 0; i < line.Length - 1; i++)
    {
        for (var j = i + 1; j < line.Length; j++)
        {
            if (!rules.ContainsKey(line[i]))
            {
                isGoodOrder = false;
                break;
            }
            
            if (!rules[line[i]].Contains(line[j]))
            {
                isGoodOrder = false;
                break;
            }
        }

        if (!isGoodOrder)
        {
            break;
        }
    }

    if (isGoodOrder)
    {
        var index = line.Length / 2;
        goodResult += line[index];
    }
    else
    {
        reorderedResult += GetReordered(line);
    }
}

Console.WriteLine($"Part 1: {goodResult}");
Console.WriteLine($"Part 2: {reorderedResult}");

int GetReordered(int[] line)
{
    var lineList = line.ToList();
    var result = 0;
    for (var i = 0; i < line.Length / 2 + 1; i++)
    {
        var first = lineList.Where(item => rules.ContainsKey(item)).First(item => lineList.Where(e => e != item).All(e =>  rules[item].Contains(e)));
        result = first;
        lineList.Remove(result);
    }

    return result;
}

