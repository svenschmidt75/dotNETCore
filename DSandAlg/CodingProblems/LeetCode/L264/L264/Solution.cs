#region

using System;
using NUnit.Framework;

#endregion

// Problem: 264. Ugly Number II
// URL: https://leetcode.com/problems/ugly-number-ii/

namespace LeetCode
{
    public class Solution
    {
        public int NthUglyNumber(int n)
        {
            // SS: Ugly number is of the form 2^a * 3^b * 5^c
            // 3 pointer for a, b and c, using Bottom-Up Dynamic
            // Programming.
            // runtime complexity: O(n)
            // space complexity: O(n)
            var dp = new int[n];
            dp[0] = 1;

            var a = 0;
            var b = 0;
            var c = 0;

            var idx = 1;
            while (idx < n)
            {
                var an = 2 * dp[a];
                var bn = 3 * dp[b];
                var cn = 5 * dp[c];

                var min = Math.Min(an, bn);
                min = Math.Min(min, cn);
                dp[idx++] = min;

                if (an == min)
                {
                    a++;
                }

                if (bn == min)
                {
                    b++;
                }

                if (cn == min)
                {
                    c++;
                }
            }

            return dp[n - 1];
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(1, 1)]
            [TestCase(10, 12)]
            [TestCase(11, 15)]
            [TestCase(16, 25)]
            [TestCase(1690, 2123366400)]
            public void Test(int n, int expected)
            {
                // Arrange

                // Act
                var uglyNumber = new Solution().NthUglyNumber(n);

                // Assert
                Assert.AreEqual(expected, uglyNumber);
            }
        }
    }
}