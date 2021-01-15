#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 137. Single Number II
// URL: https://leetcode.com/problems/single-number-ii/

namespace LeetCode
{
    public class Solution
    {
        public int SingleNumber(int[] nums)
        {
            // SS: convert numbers to base3
            // 21 digits in base3 to represent 2^32
            const int maxDigits = 21;
            var digits = new uint[maxDigits];

            var nNegative = 0;

            for (var i = 0; i < nums.Length; i++)
            {
                var n = nums[i];

                if (n < 0)
                {
                    nNegative++;
                }

                var nUnsigned = (uint) n;
                if (n < 0)
                {
                    nUnsigned = ~(uint) n + 1;
                }

                // SS: convert to base3, unsigned
                for (var j = 0; j < maxDigits; j++)
                {
                    // SS: extract least significant digit
                    var digit = nUnsigned % 3;

                    // SS: shift digits 1 to the right
                    nUnsigned /= 3;

                    // SS: base3 xor, i.e. add and ignore carry
                    digits[j] += digit;
                    digits[j] %= 3;
                }
            }

            // SS: convert back to base10
            uint number = 0;
            uint fac = 1;
            for (var i = 0; i < maxDigits; i++)
            {
                number += digits[i] * fac;
                fac *= 3;
            }

            if (nNegative % 3 == 1)
            {
                // SS: convert from unsigned to signed
                number = ~number + 1;
            }

            return (int) number;
        }

        public int SingleNumber2(int[] nums)
        {
            var freqMap = new Dictionary<int, int>();

            for (var i = 0; i < nums.Length; i++)
            {
                var v = nums[i];
                if (freqMap.TryGetValue(v, out var freq))
                {
                    freqMap[v]--;
                    if (freq == 1)
                    {
                        freqMap.Remove(v);
                    }
                }
                else
                {
                    freqMap[v] = 2;
                }
            }

            var values = freqMap.Keys.ToArray();
            return values[0];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {2, 2, 3, 2};

                // Act
                var value = new Solution().SingleNumber(nums);

                // Assert
                Assert.AreEqual(3, value);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {0, 1, 0, 1, 0, 1, 99};

                // Act
                var value = new Solution().SingleNumber(nums);

                // Assert
                Assert.AreEqual(99, value);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {-2, -2, 1, 1, -3, 1, -3, -3, -4, -2};

                // Act
                var value = new Solution().SingleNumber(nums);

                // Assert
                Assert.AreEqual(-4, value);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums =
                {
                    43, 16, 45, 89, 45, -2147483648, 45, 2147483646, -2147483647, -2147483648, 43, 2147483647, -2147483646, -2147483648, 89, -2147483646, 89, -2147483646, -2147483647, 2147483646
                    , -2147483647, 16, 16, 2147483646, 43
                };

                // Act
                var value = new Solution().SingleNumber(nums);

                // Assert
                Assert.AreEqual(2147483647, value);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {2, 2, 2, -1, -1, -1, 8, -7, 0, -7, 0, -7, 0};

                // Act
                var value = new Solution().SingleNumber(nums);

                // Assert
                Assert.AreEqual(8, value);
            }
        }
    }
}