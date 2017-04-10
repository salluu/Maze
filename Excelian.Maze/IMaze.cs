namespace Excelian.Maze
{
    public interface IMaze
    {
        void DrawMaze();
        Position GetStartingPositionOfExplorer(string symbol);
        bool CheckRange(int positionX, int positionY);
        bool IsAWall(int positionX, int positionY);
        bool IsExit(int positionX, int positionY);
        int NumberOfColumns { get; }
        int NumberOfRows { get; }
    }
}