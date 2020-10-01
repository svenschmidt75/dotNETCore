#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace L79
{
    public class Solution
    {
        private static readonly (int ro, int co)[] _neighbors = {(-1, 0), (0, -1), (0, 1), (1, 0)};

        public bool Exist(char[][] board, string word)
        {
            // SS: for each char that matches the first, do DFS
            // runtime complexity: O(N^2)

            var nrows = board.Length;
            var ncols = board[0].Length;

            if (string.IsNullOrEmpty(word) || nrows == 0 || ncols == 0)
            {
                return false;
            }

            // SS: check each cell
            for (var r = 0; r < nrows; r++)
            {
                for (var c = 0; c < ncols; c++)
                {
                    // SS: optimization
                    if (board[r][c] != word[0])
                    {
                        continue;
                    }

                    var visited = new HashSet<(int r, int c)> {(r, c)};
                    if (DFS(board, word, (r, c), 0, visited))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool DFS(char[][] board, string word, (int r, int c) cell, int index, HashSet<(int r, int c)> visited)
        {
            // SS: last char
            if (index == word.Length - 1)
            {
                return board[cell.r][cell.c] == word[^1];
            }

            var c = board[cell.r][cell.c];
            if (word[index] != c)
            {
                return false;
            }

            var nrows = board.Length;
            var ncols = board[0].Length;

            // SS: chars are the same, check neighbors
            for (var i = 0; i < _neighbors.Length; i++)
            {
                var row = cell.r + _neighbors[i].ro;
                var col = cell.c + _neighbors[i].co;

                if (row < 0 || row == nrows || col < 0 || col == ncols)
                {
                    continue;
                }

                if (visited.Contains((row, col)))
                {
                    continue;
                }

                visited.Add((row, col));
                if (DFS(board, word, (row, col), index + 1, visited))
                {
                    return true;
                }

                visited.Remove((row, col));
            }

            return false;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("ABCCED", true)]
            [TestCase("SEE", true)]
            [TestCase("ABCB", false)]
            public void Test1(string word, bool expectedResult)
            {
                // Arrange
                var board = new[]
                {
                    new[] {'A', 'B', 'C', 'E'}, new[] {'S', 'F', 'C', 'S'}, new[] {'A', 'D', 'E', 'E'}
                };

                // Act
                var result = new Solution().Exist(board, word);

                // Assert
                Assert.AreEqual(expectedResult, result);
            }
        }
    }
}