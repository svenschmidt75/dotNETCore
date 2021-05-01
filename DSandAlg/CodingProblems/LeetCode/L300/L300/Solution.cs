#region

using System;
using NUnit.Framework;

#endregion

// Problem: 300. Longest Increasing Subsequence
// URL: https://leetcode.com/problems/longest-increasing-subsequence/

namespace LeetCode
{
    public class Solution
    {
        public int LengthOfLIS(int[] nums)
        {
//            return LengthOfLIS1(nums);
            // return LengthOfLIS2(nums);
//            return LengthOfLIS3(nums);
            return LengthOfLIS4(nums);
        }

        private int LengthOfLIS4(int[] nums)
        {
            // SS: use sorted set (RB tree)
            // runtime complexity: O(n log n)
            // space complexity: O(n)

            var set = new RedBlackTree();

            for (var i = 0; i < nums.Length; i++)
            {
                var item = nums[i];
                (var found, var lb) = set.LowerBound(item);
                if (found)
                {
                    set.Remove(lb);
                }

                set.Insert(item);
            }

            var length = set.Length;
            return length;
        }

        private int LengthOfLIS3(int[] nums)
        {
            // SS: Bottom-Up Dynamic Programming
            // runtime complexity: O(n^2)
            // space complexity: O(n)

            var dp = new int[nums.Length];

            // SS: initial condition: longest subsequence = 1
            // (element itself)
            dp[^1] = 1;

            var globalMax = 1;

            for (var i = nums.Length - 2; i >= 0; i--)
            {
                // SS: min subsequence length is the element itself, which is 1
                var max = 1;

                for (var j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] < nums[j])
                    {
                        // SS: advance to j
                        var c1 = 1 + dp[j];
                        max = Math.Max(max, c1);
                    }
                }

                globalMax = Math.Max(globalMax, max);
                dp[i] = max;
            }

            return globalMax;
        }

        private int LengthOfLIS2(int[] nums)
        {
            // SS: Bottom-Up Dynamic Programming
            // runtime complexity: O(n^2)
            // space complexity: O(n^2)

            var dp = new int[nums.Length + 1][];
            for (var i = 0; i <= nums.Length; i++)
            {
                dp[i] = new int[nums.Length + 1];
            }

            var globalMax = 0;

            for (var j = nums.Length - 1; j >= 0; j--)
            {
                for (var i = j; i >= 0; i--)
                {
                    var c1 = 0;
                    if (nums[i] < nums[j])
                    {
                        // SS: advance to j
                        c1 = 1 + dp[j][j + 1];
                    }

                    // SS: skip j
                    var c2 = dp[i][j + 1];

                    var max = Math.Max(c1, c2);
                    dp[i][j] = max;

                    globalMax = Math.Max(globalMax, max);
                }
            }

            return globalMax + 1;
        }

        private int LengthOfLIS1(int[] nums)
        {
            // SS: Divide & Conquer
            // runtime complexity: O(2^n)

            int Solve(int i, int j)
            {
                if (j == nums.Length)
                {
                    return 0;
                }

                var c1 = 0;
                if (nums[i] < nums[j])
                {
                    // SS: advance to j
                    c1 = 1 + Solve(j, j + 1);
                }

                // SS: skip j
                var c2 = Solve(i, j + 1);

                var max = Math.Max(c1, c2);
                return max;
            }

            var m = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                var a = 1 + Solve(i, i + 1);
                m = Math.Max(m, a);
            }

            return m;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {10, 9, 2, 5, 3, 7, 101, 18}, 4)]
            [TestCase(new[] {1}, 1)]
            [TestCase(new[] {0, 1, 0, 3, 2, 3}, 4)]
            [TestCase(new[] {7, 7, 7, 7, 7, 7, 7}, 1)]
            [TestCase(new[] {101, 3, 2, 1}, 1)]
            [TestCase(new[] {4, 10, 4, 3, 8, 9}, 3)]
            public void Test(int[] nums, int expected)
            {
                // Arrange

                // Act
                var length = new Solution().LengthOfLIS(nums);

                // Assert
                Assert.AreEqual(expected, length);
            }
        }
    }
}