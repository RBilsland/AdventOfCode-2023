namespace AdventOfCode2023;

class Program
{
    internal static readonly char[] separator = ['=', ','];

    public static void Main()
    {
        Console.WriteLine("Day 08b");

        string[] input = File.ReadAllLines("input.txt");

        string routeToTake = input[0];

        Dictionary<string, DirectionNodes> map = [];

        for (int i = 2; i < input.Length; i++)
        {
            string[] parts = input[i].Replace(" ", "").Replace("(", "").Replace(")", "").Split(separator, StringSplitOptions.RemoveEmptyEntries);

            map.Add(parts[0], new DirectionNodes() { Left = parts[1], Right = parts[2] });
        }

        long stepsTaken;
        long answer = 1;

        foreach (string startNode in map
            .Where(m => m.Key.EndsWith('A'))
            .Select(m => m.Key ))
        {
            string currentNode = startNode;
            stepsTaken = 0;

            while (!currentNode.EndsWith('Z'))
            {
                currentNode = routeToTake[(int)(stepsTaken % routeToTake.Length)] == 'L' ? map[currentNode].Left : map[currentNode].Right;

                stepsTaken++;
            }

            answer = LowestCommonMultiple(answer, stepsTaken);
        };

        Console.WriteLine(answer);

        Console.WriteLine("Complete");
    }

    public static long GreatestCommonDivisor(long number1, long number2)
    {
        while (number2 != 0)
        {
            long numberStore = number2;
            number2 = number1 % number2;
            number1 = numberStore;
        }

        return number1;
    }

    public static long LowestCommonMultiple(long number1, long number2)
    {
        return (number1 * number2) / GreatestCommonDivisor(number1, number2);
    }

    public class DirectionNodes
    {
        public required string Left { get; set; }
        public required string Right { get; set; }
    }
}