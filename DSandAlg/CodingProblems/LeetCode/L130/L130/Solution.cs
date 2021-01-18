#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 130. Surrounded Regions
// URL: https://leetcode.com/problems/surrounded-regions/

namespace LeetCode
{
    public class Solution
    {
        public void Solve(char[][] board)
        {
            // SS: runtime complexity: O(R * C)
            // space complexity: O(R * C)
            
            var nrows = board.Length;
            if (nrows == 0)
            {
                return;
            }

            var ncols = board[0].Length;
            if (ncols == 0)
            {
                return;
            }

            // SS: 1st pass: find O
            var toVisit = new HashSet<(int r, int c)>();
            var toVisitCells = new List<(int r, int c)>();
            for (var r = 0; r < nrows; r++)
            {
                for (var c = 0; c < ncols; c++)
                {
                    var cell = board[r][c];
                    if (cell == 'O')
                    {
                        toVisit.Add((r, c));
                        toVisitCells.Add((r, c));
                    }
                }
            }

            foreach (var (r, c) in toVisitCells)
            {
                if (toVisit.Contains((r, c)) == false)
                {
                    continue;
                }

                // SS: check region of Os
                var regionCells = new List<(int r, int c)>();

                var isValidRegion = true;

                var q = new Queue<(int r, int c)>();
                q.Enqueue((r, c));

                while (q.Any())
                {
                    var (r2, c2) = q.Dequeue();

                    if (toVisit.Contains((r2, c2)) == false)
                    {
                        continue;
                    }

                    toVisit.Remove((r2, c2));

                    regionCells.Add((r2, c2));

                    // SS: check if valid cell
                    if (r2 == 0 || r2 == nrows - 1 || c2 == 0 || c2 == ncols - 1)
                    {
                        // SS: we keep exploring, but it is invalid
                        isValidRegion = false;
                    }

                    if (r2 > 0)
                    {
                        if (board[r2 - 1][c2] == 'O' && toVisit.Contains((r2 - 1, c2)))
                        {
                            q.Enqueue((r2 - 1, c2));
                        }
                    }

                    if (r2 < nrows - 1)
                    {
                        if (board[r2 + 1][c2] == 'O' && toVisit.Contains((r2 + 1, c2)))
                        {
                            q.Enqueue((r2 + 1, c2));
                        }
                    }

                    if (c2 > 0)
                    {
                        if (board[r2][c2 - 1] == 'O' && toVisit.Contains((r2, c2 - 1)))
                        {
                            q.Enqueue((r2, c2 - 1));
                        }
                    }

                    if (c2 < ncols - 1)
                    {
                        if (board[r2][c2 + 1] == 'O' && toVisit.Contains((r2, c2 + 1)))
                        {
                            q.Enqueue((r2, c2 + 1));
                        }
                    }
                }

                if (isValidRegion)
                {
                    // SS: flip O to X
                    foreach (var cell in regionCells)
                    {
                        board[cell.r][cell.c] = 'X';
                    }
                }
            }
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                char[][] board =
                {
                    new[] {'X', 'X', 'X', 'X'}
                    , new[] {'X', 'O', 'O', 'X'}
                    , new[] {'X', 'X', 'O', 'X'}
                    , new[] {'X', 'O', 'X', 'X'}
                };

                // Act
                new Solution().Solve(board);

                // Assert
                CollectionAssert.AreEqual(new[]
                {
                    new[] {'X', 'X', 'X', 'X'}
                    , new[] {'X', 'X', 'X', 'X'}
                    , new[] {'X', 'X', 'X', 'X'}
                    , new[] {'X', 'O', 'X', 'X'}
                }, board);
            }
        }
    }
}