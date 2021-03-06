﻿#region

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

#endregion

// 1293. Shortest Path in a Grid with Obstacles Elimination
// https://leetcode.com/problems/shortest-path-in-a-grid-with-obstacles-elimination/

namespace L1293
{
    public class Solution
    {
        private static readonly (int r, int c)[] Directions = {(-1, 0), (0, -1), (0, 1), (1, 0)};

        public int ShortestPath3(int[][] grid, int k)
        {
            var nrows = grid.Length;
            var ncols = grid[0].Length;

            int [][][] memGrid = new int[nrows][][];
            for (var i = 0; i < nrows; i++)
            {
                memGrid[i] = new int[ncols][];
            }

            for (var i = 0; i < nrows; i++)
            {
                for (var j = 0; j < ncols; j++)
                {
                    memGrid[i][j] = new int[k + 1];

                    for (var p = 0; p <= k; p++)
                    {
                        memGrid[i][j][p] = -1;
                    }
                }
            }

            memGrid[0][0][0] = 0;

            var queue = new Queue<(int r, int c)>();
            queue.Enqueue((0, 0));

            while (queue.Any())
            {
                var (row, col) = queue.Dequeue();
                
                for (var i = 0; i < 4; i++)
                {
                    var r = row + Directions[i].r;
                    var c = col + Directions[i].c;
                    if (r >= 0 && r < nrows && c >= 0 && c < ncols)
                    {
                        var changed = PropagteCell(grid, k, memGrid, row, col, r, c);
                        if (changed)
                        {
                            queue.Enqueue((r, c));
                        }
                    }
                }
            }

            var dist = int.MaxValue;
            for (var p = 0; p <= k; p++)
            {
                var distance = memGrid[nrows - 1][ncols - 1][p];
                if (distance > -1)
                {
                    dist = Math.Min(dist, distance);
                }
            }

            return dist == int.MaxValue ? -1 : dist;
        }

        public int ShortestPath2(int[][] grid, int k)
        {
            var nrows = grid.Length;
            var ncols = grid[0].Length;

            int [][][] memGrid = new int[nrows][][];
            for (var i = 0; i < nrows; i++)
            {
                memGrid[i] = new int[ncols][];
            }

            for (var i = 0; i < nrows; i++)
            {
                for (var j = 0; j < ncols; j++)
                {
                    memGrid[i][j] = new int[k + 1];

                    for (var p = 0; p <= k; p++)
                    {
                        memGrid[i][j][p] = -1;
                    }
                }
            }

            memGrid[0][0][0] = 0;

            // Pass 1: add distances and number of obstacles removed to each cell
            for (var i = 0; i < nrows; i++)
            {
                for (var j = 0; j < ncols; j++)
                {
                    if (j > 0)
                    {
                        PropagteCell(grid, k, memGrid, i, j - 1, i, j);
                    }

                    if (i > 0)
                    {
                        PropagteCell(grid, k, memGrid, i - 1, j, i, j);
                    }
                }
            }

            // Pass 2: propagate
            for (var i = 0; i < nrows; i++)
            {
                for (var j = 0; j < ncols; j++)
                {
                    PropagateDFS(grid, k, memGrid, i, j, new HashSet<(int row, int col)> {(i, j)});
                }
            }

            var dist = int.MaxValue;
            for (var p = 0; p <= k; p++)
            {
                var distance = memGrid[nrows - 1][ncols - 1][p];
                if (distance > -1)
                {
                    dist = Math.Min(dist, distance);
                }
            }

            return dist == int.MaxValue ? -1 : dist;
        }

        private void PropagateDFS(int[][] grid, int k, int[][][] memGrid, int row, int col, HashSet<(int row, int col)> visited)
        {
            var nrows = grid.Length;
            var ncols = grid[0].Length;

            for (var p = 0; p <= k; p++)
            {
                var dist2 = memGrid[row][col][p];
                if (dist2 == -1)
                {
                    continue;
                }

                for (var i = 0; i < 4; i++)
                {
                    var r = row + Directions[i].r;
                    var c = col + Directions[i].c;
                    if (r >= 0 && r < nrows && c >= 0 && c < ncols)
                    {
                        if (visited.Contains((r, c)) == false)
                        {
                            visited.Add((r, c));

                            var changed = PropagteCell(grid, k, memGrid, row, col, r, c);
                            if (changed)
                            {
                                PropagateDFS(grid, k, memGrid, r, c, visited);
                            }
                        }
                    }
                }
            }
        }

        private bool PropagteCell(int[][] grid, int k, int [][][] memGrid, int r1, int c1, int r2
            , int c2)
        {
            var changed = false;

            for (var p = 0; p <= k; p++)
            {
                var dist1 = memGrid[r1][c1][p];
                if (dist1 == -1)
                {
                    // SS: nothing to propagate
                    continue;
                }

                var dist2 = memGrid[r2][c2][p];

                // SS: distance has not been set for this cell an k
                if (dist2 == -1)
                {
                    // SS: are we propagating to a cell with an obstacle?
                    if (grid[r2][c2] == 1)
                    {
                        // SS: can we remove this obstacle?
                        if (p + 1 <= k)
                        {
                            // SS: is the path shorter?
                            dist2 = memGrid[r2][c2][p + 1];
                            if (dist2 == -1 || dist2 > dist1 + 1)
                            {
                                memGrid[r2][c2][p + 1] = dist1 + 1;
                                changed = true;
                            }
                        }
                    }
                    else
                    {
                        // SS: are we propagating to a cell without an obstacle
                        memGrid[r2][c2][p] = dist1 + 1;
                        changed = true;
                    }
                }
                else
                {
                    if (dist2 > dist1 + 1)
                    {
                        // SS: are we propagating to a cell with an obstacle?
                        if (grid[r2][c2] == 1)
                        {
                            if (p + 1 <= k)
                            {
                                memGrid[r2][c2][p + 1] = dist1 + 1;
                                changed = true;
                            }
                        }
                        else
                        {
                            memGrid[r2][c2][p] = dist1 + 1;
                            changed = true;
                        }
                    }
                }
            }

            return changed;
        }

        public int ShortestPath1(int[][] grid, int k)
        {
            if (grid.Length < 1)
            {
                return -1;
            }

            var visited = new HashSet<(int row, int col)>
            {
                (0, 0)
            };
            var (k2, steps) = DFS(grid, 0, 0, k, 0, 0, visited);
            return k2 <= k ? steps : -1;
        }

        private (int k, int steps) DFS(int[][] grid, int row, int column, int maxK, int k, int steps
            , HashSet<(int row, int col)> visited)
        {
            var nrows = grid.Length;
            var ncols = grid[0].Length;

            if (row == nrows - 1 && column == ncols - 1)
            {
                // SS: found end
                return (k, steps);
            }

            int[] k2 = {k, k, k, k};
            int[] steps2 = {int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue};

            for (var i = 0; i < 4; i++)
            {
                var r = row + Directions[i].r;
                var c = column + Directions[i].c;
                if (r >= 0 && r < nrows && c >= 0 && c < ncols)
                {
                    if (visited.Contains((r, c)) == false)
                    {
                        var isWall = grid[r][c] == 1;
                        if (isWall == false || k + 1 <= maxK)
                        {
                            visited.Add((r, c));
                            (k2[i], steps2[i]) = DFS(grid, r, c, maxK, isWall ? k + 1 : k, steps + 1, visited);
                            visited.Remove((r, c));
                        }
                    }
                }
            }

            // SS: we are interested in the minimum length path
            var bestK = int.MaxValue;
            var bestSteps = int.MaxValue;

            for (var i = 0; i < 4; i++)
            {
                var ki = k2[i];
                var stepi = steps2[i];

                if (ki <= maxK && stepi < bestSteps)
                {
                    bestK = ki;
                    bestSteps = stepi;
                }
            }

            return (bestK, bestSteps < int.MaxValue ? bestSteps : int.MaxValue);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test11()
            {
                // Arrange
                var grid = new[]
                {
                    new[] {0, 0, 0}, new[] {1, 1, 0}, new[] {0, 0, 0}, new[] {0, 1, 1}, new[] {0, 0, 0}
                };

                var k = 1;

                // Act
                var result = new Solution().ShortestPath1(grid, k);

                // Assert
                Assert.AreEqual(6, result);
            }

            [Test]
            public void Test12()
            {
                // Arrange
                var grid = new[]
                {
                    new[] {0, 0, 0}, new[] {1, 1, 0}, new[] {0, 0, 0}, new[] {0, 1, 1}, new[] {0, 0, 0}
                };

                var k = 1;

                // Act
                var result = new Solution().ShortestPath3(grid, k);

                // Assert
                Assert.AreEqual(6, result);
            }

            [Test]
            public void Test21()
            {
                // Arrange
                var grid = new[]
                {
                    new[] {0, 1, 1}, new[] {1, 1, 1}, new[] {1, 0, 0}
                };

                var k = 1;

                // Act
                var result = new Solution().ShortestPath1(grid, k);

                // Assert
                Assert.AreEqual(-1, result);
            }

            [Test]
            public void Test22()
            {
                // Arrange
                var grid = new[]
                {
                    new[] {0, 1, 1}, new[] {1, 1, 1}, new[] {1, 0, 0}
                };

                var k = 1;

                // Act
                var result = new Solution().ShortestPath2(grid, k);

                // Assert
                Assert.AreEqual(-1, result);
            }

//            [Test]
// disable test since runtime too high
            public void Test31()
            {
                // Arrange
                var grid = new[]
                {
                    new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                };

                var k = 5;

                // Act
                var result = new Solution().ShortestPath1(grid, k);

                // Assert
                Assert.AreEqual(387, result);
            }

            [Test]
            public void Test32()
            {
                // Arrange
                var grid = new[]
                {
                    new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 0
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                    , new[]
                    {
                        0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1
                        , 1, 1, 1, 1, 1, 1, 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
                        , 0, 0, 0, 0, 0, 0, 0, 0
                    }
                };

                var k = 5;

                // Act
                var result = new Solution().ShortestPath3(grid, k);

                // Assert
                Assert.AreEqual(387, result);
            }

            [Test]
            public void Test41()
            {
                // Arrange
                var grid = new[]
                {
                    new[] {0, 1, 1, 0, 0, 0}
                    , new[] {0, 1, 0, 0, 1, 0}
                    , new[] {0, 0, 0, 1, 1, 0}
                };

                var k = 1;

                // Act
                var result = new Solution().ShortestPath1(grid, k);

                // Assert
                Assert.AreEqual(9, result);
            }

            [Test]
            public void Test42()
            {
                // Arrange
                var grid = new[]
                {
                    new[] {0, 1, 1, 0, 0, 0}
                    , new[] {0, 1, 0, 0, 1, 0}
                    , new[] {0, 0, 0, 1, 1, 0}
                };

                var k = 1;

                // Act
                var result = new Solution().ShortestPath2(grid, k);

                // Assert
                Assert.AreEqual(9, result);
            }

            [Test]
            public void Test51()
            {
                // Arrange
                var grid = new[]
                {
                    new[] {0, 1, 1, 0, 0, 0}
                    , new[] {0, 1, 0, 0, 1, 0}
                    , new[] {0, 0, 0, 1, 1, 0}
                };

                var k = 2;

                // Act
                var result = new Solution().ShortestPath1(grid, k);

                // Assert
                Assert.AreEqual(7, result);
            }

            [Test]
            public void Test52()
            {
                // Arrange
                var grid = new[]
                {
                    new[] {0, 1, 1, 0, 0, 0}
                    , new[] {0, 1, 0, 0, 1, 0}
                    , new[] {0, 0, 0, 1, 1, 0}
                };

                var k = 2;

                // Act
                var result = new Solution().ShortestPath2(grid, k);

                // Assert
                Assert.AreEqual(7, result);
            }

            [Test]
            public void Test62()
            {
                // Arrange
                var grid = new[]
                {
                    new[] {0}
                };

                var k = 1;

                // Act
                var result = new Solution().ShortestPath2(grid, k);

                // Assert
                Assert.AreEqual(0, result);
            }

            [Test]
            public void Test72()
            {
                // Arrange
                var grid = new[]
                {
                    new[]
                    {
                        0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0
                        , 1, 1
                    }
                    , new[]
                    {
                        1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 1, 1, 1, 0, 1, 0
                        , 0, 0
                    }
                    , new[]
                    {
                        1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1
                        , 0, 0
                    }
                    , new[]
                    {
                        0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 1, 0, 0, 1, 1
                        , 0, 0
                    }
                    , new[]
                    {
                        1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0
                        , 1, 0
                    }
                    , new[]
                    {
                        0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0
                        , 1, 1
                    }
                    , new[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0
                        , 1, 0
                    }
                    , new[]
                    {
                        1, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1
                        , 0, 1
                    }
                    , new[]
                    {
                        1, 1, 1, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0
                        , 1, 0
                    }
                };

                var k = 283;

                // Act
                var result = new Solution().ShortestPath3(grid, k);

                // Assert
                Assert.AreEqual(41, result);
            }
        }
    }
}