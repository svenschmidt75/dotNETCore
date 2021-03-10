#region

using System;
using NUnit.Framework;

#endregion

// Problem: 213. House Robber II
// URL: https://leetcode.com/problems/house-robber-ii/

namespace LeetCode
{
    public class Solution
    {
        public int Rob(int[] nums)
        {
            // return Rob1(nums);
            return Rob2(nums);
        }

        private int Rob2(int[] nums)
        {
            // SS: DP, bottom-up
            // time complexity: O(n)
            // space complexity: O(n)
            // can improve to O(1)...

            var dp = new int[nums.Length + 1];

            // SS: Case 1: steal from last house
            dp[^1] = 0;

            // SS: we steal from the last house
            dp[^2] = nums[^1];

            // SS: in case there is only 1 house
            var mp1 = nums[0];

            if (nums.Length > 1)
            {
                // SS: we cannot steal from the 2nd last house since we stole from the last
                dp[^3] = nums[^1];

                // SS: we cannot steal from the 1st one, since it is adjacent to the last
                for (var i = nums.Length - 3; i >= 1; i--)
                {
                    // SS: steal from the current house
                    var maxProfit1 = nums[i] + dp[i + 2];

                    // SS: do not steal from the current house
                    var maxProfit2 = dp[i + 1];

                    var maxProfit = Math.Max(maxProfit1, maxProfit2);
                    dp[i] = maxProfit;
                }

                // SS: max profit when stealing from the last house
                mp1 = dp[1];
            }

            // SS: Case 2: do not steal from last house
            dp[^1] = 0;
            dp[^2] = 0;

            for (var i = nums.Length - 2; i >= 0; i--)
            {
                // SS: steal from the current house
                var maxProfit1 = nums[i] + dp[i + 2];

                // SS: do not steal from the current house
                var maxProfit2 = dp[i + 1];

                var maxProfit = Math.Max(maxProfit1, maxProfit2);
                dp[i] = maxProfit;
            }

            // SS: max profit when stealing from the last house
            var mp2 = dp[0];

            var mp = Math.Max(mp1, mp2);
            return mp;
        }

        private int Rob1(int[] nums)
        {
            // SS: Divide & Conquer

            int SolveStart0(int pos)
            {
                // SS: base case
                if (pos >= nums.Length)
                {
                    return 0;
                }

                // SS: the last house is off-limits since we stole from the first house
                if (pos == nums.Length - 1)
                {
                    return 0;
                }

                // SS: steal from this house
                var s1 = nums[pos] + SolveStart0(pos + 2);

                // SS: do not steal from the current house
                var s2 = SolveStart0(pos + 1);

                var s = Math.Max(s1, s2);
                return s;
            }

            int Solve(int pos)
            {
                // SS: base case
                if (pos >= nums.Length)
                {
                    return 0;
                }

                // SS: steal from this house
                var s1 = nums[pos] + Solve(pos + 2);

                // SS: do not steal from the current house
                var s2 = Solve(pos + 1);

                var s = Math.Max(s1, s2);
                return s;
            }

            var maxProfit1 = nums[0] + SolveStart0(2);
            var maxProfit2 = Solve(1);
            var maxProfit = Math.Max(maxProfit1, maxProfit2);
            return maxProfit;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {2, 3, 2};

                // Act
                var maxProfit = new Solution().Rob(nums);

                // Assert
                Assert.AreEqual(3, maxProfit);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {1, 2, 3, 1};

                // Act
                var maxProfit = new Solution().Rob(nums);

                // Assert
                Assert.AreEqual(4, maxProfit);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {2, 3, 5};

                // Act
                var maxProfit = new Solution().Rob(nums);

                // Assert
                Assert.AreEqual(5, maxProfit);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {9, 2, 4, 14, 3, 8, 9, 4, 3, 5, 7, 6};

                // Act
                var maxProfit = new Solution().Rob(nums);

                // Assert
                Assert.AreEqual(42, maxProfit);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {9};

                // Act
                var maxProfit = new Solution().Rob(nums);

                // Assert
                Assert.AreEqual(9, maxProfit);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] nums = {9, 11};

                // Act
                var maxProfit = new Solution().Rob(nums);

                // Assert
                Assert.AreEqual(11, maxProfit);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[] nums = {0};

                // Act
                var maxProfit = new Solution().Rob(nums);

                // Assert
                Assert.AreEqual(0, maxProfit);
            }
        }
    }
}