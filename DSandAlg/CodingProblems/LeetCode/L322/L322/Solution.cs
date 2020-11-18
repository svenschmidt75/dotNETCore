#region

using System;
using NUnit.Framework;

#endregion

// Problem: 322. Coin Change
// URL: https://leetcode.com/problems/coin-change/

namespace LeetCode322
{
    public class Solution
    {
        public int CoinChange(int[] coins, int amount)
        {
//            return CoinChangeTopDown(coins, amount);
            return CoinChangeBottomUp(coins, amount);
        }

        public int CoinChangeTopDown(int[] coins, int amount)
        {
            if (amount == 0)
            {
                return 0;
            }

            var dp = new int[amount + 1][];
            for (var i = 0; i <= amount; i++)
            {
                dp[i] = new int[coins.Length];
            }

            CoinChangeTopDown(coins, amount, coins.Length - 1, dp);

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
            var min = CoinChangeTopDown(coins, remaining - coins[pos], pos, dp) - 2;
            if (min >= 0)
            {
                min++;
            }

            var i2 = CoinChangeTopDown(coins, remaining, pos - 1, dp) - 2;
            if (min == -1 || i2 >= 0 && i2 < min)
            {
                min = i2;
            }

            dp[remaining][pos] = min + 2;

            return min + 2;
        }

        public int CoinChangeBottomUp(int[] coins, int amount)
        {
            if (amount == 0)
            {
                return 0;
            }

            // SS: contains the min. number of coins for amount 0...amount
            var dp = new int[amount + 1];

            for (var i = 0; i <= amount; i++)
            {
                var min = int.MaxValue;

                for (var j = 0; j < coins.Length; j++)
                {
                    if (i - coins[j] >= 0)
                    {
                        var cnt = dp[i - coins[j]];

                        // SS: if the remainder i - coins[j] does not have a coin combination,
                        // i == coins[j] for it to be valid
                        if (cnt == 0 && i == coins[j] || cnt > 0)
                        {
                            min = Math.Min(min, cnt + 1);
                        }
                    }
                }

                dp[i] = min == int.MaxValue ? 0 : min;
            }

            return dp[amount] == 0 ? -1 : dp[amount];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] coins = {1, 2, 5};
                var target = 11;

                // Act
                var nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(3, nCoins);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] coins = {1, 2};
                var target = 2;

                // Act
                var nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(1, nCoins);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] coins = {2};
                var target = 3;

                // Act
                var nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(-1, nCoins);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] coins = {1};
                var target = 0;

                // Act
                var nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(0, nCoins);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] coins = {1};
                var target = 1;

                // Act
                var nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(1, nCoins);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] coins = {1};
                var target = 2;

                // Act
                var nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(2, nCoins);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[] coins = {1, 5, 8, 9};
                var target = 29;

                // Act
                var nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(4, nCoins);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                int[] coins = {52, 480, 116, 409, 170, 240, 496};
                var target = 8230;

                // Act
                var nCoins = new Solution().CoinChange(coins, target);

                // Assert
                Assert.AreEqual(18, nCoins);
            }
        }
    }
}