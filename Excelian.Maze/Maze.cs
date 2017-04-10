using System;
using Excelian.Maze.Data;

namespace Excelian.Maze
{
    public class Maze : IMaze
    {
        private readonly Map _map;
        public Maze(IMapResources mapResources)
        {
            _map = mapResources.GetMap();
        }


        public void DrawMaze()
        {
            Console.WriteLine(_map.MapDraw);
        }
        public Position GetStartingPositionOfExplorer(string symbol)
        {
            for (var i = 0; i < _map.MappedArray.GetLength(0); i++)
            {
                for (var j = 0; j < _map.MappedArray.GetLength(1); j++)
                {
                    if (_map.MappedArray[i, j].ToString().Equals(symbol))
                        return new Position() {PositionX = i, PositionY = j};
                }
            }

            return new Position() {PositionX = -1, PositionY = -1};
            ;
        }
        public bool CheckRange(int positionX, int positionY)
        {
            if (positionX < 0 || positionX > _map.MappedArray.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(positionX));
            if (positionY < 0 || positionY > _map.MappedArray.GetLength(1))
                throw new ArgumentOutOfRangeException(nameof(positionY));

            return true;
        }

        public bool IsAWall(int positionX, int positionY)
        {
            var symbol = _map.MappedArray[positionY, positionX].ToString();
            return symbol.Equals(Symbol.Wall);
        }

        public bool IsExit(int positionX, int positionY)
        {
            var symbol = _map.MappedArray[positionY, positionX].ToString();
            return symbol.Equals(Symbol.Exit);
        }

        public int NumberOfColumns => _map.Columns;
        public int NumberOfRows => _map.Rows;
    }
}
