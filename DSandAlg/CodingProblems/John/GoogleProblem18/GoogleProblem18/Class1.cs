#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// LeetCode 935. Knight Dialer
// https://leetcode.com/problems/knight-dialer/
// This is slightly different as the start pos is given...

namespace GoogleProblem18
{
    /// <summary>
    ///     Given a phone dial pad,
    ///     1 2 3
    ///     4 5 6
    ///     7 8 9
    ///     0
    ///     You can only move from one digit to the next using chess-knight
    ///     moves.
    ///     Input: - start position on dial pad
    ///     - number of moves
    ///     Output: number of unique phone numbers
    /// </summary>
    public class GoogleProblem18
    {
        private static readonly int[][] Moves =
        {
            new[] {4, 6} // knight-moves from 0
            , new[] {6, 8}
            , new[] {7, 9}
            , new[] {4, 8}
            , new[] {0, 3, 9}
            , new[] {5}
            , new[] {0, 1, 7}
            , new[] {2, 6}
            , new[] {1, 3}
            , new[] {2, 4}
        };

        public static int Solve(int startPos, int moves)
        {
            // runtime complexity: O(3^n)
            // space complexity: O(n), where n = number of moves
            var phoneNumbers = new HashSet<int>();
            SolveExp(startPos, moves, 0, phoneNumbers);
            return phoneNumbers.Count;
        }

        private static void SolveExp(int pos, int n, int phoneNumber, HashSet<int> phoneNumbers)
        {
            phoneNumber = phoneNumber * 10 + pos;

            if (n == 0)
            {
                phoneNumbers.Add(phoneNumber);
            }
            else
            {
                var moves = Moves[pos];
                foreach (var target in moves)
                {
                    SolveExp(target, n - 1, phoneNumber, phoneNumbers);
                }
            }
        }

        public static int SolveDP(int startPos, int moves)
        {
            // DP bottom-up solution
            // runtime complexity: O(n)
            // space complexity: O(n), where n = number of moves

            if (moves == 0)
            {
                return 1;
            }

            var grid = new[] {new int[10], new int[10]};

            var p1 = 0;
            var p2 = 1;

            for (var i = 0; i < 10; i++)
            {
                var nMoves = Moves[i].Length;
                grid[p1][i] = nMoves;
            }

            var k = 1;
            while (k < moves)
            {
                for (var i = 0; i < 10; i++)
                {
                    var nMoves = 0;

                    foreach (var target in Moves[i])
                    {
                        nMoves += grid[p1][target];
                    }

                    grid[p2][i] = nMoves;
                }

                var tmp = p1;
                p1 = p2;
                p2 = tmp;

                k++;
            }

            return grid[p1][startPos];
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange

            // Act
            var n = GoogleProblem18.Solve(1, 1);

            // Assert
            Assert.AreEqual(2, n);
        }

        [Test]
        public void Test2()
        {
            // Arrange

            // Act
            var n = GoogleProblem18.Solve(5, 10);

            // Assert
            Assert.AreEqual(1, n);
        }

        [Test]
        public void Test31()
        {
            // Arrange

            // Act
            var n = GoogleProblem18.Solve(9, 10);

            // Assert
            Assert.AreEqual(3728, n);
        }

        [Test]
        public void Test32()
        {
            // Arrange

            // Act
            var n = GoogleProblem18.SolveDP(9, 10);

            // Assert
            Assert.AreEqual(3728, n);
        }

        [Test]
        public void Test41()
        {
            // Arrange

            // Act
            var n = GoogleProblem18.Solve(1, 2);

            // Assert
            Assert.AreEqual(5, n);
        }

        [Test]
        public void Test42()
        {
            // Arrange

            // Act
            var n = GoogleProblem18.SolveDP(1, 2);

            // Assert
            Assert.AreEqual(5, n);
        }

        [Test]
        public void Test5()
        {
            // Arrange

            // Act
            var n = Enumerable.Range(0, 10).Select(digit => GoogleProblem18.Solve(digit, 0)).Sum();

            // Assert
            Assert.AreEqual(10, n);
        }

        [Test]
        public void Test6()
        {
            // Arrange

            // Act
            var n = Enumerable.Range(0, 10).Select(digit => GoogleProblem18.Solve(digit, 1)).Sum();

            // Assert
            Assert.AreEqual(21, n);
        }

        [Test]
        public void Test7()
        {
            // Arrange

            // Act
            var n = Enumerable.Range(0, 10).Select(digit => GoogleProblem18.Solve(digit, 2)).Sum();

            // Assert
            Assert.AreEqual(47, n);
        }
    }
}