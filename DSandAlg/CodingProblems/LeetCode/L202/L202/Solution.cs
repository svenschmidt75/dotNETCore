#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 202. Happy Number
// URL: https://leetcode.com/problems/happy-number/

namespace LeetCode
{
    public class Solution
    {
        public bool IsHappy(int n)
        {
            // SS: runtime performance: ?
            // space complexity: ?

            // SS: to detect cycles. Notice that cycles might nor form until
            // after a few values have been seen, so we need to keep all of them.
            var dict = new HashSet<int> {n};

            var number = n;

            while (true)
            {
                var p = number;
                var sumOfSquares = 0;
                while (p > 0)
                {
                    var digit = p % 10;
                    sumOfSquares += digit * digit;

                    // SS: move by one digit to right
                    p /= 10;
                }

                if (sumOfSquares == 1)
                {
                    return true;
                }

                if (dict.Contains(sumOfSquares))
                {
                    // SS: we have a cycle
                    return false;
                }

                dict.Add(sumOfSquares);

                number = sumOfSquares;
            }
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(19, true)]
            [TestCase(2, false)]
            [TestCase(8, false)]
            [TestCase(1, true)]
            [TestCase(int.MaxValue, false)]
            public void Test1(int n, bool expectedIsHappy)
            {
                // Arrange

                // Act
                var isHappy = new Solution().IsHappy(n);

                // Assert
                Assert.AreEqual(expectedIsHappy, isHappy);
            }
        }
    }
}