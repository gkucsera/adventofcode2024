// See https://aka.ms/new-console-template for more information

using Day12;

Console.WriteLine("Day 12");
var input = File.ReadAllLines("input.txt").Select(item => item.ToCharArray()).ToArray();

var checkPositions = new HashSet<Position>();
var regions = new List<Region>();
for (var y = 0; y < input.Length; y++)
{
    for (var x = 0; x < input[y].Length; x++)
    {
        if (checkPositions.Contains(new Position(x, y)))
            continue;

        var currentRegion = new HashSet<Position>();
        CreateRegion(currentRegion, y, x, input[y][x]);
        if (currentRegion.Any())
        {
            regions.Add(new Region(currentRegion));
        }
    }
}

var result = regions.Sum(item => item.Size * item.Perimeter);
Console.WriteLine($"Part 1: {result}");
result = regions.Sum(item => item.Size * item.Sides);
Console.WriteLine($"Part 2: {result}");
return;

void CreateRegion(HashSet<Position> positions, int y, int x, char tree)
{
    if (checkPositions.Contains(new Position(x, y)))
        return;
    if (y < 0 || x < 0 || y >= input.Length || x >= input[y].Length)
        return;

    if (input[y][x] == tree)
    {
        var position = new Position(x, y);
        positions.Add(position);
        checkPositions.Add(position);

        CreateRegion(positions, y + 1, x, tree);
        CreateRegion(positions, y, x + 1, tree);
        CreateRegion(positions, y - 1, x, tree);
        CreateRegion(positions, y, x - 1, tree);
    }
}