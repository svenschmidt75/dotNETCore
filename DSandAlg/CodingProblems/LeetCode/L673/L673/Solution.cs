#region

using NUnit.Framework;

#endregion

// Problem: 673. Number of Longest Increasing Subsequence
// URL: https://leetcode.com/problems/number-of-longest-increasing-subsequence/

namespace LeetCode
{
    public class Solution
    {
        public int FindNumberOfLIS(int[] nums)
        {
            return FindNumberOfLIS1(nums);
        }

        private int FindNumberOfLIS1(int[] nums)
        {
            // SS: dp[i][0]: length
            // dp[i][1]: count
            // Bottom-Up dynamic programming
            // runtime complexity: O(N^2)
            // space complexity: O(N)
            var dp = new int[nums.Length][];
            for (var i = 0; i < nums.Length; i++)
            {
                dp[i] = new int[2];
            }

            // SS: boundary condition
            dp[^1][0] = 1;
            dp[^1][1] = 1;

            var globalLength = 1;
            var globalCount = 1;

            for (var i = nums.Length - 2; i >= 0; i--)
            {
                var localLength = 1;
                var localCount = 1;

                for (var j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] < nums[j])
                    {
                        var length = 1 + dp[j][0];
                        var count = dp[j][1];

                        if (length > localLength)
                        {
                            localLength = length;
                            localCount = count;
                        }
                        else if (length == localLength)
                        {
                            localCount += count;
                        }
                    }
                }

                dp[i][0] = localLength;
                dp[i][1] = localCount;

                if (localLength > globalLength)
                {
                    globalLength = localLength;
                    globalCount = localCount;
                }
                else if (localLength == globalLength)
                {
                    globalCount += localCount;
                }
            }

            return globalCount;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {1, 2}, 1)]
            [TestCase(new[] {1, 2, 2}, 2)]
            [TestCase(new[] {1, 3, 5, 4, 7}, 2)]
            [TestCase(new[] {2, 2, 2, 2, 2}, 5)]
            [TestCase(new[] {12, 23, 34, 54, 67, 7, 78, 89, 34, 4, 56, 67}, 1)]
            [TestCase(new[] {12, 23, 34, 54, 67, 7}, 1)]
            [TestCase(new[] {8, 13, 9, 14, 10}, 3)]
            [TestCase(new[] {12, 8, 13, 9, 14, 10}, 4)]
            [TestCase(new[] {5, 12, 8, 13, 9, 14, 10}, 4)]
            [TestCase(new[] {5, 12, 8, 1, 33, 23, 15, 3, 98, 13, 9, 14, 10}, 13)]
            public void Test(int[] nums, int expected)
            {
                // Arrange

                // Act
                var result = new Solution().FindNumberOfLIS(nums);

                // Assert
                Assert.AreEqual(expected, result);
            }
        }
    }
}