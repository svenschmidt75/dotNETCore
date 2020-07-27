#region

using System;
using NUnit.Framework;

#endregion

// 1292. Maximum Side Length of a Square with Sum Less than or Equal to Threshold
// https://leetcode.com/problems/maximum-side-length-of-a-square-with-sum-less-than-or-equal-to-threshold/

namespace L1292
{
    public class Solution
    {
        public int MaxSideLength1(int[][] mat, int threshold)
        {
            var ncols = mat[0].Length;
            var nrows = mat.Length;

            var maxLength = 0;

            for (var i = 0; i < nrows; i++)
            {
                for (var j = 0; j < ncols; j++)
                {
                    var length = MaxSideLengthFrom(mat, threshold, i, j);
                    maxLength = Math.Max(maxLength, length);
                }
            }

            return maxLength;
        }

        private int MaxSideLengthFrom(int[][] mat, int threshold, int row, int col)
        {
            var ncols = mat[0].Length;
            var nrows = mat.Length;

            var w = 1;
            var sum = mat[row][col];

            if (sum > threshold)
            {
                return 0;
            }

            while (row + w < nrows && col + w < ncols && sum < threshold)
            {
                // SS: grow square
                var s = 0;

                for (var i = row; i <= row + w; i++)
                {
                    var v = mat[i][col + w];
                    s += v;
                }

                for (var i = col; i < col + w; i++)
                {
                    var v = mat[row + w][i];
                    s += v;
                }

                if (sum + s <= threshold)
                {
                    sum += s;
                    w++;
                }
                else
                {
                    break;
                }
            }

            return w;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var mat = new[] {new[] {1, 1, 3, 2, 4, 3, 2}, new[] {1, 1, 3, 2, 4, 3, 2}, new[] {1, 1, 3, 2, 4, 3, 2}};
                var threshold = 4;

                // Act
                var result = new Solution().MaxSideLength1(mat, threshold);

                // Assert
                Assert.AreEqual(2, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var mat = new[]
                {
                    new[] {2, 2, 2, 2, 2}, new[] {2, 2, 2, 2, 2}, new[] {2, 2, 2, 2, 2}, new[] {2, 2, 2, 2, 2}
                    , new[] {2, 2, 2, 2, 2}
                };
                var threshold = 1;

                // Act
                var result = new Solution().MaxSideLength1(mat, threshold);

                // Assert
                Assert.AreEqual(0, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var mat = new[] {new[] {1, 1, 1, 1}, new[] {1, 0, 0, 0}, new[] {1, 0, 0, 0}, new[] {1, 0, 0, 0}};
                var threshold = 6;

                // Act
                var result = new Solution().MaxSideLength1(mat, threshold);

                // Assert
                Assert.AreEqual(3, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var mat = new[]
                {
                    new[] {18, 70}, new[] {61, 1}, new[] {25, 85}, new[] {14, 40}, new[] {11, 96}, new[] {97, 96}
                    , new[] {63, 45}
                };
                var threshold = 40184;

                // Act
                var result = new Solution().MaxSideLength1(mat, threshold);

                // Assert
                Assert.AreEqual(2, result);
            }
        }
    }
}