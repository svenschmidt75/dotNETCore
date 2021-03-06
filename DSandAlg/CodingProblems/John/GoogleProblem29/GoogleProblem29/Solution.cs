﻿#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Also https://www.geeksforgeeks.org/number-of-submatrices-with-all-1s/
// Also LeetCode 1504. Count Submatrices With All Ones
// https://leetcode.com/problems/count-submatrices-with-all-ones/

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
                            if (IsValid(m, r, c, width, height))
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

        private static int[][] FindPrefixCount(int[][] m)
        {
            // Function to find required prefix-count for 
            // each row from right to left 
            var nrows = m.Length;
            var ncols = m[0].Length;

            // Array to store required prefix count of 
            // 1s from right to left for boolean array 
            var p_arr = new int[nrows][];
            for (var i = 0; i < nrows; i++)
            {
                p_arr[i] = new int[ncols];
            }

            for (var i = 0; i < nrows; i++)
            {
                for (var j = ncols - 1; j >= 0; j--)
                {
                    if (m[i][j] == 1)
                    {
                        continue;
                    }

                    if (j < ncols - 1)
                    {
                        p_arr[i][j] += p_arr[i][j + 1];
                    }

                    p_arr[i][j] += m[i][j] == 0 ? 1 : 0;
                }
            }

            return p_arr;
        }

        private static int[][] FindRowPrefixSum(int[][] m)
        {
            var nrows = m.Length;
            var ncols = m[0].Length;

            var rowPrefixSum = new int[nrows][];
            for (var i = 0; i < nrows; i++)
            {
                rowPrefixSum[i] = new int[ncols];
            }

            for (var i = 0; i < nrows; i++)
            {
                for (var j = ncols - 1; j >= 0; j--)
                {
                    if (m[i][j] == 0)
                    {
                        continue;
                    }

                    if (j < ncols - 1)
                    {
                        rowPrefixSum[i][j] += rowPrefixSum[i][j + 1];
                    }

                    rowPrefixSum[i][j] += m[i][j] == 1 ? 1 : 0;
                }
            }

            return rowPrefixSum;
        }

        public int Solve2DOptimally(int[][] m)
        {
            // SS: adapted from https://www.geeksforgeeks.org/number-of-submatrices-with-all-1s/
            if (m.Length == 0)
            {
                return 0;
            }

            var nrows = m.Length;
            var ncols = m[0].Length;

            var p_arr = FindPrefixCount(m);

            // variable to store the final answer 
            var ans = 0;

            /*  Loop to evaluate each column of 
                the prefix matrix uniquely. 
                For each index of a column we will try to 
                determine the number of sub-matrices 
                starting from that index 
                and has all 0s
                 */
            for (var col = 0; col < ncols; col++)
            {
                var row = nrows - 1;

                /*  Stack to store elements and the count 
                    of the numbers they popped 
                      
                    First part of pair will be the 
                    value of inserted element. 
                    Second part will be the count 
                    of the number of elements pushed 
                    before with a greater value */
                var q = new Stack<(int p_arr_cell, int c2)>();

                // variable to store the number of 
                // submatrices with all 1s 
                var to_sum = 0;

                while (row >= 0)
                {
                    var c = 0;

                    var p_arr_cell = p_arr[row][col];

                    while (q.Count != 0 && q.Peek().p_arr_cell > p_arr_cell)
                    {
                        var (p_arr_cell2, c2) = q.Pop();
                        to_sum -= (c2 + 1) * (p_arr_cell2 - p_arr_cell);
                        c += c2 + 1;
                    }

                    to_sum += p_arr_cell;
                    ans += to_sum;
                    q.Push((p_arr_cell, c));
                    row--;
                }
            }

            return ans;
        }

        private static bool IsValid(int[][] m, int row, int col, int width, int height)
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

        public int Solve2DSubOptimally(int[][] mat)
        {
            // SS: runtime complexity: O(R^2 * C)
            // space complexity: O(R * C)

            var nrows = mat.Length;
            if (nrows == 0)
            {
                return 0;
            }

            var ncols = mat[0].Length;
            if (ncols == 0)
            {
                return 0;
            }

            // SS: O(R * C)
            var rowPrefixSum = FindRowPrefixSum(mat);

            // SS: number of solution
            var count = 0;

            // SS: O(C)
            for (var col = 0; col < ncols; col++)
            {
                // SS: O(R)
                for (var row = 0; row < nrows; row++)
                {
                    var pv = rowPrefixSum[row][col];
                    if (pv == 0)
                    {
                        continue;
                    }

                    // SS: number of submatrices starting at cell (row, col)
                    var localCount = 0;

                    // SS: maximum possible width of submatrix
                    var width = pv;

                    // SS: current height of submatrix
                    var height = 1;

                    // SS: height where the submatrix changed width
                    var height2 = 0;

                    var row2 = row + 1;
                    while (row2 < nrows)
                    {
                        var pv2 = rowPrefixSum[row2][col];
                        if (pv2 < width)
                        {
                            // SS: add combinations for rectangle so far
                            // rectangle size is 'height x width', taking into account
                            // double-counting
                            localCount += (height - height2) * width;
                            width = pv2;
                            height2 = height;
                        }

                        height++;
                        row2++;
                    }

                    localCount += (height - height2) * width;
                    count += localCount;
                }
            }

            return count;
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
            public void Test2D_1_1()
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

            [Test]
            public void Test2D_1_4()
            {
                // Arrange
                var m = new[]
                {
                    new[] {0, 0, 1}
                    , new[] {0, 0, 0}
                    , new[] {0, 0, 1}
                    , new[] {1, 0, 1}
                };

                // Act
                var nValid = new Solution().BruteForce2D(m);

                // Assert
                Assert.AreEqual(25, nValid);
            }

            [Test]
            public void Test2D_2_2()
            {
                // Arrange
                var m = new[]
                {
                    new[] {0, 0, 1}
                    , new[] {0, 0, 0}
                    , new[] {1, 0, 1}
                };

                // Act
                var nValid = new Solution().Solve2DOptimally(m);

                // Assert
                Assert.AreEqual(6 + 6 + 2 + 1, nValid);
            }

            [Test]
            public void Test2D_2_3()
            {
                // Arrange
                var m = new[]
                {
                    new[] {0, 0, 0}
                    , new[] {1, 1, 1}
                };

                // Act
                var nValid = new Solution().Solve2DOptimally(m);

                // Assert
                Assert.AreEqual(6, nValid);
            }

            [Test]
            public void Test2D_2_4()
            {
                // Arrange
                var m = new[]
                {
                    new[] {0, 1, 1}
                    , new[] {0, 0, 1}
                    , new[] {0, 0, 0}
                    , new[] {0, 0, 1}
                    , new[] {1, 0, 1}
                };

                // Act
                var nValid = new Solution().Solve2DOptimally(m);

                // Assert
                Assert.AreEqual(29, nValid);
            }

            [Test]
            public void Test2D_2_5()
            {
                // Arrange
                var m = new[]
                {
                    new[] {0, 0, 0, 0, 0}
                    , new[] {0, 0, 0, 1, 1}
                    , new[] {0, 0, 1, 1, 1}
                    , new[] {0, 1, 1, 1, 1}
                    , new[] {1, 1, 1, 1, 1}
                };

                // Act
                var nValid = new Solution().Solve2DOptimally(m);

                // Assert
                Assert.AreEqual(40, nValid);
            }

            [Test]
            public void Test2D_3_1()
            {
                // Arrange
                var m = new[]
                {
                    new[] {1, 1, 0}
                    , new[] {1, 1, 1}
                    , new[] {0, 1, 0}
                };

                // Act
                var nValid = new Solution().Solve2DSubOptimally(m);

                // Assert
                Assert.AreEqual(6 + 6 + 2 + 1, nValid);
            }

            [Test]
            public void Test2D_3_2()
            {
                // Arrange
                var m = new[]
                {
                    new[] {1, 1, 0}
                    , new[] {1, 1, 1}
                    , new[] {1, 1, 0}
                    , new[] {0, 1, 0}
                };

                // Act
                var nValid = new Solution().Solve2DSubOptimally(m);

                // Assert
                Assert.AreEqual(25, nValid);
            }

            [Test]
            public void Test2D_3_3()
            {
                // Arrange
                var m = new[]
                {
                    new[] {1, 1, 1, 1, 1}
                    , new[] {1, 1, 1, 0, 0}
                    , new[] {1, 1, 0, 0, 0}
                    , new[] {1, 0, 0, 0, 0}
                    , new[] {0, 0, 0, 0, 0}
                };

                // Act
                var nValid = new Solution().Solve2DSubOptimally(m);

                // Assert
                Assert.AreEqual(40, nValid);
            }

            [Test]
            public void Test2D_3_4()
            {
                // Arrange
                var m = new[]
                {
                    new[] {1, 0, 0}
                    , new[] {1, 1, 0}
                    , new[] {1, 1, 1}
                    , new[] {1, 1, 0}
                    , new[] {0, 1, 0}
                };

                // Act
                var nValid = new Solution().Solve2DSubOptimally(m);

                // Assert
                Assert.AreEqual(29, nValid);
            }

            [Test]
            public void Test2D_3_5()
            {
                // Arrange
                var m = new[]
                {
                    new[] {1, 1, 1}
                    , new[] {1, 1, 1}
                    , new[] {1, 1, 1}
                };

                // Act
                var nValid = new Solution().Solve2DSubOptimally(m);

                // Assert
                Assert.AreEqual(36, nValid);
            }

            [Test]
            public void Test2D_3_6()
            {
                // Arrange
                var m = new[]
                {
                    new[] {1, 1, 1}
                    , new[] {1, 0, 1}
                    , new[] {1, 1, 1}
                };

                // Act
                var nValid = new Solution().Solve2DSubOptimally(m);

                // Assert
                Assert.AreEqual(20, nValid);
            }
        }
    }
}