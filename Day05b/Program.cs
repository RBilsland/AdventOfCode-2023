using static System.Linq.Enumerable;

namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 05b");

        string[] input = File.ReadAllLines("input.txt");

        long[] inputValues = [];

        int level = 0;

        List<(long baseStart, long baseEnd)> baseSeeds = [];
        List<(int level, long mappingStart, long mappingEnd, long offset)> mappings = [];

        foreach (string line in input)
        {
            if (line.StartsWith("seeds:"))
            {
                inputValues = Array.ConvertAll(line.Replace("seeds:", "").Split(' ', StringSplitOptions.RemoveEmptyEntries), s => long.Parse(s));

                for (int inputPosition = 0; inputPosition < inputValues.Length; inputPosition += 2)
                {
                    baseSeeds.Add((inputValues[inputPosition], inputValues[inputPosition] + inputValues[inputPosition + 1] - 1));
                }
            }
            else if (line == string.Empty)
            {
                level++;

                mappings.Add((level, 0, long.MaxValue, 0));
            }
            else if (!line.Contains(':'))
            {
                long[] parts = Array.ConvertAll(line.Split(' ', StringSplitOptions.RemoveEmptyEntries), s => long.Parse(s));

                mappings.Add((level, parts[1], parts[1] + parts[2] - 1, parts[0] - parts[1]));
            }
        }

        List<(long baseStart, long baseEnd)> ranges = baseSeeds;

        for (int levelCount = 1; levelCount <= level; levelCount++)
        {
            var newRanges = ranges
                .Select(r =>
                (
                    r.baseStart,
                    r.baseEnd,
                    mappings
                        .Where(m => m.level == levelCount)
                        .Select(m => (
                            mappingStart: Math.Max(r.baseStart, m.mappingStart),
                            mappingEnd: Math.Min(r.baseEnd, m.mappingEnd),
                            m.offset,
                            overlap: Math.Max(0, Math.Min(r.baseEnd, m.mappingEnd) - Math.Max(r.baseStart, m.mappingStart) + 1)
                        ))
                        .Where(r => r.overlap > 0)
                        .OrderBy(r => r.mappingStart)
                        .ThenBy(r => r.offset)
                        .ToList()
                ));

            ranges = [];

            foreach ((long baseStart, long baseEnd, List<(long mappingStart, long mappingEnd, long offset, long overlap)> newMappings) in newRanges)
            {
                long currentStart = baseStart;

                foreach ((long mappingStart, long mappingEnd, long offset, long overlap) in newMappings)
                {
                    if (offset != 0)
                    {
                        if (mappingStart > currentStart)
                        {
                            ranges.Add((currentStart, mappingStart - 1));
                        }

                        ranges.Add((mappingStart + offset, mappingEnd + offset));

                        currentStart = mappingEnd + 1;
                    }
                }

                if (currentStart < baseEnd)
                {
                    ranges.Add((currentStart, baseEnd));
                }
            }
        }

        Console.WriteLine(ranges
            .Min(r => r.baseStart));

        Console.WriteLine("Complete");
    }
}