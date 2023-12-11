namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 08a");

        string[] input = File.ReadAllLines("input.txt");

        string route = input[0];

        Dictionary<string, directionNodes> map = [];
        int stepsTaken = 0;
        string currentNode = "AAA";

        for (int i = 2; i < input.Length; i++)
        {
            string[] parts = input[i].Replace(" ", "").Replace("(","").Replace(")","").Split(new char[] { '=', ',' }, StringSplitOptions.RemoveEmptyEntries);

            map.Add(parts[0], new directionNodes() { left = parts[1], right = parts[2] });
        }

        while (currentNode != "ZZZ") {
            switch (route[stepsTaken % route.Length])
            {
                case 'L':
                    currentNode = map[currentNode].left; 
                    break;
                case 'R':
                    currentNode = map[currentNode].right;
                    break;
            }
            stepsTaken++;
        }

        Console.WriteLine(stepsTaken);

        Console.WriteLine("Complete");
    }

    public class directionNodes
    {
        public string left { get; set; }
        public string right { get; set; }
    }
}