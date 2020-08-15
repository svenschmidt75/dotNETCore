#region

using NUnit.Framework;

#endregion

namespace GoogleProblem29
{
    public class Solution
    {
        public int Solve1D(int[] m)
        {
            // SS: Partition 1d range into 0s and 1s.
            // Count how many ways there are to arrange 0, i.e.
            // [0, 0, 0] -> 3 x 1 + 2 x 2 + 1 x 3
            var cnt = 0;

            var start = 0;
            while (start < m.Length)
            {
                if (m[start] == 1)
                {
                    start++;
                    continue;
                }

                var end = start;
                while (end < m.Length && m[end] == 0)
                {
                    end++;
                }

                var d = end - start;
                cnt += d * (d + 1) / 2;

                start = end;
            }

            return cnt;
        }

        public int BruteForce2D(int[][] m)
        {
            if (m.Length == 0)
            {
                return 0;
            }

            // SS: First, loop over all widths and heights of potential submatrices.
            // Then, for each cell, test whether that submatrix is valid.
            // Runtime complexity: O(R^3 * C^3), R=#rows, C=#cols
            var nrows = m.Length;
            var ncols = m[0].Length;

            var nValid = 0;

            // O(R)
            for (var height = 1; height <= nrows; height++)
            {
                // O(C)
                for (var width = 1; width <= ncols; width++)
                {
                    // O(R)
                    for (var r = 0; r < nrows; r++)
                    {
                        if (r + height > nrows)
                        {
                            continue;
                        }

                        // O(C)
                        for (var c = 0; c < ncols; c++)
                        {
                            if (c + width > ncols)
                            {
                                continue;
                            }

                            // SS: Starting at cell (r, c), check whether (r, c, r + width, c + height)
                            // is a valid submatrix, O(R * c)
                            if (isValid(m, r, c, width, height))
                            {
//                                TestContext.Progress.WriteLine($"{r}, {c}, {width}, {height}");
                                nValid++;
                            }
                        }
                    }
                }
            }

            return nValid;
        }

        private bool isValid(int[][] m, int row, int col, int width, int height)
        {
            for (var r = 0; r < width; r++)
            {
                for (var c = 0; c < height; c++)
                {
                    var value = m[row + c][col + r];
                    if (value == 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1D1()
            {
                // Arrange
                var m = new[]
                {
                    1, 0, 0, 1
                };

                // Act
                var nValid = new Solution().Solve1D(m);

                // Assert
                Assert.AreEqual(2 + 1, nValid);
            }

            [Test]
            public void Test1D2()
            {
                // Arrange
                var m = new[]
                {
                    0, 0, 0, 1
                };

                // Act
                var nValid = new Solution().Solve1D(m);

                // Assert
                Assert.AreEqual(3 + 2 + 1, nValid);
            }

            [Test]
            public void Test1D3()
            {
                // Arrange
                var m = new[]
                {
                    0, 0, 1, 0
                };

                // Act
                var nValid = new Solution().Solve1D(m);

                // Assert
                Assert.AreEqual(2 + 1 + 1, nValid);
            }

            [Test]
            public void Test2D1()
            {
                // Arrange
                var m = new[]
                {
                    new[] {0, 0, 1}
                    , new[] {0, 0, 0}
                    , new[] {1, 0, 1}
                };

                // Act
                var nValid = new Solution().BruteForce2D(m);

                // Assert
                Assert.AreEqual(6 + 6 + 2 + 1, nValid);
            }
        }
    }
}