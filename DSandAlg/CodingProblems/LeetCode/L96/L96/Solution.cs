#region

using System;
using NUnit.Framework;

#endregion

// Problem: 96. Unique Binary Search Trees
// URL: https://leetcode.com/problems/unique-binary-search-trees/

namespace Leetcode
{
    public class Solution
    {
        public int NumTrees(int n)
        {
            // SS: Divide & Conquer
            // runtime performance: O(1 + 1 + 3 + 4 + ... + n) = O(N^2)
            // The idea is that for n = 4, we iterate over 1, 2, 3, 4 and for
            // each iteration, we have a sub problem on the left and right of
            // a size we have already solved, so we just lookup.
            // n = 4, i.e. P(4):
            //      1             2            3            4      
            //    /   \         /   \        /   \        /   \
            //  P(0)  P(3)   P(1)   P(2)   P(2)  P(1)   P(3)  P(0)
            // etc.
            // According to Larry, the numbers are the Catalan sequence, 
            // https://www.youtube.com/watch?v=y0r847XC7Z4...
            // Actually, this problem is described in Wikipedia as and
            // example where the Catalan sequence occurs:
            // https://en.wikipedia.org/wiki/Catalan_number

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
                var total = 0;

                for (var j = 1; j <= i; j++)
                {
                    var left = Math.Max(1, j - 1);
                    var right = Math.Max(1, i - j);
                    var v = dp[left] * dp[right];
                    total += v;
                }

                dp[i] = total;
            }

            return dp[n];
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(1, 1)]
            [TestCase(2, 2)]
            [TestCase(3, 5)]
            [TestCase(4, 14)]
            [TestCase(10, 16796)]
            [TestCase(19, 1767263190)]
            public void Test1(int n, int expectedCount)
            {
                // Arrange

                // Act
                var count = new Solution().NumTrees(n);

                // Assert
                Assert.AreEqual(expectedCount, count);
            }
        }
    }
}