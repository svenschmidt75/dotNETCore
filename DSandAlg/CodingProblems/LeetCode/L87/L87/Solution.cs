#region

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
            bool Solve(string s, string suffix)
            {
                if (s.Length == 1)
                {
                    return s + suffix == s2;
                }
                
                for (var i = 1; i < s.Length; i++)
                {
                    var s11 = s[..i];
                    var s12 = s[i..];

                    if (Solve(s11, s12 + suffix))
                    {
                        return true;
                    }

                    if (Solve(s12, s11 + suffix))
                    {
                        return true;
                    }
                }

                return false;
            }

            return Solve(s1, "");
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
        }
    }
}