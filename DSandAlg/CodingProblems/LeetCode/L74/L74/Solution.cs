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
            // SS: find the row the number could be in, O(R), then
            // do binary search on the row, O(log C) for a total of
            // O(R * log C)

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

        private static int FindRow(int[][] matrix, in int target)
        {
            // SS: O(R)

            // SS: target too small?
            if (matrix[0][0] > target)
            {
                return -1;
            }

            // SS: target too large?
            if (matrix[^1][^1] < target)
            {
                return -1;
            }

            var nrows = matrix.Length;

            if (nrows == 1)
            {
                return 0;
            }

            for (var i = 1; i < nrows; i++)
            {
                if (matrix[i - 1][0] <= target && matrix[i][0] > target)
                {
                    return i - 1;
                }
            }

            return nrows - 1;
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