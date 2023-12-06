namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 06b");

        string[] input = File.ReadAllLines("input.txt");

        long time = long.Parse(input[0].Replace("Time:", "").Replace(" ", ""));
        long distance = long.Parse(input[1].Replace("Distance:", "").Replace(" ", ""));

        long runningTotal = 1;

        long position = (int)Math.Floor(time / 2.0);

        double maxDistance = position * (time - position);

        if (maxDistance > distance)
        {
            var calculatedDistance = maxDistance;

            while (calculatedDistance > distance && position >= 0)
            {
                position--;
                calculatedDistance = position * (time - position);
            }

            runningTotal *= (time + 1) - ((position + 1) * 2);
        }

        Console.WriteLine(runningTotal);

        Console.WriteLine("Complete");
    }
}