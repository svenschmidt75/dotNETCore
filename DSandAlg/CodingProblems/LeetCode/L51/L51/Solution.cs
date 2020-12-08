#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 51. N-Queens
// URL: https://leetcode.com/problems/n-queens/

namespace LeetCode51
{
    public class Solution
    {
        public IList<IList<string>> SolveNQueens(int n)
        {
            // SS: solution is using backtracking, exhaustive search
            // runtime complexity: O(N^3) I think, because
            // per row i, we touch (n - i) * n cells, and for each cell,
            // we do an O(n) check for violations.  

            var dp = new int[n][];
            for (var i = 0; i < n; i++)
            {
                dp[i] = new int[n];
            }

            IList<IList<string>> results = new List<IList<string>>();
            Solve(n, 0, dp, results);

            return results;
        }

        private static void Solve(int n, int r, int[][] dp, IList<IList<string>> results)
        {
            if (n == 0)
            {
                // SS: add solution
                var solution = GenerateSolution(dp);
                results.Add(solution);
                return;
            }

            var dim = dp.Length;

            if (r == dim)
            {
                return;
            }

            // SS: check every column of the current row
            for (var i = 0; i < dim; i++)
            {
                if (CanPlaceQueen(dim, r, i, dp))
                {
                    dp[r][i] = 1;
                    Solve(n - 1, r + 1, dp, results);

                    // SS: reset state
                    dp[r][i] = 0;
                }
            }
        }

        private static IList<string> GenerateSolution(int[][] dp)
        {
            var result = new List<string>();
            for (var i = 0; i < dp.Length; i++)
            {
                var row = new char[dp.Length];

                for (var j = 0; j < dp.Length; j++)
                {
                    var v = dp[i][j];
                    row[j] = v == 0 ? '.' : 'Q';
                }

                result.Add(new string(row));
            }

            return result;
        }

        private static bool CanPlaceQueen(int n, int r, int c, int[][] dp)
        {
            // SS: runtime complexity O(n) 

            // SS: check all 4 diagonals from (r, c)
            for (var i = 0; i < n; i++)
            {
                // SS: check same row
                if (dp[r][i] == 1)
                {
                    return false;
                }

                // SS: check same column
                if (dp[i][c] == 1)
                {
                    return false;
                }

                // SS: (r + 1, c + 1)
                if (r + i < n && c + i < n && dp[r + i][c + i] == 1)
                {
                    return false;
                }

                // SS: (r - 1, c + 1)
                if (r - i >= 0 && c + i < n && dp[r - i][c + i] == 1)
                {
                    return false;
                }

                // SS: (r - 1, c - 1)
                if (r - i >= 0 && c - i >= 0 && dp[r - i][c - i] == 1)
                {
                    return false;
                }

                // SS: (r + 1, c - 1)
                if (r + i < n && c - i >= n && dp[r + i][c - i] == 1)
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

                // Act
                var results = new Solution().SolveNQueens(4);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {".Q..", "...Q", "Q...", "..Q."}, new[] {"..Q.", "Q...", "...Q", ".Q.."}}, results);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var results = new Solution().SolveNQueens(1);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {"Q"}}, results);
            }
        }
    }
}