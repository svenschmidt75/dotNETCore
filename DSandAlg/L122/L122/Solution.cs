#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 122. Best Time to Buy and Sell Stock II
// URL: https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/

namespace LeetCode
{
    public class Solution
    {
        public int MaxProfit(int[] prices)
        {
//            return MaxProfitDQ(prices);
//            return MaxProfitBottomUp(prices);
//            return MaxProfitStack(prices);
//            return MaxProfitSimple(prices);
            return MaxProfitTrivial(prices);
        }

        private int MaxProfitTrivial(int[] prices)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(1)

            var maxProfit = 0;

            for (var i = 1; i < prices.Length; i++)
            {
                if (prices[i - 1] < prices[i])
                {
                    maxProfit += prices[i] - prices[i - 1];
                }
            }

            return maxProfit;
        }

        private int MaxProfitSimple(int[] prices)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(1)

            var maxProfit = 0;

            var i = 0;

            while (i < prices.Length)
            {
                // SS: find low price
                var lowPrice = prices[i];
                var j = i + 1;
                while (j < prices.Length && prices[j] < lowPrice)
                {
                    lowPrice = prices[j];
                    j++;
                }

                if (j == prices.Length)
                {
                    break;
                }

                // SS: find high price
                var highPrice = prices[j];
                var k = j + 1;
                while (k < prices.Length && prices[k] > highPrice)
                {
                    highPrice = prices[k];
                    k++;
                }

                var profit = highPrice - lowPrice;
                maxProfit += profit;

                i = k;
            }

            return maxProfit;
        }

        private int MaxProfitStack(int[] prices)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(N)

            var stack = new Stack<int>();

            var maxProfit = 0;

            var i = 0;
            while (i < prices.Length)
            {
                var price = prices[i];
                if (stack.Any() == false || price <= stack.Peek())
                {
                    stack.Push(price);
                    i++;
                }
                else
                {
                    var b = price;
                    var top = stack.Pop();

                    var localMaxProfit = 0;

                    var j = i;

                    // SS: while the current price is higher, keep
                    // updating the local profit
                    while (j < prices.Length)
                    {
                        var p = prices[j];
                        if (p < b)
                        {
                            break;
                        }

                        var profit = p - top;
                        localMaxProfit = Math.Max(localMaxProfit, profit);
                        j++;

                        // SS: keep updating, when the next price is higher than the
                        // current one 
                        b = p;
                    }

                    i = j;
                    maxProfit += localMaxProfit;

                    // SS: overlap between transactions is not allowed
                    stack.Clear();
                }
            }

            return maxProfit;
        }

        public int MaxProfitBottomUp(int[] prices)
        {
            // SS: runtime complexity: O(N^2)
            // space complexity: O(N)

            var dp1 = new int[prices.Length + 1];
            var dp2 = new int[prices.Length + 1];

            var sign = 1;

            var n = prices.Length / 2;
            var nTransactions = 2 * n;

            for (var i = 0; i < nTransactions; i++)
            {
                for (var idx = prices.Length - 1; idx >= 0; idx--)
                {
                    var price = prices[idx];

                    // SS: buy or sell at this price
                    var c1 = sign * price + dp1[idx + 1];

                    // SS: do not buy or sell at this price
                    var c2 = dp2[idx + 1];

                    var c = Math.Max(c1, c2);
                    dp2[idx] = c;
                }

                var tmp = dp2;
                dp2 = dp1;
                dp1 = tmp;

                sign *= -1;
            }

            return dp1[0];
        }

        public int MaxProfitDQ(int[] prices)
        {
            int Solve(int idx, int type)
            {
                // SS: base case
                if (idx == prices.Length)
                {
                    return 0;
                }

                int c1;
                int c2;

                if (type == 0)
                {
                    // SS: buy at price[idx]
                    c1 = -prices[idx] + Solve(idx + 1, 1);

                    // SS: no not buy
                    c2 = Solve(idx + 1, 0);
                }
                else
                {
                    // SS: sell at price[idx]
                    c1 = prices[idx] + Solve(idx + 1, 0);

                    // SS: no not sell
                    c2 = Solve(idx + 1, 1);
                }

                var max = Math.Max(c1, c2);
                return max;
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
                int[] prices = {7, 1, 5, 3, 6, 4};

                // Act
                var maxProfit = new Solution().MaxProfit(prices);

                // Assert
                Assert.AreEqual(7, maxProfit);
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
                int[] prices = {3, 2, 6, 5, 0, 3};

                // Act
                var maxProfit = new Solution().MaxProfit(prices);

                // Assert
                Assert.AreEqual(7, maxProfit);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] prices = {3, 3, 5, 0, 0, 3, 1, 4};

                // Act
                var maxProfit = new Solution().MaxProfit(prices);

                // Assert
                Assert.AreEqual(8, maxProfit);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] prices = {1, 2, 4, 2, 5, 7, 2, 4, 9, 0};

                // Act
                var maxProfit = new Solution().MaxProfit(prices);

                // Assert
                Assert.AreEqual(15, maxProfit);
            }
        }
    }
}