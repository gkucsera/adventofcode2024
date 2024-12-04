// See https://aka.ms/new-console-template for more information

Console.WriteLine("Day 4!");

var input = File.ReadAllLines("input.txt");

var result = 0;
for (var y = 0; y < input.Length; y++)
{
    for (var x = 0; x < input[y].Length; x++)
    {
        if (input[y][x] != 'X')
        {
            continue;
        }

        try
        {
            if (CheckHorizontal(input, y, x, false))
            {
                result++;
            }

            if (CheckHorizontal(input, y, x, true))
            {
                result++;
            }

            if (CheckVertical(input, y, x, false))
            {
                result++;
            }

            if (CheckVertical(input, y, x, true))
            {
                result++;
            }

            if (CheckDiagonal(input, y, x, false, false))
            {
                result++;
            }

            if (CheckDiagonal(input, y, x, false, true))
            {
                result++;
            }

            if (CheckDiagonal(input, y, x, true, false))
            {
                result++;
            }

            if (CheckDiagonal(input, y, x, true, true))
            {
                result++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

Console.WriteLine($"Part 1: {result}");

result = 0;
for (var y = 1; y < input.Length - 1; y++)
{
    for (var x = 1; x < input[y].Length - 1; x++)
    {
        if (CheckCross(input, y, x))
            result++;
    }
}

Console.WriteLine($"Part 2: {result}");

bool CheckHorizontal(string[] grid, int y, int x, bool backwards)
{
    if (!backwards && x > grid[y].Length - 4)
        return false;
    if (backwards && x < 3)
        return false;

    var diff = backwards ? -1 : 1;
    return grid[y][x + diff] == 'M' && grid[y][x + diff * 2] == 'A' && grid[y][x + diff * 3] == 'S';
}

bool CheckVertical(string[] grid, int y, int x, bool backwards)
{
    if (!backwards && y > grid.Length - 4)
        return false;
    if (backwards && y < 3)
        return false;

    var diff = backwards ? -1 : 1;
    return grid[y + diff][x] == 'M' && grid[y + diff * 2][x] == 'A' && grid[y + diff * 3][x] == 'S';
}

bool CheckDiagonal(string[] grid, int y, int x, bool backwards, bool upwards)
{
    if (backwards && upwards && (y < 3 || x < 3))
        return false;
    if (backwards && !upwards && (y > grid.Length - 4 || x < 3))
        return false;
    if (!backwards && upwards && (y < 3 || x > grid[y].Length - 4))
        return false;
    if (!backwards && !upwards && (y > grid.Length - 4 || x > grid[y].Length - 4))
        return false;

    var yDiff = upwards ? -1 : 1;
    var xDiff = backwards ? -1 : 1;

    return grid[y + yDiff][x + xDiff] == 'M' && grid[y + yDiff * 2][x + xDiff * 2] == 'A' && grid[y + yDiff * 3][x + xDiff * 3] == 'S';
}

bool CheckCross(string[] grid, int y, int x)
{
    if (grid[y][x] != 'A')
        return false;
    
    var matches = 0;
    if (grid[y - 1][x - 1] == 'S' && grid[y + 1][x + 1] == 'M')
        matches++;
    if (grid[y - 1][x - 1] == 'M' && grid[y + 1][x + 1] == 'S')
        matches++;
    if (grid[y + 1][x - 1] == 'S' && grid[y - 1][x + 1] == 'M')
        matches++;
    if (grid[y + 1][x - 1] == 'M' && grid[y - 1][x + 1] == 'S')
        matches++;

    return matches == 2;
}