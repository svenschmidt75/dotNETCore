#region

using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 518. Coin Change 2
// URL: https://leetcode.com/problems/coin-change-2/

namespace LeetCode518
{
    public class Solution
    {
        public int Change(int amount, int[] coins)
        {
            if (amount == 0)
            {
                return 1;
            }

            if (coins.Length == 0)
            {
                return 0;
            }

//            return ChangeTopDown(amount, coins);
//            return ChangeBottomUp(amount, coins);
            return ChangeBottomUp2(amount, coins);
        }

        private int ChangeTopDown(int amount, int[] coins)
        {
            var dp = new int[amount + 1][];
            for (var i = 0; i <= amount; i++)
            {
                dp[i] = new int[coins.Length];
            }

            int Change2(int remaining, int[] coins, int idx)
            {
                if (remaining == 0)
                {
                    return 2;
                }

                if (idx == coins.Length || remaining < 0)
                {
                    return 1;
                }

                if (dp[remaining][idx] > 0)
                {
                    return dp[remaining][idx];
                }

                var l1 = Change2(remaining - coins[idx], coins, idx) - 1;
                var l2 = Change2(remaining, coins, idx + 1) - 1;

                var n = l1 + l2 + 1;

                dp[remaining][idx] = n;

                return n;
            }

            var n = Change2(amount, coins, 0) - 1;
            return n;
        }

        private int ChangeBottomUp(int amount, int[] coins)
        {
            var sortedCoins = coins.OrderBy(x => x).ToArray();

            var dp = new int[amount + 1][];
            for (var i = 0; i <= amount; i++)
            {
                dp[i] = new int[sortedCoins.Length];
            }

            // SS: initialize 0 amount
            for (var i = 0; i < sortedCoins.Length; i++)
            {
                dp[0][i] = 1;
            }

            for (var i = 1; i <= amount; i++)
            {
                for (var j = sortedCoins.Length - 1; j >= 0; j--)
                {
                    if (i - sortedCoins[j] < 0)
                    {
                        continue;
                    }

                    var cnt = dp[i - sortedCoins[j]][j];

                    if (j <= sortedCoins.Length - 2)
                    {
                        cnt += dp[i][j + 1];
                    }

                    dp[i][j] = cnt;
                }
            }

            return dp[amount][0];
        }

        private int ChangeBottomUp2(int amount, int[] coins)
        {
            int[] dp = new int[amount + 1];
        
            // SS: initialize 0 amount
            dp[0] = 1;

            // SS outer loop is over each coin
            for (var i = 0; i < coins.Length; i++)
            {
                int coin = coins[i];
                
                for (var j = coin; j <= amount; j++)
                {
                    dp[j] += dp[j - coin];
                }
            }

            return dp[amount];
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] coins = {1, 2, 5};
                var amount = 5;

                // Act
                var n = new Solution().Change(amount, coins);

                // Assert
                Assert.AreEqual(4, n);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] coins = {2};
                var amount = 3;

                // Act
                var n = new Solution().Change(amount, coins);

                // Assert
                Assert.AreEqual(0, n);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] coins = {10};
                var amount = 10;

                // Act
                var n = new Solution().Change(amount, coins);

                // Assert
                Assert.AreEqual(1, n);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] coins = {52, 480, 116, 409, 170, 240, 496};
                var amount = 8230;

                // Act
                var n = new Solution().Change(amount, coins);

                // Assert
                Assert.AreEqual(39197, n);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] coins = { };
                var amount = 0;

                // Act
                var n = new Solution().Change(amount, coins);

                // Assert
                Assert.AreEqual(1, n);
            }
        }
    }
}