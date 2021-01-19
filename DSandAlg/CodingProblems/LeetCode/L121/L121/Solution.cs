using System;
using NUnit.Framework;

// Problem: 121. Best Time to Buy and Sell Stock
// URL: https://leetcode.com/problems/best-time-to-buy-and-sell-stock/

namespace LeetCode
{
    public class Solution
    {
        public int MaxProfit(int[] prices)
        {
//            return MaxProfitSell(prices);
            return MaxProfitBuy(prices);
        }

        private int MaxProfitBuy(int[] prices)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(1)
            
            // SS: if we can only buy, we don't do anything
            if (prices.Length <= 0)
            {
                return 0;
            }

            int buyPrice = prices[0];
            int maxProfit = 0;

            for (int i = 1; i < prices.Length; i++)
            {
                int sellPrice = prices[i];
                maxProfit = Math.Max(maxProfit, -buyPrice + sellPrice);
                buyPrice = Math.Min(buyPrice, sellPrice);
            }

            return maxProfit;
        }

        private int MaxProfitSell(int[] prices)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(1)
            
            // SS: if we can only buy, we don't do anything
            if (prices.Length <= 0)
            {
                return 0;
            }

            int sellPrice = prices[^1];
            int maxProfit = 0;

            for (int i = prices.Length - 2; i >= 0; i--)
            {
                int buyPrice = prices[i];
                maxProfit = Math.Max(maxProfit, -buyPrice + sellPrice);
                sellPrice = Math.Max(sellPrice, buyPrice);
            }

            return maxProfit;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] prices = {7, 1, 5, 3, 6, 4};

                // Act
                int maxProfit = new Solution().MaxProfit(prices);

                // Assert
                Assert.AreEqual(5, maxProfit);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] prices = {7,6,4,3,1};

                // Act
                int maxProfit = new Solution().MaxProfit(prices);

                // Assert
                Assert.AreEqual(0, maxProfit);
            }

        }
    }
}