using System.Data;
using static AdventOfCode2023.Program;

namespace AdventOfCode2023;

class Program
{
    public enum Direction
    {
        north,
        south, 
        east,
        west
    }

    readonly static List<DirectionMapping> directionMappings =
    [
        new DirectionMapping { 
            CurrentDirection = Direction.north, 
            XOffset = 0, 
            YOffset = -1, 
            CellMappings = [ 
                new CellMapping { 
                    ValidCell = "|", 
                    NextDirection = Direction.north
                }, 
                new CellMapping { 
                    ValidCell = "7", 
                    NextDirection = Direction.west 
                }, 
                new CellMapping { 
                    ValidCell = "F", 
                    NextDirection = Direction.east 
                } 
            ] 
        },
        new DirectionMapping { 
            CurrentDirection = Direction.east, 
            XOffset = 1, 
            YOffset = 0, 
            CellMappings = [ 
                new CellMapping { 
                    ValidCell = "-", 
                    NextDirection = Direction.east 
                }, 
                new CellMapping {
                    ValidCell = "J", 
                    NextDirection = Direction.north 
                }, 
                new CellMapping { 
                    ValidCell = "7", 
                    NextDirection = Direction.south 
                } 
            ] 
        },
        new DirectionMapping { 
            CurrentDirection = Direction.south, 
            XOffset = 0, 
            YOffset = 1, 
            CellMappings = [ 
                new CellMapping { 
                    ValidCell = "|", 
                    NextDirection = Direction.south 
                }, 
                new CellMapping { 
                    ValidCell = "L", 
                    NextDirection = Direction.east 
                }, 
                new CellMapping { 
                    ValidCell = "J", 
                    NextDirection = Direction.west 
                } 
            ] 
        },
        new DirectionMapping { 
            CurrentDirection = Direction.west, 
            XOffset = -1, 
            YOffset = 0, 
            CellMappings = [ 
                new CellMapping { 
                    ValidCell = "-", 
                    NextDirection = Direction.west 
                }, 
                new CellMapping { 
                    ValidCell = "F", 
                    NextDirection = Direction.south
                }, 
                new CellMapping { 
                    ValidCell = "L", 
                    NextDirection = Direction.north
                } 
            ] 
        }
    ];

    public static void Main()
    {
        Console.WriteLine("Day 10a");

        string[] input = File.ReadAllLines("input.txt");

        Maze maze = new(input);

        Console.WriteLine(maze.Solve());

        Console.WriteLine("Complete");
    }

    public class Maze
    {
        public List<Cell> cells = [];

        public Maze(string[] rows) {
            for (int y = 0; y < rows.Length; y++)
            {
                for (int x = 0; x < rows[y].Length; x++)
                {
                    this.cells
                        .Add(new Cell() { X = x, Y = y, Tile = (rows[y][x]).ToString() });
                }
            }
        }

        public int Solve()
        {
            (int startX, int startY) = this.cells
                .Where(c => c.Tile == "S")
                .Select(c => (c.X, c.Y))
                .FirstOrDefault();

            this.Navigate(startX, startY, Direction.north);
            this.Navigate(startX, startY, Direction.east);
            this.Navigate(startX, startY, Direction.south);
            this.Navigate(startX, startY, Direction.west);

            return this.cells
                .Select(c => c.Steps)
                .Where(s => s < int.MaxValue)
                .OrderByDescending(s => s)
                .FirstOrDefault();
            }

        public void Navigate(int x, int y, Direction direction)
        {
            int steps = 0;

            bool keepGoing = true;

            int nextX = x;
            int nextY = y;
            Direction nextDirection = direction;

            while (keepGoing)
            {
                DirectionMapping directionMapping = directionMappings
                    .Where(dm => dm.CurrentDirection == nextDirection)
                    .FirstOrDefault() ?? new DirectionMapping();

                nextX += directionMapping.XOffset;
                nextY += directionMapping.YOffset;

                IEnumerable<Cell> nextCells = this.cells
                    .Where(c => c.X == nextX && c.Y == nextY);

                if (nextCells.Any())
                {
                    Cell nextCell = nextCells.First();

                    IEnumerable<CellMapping> cellMappings = directionMapping.CellMappings
                        .Where(cm => cm.ValidCell.Contains(nextCell.Tile));

                    if (cellMappings.Any())
                    {
                        CellMapping cellMapping = cellMappings.First();

                        steps++;

                        if (nextCell.Steps <= steps)
                        {
                            keepGoing = false;
                        } else
                        {
                            nextCell.Steps = steps;

                            nextDirection = cellMapping.NextDirection;
                        }
                    } else
                    {
                        keepGoing = false;
                    }
                } else
                {
                    keepGoing = false;
                }
            }
        }
    }

    public class Cell
    {
        public required int X { get; set; }
        public required int Y { get; set; }
        public required string Tile { get; set; }
        public int Steps { get; set; } = int.MaxValue;

    }

    public class DirectionMapping
    {
        public Direction CurrentDirection { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public List<CellMapping> CellMappings { get; set; } = [];
    }

    public class CellMapping {
        public required string ValidCell {  get; set; }
        public required Direction NextDirection { get; set; }
    }
}