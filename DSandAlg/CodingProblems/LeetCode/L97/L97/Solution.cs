#region

using NUnit.Framework;

#endregion

// Problem: 97. Interleaving String
// URL: https://leetcode.com/problems/interleaving-string/

namespace LeetCode
{
    public class Solution
    {
        public bool IsInterleave(string s1, string s2, string s3)
        {
            if (s3.Length != s1.Length + s2.Length)
            {
                return false;
            }

            var dp = new bool[s2.Length + 1][];
            for (var i = 0; i <= s2.Length; i++)
            {
                dp[i] = new bool[s1.Length + 1];
            }

            // SS: set boundary conditions
            dp[^1][^1] = true;

            // SS: last column
            var state = true;
            for (var i = s2.Length - 1; i >= 0; i--)
            {
                if (state && s2[i] == s3[s3.Length - (s2.Length - 1 - i) - 1])
                {
                    dp[i][s1.Length] = true;
                }
                else
                {
                    state = false;
                }
            }

            // SS: last row
            state = true;
            for (var i = s1.Length - 1; i >= 0; i--)
            {
                if (state && s1[i] == s3[s3.Length - (s1.Length - 1 - i) - 1])
                {
                    dp[s2.Length][i] = true;
                }
                else
                {
                    state = false;
                }
            }

            // SS: fill in grid
            for (var i = s2.Length - 1; i >= 0; i--)
            {
                for (var j = s1.Length - 1; j >= 0; j--)
                {
                    var s3Idx = s3.Length - (s1.Length - j) - (s2.Length - i);

                    state = false;

                    if (s1[j] == s3[s3Idx])
                    {
                        state = dp[i][j + 1];
                    }

                    if (s2[i] == s3[s3Idx])
                    {
                        state |= dp[i + 1][j];
                    }

                    dp[i][j] = state;
                }
            }

            return dp[0][0];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s1 = "aabcc";
                var s2 = "dbbca";
                var s3 = "aadbbcbcac";

                // Act
                var result = new Solution().IsInterleave(s1, s2, s3);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s1 = "aabcc";
                var s2 = "dbbca";
                var s3 = "aadbbbaccc";

                // Act
                var result = new Solution().IsInterleave(s1, s2, s3);

                // Assert
                Assert.False(result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s1 = "";
                var s2 = "";
                var s3 = "";

                // Act
                var result = new Solution().IsInterleave(s1, s2, s3);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s1 = "abcf";
                var s2 = "dgf";
                var s3 = "a";

                // Act
                var result = new Solution().IsInterleave(s1, s2, s3);

                // Assert
                Assert.False(result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s1 = "abcdef";
                var s2 = "";
                var s3 = "abcdef";

                // Act
                var result = new Solution().IsInterleave(s1, s2, s3);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var s1 = "";
                var s2 = "abcdef";
                var s3 = "abcdef";

                // Act
                var result = new Solution().IsInterleave(s1, s2, s3);

                // Assert
                Assert.True(result);
            }
        }
    }
}