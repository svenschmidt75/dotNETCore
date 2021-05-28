#region

using NUnit.Framework;

#endregion

// Problem: 377. Combination Sum IV
// URL: https://leetcode.com/problems/combination-sum-iv/

namespace LeetCode
{
    public class Solution
    {
        public int CombinationSum4(int[] nums, int target)
        {
            // return CombinationSum41(nums, target);
            return CombinationSum42(nums, target);
        }

        private int CombinationSum42(int[] nums, int target)
        {
            // SS: Bottom-Up Dynamic Programming
            // runtime complexity: O(target * n)
            // space complexity: O(target)

            var dp = new int[target + 1];
            dp[0] = 1;

            for (var i = 1; i <= target; i++)
            {
                var count = 0;

                for (var j = 0; j < nums.Length; j++)
                {
                    var s = nums[j];
                    if (s > i)
                    {
                        continue;
                    }

                    if (i - s >= 0)
                    {
                        count += dp[i - s];
                    }
                }

                dp[i] = count;
            }

            return dp[target];
        }

        private int CombinationSum41(int[] nums, int target)
        {
            // SS: runtime complexity: O(n * (n - 1) * (n - 2) * ... * 1) = O(n!)
            var count = 0;

            void Solve(int t)
            {
                // SS: base case
                if (t > target)
                {
                    return;
                }

                if (t == target)
                {
                    count++;
                    return;
                }

                for (var i = 0; i < nums.Length; i++)
                {
                    var n = nums[i];
                    Solve(n + t);
                }
            }

            Solve(0);
            return count;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {1, 2, 3}, 4, 7)]
            [TestCase(new[] {1, 2, 3, 4}, 19, 147312)]
            [TestCase(new[] {9}, 3, 0)]
            public void Test1(int[] nums, int target, int expected)
            {
                // Arrange

                // Act
                var result = new Solution().CombinationSum4(nums, target);

                // Assert
                Assert.AreEqual(expected, result);
            }
        }
    }
}