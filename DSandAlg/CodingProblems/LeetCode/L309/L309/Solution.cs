#region

using System;
using NUnit.Framework;

#endregion

// Problem: 309. Best Time to Buy and Sell Stock with Cooldown
// URL: https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-cooldown/

namespace LeetCode
{
    public class Solution
    {
        public int MaxProfit(int[] prices)
        {
            // return MaxProfit1(prices);
            return MaxProfit2(prices);
        }

        private int MaxProfit2(int[] prices)
        {
            // SS: Bottom-Up Dynamic Programming
            // runtime complexity: O(n)
            // space complexity: O(n)

            var dp = new int[prices.Length + 2][];
            for (var i = 0; i < prices.Length + 2; i++)
            {
                dp[i] = new int[2];
            }

            for (var i = prices.Length - 1; i >= 0; i--)
            {
                var price = prices[i];

                // SS: buy
                var buyProfit = -price + dp[i + 1][1];
                var buyIgnoreProfit = dp[i + 1][0];
                buyProfit = Math.Max(buyProfit, buyIgnoreProfit);
                dp[i][0] = buyProfit;

                // SS: sell
                var sellProfit = price + dp[i + 2][0];
                var sellIgnoreProfit = dp[i + 1][1];
                sellProfit = Math.Max(sellProfit, sellIgnoreProfit);
                dp[i][1] = sellProfit;
            }

            // SS: return the profit when we have to buy (NOT sell!)
            return dp[0][0];
        }

        private int MaxProfit1(int[] prices)
        {
            // SS: Divide & Conquer
            // runtime complexity: O(2^N)
            // space complexity: O(N)

            int DFS(int day, int state)
            {
                // SS: base case
                if (day >= prices.Length)
                {
                    return 0;
                }

                int maxProfit;

                if (state == 0)
                {
                    // SS: buy
                    maxProfit = -prices[day] + DFS(day + 1, 1);
                }
                else
                {
                    // SS: sell, have to skip an extra day
                    maxProfit = prices[day] + DFS(day + 2, 0);
                }

                // SS: ignore this day
                var profit2 = DFS(day + 1, state);

                maxProfit = Math.Max(maxProfit, profit2);
                return maxProfit;
            }

            var maxProfit = DFS(0, 0);
            return maxProfit;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] { 1, 2, 3, 0, 2 }, 3)]
            [TestCase(new[] { 1 }, 0)]
            [TestCase(new[] { 1, 4, 6, 2, 3, 8, 0, 3, 2 }, 10)]
            public void Test(int[] prices, int expectedProfit)
            {
                // Arrange

                // Act
                var profit = new Solution().MaxProfit(prices);

                // Assert
                Assert.AreEqual(expectedProfit, profit);
            }
        }
    }
}