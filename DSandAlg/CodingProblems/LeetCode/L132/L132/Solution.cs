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
            return MinCutSlow(s);
        }

        private int MinCutSlow(string s)
        {
            // SS: Divide & Conquer
            // runtime complexity: O(2^N * N), at each position, we have two paths,
            // and we need to check whether a substring is a palindrome
            
            int Solve(int idx, int startIdx)
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
                var c2 = Solve(idx + 1, startIdx);

                return Math.Min(c1, c2);
            }

            var minCuts = Solve(0, 0);
            return minCuts;
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