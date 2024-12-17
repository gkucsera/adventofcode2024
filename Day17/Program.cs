// See https://aka.ms/new-console-template for more information

using Day17;

Console.WriteLine("Day 17");

var input = File.ReadAllLines("input.txt").Select(item => item.Split(' ')).ToArray();

var regA = long.Parse(input[0].Last());
var regB = int.Parse(input[1].Last());
var regC = int.Parse(input[2].Last());

var operations = input[4].Last().Split(',').Select(int.Parse).ToArray();
var cpu = new Cpu(regA, regB, regC, operations);
cpu.RunProgram();

var result = string.Join(",", cpu.Output);

Console.WriteLine($"part 1: {result}");


var regAList = new List<long> { 1 };

for (var i = operations.Length - 1; i >= 0; i--)
{
    var temp = new List<long>();
    var currentResult = operations[i];
    for (var j = 0; j < 8; j++)
    {
        foreach (var item in regAList)
        {
            var a = item + j;
            cpu = new Cpu(item + j, 0, 0, operations);
            cpu.RunProgram();
            if (cpu.Output.First() == currentResult)
            {
                temp.Add(a * 8);
            }
        }
    }

    regAList = temp;
}

Console.WriteLine($"part 2: {regAList.Min() / 8}");

return;