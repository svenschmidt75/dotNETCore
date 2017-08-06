namespace MazeSolver
{
    public interface IMaze
    {
        bool IsWall(int x, int y);

        int Width { get; }

        int Height { get; }
    }
}