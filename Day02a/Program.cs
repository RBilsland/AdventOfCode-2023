using System.Runtime.ExceptionServices;

namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 02a");

        int runningTotal = 0;

        string[] lines = File.ReadAllLines("input.txt");
        foreach(string line in lines)
        {
            string[] parts = line.Split(':');
            int gameNo = int.Parse(parts[0].Replace("Game ", ""));
            string[] games = parts[1].Split(";");

            bool goodGame = true;

            foreach (string game in games)
            {
                string[] colours = game.Split(",");

                foreach(string colour in colours)
                {
                    string[] bits = colour.ToLower().Trim().Split(" ");

                    int numberOf = int.Parse(bits[0]);

                    switch(bits[1])
                    {
                        case "red":
                            if (numberOf > 12)
                            {
                                goodGame = false;
                            }
                            break;
                        case "green":
                            if (numberOf > 13)
                            {
                                goodGame = false;
                            }
                            break;
                        case "blue":
                            if (numberOf > 14)
                            {
                                goodGame = false;
                            }
                            break;
                    }
                }
            }

            if (goodGame)
            {
                runningTotal += gameNo;
            }
        }

        Console.WriteLine(runningTotal);

        Console.WriteLine("Complete");
    }
}