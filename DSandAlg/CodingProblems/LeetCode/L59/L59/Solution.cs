#region

using NUnit.Framework;

#endregion

// Problem: 59. Spiral Matrix II
// URL: https://leetcode.com/problems/spiral-matrix-ii/

namespace LeetCode
{
    public class Solution
    {
        public int[][] GenerateMatrix(int n)
        {
            // SS: runtime performance: O(n^2)
            var matrix = new int[n][];
            for (var i = 0; i < n; i++)
            {
                matrix[i] = new int[n];
            }

            var diagonal = 0;
            var maxDiagonal = (n - 1) / 2;

            var idx = 1;

            while (diagonal <= maxDiagonal)
            {
                var (row, col) = (diagonal, diagonal);

                // SS: go "right"
                while (col < n - diagonal)
                {
                    matrix[row][col] = idx++;
                    col++;
                }

                col = n - diagonal - 1;
                row++;

                // SS: go "down"
                while (row < n - diagonal)
                {
                    matrix[row][col] = idx++;
                    row++;
                }

                row = n - diagonal - 1;
                col--;

                // SS: go "left"
                while (col >= diagonal)
                {
                    matrix[row][col] = idx++;
                    col--;
                }

                col = diagonal;
                row--;

                // SS: go "up"
                while (row > diagonal)
                {
                    matrix[row][col] = idx++;
                    row--;
                }

                diagonal++;
            }

            return matrix;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var matrix = new Solution().GenerateMatrix(3);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {1, 2, 3}, new[] {8, 9, 4}, new[] {7, 6, 5}}, matrix);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var matrix = new Solution().GenerateMatrix(1);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {1}}, matrix);
            }

            [Test]
            public void Test3()
            {
                // Arrange

                // Act
                var matrix = new Solution().GenerateMatrix(4);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {1, 2, 3, 4}, new[] {12, 13, 14, 5}, new[] {11, 16, 15, 6}, new[] {10, 9, 8, 7}}, matrix);
            }
        }
    }
}