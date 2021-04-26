#region

using NUnit.Framework;

#endregion

// Problem: 258. Add Digits
// URL: https://leetcode.com/problems/add-digits/

namespace LeetCode
{
    public class Solution
    {
        public int AddDigits(int num)
        {
//            return AddDigits1(num);
            return AddDigits2(num);
        }

        private int AddDigits2(int num)
        {
            // SS: Compute the digital root, https://en.wikipedia.org/wiki/Digital_root
            int dr;
            if (num == 0)
            {
                dr = 0;
            }
            else if (num % 9 == 0)
            {
                dr = 9;
            }
            else
            {
                dr = num % 9;
            }

            return dr;
        }

        private static int AddDigits1(int num)
        {
            var result = num;

            while (result > 9)
            {
                var sum = 0;

                while (result > 0)
                {
                    // SS: get digit
                    var d = result % 10;
                    sum += d;

                    // SS: shift digit to right
                    result /= 10;
                }

                result = sum;
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(0, 0)]
            [TestCase(38, 2)]
            [TestCase(int.MaxValue, 1)]
            public void Test(int n, int expected)
            {
                // Arrange

                // Act
                var sum = new Solution().AddDigits(n);

                // Assert
                Assert.AreEqual(expected, sum);
            }
        }
    }
}