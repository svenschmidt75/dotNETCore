#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 219. Contains Duplicate II
// URL: https://leetcode.com/problems/contains-duplicate-ii/

namespace LeetCode
{
    public class Solution
    {
        public bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            // SS: runtime solution: O(N)
            // space complexity: O(N)

            if (k == 0)
            {
                return false;
            }

            var dict = new Dictionary<int, int>();

            for (var i = 0; i < nums.Length; i++)
            {
                if (dict.TryGetValue(nums[i], out var idx2))
                {
                    if (Math.Abs(idx2 - i) <= k)
                    {
                        return true;
                    }
                }

                dict[nums[i]] = i;
            }

            return false;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {1, 2, 3, 1}, 3, true)]
            [TestCase(new[] {1, 0, 1, 1}, 1, true)]
            [TestCase(new[] {1, 2, 3, 1, 2, 3}, 2, false)]
            public void Test1(int[] nums, int k, bool expected)
            {
                // Arrange

                // Act
                var found = new Solution().ContainsNearbyDuplicate(nums, k);

                // Assert
                Assert.AreEqual(expected, found);
            }
        }
    }
}