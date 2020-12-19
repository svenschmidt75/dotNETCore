#region

using System;
using NUnit.Framework;

#endregion

// Problem: 53. Maximum Subarray (Kadane's algorithm)
// URL: https://leetcode.com/problems/maximum-subarray/

namespace LeetCode
{
    public class Solution
    {
        public int MaxSubArray(int[] nums)
        {
            // SS: Kadane's algorithm, O(1)
            if (nums.Length == 1)
            {
                return nums[0];
            }

            var bestSum = nums[0];
            var sum = bestSum;

            var i = 1;
            while (i < nums.Length)
            {
                var v = nums[i];
                if (v > sum + v)
                {
                    sum = v;
                }
                else
                {
                    sum += v;
                }

                bestSum = Math.Max(bestSum, sum);

                i++;
            }

            return bestSum;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {-2, 1, -3, 4, -1, 2, 1, -5, 4};

                // Act
                var maxSum = new Solution().MaxSubArray(nums);

                // Assert
                Assert.AreEqual(6, maxSum);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {-2, 1, -3, -1, -1, 2, 1, -5, 4};

                // Act
                var maxSum = new Solution().MaxSubArray(nums);

                // Assert
                Assert.AreEqual(4, maxSum);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {5, 7, -3, 2, 9, 6, 16, 22, 21, 29, -14, 10, 12};

                // Act
                var maxSum = new Solution().MaxSubArray(nums);

                // Assert
                Assert.AreEqual(122, maxSum);
            }
        }
    }
}