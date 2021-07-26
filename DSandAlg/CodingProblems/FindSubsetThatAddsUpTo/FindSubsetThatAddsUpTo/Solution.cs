using NUnit.Framework;

// Problem:
// URL:

namespace LeetCode
{
    public class Solution
    {
        public int FindNumberOfSubsets(int[] array, int target)
        {
            // SS: 0 < arr[i], no duplicates
            // return FindNumberOfSubsets1(array, target);
            return FindNumberOfSubsets2(array, target);
        }

        private int FindNumberOfSubsets2(int[] array, int target)
        {
            // SS: converting DFS(int idx, int sum) to bottom-up DP
            // runtime complexity: O(n * target)
            // space complexity: O(n * target)

            int[][] dp = new int[array.Length + 1][];
            for (int i = 0; i <= array.Length; i++)
            {
                dp[i] = new int[target + 1];
            }

            for (int i = array.Length - 1; i >= 0; i--)
            {
                for (int sum = 0; sum <= target; sum++)
                {
                    // SS: case 1: take the current value
                    int value = array[i] + sum;
                    int s1 = 0;
                    if (value <= target)
                    {
                        s1 = dp[i + 1][value];
                    }

                    // SS: case 2: do not take the current value
                    int s2 = dp[i + 1][sum];
                    int s = s1 + s2;

                    if (value == target)
                    {
                        // SS: we found another solution
                        s++;
                    }

                    dp[i][sum] = s;
                }
            }

            return dp[0][0];
        }

        private int FindNumberOfSubsets1(int[] array, int target)
        {
            // SS: backtracking, i.e. runtime complexity O(2^n)
            // space complexity: O(n) for stack

            int DFS(int idx, int sum)
            {
                if (sum == target)
                {
                    // SS: we found a solution
                    return 1;
                }

                if (sum > target || idx == array.Length)
                {
                    return 0;
                }

                var value = array[idx] + sum;

                // SS: case 1: include value
                int s1 = DFS(idx + 1, value);

                // SS: case 2: do not include current value
                int s2 = DFS(idx + 1, sum);

                return s1 + s2;
            }

            int n = DFS(0, 0);
            return n;
        }


        [TestFixture]
        public class Tests
        {
            [TestCase(new[] { 2, 4, 6, 10 }, 16, 2)]
            [TestCase(new[] { 2, 4, 6, 10, 13, 18, 21, 65, 185 }, 95, 2)]
            public void Test(int[] array, int target, int expected)
            {
                // Arrange

                // Act
                int nSubsets = new Solution().FindNumberOfSubsets(array, target);

                // Assert
                Assert.AreEqual(expected, nSubsets);
            }
        }
    }
}