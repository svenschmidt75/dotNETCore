#region

using System;
using NUnit.Framework;

#endregion

// Problem: 123. Best Time to Buy and Sell Stock III
// URL: https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iii/

namespace LeetCode
{
    public class Solution
    {
        public int MaxProfit(int[] prices)
        {
            return MaxProfitDQ(prices);
        }

        private int MaxProfitDQ(int[] prices)
        {
            int Solve(int state, int idx)
            {
                if (idx == prices.Length || state == 4)
                {
                    return 0;
                }

                var price = prices[idx];

                if (state == 0)
                {
                    // SS: buy1 at price
                    var mp1 = -price + Solve(state + 1, idx + 1);

                    // SS: skip, do not buy at this price
                    var mp2 = Solve(state, idx + 1);

                    return Math.Max(mp1, mp2);
                }

                if (state == 1)
                {
                    // SS: sell1 at price
                    var mp1 = price + Solve(state + 1, idx + 1);

                    // SS: skip, do not sell at this price
                    var mp2 = Solve(state, idx + 1);

                    return Math.Max(mp1, mp2);
                }

                if (state == 2)
                {
                    // SS: buy2 at price
                    var mp1 = -price + Solve(state + 1, idx + 1);

                    // SS: skip, do not buy at this price
                    var mp2 = Solve(state, idx + 1);

                    return Math.Max(mp1, mp2);
                }

                if (state == 3)
                {
                    // SS: sell2 at price
                    var mp1 = price + Solve(state + 1, idx + 1);

                    // SS: skip, do not sell at this price
                    var mp2 = Solve(state, idx + 1);

                    return Math.Max(mp1, mp2);
                }

                throw new InvalidOperationException();
            }

            var maxProfit = Solve(0, 0);
            return maxProfit;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] prices = {3, 3, 5, 0, 0, 3, 1, 4};

                // Act
                var maxProfit = new Solution().MaxProfit(prices);

                // Assert
                Assert.AreEqual(6, maxProfit);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] prices = {1, 2, 3, 4, 5};

                // Act
                var maxProfit = new Solution().MaxProfit(prices);

                // Assert
                Assert.AreEqual(4, maxProfit);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] prices = {7, 6, 4, 3, 1};

                // Act
                var maxProfit = new Solution().MaxProfit(prices);

                // Assert
                Assert.AreEqual(0, maxProfit);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] prices = {1};

                // Act
                var maxProfit = new Solution().MaxProfit(prices);

                // Assert
                Assert.AreEqual(0, maxProfit);
            }
        }
    }
}