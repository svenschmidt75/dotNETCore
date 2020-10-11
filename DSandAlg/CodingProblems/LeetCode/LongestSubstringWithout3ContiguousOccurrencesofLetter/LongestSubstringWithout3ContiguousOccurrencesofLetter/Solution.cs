#region

using NUnit.Framework;

#endregion

// https://leetcode.com/discuss/interview-question/398031/

namespace LongestSubstringWithout3ContiguousOccurrencesofLetter
{
    public class Solution
    {
        public string Solve(string s)
        {
            // SS: sliding window approach
            // runtime complexity: O(N)
            // space complexity: O(1)
            var i = 0;
            var j = 0;
            var na = 0;
            var nb = 0;

            var maxLength = 0;
            var maxi = 0;

            while (j < s.Length)
            {
                var c = s[j];

                if (c == 'a')
                {
                    nb = 0;
                    na++;

                    // SS: 3 consecutive a?
                    if (na == 3)
                    {
                        // SS: did we find a longer substring?
                        if (j - i > maxLength)
                        {
                            maxLength = j - i;
                            maxi = i;
                        }

                        na = 2;
                        i = j - 1;
                    }
                }
                else if (c == 'b')
                {
                    na = 0;
                    nb++;

                    // SS: 3 consecutive b?
                    if (nb == 3)
                    {
                        // SS: did we find a longer substring?
                        if (j - i > maxLength)
                        {
                            maxLength = j - i;
                            maxi = i;
                        }

                        nb = 2;
                        i = j - 1;
                    }
                }
                else
                {
                    na = 0;
                    nb = 0;
                }

                j++;
            }

            return maxLength > 0 ? s.Substring(maxi, maxLength) : s;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "aabbaaaabb";

                // Act
                var result = new Solution().Solve(s);

                // Assert
                Assert.AreEqual("aabbaa", result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "aabbaabbaa";

                // Act
                var result = new Solution().Solve(s);

                // Assert
                Assert.AreEqual(s, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "";

                // Act
                var result = new Solution().Solve(s);

                // Assert
                Assert.AreEqual(string.Empty, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "abbaabbaaabbaaa";

                // Act
                var result = new Solution().Solve(s);

                // Assert
                Assert.AreEqual("abbaabbaa", result);
            }
        }
    }
}