using System;
using NUnit.Framework;

// Problem: 322. Coin Change
// URL: https://leetcode.com/problems/coin-change/

namespace LeetCode322
{
    public class Solution
    {
        private static int _cnt = 0;
        
        public int CoinChange(int[] coins, int amount)
        {
            if (amount == 0)
            {
                return 0;
            }

            int[][] dp = new int[amount + 1][];
            for (int i = 0; i <= amount; i++)
            {
                dp[i] = new int[coins.Length];
            }

            CoinChangeTopDown(coins, amount, coins.Length - 1, dp);

            Console.WriteLine($"{_cnt} evaluations");

            return dp[amount][^1] - 2;
        }

        private int CoinChangeTopDown(int[] coins, int remaining, int pos, int[][] dp)
        {
            if (pos == -1)
            {
                return -1 + 2;
            }

            if (remaining == 0)
            {
                return 0 + 2;
            }

            if (remaining < 0)
            {
                return -1 + 2;
            }

            if (dp[remaining][pos] > 0)
            {
                return dp[remaining][pos];
            }
            
            // SS: use current coin and do not advance to next coin
            int min = CoinChangeTopDown(coins, remaining - coins[pos], pos, dp) - 2;
            if (min >= 0)
            {
                min++;
            }
            
            int i2 = CoinChangeTopDown(coins, remaining, pos - 1, dp) - 2;
            if (min == -1 || i2 >= 0 && i2 < min)
            {
                min = i2;
            }

            dp[remaining][pos] = min + 2;

            _cnt++;
            
            return min + 2;
        }
        
        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] coins = {1, 2, 5};
                int target = 11;

                // Act
                int nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(3, nCoins);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] coins = {1, 2};
                int target = 2;

                // Act
                int nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(1, nCoins);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] coins = {2};
                int target = 3;

                // Act
                int nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(-1, nCoins);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] coins = {1};
                int target = 0;

                // Act
                int nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(0, nCoins);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] coins = {1};
                int target = 1;

                // Act
                int nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(1, nCoins);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] coins = {1};
                int target = 2;

                // Act
                int nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(2, nCoins);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[] coins = {1, 5, 8, 9};
                int target = 29;

                // Act
                int nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(4, nCoins);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                int[] coins = {52,480,116,409,170,240,496};
                int target = 8230;

                // Act
                int nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(18, nCoins);
            }

        }

    }

}
