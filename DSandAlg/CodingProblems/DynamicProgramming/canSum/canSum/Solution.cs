using NUnit.Framework;

// Problem: Dynamic Programming - Learn to Solve Algorithmic Problems & Coding Challenges, can Sum
// URL: https://www.youtube.com/watch?v=oBt53YbR9Kk

namespace LeetCode
{
    public class Solution
    {
        public bool canSum(int[] array, int target)
        {
//            return canSum1(array, target);
//            return canSum2(array, target);
            return canSum3(array, target);
        }

        private bool canSum3(int[] array, int target)
        {
            // SS: top-down DP using memoization
            // 0: undefined
            // 1: false
            // 2: true
            // runtime complexity: O(target * n)
            // space complexity: O(target)

            int[] dp = new int[target + 1];
            dp[0] = 2;

            for (int i = 1; i <= target; i++)
            {
                int localResult = 1;

                for (int j = 0; j < array.Length; j++)
                {
                    int v = array[j];
                    int d = i - v;
                    if (d >= 0)
                    {
                        if (dp[d] == 2)
                        {
                            localResult = 2;
                            break;
                        }
                    }
                }

                dp[i] = localResult;
            }

            return dp[target] == 2;
        }

        private bool canSum2(int[] array, int target)
        {
            // SS: top-down DP using memoization
            // 0: undefined
            // 1: false
            // 2: true
            // runtime complexity: O(target * n)
            // space complexity: O(target)
            
            int[] dp = new int[target + 1];
            dp[0] = 2;

            int Solve(int sum)
            {
                // SS: base case
                if (dp[sum] > 0)
                {
                    return dp[sum];
                }

                // SS: since any number can be used multiple times,
                // we have to loop over all numbers...
                int result = 1;

                for (int i = 0; i < array.Length; i++)
                {
                    int v = array[i];
                    if (sum - v >= 0)
                    {
                        int r = Solve(sum - v);
                        if (r == 2)
                        {
                            result = 2;

                            // SS: short-curcuit
                            break;
                        }
                    }
                }

                dp[sum] = result;
                return result;
            }

            return Solve(target) == 2;
        }

        private bool canSum1(int[] array, int target)
        {
            // SS: D&C
            // runtime complexity: O(n ^ target)
            // space complexity: O(target)

            bool Solve(int sum)
            {
                // SS: base case
                if (sum == 0)
                {
                    return true;
                }

                // SS: since any number can be used multiple times,
                // we have to loop over all numbers...
                for (int i = 0; i < array.Length; i++)
                {
                    int v = array[i];
                    if (sum - v >= 0)
                    {
                        if (Solve(sum - v))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            return Solve(target);
        }


        [TestFixture]
        public class Tests
        {
            [TestCase(new[] { 5, 3, 4, 7 }, 7, true)]
            [TestCase(new[] { 2, 4 }, 7, false)]
            [TestCase(new[] { 2, 3 }, 7, true)]
            [TestCase(new[] { 2, 3, 5 }, 8, true)]
            [TestCase(new[] { 7, 14 }, 300, false)]
            public void Test(int[] array, int target, bool expected)
            {
                // Arrange

                // Act
                var result = new Solution().canSum(array, target);

                // Assert
                Assert.AreEqual(expected, result);
            }
        }
    }
}