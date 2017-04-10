using System;

namespace Excelian.Maze
{
    public class MazeConsole : IMazeConsole
    {
        public void SetCursorPosition(int x , int y)
        {
            Console.SetCursorPosition(x,y);
        }
    }
}