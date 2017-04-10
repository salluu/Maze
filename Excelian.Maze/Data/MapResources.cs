using System.IO;

namespace Excelian.Maze.Data
{
    public class MapResources : IMapResources
    {
        public Map GetMap()
        {
            var map = new Map();
            var mapLines = File.ReadAllLines(Configuration.ExampleMaze);
            map.MapDraw = File.ReadAllText(Configuration.ExampleMaze);

            map.Rows = mapLines.Length;
            map.Columns = mapLines[0].Length;

            map.MappedArray = new char[map.Rows, map.Columns];

            for (var i = 0; i < map.MappedArray.GetLength(0); i++)
            {
                for (var j = 0; j < map.MappedArray.GetLength(1); j++)
                {
                    map.MappedArray[i, j] = mapLines[i][j];
                }
            }
            return map;

        }

    }
}