#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 200. Number of Islands
// URL: https://leetcode.com/problems/number-of-islands/

namespace LeetCode
{
    public class Solution
    {
        public int NumIslands(char[][] grid)
        {
            // SS: instead of explicitly using a hash set to keep track of 
            // the visited cells, we set a 1 cell to 0 after we have visited it.
            var nrows = grid.Length;
            var ncols = grid[0].Length;

            var nIslands = 0;

            for (var r = 0; r < nrows; r++)
            {
                for (var c = 0; c < ncols; c++)
                {
                    int cellValue = grid[r][c];
                    if (cellValue == '1')
                    {
                        nIslands++;

                        // SS: explore island
                        DFS(r, c, grid);
                    }
                }
            }

            return nIslands;
        }

        private void DFS(int r, int c, char[][] grid)
        {
            var nrows = grid.Length;
            var ncols = grid[0].Length;

            if (r < 0 || r == nrows || c < 0 || c == ncols)
            {
                return;
            }

            if (grid[r][c] == '0')
            {
                return;
            }

            // SS: set call visited
            grid[r][c] = '0';

            DFS(r - 1, c, grid);
            DFS(r + 1, c, grid);
            DFS(r, c - 1, grid);
            DFS(r, c + 1, grid);
        }

        public int NumIslandsSlow(char[][] grid)
        {
            // SS: DFS with runtime complexity O(R * C)
            // space complexity: O(R * C) due to call stack and hash set

            var nrows = grid.Length;
            var ncols = grid[0].Length;

            var visited = new HashSet<(int, int)>();

            var nIslands = 0;

            for (var r = 0; r < nrows; r++)
            {
                for (var c = 0; c < ncols; c++)
                {
                    if (visited.Contains((r, c)))
                    {
                        continue;
                    }

                    visited.Add((r, c));

                    int cellValue = grid[r][c];
                    if (cellValue == '1')
                    {
                        nIslands++;

                        // SS: explore island
                        DFS(r, c, visited, grid);
                    }
                }
            }

            return nIslands;
        }

        private void DFS(int r, int c, HashSet<(int, int)> visited, char[][] grid)
        {
            (int, int)[] neighbors = {(-1, 0), (1, 0), (0, -1), (0, 1)};

            var nrows = grid.Length;
            var ncols = grid[0].Length;

            for (var i = 0; i < neighbors.Length; i++)
            {
                var (ro, co) = neighbors[i];
                var r2 = r + ro;
                var c2 = c + co;
                if (r2 >= 0 && r2 < nrows && c2 >= 0 && c2 < ncols)
                {
                    if (grid[r2][c2] == '0' || visited.Contains((r2, c2)))
                    {
                        continue;
                    }

                    visited.Add((r2, c2));
                    DFS(r2, c2, visited, grid);
                }
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                char[][] grid =
                {
                    new[] {'1', '1', '1', '1', '0'}
                    , new[] {'1', '1', '0', '1', '0'}
                    , new[] {'1', '1', '0', '0', '0'}
                    , new[] {'0', '0', '0', '0', '0'}
                };

                // Act
                var nIslands = new Solution().NumIslands(grid);

                // Assert
                Assert.AreEqual(1, nIslands);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                char[][] grid =
                {
                    new[] {'1', '1', '0', '0', '0'}
                    , new[] {'1', '1', '0', '0', '0'}
                    , new[] {'0', '0', '1', '0', '0'}
                    , new[] {'0', '0', '0', '1', '1'}
                };

                // Act
                var nIslands = new Solution().NumIslands(grid);

                // Assert
                Assert.AreEqual(3, nIslands);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                char[][] grid = {new[] {'1'}};

                // Act
                var nIslands = new Solution().NumIslands(grid);

                // Assert
                Assert.AreEqual(1, nIslands);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                char[][] grid = {new[] {'0'}};

                // Act
                var nIslands = new Solution().NumIslands(grid);

                // Assert
                Assert.AreEqual(0, nIslands);
            }
        }
    }
}