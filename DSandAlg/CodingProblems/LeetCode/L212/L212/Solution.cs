#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 212. Word Search II
// URL: https://leetcode.com/problems/word-search-ii/

namespace LeetCode
{
    public class Solution
    {
        public IList<string> FindWords(char[][] board, string[] words)
        {
            var wordsFound = new List<string>();

            var wordFound = new int[words.Length];

            var nrows = board.Length;
            var ncols = board[0].Length;

            for (var r = 0; r < nrows; r++)
            {
                for (var c = 0; c < ncols; c++)
                {
                    // SS: for each cell, try each remaining word
                    for (var i = 0; i < wordFound.Length; i++)
                    {
                        if (wordFound[i] == 1)
                        {
                            continue;
                        }

                        var word = words[i];

                        var visited = new HashSet<(int, int)> {(r, c)};

                        // SS: try left-to-right character order
                        var found = DFS(board, r, c, word, 1, 0, visited);
                        if (found)
                        {
                            wordFound[i] = 1;
                        }
                        else
                        {
                            // SS: try right-to-left character order
                            found = DFS(board, r, c, word, -1, word.Length - 1, visited);
                            if (found)
                            {
                                wordFound[i] = 1;
                            }
                        }
                    }
                }
            }

            for (var i = 0; i < wordFound.Length; i++)
            {
                if (wordFound[i] == 1)
                {
                    wordsFound.Add(words[i]);
                }
            }

            return wordsFound;
        }

        private static bool DFS(char[][] board, int row, int col, string word, int direction, int pos, HashSet<(int, int)> visited)
        {
            var nrows = board.Length;
            var ncols = board[0].Length;

            if (board[row][col] != word[pos])
            {
                return false;
            }

            if (direction == 1 && pos == word.Length - 1 || direction == -1 && pos == 0)
            {
                // SS: we found the word
                return true;
            }

            // SS: visit neighbors
            int[][] neighbors = {new[] {-1, 0}, new[] {1, 0}, new[] {0, -1}, new[] {0, 1}};
            for (var i = 0; i < neighbors.Length; i++)
            {
                var r = row + neighbors[i][0];
                var c = col + neighbors[i][1];

                if (r < 0 || r == nrows || c < 0 || c == ncols)
                {
                    continue;
                }

                if (visited.Contains((r, c)))
                {
                    continue;
                }

                visited.Add((r, c));

                if (direction == 1)
                {
                    if (DFS(board, r, c, word, direction, pos + 1, visited))
                    {
                        return true;
                    }
                }
                else if (direction == -1)
                {
                    if (DFS(board, r, c, word, direction, pos - 1, visited))
                    {
                        return true;
                    }
                }

                // SS: backtrack
                visited.Remove((r, c));
            }

            return false;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                char[][] board = {new[] {'o', 'a', 'a', 'n'}, new[] {'e', 't', 'a', 'e'}, new[] {'i', 'h', 'k', 'r'}, new[] {'i', 'f', 'l', 'v'}};
                string[] words = {"oath", "pea", "eat", "rain"};

                // Act
                var wordsFound = new Solution().FindWords(board, words);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"eat", "oath"}, wordsFound);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                char[][] board = {new[] {'a', 'b'}, new[] {'c', 'd'}};
                string[] words = {"abcd"};

                // Act
                var wordsFound = new Solution().FindWords(board, words);

                // Assert
                Assert.IsEmpty(wordsFound);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                char[][] board = {new[] {'a', 'b'}, new[] {'c', 'd'}};
                string[] words = {"acdb"};

                // Act
                var wordsFound = new Solution().FindWords(board, words);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"acdb"}, wordsFound);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                char[][] board = {new[] {'o'}};
                string[] words = {"oath", "pea", "eat", "rain"};

                // Act
                var wordsFound = new Solution().FindWords(board, words);

                // Assert
                Assert.IsEmpty(wordsFound);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                char[][] board = {new[] {'a', 'b'}, new[] {'c', 'd'}};
                string[] words = {"acd"};

                // Act
                var wordsFound = new Solution().FindWords(board, words);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"acd"}, wordsFound);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                char[][] board = {new[] {'a'}, new[] {'a'}};
                string[] words = {"aaa"};

                // Act
                var wordsFound = new Solution().FindWords(board, words);

                // Assert
                Assert.IsEmpty(wordsFound);
            }
        }
    }
}