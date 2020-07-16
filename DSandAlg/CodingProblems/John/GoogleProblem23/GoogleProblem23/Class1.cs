#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace GoogleProblem23
{
    public class MinRectangleAxisAligned
    {
        public int Solve(int[][] points)
        {
            // SS: sort points by row, so we can make certain assumptions later...
            var sortedByRow = points.OrderBy(p => p[1]).ToArray();

            // SS: associate points with columns and rows
            var pointsByColumn = new Dictionary<int, List<int>>();
            var pointsByRow = new Dictionary<int, List<int>>();
            var minRow = int.MaxValue;
            var maxRow = int.MinValue;
            var minCol = int.MaxValue;
            var maxCol = int.MinValue;
            for (var i = 0; i < sortedByRow.Length; i++)
            {
                var point = sortedByRow[i];
                var row = point[1];
                var col = point[0];

                minRow = Math.Min(minRow, row);
                maxRow = Math.Max(maxRow, row);

                minCol = Math.Min(minCol, col);
                maxCol = Math.Max(maxCol, col);

                List<int> ps;
                if (pointsByColumn.TryGetValue(col, out ps) == false)
                {
                    ps = new List<int>();
                    pointsByColumn[col] = ps;
                }

                ps.Add(i);

                if (pointsByRow.TryGetValue(row, out ps) == false)
                {
                    ps = new List<int>();
                    pointsByRow[row] = ps;
                }

                ps.Add(i);
            }


            // SS: find rectangles
            var minRectangleArea = int.MaxValue;

            var currentColumn = minCol;
            while (currentColumn <= maxCol)
            {
                if (pointsByColumn.ContainsKey(currentColumn) == false)
                {
                    currentColumn++;
                    continue;
                }

                var pointsInColumn1 = pointsByColumn[currentColumn];
                if (pointsInColumn1.Count < 2)
                {
                    currentColumn++;
                    continue;
                }

                var minColArea = int.MaxValue;

                var nextColumn = currentColumn + 1;
                while (nextColumn <= maxCol)
                {
                    if (pointsByColumn.ContainsKey(nextColumn) == false)
                    {
                        nextColumn++;
                        continue;
                    }

                    var pointsInColumn2 = pointsByColumn[nextColumn];
                    if (pointsInColumn2.Count < 2)
                    {
                        nextColumn++;
                        continue;
                    }

                    var minArea = Process(pointsByColumn, pointsByRow, currentColumn, nextColumn, sortedByRow);

                    if (minArea < int.MaxValue)
                    {
                        // SS: we found some rectangles
                        minColArea = Math.Min(minColArea, minArea);

                        // SS: do not check next column, as rectangles will have larger area
                        break;
                    }

                    nextColumn++;
                }

                if (minColArea < int.MaxValue)
                {
                    // SS: we found some rectangles
                    minRectangleArea = Math.Min(minRectangleArea, minColArea);
                }

                currentColumn++;
            }

            return minRectangleArea;
        }

        private int Process(Dictionary<int, List<int>> pointsByColumn, Dictionary<int, List<int>> pointsByRow
            , int column1, int column2, int[][] points)
        {
            var minArea = int.MaxValue;

            var col1Points = pointsByColumn[column1];
            var col2Points = pointsByColumn[column2];

            for (var i = 0; i <= col1Points.Count - 2; i++)
            {
                var p11 = col1Points[i];

                for (var j = i + 1; j < col1Points.Count; j++)
                {
                    var p12 = col1Points[j];

                    for (var m = 0; m <= col2Points.Count - 2; m++)
                    {
                        var p21 = col2Points[m];

                        for (var n = m + 1; n < col2Points.Count; n++)
                        {
                            var p22 = col2Points[n];

                            // SS: check whether these points form a rectangle
                            var area = GetRectangleArea(points[p11], points[p12], points[p21], points[p22]);
                            minArea = Math.Min(minArea, area);
                        }
                    }
                }
            }

            return minArea;
        }

        private int GetRectangleArea(int[] p11, int[] p12, int[] p21, int[] p22)
        {
            if (p11[1] != p21[1])
            {
                // SS: not in same row...
                return int.MaxValue;
            }

            if (p12[1] != p22[1])
            {
                // SS: not in same row...
                return int.MaxValue;
            }

            var colDst = p21[0] - p11[0];
            var rowDst = p12[1] - p11[1];
            var area = colDst * rowDst;
            return area;
        }
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
            var minArea = new MinRectangleAxisAligned().Solve(points);

            // Assert
            Assert.AreEqual(1, minArea);
        }
    }
}