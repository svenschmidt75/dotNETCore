#region

using NUnit.Framework;

#endregion

// Problem: 36. Valid Sudoku
// URL: https://leetcode.com/problems/valid-sudoku/

namespace LeetCode36
{
    public class Solution
    {
        public bool IsValidSudoku(char[][] board)
        {
            // SS: O(1)
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    // SS: O(1)
                    if (CheckRow(board, i, j) == false || CheckColumn(board, i, j) == false || CheckSquare(board, i, j) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CheckSquare(char[][] board, int i, int j)
        {
            var c = board[i][j];
            if (c == '.')
            {
                return true;
            }

            var d = c - '0';

            var sr = i / 3;
            var sc = j / 3;

            for (var a = 0; a < 3; a++)
            {
                var row = sr * 3 + a;

                for (var b = 0; b < 3; b++)
                {
                    var col = sc * 3 + b;

                    c = board[row][col];
                    if (c == '.')
                    {
                        continue;
                    }

                    var d2 = c - '0';
                    if (d2 == d && (row != i || col != j))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CheckColumn(char[][] board, int i, int j)
        {
            var c = board[i][j];
            if (c == '.')
            {
                return true;
            }

            var d = c - '0';

            for (var k = 0; k < 9; k++)
            {
                c = board[k][j];
                if (c == '.')
                {
                    continue;
                }

                var d2 = c - '0';
                if (d2 == d && k != i)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckRow(char[][] board, int i, int j)
        {
            var c = board[i][j];
            if (c == '.')
            {
                return true;
            }

            var d = c - '0';

            for (var k = 0; k < 9; k++)
            {
                c = board[i][k];
                if (c == '.')
                {
                    continue;
                }

                var d2 = c - '0';
                if (d2 == d && k != j)
                {
                    return false;
                }
            }

            return true;
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
                    new[] {'5', '3', '.', '.', '7', '.', '.', '.', '.'}
                    , new[] {'6', '.', '.', '1', '9', '5', '.', '.', '.'}
                    , new[] {'.', '9', '8', '.', '.', '.', '.', '6', '.'}
                    , new[] {'8', '.', '.', '.', '6', '.', '.', '.', '3'}
                    , new[] {'4', '.', '.', '8', '.', '3', '.', '.', '1'}
                    , new[] {'7', '.', '.', '.', '2', '.', '.', '.', '6'}
                    , new[] {'.', '6', '.', '.', '.', '.', '2', '8', '.'}
                    , new[] {'.', '.', '.', '4', '1', '9', '.', '.', '5'}
                    , new[] {'.', '.', '.', '.', '8', '.', '.', '7', '9'}
                };

                // Act
                var isValid = new Solution().IsValidSudoku(board);

                // Assert
                Assert.True(isValid);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                char[][] board =
                {
                    new[] {'8', '3', '.', '.', '7', '.', '.', '.', '.'}
                    , new[] {'6', '.', '.', '1', '9', '5', '.', '.', '.'}
                    , new[] {'.', '9', '8', '.', '.', '.', '.', '6', '.'}
                    , new[] {'8', '.', '.', '.', '6', '.', '.', '.', '3'}
                    , new[] {'4', '.', '.', '8', '.', '3', '.', '.', '1'}
                    , new[] {'7', '.', '.', '.', '2', '.', '.', '.', '6'}
                    , new[] {'.', '6', '.', '.', '.', '.', '2', '8', '.'}
                    , new[] {'.', '.', '.', '4', '1', '9', '.', '.', '5'}
                    , new[] {'.', '.', '.', '.', '8', '.', '.', '7', '9'}
                };

                // Act
                var isValid = new Solution().IsValidSudoku(board);

                // Assert
                Assert.False(isValid);
            }
        }
    }
}