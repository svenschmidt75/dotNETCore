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
            // SS: from LeetCode submissions...
            
            if (string.IsNullOrWhiteSpace(s))
            {
                return s;
            }

            // SS: get length of "prefix" palindrome
            var n = s.Length;
            var i = 0;
            for (var j = n - 1; j >= 0; j--)
            {
                if (s[i] == s[j])
                {
                    i++;
                }
            }

            Console.WriteLine("i is " + i);
            if (i == n)
            {
                return s;
            }

            var arr = s.Substring(i, n - i).ToCharArray();
            Array.Reverse(arr);
            var remain_rev = new string(arr);
            return remain_rev + ShortestPalindrome(s.Substring(0, i)) + s.Substring(i);
        }

        public string ShortestPalindrome3(string s)
        {
            // SS: runtime complexity: O(s^2)

            if (string.IsNullOrWhiteSpace(s))
            {
                return string.Empty;
            }

            var prefix = "";

            var left = 0;
            var right = s.Length - 1;
            while (IsPalindrome(s, left, right) == false)
            {
                prefix = prefix + s[right];
                right--;
            }

            return prefix + s;
        }

        private static bool IsPalindrome(string s, int left, int right)
        {
            // while (left < right)
            // {
            //     if (s[left] != s[right])
            //     {
            //         return false;
            //     }
            //
            //     left++;
            //     right--;
            // }
            //
            // return true;

            int i = 0;
            for (int j = right; j >= left; j--)
            {
                if (s[i] == s[j])
                {
                    i++;
                }
            }

            return i == right - left + 1;
        }

        public string ShortestPalindrome2(string s)
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
                    var palindrome = prefix + s[Math.Max(posLeft, posRight)..];

                    // SS: check if really is a valid palindrome
                    if (palindrome.Substring(palindrome.Length - s.Length) == s)
                    {
                        return (palindrome.Length, palindrome);
                    }

                    return (int.MaxValue, "");
                }

                // SS: add character and move posRight 1 to the left
                var (shortestLength, shortestPalindrome) = Solve(prefix + s[posRight], posLeft, posRight - 1);

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
                return string.Empty;
            }

            var (_, palindrome) = Solve("", 0, s.Length - 1);
            return palindrome;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("adam", "madam")]
            [TestCase("adaam", "maadaam")]
            [TestCase("aacecaaa", "aaacecaaa")]
            [TestCase("aaceeaaa", "aaaeecaaceeaaa")]
            [TestCase("abcd", "dcbabcd")]
            [TestCase("abca", "acbabca")]
            [TestCase("lsudhjfgaksjhfg", "gfhjskagfjhduslsudhjfgaksjhfg")]
            [TestCase("lsudhjfgaksjhfgl", "lgfhjskagfjhduslsudhjfgaksjhfgl")]
            [TestCase("lsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfg"
                , "gfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslgfhjskagfjhduslsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfglsudhjfgaksjhfg")]
            [TestCase("madam", "madam")]
            [TestCase("", "")]
            [TestCase("a", "a")]
            [TestCase("ab", "bab")]
            [TestCase("aa", "aa")]
            public void Test1(string s, string expected)
            {
                // Arrange

                // Act
                var result = new Solution().ShortestPalindrome3(s);

                // Assert
                Assert.AreEqual(expected, result);
            }
        }
    }
}