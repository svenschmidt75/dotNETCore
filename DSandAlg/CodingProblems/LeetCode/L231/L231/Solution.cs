#region

using NUnit.Framework;

#endregion

// Problem: 231. Power of Two
// URL: https://leetcode.com/problems/power-of-two/

namespace LeetCode
{
    public class Solution
    {
        public bool IsPowerOfTwo(int n)
        {
            // SS: check if bit pattern is 10000...0
            if (n <= 0)
            {
                return false;
            }

            return (n & (n - 1)) == 0;
        }

        public bool IsPowerOfTwo3(int n)
        {
            // SS: check if bit pattern is 10000...0
            if (n <= 0)
            {
                return false;
            }

            // SS: check that only 1 bit is set
            var nOne = 0;
            for (var i = 0; i < 31; i++)
            {
                if (((n >> i) & 1) == 1)
                {
                    if (nOne == 1)
                    {
                        return false;
                    }

                    nOne++;
                }
            }

            return true;
        }

        public bool IsPowerOfTwo2(int n)
        {
            // SS: power of 2 must be even and > 1, or 1
            if (n == 1)
            {
                return true;
            }

            if (n <= 0 || n % 2 == 1)
            {
                return false;
            }

            return IsPowerOfTwo(n / 2);
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(1, true)]
            [TestCase(16, true)]
            [TestCase(3, false)]
            [TestCase(4, true)]
            [TestCase(5, false)]
            public void Test1(int n, bool expected)
            {
                // Arrange

                // Act
                var isPowerOfTwo = new Solution().IsPowerOfTwo(n);

                // Assert
                Assert.AreEqual(expected, isPowerOfTwo);
            }
        }
    }
}