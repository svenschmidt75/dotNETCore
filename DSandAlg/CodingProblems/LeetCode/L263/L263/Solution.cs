#region

using NUnit.Framework;

#endregion

// Problem: 263. Ugly Number
// URL: https://leetcode.com/problems/ugly-number/

namespace LeetCode
{
    public class Solution
    {
        public bool IsUgly(int n)
        {
            if (n == 0)
            {
                return false;
            }

            while (true)
            {
                if (n == 1)
                {
                    return true;
                }

                if (n % 2 == 0)
                {
                    n /= 2;
                    continue;
                }

                if (n % 3 == 0)
                {
                    n /= 3;
                    continue;
                }

                if (n % 5 == 0)
                {
                    n /= 5;
                    continue;
                }

                return false;
            }
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(0, false)]
            [TestCase(1, true)]
            [TestCase(6, true)]
            [TestCase(8, true)]
            [TestCase(14, false)]
            [TestCase(int.MaxValue, false)]
            [TestCase(int.MinValue, false)]
            public void Test(int n, bool isUglyExpected)
            {
                // Arrange

                // Act
                var isUgly = new Solution().IsUgly(n);

                // Assert
                Assert.AreEqual(isUglyExpected, isUgly);
            }
        }
    }
}