namespace AdventOfCode2023;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Day 03b");

        Engine engine = new(File.ReadAllLines("input.txt"));

        Console.WriteLine(engine.FindGears());

        Console.WriteLine("Complete");
    }

    public class Engine(string[] schematic)
    {
        public readonly string[] schematic = schematic;

        private readonly List<Coordinate> offsets =
        [
            new Coordinate() { xPos = -1, yPos = -1 },
            new Coordinate() { xPos = 0, yPos = -1 },
            new Coordinate() { xPos = 1, yPos = -1 },
            new Coordinate() { xPos = -1, yPos = 0 },
            new Coordinate() { xPos = 1, yPos = 0 },
            new Coordinate() { xPos = -1, yPos = 1 },
            new Coordinate() { xPos = 0, yPos = 1 },
            new Coordinate() { xPos = 1, yPos = 1 },
        ];

        public int FindGears()
        {
            int runningTotal = 0;

            int yPos = 0;

            while(yPos < schematic.Length)
            {
                int xPos = 0;

                while(xPos < schematic[yPos].Length)
                {
                    if (schematic[yPos][xPos] == '*')
                    {
                        runningTotal += FindGearNumbers(xPos, yPos);
                    }

                    xPos++;
                }

                yPos++;
            }

            return runningTotal;
        }

        private int FindGearNumbers(int xPos, int yPos)
        {
            List<NumberRange> numberRanges = [];

            foreach (Coordinate offset in offsets)
            {
                if (char.IsDigit(schematic[yPos + offset.yPos][xPos + offset.xPos]))
                {
                    int firstXPos = FindEnd(xPos + offset.xPos, yPos + offset.yPos, -1);
                    int lastXPos = FindEnd(xPos + offset.xPos, yPos + offset.yPos, 1);

                    NumberRange numberRange = new NumberRange()
                    {
                        xPos = firstXPos,
                        length = lastXPos - firstXPos + 1, 
                        yPos = yPos + offset.yPos
                    };

                    if (!numberRanges.Contains(numberRange))
                    {
                        numberRanges.Add(numberRange);
                    }
                }
            }

            int firstGear = 0;
            int secondGear = 0;

            if (numberRanges.Count == 2)
            {
                firstGear = int.Parse(schematic[numberRanges[0].yPos].Substring(numberRanges[0].xPos, numberRanges[0].length)); 
                secondGear = int.Parse(schematic[numberRanges[1].yPos].Substring(numberRanges[1].xPos, numberRanges[1].length));
            }

            return firstGear * secondGear;
        }

        private int FindEnd(int xPos, int yPos, int direction)
        {
            while(xPos + direction > -1 && xPos + direction < schematic[yPos].Length && char.IsDigit(schematic[yPos][xPos + direction]))
            {
                xPos += direction;
            }

            return xPos;
        }

        public class Coordinate()
        {
            public int xPos;
            public int yPos;
        }

        public class NumberRange() : IEquatable<NumberRange>
        {
            public int xPos;
            public int length;
            public int yPos;

            public bool Equals(NumberRange other)
            {
                return this.xPos == other.xPos &&
                       this.length == other.length &&
                       this.yPos == other.yPos;
            }
        }
    }
}