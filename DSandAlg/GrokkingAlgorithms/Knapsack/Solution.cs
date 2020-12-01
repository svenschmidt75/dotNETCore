#region

using System;
using NUnit.Framework;

#endregion

// Problem:
// URL:

namespace Knapsack
{
    public class Solution
    {
        public int Knapsack(int[] value, int[] weight, int knapsackCapacity)
        {
            return KnapsackBottomUp2(value, weight, knapsackCapacity);
        }

        public int Knapsack1(int[] value, int[] weight, int knapsackCapacity)
        {
            int Solve(int idx, int remainingCapacity)
            {
                // SS: boundary conditions
                if (idx == value.Length || remainingCapacity == 0)
                {
                    return 0;
                }

                // SS: steal the item if there is enough capacity in the knapsack
                var c1 = 0;
                var d = remainingCapacity - weight[idx];
                if (d >= 0)
                {
                    c1 = value[idx] + Solve(idx + 1, d);
                }

                // SS: do not steal the item
                var c2 = Solve(idx + 1, remainingCapacity);

                var totalProfit = Math.Max(c1, c2);
                return totalProfit;
            }

            return Solve(0, knapsackCapacity);
        }

        public int KnapsackBottomUp(int[] value, int[] weight, int knapsackCapacity)
        {
            // SS: runtime complexity: O(N * capacity)
            // SS: space complexity: O(N * capacity)

            // SS: 2d dp grid due to two parameters, remaining capacity and item position
            var dp = new int[value.Length + 1][];
            for (var i = 0; i <= value.Length; i++)
            {
                dp[i] = new int[knapsackCapacity + 1];
            }

            // SS: nothing to do in set boundary conditions since the dp array
            // is already initialized with 0

            // SS: fill in the dp grid
            for (var i = 1; i <= knapsackCapacity; i++)
            {
                for (var j = value.Length - 1; j >= 0; j--)
                {
                    // SS: steal the item if possible
                    var w = weight[j];

                    var c1 = 0;
                    if (i - w >= 0)
                    {
                        c1 = value[j] + dp[j + 1][i - w];
                    }

                    // SS: do not steal the item
                    var c2 = dp[j + 1][i];

                    dp[j][i] = Math.Max(c1, c2);
                }
            }

            return dp[0][knapsackCapacity];
        }

        public int KnapsackBottomUp2(int[] value, int[] weight, int knapsackCapacity)
        {
            // SS: runtime complexity: O(N * capacity)
            // SS: space complexity: O(capacity) since we only access previous columns
            // of the current dp row and the previous dp row.
            // dp row: given item, columns are knapsack capacity

            // SS: 2d dp grid due to two parameters, remaining capacity and item position
            var dp1 = new int[knapsackCapacity + 1];
            var dp2 = new int[knapsackCapacity + 1];

            // SS: nothing to do in set boundary conditions since the dp array
            // is already initialized with 0

            // SS: fill in the dp grid
            for (var j = value.Length - 1; j >= 0; j--)
            {
                for (var i = 1; i <= knapsackCapacity; i++)
                {
                    // SS: steal the item if possible
                    var w = weight[j];

                    var c1 = 0;
                    if (i - w >= 0)
                    {
                        c1 = value[j] + dp2[i - w];
                    }

                    // SS: do not steal the item
                    var c2 = dp2[i];

                    dp1[i] = Math.Max(c1, c2);
                }

                var tmp = dp1;
                dp1 = dp2;
                dp2 = tmp;
            }

            return dp2[knapsackCapacity];
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] value = {3000, 2000, 1500};
                int[] weight = {4, 3, 1};

                // Act
                var profit = new Solution().Knapsack(value, weight, 4);

                // Assert
                Assert.AreEqual(3500, profit);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] value = {3000, 2000, 1500, 2000};
                int[] weight = {4, 3, 1, 1};

                // Act
                var profit = new Solution().Knapsack(value, weight, 4);

                // Assert
                Assert.AreEqual(4000, profit);
            }
        }
    }
}