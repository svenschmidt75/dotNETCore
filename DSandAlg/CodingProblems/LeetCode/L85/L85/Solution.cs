#region

using System;
using NUnit.Framework;

#endregion

// Problem: 85. Maximal Rectangle
// URL: https://leetcode.com/problems/maximal-rectangle/

namespace LeetCode
{
    public class Solution
    {
        public int MaximalRectangle(char[][] matrix)
        {
            // SS: runtime complexity: O(R^2 * C^2)
            // space complexity: O(R * C)
            
            var nrows = matrix.Length;
            if (nrows == 0)
            {
                return 0;
            }

            var ncols = matrix[0].Length;
            if (ncols == 0)
            {
                return 0;
            }

            // SS: we cannot do this in-place because the prefix sum could
            // be >= 10...
            var prefixSum = CreatePrefixSum(matrix);

            var maxArea = 0;

            // SS: starting at each cell, we calculate the largest
            // area...
            for (var row = 0; row < nrows; row++)
            {
                for (var col = 0; col < ncols; col++)
                {
                    var height = 1;
                    var width = prefixSum[row][col];

                    var r = row;
                    var c = col;

                    // SS: we now go down and adjust the window accordingly
                    while (width > 0 && r <= nrows - 1)
                    {
                        var w = prefixSum[r][c];
                        width = Math.Min(width, w);
                        maxArea = Math.Max(maxArea, width * height);

                        height++;
                        r++;
                    }
                }
            }

            return maxArea;
        }

        private static int[][] CreatePrefixSum(char[][] matrix)
        {
            var nrows = matrix.Length;
            var ncols = matrix[0].Length;

            var prefixSum = new int[nrows][];
            for (var row = 0; row < nrows; row++)
            {
                prefixSum[row] = new int[ncols];
            }

            for (var row = 0; row < nrows; row++)
            {
                for (var col = ncols - 1; col >= 0; col--)
                {
                    var c = matrix[row][col] - '0';
                    if (c > 0 && col <= ncols - 2)
                    {
                        c += prefixSum[row][col + 1];
                    }

                    prefixSum[row][col] = c;
                }
            }

            return prefixSum;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                char[][] matrix = {new[] {'1', '0', '1', '0', '0'}, new[] {'1', '0', '1', '1', '1'}, new[] {'1', '1', '1', '1', '1'}, new[] {'1', '0', '0', '1', '0'}};

                // Act
                var area = new Solution().MaximalRectangle(matrix);

                // Assert
                Assert.AreEqual(6, area);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var matrix = new char[0][];

                // Act
                var area = new Solution().MaximalRectangle(matrix);

                // Assert
                Assert.AreEqual(0, area);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                char[][] matrix = {new[] {'0'}};

                // Act
                var area = new Solution().MaximalRectangle(matrix);

                // Assert
                Assert.AreEqual(0, area);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                char[][] matrix = {new[] {'1'}};

                // Act
                var area = new Solution().MaximalRectangle(matrix);

                // Assert
                Assert.AreEqual(1, area);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                char[][] matrix = {new[] {'0', '0'}};

                // Act
                var area = new Solution().MaximalRectangle(matrix);

                // Assert
                Assert.AreEqual(0, area);
            }
        }
    }
}