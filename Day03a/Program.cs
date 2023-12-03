namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 03a");

        int runningTotal = 0;

        Engine engine = new(File.ReadAllLines("input.txt"));

        string number = string.Empty;
        bool adjacentSymbolFound = false;

        int yPos = 0;
        while (yPos < engine.schematic.Length)
        {
            int xPos = 0;
            while (xPos < engine.schematic[yPos].Length)
            {
                if (char.IsDigit(engine.schematic[yPos][xPos]))
                {
                    number += engine.schematic[yPos][xPos];

                    if (engine.AdjacentSymbol(xPos, yPos))
                    {
                        adjacentSymbolFound = true;
                    }
                } else
                {
                    runningTotal += number != string.Empty & adjacentSymbolFound ? int.Parse(number) : 0;

                    number = string.Empty;
                    adjacentSymbolFound = false;
                }

                xPos++;
            }

            runningTotal += number != string.Empty & adjacentSymbolFound ? int.Parse(number) : 0;

            number = string.Empty;
            adjacentSymbolFound = false;

            yPos++;
        }

        runningTotal += number != string.Empty & adjacentSymbolFound ? int.Parse(number) : 0;

        Console.WriteLine(runningTotal);

        Console.WriteLine("Complete");
    }

    public class Engine(string[] schematic)
    {
        public readonly string[] schematic = schematic;

        private readonly List<Offset> offsets =
        [
            new Offset() { xOffset = -1, yOffset = -1 },
            new Offset() { xOffset = 0, yOffset = -1 },
            new Offset() { xOffset = 1, yOffset = -1 },
            new Offset() { xOffset = -1, yOffset = 0 },
            new Offset() { xOffset = 1, yOffset = 0 },
            new Offset() { xOffset = -1, yOffset = 1 },
            new Offset() { xOffset = 0, yOffset = 1 },
            new Offset() { xOffset = 1, yOffset = 1 },
        ];

        private readonly char[] nonSymbolChars = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.'];

        public bool AdjacentSymbol(int xPos, int yPos)
        {
            bool adjacentSymbol = false;

            foreach(Offset offset in offsets) {
                if (CheckForSymbol(xPos + offset.xOffset, yPos + offset.yOffset))
                {
                    adjacentSymbol = true;
                }
            }

            return adjacentSymbol;
        }

        public bool CheckForSymbol(int xPos, int yPos)
        {
            bool symbol = false;

            if (yPos > -1 && yPos < schematic.Length)
            {
                if (xPos > -1 && xPos < schematic[yPos].Length)
                {
                    if (!nonSymbolChars.Contains(schematic[yPos][xPos]))
                    {
                        symbol = true;
                    }
                }
            }

            return symbol;
        }

        private class Offset()
        {
            public int xOffset;
            public int yOffset;
        }
    }
}