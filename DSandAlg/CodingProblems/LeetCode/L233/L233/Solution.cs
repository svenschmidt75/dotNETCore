#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 233. Number of Digit One
// URL: https://leetcode.com/problems/number-of-digit-one/

namespace LeetCode
{
    public class Solution
    {
        public int CountDigitOne(int n)
        {
//            return CountDigitOne1(n);
            return CountDigitOne2(n);
        }

        private int CountDigitOne2(int n)
        {
            var r = CountDigitOne2_1(0, n, new Dictionary<(long, long), long>());
            return (int) r;
        }

        private long CountDigitOne2_1(long min, long max, IDictionary<(long, long), long> memoization)
        {
            // SS: use memoization to speed up
            if (memoization.ContainsKey((min, max)))
            {
                return memoization[(min, max)];
            }

            long m1 = Magnitude(min);
            long m2 = Magnitude(max);

            if (m1 == m2)
            {
                // SS: f(0..x), x <= 9
                if (max <= 9)
                {
                    var r = max < 1 ? 0 : 1;
                    memoization[(min, max)] = r;
                    return r;
                }

                // SS: min is 10, 100, 1000, ...
                m1 = min;
                m2 = Math.Min(2 * min - 1, max);
                long count = 0;
                while (m2 <= max)
                {
                    var d = m2 / min;
                    var tmp = CountDigitOne2_1(m1 % min, m2 % min, memoization);
                    count += tmp;
                    if (d == 1)
                    {
                        count += m2 - m1 + 1;
                    }

                    if (m2 == max)
                    {
                        break;
                    }

                    m1 = m2 + 1;
                    m2 = Math.Min(m2 + min, max);
                }

                memoization[(min, max)] = count;
                return count;
            }

            // SS: decompose in same magnitude
            long m = 0;
            long i = 10;
            long result = 0;

            while (m <= max)
            {
                var u = Math.Min(max, i - 1);
                var tmp = CountDigitOne2_1(m, u, memoization);
                memoization[(m, u)] = tmp;
                result += tmp;
                m = u + 1;
                i *= 10;
            }

            memoization[(min, max)] = result;
            return result;
        }

        private static int Magnitude(long n)
        {
            if (n == 0)
            {
                return 0;
            }

            return (int) Math.Log10(n);
        }

        private int CountDigitOne1(int n)
        {
            // SS: runtime complexity: O(n)
            // times out
            var count = 0;

            for (var i = 1; i <= n; i++)
            {
                var m = i;
                while (m > 0)
                {
                    var d = m % 10;
                    if (d == 1)
                    {
                        count++;
                    }

                    m /= 10;
                }
            }

            return count;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(0, 0)]
            [TestCase(10, 2)]
            [TestCase(13, 6)]
            [TestCase(19, 1 + 11)]
            [TestCase(70, 1 + 16)]
            [TestCase(100, 12 + 9)]
            [TestCase(110, 33)]
            [TestCase(154, 91)]
            [TestCase(300, 160)]
            [TestCase(1000, 301)]
            [TestCase(1374, 853)]
            [TestCase(3769, 2157)]
            [TestCase(23754, 20156)]
            [TestCase(3674545, 3240415)]
            [TestCase(1410065408, 1737167499)]
            [TestCase(65408, 36681)]
            public void Test1(int n, int expected)
            {
                // Arrange

                // Act

                // Assert
                Assert.AreEqual(expected, new Solution().CountDigitOne(n));
            }
        }
    }
}