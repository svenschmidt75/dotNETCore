#region

using NUnit.Framework;

#endregion

// Problem: 240. Search a 2D Matrix II
// URL: https://leetcode.com/problems/search-a-2d-matrix-ii/

namespace LeetCode
{
    public class Solution
    {
        public bool SearchMatrix(int[][] matrix, int target)
        {
            // return SearchMatrix1(matrix, target);
            return SearchMatrix2(matrix, target);
        }

        private bool SearchMatrix2(int[][] matrix, int target)
        {
            // SS: runtime complexity: O(n + m)
            // space complexity: O(1)
            var nrows = matrix.Length;
            var ncols = matrix[0].Length;

            int row = 0;
            int col = ncols - 1;

            while (row < nrows && col >= 0)
            {
                if (matrix[row][col] > target)
                {
                    col--;
                }
                else if (matrix[row][col] < target)
                {
                    row++;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        private bool SearchMatrix1(int[][] matrix, int target)
        {
            // SS: runtime complexity: O(R log C)
            var nrows = matrix.Length;
            var ncols = matrix[0].Length;
            for (var r = 0; r < nrows; r++)
            {
                if (target < matrix[r][0] && target > matrix[r][ncols - 1])
                {
                    continue;
                }

                // SS: perform Binary Search
                var min = 0;
                var max = ncols - 1;
                while (min <= max)
                {
                    var mid = (min + max) / 2;
                    if (target < matrix[r][mid])
                    {
                        max = mid - 1;
                    }
                    else if (target > matrix[r][mid])
                    {
                        min = mid + 1;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] matrix = {new[] {1, 4, 7, 11, 15}, new[] {2, 5, 8, 12, 19}, new[] {3, 6, 9, 16, 22}, new[] {10, 13, 14, 17, 24}, new[] {18, 21, 23, 26, 30}};

                // Act
                var found = new Solution().SearchMatrix(matrix, 5);

                // Assert
                Assert.True(found);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] matrix = {new[] {1, 4, 7, 11, 15}, new[] {2, 5, 8, 12, 19}, new[] {3, 6, 9, 16, 22}, new[] {10, 13, 14, 17, 24}, new[] {18, 21, 23, 26, 30}};

                // Act
                var found = new Solution().SearchMatrix(matrix, 20);

                // Assert
                Assert.False(found);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[][] matrix = {new[] {1, 1}};

                // Act
                var found = new Solution().SearchMatrix(matrix, 0);

                // Assert
                Assert.False(found);
            }
            
            [Test]
            public void Test4()
            {
                // Arrange
                int[][] matrix = {new[] {-1, 3}};

                // Act
                var found = new Solution().SearchMatrix(matrix, -1);

                // Assert
                Assert.True(found);
            }

        }
    }
}