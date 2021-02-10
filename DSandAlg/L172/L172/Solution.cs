#region

using NUnit.Framework;

#endregion

// Problem: 172. Factorial Trailing Zeroes
// URL: https://leetcode.com/problems/factorial-trailing-zeroes/

namespace LeetCode
{
    public class Solution
    {
        public int TrailingZeroes(int n)
        {
            // SS: count number of 5s in decomposition of n!
            // runtime complexity: O(log_{5} n)
            var nFives = 0;
            var fac = 5;
            while (fac <= n)
            {
                nFives += n / fac;
                fac *= 5;
            }

            return nFives;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(1, 0)]
            [TestCase(5, 1)]
            [TestCase(10, 2)]
            [TestCase(25, 6)]
            public void Test(int n, int expected)
            {
                // Arrange

                // Act
                var nTrailingZeros = new Solution().TrailingZeroes(n);

                // Assert
                Assert.AreEqual(expected, nTrailingZeros);
            }
        }
    }
}