// See https://aka.ms/new-console-template for more information

Console.WriteLine("Day 16");

var grid = File.ReadAllLines("input.txt").Select(item => item.ToCharArray()).ToArray();

var startX = 0;
var startY = 0;

for (var i = 0; i < grid.Length; i++)
{
    for (var j = 0; j < grid[i].Length; j++)
    {
        if (grid[i][j] == 'S')
        {
            startY = i;
            startX = j;
            grid[i][j] = '.';
        }
    }
}

var result = int.MaxValue;
var visited = new Dictionary<(int x, int y), int>();
var bestPathPositions = new List<(int x, int y)>();
//start forward
visited[(startX, startY)] = 0;
var currentList = new LinkedList<(int x, int y)>();
currentList.AddFirst((startX, startY));
GoNext(startX, startY, 0, 0, 1, currentList);
// start left
visited[(startX, startY)] = 1000;
currentList = [];
currentList.AddFirst((startX, startY));
GoNext(startX, startY, 1000, -1, 0, currentList);
// start right
visited[(startX, startY)] = 1000;
currentList = [];
currentList.AddFirst((startX, startY));
GoNext(startX, startY, 1000, 1, 0, currentList);
//start backward
visited[(startX, startY)] = 2000;
currentList = [];
currentList.AddFirst((startX, startY));
GoNext(startX, startY, 2000, 0, -1, currentList);

Console.WriteLine($"Part 1: {result}");
bestPathPositions = bestPathPositions.Distinct().ToList();
Console.WriteLine($"Part 2: {bestPathPositions.Distinct().Count()}");

return;

void GoNext(int currentX, int currentY, int currentResult, int forwardY, int forwardX, LinkedList<(int x, int y)> currentPath)
{
    if (grid[currentY][currentX] == 'E')
    {
        if (currentResult < result)
        {
            result = currentResult;
            bestPathPositions = currentPath.ToList();
        }
        else if (currentResult == result)
        {
            bestPathPositions.AddRange(currentPath);
        }

        currentPath.RemoveLast();
        return;
    }


    visited[(currentX, currentY)] = currentResult;
    // move forward + 1
    if (grid[currentY + forwardY][currentX + forwardX] != '#' &&
        CheckMove(currentX + forwardX, currentY + forwardY, currentResult))
    {
        currentPath.AddLast((currentX + forwardX, currentY + forwardY));
        GoNext(currentX + forwardX, currentY + forwardY, currentResult + 1, forwardY, forwardX, currentPath);
    }

    // move left + 1000
    var nextDirLeft = GetNextLeftDirection(forwardY, forwardX);
    if (grid[currentY + nextDirLeft.yDiff][currentX + nextDirLeft.xDiff] != '#' &&
        CheckMove(currentX + nextDirLeft.xDiff, currentY + nextDirLeft.yDiff, currentResult))
    {
        visited[(currentX, currentY)] = currentResult + 1000;
        currentPath.AddLast((currentX + nextDirLeft.xDiff, currentY + nextDirLeft.yDiff));
        GoNext(currentX + nextDirLeft.xDiff, currentY + nextDirLeft.yDiff, currentResult + 1001, nextDirLeft.yDiff, nextDirLeft.xDiff, currentPath);
    }

    // move right + 1000
    var nextDirRight = GetNextRightDirection(forwardY, forwardX);
    if (grid[currentY + nextDirRight.yDiff][currentX + nextDirRight.xDiff] != '#' &&
        CheckMove(currentX + nextDirRight.xDiff, currentY + nextDirRight.yDiff, currentResult))
    {
        visited[(currentX, currentY)] = currentResult + 1000;
        currentPath.AddLast((currentX + nextDirRight.xDiff, currentY + nextDirRight.yDiff));
        GoNext(currentX + nextDirRight.xDiff, currentY + nextDirRight.yDiff, currentResult + 1001, nextDirRight.yDiff, nextDirRight.xDiff, currentPath);
    }

    currentPath.RemoveLast();
}

(int yDiff, int xDiff) GetNextRightDirection(int yDiff, int xDiff) =>
    yDiff switch
    {
        0 when xDiff == 1 => (1, 0),
        1 when xDiff == 0 => (0, -1),
        0 when xDiff == -1 => (-1, 0),
        -1 when xDiff == 0 => (0, 1),
        _ => throw new Exception("invalid Right turn")
    };

(int yDiff, int xDiff) GetNextLeftDirection(int yDiff, int xDiff) =>
    yDiff switch
    {
        0 when xDiff == 1 => (-1, 0),
        -1 when xDiff == 0 => (0, -1),
        0 when xDiff == -1 => (1, 0),
        1 when xDiff == 0 => (0, 1),
        _ => throw new Exception("invalid left turn")
    };

bool CheckMove(int posx, int posy, int currentResult) => !visited.TryGetValue((posx, posy), out var res) || currentResult <= res;

void DrawGrid(int posX, int posY)
{
    Console.Clear();
    for (var j = 0; j < grid.Length; j++)
    {
        for (int i = 0; i < grid[j].Length; i++)
        {
            if (j == posY && i == posX)
            {
                Console.Write('@');
            }
            else
            {
                Console.Write(grid[j][i]);
            }
        }

        Console.WriteLine();
    }

    Thread.Sleep(300);
}

void DrawGridBestPos(LinkedList<(int posx, int posy)> positions)
{
    Console.Clear();
    for (var j = 0; j < grid.Length; j++)
    {
        for (int i = 0; i < grid[j].Length; i++)
        {
            if (positions.Contains((i, j)))
            {
                Console.Write('O');
            }
            else
            {
                Console.Write(grid[j][i]);
            }
        }

        Console.WriteLine();
    }

    Thread.Sleep(300);
}