using static System.Linq.Enumerable;

namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 06a");

        string[] input = File.ReadAllLines("input.txt");

        int[] time = Array.ConvertAll(input[0].Replace("Time:", "").Split(' ', StringSplitOptions.RemoveEmptyEntries), s => int.Parse(s));
        int[] distance = Array.ConvertAll(input[1].Replace("Distance:", "").Split(' ', StringSplitOptions.RemoveEmptyEntries), s => int.Parse(s));

        int runningTotal = 1;

        for(int i = 0; i < time.Length; i++)
        {
            int position = (int)Math.Floor(time[i] / 2.0);

            double maxDistance = position * (time[i] - position);

            if (maxDistance > distance[i])
            {
                var calculatedDistance = maxDistance;

                while(calculatedDistance > distance[i] && position >= 0)
                {
                    position--;
                    calculatedDistance = position * (time[i] - position);
                }

                runningTotal *= (time[i] + 1) - ((position + 1) * 2);
            }
        }

        Console.WriteLine(runningTotal);

        Console.WriteLine("Complete");
    }
}