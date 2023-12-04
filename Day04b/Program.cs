namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 04b");

        string[] input = File.ReadAllLines("input.txt");

        var scratchCards = input
            .Select(sc => sc.Split(':', '|'))
            .Select(sc => new { 
                Winning = Array.ConvertAll(sc[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries), s => int.Parse(s)), 
                Chosen = Array.ConvertAll(sc[2].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries), s => int.Parse(s))
            })
            .Select(sc => new { 
                Matches = sc.Chosen.Intersect(sc.Winning).Count(), 
                Occurrences = 1 
            })
            .ToArray();

        for(int position = 0; position < scratchCards.Length; position++)
        {
            for (int offset = 1; offset < scratchCards[position].Matches + 1; offset++)
            {
                if (position + offset < scratchCards.Length)
                {
                    scratchCards[position + offset] = new {
                        scratchCards[position + offset].Matches, 
                        Occurrences = scratchCards[position].Occurrences + scratchCards[position + offset].Occurrences
                    };
                }
            }
        }

        Console.WriteLine(scratchCards
            .Sum(sc => sc.Occurrences));

        Console.WriteLine("Complete");
    }
}