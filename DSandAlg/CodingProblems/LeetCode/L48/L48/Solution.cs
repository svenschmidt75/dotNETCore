#region

using NUnit.Framework;

#endregion

// Problem: 48. Rotate Image
// URL: https://leetcode.com/problems/rotate-image/

namespace LeetCode48
{
    public class Solution
    {
        public void Rotate(int[][] matrix)
        {
            // SS: square matrix
            var n = matrix.Length;

            // SS: "going down the diagonal" loop
            for (var i = 0; i < n / 2; i++)
            {
                // SS: "tracing a row" loop
                for (var j = 0; j < n - 2 * i - 1; j++)
                {
                    var cell1 = matrix[i][i + j];
                    var cell2 = matrix[i + j][n - 1 - i];
                    var cell3 = matrix[n - 1 - i][n - 1 - i - j];
                    var cell4 = matrix[n - 1 - i - j][i];

                    // SS: move cell 4 to cell 1
                    matrix[i][i + j] = cell4;

                    // SS: move cell 3 to cell 4
                    matrix[n - 1 - i - j][i] = cell3;

                    // SS: move cell 2 to cell 3
                    matrix[n - 1 - i][n - 1 - i - j] = cell2;

                    // SS: move cell 1 to cell 2
                    matrix[i + j][n - 1 - i] = cell1;
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
                int[][] matrix = {new[] {1, 2, 3}, new[] {4, 5, 6}, new[] {7, 8, 9}};

                // Act
                new Solution().Rotate(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {7, 4, 1}, new[] {8, 5, 2}, new[] {9, 6, 3}}, matrix);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] matrix = {new[] {5, 1, 9, 11}, new[] {2, 4, 8, 10}, new[] {13, 3, 6, 7}, new[] {15, 14, 12, 16}};

                // Act
                new Solution().Rotate(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {15, 13, 2, 5}, new[] {14, 3, 4, 1}, new[] {12, 6, 8, 9}, new[] {16, 7, 10, 11}}, matrix);
            }
        }
    }
}