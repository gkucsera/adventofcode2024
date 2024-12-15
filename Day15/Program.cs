// See https://aka.ms/new-console-template for more information

using System.Text;

Console.WriteLine("Day 15");

var input = File.ReadAllLines("input.txt");
var count = 0;
foreach (var line in input)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        break;
    }

    count++;
}

var grid = new char[count, input[0].Length];

var posX = 0;
var posY = 0;
for (var i = 0; i < grid.GetLength(0); i++)
{
    for (var j = 0; j < grid.GetLength(1); j++)
    {
        if (input[i][j] == '@')
        {
            posY = i;
            posX = j;
            grid[i, j] = '.';
        }
        else
        {
            grid[i, j] = input[i][j];
        }
    }
}

var stringBuilder = new StringBuilder();
for (var i = count + 1; i < input.Length; i++)
{
    stringBuilder.Append(input[i]);
}

var moves = stringBuilder.ToString();

DoMoves();

var sum = GetPositionsSum();

Console.WriteLine($"Part 1: {sum}");


grid = new char[count, input[0].Length * 2];

posX = 0;
posY = 0;
for (var i = 0; i < grid.GetLength(0); i++)
{
    for (var j = 0; j < grid.GetLength(1) / 2; j++)
    {
        if (input[i][j] == '@')
        {
            var gridJ = j * 2;
            posY = i;
            posX = gridJ;
            grid[i, gridJ] = '.';
            grid[i, gridJ + 1] = '.';
        }
        else
        {
            if (input[i][j] == 'O')
            {
                var gridJ = j * 2;
                grid[i, gridJ] = '[';
                grid[i, gridJ + 1] = ']';
            }
            else
            {
                var gridJ = j * 2;
                grid[i, gridJ] = input[i][j];
                grid[i, gridJ + 1] = input[i][j];
            }
        }
    }
}

DoMoves();
sum = GetPositionsSum();
Console.WriteLine($"Part 2: {sum}");
return;

void DoMoves()
{
    foreach (var move in moves)
    {
        var nextMove = move switch
        {
            '^' => (y: -1, X: 0),
            'v' => (y: 1, X: 0),
            '<' => (y: 0, X: -1),
            '>' => (y: 0, X: 1),
            _ => throw new Exception("Invalid move")
        };
        Move(nextMove.X, nextMove.y);
    }
}

void Move(int diffX, int diffY)
{
    var nextX = posX + diffX;
    var nextY = posY + diffY;
    switch (grid[nextY, nextX])
    {
        case '.':
            posX = nextX;
            posY = nextY;
            break;
        case '#':
            break;
        case 'O':
            Push(diffX, diffY, nextX, nextY);
            break;
        case '[':
        case ']':
            PushDoubled(diffX, diffY, nextX, nextY);
            break;
        default:
            throw new Exception("Invalid character");
    }
}

void Push(int diffX, int diffY, int nextX, int nextY)
{
    var checkNext = true;
    var canReplace = false;
    var replaceToX = 0;
    var replaceToY = 0;
    var x = nextX;
    var y = nextY;
    while (checkNext)
    {
        switch (grid[y, x])
        {
            case 'O':
                x += diffX;
                y += diffY;
                break;
            case '#':
                checkNext = false;
                break;
            case '.':
                replaceToX = x;
                replaceToY = y;
                checkNext = false;
                canReplace = true;
                break;
        }
    }

    if (canReplace)
    {
        grid[nextY, nextX] = '.';
        grid[replaceToY, replaceToX] = 'O';
        posX = nextX;
        posY = nextY;
    }
}

void PushDoubled(int diffX, int diffY, int nextX, int nextY)
{
    if (diffX != 0)
    {
        PushDoubledHorizontally(diffX, nextX, nextY);
    }
    else
    {
        PushDoubledVertically(diffY, nextX, nextY);
    }
}

void PushDoubledHorizontally(int diffX, int nextX, int nextY)
{
    var checkNext = true;
    var canReplace = false;
    var replaceToX = 0;
    var replaceToY = 0;
    var x = nextX;
    var y = nextY;
    while (checkNext)
    {
        switch (grid[y, x])
        {
            case '[':
            case ']':
                x += diffX;
                break;
            case '#':
                checkNext = false;
                break;
            case '.':
                replaceToX = x;
                replaceToY = y;
                checkNext = false;
                canReplace = true;
                break;
        }
    }

    if (canReplace)
    {
        grid[nextY, nextX] = '.';
        grid[replaceToY, replaceToX] = diffX == 1 ? '[' : ']';

        for (var i = nextX + diffX; diffX == 1 ? i <= replaceToX : i >= replaceToX; i += diffX)
        {
            switch (grid[nextY, i])
            {
                case '[':
                    grid[nextY, i] = ']';
                    break;
                case ']':
                    grid[nextY, i] = '[';
                    break;
                default: throw new Exception("invalid character");
            }
        }


        posX = nextX;
    }
}

void PushDoubledVertically(int diffY, int nextX, int nextY)
{
    var checkNext = true;
    var canReplace = false;
    var y = nextY;
    var boxes = new Stack<HashSet<int>>();
    boxes.Push([nextX]);
    while (checkNext)
    {
        var currentBoxes = new HashSet<int>();
        foreach (var box in boxes.Peek())
        {
            switch (grid[y, box])
            {
                case '[':
                    currentBoxes.Add(box);
                    currentBoxes.Add(box + 1);
                    break;
                case ']':
                    currentBoxes.Add(box);
                    currentBoxes.Add(box - 1);
                    break;
                case '#':
                    checkNext = false;
                    canReplace = false;
                    break;
                case '.':
                    break;
                default:
                    throw new Exception("invalid character");
            }
        }

        if (checkNext)
        {
            if (currentBoxes.Any())
            {
                boxes.Push(currentBoxes);
                y += diffY;
            }
            else
            {
                checkNext = false;
                canReplace = true;
            }

        }
    }

    if (canReplace)
    {
        foreach (var box in boxes)
        {
            foreach (var x in box)
            {
                grid[y, x] = grid[y - diffY, x];
                grid[y - diffY, x] = '.';
            }

            y -= diffY;
        }

        posX = nextX;
        posY = nextY;
    }
}


int GetPositionsSum()
{
    var result = 0;
    for (var y = 1; y < grid.GetLength(0) - 1; y++)
    {
        for (var x = 1; x < grid.GetLength(1) - 1; x++)
        {
            if (grid[y, x] == 'O' || grid[y, x] == '[')
            {
                result += 100 * y + x;
            }
        }
    }

    return result;
}

void DrawGrid()
{
    Console.Clear();
    for (int y = 0; y < grid.GetLength(0); y++)
    {
        for (int x = 0; x < grid.GetLength(1); x++)
        {
            if (y == posY && x == posX)
            {
                Console.Write('@');
            }
            else
            {
                Console.Write(grid[y, x]);
            }
        }

        Console.WriteLine();
    }
}