#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace RatInMaze
{
    public static class RatInMaze
    {
        public static List<(int row, int col)> Solve(int[] maze, int n)
        {
            // SS: Rat in a Maze, start point is (0, 0)
            // 1: valid cell, 0: dead end
            var path = new List<(int, int)>();

            if (maze.Length < 2)
            {
                return path;
            }

            var visited = new HashSet<int> {0};
            Solve(maze, n, 0, visited, path);
            return path;
        }

        private static bool Solve(int[] maze, int n, int index, HashSet<int> visited, List<(int, int)> path)
        {
            var (r, c) = FromIndex(index, n);
            path.Add((r, c));

            if (r == n - 1 && c == n - 1)
            {
                // SS: we reached the destination
                return true;
            }

            // SS: neighbors
            var ns = new[] {(0, -1), (-1, 0), (0, 1), (1, 0)};
            foreach (var (rOffset, cOffset) in ns)
            {
                var neighborIndex = ToIndex(r + rOffset, c + cOffset, n);
                if (neighborIndex >= 0 && neighborIndex < maze.Length)
                {
                    if (maze[neighborIndex] == 1 && visited.Contains(neighborIndex) == false)
                    {
                        visited.Add(neighborIndex);
                        if (Solve(maze, n, neighborIndex, visited, path))
                        {
                            // SS: we reached the destination
                            return true;
                        }
                    }
                }
            }

            // SS: no path
            path.RemoveAt(path.Count - 1);
            return false;
        }

        private static int ToIndex(int row, int col, int n)
        {
            return row * n + col;
        }

        private static (int row, int col) FromIndex(int index, int n)
        {
            var row = index / n;
            var col = index - row * n;
            return (row, col);
        }
    }

    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var maze = new[]
            {
                1, 0, 0, 0, 1, 1, 0, 1, 0, 1, 0, 0, 1, 1, 1, 1
            };

            // Act
            var path = RatInMaze.Solve(maze, 4);

            // Act
            CollectionAssert.AreEqual(new[] {(0, 0), (1, 0), (1, 1), (2, 1), (3, 1), (3, 2), (3, 3)}, path);
        }
    }
}