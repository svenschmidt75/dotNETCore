#region

using NUnit.Framework;

#endregion

// Problem: 63. Unique Paths II
// URL: https://leetcode.com/problems/unique-paths-ii/

namespace LeetCode
{
    public class Solution
    {
        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            var nrows = obstacleGrid.Length;
            var ncols = obstacleGrid[0].Length;

            // SS: if start position is an obstacle, there is no path
            if (obstacleGrid[0][0] == 1)
            {
                return 0;
            }

            // SS: if end position is an obstacle, there is no path
            if (obstacleGrid[^1][^1] == 1)
            {
                return 0;
            }

            // set boundary conditions
            obstacleGrid[^1][^1] = -1;

            for (var i = nrows - 1; i >= 0; i--)
            {
                for (var j = ncols - 1; j >= 0; j--)
                {
                    if (i == nrows - 1 && j == ncols - 1)
                    {
                        continue;
                    }

                    if (obstacleGrid[i][j] == 1)
                    {
                        // obstacle
                        continue;
                    }

                    var n = 0;

                    if (i <= nrows - 2)
                    {
                        // SS: can go down
                        var c = obstacleGrid[i + 1][j];
                        if (c != 1)
                        {
                            n += c;
                        }
                    }

                    if (j <= ncols - 2)
                    {
                        // SS: can go right
                        var c = obstacleGrid[i][j + 1];
                        if (c != 1)
                        {
                            n += c;
                        }
                    }

                    obstacleGrid[i][j] = n;
                }
            }

            return -obstacleGrid[0][0];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] obstacleGrid = {new[] {0, 0, 0}, new[] {0, 1, 0}, new[] {0, 0, 0}};

                // Act
                var nPaths = new Solution().UniquePathsWithObstacles(obstacleGrid);

                // Assert
                Assert.AreEqual(2, nPaths);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] obstacleGrid = {new[] {0, 1}, new[] {0, 0}};

                // Act
                var nPaths = new Solution().UniquePathsWithObstacles(obstacleGrid);

                // Assert
                Assert.AreEqual(1, nPaths);
            }
        }
    }
}