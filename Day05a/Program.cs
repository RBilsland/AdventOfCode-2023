using static System.Linq.Enumerable;

namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 05a");

        string[] input = File.ReadAllLines("input.txt");

        long[] seeds = [];
        Dictionary<string, Mapper> mappings = [];

        string currentName = string.Empty;
        Mapper currentMapper = new();

        foreach(string line in input)
        {
            if (line.StartsWith("seeds:"))
            {
                seeds = Array.ConvertAll(line.Replace("seeds:", "").Split(' ', StringSplitOptions.RemoveEmptyEntries), s => long.Parse(s));
            }
            else if (line.Contains(':'))
            {
                if (currentName != string.Empty)
                {
                    mappings.Add(currentName, currentMapper);
                }

                currentName = line.Replace(" map:", "");
                currentMapper = new();
            }
            else if (line.Trim() != string.Empty)
            {
                if (currentName != string.Empty)
                {
                    long[] parts = Array.ConvertAll(line.Split(' ', StringSplitOptions.RemoveEmptyEntries), s => long.Parse(s));

                    currentMapper.UpdateMapping(parts[0], parts[1], Convert.ToInt32(parts[2]));
                }
            }
        }

        if (currentName != string.Empty)
        {
            mappings.Add(currentName, currentMapper);
        }

        long lowestSeed = long.MaxValue;

        foreach(long seed in seeds)
        {
            long currentSeed = seed;

            foreach(KeyValuePair<string, Mapper> mapping in mappings)
            {
                currentSeed = mapping.Value.ApplyMapping(currentSeed);
            }

            if (lowestSeed > currentSeed)
            { 
                lowestSeed = currentSeed;
            }
        }

        Console.WriteLine(lowestSeed.ToString());

        Console.WriteLine("Complete");
    }

    public class Mapper
    {
        public string SourceName = string.Empty;
        public string DestinationName = string.Empty;
        private readonly List<Range> _ranges = [];

        public void UpdateMapping(long destinationRangeStart, long sourceRangeStart, int rangeLength)
        {
            _ranges.Add(new Range()
            {
                SourceRangeStart = sourceRangeStart,
                SourceRangeEnd = sourceRangeStart + rangeLength - 1,
                DestinationOffset = destinationRangeStart - sourceRangeStart
            });
        }

        public long ApplyMapping(long inbound)
        {
            return inbound + _ranges
                .Where(r => inbound >= r.SourceRangeStart && inbound <= r.SourceRangeEnd)
                .Select(r => r.DestinationOffset)
                .FirstOrDefault();
        }
    }

    public class Range
    {
        public long SourceRangeStart { get; set; }
        public long SourceRangeEnd {get; set; }
        public long DestinationOffset { get; set; }
    }
}
