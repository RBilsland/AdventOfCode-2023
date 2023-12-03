using System.Runtime.ExceptionServices;

namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 02b");

        int runningTotal = 0;

        string[] lines = File.ReadAllLines("input.txt");
        foreach (string line in lines)
        {
            string[] parts = line.Split(':');
            int gameNo = int.Parse(parts[0].Replace("Game ", ""));
            string[] games = parts[1].Split(";");

            int maxRed = 0;
            int maxGreen = 0;
            int maxBlue = 0;

            foreach (string game in games)
            {
                string[] colours = game.Split(",");

                foreach (string colour in colours)
                {
                    string[] bits = colour.ToLower().Trim().Split(" ");

                    int numberOf = int.Parse(bits[0]);

                    switch (bits[1])
                    {
                        case "red":
                            if (numberOf > maxRed)
                            {
                                maxRed = numberOf;
                            }
                            break;
                        case "green":
                            if (numberOf > maxGreen)
                            {
                                maxGreen = numberOf;
                            }
                            break;
                        case "blue":
                            if (numberOf > maxBlue)
                            {
                                maxBlue = numberOf;
                            }
                            break;
                    }
                }
            }

            runningTotal += (maxRed * maxGreen * maxBlue);
        }

        Console.WriteLine(runningTotal);

        Console.WriteLine("Complete");
    }
}