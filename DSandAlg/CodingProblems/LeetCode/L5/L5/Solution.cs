#region

using System;
using NUnit.Framework;

#endregion

// 5. Longest Palindromic Substring
// https://leetcode.com/problems/longest-palindromic-substring/
// done with John on Tuesday, 27th 2020

namespace L5
{
    public class Solution
    {
        public string LongestPalindrome(string s)
        {
            var maxPalindromeLength = 0;
            var minIdx = s.Length;
            var maxIdx = 0;

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];

                var j = i + 1;
                while (j < s.Length && s[j] == c)
                {
                    j++;
                }

                var palindromeLength = j - i;

                if (j == s.Length)
                {
                    if (palindromeLength > maxPalindromeLength)
                    {
                        maxPalindromeLength = palindromeLength;
                        minIdx = i;
                        maxIdx = j;
                    }
                }
                else
                {
                    var k = i;
                    while (k > 0 && j < s.Length && s[k - 1] == s[j])
                    {
                        k--;
                        j++;
                        palindromeLength += 2;
                    }

                    if (palindromeLength > maxPalindromeLength)
                    {
                        maxPalindromeLength = palindromeLength;
                        minIdx = Math.Max(0, k);
                        maxIdx = j;
                    }
                }
            }

            return s[minIdx..maxIdx];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "babad";

                // Act
                var result = new Solution().LongestPalindrome(s);

                // Assert
                Assert.AreEqual("bab", result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "cbbd";

                // Act
                var result = new Solution().LongestPalindrome(s);

                // Assert
                Assert.AreEqual("bb", result);
            }


            [Test]
            public void Test3()
            {
                // Arrange
                var s = "a";

                // Act
                var result = new Solution().LongestPalindrome(s);

                // Assert
                Assert.AreEqual("a", result);
            }


            [Test]
            public void Test4()
            {
                // Arrange
                var s = "ac";

                // Act
                var result = new Solution().LongestPalindrome(s);

                // Assert
                Assert.AreEqual("a", result);
            }
        }
    }
}