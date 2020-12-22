#region

using NUnit.Framework;

#endregion

// Problem: 70. Climbing Stairs
// URL: https://leetcode.com/problems/climbing-stairs/

namespace LeetCode
{
    public class Solution
    {
        public int ClimbStairs(int n)
        {
            // SS: DP, recurrence relation:
            // f(n) = f(n - 1) + f(n - 2)
            // f(1) = 1 (can only do one step)
            // f(2) = 2 (can do one step of size 2, or two of size 1)
            if (n <= 2)
            {
                return n;
            }

            var dp = new int[n + 1];
            dp[0] = 0;
            dp[1] = 1;
            dp[2] = 2;

            for (var i = 3; i <= n; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }

            // SS: notice that we are creating the Fibonacci sequence, i.e.
            // the number of distinct ways to climb for n is the nth Fibonacci
            // number...
            return dp[^1];
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(1, 1)]
            [TestCase(2, 2)]
            [TestCase(3, 3)]
            [TestCase(4, 5)]
            [TestCase(9, 55)]
            public void Test(int n, int expectedWays)
            {
                // Arrange

                // Act
                var nWays = new Solution().ClimbStairs(n);

                // Assert
                Assert.AreEqual(expectedWays, nWays);
            }
        }
    }
}