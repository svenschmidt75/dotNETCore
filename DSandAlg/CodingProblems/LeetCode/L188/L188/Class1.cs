#region

using System;
using NUnit.Framework;

#endregion

// LeetCode 188. Best Time to Buy and Sell Stock IV
// https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iv/

namespace L188
{
    public class Solution
    {
        public int MaxProfit(int k, int[] prices)
        {
            var maxProfit = MaxProfitDivideAndConquer(k, prices, 0, 0, true);
            return maxProfit;
        }

        private int MaxProfitDivideAndConquer(int k, int[] prices, int profit, int position, bool buy)
        {
            if (k == 0 || position == prices.Length)
            {
                return profit;
            }

            int m;

            // skip this day
            var m1 = MaxProfitDivideAndConquer(k, prices, profit, position + 1, buy);
            int m2;

            if (buy)
            {
                // we have to buy
                var cost = prices[position];
                profit -= cost;
                m2 = MaxProfitDivideAndConquer(k, prices, profit, position + 1, false);
            }
            else
            {
                // we have to sell
                var cost = prices[position];
                profit += cost;
                m2 = MaxProfitDivideAndConquer(k - 1, prices, profit, position + 1, true);
            }

            m = Math.Max(m1, m2);
            return m;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var prices = new[] {2, 4, 1};

            // Act
            var maxProfit = new Solution().MaxProfit(2, prices);

            // Assert
            Assert.AreEqual(2, maxProfit);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var prices = new[] {3, 2, 6, 5, 0, 3};

            // Act
            var maxProfit = new Solution().MaxProfit(2, prices);

            // Assert
            Assert.AreEqual(7, maxProfit);
        }
    }
}