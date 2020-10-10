#region

using System;
using NUnit.Framework;

#endregion

namespace L1504
{
    public class Solution
    {
        public int NumSubmat(int[][] mat)
        {
            var nrows = mat.Length;
            var ncols = mat[0].Length;

            if (nrows == 0 || ncols == 0)
            {
                return 0;
            }

            var prefixSum = PrefixSum(mat);

            var count = 0;

            for (var r = 0; r < nrows; r++)
            {
                for (var c = 0; c < ncols; c++)
                {
                    var s1 = prefixSum[r][c];
                    if (s1 == 0)
                    {
                        continue;
                    }

                    var localCount = s1;
                    var maxWidth = s1;

                    for (var k = r + 1; k < nrows; k++)
                    {
                        var s2 = prefixSum[k][c];
                        if (s2 == 0)
                        {
                            break;
                        }

                        if (s2 > maxWidth)
                        {
                            localCount += maxWidth;
                        }
                        else
                        {
                            maxWidth = s2;
                            localCount += s2;
                        }
                    }

//                Console.WriteLine($"{r} {c}: {localCount}");
                    count += localCount;
                }
            }


            return count;
        }

        private int[][] PrefixSum(int[][] mat)
        {
            var nrows = mat.Length;
            var ncols = mat[0].Length;

            var prefixSum = new int[nrows][];
            for (var i = 0; i < nrows; i++)
            {
                prefixSum[i] = new int[ncols];
            }

            for (var r = 0; r < nrows; r++)
            {
                for (var c = ncols - 1; c >= 0; c--)
                {
                    var sum = mat[r][c];

                    if (sum == 1 && c < ncols - 1)
                    {
                        sum += prefixSum[r][c + 1];
                    }

                    prefixSum[r][c] = sum;
                    Console.WriteLine($"{r} {c}: {sum}");
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
                var mat = new[] {new[] {1, 0, 1}, new[] {1, 1, 0}, new[] {1, 1, 0}};

                // Act
                var count = new Solution().NumSubmat(mat);

                // Assert
                Assert.AreEqual(13, count);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var mat = new[] {new[] {0, 1, 1, 0}, new[] {0, 1, 1, 1}, new[] {1, 1, 1, 0}};

                // Act
                var count = new Solution().NumSubmat(mat);

                // Assert
                Assert.AreEqual(24, count);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var mat = new[] {new[] {1, 1, 1, 1, 1, 1}};

                // Act
                var count = new Solution().NumSubmat(mat);

                // Assert
                Assert.AreEqual(21, count);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var mat = new[] {new[] {1, 0, 1}, new[] {0, 1, 0}, new[] {1, 0, 1}};

                // Act
                var count = new Solution().NumSubmat(mat);

                // Assert
                Assert.AreEqual(5, count);
            }
        }
    }
}