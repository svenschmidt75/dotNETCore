#region

using NUnit.Framework;

#endregion

// Problem: 73. Set Matrix Zeroes
// URL: https://leetcode.com/problems/set-matrix-zeroes/

namespace LeetCode
{
    public class Solution
    {
        public void SetZeroes(int[][] matrix)
        {
            // SS: O(1) space complexity by using a row and column to track
            // which rows and columns we need to set to 0.
            // I needed to look at hints to see this...

            var nrows = matrix.Length;
            var ncols = matrix[0].Length;

            // SS: row/col to store the data
            var mr = -1;
            var mc = -1;

            for (var i = 0; i < nrows; i++)
            {
                for (var j = 0; j < ncols; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        mr = i;
                        mc = j;
                        break;
                    }
                }

                if (mr != -1)
                {
                    break;
                }
            }

            // SS: no 0 in matrix?
            if (mr == -1)
            {
                return;
            }

            // SS: find all 0s and remember their row and column index
            for (var r = 0; r < nrows; r++)
            {
                for (var c = 0; c < ncols; c++)
                {
                    if (matrix[r][c] == 0)
                    {
                        matrix[mr][c] = 0;
                        matrix[r][mc] = 0;
                    }
                }
            }

            // SS: traverse the special column mc and set all elements
            // to 0 which have a 0 in the special column
            for (var r = 0; r < nrows; r++)
            {
                if (r == mr)
                {
                    // SS: we need those values later to 0 the columns
                    continue;
                }

                if (matrix[r][mc] == 0)
                {
                    // SS: set row to 0
                    for (var c = 0; c < ncols; c++)
                    {
                        matrix[r][c] = 0;
                    }
                }
            }

            // SS: traverse the special row mr and set all elements
            // to 0 which have a 0 in the special row
            for (var c = 0; c < ncols; c++)
            {
                if (matrix[mr][c] == 0)
                {
                    // SS: set column to 0
                    for (var r = 0; r < nrows; r++)
                    {
                        matrix[r][c] = 0;
                    }
                }
            }

            // set all elements in the special row/column to 0
            for (var c = 0; c < ncols; c++)
            {
                matrix[mr][c] = 0;
            }

            for (var r = 0; r < nrows; r++)
            {
                matrix[r][mc] = 0;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] matrix = {new[] {1, 1, 1}, new[] {1, 0, 1}, new[] {1, 1, 1}};

                // Act
                new Solution().SetZeroes(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {1, 0, 1}, new[] {0, 0, 0}, new[] {1, 0, 1}}, matrix);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] matrix = {new[] {0, 1, 2, 0}, new[] {3, 4, 5, 2}, new[] {1, 3, 1, 5}};

                // Act
                new Solution().SetZeroes(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {0, 0, 0, 0}, new[] {0, 4, 5, 0}, new[] {0, 3, 1, 0}}, matrix);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[][] matrix = {new[] {1, 0, 1}, new[] {1, 1, 0}, new[] {1, 1, 1}};

                // Act
                new Solution().SetZeroes(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {0, 0, 0}, new[] {0, 0, 0}, new[] {1, 0, 0}}, matrix);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[][] matrix = {new[] {1, 0, 1}, new[] {1, 1, 1}, new[] {1, 1, 0}};

                // Act
                new Solution().SetZeroes(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {0, 0, 0}, new[] {1, 0, 0}, new[] {0, 0, 0}}, matrix);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[][] matrix = {new[] {1, 1, 0}, new[] {1, 1, 1}, new[] {0, 1, 0}};

                // Act
                new Solution().SetZeroes(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {0, 0, 0}, new[] {0, 1, 0}, new[] {0, 0, 0}}, matrix);
            }
        }
    }
}