#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 136. Single Number
// URL: https://leetcode.com/problems/single-number/

namespace LeetCode
{
    public class Solution
    {
        public int SingleNumber(int[] nums)
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
                    freqMap[v] = 1;
                }
            }

            var values = freqMap.Keys.ToArray();
            return values[0];
        }

        public int SingleNumberXorTrick(int[] nums)
        {
            var count = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                count ^= nums[i];
            }

            return count;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {2, 2, 1};

                // Act
                var n = new Solution().SingleNumber(nums);

                // Assert
                Assert.AreEqual(1, n);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {4, 1, 2, 1, 2};

                // Act
                var n = new Solution().SingleNumber(nums);

                // Assert
                Assert.AreEqual(4, n);
            }
        }
    }
}