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
            return LengthOfLIS2(nums);
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

            for (var j = nums.Length - 1; j >= 0; j--)
            {
                for (var i = j; i >= 0; i--)
                {
                    var c1 = 0;
                    var c2 = 0;

                    if (nums[i] < nums[j])
                    {
                        // SS: advance to j
                        c1 = 1 + dp[j][j + 1];
                    }
                    else
                    {
                        // SS: skip j
                        c2 = dp[i][j + 1];
                    }

                    // SS: start subsequence from i + 1
                    var c3 = dp[i + 1][i + 1];

                    var max = Math.Max(c1, c2);
                    max = Math.Max(max, c3);
                    dp[i][j] = max;
                }
            }

            return dp[0][0] + 1;
        }

        private int LengthOfLIS1(int[] nums)
        {
            int Solve(int i, int j)
            {
                if (j == nums.Length)
                {
                    return 0;
                }

                var c1 = 0;
                var c2 = 0;

                if (nums[i] < nums[j])
                {
                    // SS: advance to j
                    c1 = 1 + Solve(j, j + 1);
                }
                else
                {
                    // SS: skip j
                    c2 = Solve(i, j + 1);
                }

                // SS: start subsequence from i + 1
                var c3 = Solve(i + 1, i + 1);

                var max = Math.Max(c1, c2);
                max = Math.Max(max, c3);
                return max;
            }

            var m = 1 + Solve(0, 1);
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