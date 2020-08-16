#region

using System;
using NUnit.Framework;

#endregion

namespace L221
{
    public class Solution
    {
        public int MaximalSquare(char[][] matrix)
        {
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

            // SS: O(R * C)
            // Use prefix sum to reduce the order by 1 (by reducing the dimension by 1)
            var rowPrefixSum = FindRowPrefixSum(matrix);

            var maxSize = 0;

            // SS: O(C * R^2)
            for (var col = 0; col < ncols; col++)
            {
                for (var row = 0; row < nrows; row++)
                {
                    var rv = rowPrefixSum[row][col];

                    // SS: max. possible side length of square
                    var width = rv;

                    // SS: height of square so far
                    var height = 1;

                    var row2 = row + 1;
                    while (row2 < nrows && height < width)
                    {
                        var rv2 = rowPrefixSum[row2][col];
                        if (rv2 > width)
                        {
                            height++;
                        }
                        else
                        {
                            // SS: square needs to be smaller
                            if (rv2 < height)
                            {
                                // SS: smallest square side length is height
                                break;
                            }

                            // SS: adjust new maximum square side length
                            width = rv2;
                            height++;
                        }

                        row2++;
                    }

                    // SS: add squares with 1s
                    var maxWidth = Math.Min(width, height);
                    maxSize = Math.Max(maxSize, maxWidth);
                }
            }

            return maxSize * maxSize;
        }

        private static int[][] FindRowPrefixSum(char[][] m)
        {
            var nrows = m.Length;
            var ncols = m[0].Length;

            var prefixSum = new int[nrows][];
            for (var i = 0; i < nrows; i++)
            {
                prefixSum[i] = new int[ncols];
            }

            for (var i = 0; i < nrows; i++)
            {
                for (var j = ncols - 1; j >= 0; j--)
                {
                    if (m[i][j] == '0')
                    {
                        continue;
                    }

                    if (j < ncols - 1)
                    {
                        prefixSum[i][j] += prefixSum[i][j + 1];
                    }

                    prefixSum[i][j] += m[i][j] == '1' ? 1 : 0;
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
                var m = new[]
                {
                    new[] {'1', '0', '1', '0', '0'}
                    , new[] {'1', '0', '1', '1', '1'}
                    , new[] {'1', '1', '1', '1', '1'}
                    , new[] {'1', '0', '0', '1', '0'}
                };

                // Act
                var result = new Solution().MaximalSquare(m);

                // Assert
                Assert.AreEqual(4, result);
            }
        }
    }
}