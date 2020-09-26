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
        private readonly int _ncols;
        private readonly int _nrows;
        private readonly int[][][] _grid;

        public Minesweeper(int nrows, int ncols, int nBombs, IBoardInitializer boardInitializer)
        {
            _nrows = nrows;
            _ncols = ncols;
            // SS: initialize grid/game board
            _grid = new int[nrows][][];

            for (var i = 0; i < nrows; i++)
            {
                _grid[i] = new int[ncols][];

                for (var j = 0; j < ncols; j++)
                {
                    _grid[i][j] = new int[2];
                }
            }

            boardInitializer.Initialize(nrows, ncols, nBombs, _grid);

            Preprocess(nrows, ncols);
        }

        private void Preprocess(int nrows, int ncols)
        {
            // SS: preprocess grid
            for (var i = 0; i < nrows; i++)
            {
                var row = _grid[i];

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

                        if (_grid[nr][nc][1] == -1)
                        {
                            nBombs++;
                        }
                    }

                    // SS: assign number of bombs to cell
                    cell[1] = nBombs;
                }
            }
        }

        public int[][][] Click(int rowClick, int colClick)
        {
            if (rowClick < 0 || rowClick >= _nrows || colClick < 0 || colClick >= _ncols)
            {
                return _grid;
            }

            var queue = new Queue<(int r, int c)>();
            queue.Enqueue((rowClick, colClick));

            while (queue.Any())
            {
                var (r, c) = queue.Dequeue();
                var cell = _grid[r][c];

                if (cell[0] == 1)
                {
                    // cell already revealed, nothing to do
                    continue;
                }

                // SS: reveal cell
                cell[0] = 1;

                if (cell[1] == -1)
                {
                    // clicked on bomb, game over
                    PrintGrid(_grid);
                    Console.WriteLine("You lost - Game over");
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

            return _grid;
        }

        private void PrintGrid(int[][][] grid)
        {
            for (int i = 0; i < _nrows; i++)
            {
                for (int j = 0; j < _ncols; j++)
                {
                    var cell = _grid[i][j];

                    if (cell[0] == 0)
                    {
                        // SS: cell has net yet been revealed
                        Console.Write("   ");
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
            class TestBoardInitializer : IBoardInitializer
            {
                void IBoardInitializer.Initialize(int nrows, int ncols, int nBombs, int[][][] grid) { }
            }

            [Test]
            public void Test1()
            {
                // Arrange

                // Act

                // Assert
            }
        }
    }
}