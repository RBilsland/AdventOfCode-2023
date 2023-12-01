namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 01b");

        Dictionary<string, int> keyValuePairs = new()
        {
            {"1", 1 },
            {"one", 1 },
            {"2", 2 },
            {"two", 2 },
            {"3", 3 },
            {"three", 3 },
            {"4", 4 },
            {"four", 4 },
            {"5", 5 },
            {"five", 5 },
            {"6", 6 },
            {"six", 6 },
            {"7", 7 },
            {"seven", 7 },
            {"8", 8 },
            {"eight", 8 },
            {"9", 9 },
            {"nine", 9 },
        };

        int runningTotal = 0;

        string[] lines = File.ReadAllLines("input.txt");
        foreach (string line in lines)
        {
            var Firsts = keyValuePairs
                .Select(kvp => new { kvp.Value, Position = line.IndexOf(kvp.Key) })
                .Where(r => r.Position > -1)
                .OrderBy(r => r.Position)
                .Select(r => r.Value);

            var Lasts = keyValuePairs
                .Select(kvp => new { kvp.Value, Position = line.LastIndexOf(kvp.Key) })
                .Where(r => r.Position > -1)
                .OrderByDescending(r => r.Position)
                .Select(r => r.Value);

            Console.WriteLine($"{line},{Firsts.First()},{Lasts.First()}");

            runningTotal += Firsts.First() * 10;
            runningTotal += Lasts.First();
        }

        Console.WriteLine(runningTotal);

        Console.WriteLine("Complete");
    }
}