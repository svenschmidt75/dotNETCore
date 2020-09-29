#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace GoogleProblem33
{
    public class Minesweeper
    {
        private static readonly (int, int)[] _neighbors = {(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)};
        private readonly int _nBombs;
        private readonly int _ncols;
        private readonly int _nrows;
        private int _nRevealedCells;

        /// <summary>
        /// Each cell is an array of size 2 with
        /// cell[0] = 0 => cell is not yet revealed, 1 => cell was revealed
        /// cell[1] = -1 => a bomb is placed, x: there are x bombs in surrounding
        /// cells, 0: no bomb in surrounding cells   
        /// </summary>
        public Minesweeper(int nrows, int ncols, int nBombs, IBoardInitializer boardInitializer)
        {
            _nrows = nrows;
            _ncols = ncols;
            _nBombs = nBombs;

            // SS: initialize grid/game board
            Grid = new int[nrows][][];

            for (var i = 0; i < nrows; i++)
            {
                Grid[i] = new int[ncols][];

                for (var j = 0; j < ncols; j++)
                {
                    Grid[i][j] = new int[2];
                }
            }

            boardInitializer.Initialize(nrows, ncols, nBombs, Grid);

            // SS: O(n) runtime complexity
            Preprocess(nrows, ncols);
        }

        internal int[][][] Grid { get; }

        private void Preprocess(int nrows, int ncols)
        {
            // SS: preprocess grid
            for (var i = 0; i < nrows; i++)
            {
                var row = Grid[i];

                for (var j = 0; j < ncols; j++)
                {
                    var cell = row[j];

                    if (cell[1] == -1)
                    {
                        // SS: cell is a bomb, nothing to do
                        continue;
                    }

                    var nBombs = 0;

                    // SS: cell is not a bomb, count neighbors that are bombs
                    for (var k = 0; k < _neighbors.Length; k++)
                    {
                        var (rOffset, cOffset) = _neighbors[k];
                        var nr = i + rOffset;
                        if (nr < 0 || nr == nrows)
                        {
                            continue;
                        }

                        var nc = j + cOffset;
                        if (nc < 0 || nc == ncols)
                        {
                            continue;
                        }

                        if (Grid[nr][nc][1] == -1)
                        {
                            nBombs++;
                        }
                    }

                    // SS: assign number of bombs to cell
                    cell[1] = nBombs;
                }
            }
        }

        public bool Click(int rowClick, int colClick)
        {
            // SS: O(n) runtime complexity

            if (rowClick < 0 || rowClick >= _nrows || colClick < 0 || colClick >= _ncols)
            {
                return false;
            }

            var queue = new Queue<(int r, int c)>();
            queue.Enqueue((rowClick, colClick));

            while (queue.Any())
            {
                var (r, c) = queue.Dequeue();
                var cell = Grid[r][c];

                if (cell[0] == 1)
                {
                    // cell already revealed, nothing to do
                    continue;
                }

                // SS: reveal cell
                cell[0] = 1;
                _nRevealedCells++;

                if (cell[1] == -1)
                {
                    // clicked on bomb, game over
                    PrintGrid(Grid);
                    Console.WriteLine("You lost - Game over");
                    return false;
                }

                // SS: if clicked on empty cell, add all its neighbors
                // if clicked on a cell that has bombs as neighbors,
                // don't do anything
                if (cell[1] == 0)
                {
                    for (var k = 0; k < _neighbors.Length; k++)
                    {
                        var (rOffset, cOffset) = _neighbors[k];
                        var nr = r + rOffset;
                        if (nr < 0 || nr == _nrows)
                        {
                            continue;
                        }

                        var nc = c + cOffset;
                        if (nc < 0 || nc == _ncols)
                        {
                            continue;
                        }

                        queue.Enqueue((nr, nc));
                    }
                }
            }

            return _nRevealedCells == _nrows * _ncols - _nBombs;
        }

        private void PrintGrid(int[][][] grid)
        {
            for (var i = 0; i < _nrows; i++)
            {
                for (var j = 0; j < _ncols; j++)
                {
                    var cell = Grid[i][j];

                    if (cell[0] == 0)
                    {
                        // SS: cell has net yet been revealed
                        Console.Write("00 ");
                    }
                    else
                    {
                        if (cell[1] == -1)
                        {
                            // SS: bomb
                            Console.Write("** ");
                        }
                        else
                        {
                            Console.Write($"{cell[1]:N2} ");
                        }
                    }
                }

                Console.WriteLine();
            }
        }

        [TestFixture]
        public class Tests
        {
            private class TestBoardInitializer1 : IBoardInitializer
            {
                void IBoardInitializer.Initialize(int nrows, int ncols, int nBombs, int[][][] grid)
                {
                    // SS: place bomb in cell (0, 4)
                    grid[0][4][1] = -1;
                }
            }

            [Test]
            public void TestPreprocessing()
            {
                // Arrange
                var boardInitializer = new TestBoardInitializer1();
                var game = new Minesweeper(2, 8, 1, boardInitializer);

                // Act
                // Assert

                Assert.AreEqual(1, game.Grid[0][3][1]);
                Assert.AreEqual(1, game.Grid[0][5][1]);
                Assert.AreEqual(1, game.Grid[1][3][1]);
                Assert.AreEqual(1, game.Grid[1][4][1]);
                Assert.AreEqual(1, game.Grid[1][5][1]);

                // verify that cells are not revealed
                Assert.AreEqual(0, game.Grid[0][4][0]);
            }

            [Test]
            public void Test1MoveOnce()
            {
                // Arrange
                var boardInitializer = new TestBoardInitializer1();
                var game = new Minesweeper(2, 8, 1, boardInitializer);

                // Act
                var wonGame = game.Click(0, 0);

                // Assert
                Assert.False(wonGame);

                // check that cells are uncovered
                Assert.AreEqual(1, game.Grid[0][0][0]);
                Assert.AreEqual(1, game.Grid[0][1][0]);
                Assert.AreEqual(1, game.Grid[0][2][0]);
                Assert.AreEqual(1, game.Grid[0][3][0]);
                Assert.AreEqual(0, game.Grid[0][4][0]);
                Assert.AreEqual(0, game.Grid[0][5][0]);
                Assert.AreEqual(0, game.Grid[0][6][0]);
                Assert.AreEqual(0, game.Grid[0][7][0]);

                Assert.AreEqual(1, game.Grid[1][0][0]);
                Assert.AreEqual(1, game.Grid[1][1][0]);
                Assert.AreEqual(1, game.Grid[1][2][0]);
                Assert.AreEqual(1, game.Grid[1][3][0]);
                Assert.AreEqual(0, game.Grid[1][4][0]);
                Assert.AreEqual(0, game.Grid[1][5][0]);
                Assert.AreEqual(0, game.Grid[1][6][0]);
                Assert.AreEqual(0, game.Grid[1][7][0]);
            }

            [Test]
            public void Test1MoveTwice()
            {
                // Arrange
                var boardInitializer = new TestBoardInitializer1();
                var game = new Minesweeper(2, 8, 1, boardInitializer);
                game.Click(0, 0);

                // Act
                var wonGame = game.Click(0, 7);

                // Assert
                Assert.False(wonGame);

                // check that cells are uncovered
                Assert.AreEqual(1, game.Grid[0][0][0]);
                Assert.AreEqual(1, game.Grid[0][1][0]);
                Assert.AreEqual(1, game.Grid[0][2][0]);
                Assert.AreEqual(1, game.Grid[0][3][0]);
                Assert.AreEqual(0, game.Grid[0][4][0]);
                Assert.AreEqual(1, game.Grid[0][5][0]);
                Assert.AreEqual(1, game.Grid[0][6][0]);
                Assert.AreEqual(1, game.Grid[0][7][0]);

                Assert.AreEqual(1, game.Grid[1][0][0]);
                Assert.AreEqual(1, game.Grid[1][1][0]);
                Assert.AreEqual(1, game.Grid[1][2][0]);
                Assert.AreEqual(1, game.Grid[1][3][0]);
                Assert.AreEqual(0, game.Grid[1][4][0]);
                Assert.AreEqual(1, game.Grid[1][5][0]);
                Assert.AreEqual(1, game.Grid[1][6][0]);
                Assert.AreEqual(1, game.Grid[1][7][0]);
            }

            [Test]
            public void Test1MoveThree()
            {
                // Arrange
                var boardInitializer = new TestBoardInitializer1();
                var game = new Minesweeper(2, 8, 1, boardInitializer);
                game.Click(0, 0);
                game.Click(0, 7);

                // Act
                var wonGame = game.Click(1, 4);

                // Assert
                Assert.True(wonGame);

                // check that cells are uncovered
                Assert.AreEqual(1, game.Grid[0][0][0]);
                Assert.AreEqual(1, game.Grid[0][1][0]);
                Assert.AreEqual(1, game.Grid[0][2][0]);
                Assert.AreEqual(1, game.Grid[0][3][0]);
                Assert.AreEqual(0, game.Grid[0][4][0]);
                Assert.AreEqual(1, game.Grid[0][5][0]);
                Assert.AreEqual(1, game.Grid[0][6][0]);
                Assert.AreEqual(1, game.Grid[0][7][0]);

                Assert.AreEqual(1, game.Grid[1][0][0]);
                Assert.AreEqual(1, game.Grid[1][1][0]);
                Assert.AreEqual(1, game.Grid[1][2][0]);
                Assert.AreEqual(1, game.Grid[1][3][0]);
                Assert.AreEqual(1, game.Grid[1][4][0]);
                Assert.AreEqual(1, game.Grid[1][5][0]);
                Assert.AreEqual(1, game.Grid[1][6][0]);
                Assert.AreEqual(1, game.Grid[1][7][0]);
            }

            [Test]
            public void TestFail()
            {
                // Arrange
                var boardInitializer = new TestBoardInitializer1();
                var game = new Minesweeper(2, 8, 1, boardInitializer);

                // Act
                var wonGame = game.Click(0, 4);

                // Assert
                Assert.False(wonGame);
            }
        }
    }
}