using System;
using Excelian.Maze;
using Excelian.Maze.Data;

namespace Excelian.MazeRunner
{
    class Program
    {
        static void Main(string[] args)
        {

            var maze = new Maze.Maze(new MapResources());
            maze.DrawMaze();
            
            var explorer = new Explorer(maze, new MazeConsole());
            Console.SetCursorPosition(explorer.CurrentPosition.PositionX, explorer.CurrentPosition.PositionY);

            var direction = Direction.Up;
            while (!explorer.Finished)
            {
                var command = Console.ReadKey().Key;

                switch (command)
                {
                    case ConsoleKey.DownArrow:
                        direction = Direction.Down;
                        break;
                    case ConsoleKey.UpArrow:
                        direction = Direction.Up;
                        break;
                    case ConsoleKey.LeftArrow:
                        direction = Direction.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        direction = Direction.Right;
                        break;
                }


                explorer.MoveTowards(direction);
            }

            explorer.PrintPathHistory();
            Console.WriteLine();
            Console.WriteLine("AutoExplore will start now! Please press enter");
            Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;

            explorer = new Explorer(maze, new MazeConsole());
            maze.DrawMaze();
            Console.SetCursorPosition(explorer.CurrentPosition.PositionX, explorer.CurrentPosition.PositionY);

            explorer.AutoExploreMaze();

            Console.WriteLine();


        }


    }
}
