// See https://aka.ms/new-console-template for more information

using System.Diagnostics.CodeAnalysis;

Console.WriteLine("Day 6");

var input = File.ReadAllLines("input.txt").Select(item => item.ToCharArray()).ToArray();

var posY = 0;
var posX = 0;

var diffY = -1;
var diffX = 0;
var exit = false;
var startX = 0;
var startY = 0;
for (var i = 0; i < input.Length; i++)
{
    for (var j = 0; j < input[i].Length; j++)
    {
        if (input[i][j] == '^')
        {
            posY = i;
            posX = j;
            startX = j;
            startY = i;
            input[i][j] = 'X';
            break;
        }
    }
}

var copy = input.Select(item => item.ToArray()).ToArray();

do
{
    exit = IsNextExit(input);
    if (!exit)
    {
        var move = TryMove(input);
        if (!move)
        {
            Turn();
        }
    }
} while (!exit);

var result = input.SelectMany(item => item).Count(item => item == 'X');

Console.WriteLine($"Part  1: {result}");

var loopCount = 0;
for (var i = 0; i < input.Length; i++)
{
    for (var j = 0; j < input[i].Length; j++)
    {

        if (i == startY && j == startX)
        {
            continue;
        }

        var positions = new HashSet<Position>();
        var currentGrid = copy.Select(item => item.ToArray()).ToArray();
        currentGrid[i][j] = '#'; 
        posY = startY;
        posX = startX;

        diffY = -1;
        diffX = 0;
        exit = false;
        var position = new Position{PosX = posX, PosY = posY, DiffX = diffX, DiffY = diffY};
        positions.Add(position);
        do
        {
            exit = IsNextExit(currentGrid);
            if (!exit)
            {
                var move = TryMove(currentGrid);
                if (!move)
                {
                    Turn();
                }
            }
            position = new Position{PosX = posX, PosY = posY, DiffX = diffX, DiffY = diffY};

            if (!exit && !positions.Add(position))
            {
                loopCount++;
                exit = true;
            }
        } while (!exit);
    }
}

Console.WriteLine($"Part  2: {loopCount}");

bool IsNextExit(char[][] map)
{
    var nextY = posY + diffY;
    var nextX = posX + diffX;
    if (nextY == -1 || nextY == map.Length || nextX == -1 || nextX == map[0].Length)
    {
        map[posY][posX] = 'X';
        return true;
    }

    return false;
}

bool TryMove(char[][] map)
{
    map[posY][posX] = 'X';
    if (map[posY + diffY][posX + diffX] != '#')
    {
        posY += diffY;
        posX += diffX;
        return true;
    }

    return false;
}

void Turn()
{
    if (diffY == -1 && diffX == 0)
    {
        diffY = 0;
        diffX = 1;
    }
    else if (diffY == 0 && diffX == 1)
    {
        diffY = 1;
        diffX = 0;
    }
    else if (diffY == 1 && diffX == 0)
    {
        diffY = 0;
        diffX = -1;
    }
    else if (diffY == 0 && diffX == -1)
    {
        diffY = -1;
        diffX = 0;
    }
    else
    {
        throw new Exception("cannot turn");
    }
}

struct Position : IEquatable<Position>
{
    public int PosX { get; set; }
    public int PosY { get; set; }
    public int DiffX { get; set; }
    public int DiffY { get; set; }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Position pos ? Equals(pos) : base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PosX, PosY, DiffX, DiffY);
    }

    public bool Equals(Position other)
    {
        return PosX == other.PosX && PosY == other.PosY && DiffX == other.DiffX && DiffY == other.DiffY;
    }
}