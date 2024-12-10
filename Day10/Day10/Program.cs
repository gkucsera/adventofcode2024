// See https://aka.ms/new-console-template for more information

Console.WriteLine("Day 10");

var input = File.ReadAllLines("input.txt").Select(item => item.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

var result = 0;
var ratingResult = 0;

for (var y = 0; y < input.Length; y++)
{
    for (var x = 0; x < input[y].Length; x++)
    {
        if (input[y][x] == 0)
        {
            var trails = CheckTrail(input, y, x);
            result += trails.Count;
            ratingResult += trails.Sum(item => item.Value);
        }
    }
}

Console.WriteLine($"Part 1: {result}");
Console.WriteLine($"Part 2: {ratingResult}");

Dictionary<Position, int> CheckTrail(int[][] grid, int y, int x)
{
    var reachableEnds = new Dictionary<Position, int>();

    GoNext(grid, y + 1, x, 1, reachableEnds);
    GoNext(grid, y, x + 1, 1, reachableEnds);
    GoNext(grid, y - 1, x, 1, reachableEnds);
    GoNext(grid, y, x - 1, 1, reachableEnds);

    return reachableEnds;
}

void GoNext(int[][] grid, int y, int x, int target, Dictionary<Position, int> reachableEnds)
{
    if (y < 0 || x < 0 || y >= grid.Length || x >= grid[y].Length)
    {
        return;
    }

    if (target == 9 && grid[y][x] == target)
    {
        var currentPos = new Position { X = x, Y = y };
        reachableEnds.TryAdd(currentPos, 0);

        reachableEnds[currentPos]++;
        return;
    }

    if (target == 9)
    {
        return;
    }

    if (target == grid[y][x])
    {
        GoNext(grid, y + 1, x, target + 1, reachableEnds);
        GoNext(grid, y, x + 1, target + 1, reachableEnds);
        GoNext(grid, y - 1, x, target + 1, reachableEnds);
        GoNext(grid, y, x - 1, target + 1, reachableEnds);
    }
}

struct Position : IEquatable<Position>
{
    public int Y { get; set; }
    public int X { get; set; }

    public override int GetHashCode()
    {
        return HashCode.Combine(Y, X);
    }

    public bool Equals(Position other)
    {
        return Y == other.Y && X == other.X;
    }

    public override bool Equals(object? obj)
    {
        return obj is Position other && Equals(other);
    }
}