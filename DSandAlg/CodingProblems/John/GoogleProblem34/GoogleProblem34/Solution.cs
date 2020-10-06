#region

using NUnit.Framework;

#endregion

namespace GoogleProblem34
{
    /// <summary>
    ///     Given: 2D array of size W * H. Start from the cell at 0,0 and go to the cell at W,0, given the available moves from cell (x,y) are (x+1, y+{-1,0,1}).
    ///     1. In how many ways can you go from Start to End following these rules?
    ///        see below
    ///     2. You must touch coordinates s,t on the way
    ///        Do one pass from start cell (s, t) to end cell (w - 1, 0) and another pass
    ///        from start cell (0, 0) to end cell (s, t). Runtime: O(N)
    ///     3. You must touch height z on the way
    ///        Do one pass with the start cell (0, 0) and end cell (w - 1, h - 1).
    ///        Then, extract all #paths for cells with row z from bottom (height z).
    ///        For each such cell, do another pass with that cell as end point and check
    ///        whether there are any solutions. If there are, multiply the #paths to get
    ///        the answer. Runtime complexity: O(N * #cols).
    ///     4. You must touch {ordered list of heights} in the given order on the way.
    ///        Similar to before, but for each cell with height, do another pass up to
    ///        #rows times. Runtime complexity: O(N * #rows^2)
    ///        NOTE: the list of heights is ordered! That can probably be used to cut down
    ///              on the runtime complexity...
    /// </summary>
    public class Solution
    {
        public int GetNumberOfPaths(int w, int h)
        {
            // SS: runtime complexity: O(3^N) when using Divide and Conquer, N=#grid cells
            // Here, we are using top-down DP with runtime complexity and space complexity O(N)
            var memArray = new int[h][];
            for (var i = 0; i < h; i++)
            {
                memArray[i] = new int[w];

                for (var j = 0; j < w; j++)
                {
                    memArray[i][j] = -1;
                }
            }

            var originRow = 0;
            var originCol = 0;
            GetNumberOfPathsDP(w, h, originRow, originCol, memArray);
            return memArray[originRow][originCol];
        }

        private int GetNumberOfPathsDP(int w, int h, int row, int col, int[][] memArray)
        {
            if (row < 0 || row == h)
            {
                return 0;
            }

            if (col == w - 1)
            {
                return row == 0 ? 1 : 0;
            }

            if (memArray[row][col] != -1)
            {
                return memArray[row][col];
            }

            var p1 = GetNumberOfPathsDP(w, h, row - 1, col + 1, memArray);
            var p2 = GetNumberOfPathsDP(w, h, row, col + 1, memArray);
            var p3 = GetNumberOfPathsDP(w, h, row + 1, col + 1, memArray);

            var numberOfPaths = p1 + p2 + p3;

            memArray[row][col] = numberOfPaths;
            return numberOfPaths;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(6, 21)]
            [TestCase(8, 127)]
            public void Test(int n, int expectedPaths)
            {
                // Arrange

                // Act
                var nPaths = new Solution().GetNumberOfPaths(n, n);

                // Assert
                Assert.AreEqual(expectedPaths, nPaths);
            }
        }
    }
}