using System;

namespace GoogleProblem33
{
    public class BoardInitializer : IBoardInitializer
    {
        void IBoardInitializer.Initialize(int nrows, int ncols, int nBombs, int[][][] grid)
        {
            var rnd = new Random(DateTime.Now.Millisecond);

            var maxIndex = nrows * ncols;

            var nb = 0;
            while (nb < nBombs)
            {
                var gridIdx = rnd.Next(0, maxIndex);

                var row = gridIdx / ncols;
                var col = gridIdx - row * ncols;

                if (grid[row][col][1] == -1)
                {
                    // grid already contains a bomb
                    continue;
                }

                nb++;

                var isBomb = rnd.Next(100) > 50;
                var cellValue = isBomb ? -1 : 0;
                grid[row][col][1] = cellValue;
            }
        }
    }
}