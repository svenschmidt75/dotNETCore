#region

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
            // SS: exponential time solution
            var maxLength = 2 * s.Length;

            (int, string) Solve(string prefix, int posLeft, int posRight)
            {
                // SS: base cases
                if (prefix.Length + s.Length > maxLength)
                {
                    return (int.MaxValue, "");
                }

                if (posLeft > posRight)
                {
                    return (int.MaxValue, "");
                }

                if (posLeft == posRight)
                {
                    return (prefix.Length + s.Length, prefix + s);
                }

                var shortestPalindrome = string.Empty;
                var shortestLength = int.MaxValue;

                // SS: case 1, equal chars
                if (s[posLeft] == s[posRight])
                {
                    // SS: case 1a: add character and move posRight 1 to the right
                    var case1 = Solve(prefix + s[posRight], posLeft, posRight - 1);

                    // SS: case 1b: do not add and move both closer
                    var case2 = Solve(prefix, posLeft + 1, posRight - 1);

                    // SS: take shortest
                    if (case1.Item1 < case2.Item1)
                    {
                        (shortestLength, shortestPalindrome) = case1;
                    }
                    else if (case2.Item1 < int.MaxValue)
                    {
                        (shortestLength, shortestPalindrome) = case2;
                    }
                }
                else
                {
                    // SS: case 2: add character and move posRight 1 to the right
                    var case2 = Solve(prefix + s[posRight], posLeft, posRight - 1);

                    if (case2.Item1 < shortestLength)
                    {
                        (shortestLength, shortestPalindrome) = case2;
                    }
                }

                return (shortestLength, shortestPalindrome);
            }

            (var length, var palindrome) = Solve("", 0, s.Length - 1);

            return palindrome;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("adam", "madam")]
            [TestCase("adaam", "maadaam")]
            [TestCase("aacecaaa", "aaacecaaa")]
            [TestCase("abcd", "dcbabcd")]
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