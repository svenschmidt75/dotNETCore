#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 720. Longest Word in Dictionary
// URL: https://leetcode.com/problems/longest-word-in-dictionary/

namespace LeetCode
{
    public class Solution
    {
        public string LongestWord(string[] words)
        {
//            return LongestWord1(words);
            return LongestWord2(words);
        }

        private string LongestWord2(string[] words)
        {
            // SS: runtime complexity: O(n log n) for sorting and O(n * s) for loop, so
            // combined: O(s * n + n * log n)
            // space complexity: O(n)

            // SS: sort input in lexicographic order
            Array.Sort(words, (s1, s2) => s1.Length.CompareTo(s2.Length));

            var set = new HashSet<string>();

            var longestWord = -1;
            var longestWordLength = 0;

            // SS: O(n * s) if we have to compare strings every time
            for (var i = 0; i < words.Length; i++)
            {
                var word = words[i];
                if (word.Length == 1)
                {
                    set.Add(word);

                    if (longestWord == -1 || word.CompareTo(words[longestWord]) < 0)
                    {
                        longestWord = i;
                        longestWordLength = 1;
                    }
                }
                else
                {
                    var w = word[..^1];

                    // SS: is there succession?
                    if (set.Contains(w))
                    {
                        set.Add(word);

                        if (longestWord >= 0 && longestWordLength < word.Length || longestWord >= 0 && word.CompareTo(words[longestWord]) < 0)
                        {
                            longestWord = i;
                            longestWordLength = word.Length;
                        }
                    }
                }
            }

            return longestWord >= 0 ? words[longestWord] : "";
        }

        private string LongestWord1(string[] words)
        {
            // SS: runtime complexity: O(n^2 log n), because sorting is O(n log n),
            // but comparing strings is O(s), so O(s * n * log n) combined.
            // space complexity: O(n)

            // SS: sort input in lexicographic order
            Array.Sort(words);

            var set = new HashSet<string>();

            var longestWord = -1;
            var longestWordLength = 0;

            for (var i = 0; i < words.Length; i++)
            {
                var word = words[i];
                if (word.Length == 1)
                {
                    set.Add(word);

                    if (longestWord == -1)
                    {
                        longestWord = i;
                        longestWordLength = 1;
                    }
                }
                else
                {
                    var w = word[..^1];

                    // SS: is there succession?
                    if (set.Contains(w))
                    {
                        set.Add(word);

                        if (longestWord >= 0 && longestWordLength < word.Length)
                        {
                            longestWord = i;
                            longestWordLength = word.Length;
                        }
                    }
                }
            }

            return longestWord >= 0 ? words[longestWord] : "";
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {"w", "wo", "wor", "worl", "world"}, "world")]
            [TestCase(new[] {"a", "banana", "app", "appl", "ap", "apply", "apple"}, "apple")]
            [TestCase(new[] {"wa", "was"}, "")]
            [TestCase(new[] {"a"}, "a")]
            [TestCase(new[] {"ts", "e", "x", "pbhj", "opto", "xhigy", "erikz", "pbh", "opt", "erikzb", "eri", "erik", "xlye", "xhig", "optoj", "optoje", "xly", "pb", "xhi", "x", "o"}, "e")]
            public void Test(string[] words, string expected)
            {
                // Arrange

                // Act
                var result = new Solution().LongestWord(words);

                // Assert
                Assert.AreEqual(expected, result);
            }
        }
    }
}