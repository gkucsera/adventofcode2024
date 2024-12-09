// See https://aka.ms/new-console-template for more information

Console.WriteLine("Day 8");
var input = File.ReadAllText("input.txt").Select(item => int.Parse(item.ToString())).ToArray();
var memoryList = new List<int>();
var idCount = new Dictionary<int, (int start, int size)>();
var emptyCount = new Dictionary<int, SortedSet<int>>();
for (var i = 0; i < input.Length; i++)
{
    var id = i % 2 == 0 ? i / 2 : -1;
    
    if (id != -1)
    {
        idCount[id] = (memoryList.Count, input[i]);
    }
    else
    {
        if (input[i] != 0)
        {
            if (!emptyCount.ContainsKey(input[i]))
            {
                emptyCount[input[i]] = [];
            }

            emptyCount[input[i]].Add(memoryList.Count);
        }
    }
    
    for (var j = 0; j < input[i]; j++)
    {
        memoryList.Add(id);
    }
}

var memory = memoryList.ToArray();
var result = 0L;
var endId = memory.Length - 1;
var startId = 0;
var counter = 0;
while (startId <= endId)
{
    if (memory[startId] != -1)
    {
        result += memory[startId] * counter;
        counter++;
        startId++;
    }
    else
    {
        memory[startId] = memory[endId];
        memory[endId] = -1;
        endId--;
    }
}
Console.WriteLine($"Part 1: {result}");

memory = memoryList.ToArray();
result = 0L;
foreach (var id in idCount.OrderByDescending(item => item.Key))
{
    var size = id.Value.size;
    var start = id.Value.start;
    var emptyList = emptyCount.Where(item => item.Key >= size && item.Value.Any()).ToList();
    if (!emptyList.Any())
    {
        continue;
    }

    var firstEmpty = emptyList.OrderBy(item => item.Value.Min()).First();
    var emptyStart = firstEmpty.Value.Min();

    if (emptyStart > id.Value.start)
    {
        continue;
    }
    
    for (var i = 0; i < size; i++)
    {
        memory[emptyStart + i] = id.Key;
        memory[start + i] = -1;
    }

    var newEmptySize = firstEmpty.Key - size;
    var newStart = emptyStart + size;
    firstEmpty.Value.Remove(emptyStart);
    if (newEmptySize != 0)
    {
        if (!emptyCount.ContainsKey(newEmptySize))
        {
            emptyCount[newEmptySize] = [];
        }
        emptyCount[newEmptySize].Add(newStart);
    }
}

for (var i = 0; i < memory.Length; i++)
{
    if (memory[i] != -1)
    {
        result += memory[i] * i;
    }
}

Console.WriteLine($"Part 2: {result}");