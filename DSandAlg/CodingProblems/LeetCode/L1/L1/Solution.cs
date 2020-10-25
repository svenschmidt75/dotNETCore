#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// 1. Two Sum
// https://leetcode.com/problems/two-sum/

namespace L1
{
    public class Solution
    {
        public int[] TwoSum(int[] nums, int target)
        {
            var hashMap = new Dictionary<int, int>();

            for (var i = 0; i < nums.Length; i++)
            {
                var v = nums[i];
                if (hashMap.TryGetValue(v, out var idx))
                {
                    return new[] {idx, i};
                }

                hashMap[target - v] = i;
            }

            throw new InvalidOperationException();
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {2, 7, 11, 16};
                var target = 9;

                // Act
                var result = new Solution().TwoSum(nums, target);

                // Assert
                Assert.AreEqual(target, nums[result[0]] + nums[result[1]]);
            }
            
            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {3, 2, 4};
                var target = 6;

                // Act
                var result = new Solution().TwoSum(nums, target);

                // Assert
                Assert.AreEqual(target, nums[result[0]] + nums[result[1]]);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {3, 3};
                var target = 6;

                // Act
                var result = new Solution().TwoSum(nums, target);

                // Assert
                Assert.AreEqual(target, nums[result[0]] + nums[result[1]]);
            }
        }
    }
}