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
//            var maxProfit = MaxProfitDivideAndConquer(2 * k, prices, 0);

            var maxProfit = MaxProfitDPTopDown(k, prices);

            return maxProfit;
        }

        private static int MaxProfitDPTopDown(int k, int[] prices)
        {
            var memoizationArray = new int[2 * k + 1][];
            for (int i = 0; i < 2 * k + 1; i++)
            {
                memoizationArray[i] = new int[prices.Length];
                for (int j = 0; j < prices.Length; j++)
                {
                    memoizationArray[i][j] = int.MinValue;
                }
            }

            var maxProfit = MaxProfitDPTopDown(2 * k, prices, 0, memoizationArray);
            return maxProfit;
        }

        private static int MaxProfitDPTopDown(int k, int[] prices, int position, int[][] memoizationArray)
        {
            Console.WriteLine($"k: {k} - position: {position}");

            if (k == 2 && position == 4)
            {
                int a = 1;
                a++;
            }
            
            
            if (k == 0 || position == prices.Length)
            {
                return 0;
            }

            // if (memoizationArray[k][position] != int.MinValue)
            // {
            //     int p = memoizationArray[k][position];
            //     Console.WriteLine($"Memoized profit: {p}");
            //     return p;
            // }
            
            int m1;

            if (k % 2 == 0)
            {
                // we have to buy
//                m1 = MaxProfitDPTopDown(k - 1, prices, profit - cost, position + 1, memoizationArray);
                m1 = MaxProfitDPTopDown(k - 1, prices, position + 1, memoizationArray);
                var cost = prices[position];
                m1 -= cost;
            }
            else
            {
                // we have to sell
                m1 = MaxProfitDPTopDown(k - 1, prices, position + 1, memoizationArray);
                var cost = prices[position];
                m1 += cost;
            }

            // skip this day
            var m2 = MaxProfitDPTopDown(k, prices, position + 1, memoizationArray);

            var m = Math.Max(m1, m2);

            Console.WriteLine($"Calculated profit: {m}");
            memoizationArray[k][position] = m;
            
            return m;
        }

        private static int MaxProfitDivideAndConquer(int k, int[] prices, int position)
        {
            Console.WriteLine($"k: {k} position: {position}");
            
            if (k == 0 || position == prices.Length)
            {
                return 0;
            }

            int m1;

            if (k % 2 == 0)
            {
                // we have to buy
                m1 = MaxProfitDivideAndConquer(k - 1, prices, position + 1);
                var cost = prices[position];
                m1 -= cost;
            }
            else
            {
                // we have to sell
                m1 = MaxProfitDivideAndConquer(k - 1, prices, position + 1);
                var cost = prices[position];
                m1 += cost;
            }

            // skip this day
            var m2 = MaxProfitDivideAndConquer(k, prices, position + 1);

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