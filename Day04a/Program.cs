namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 04a");

        string[] input = File.ReadAllLines("input.txt");

        var points = input
            .Select(c => c.Split(':', '|'))
            .Select(c => new { No = int.Parse(c[0].Replace("Card ", "")), Winning = Array.ConvertAll(c[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries), s => int.Parse(s)), Chosen = Array.ConvertAll(c[2].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries), s => int.Parse(s)) })
            .Select(c => c.Chosen.Intersect(c.Winning).Count())
            .Where(c => c > 0)
            .Select(c => c == 1 ? 1 : Math.Pow(2, c - 1))
            .Sum();

        Console.WriteLine(points);

        Console.WriteLine("Complete");
    }
}