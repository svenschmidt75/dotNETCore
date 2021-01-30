#region

using System;
using NUnit.Framework;

#endregion

// Problem: 132. Palindrome Partitioning II
// URL: https://leetcode.com/problems/palindrome-partitioning-ii/

namespace LeetCode
{
    public class Solution
    {
        public int MinCut(string s)
        {
            // SS: There are two basic approaches:
            // 1: You somehow try to find where the cuts occur with some algorithm
            // 2: At each step, you assume both situations and try to find ways to
            //    manage the exponential complexity. Sometimes, DP can be used to
            //    do so, as was done here.

            //            return MinCutSlow(s);
            return MinCutBottomUp(s);
        }

        private int MinCutSlow(string s)
        {
            // SS: Divide & Conquer
            // runtime complexity: O(2^N * N), at each position, we have two paths,
            // and we need to check whether a substring is a palindrome

            int Solve(int startIdx, int idx)
            {
                // SS: base case
                if (idx == s.Length)
                {
                    if (startIdx == s.Length)
                    {
                        // SS: empty string, so "valid" palindrome, no split
                        return 0;
                    }

                    // SS: check whether valid palindrome in [startIdx, s.Length - 1]
                    if (IsPalindrome(s, startIdx, s.Length - 1))
                    {
                        // SS: if valid palindrome, no split
                        return 0;
                    }

                    // SS: not a palindrome, so invalid case, do not count
                    return int.MaxValue;
                }

                // SS: assume cut at idx, if [startIdx, idx] is a palindrome
                // since we can only cut when we have a palindrome
                var c1 = int.MaxValue;
                if (IsPalindrome(s, startIdx, idx))
                {
                    c1 = 1 + Solve(idx + 1, idx + 1);
                }

                // SS: assume no break
                var c2 = Solve(startIdx, idx + 1);

                return Math.Min(c1, c2);
            }

            var minCuts = Solve(0, 0);
            return minCuts;
        }

        private int MinCutBottomUp(string s)
        {
            // SS: bottom-up DP, runtime complexity: O(s^3)
            // space complexity: O(s)

            var dp1 = new int[s.Length + 1];
            var dp2 = new int[s.Length + 1];

            // SS: set boundary conditions
            for (var startIdx = s.Length - 1; startIdx >= 0; startIdx--)
            {
                if (IsPalindrome(s, startIdx, s.Length - 1))
                {
                    // SS: if valid palindrome, no split
                    dp2[startIdx] = 0;
                }
                else
                {
                    // SS: not a palindrome, so invalid case, do not count
                    dp2[startIdx] = int.MaxValue;
                }
            }

            // SS: fill in grid
            for (var idx = s.Length - 1; idx >= 0; idx--)
            {
                for (var startIdx = idx; startIdx >= 0; startIdx--)
                {
                    // SS: assume cut at idx, if [startIdx, idx] is a palindrome
                    // since we can only cut when we have a palindrome
                    var c1 = int.MaxValue;
                    if (IsPalindrome(s, startIdx, idx))
                    {
                        c1 = 1 + dp2[idx + 1];
                    }

                    // SS: assume no break
                    var c2 = dp2[startIdx];

                    var c = Math.Min(c1, c2);
                    dp1[startIdx] = c;
                }

                // SS: swap
                var tmp = dp1;
                dp1 = dp2;
                dp2 = tmp;
            }

            return dp2[0];
        }

        internal static bool IsPalindrome(string s, int min, int max)
        {
            // SS: O( (max - min + 1) / 2) = O(max - min)
            var i = min;
            var j = max;
            while (i < j && s[i] == s[j])
            {
                i++;
                j--;
            }

            return i >= j;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("abba", true)]
            [TestCase("aba", true)]
            [TestCase("abca", false)]
            public void TestPalindrome(string s, bool expected)
            {
                // Arrange

                // Act

                // Assert
                Assert.AreEqual(expected, IsPalindrome(s, 0, s.Length - 1));
            }

            [Test]
            public void Test1()
            {
                // Arrange
                var s = "aab";

                // Act
                var minCuts = new Solution().MinCut(s);

                // Assert
                Assert.AreEqual(1, minCuts);
            }

            [TestCase("b", 0)]
            [TestCase("ab", 1)]
            [TestCase("aab", 1)]
            [TestCase("baab", 0)]
            [TestCase("bbaab", 1)]
            [TestCase("abbaab", 2)]
            [TestCase("aabbaab", 1)]
            [TestCase("caabbaab", 2)]
            [TestCase("saghdfdjhfyyfgjdjsssuitybferuiy", 21)]
            public void Test2(string s, int expectedMinCuts)
            {
                // Arrange

                // Act
                var minCuts = new Solution().MinCut(s);

                // Assert
                Assert.AreEqual(expectedMinCuts, minCuts);
            }
        }
    }
}