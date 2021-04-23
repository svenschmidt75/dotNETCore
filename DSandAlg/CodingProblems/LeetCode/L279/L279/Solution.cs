#region

using System;
using NUnit.Framework;

#endregion

// Problem: 279. Perfect Squares
// URL: https://leetcode.com/problems/perfect-squares/

namespace LeetCode
{
    public class Solution
    {
        public int NumSquares(int n)
        {
//            return NumSquares1(n);
            return NumSquares2(n);
//            return NumSquares3(n);
        }

        private int NumSquares3(int n)
        {
            // SS: Bottom-up Dynamic Programming
            // runtime complexity: O(n * sqrt(n))
            // space complexity: O(sqrt(n))

            // SS: max. perfect number index
            var maxPNIdx = (int) Math.Sqrt(n);

            var dp = new int[maxPNIdx + 1];
            for (var i = 1; i <= maxPNIdx; i++)
            {
                dp[i] = int.MaxValue;
            }

            for (var i = 1; i <= n; i++)
            {
                for (var pnIdx = 0; pnIdx < maxPNIdx; pnIdx++)
                {
                    var perfectSquare = (pnIdx + 1) * (pnIdx + 1);
                    if (i < perfectSquare)
                    {
                        break;
                    }

                    var count = i / perfectSquare;
                    
                    // SS: the remainder is always < perfectSquare
                    
                    var c = count > 0 ? count + dp[i - count * perfectSquare] : int.MaxValue;
                    dp[i] = Math.Min(dp[i], c);
                }
            }

            return dp[n];
        }

        private int NumSquares2(int n)
        {
            // SS: Bottom-up Dynamic Programming
            // runtime complexity: O(n * sqrt(n))
            // space complexity: O(n)

            // SS: max. perfect number index
            var maxPNIdx = (int) Math.Sqrt(n);

            var dp = new int[n + 1];
            for (var i = 1; i <= n; i++)
            {
                dp[i] = int.MaxValue;
            }

            for (var i = 1; i <= n; i++)
            {
                for (var pnIdx = 0; pnIdx < maxPNIdx; pnIdx++)
                {
                    var perfectSquare = (pnIdx + 1) * (pnIdx + 1);
                    if (i < perfectSquare)
                    {
                        break;
                    }

                    var count = i / perfectSquare;
                    var c = count > 0 ? count + dp[i - count * perfectSquare] : int.MaxValue;
                    dp[i] = Math.Min(dp[i], c);
                }
            }

            return dp[n];
        }

        private int NumSquares1(int n)
        {
            // SS: Divide & Conquer
            // Runtime complexity: O(2^n)
            // space complexity: O(n) (stack space due to recursion)

            long Solve(long n2, int pnIdx)
            {
                if (n2 == 0)
                {
                    // SS: we have a solution
                    return 0;
                }

                if (n2 < 0 || pnIdx == 0)
                {
                    // SS: no solution
                    return int.MaxValue;
                }

                var perfectNumber = pnIdx * pnIdx;

                // SS: take perfect number
                long c1 = int.MaxValue;
                var count1 = n2 / perfectNumber;
                if (count1 > 0)
                {
                    c1 = count1 + Solve(n2 - count1 * perfectNumber, pnIdx - 1);
                }

                // SS: do not take perfect number
                var c2 = Solve(n2, pnIdx - 1);

                var cMin = Math.Min(c1, c2);
                return cMin;
            }

            var min = (int) Solve(n, (int) Math.Sqrt(n));
            return min;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(1, 1)]
            [TestCase(2, 2)]
            [TestCase(3, 3)]
            [TestCase(4, 1)]
            [TestCase(5, 2)]
            [TestCase(6, 3)]
            [TestCase(7, 4)]
            [TestCase(8, 2)]
            [TestCase(9, 1)]
            [TestCase(10, 2)]
            [TestCase(11, 3)]
            [TestCase(12, 3)]
            [TestCase(13, 2)]
            [TestCase(7346, 2)]
            [TestCase(234, 2)]
            [TestCase(96, 3)]
            [TestCase(4236, 3)]
            public void Test(int n, int expected)
            {
                // Arrange

                // Act
                var c = new Solution().NumSquares(n);

                // Assert
                Assert.AreEqual(expected, c);
            }
        }
    }
}