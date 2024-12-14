// See https://aka.ms/new-console-template for more information

Console.WriteLine("Day 13");

var input = File.ReadAllLines("input.txt");
var machines = new List<Machine>();

for (var i = 0; i < input.Length; i += 4)
{
    var buttonA = input[i].Split(' ');
    var buttonB = input[i + 1].Split(' ');
    var prize = input[i + 2].Split(' ');
    var ax = int.Parse(buttonA[2].Replace("X+", "").Replace(",", ""));
    var ay = int.Parse(buttonA[3].Replace("Y+", ""));
    var bx = int.Parse(buttonB[2].Replace("X+", "").Replace(",", ""));
    var by = int.Parse(buttonB[3].Replace("Y+", ""));
    var totalx = int.Parse(prize[1].Replace("X=", "").Replace(",", ""));
    var totaly = int.Parse(prize[2].Replace("Y=", ""));
    machines.Add(new Machine(ax, ay, bx, by, totalx, totaly));
}

var totalCost = 0L;
foreach (var machine in machines)
{
    var cost = GetLowestCostWithOffset(machine, 0);
    totalCost += cost;
}

Console.WriteLine($"Part 1: {totalCost}");
totalCost = 0;
foreach (var machine in machines)
{
    var cost = GetLowestCostWithOffset(machine, 10000000000000);
    {
        totalCost += cost;
    }
}

Console.WriteLine($"Part 2: {totalCost}");

return;

long GetLowestCostWithOffset(Machine machine, long offset)
{
    var totalX = machine.TotalX + offset;
    var totalY = machine.TotalY + offset;
    var det = machine.DiffXA * machine.DiffYB - machine.DiffYA * machine.DiffXB;
    var a = (totalX * machine.DiffYB - totalY * machine.DiffXB) / det;
    var b = (machine.DiffXA * totalY - machine.DiffYA * totalX) / det;
    if (machine.DiffXA * a + machine.DiffXB * b == totalX && machine.DiffYA * a + machine.DiffYB * b == totalY)
    {
        return a * 3 + b;
    }

    return 0;
}

record Machine(int DiffXA, int DiffYA, int DiffXB, int DiffYB, long TotalX, long TotalY);