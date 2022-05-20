#region

using NUnit.Framework;

#endregion

namespace L304;

// Problem: 304. Range Sum Query 2D - Immutable
// URL: https://leetcode.com/problems/range-sum-query-2d-immutable/

public class NumMatrix
{
    private readonly int[][] _prefixSums;

    public NumMatrix(int[][] matrix)
    {
        // SS: create the prefix sums for each column
        var nrows = matrix.Length;
        var ncols = matrix[0].Length;
        _prefixSums = new int[ncols][];

        for (int c = 0; c < ncols; c++)
        {
            var rowPrefixSum = new int[nrows + 1];
            _prefixSums[c] = rowPrefixSum;

            for (int r = 0; r < nrows; r++)
            {
                var v = matrix[r][c];
                rowPrefixSum[r + 1] = rowPrefixSum[r] + v;
            }
        }
    }

    public int SumRegion(int row1, int col1, int row2, int col2)
    {
        var sum = 0;

        for (var c = col1; c <= col2; c++)
        {
            var rowSum = _prefixSums[c][row2 + 1] - _prefixSums[c][row1];
            sum += rowSum;
        }

        return sum;
    }

    [TestFixture]
    private class Tests
    {
        [TestCase(2, 1, 4, 3, 8)]
        [TestCase(1, 1, 2, 2, 11)]
        [TestCase(1, 2, 2, 4, 12)]
        public void Test1(int row1, int col1, int row2, int col2, int expected)
        {
            // Arrange
            var numMatrix = new NumMatrix(new[] { new[] { 3, 0, 1, 4, 2 }, new[] { 5, 6, 3, 2, 1 }, new[] { 1, 2, 0, 1, 5 }, new[] { 4, 1, 0, 1, 7 }, new[] { 1, 0, 3, 0, 5 } });

            // Act
            var value = numMatrix.SumRegion(row1, col1, row2, col2);

            // Assert
            Assert.AreEqual(expected, value);
        }
    }
}