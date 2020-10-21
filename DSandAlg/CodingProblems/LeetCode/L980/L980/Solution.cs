#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// 980. Unique Paths III
// https://leetcode.com/problems/unique-paths-iii/

namespace L980
{
    public class Solution
    {
        private readonly (int, int)[] _neighbors = {(-1, 0), (1, 0), (0, -1), (0, 1)};

        public int UniquePathsIII(int[][] grid)
        {
            var nrows = grid.Length;
            if (nrows == 0)
            {
                return 0;
            }

            var ncols = grid[0].Length;
            if (ncols == 0)
            {
                return 0;
            }

            var startRow = 0;
            var startCol = 0;
            var endRow = 0;
            var endCol = 0;

            var toVisit = new HashSet<(int r, int c)>();

            for (var i = 0; i < nrows; i++)
            {
                for (var j = 0; j < ncols; j++)
                {
                    var cellValue = grid[i][j];

                    if (cellValue == 1)
                    {
                        startRow = i;
                        startCol = j;
                    }
                    else if (cellValue == 2)
                    {
                        endRow = i;
                        endCol = j;
                    }
                    else if (cellValue == 0)
                    {
                        toVisit.Add((i, j));
                    }
                }
            }

            // SS: add end node as we have to visit it
            toVisit.Add((endRow, endCol));

            var nPaths = DFS(grid, nrows, ncols, startRow, startCol, endRow, endCol, toVisit);

            return nPaths;
        }

        private int DFS(int[][] grid, int nrows, int ncols, int row, int col, in int endRow, in int endCol, HashSet<(int r, int c)> toVisit)
        {
            // SS: end reached?
            if (row == endRow && col == endCol)
            {
                return toVisit.Any() == false ? 1 : 0;
            }

            var cellValue = grid[row][col];
            if (cellValue == -1)
            {
                // SS: not allowed
                return 0;
            }

            // SS: must be a 0-cell
            var nPaths = 0;

            for (var i = 0; i < _neighbors.Length; i++)
            {
                (var ro, var co) = _neighbors[i];

                var newRow = row + ro;
                var newCol = col + co;

                if (newRow < 0 || newRow == nrows || newCol < 0 || newCol == ncols)
                {
                    continue;
                }

                if (toVisit.Contains((newRow, newCol)) == false)
                {
                    // SS: we have already visited this cell
                    continue;
                }

                toVisit.Remove((newRow, newCol));

                nPaths += DFS(grid, nrows, ncols, newRow, newCol, endRow, endCol, toVisit);

                // SS: re-add cell for later paths
                toVisit.Add((newRow, newCol));
            }

            return nPaths;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] grid =
                {
                    new[] {1, 0, 0, 0}
                    , new[] {0, 0, 0, 0}
                    , new[] {0, 0, 2, -1}
                };

                // Act
                var nPaths = new Solution().UniquePathsIII(grid);

                // Assert
                Assert.AreEqual(2, nPaths);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] grid =
                {
                    new[] {1, 0, 0, 0}
                    , new[] {0, 0, 0, 0}
                    , new[] {0, 0, 0, 2}
                };

                // Act
                var nPaths = new Solution().UniquePathsIII(grid);

                // Assert
                Assert.AreEqual(4, nPaths);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[][] grid =
                {
                    new[] {0, 1}
                    , new[] {2, 0}
                };

                // Act
                var nPaths = new Solution().UniquePathsIII(grid);

                // Assert
                Assert.AreEqual(0, nPaths);
            }
        }
    }
}