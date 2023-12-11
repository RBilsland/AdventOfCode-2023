namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 09b");

        string[] input = File.ReadAllLines("input.txt");

        List<long> values;
        long runningTotal = 0;

        foreach (string line in input)
        {
            values = line
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => long.Parse(s))
                .Reverse()
                .ToList();

            runningTotal += CalculateNextDigit(values);
        }

        Console.WriteLine(runningTotal);

        Console.WriteLine("Complete");
    }

    public static long CalculateNextDigit(List<long> calculationValues)
    {
        List<long> valueDifferences = [];

        for (int i = 1; i < calculationValues.Count; i++)
        {
            valueDifferences.Add(calculationValues[i] - calculationValues[i - 1]);
        }

        long returnValue = calculationValues.Last();

        if (valueDifferences
            .Where(v => v == 0)
            .Count() != valueDifferences.Count)
        {
            returnValue += CalculateNextDigit(valueDifferences);
        }

        return returnValue;
    }
}