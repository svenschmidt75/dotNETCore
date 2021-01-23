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
//            return MaxProfitDQ(prices);
            return MaxProfitBottomUp(prices);
        }

        private int MaxProfitDQ(int[] prices)
        {
            // SS: runtime complexity: O(2^N)
            // space complexity: O(N), due to callstack
            
            int Solve(int state, int idx)
            {
                // SS: recursion base case
                if (idx == prices.Length || state == 4)
                {
                    return 0;
                }

                var price = prices[idx];

                if (state % 2 == 0)
                {
                    // SS: buy at price
                    var mp1 = -price + Solve(state + 1, idx + 1);

                    // SS: skip, do not buy at this price
                    var mp2 = Solve(state, idx + 1);

                    return Math.Max(mp1, mp2);
                }
                else
                {
                    // SS: sell at price
                    var mp1 = price + Solve(state + 1, idx + 1);

                    // SS: skip, do not sell at this price
                    var mp2 = Solve(state, idx + 1);

                    return Math.Max(mp1, mp2);
                }
            }

            var maxProfit = Solve(0, 0);
            return maxProfit;
        }

        private int MaxProfitBottomUp(int[] prices)
        {
            // SS: runtime complexity: O(#t * N), #t: number of transactions,
            // i.e. t = 4 (buy1, sell1, buy2, sell2)
            // space complexity: O(N)
            
            int[] dpEvenState = new int[prices.Length + 1];
            int[] dpOddState = new int[prices.Length + 1];

            int[] currentRow = dpOddState;
            int[] prevRow = dpEvenState;

            int sign = 1;
            
            // SS: t for transactions
            for (int t = 3; t >= 0; t--)
            {
                for (int idx = prices.Length - 1; idx >= 0; idx--)
                {
                    // SS: transitions
                    var price = prices[idx];

                    // SS: do transaction (buy or sell)
                    var mp1 = sign * price + prevRow[idx + 1];

                    // SS: skip, do not buy/sell at this price
                    var mp2 = currentRow[idx + 1];

                    int maxProfit = Math.Max(mp1, mp2);

                    currentRow[idx] = maxProfit;
                }

                // SS: swap rows (i.e. even state to odd state and vice versa)
                int[] tmp = currentRow;
                currentRow = prevRow;
                prevRow = tmp;

                sign *= -1;
            }

            return dpEvenState[0];
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

            [Test]
            public void Test5()
            {
                // Arrange
                int[] prices = new int[0];

                // Act
                var maxProfit = new Solution().MaxProfit(prices);

                // Assert
                Assert.AreEqual(0, maxProfit);
            }
            
        }
    }
}