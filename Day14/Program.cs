// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using Day14;

Console.WriteLine("Day 14");
var regex = new Regex("-{0,1}\\d+");
const int sizeX = 101;
const int sizeY = 103;
var input = File.ReadAllLines("input.txt").Select(item =>
{
    var param = regex.Matches(item);
    return new Robot(int.Parse(param[0].Value), int.Parse(param[1].Value), int.Parse(param[2].Value), int.Parse(param[3].Value), sizeX, sizeY);
}).ToList();

var quadrants = new int[4];
var quadrantXSkip = sizeX / 2;
var quadrantYSkip = sizeY / 2;
foreach (var robot in input)
{
    robot.Move(sizeX, sizeY, 100);

    if (robot.PosX < quadrantXSkip && robot.PosY < quadrantYSkip)
    {
        quadrants[0]++;
    }
    else if (robot.PosX > quadrantXSkip && robot.PosY < quadrantYSkip)
    {
        quadrants[1]++;
    }
    else if (robot.PosX < quadrantXSkip && robot.PosY > quadrantYSkip)
    {
        quadrants[2]++;
    }
    else if (robot.PosX > quadrantXSkip && robot.PosY > quadrantYSkip)
    {
        quadrants[3]++;
    }
}

var result = quadrants.Aggregate(1, (current, quadrant) => current * quadrant);
Console.WriteLine($"Part 1: {result}");


input = File.ReadAllLines("input.txt").Select(item =>
{
    var param = regex.Matches(item);
    return new Robot(int.Parse(param[0].Value), int.Parse(param[1].Value), int.Parse(param[2].Value), int.Parse(param[3].Value), sizeX, sizeY);
}).ToList();

result = int.MaxValue;
var time = 1;
for (var i = 1; i < 100_000; i++)
{
    Array.Clear(quadrants);
    foreach (var robot in input)
    {
        robot.Move(sizeX, sizeY, 1);

        if (robot.PosX < quadrantXSkip && robot.PosY < quadrantYSkip)
        {
            quadrants[0]++;
        }
        else if (robot.PosX > quadrantXSkip && robot.PosY < quadrantYSkip)
        {
            quadrants[1]++;
        }
        else if (robot.PosX < quadrantXSkip && robot.PosY > quadrantYSkip)
        {
            quadrants[2]++;
        }
        else if (robot.PosX > quadrantXSkip && robot.PosY > quadrantYSkip)
        {
            quadrants[3]++;
        }
    }
    
    var currentResult = quadrants.Aggregate(1, (current, quadrant) => current * quadrant);
    if (currentResult < result)
    {
        result = currentResult;
        time = i;
    }
}

Console.WriteLine($"Part 2: {time}");