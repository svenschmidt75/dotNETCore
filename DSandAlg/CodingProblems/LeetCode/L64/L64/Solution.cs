#region

using System;
using NUnit.Framework;

#endregion

// Problem: 64. Minimum Path Sum
// URL: https://leetcode.com/problems/minimum-path-sum/

namespace Leetcode
{
    public class Solution
    {
        public int MinPathSum(int[][] grid)
        {
            // SS: bottom-up DP, runtime complexity: O(n * m)
            // space complexity: O(m), m = number of columns
            var nrows = grid.Length;
            var ncols = grid[0].Length;

            var dp1 = new int[ncols];
            var dp2 = new int[ncols];

            // SS: initial condition
            dp1[^1] = grid[^1][^1];

            for (var i = nrows - 1; i >= 0; i--)
            {
                for (var j = ncols - 1; j >= 0; j--)
                {
                    if (i == nrows - 1 && j == ncols - 1)
                    {
                        continue;
                    }

                    var right = int.MaxValue;
                    if (j <= ncols - 2)
                    {
                        right = grid[i][j] + dp1[j + 1];
                    }

                    var down = int.MaxValue;
                    if (i <= nrows - 2)
                    {
                        down = grid[i][j] + dp2[j];
                    }

                    dp1[j] = Math.Min(right, down);
                }

                var tmp = dp1;
                dp1 = dp2;
                dp2 = tmp;
            }

            return dp2[0];
        }

        public int MinPathSum2(int[][] grid)
        {
            // SS: bottom-up DP, runtime complexity: O(n * m)
            // space complexity: O(n * m)
            var nrows = grid.Length;
            var ncols = grid[0].Length;

            var dp = new int[nrows][];
            for (var i = 0; i < nrows; i++)
            {
                dp[i] = new int[ncols];
            }

            // SS: initial condition
            dp[^1][^1] = grid[^1][^1];

            for (var i = nrows - 1; i >= 0; i--)
            {
                for (var j = ncols - 1; j >= 0; j--)
                {
                    if (i == nrows - 1 && j == ncols - 1)
                    {
                        continue;
                    }

                    var right = int.MaxValue;
                    if (j <= ncols - 2)
                    {
                        right = grid[i][j] + dp[i][j + 1];
                    }

                    var down = int.MaxValue;
                    if (i <= nrows - 2)
                    {
                        down = grid[i][j] + dp[i + 1][j];
                    }

                    dp[i][j] = Math.Min(right, down);
                }
            }

            return dp[0][0];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] grid = {new[] {1, 3, 1}, new[] {1, 5, 1}, new[] {4, 2, 1}};

                // Act
                var minPathSum = new Solution().MinPathSum(grid);

                // Assert
                Assert.AreEqual(7, minPathSum);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] grid = {new[] {1, 2, 3}, new[] {4, 5, 6}};

                // Act
                var minPathSum = new Solution().MinPathSum(grid);

                // Assert
                Assert.AreEqual(12, minPathSum);
            }
        }
    }
}