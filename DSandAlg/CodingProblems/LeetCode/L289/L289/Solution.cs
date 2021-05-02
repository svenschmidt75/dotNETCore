#region

using NUnit.Framework;

#endregion

// Problem: 289. Game of Life
// URL: https://leetcode.com/problems/game-of-life/

namespace LeetCode
{
    public class Solution
    {
        private static readonly int[][] NeighborOffsets =
        {
            new[] {-1, -1}, new[] {-1, 0}, new[] {-1, 1}, new[] {0, -1}, new[] {0, 1}, new[] {1, -1}, new[] {1, 0}, new[] {1, 1}
        };

        public void GameOfLife(int[][] board)
        {
            // GameOfLife1(board);
            GameOfLife2(board);
        }

        private void GameOfLife2(int[][] board)
        {
            // SS: in-place
            // runtime complexity: O(n * m) or linear in the number of cells
            // space complexity: O(1)

            // SS: apply one round
            for (var i = 0; i < board.Length; i++)
            {
                for (var j = 0; j < board[i].Length; j++)
                {
                    var cellState = board[i][j];
                    var liveNeighbors = GetLiveNeighbors2(board, i, j);

                    var newCellState = 0;

                    if (cellState == 1)
                    {
                        if (liveNeighbors == 2 || liveNeighbors == 3)
                        {
                            // SS: was alive, is alive
                            newCellState = 1;
                        }
                        else
                        {
                            // SS: was alive, is dead
                            newCellState = 3;
                        }
                    }
                    else if (liveNeighbors == 3)
                    {
                        // SS: was dead, is alive
                        newCellState = 2;
                    }

                    board[i][j] = newCellState;
                }
            }

            // SS: transform
            for (var i = 0; i < board.Length; i++)
            {
                for (var j = 0; j < board[i].Length; j++)
                {
                    var state = board[i][j];
                    if (state == 2)
                    {
                        // SS: was dead, is alive
                        state = 1;
                    }
                    else if (state == 3)
                    {
                        // SS: was alive, is dead
                        state = 0;
                    }

                    board[i][j] = state;
                }
            }
        }

        private void GameOfLife1(int[][] board)
        {
            // SS: not in-place
            // runtime complexity: O(n * m) or linear in the number of cells
            // space complexity: O(n * m) or linear in the number of cells

            var nextState = new int[board.Length][];
            for (var i = 0; i < board.Length; i++)
            {
                nextState[i] = new int[board[i].Length];
            }

            // SS: apply one round
            for (var i = 0; i < board.Length; i++)
            {
                for (var j = 0; j < board[i].Length; j++)
                {
                    var cellState = board[i][j];
                    var liveNeighbors = GetLiveNeighbors(board, i, j);

                    var newCellState = 0;

                    if (cellState == 1)
                    {
                        if (liveNeighbors == 2 || liveNeighbors == 3)
                        {
                            newCellState = 1;
                        }
                    }
                    else if (liveNeighbors == 3)
                    {
                        newCellState = 1;
                    }

                    nextState[i][j] = newCellState;
                }
            }

            // SS: transfer state
            for (var i = 0; i < board.Length; i++)
            {
                for (var j = 0; j < board[i].Length; j++)
                {
                    board[i][j] = nextState[i][j];
                }
            }
        }

        private static int GetLiveNeighbors2(int[][] board, int i, int j)
        {
            // SS: return number of neighbors alive 
            var nAlive = 0;

            foreach (var offset in NeighborOffsets)
            {
                var nr = i + offset[0];
                var nc = j + offset[1];
                if (nr < 0 || nc < 0 || nr == board.Length || nc == board[0].Length)
                {
                    continue;
                }

                var cellState = board[nr][nc];
                if (cellState == 1 || cellState == 3)
                {
                    nAlive++;
                }
            }

            return nAlive;
        }

        private static int GetLiveNeighbors(int[][] board, int i, int j)
        {
            // SS: return number of neighbors alive 
            var nAlive = 0;

            foreach (var offset in NeighborOffsets)
            {
                var nr = i + offset[0];
                var nc = j + offset[1];
                if (nr < 0 || nc < 0 || nr == board.Length || nc == board[0].Length)
                {
                    continue;
                }

                nAlive += board[nr][nc];
            }

            return nAlive;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] board =
                {
                    new[] {0, 1, 0}, new[] {0, 0, 1}, new[] {1, 1, 1}, new[] {0, 0, 0}
                };

                // Act
                new Solution().GameOfLife(board);

                // Assert
                int[][] expectedBoard =
                {
                    new[] {0, 0, 0}, new[] {1, 0, 1}, new[] {0, 1, 1}, new[] {0, 1, 0}
                };
                CollectionAssert.AreEqual(expectedBoard, board);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] board =
                {
                    new[] {1, 1}, new[] {1, 0}
                };

                // Act
                new Solution().GameOfLife(board);

                // Assert
                int[][] expectedBoard =
                {
                    new[] {1, 1}, new[] {1, 1}
                };
                CollectionAssert.AreEqual(expectedBoard, board);
            }
        }
    }
}