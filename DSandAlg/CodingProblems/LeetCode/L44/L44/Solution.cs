#region

using NUnit.Framework;

#endregion

// Problem: 44. Wildcard Matching
// URL: https://leetcode.com/problems/wildcard-matching/

namespace LeetCode44
{
    public class Solution
    {
        public bool IsMatch(string s, string p)
        {
            var dp = new bool[s.Length + 1][];
            for (var i = 0; i <= s.Length; i++)
            {
                dp[i] = new bool[p.Length + 1];
            }

            // SS: set boundary conditions...

            // SS: if exhausted, then it is a match
            dp[s.Length][p.Length] = true;

            // SS: This condition means that if we have exhausted the input string,
            // we still match if the pattern is all *
            for (var i = p.Length - 1; i >= 0; i--)
            {
                dp[s.Length][i] = p[i] == '*' && dp[s.Length][i + 1];
            }

            // SS: if the pattern is exhausted, but there is input left to match,
            // the match is false
            for (var i = 0; i < s.Length; i++)
            {
                dp[i][^1] = false;
            }

            for (var i = s.Length - 1; i >= 0; i--)
            {
                for (var j = p.Length - 1; j >= 0; j--)
                {
                    if (p[j] == '*')
                    {
                        // SS: match 1 char
                        var b1 = dp[i + 1][j];

                        // SS: match 0 char
                        var b2 = dp[i][j + 1];

                        dp[i][j] = b1 || b2;
                    }
                    else if (p[j] == '?' || s[i] == p[j])
                    {
                        var b = dp[i + 1][j + 1];
                        dp[i][j] = b;
                    }
                    else
                    {
                        dp[i][j] = false;
                    }
                }
            }

            return dp[0][0];
        }

        public bool IsMatch2(string s, string p)
        {
            // SS: Divide & Conquer solution, times out

            bool Matches(int sIdx, int pIdx)
            {
                if (sIdx == s.Length)
                {
                    while (pIdx < p.Length && p[pIdx] == '*')
                    {
                        pIdx++;
                    }

                    return pIdx == p.Length;
                }

                if (pIdx == p.Length)
                {
                    return false;
                }

                var b1 = true;
                var b2 = false;

                if (p[pIdx] == '*')
                {
                    // SS: zero match
                    b1 = Matches(sIdx, pIdx + 1);

                    // SS: match match
                    b2 = Matches(sIdx + 1, pIdx);
                }
                else if (p[pIdx] == '?' || s[sIdx] == p[pIdx])
                {
                    // SS: match 1 char
                    b1 = Matches(sIdx + 1, pIdx + 1);
                }
                else
                {
                    b1 = false;
                }

                return b1 || b2;
            }

            return Matches(0, 0);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "aa";
                var p = "a";

                // Act
                var matches = new Solution().IsMatch(s, p);

                // Assert
                Assert.False(matches);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "aa";
                var p = "*";

                // Act
                var matches = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(matches);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "cb";
                var p = "?a";

                // Act
                var matches = new Solution().IsMatch(s, p);

                // Assert
                Assert.False(matches);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "adceb";
                var p = "*a*b";

                // Act
                var matches = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(matches);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "acdcb";
                var p = "a*c?b";

                // Act
                var matches = new Solution().IsMatch(s, p);

                // Assert
                Assert.False(matches);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var s = "acdcb";
                var p = "**";

                // Act
                var matches = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(matches);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var s = "acdcb";
                var p = "*?";

                // Act
                var matches = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(matches);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                var s = "aaaabaaaabbbbaabbbaabbaababbabbaaaababaaabbbbbbaabbbabababbaaabaabaaaaaabbaabbbbaababbababaabbbaababbbba";
                var p = "*****b*aba***babaa*bbaba***a*aaba*b*aa**a*b**ba***a*a*";

                // Act
                var matches = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(matches);
            }

            [Test]
            public void Test9()
            {
                // Arrange
                var s = "";
                var p = "*****";

                // Act
                var matches = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(matches);
            }

            [Test]
            public void Test10()
            {
                // Arrange
                var s = "abcabczzzde";
                var p = "*abc???de*";

                // Act
                var matches = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(matches);
            }
        }
    }
}