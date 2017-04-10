using System;
using System.Collections.Generic;
using System.Threading;

namespace Excelian.Maze
{
    public class Explorer : IExplorer
    {

        private readonly IMaze _maze;
        private readonly IMazeConsole _console;
        public Position CurrentPosition;
        public  bool Finished;
        public Explorer(IMaze maze, IMazeConsole console)
        {
            _maze = maze;
            _console = console;
            CurrentPosition = _maze.GetStartingPositionOfExplorer(Symbol.Start);
            CurrentPosition.PositionHistory = new List<Position>();
        }      
      
        public void MoveTowards(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    MoveExplorer(0, -1);
                    break;

                case Direction.Right:
                    MoveExplorer(1, 0);
                    break;

                case Direction.Down:
                    MoveExplorer(0, 1);
                    break;

                case Direction.Left:
                    MoveExplorer(-1, 0);
                    break;
            }
        }
        public void MoveExplorer(int x, int y)
        {
            try
            {
                var positionX = CurrentPosition.PositionX + x;
                var positionY = CurrentPosition.PositionY + y;

                if (!_maze.CheckRange(positionX, positionY)) return;

                if (_maze.IsExit(positionX, positionY))
                {
                    Finished = true;

                }
                if (!_maze.IsAWall(positionX, positionY))
                {
                    CurrentPosition.PositionX = positionX;
                    CurrentPosition.PositionY = positionY;
                    PrintPath(positionX, positionY);
                    CurrentPosition.PositionHistory.Add(new Position() { PositionX = positionX, PositionY = positionY });

                }
                else
                {
                    PrintPath(CurrentPosition.PositionX, CurrentPosition.PositionY);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.Beep();
                PrintPath(CurrentPosition.PositionX, CurrentPosition.PositionY);
            }
        }
        public void AutoExploreMaze()
        {
            var alreadySearched = new bool[_maze.NumberOfRows, _maze.NumberOfColumns];
            ExploreMaze(CurrentPosition.PositionX, CurrentPosition.PositionY, alreadySearched);
        }

        public bool ExploreMaze(int positionX, int positionY, bool[,] visited)
        {
            var correctPath = false;
            var shouldContinue = true;

            if (positionX >= _maze.NumberOfRows || positionX < 0 || positionY >= _maze.NumberOfColumns || positionY < 0)
                shouldContinue = false;
            else
            {
                if (_maze.IsExit(positionX, positionY))
                {
                    correctPath = true;
                    shouldContinue = false;
                }

                if (_maze.IsAWall(positionX, positionY))
                    shouldContinue = false;
                else
                    PrintExplorerPath(positionX, positionY);

                if (visited[positionX, positionY])
                    shouldContinue = false;

            }

            if (shouldContinue)
            {

                visited[positionX, positionY] = true;
                //right
                correctPath = ExploreMaze(positionX + 1, positionY, visited);
                //down
                correctPath = correctPath || ExploreMaze(positionX, positionY + 1, visited);
                //left
                correctPath = correctPath || ExploreMaze(positionX - 1, positionY, visited);
                //up
                correctPath = correctPath || ExploreMaze(positionX, positionY - 1, visited);
            }
            return correctPath;
        }


        private void PrintExplorerPath(int positionX, int positionY)
        {
            Thread.Sleep(100);
            Console.ForegroundColor = ConsoleColor.Green;
            _console.SetCursorPosition(positionX, positionY);
            Console.Write(Symbol.Path);
            CurrentPosition.PositionHistory.Add(new Position() { PositionX = positionX, PositionY = positionY });

        }

        public void PrintPathHistory()
        {
            foreach (var path in CurrentPosition.PositionHistory)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                _console.SetCursorPosition(path.PositionX, path.PositionY);
                Console.Write(Symbol.Path);

            }
            
        }
        private void PrintPath(int x, int y )
        {
            _console.SetCursorPosition(x,y);
            Console.Write(Symbol.Start);
            _console.SetCursorPosition(x,y);
        }
    }
}
