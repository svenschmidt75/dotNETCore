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
        public int ShortestBridge(int[][] A)
        {
            var nrows = A.Length;
            var ncols = A[0].Length;

            var temp = new int[nrows][];
            for (var i = 0; i < nrows; i++)
            {
                temp[i] = new int[ncols];
            }

            var visited = new HashSet<(int row, int col)>();

            // extract all 1s
            var q2 = new Queue<(int row, int col)>();

            for (var r = 0; r < nrows; r++)
            {
                for (var c = 0; c < ncols; c++)
                {
                    var val = A[r][c];
                    if (val == 1)
                    {
                        q2.Enqueue((r, c));
                        visited.Add((r, c));
                    }
                }
            }

            var neighbors = new[] {(-1, 0), (0, -1), (1, 0), (0, 1)};

            var level = 0;

            while (q2.Any())
            {
                var q = q2;
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

                        if (A[nr][nc] == 1)
                        {
                            continue;
                        }

                        if (temp[nr][nc] > 0)
                        {
                            var v = temp[nr][nc];
                            if (v == level)
                            {
                                return level;
                            }

                            return level + 1;
                        }

                        visited.Add((nr, nc));
                        q2.Enqueue((nr, nc));
                        temp[nr][nc] = level;
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