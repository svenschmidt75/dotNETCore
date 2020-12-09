#region

using NUnit.Framework;

#endregion

// Problem: 91. Decode Ways
// URL: https://leetcode.com/problems/decode-ways/

namespace LeetCode
{
    public class Solution
    {
        public int NumDecodings(string s)
        {
            // SS: runtime complexity: O(N)

            var dp = new int[s.Length + 1];
            dp[^1] = 1;

            for (var i = s.Length - 1; i >= 0; i--)
            {
                var n = 0;

                if (s[i] != '0')
                {
                    n = dp[i + 1];

                    if (i <= s.Length - 2)
                    {
                        var d1 = s[i] - '0';
                        var d2 = s[i + 1] - '0';
                        var v = d1 * 10 + d2;
                        if (v >= 1 && v <= 26)
                        {
                            var n2 = dp[i + 2];
                            n += n2;
                        }
                    }
                }

                dp[i] = n;
            }

            return dp[0];
        }

        public int NumDecodings2(string s)
        {
            // SS: runtime complexity: O(2^N)

            int Decode(int i)
            {
                if (i == s.Length)
                {
                    return 1;
                }

                // SS: a single digit is always going to work
                var n = 0;
                if (s[i] != '0')
                {
                    n = Decode(i + 1);
                }

                // SS: no leading 0
                if (i <= s.Length - 2 && s[i] != '0')
                {
                    var d1 = s[i] - '0';
                    var d2 = s[i + 1] - '0';
                    var v = d1 * 10 + d2;
                    if (v >= 1 && v <= 26)
                    {
                        var n2 = Decode(i + 2);
                        n += n2;
                    }
                }

                return n;
            }

            var n = Decode(0);
            return n;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "12";

                // Act
                var n = new Solution().NumDecodings(s);

                // Assert
                Assert.AreEqual(2, n);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "226";

                // Act
                var n = new Solution().NumDecodings(s);

                // Assert
                Assert.AreEqual(3, n);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "1";

                // Act
                var n = new Solution().NumDecodings(s);

                // Assert
                Assert.AreEqual(1, n);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "0";

                // Act
                var n = new Solution().NumDecodings(s);

                // Assert
                Assert.AreEqual(0, n);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "10";

                // Act
                var n = new Solution().NumDecodings(s);

                // Assert
                Assert.AreEqual(1, n);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var s = "99";

                // Act
                var n = new Solution().NumDecodings(s);

                // Assert
                Assert.AreEqual(1, n);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var s = "3500465";

                // Act
                var n = new Solution().NumDecodings(s);

                // Assert
                Assert.AreEqual(0, n);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                var s = "204";

                // Act
                var n = new Solution().NumDecodings(s);

                // Assert
                Assert.AreEqual(1, n);
            }

            [Test]
            public void Test9()
            {
                // Arrange
                var s = "2100465";

                // Act
                var n = new Solution().NumDecodings(s);

                // Assert
                Assert.AreEqual(0, n);
            }

            [Test]
            public void Test10()
            {
                // Arrange
                var s = "99457486734823498762349876987634587623432023498765349786534";

                // Act
                var n = new Solution().NumDecodings(s);

                // Assert
                Assert.AreEqual(16, n);
            }

            [Test]
            public void Test11()
            {
                // Arrange
                var s = "20234";

                // Act
                var n = new Solution().NumDecodings(s);

                // Assert
                Assert.AreEqual(2, n);
            }
        }
    }
}