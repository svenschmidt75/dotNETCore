#region

using NUnit.Framework;

#endregion

// Problem: 74. Search a 2D Matrix
// URL: https://leetcode.com/problems/search-a-2d-matrix/

namespace LeetCode
{
    public class Solution
    {
        public bool SearchMatrix(int[][] matrix, int target)
        {
            // SS: find the row the number could be in, O(log R), then
            // do binary search on the row, O(log C) for a total of
            // O(log R + log C) = O(log (R * C))
            //
            // If each item is larger than the item in the previous row
            // as well, we can concatenate all rows and search for the
            // element in a single 1D array, also at cost O(log (R * C))... 

            var nrows = matrix.Length;
            if (nrows == 0)
            {
                return false;
            }

            var ncols = matrix[0].Length;
            if (ncols == 0)
            {
                return false;
            }

            // SS: find row
            var row = FindRow(matrix, target);
            if (row == -1)
            {
                return false;
            }

            // SS: BS on row data, O(log C)
            var min = 0;
            var max = ncols;
            while (min < max)
            {
                var mid = min + (max - min) / 2;
                var v = matrix[row][mid];

                if (v == target)
                {
                    return true;
                }

                if (v < target)
                {
                    min = mid + 1;
                }
                else
                {
                    max = mid;
                }
            }

            return false;
        }

        private static int FindRow(int[][] matrix, int target)
        {
            // SS: O(log R)
            var nrows = matrix.Length;

            // SS: BS on the first column, O(log R)
            var min = 0;
            var max = nrows;
            while (min < max)
            {
                var mid = min + (max - min) / 2;
                var v = matrix[mid][0];

                if (v == target)
                {
                    return mid;
                }

                if (v < target)
                {
                    min = mid + 1;
                }
                else
                {
                    max = mid;
                }
            }

            return min == -1 ? -1 : min - 1;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] matrix = {new[] {1, 3, 5, 7}, new[] {10, 11, 16, 20}, new[] {23, 30, 34, 50}};

                // Act
                var found = new Solution().SearchMatrix(matrix, 3);

                // Assert
                Assert.True(found);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] matrix = {new[] {1, 3, 5, 7}, new[] {10, 11, 16, 20}, new[] {23, 30, 34, 50}};

                // Act
                var found = new Solution().SearchMatrix(matrix, 13);

                // Assert
                Assert.False(found);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var matrix = new int[0][];

                // Act
                var found = new Solution().SearchMatrix(matrix, 0);

                // Assert
                Assert.False(found);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[][] matrix = {new[] {1, 3, 5, 7}, new[] {10, 11, 16, 20}, new[] {23, 30, 34, 50}, new[] {55, 57, 59, 60}};

                // Act
                var found = new Solution().SearchMatrix(matrix, 60);

                // Assert
                Assert.True(found);
            }
        }
    }
}