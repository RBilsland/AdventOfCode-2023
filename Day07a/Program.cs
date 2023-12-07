using System.Threading.Tasks.Sources;

namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 07a");

        string cardOrder = "AKQJT98765432";

        string[] input = File.ReadAllLines("input.txt");

        List<(int bid, int score, int card1Value, int card2Value, int card3Value, int card4Value, int card5Value)> readyForSorting = [];

        foreach(string row in input)
        {
            string[] parts = row.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var results = parts[0]
                .GroupBy(r => r)
                .Select(r => (character: r.Key, occurances: r.Count()))
                .OrderByDescending(r => r.occurances)
                .ToList();

            int score = 0;

            if (results[0].occurances == 5)
            {
                score = 7;
            } 
            else if (results[0].occurances == 4)
            {
                score = 6;
            }
            else if (results[0].occurances == 3)
            {
                if (results[1].occurances == 2)
                {
                    score = 5;
                } 
                else
                {
                    score = 4;
                }
            }
            else if (results[0].occurances == 2)
            {
                if (results[1].occurances == 2)
                {
                    score = 3;
                }
                else
                {
                    score = 2;
                }
            }
            else if (results[0].occurances == 1) 
            {
                score = 1;
            }

            var cardValues = parts[0]
                .Select(r => cardOrder.IndexOf(r))
                .ToList();

            readyForSorting.Add((int.Parse(parts[1]), score, cardValues[0], cardValues[1], cardValues[2], cardValues[3], cardValues[4]));
        }

        List<(int bid, int score, int card1Value, int card2Value, int card3Value, int card4Value, int card5Value)> sorted = readyForSorting
            .OrderBy(r => r.score)
            .ThenByDescending(r => r.card1Value)
            .ThenByDescending(r => r.card2Value)
            .ThenByDescending(r => r.card3Value)
            .ThenByDescending(r => r.card4Value)
            .ThenByDescending(r => r.card5Value)
            .ToList();

        int runningTotal = 0;

        for(int i = 0; i < sorted.Count; i++)
        {
            runningTotal += sorted[i].bid * (i + 1);
        }

        Console.WriteLine(runningTotal);

        Console.WriteLine("Complete");
    }
}