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
            var maxProfit = MaxProfitDivideAndConquer(2 * k, prices, 0, 0);
            return maxProfit;
        }

        private int MaxProfitDivideAndConquer(int k, int[] prices, int profit, int position)
        {
            if (k == 0 || position == prices.Length)
            {
                return profit;
            }

            int m1;

            if (k % 2 == 0)
            {
                // we have to buy
                var cost = prices[position];
                m1 = MaxProfitDivideAndConquer(k - 1, prices, profit - cost, position + 1);
            }
            else
            {
                // we have to sell
                var cost = prices[position];
                m1 = MaxProfitDivideAndConquer(k - 1, prices, profit + cost, position + 1);
            }

            // skip this day
            var m2 = MaxProfitDivideAndConquer(k, prices, profit, position + 1);

            var m = Math.Max(m1, m2);
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