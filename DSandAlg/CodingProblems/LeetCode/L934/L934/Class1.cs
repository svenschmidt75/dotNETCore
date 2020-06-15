#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// 934. Shortest Bridge
// https://leetcode.com/problems/shortest-bridge/


namespace L934
{
    public class Solution
    {
        /// <summary>
        ///     1. find a single 1, part of the 1st cluster
        ///     2. find and mark all 1s connected to the 1st cluster
        ///     3. do a BFS keeping track of the level
        ///     4. the next unmarked 1 we find is part of the 2nd cluster.
        ///     Since we do BFS, we are guaranteed to find the shortest
        ///     path.
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public int ShortestBridge(int[][] A)
        {
            var nrows = A.Length;
            var ncols = A[0].Length;

            // find a single 1
            var row = -1;
            var col = -1;
            for (var r = 0; r < nrows; r++)
            {
                for (var c = 0; c < ncols; c++)
                {
                    var val = A[r][c];
                    if (val == 1)
                    {
                        row = r;
                        col = c;
                        break;
                    }
                }

                if (row != -1)
                {
                    break;
                }
            }

            var neighbors = new[] {(-1, 0), (0, -1), (1, 0), (0, 1)};


            var q = new Queue<(int row, int col)>();
            q.Enqueue((row, col));

            var visited = new HashSet<(int row, int col)>();
            visited.Add((row, col));

            var q2 = new Queue<(int row, int col)>();

            // extract and mark all 1s part of the 1st cluster
            while (q.Any())
            {
                var (r, c) = q.Dequeue();
                q2.Enqueue((r, c));

                foreach (var neighbor in neighbors)
                {
                    var (nr, nc) = (r + neighbor.Item1, c + neighbor.Item2);
                    if (nr < 0 || nr == nrows || nc < 0 || nc == ncols)
                    {
                        continue;
                    }

                    if (visited.Contains((nr, nc)))
                    {
                        continue;
                    }

                    if (A[nr][nc] == 1)
                    {
                        visited.Add((nr, nc));
                        q.Enqueue((nr, nc));
                    }
                }
            }

            // BFS starting from all 1s in the 1st cluster
            var level = 0;

            while (q2.Any())
            {
                q = q2;
                q2 = new Queue<(int row, int col)>();

                level++;

                while (q.Any())
                {
                    var (r, c) = q.Dequeue();

                    // check neighbors
                    foreach (var neighbor in neighbors)
                    {
                        var (nr, nc) = (r + neighbor.Item1, c + neighbor.Item2);
                        if (nr < 0 || nr == nrows || nc < 0 || nc == ncols)
                        {
                            continue;
                        }

                        if (visited.Contains((nr, nc)))
                        {
                            continue;
                        }

                        // found 1 of 2nd cluster
                        if (A[nr][nc] == 1)
                        {
                            return level - 1;
                        }

                        visited.Add((nr, nc));
                        q2.Enqueue((nr, nc));
                    }
                }
            }

            return -1;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var input = new[] {new[] {0, 1}, new[] {1, 0}};

            // Act
            var dst = new Solution().ShortestBridge(input);

            // Assert
            Assert.AreEqual(1, dst);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var input = new[] {new[] {0, 1, 0}, new[] {0, 0, 0}, new[] {0, 0, 1}};

            // Act
            var dst = new Solution().ShortestBridge(input);

            // Assert
            Assert.AreEqual(2, dst);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var input = new[]
            {
                new[] {1, 1, 1, 1, 1}, new[] {1, 0, 0, 0, 1}, new[] {1, 0, 1, 0, 1}, new[] {1, 0, 0, 0, 1}
                , new[] {1, 1, 1, 1, 1}
            };

            // Act
            var dst = new Solution().ShortestBridge(input);

            // Assert
            Assert.AreEqual(1, dst);
        }

        [Test]
        public void Test4()
        {
            // Arrange
            var input = new[]
            {
                new[] {1, 1, 0, 0, 0}, new[] {1, 0, 0, 0, 0}, new[] {1, 0, 0, 0, 0}, new[] {0, 0, 0, 1, 1}
                , new[] {0, 0, 0, 1, 1}
            };

            // Act
            var dst = new Solution().ShortestBridge(input);

            // Assert
            Assert.AreEqual(3, dst);
        }
    }
}