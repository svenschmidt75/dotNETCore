namespace GoogleProblem33
{
    public interface IBoardInitializer
    {
        void Initialize(int nrows, int ncols, int nBombs, int[][][] grid);
    }
}