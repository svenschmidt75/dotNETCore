#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 87. Scramble String
// URL: https://leetcode.com/problems/scramble-string/

namespace LeetCode
{
    public class Solution
    {
        public bool IsScramble(string s1, string s2)
        {
            return IsScrambleFast(s1, s2);
        }

        public bool IsScrambleFast(string s1, string s2)
        {
            // https://leetcode.com/problems/scramble-string/discuss/29392/Share-my-4ms-c%2B%2B-recursive-solution
            // scramble both strings until they are equal

            // SS: this check is important (i.e. terminating condition)
            if (s1 == s2) return true;

            // SS: check each character occurs with the same
            // frequency between the two strings.
            // This is a filtering step...
            var len = s1.Length;
            var count = new int[26];
            for (var i = 0; i < len; i++)
            {
                count[s1[i] - 'a']++;
                count[s2[i] - 'a']--;
            }

            // if un-match, return false;
            for (var i = 0; i < 26; i++)
            {
                if (count[i] != 0) return false;
            }

            for (var i = 1; i <= len - 1; i++)
            {
                if (IsScramble(s1.Substring(0, i), s2.Substring(0, i)) && IsScramble(s1.Substring(i), s2.Substring(i))) return true;

                if (IsScramble(s1.Substring(0, i), s2.Substring(len - i)) && IsScramble(s1.Substring(i), s2.Substring(0, len - i))) return true;
            }

            return false;
        }

        public bool IsScrambleDivideConquer(string s1, string s2)
        {
            // SS: runtime complexity: O(2^n)

            bool Solve(string s, int idx)
            {
                if (s.Length == 1)
                {
                    return s[0] == s2[idx];
                }

                for (var i = 1; i < s.Length; i++)
                {
                    var s11 = s[..i];
                    var s12 = s[i..];

                    if (Solve(s11, idx) && Solve(s12, idx + s11.Length))
                    {
                        return true;
                    }

                    if (Solve(s12, idx) && Solve(s11, idx + s12.Length))
                    {
                        return true;
                    }
                }

                return false;
            }

            return Solve(s1, 0);
        }

        public bool IsScrambleTopDown(string s1, string s2)
        {
            // SS: runtime complexity: 

            var dict = new Dictionary<string, bool>[s1.Length];
            for (var i = 0; i < s1.Length; i++)
            {
                dict[i] = new Dictionary<string, bool>();
            }

            bool Solve(string s, int idx)
            {
                if (s.Length == 1)
                {
                    return s[0] == s2[idx];
                }

                if (dict[idx].TryGetValue(s, out var v))
                {
                    return v;
                }

                for (var i = 1; i < s.Length; i++)
                {
                    var s11 = s[..i];
                    var s12 = s[i..];

                    if (Solve(s11, idx) && Solve(s12, idx + s11.Length))
                    {
                        dict[idx][s] = true;
                        return true;
                    }

                    if (Solve(s12, idx) && Solve(s11, idx + s12.Length))
                    {
                        dict[idx][s] = true;
                        return true;
                    }
                }

                dict[idx][s] = false;
                return false;
            }

            return Solve(s1, 0);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s1 = "great";
                var s2 = "rgeat";

                // Act
                var result = new Solution().IsScramble(s1, s2);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s1 = "abcde";
                var s2 = "caebd";

                // Act
                var result = new Solution().IsScramble(s1, s2);

                // Assert
                Assert.False(result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s1 = "a";
                var s2 = "a";

                // Act
                var result = new Solution().IsScramble(s1, s2);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s1 = "greatgreatgreatgreat";
                var s2 = "taregtaregtaregtareg";

                // Act
                var result = new Solution().IsScramble(s1, s2);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s1 = "abc";
                var s2 = "acb";

                // Act
                var result = new Solution().IsScramble(s1, s2);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var s1 = "abcd";
                var s2 = "badc";

                // Act
                var result = new Solution().IsScramble(s1, s2);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var s1 = "great";
                var s2 = "rgtae";

                // Act
                var result = new Solution().IsScramble(s1, s2);

                // Assert
                Assert.True(result);
            }
        }
    }
}