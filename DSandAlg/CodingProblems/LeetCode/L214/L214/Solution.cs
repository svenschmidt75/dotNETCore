#region

using System;
using NUnit.Framework;

#endregion

// Problem: 214. Shortest Palindrome
// URL:

namespace LeetCode
{
    public class Solution
    {
        public string ShortestPalindrome(string s)
        {
            // SS: exponential time solution, O(2^s)
            
            var maxLength = 2 * s.Length;

            (int, string) Solve(string prefix, int posLeft, int posRight)
            {
                // SS: base cases
                
                // SS: the palindrome must be < 2 * s.Length
                if (prefix.Length + s[posRight..].Length > maxLength)
                {
                    return (int.MaxValue, "");
                }

                // SS: found a palindrome?
                if (posLeft >= posRight)
                {
                    string palindrome = prefix + s[Math.Max(posLeft, posRight)..];

                    // SS: check if really is a valid palindrome
                    if (palindrome.Substring(palindrome.Length - s.Length) == s)
                    {
                        return (palindrome.Length, palindrome);
                    }
                    return (int.MaxValue, "");
                }

                // SS: add character and move posRight 1 to the left
                (int shortestLength, string shortestPalindrome) = Solve(prefix + s[posRight], posLeft, posRight - 1);

                // SS: equal chars
                if (s[posLeft] == s[posRight])
                {
                    // SS: case 1b: do not add and move both pointers closer
                    var case2 = Solve(prefix + s[posRight], posLeft + 1, posRight - 1);

                    // SS: take shortest
                    if (case2.Item1 < int.MaxValue)
                    {
                        (shortestLength, shortestPalindrome) = case2;
                    }
                }

                return (shortestLength, shortestPalindrome);
            }

            if (string.IsNullOrWhiteSpace(s))
            {
                return String.Empty;
            }

            (_, string palindrome) = Solve("", 0, s.Length - 1);
            return palindrome;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("adam", "madam")]
            [TestCase("adaam", "maadaam")]
            [TestCase("aacecaaa", "aaacecaaa")]
            [TestCase("abcd", "dcbabcd")]
            [TestCase("abca", "acbabca")]
            [TestCase("lsudhjfgaksjhfg", "gfhjskagfjhduslsudhjfgaksjhfg")]
            [TestCase("lsudhjfgaksjhfgl", "lgfhjskagfjhduslsudhjfgaksjhfgl")]
            [TestCase("lsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfg", "gfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfg")]
            [TestCase("madam", "madam")]
            [TestCase("", "")]
            [TestCase("a", "a")]
            [TestCase("ab", "bab")]
            [TestCase("aa", "aa")]
            public void Test1(string s, string expected)
            {
                // Arrange

                // Act
                var result = new Solution().ShortestPalindrome(s);

                // Assert
                Assert.AreEqual(expected, result);
            }

        }
    }
}