#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace GoogleProblem19
{
    /// <summary>
    ///     Given a 1d array with unique integers. Apply compression with these rules:
    ///     1. smallest value is array is 1 (after compression)
    ///     2. largest value is as small as possible
    ///     3. preserve ordering, i.e. if x_i < x_j before sort, then c_i
    ///     < c_j after compression
    ///         Example:
    ///         Input: [10, 20, 30]
    ///         Output: [1, 2, 3]
    ///         Follow-up: Similar, but given 2 d array. The above rules apply by column and row.
    ///         Example:
    ///         Input: [[20, 30, 40],
    ///         [50, 60, 10]]
    ///         Output: [[1, 2, 3]
    ///     ,[2, 3, 1]]
    /// </summary>
    public class Solution
    {
        public static int[] Solve1D(int[] input)
        {
            var result = new int[input.Length];

            // sort at O(n log n)
            var sorted = input.Select((v, idx) => (idx, v)).OrderBy(t => t.v).ToArray();

            for (var i = 0; i < sorted.Length; i++)
            {
                var idx = sorted[i].idx;
                result[idx] = i + 1;
            }

            return result;
        }

        public static int[][] Solve2D(int[][] input)
        {
            // TODO SS: deal with special cases


            // SS: runtime complexity: O(nrows * ncols + nrows * (ncols * log(ncols) + ncols^2) + ncols * (nrows * log(nrows) + nrows^2) + (ncols + nrows) * ncols * nrows + ncols * nrows)
            // space complexity: 

            var nrows = input.Length;
            var ncols = input[0].Length;

            var result = new int[nrows][];

            for (var i = 0; i < nrows; i++)
            {
                result[i] = new int[ncols];
            }

            // create directed graph such that vertices connect to neighboring vertices if they have
            // a smaller value
            // O(nrows * ncols)
            var graph = new Graph();
            for (var i = 0; i < nrows; i++)
            {
                for (var j = 0; j < ncols; j++)
                {
                    var index = i * ncols + j;
                    graph.AddVertex(index);
                }
            }

            // connect vertices within a row
            // O(nrows * (ncols * log(ncols) + ncols^2))
            for (var row = 0; row < nrows; row++)
            {
                var r = input[row];

                // sort at O(ncols * log(ncols))
                var sorted = r.Select((v, idx) => (idx, v)).OrderByDescending(t => t.v).ToArray();

                // connect vertices
                // this is O(ncols^2)...
                for (var j = 0; j <= ncols - 2; j++)
                {
                    var colIdx1 = sorted[j].idx;
                    var c1Idx = row * ncols + colIdx1;
                    var c1 = input[row][colIdx1];

                    for (var k = j + 1; k < ncols; k++)
                    {
                        var colIdx2 = sorted[k].idx;
                        var c2Idx = row * ncols + colIdx2;
                        var c2 = input[row][colIdx2];

                        graph.AddDirectedEdge(c1Idx, c2Idx);
                    }
                }
            }

            // connect vertices within a column
            // O(ncols * (nrows * log(nrows) + nrows^2))
            for (var col = 0; col < ncols; col++)
            {
                var r = new int[nrows];
                for (var i = 0; i < nrows; i++)
                {
                    var c = input[i][col];
                    r[i] = c;
                }

                // sort at O(nrows * log(nrows))
                var sorted = r.Select((v, idx) => (idx, v)).OrderByDescending(t => t.v).ToArray();

                // connect vertices
                // this is O(nrows^2)...
                for (var j = 0; j <= nrows - 2; j++)
                {
                    var rowIdx1 = sorted[j].idx;
                    var c1Idx = rowIdx1 * ncols + col;
                    var c1 = input[rowIdx1][col];

                    for (var k = j + 1; k < nrows; k++)
                    {
                        var rowIdx2 = sorted[k].idx;
                        var c2Idx = rowIdx2 * ncols + col;
                        var c2 = input[rowIdx2][col];

                        graph.AddDirectedEdge(c1Idx, c2Idx);
                    }
                }
            }

            // DFS
            // O(E + V) = O((ncols + nrows) * ncols * nrows + ncols * nrows)
            // O(E) = O((ncols + nrows) * ncols * nrows)
            // O(V) = O(ncols * nrows)
            var res = new int[ncols * nrows];
            var visited = new HashSet<int>();
            for (var i = 0; i < nrows; i++)
            {
                for (var j = 0; j < ncols; j++)
                {
                    var index = i * ncols + j;

                    if (visited.Contains(index))
                    {
                        result[i][j] = res[index];
                        continue;
                    }

                    visited.Add(index);
                    var value = ProcessVertex(graph, index, visited, res);
                    res[index] = value;
                    result[i][j] = value;
                }
            }

            return result;
        }

        private static int ProcessVertex(Graph graph, int vertex, HashSet<int> visited, int[] res)
        {
            var neighbors = graph.AdjacencyList[vertex];

            var idx = 0;

            foreach (var neighbor in neighbors)
            {
                int i;

                if (visited.Contains(neighbor))
                {
                    i = res[neighbor];
                }
                else
                {
                    visited.Add(neighbor);
                    i = ProcessVertex(graph, neighbor, visited, res);
                }

                idx = Math.Max(idx, i);
            }

            res[vertex] = idx + 1;
            return res[vertex];
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var input = new[] {10, 20, 30};

            // Act
            var result = Solution.Solve1D(input);

            // Act
            CollectionAssert.AreEqual(new[] {1, 2, 3}, result);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var input = new[] {5, 7, 9, 3, 6, 1, 2};

            // Act
            var result = Solution.Solve1D(input);

            // Act
            CollectionAssert.AreEqual(new[] {4, 6, 7, 3, 5, 1, 2}, result);
        }

        [Test]
        public void Test2D_1()
        {
            // Arrange
            var input = new[] {new[] {20, 30, 40}, new[] {50, 60, 10}};

            // Act
            var result = Solution.Solve2D(input);

            // Act
            CollectionAssert.AreEqual(new[] {new[] {1, 2, 3}, new[] {2, 3, 1}}, result);
        }

        [Test]
        public void Test2D_2()
        {
            // Arrange
            var input = new[] {new[] {20, 30, 40}, new[] {60, 50, 10}};

            // Act
            var result = Solution.Solve2D(input);

            // Act
            CollectionAssert.AreEqual(new[] {new[] {1, 2, 3}, new[] {4, 3, 1}}, result);
        }

        [Test]
        public void Test2D_3()
        {
            // Arrange
            var input = new[] {new[] {6, 9, 15}, new[] {3, 1, 7}, new[] {8, 2, 4}};

            // Act
            var result = Solution.Solve2D(input);

            // Act
            CollectionAssert.AreEqual(new[] {new[] {3, 4, 5}, new[] {2, 1, 4}, new[] {4, 2, 3}}, result);
        }

        [Test]
        public void Test2D_4()
        {
            // Arrange
            var input = new[] {new[] {1, 3, 4}, new[] {1, 2, 3}};

            // Act
            var result = Solution.Solve2D(input);

            // Act
            CollectionAssert.AreEqual(new[] {new[] {2, 3, 4}, new[] {1, 2, 3}}, result);
        }
    }
}