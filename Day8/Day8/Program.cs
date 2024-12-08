// See https://aka.ms/new-console-template for more information

using Day8;

Console.WriteLine("Day 8");

var input = File.ReadAllLines("input.txt").Select(item => item.ToCharArray()).ToArray();

var nodes = new Dictionary<char, List<Position>>();
var antiNodes = new HashSet<Position>();

for (var y = 0; y < input.Length; y++)
{
    for (var x = 0; x < input[y].Length; x++)
    {
        if (input[y][x] == '.')
            continue;
        if (nodes.ContainsKey(input[y][x]))
            nodes[input[y][x]].Add(new Position { Y = y, X = x });
        else
            nodes[input[y][x]] = [new Position { Y = y, X = x }];
    }
}

foreach (var node in nodes)
{
    var currentNodes = node.Value.ToArray();
    for (var i = 0; i < currentNodes.Length - 1; i++)
    {
        var nodeA = currentNodes[i];
        for (var j = i + 1; j < currentNodes.Length; j++)
        {
            var nodeB = currentNodes[j];

            var diffY = nodeB.Y - nodeA.Y;
            var diffX = nodeB.X - nodeA.X;
            var newPosA = new Position { Y = nodeA.Y - diffY, X = nodeA.X - diffX };
            var newPosB = new Position { Y = nodeB.Y + diffY, X = nodeB.X + diffX };
            
            antiNodes.Add(newPosA);
            antiNodes.Add(newPosB);
        }
    }
}

antiNodes = antiNodes.Where(item => item.Y >= 0 && item.X >= 0 && item.Y < input.Length && item.X < input[0].Length).ToHashSet();
Console.WriteLine($"Part 1: {antiNodes.Count}");

antiNodes.Clear();

foreach (var node in nodes)
{
    var currentNodes = node.Value.ToArray();
    for (var i = 0; i < currentNodes.Length - 1; i++)
    {
        var nodeA = currentNodes[i];
        for (var j = i + 1; j < currentNodes.Length; j++)
        {
            var nodeB = currentNodes[j];
            antiNodes.Add(nodeA);
            antiNodes.Add(nodeB);

            var diffY = nodeB.Y - nodeA.Y;
            var diffX = nodeB.X - nodeA.X;
            var newPosA = new Position { Y = nodeA.Y - diffY, X = nodeA.X - diffX };
            antiNodes.Add(newPosA);

            while (newPosA.Y >= 0 && newPosA.X >= 0 && newPosA.X < input[0].Length)
            {
                newPosA = new Position { Y = newPosA.Y - diffY, X = newPosA.X - diffX };
                antiNodes.Add(newPosA);
            }
            
            var newPosB = new Position { Y = nodeB.Y + diffY, X = nodeB.X + diffX };
            antiNodes.Add(newPosB);
            while (newPosB.Y < input.Length && newPosB.X >= 0 && newPosB.X < input[0].Length)
            {
                newPosB = new Position { Y = newPosB.Y + diffY, X = newPosB.X + diffX };
                antiNodes.Add(newPosB);
            }
        }
    }
}

antiNodes = antiNodes.Where(item => item.Y >= 0 && item.X >= 0 && item.Y < input.Length && item.X < input[0].Length).ToHashSet();
Console.WriteLine($"Part 2: {antiNodes.Count}");