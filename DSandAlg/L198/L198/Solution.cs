#region

using System;
using NUnit.Framework;

#endregion

// Problem: 198. House Robber
// URL: https://leetcode.com/problems/house-robber/

namespace LeetCode
{
    public class Solution
    {
        public int Rob(int[] nums)
        {
            // SS: DP, bottom-up
            // runtime performance: O(n)
            // space complexity: O(1)

            if (nums.Length == 0)
            {
                return 0;
            }

            var p2 = 0;
            var p1 = 0;
            var p0 = 0;

            for (var i = nums.Length - 1; i >= 0; i--)
            {
                p2 = p1;
                p1 = p0;
                p0 = Math.Max(nums[i] + p2, p1);
            }

            return p0;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act

                // Assert
            }
        }
    }
}