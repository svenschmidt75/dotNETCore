#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 290. Word Pattern
// URL: https://leetcode.com/problems/word-pattern/

namespace LeetCode
{
    public class Solution
    {
        public bool WordPattern(string pattern, string s)
        {
            var words = s.Split(' ');

            var patternToWord = new Dictionary<char, string>();

            if (pattern.Length != words.Length)
            {
                return false;
            }

            for (var i = 0; i < words.Length; i++)
            {
                if (patternToWord.TryGetValue(pattern[i], out var word) == false)
                {
                    // SS: check if word is already associated with different key
                    if (patternToWord.ContainsValue(words[i]))
                    {
                        // SS: yes, invalid
                        return false;
                    }

                    word = words[i];
                    patternToWord[pattern[i]] = word;
                }

                if (patternToWord[pattern[i]] != words[i])
                {
                    return false;
                }
            }

            return true;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("abba", "dog cat cat dog", true)]
            [TestCase("abba", "dog cat cat fish", false)]
            [TestCase("aaaa", "dog cat cat dog", false)]
            [TestCase("abba", "dog dog dog dog", false)]
            public void Test(string pattern, string s, bool expectedMatch)
            {
                // Arrange

                // Act
                var isMatch = new Solution().WordPattern(pattern, s);

                // Assert
                Assert.AreEqual(expectedMatch, isMatch);
            }
        }
    }
}