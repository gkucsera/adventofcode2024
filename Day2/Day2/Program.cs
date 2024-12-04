// See https://aka.ms/new-console-template for more information

Console.WriteLine("Day 2");

var reports = File.ReadAllLines("input.txt").Select(item => item.Split(" ").Select(int.Parse).ToArray()).ToList();

var numberOfSafeResults = 0;

foreach (var report in reports)
{
    if (report[0] == report[1])
        continue;

    var isSafe = true;

    var isDecreasing = report[1] < report[0];
    for (var i = 1; i < report.Length; i++)
    {
        if (report[i] == report[i - 1])
        {
            isSafe = false;
            break;
        }

        var diff = isDecreasing ? report[i - 1] - report[i] : report[i] - report[i - 1];
        if (diff < 1 || diff > 3)
        {
            isSafe = false;
            break;
        }
    }

    if (isSafe)
    {
        numberOfSafeResults++;
    }
}

Console.WriteLine($"Part 1: {numberOfSafeResults}");

numberOfSafeResults = 0;

foreach (var report in reports)
{
    var reportList = report.ToList();
    var isSafe = CheckReport(reportList);
    if (!isSafe)
    {
        for (var i = 0; i < report.Length; i++)
        {
            var tempList = report.ToList();
            tempList.RemoveAt(i);
            isSafe = CheckReport(tempList);
            if (isSafe)
            {
                break;
            }
        }
    }
    

    if (isSafe)
    {
        numberOfSafeResults++;
    }
}

Console.WriteLine($"Part 2: {numberOfSafeResults}");

bool CheckReport(List<int> report)
{
    if (report[0] == report[1])
        return false;

    var isDecreasing = report[1] < report[0];
    for (var i = 1; i < report.Count; i++)
    {
        var diff = isDecreasing ? report[i - 1] - report[i] : report[i] - report[i - 1];
        if (diff < 1 || diff > 3)
        {
            return false;
        }
    }

    return true;
}

