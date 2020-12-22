#region

using System;
using NUnit.Framework;

#endregion

// Problem: 69. Sqrt(x)
// URL: https://leetcode.com/problems/sqrtx/

namespace LeetCode
{
    public class Solution
    {
        public int MySqrt(int x)
        {
            return MySqrt3(x);
        }

        private int MySqrt2(int x)
        {
            // SS: runtime complexity: O(x)
            // space complexity: O(1)
            var i = 0;
            var prev = 0;

            while (i * i <= x)
            {
                prev = i;
                i++;
            }

            return prev;
        }

        private int MySqrt3(int x)
        {
            // SS: runtime complexity: O(log x)
            // space complexity: O(1)
            long min = 0;
            var max = (long) x + 1;
            while (min < max)
            {
                var mid = min + (max - min) / 2;

                if (mid * mid == x)
                {
                    return (int) mid;
                }

                if (mid * mid > x)
                {
                    max = mid;
                }
                else
                {
                    min = mid + 1;
                }
            }

            return (int) Math.Max(0, min - 1);
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(0, 0)]
            [TestCase(1, 1)]
            [TestCase(4, 2)]
            [TestCase(8, 2)]
            [TestCase(834789345, 28892)]
            [TestCase(int.MaxValue, 46340)]
            public void Test1(int x, int expected)
            {
                // Arrange

                // Act
                var result = new Solution().MySqrt(x);

                // Assert
                Assert.AreEqual(expected, result);
            }
        }
    }
}