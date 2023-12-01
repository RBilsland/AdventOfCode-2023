namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 01a");

        char[] digits = ['1', '2', '3', '4', '5', '6', '7', '8', '9'];

        int runningTotal = 0;

        string[] lines = File.ReadAllLines("input.txt");
        foreach(string line in lines)
        {
            runningTotal += int.Parse($"{line[line.IndexOfAny(digits)]}{line[line.LastIndexOfAny(digits)]}");
        }

        Console.WriteLine(runningTotal);

        Console.WriteLine("Complete");
    }
}