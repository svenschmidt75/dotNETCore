#region

using System;
using NUnit.Framework;

#endregion

// Problem: 152. Maximum Product Subarray
// URL: https://leetcode.com/problems/maximum-product-subarray/

namespace LeetCode
{
    public class Solution
    {
        public int MaxProduct(int[] nums)
        {
//            return MaxProductBruteForce(nums);
//            return MaxProductTopDown(nums);
            return MaxProductBottomUp(nums);
        }

        private int MaxProductTopDown(int[] nums)
        {
            // SS: Larry's solution, https://www.youtube.com/watch?v=R6buQI09vVI
            var dp = new int[nums.Length + 1][];
            var dpCalculated = new bool[nums.Length + 1][];
            for (var i = 0; i < nums.Length + 1; i++)
            {
                dp[i] = new int[2];
                dpCalculated[i] = new bool[2];
            }

            int Solve(int idx, int nNegativesIn)
            {
                // SS: base case 
                if (idx == nums.Length)
                {
                    return 1;
                }

                if (nums[idx] == 0)
                {
                    // SS: split
                    return 0;
                }

                if (dpCalculated[idx][nNegativesIn])
                {
                    return dp[idx][nNegativesIn];
                }

                var nNegatives = nNegativesIn;
                if (nums[idx] < 0)
                {
                    nNegatives++;
                    nNegatives %= 2;
                }

                var c = Solve(idx + 1, nNegatives);
                var localBest = nNegativesIn == 1 ? Math.Min(nums[idx], nums[idx] * c) : Math.Max(nums[idx], nums[idx] * c);

                dp[idx][nNegativesIn] = localBest;
                dpCalculated[idx][nNegativesIn] = true;

                return localBest;
            }

            // SS: do for each position to get the correct nNegatives
            var best = int.MinValue;
            for (var i = 0; i < nums.Length; i++)
            {
                best = Math.Max(best, Solve(i, 0));
            }

            return best;
        }

        private int MaxProductBottomUp(int[] nums)
        {
            var dp1 = new int[nums.Length + 1];
            var dp2 = new int[nums.Length + 1];

            // SS: boundary conditions
            dp1[^1] = 1;
            dp2[^1] = 1;

            var best = int.MinValue;

            for (var idx = nums.Length - 1; idx >= 0; idx--)
            {
                var v = nums[idx];

                var b1 = 0;
                var b2 = 0;

                if (v != 0)
                {
                    int prevPos = dp1[idx + 1];
                    int prevNeg = dp2[idx + 1];

                    var b11 = Math.Max(v, v * prevNeg);
                    var b12 = Math.Max(v, v * prevPos);
                    b1 = Math.Max(b11, b12);

                    var b21 = Math.Min(v, v * prevNeg);
                    var b22 = Math.Min(v, v * prevPos);
                    b2 = Math.Min(b21, b22);
                }

                // SS: positive
                dp1[idx] = b1;
                best = Math.Max(best, b1);

                // SS: negative
                dp2[idx] = b2;
                best = Math.Max(best, b2);
            }

            return best;
        }

        private int MaxProductBruteForce(int[] nums)
        {
            // SS: runtime complexity: O(N^2)
            // space complexity: O(1)

            var best = 0;

            for (var i = 0; i < nums.Length; i++)
            {
                var maxProduct = 1;

                for (var j = i; j < nums.Length; j++)
                {
                    var v = nums[j];
                    maxProduct *= v;
                    best = Math.Max(best, maxProduct);
                }
            }

            return best;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {2, 3, -2, 4};

                // Act
                var maxProduct = new Solution().MaxProduct(nums);

                // Assert
                Assert.AreEqual(6, maxProduct);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {-2, 0, -1};

                // Act
                var maxProduct = new Solution().MaxProduct(nums);

                // Assert
                Assert.AreEqual(0, maxProduct);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {-4, 3, -1};

                // Act
                var maxProduct = new Solution().MaxProduct(nums);

                // Assert
                Assert.AreEqual(12, maxProduct);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {4, 3, -1};

                // Act
                var maxProduct = new Solution().MaxProduct(nums);

                // Assert
                Assert.AreEqual(12, maxProduct);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {4, 3, -1, 4, 5};

                // Act
                var maxProduct = new Solution().MaxProduct(nums);

                // Assert
                Assert.AreEqual(4 * 5, maxProduct);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] nums = {4, -1, 2, 3, 2, -1, -1, 4, 5};

                // Act
                var maxProduct = new Solution().MaxProduct(nums);

                // Assert
                Assert.AreEqual(2 * 3 * 2 * 4 * 5, maxProduct);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[] nums = {-4, 3, 1, 2};

                // Act
                var maxProduct = new Solution().MaxProduct(nums);

                // Assert
                Assert.AreEqual(3 * 1 * 2, maxProduct);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                int[] nums = {-4, 3, 2, -1};

                // Act
                var maxProduct = new Solution().MaxProduct(nums);

                // Assert
                Assert.AreEqual(4 * 3 * 2 * 1, maxProduct);
            }
        }
    }
}