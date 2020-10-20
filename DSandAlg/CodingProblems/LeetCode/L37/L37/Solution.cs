#region

using NUnit.Framework;

#endregion

// 37. Sudoku Solver
// https://leetcode.com/problems/sudoku-solver/

namespace L37
{
    public class Solution
    {
        public void SolveSudoku(char[][] board)
        {
            Solve(board, 0, 0);
            ConvertNumbers(board);
        }

        private static void ConvertNumbers(char[][] board)
        {
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    var cellValue = board[i][j];
                    if (cellValue > '9')
                    {
                        var newCellValue = (char) ('0' + -cellValue);
                        board[i][j] = newCellValue;
                    }
                }
            }
        }

        private bool Solve(char[][] board, int row, int col)
        {
            if (row > 8)
            {
                // SS: we have filled in all cells
                return false;
            }

            if (col > 8)
            {
                return Solve(board, row + 1, 0);
            }

            // SS: check whether this cell contains a 'fixed' number
            if (board[row][col] >= '1' && board[row][col] <= '9')
            {
                return Solve(board, row, col + 1);
            }

            // SS: brute-force test all numbers
            for (var i = 1; i < 10; i++)
            {
                // SS: a cell is a 3x3 subgrid
                if (CellContainsNumber(board, row, col, (char) i))
                {
                    continue;
                }

                if (RowContainsNumber(board, row, (char) i))
                {
                    continue;
                }

                if (ColumnContainsNumber(board, col, (char) i))
                {
                    continue;
                }

                // SS: insert number into cell
                board[row][col] = (char) -i;

                var violation = Solve(board, row, col + 1);
                if (violation == false)
                {
                    return false;
                }

                // SS: we have a violation, revert number
                board[row][col] = '.';
            }

            // SS: we have a violation, none of the numbers from 1 to 9 work
            return true;
        }

        private bool ColumnContainsNumber(char[][] board, int col, char number)
        {
            for (var i = 0; i < 9; i++)
            {
                var cellValue = board[i][col];
                if (cellValue == number || (char) -cellValue == number || cellValue == number + '0')
                {
                    return true;
                }
            }

            return false;
        }

        private bool RowContainsNumber(char[][] board, int row, char number)
        {
            for (var i = 0; i < 9; i++)
            {
                var cellValue = board[row][i];
                if (cellValue == number || (char) -cellValue == number || cellValue == number + '0')
                {
                    return true;
                }
            }

            return false;
        }

        private bool CellContainsNumber(char[][] board, int row, int col, char number)
        {
            var cr = row / 3;
            var cc = col / 3;

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    var r = cr * 3 + i;
                    var c = cc * 3 + j;

                    var cellValue = board[r][c];
                    if (cellValue == number || (char) -cellValue == number || cellValue == number + '0')
                    {
                        return true;
                    }
                }
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
                new Solution().SolveSudoku(board);

                // Assert
                CollectionAssert.AreEqual(new[] {'5', '3', '4', '6', '7', '8', '9', '1', '2'}, board[0]);
                CollectionAssert.AreEqual(new[] {'6', '7', '2', '1', '9', '5', '3', '4', '8'}, board[1]);
                CollectionAssert.AreEqual(new[] {'1', '9', '8', '3', '4', '2', '5', '6', '7'}, board[2]);
                CollectionAssert.AreEqual(new[] {'8', '5', '9', '7', '6', '1', '4', '2', '3'}, board[3]);
                CollectionAssert.AreEqual(new[] {'4', '2', '6', '8', '5', '3', '7', '9', '1'}, board[4]);
                CollectionAssert.AreEqual(new[] {'7', '1', '3', '9', '2', '4', '8', '5', '6'}, board[5]);
                CollectionAssert.AreEqual(new[] {'9', '6', '1', '5', '3', '7', '2', '8', '4'}, board[6]);
                CollectionAssert.AreEqual(new[] {'2', '8', '7', '4', '1', '9', '6', '3', '5'}, board[7]);
                CollectionAssert.AreEqual(new[] {'3', '4', '5', '2', '8', '6', '1', '7', '9'}, board[8]);
            }
        }
    }
}