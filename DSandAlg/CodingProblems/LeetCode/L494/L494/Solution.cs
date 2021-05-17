#region

using NUnit.Framework;

#endregion

// Problem: 494. Target Sum 
// URL: https://leetcode.com/problems/target-sum/

namespace LeetCode
{
    public class Solution
    {
        public int FindTargetSumWays(int[] nums, int target)
        {
//            return FindTargetSumWays1(nums, target);
            return FindTargetSumWays2(nums, target);
        }

        private int FindTargetSumWays2(int[] nums, int target)
        {
            // SS: Bottom-Up DP
            // Note: The number of combinations grows exponentially with n,
            // so you might think the memory requirements grows with O(2^n).
            // But, the sum is bounded, so we never have more than
            // [-1000, 1000] possibilities, so the bound is actually O(1)!!!
            // Runtime complexity: O(n)

            var dp = new int[nums.Length + 1][];
            for (var i = 0; i <= nums.Length; i++)
            {
                dp[i] = new int[2 * 1000 + 1];
            }

            // SS: set zero => 1
            dp[^1][1000] = 1;

            for (var i = nums.Length - 1; i >= 0; i--)
            {
                // SS: O(1)!!!
                for (var j = 0; j < 2 * 1000 + 1; j++)
                {
                    if (dp[i + 1][j] > 0)
                    {
                        dp[i][j + nums[i]] += dp[i + 1][j];
                        dp[i][j - nums[i]] += dp[i + 1][j];
                    }
                }
            }

            var count = dp[0][target + 1000];
            return count;
        }

        private int FindTargetSumWays1(int[] nums, int target)
        {
            var cnt = 0;

            void DFS(int idx, int t)
            {
                // SS: base case
                if (idx == nums.Length)
                {
                    if (t == target)
                    {
                        cnt++;
                    }

                    return;
                }

                // SS: case 1: +
                DFS(idx + 1, t + nums[idx]);

                // SS: case 2: -
                DFS(idx + 1, t - nums[idx]);
            }

            DFS(0, 0);
            return cnt;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {1, 1, 1, 1, 1}, 3, 5)]
            [TestCase(new[] {1}, 1, 1)]
            [TestCase(new[] {2, 2, 4, 5, 7, 9}, 15, 3)]
            [TestCase(new[] {1, 0, 1}, -2, 2)]
            public void Test(int[] nums, int target, int expected)
            {
                // Arrange

                // Act
                var result = new Solution().FindTargetSumWays(nums, target);

                // Assert
                Assert.AreEqual(expected, result);
            }
        }
    }
}