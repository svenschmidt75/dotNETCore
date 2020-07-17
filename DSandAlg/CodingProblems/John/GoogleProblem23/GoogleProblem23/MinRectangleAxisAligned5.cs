#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace GoogleProblem23
{
    public class MinRectangleAxisAligned5
    {
        public int MinAreaRect(int[][] points)
        {
            var orderedByColumn = points.OrderBy(p => p[0]).ToArray();

            var byColumn = new Dictionary<int, List<int>>();
            var byRow = new Dictionary<int, List<int>>();

            var byDiag1 = new Dictionary<int, List<int>>();
            var byDiag2 = new Dictionary<int, List<int>>();

            var pointsMap = new Dictionary<(int row, int col), int>();

            for (var i = 0; i < orderedByColumn.Length; i++)
            {
                var point = orderedByColumn[i];
                var row = point[1];
                var col = point[0];

                // SS: only one point per position
                pointsMap[(row, col)] = i;


                List<int> ps;
                if (byColumn.TryGetValue(col, out ps) == false)
                {
                    ps = new List<int>();
                    byColumn[col] = ps;
                }

                ps.Add(i);


                if (byRow.TryGetValue(row, out ps) == false)
                {
                    ps = new List<int>();
                    byRow[row] = ps;
                }

                ps.Add(i);


                var d1 = row - col;
                if (byDiag1.TryGetValue(d1, out ps) == false)
                {
                    ps = new List<int>();
                    byDiag1[d1] = ps;
                }

                ps.Add(i);


                var d2 = col + row;
                if (byDiag2.TryGetValue(d2, out ps) == false)
                {
                    ps = new List<int>();
                    byDiag2[d2] = ps;
                }

                ps.Add(i);
            }

            // SS: order row indices
            var rowIndices = byRow.Keys.OrderBy(x => x);
            foreach (var rowIndex in rowIndices)
            {
                var pointsInRow = byRow[rowIndex].OrderBy(idx => points[idx][0]).ToList();
                byRow[rowIndex] = pointsInRow;
            }

            // SS: order column indices
            var colIndices = byColumn.Keys.OrderBy(x => x);
            foreach (var colIndex in colIndices)
            {
                var pointsInCol = byColumn[colIndex].OrderBy(idx => points[idx][1]).ToList();
                byColumn[colIndex] = pointsInCol;
            }

            // SS: order diagonal indices
            var diag1Indices = byDiag1.Keys.OrderBy(x => x);
            foreach (var diag1Index in diag1Indices)
            {
                var pointsInDiag1 = byDiag1[diag1Index].OrderBy(idx => points[idx][0]).ToList();
                byDiag1[diag1Index] = pointsInDiag1;
            }

            // SS: order diagonal indices
            var diag2Indices = byDiag2.Keys.OrderBy(x => x);
            foreach (var diag2Index in diag2Indices)
            {
                var pointsInDiag2 = byDiag2[diag2Index].OrderByDescending(idx => points[idx][0]).ToList();
                byDiag2[diag2Index] = pointsInDiag2;
            }

            return 0;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var points = new[]
                {
                    new[] {1, 1}, new[] {1, 4}, new[] {2, 2}, new[] {3, 1}, new[] {3, 3}, new[] {3, 4}, new[] {4, 1}
                    , new[] {4, 3}, new[] {4, 4}
                };

                // Act
                var minArea = new MinRectangleAxisAligned5().MinAreaRect(points);

                // Assert
                Assert.AreEqual(1, minArea);
            }
        }
    }
}