#region

using System;
using NUnit.Framework;

#endregion

// Problem: 174. Dungeon Game
// URL: https://leetcode.com/problems/dungeon-game/

namespace LeetCode
{
    public class Solution
    {
        public int CalculateMinimumHP(int[][] dungeon)
        {
            // return CalculateMinimumHpDivideAndConquer(dungeon);
            return CalculateMinimumHpBottomUp(dungeon);
        }

        private static int CalculateMinimumHpBottomUp(int[][] dungeon)
        {
            // SS: Bottom-up DP
            // Is this problem symmetric?
            // runtime complexity: O(R x C)
            // space complexity: O(C). We can do O(1) by doing in-place...

            var nrows = dungeon.Length;
            var ncols = dungeon[0].Length;

            // SS: memoization array
            var dp1 = new int[ncols];
            var dp2 = new int[ncols];

            // SS: initial condition
            dp1[^1] = Math.Min(0, dungeon[^1][^1]);

            for (var row = nrows - 1; row >= 0; row--)
            {
                for (var col = ncols - 1; col >= 0; col--)
                {
                    if (row == nrows - 1 && col == ncols - 1)
                    {
                        // SS: end position
                        continue;
                    }

                    var roomValue = dungeon[row][col];
                    var v = Math.Min(0, roomValue);

                    var p1 = int.MinValue;
                    var p2 = int.MinValue;

                    if (row < nrows - 1)
                    {
                        // SS: we moved down
                        p1 = Math.Min(v, roomValue + dp2[col]);
                    }

                    if (col < ncols - 1)
                    {
                        // SS: we moved right
                        p2 = Math.Min(v, roomValue + dp1[col + 1]);
                    }

                    dp1[col] = Math.Max(p1, p2);
                }

                var tmp = dp1;
                dp1 = dp2;
                dp2 = tmp;
            }

            var minHealth = Math.Abs(dp2[0]) + 1;
            return minHealth;
        }

        private static int CalculateMinimumHpDivideAndConquer(int[][] dungeon)
        {
            // SS: runtime complexity: O(2^N)
            // Note: In DFS, we are injecting state, pathSum. This is generally a bad idea,
            // because we now cannot just add memoization to get the top-down DP approach.
            // This is because the value at a cell will change based in what that state is
            // and that cannot be captured when using memoization.

            var nrows = dungeon.Length;
            var ncols = dungeon[0].Length;

            int DFS(int minPathSum, int pathSum, int row, int col)
            {
                // SS: base cases
                var roomValue = dungeon[row][col];
                var pathSum2 = pathSum + roomValue;
                minPathSum = Math.Min(minPathSum, pathSum2);

                if (row == nrows - 1 && col == ncols - 1)
                {
                    // SS: end cell
                    return minPathSum;
                }

                // SS: move right
                var p1 = int.MinValue;
                if (col + 1 < ncols)
                {
                    p1 = DFS(minPathSum, pathSum2, row, col + 1);
                }

                // SS: move down
                var p2 = int.MinValue;
                if (row + 1 < nrows)
                {
                    p2 = DFS(minPathSum, pathSum2, row + 1, col);
                }

                var p = Math.Max(p1, p2);
                return p;
            }

            var minHealth = DFS(int.MaxValue, 0, 0, 0);
            minHealth = minHealth > 0 ? 1 : -minHealth + 1;
            return minHealth;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] dungeon = {new[] {-2, -3, 3}, new[] {-5, -10, 1}, new[] {10, 30, -5}};

                // Act
                var minHealth = new Solution().CalculateMinimumHP(dungeon);

                // Assert
                Assert.AreEqual(7, minHealth);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] dungeon = {new[] {1, 2, -1}, new[] {2, -4, -3}, new[] {2, -10, 1}};

                // Act
                var minHealth = new Solution().CalculateMinimumHP(dungeon);

                // Assert
                Assert.AreEqual(2, minHealth);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[][] dungeon = {new[] {0, 0}};

                // Act
                var minHealth = new Solution().CalculateMinimumHP(dungeon);

                // Assert
                Assert.AreEqual(1, minHealth);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[][] dungeon = {new[] {0}, new[] {0}};

                // Act
                var minHealth = new Solution().CalculateMinimumHP(dungeon);

                // Assert
                Assert.AreEqual(1, minHealth);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[][] dungeon = {new[] {1, -3, 3}, new[] {0, -2, 0}, new[] {-3, -3, -3}};

                // Act
                var minHealth = new Solution().CalculateMinimumHP(dungeon);

                // Assert
                Assert.AreEqual(3, minHealth);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[][] dungeon = {new[] {3, -20, 30}, new[] {-3, 4, 0}};

                // Act
                var minHealth = new Solution().CalculateMinimumHP(dungeon);

                // Assert
                Assert.AreEqual(1, minHealth);
            }
        }
    }
}