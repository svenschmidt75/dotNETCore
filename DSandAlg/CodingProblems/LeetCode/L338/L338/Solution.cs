#region

using NUnit.Framework;

#endregion

// Problem: 338. Counting Bits
// URL: https://leetcode.com/problems/counting-bits/

namespace LeetCode
{
    public class Solution
    {
        public int[] CountBits(int n)
        {
//            return CountBits1(n);
            return CountBits2(n);
        }

        private int[] CountBits2(int n)
        {
            // SS: similar idea to Sieve of Eratosthenes
            // We loop over all numbers and multiply by 2, because this amounts
            // to a left-shift by 1 and has the same number of bits.
            // This way, we only ever process each i in [0..n] once, i.e. linear
            // runtime.

            var dp = new int[n + 1];
            if (n == 0)
            {
                return dp;
            }

            dp[1] = 1;
            if (n == 1)
            {
                return dp;
            }

            for (var i = 2; i <= n; i++)
            {
                var count = dp[i];
                if (count > 0)
                {
                    // SS: already processed
                    continue;
                }

                count += dp[i - 1];
                if (i > 2)
                {
                    count += dp[1];
                }

                var j = i;
                while (j <= n)
                {
                    dp[j] = count;
                    j *= 2;
                }
            }

            return dp;
        }

        private int[] CountBits1(int n)
        {
            // SS: runtime complexity: O(n log n)
            // We have an outer loop n, and per n, we subtract the LSB(n)
            // from n until it is zero, hence the inner loop is O(log n).
            // Space complexity: O(n)

            var dp = new int[n + 1];
            if (n == 0)
            {
                return dp;
            }

            dp[1] = 1;
            if (n == 1)
            {
                return dp;
            }

            dp[2] = 1;
            if (n == 2)
            {
                return dp;
            }

            var i = 3;
            while (i <= n)
            {
                var count = 0;
                var k = i;
                while (k > 0 && k <= n)
                {
                    var lsb = k & -k;
                    if (lsb == k)
                    {
                        // SS: power of two
                        count++;
                    }
                    else
                    {
                        count += dp[lsb];
                    }

                    k -= lsb;
                }

                dp[i] = count;
                i++;
            }

            return dp;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(0, new[] { 0 })]
            [TestCase(1, new[] { 0, 1 })]
            [TestCase(2, new[] { 0, 1, 1 })]
            [TestCase(5, new[] { 0, 1, 1, 2, 1, 2 })]
            [TestCase(123
                , new[]
                {
                    0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5
                    , 4, 5, 5, 6, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6
                    , 4, 5, 5, 6
                })]
            public void Test(int n, int[] expected)
            {
                // Arrange

                // Act
                var result = new Solution().CountBits(n);

                // Assert
                CollectionAssert.AreEqual(expected, result);
            }
        }
    }
}