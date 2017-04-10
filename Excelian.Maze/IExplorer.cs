namespace Excelian.Maze
{
    public interface IExplorer
    {
        void AutoExploreMaze();
        bool ExploreMaze(int positionX, int positionY, bool[,] visited);
        void MoveTowards(Direction direction);
        void MoveExplorer(int x, int y);
        void PrintPathHistory();
    }
}