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
            return MaxSubArrayDQ(nums);
        }

        public int MaxSubArray1(int[] nums)
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
                // if (v > sum + v)
                // {
                //     sum = v;
                // }
                // else
                // {
                //     sum += v;
                // }
                sum = Math.Max(sum, 0) + v;

                bestSum = Math.Max(bestSum, sum);

                i++;
            }

            return bestSum;
        }

        public int MaxSubArray2(int[] nums)
        {
            // SS: Kadane's algorithm, O(1)
            // in-place, dynamic programming
            // recurrence relation: dp[i] = Math.Max(dp[i - 1], 0) + dp[i]
            if (nums.Length == 1)
            {
                return nums[0];
            }

            var maxSum = nums[0];
            for (var j = 1; j < nums.Length; j++)
            {
                nums[j] = Math.Max(nums[j - 1], 0) + nums[j];
                maxSum = Math.Max(maxSum, nums[j]);
            }

            return maxSum;
        }

        public int MaxSubArrayDQ(int[] nums)
        {
            // SS: Divide & Conquer
            // dp[i] = Math.Max(dp[i - 1], 0) + dp[i]

            var bestSum = nums[0];

            if (nums.Length == 1)
            {
                return bestSum;
            }

            int Solve(int idx)
            {
                if (idx == 0)
                {
                    return nums[0];
                }

                var b = Solve(idx - 1);
                var best = Math.Max(b, 0) + nums[idx];
                bestSum = Math.Max(bestSum, best);
                return best;
            }

            Solve(nums.Length - 1);
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

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {-1, -2};

                // Act
                var maxSum = new Solution().MaxSubArray(nums);

                // Assert
                Assert.AreEqual(-1, maxSum);
            }
        }
    }
}