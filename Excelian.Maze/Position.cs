using System.Collections.Generic;

namespace Excelian.Maze
{
    public class Position
    {
        public int PositionX;
        public int PositionY;
        public IList<Position> PositionHistory;
        public bool Visited { get; set; }
    }
}