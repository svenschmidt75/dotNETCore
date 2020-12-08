#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 54. Spiral Matrix
// URL: https://leetcode.com/problems/spiral-matrix/

namespace LeetCode54
{
    public class Solution
    {
        public IList<int> SpiralOrder(int[][] matrix)
        {
            return SpiralOrder3(matrix);
        }

        public IList<int> SpiralOrder3(int[][] matrix)
        {
            // SS: runtime complexity: O(m n)
            // Larry's solution...
            var nrows = matrix.Length;
            var ncols = matrix[0].Length;

            var minRow = 0;
            var maxRow = nrows - 1;

            var minCol = 0;
            var maxCol = ncols - 1;

            var result = new List<int>();

            while (minRow <= maxRow && minCol <= maxCol)
            {
                var x = minRow;
                var y = minCol;

                while (y <= maxCol)
                {
                    result.Add(matrix[x][y]);
                    y++;
                }

                y--;
                x++;

                if (x > maxRow)
                {
                    break;
                }

                while (x <= maxRow)
                {
                    result.Add(matrix[x][y]);
                    x++;
                }

                if (minCol == maxCol && y == maxCol)
                {
                    break;
                }

                x--;
                y--;

                if (y < 0)
                {
                    break;
                }

                while (y >= minCol)
                {
                    result.Add(matrix[x][y]);
                    y--;
                }

                y++;
                x--;

                while (x > minRow)
                {
                    result.Add(matrix[x][y]);
                    x--;
                }

                minRow++;
                maxRow--;

                minCol++;
                maxCol--;
            }

            return result;
        }

        public IList<int> SpiralOrder2(int[][] matrix)
        {
            // SS: runtime complexity: O(m n)
            var nrows = matrix.Length;
            var ncols = matrix[0].Length;

            var result = new List<int>();

            var maxDiagIdx = Math.Min(nrows - 1, ncols - 1) / 2;

            var diagIdx = 0;

            while (diagIdx <= maxDiagIdx)
            {
                var maxIdx = GetMaxIdx(diagIdx, nrows, ncols);
                for (var idx = 0; idx < maxIdx; idx++)
                {
                    var (r, c) = FromIndex(idx, diagIdx, nrows, ncols);
                    var cell = matrix[r][c];
                    result.Add(cell);
                }

                diagIdx++;
            }

            return result;
        }

        private static int GetMaxIdx(int diagIdx, int nrows, int ncols)
        {
            var nelem = ncols - 2 * diagIdx;
            if (nrows - 2 * diagIdx > 1)
            {
                nelem *= 2;
                var addRows = nrows - 2 * diagIdx - 2;
                if (addRows > 0)
                {
                    var delta = 2;
                    if (ncols - 2 * diagIdx == 1)
                    {
                        delta--;
                    }

                    nelem += addRows * delta;
                }
            }

            return nelem;
        }

        private static (int r, int c) FromIndex(int index, int diagIdx, int nrows, int ncols)
        {
            var cncols = ncols - 2 * diagIdx;
            var cnrows = nrows - 2 * diagIdx;

            var r = diagIdx;
            var c = diagIdx;

            var idx = index;

            // SS: is the cell on the current row?
            if (idx < cncols)
            {
                return (diagIdx, diagIdx + idx);
            }

            c += cncols - 1;
            idx -= cncols - 1;

            if (idx < cnrows)
            {
                return (r + idx, c);
            }

            r += cnrows - 1;
            idx -= cnrows - 1;

            if (idx < cncols)
            {
                return (r, c - idx);
            }

            c -= cncols - 1;
            idx -= cncols - 1;

            if (idx < cnrows)
            {
                return (r - idx, c);
            }

            return (diagIdx, diagIdx);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] matrix = {new[] {1, 2, 3}, new[] {4, 5, 6}, new[] {7, 8, 9}};

                // Act
                var result = new Solution().SpiralOrder(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 2, 3, 6, 9, 8, 7, 4, 5}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] matrix = {new[] {1, 2, 3, 4}, new[] {5, 6, 7, 8}, new[] {9, 10, 11, 12}};

                // Act
                var result = new Solution().SpiralOrder(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 2, 3, 4, 8, 12, 11, 10, 9, 5, 6, 7}, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[][] matrix = {new[] {6, 9, 7}};

                // Act
                var result = new Solution().SpiralOrder(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {6, 9, 7}, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[][] matrix = {new[] {6}, new[] {9}, new[] {10}};

                // Act
                var result = new Solution().SpiralOrder(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {6, 9, 10}, result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[][] matrix = {new[] {6}};

                // Act
                var result = new Solution().SpiralOrder(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {6}, result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[][] matrix = {new[] {2, 5, 8}, new[] {4, 0, -1}};

                // Act
                var result = new Solution().SpiralOrder(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 5, 8, -1, 0, 4}, result);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[][] matrix = {new[] {2, 3, 4}, new[] {5, 6, 7}, new[] {8, 9, 10}, new[] {11, 12, 13}, new[] {14, 15, 16}};

                // Act
                var result = new Solution().SpiralOrder(matrix);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 3, 4, 7, 10, 13, 16, 15, 14, 11, 8, 5, 6, 9, 12}, result);
            }
        }
    }
}