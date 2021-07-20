#region

using System.Collections.Generic;
using System.Security;
using NUnit.Framework;
using NUnit.Framework.Constraints;

#endregion

// Medium Google Coding Interview With Ben Awad
// https://www.youtube.com/watch?v=4tYoVx0QoN0

namespace RemoveIslands
{
    public class Solution
    {
        public void RemoveIslands(int[][] matrix)
        {
            RemoveIslands1(matrix);
        }

        public static void RemoveIslands1(int[][] matrix)
        {
            // SS: Mark all 1's reachable from the boundary
            // Remove unmarked 1s
            // runtime complexity: O(n), where n = #cells
            // sapce complexity: O(n), for visited array

            int nrows = matrix.Length;
            if (nrows <= 1)
            {
                return;
            }

            int ncols = matrix[0].Length;

            var visited = new HashSet<(int row, int col)>();

            void DFS(int row, int col)
            {
                // SS: already visited?
                if (visited.Contains((row, col)))
                {
                    return;
                }

                visited.Add((row, col));

                // SS: check all neighbors
                int[][] neighbors = {new[] {-1, 0}, new[] {1, 0}, new[] {0, -1}, new[] {0, 1}};
                foreach (int[] neighbor in neighbors)
                {
                    int r = row + neighbor[0];
                    int c = col + neighbor[1];

                    if (r < 0 || r == nrows || c < 0 || c == ncols)
                    {
                        // SS: invalid position
                        continue;
                    }

                    if (matrix[r][c] == 1)
                    {
                        DFS(r, c);
                    }
                }
            }

            // SS: check top and bottom row
            for (int c = 0; c < ncols; c++)
            {
                if (matrix[0][c] == 1 && visited.Contains((0, c)) == false)
                {
                    DFS(0, c);
                }

                if (matrix[nrows - 1][c] == 1 && visited.Contains((nrows - 1, c)) == false)
                {
                    DFS(nrows - 1, c);
                }
            }

            // SS: check left and right column
            for (int r = 1; r < nrows - 1; r++)
            {
                if (matrix[r][0] == 1 && visited.Contains((r, 0)) == false)
                {
                    DFS(r, 0);
                }

                if (matrix[r][ncols - 1] == 1 && visited.Contains((r, ncols - 1)) == false)
                {
                    DFS(r, ncols - 1);
                }
            }

            // SS: remove all unmarked 1s
            for (int r = 1; r < nrows - 1; r++)
            {
                for (int c = 1; c < ncols - 1; c++)
                {
                    if (matrix[r][c] == 1 && visited.Contains((r, c)) == false)
                    {
                        matrix[r][c] = 0;
                    }
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
            int[][] matrix =
            {
                new []{1, 0, 0, 0, 0, 0}
                , new []{0, 1, 0, 1, 1, 1}
                , new []{0, 0, 1, 0, 1, 0}
                , new []{1, 1, 0, 0, 1, 0}
                , new []{1, 0, 1, 1, 0, 0}
                , new []{1, 0, 0, 0, 0, 1}
            };

            // Act
            new Solution().RemoveIslands(matrix);

            // Assert
            int[][] expectedMatrix =
            {
                new []{1, 0, 0, 0, 0, 0}
                , new []{0, 0, 0, 1, 1, 1}
                , new []{0, 0, 0, 0, 1, 0}
                , new []{1, 1, 0, 0, 1, 0}
                , new []{1, 0, 0, 0, 0, 0}
                , new []{1, 0, 0, 0, 0, 1}
            };
            CollectionAssert.AreEqual(expectedMatrix, matrix);
        }
    }
}