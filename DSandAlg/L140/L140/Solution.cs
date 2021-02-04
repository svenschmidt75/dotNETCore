#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 140. Word Break II
// URL: https://leetcode.com/problems/word-break-ii/

namespace LeetCode
{
    public class Solution
    {
        public IList<string> WordBreak(string s, IList<string> wordDict)
        {
            return WordBreakDFS(s, wordDict);
        }

        public IList<string> WordBreakDFS(string s, IList<string> wordDict)
        {
            var result = new List<string>();

            // SS: is there a solution?
            if (CountSentences(s, wordDict) == 0)
            {
                return result;
            }

            void DFS(int sIdx, string currentString)
            {
                if (sIdx == s.Length)
                {
                    result.Add(currentString.TrimStart());
                    return;
                }

                for (var i = 0; i < wordDict.Count; i++)
                {
                    var w = wordDict[i];
                    if (sIdx + w.Length <= s.Length && s.Substring(sIdx, w.Length) == w)
                    {
                        DFS(sIdx + w.Length, currentString + " " + w);
                    }
                }
            }

            DFS(0, "");

            return result;
        }

        public int CountSentences(string s, IList<string> wordDict)
        {
            // SS: Dynamic Programming, Bottom-Up
            // runtime complexity: O(|s| * |wordDict|)
            // space complexity: O(|s|)

            var dp = new int[s.Length + 1];

            // SS: boundary condition
            dp[^1] = 1;

            for (var i = s.Length - 1; i >= 0; i--)
            {
                foreach (var word in wordDict)
                {
                    if (i + word.Length <= s.Length && s.Substring(i, word.Length) == word)
                    {
                        dp[i] += dp[i + word.Length];
                    }
                }
            }

            return dp[0];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "catsanddog";
                string[] wordDict = {"cat", "cats", "and", "sand", "dog"};

                // Act
                var result = new Solution().WordBreak(s, wordDict);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"cats and dog", "cat sand dog"}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "pineapplepenapple";
                string[] wordDict = {"apple", "pen", "applepen", "pine", "pineapple"};

                // Act
                var result = new Solution().WordBreak(s, wordDict);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"pine apple pen apple", "pineapple pen apple", "pine applepen apple"}, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "catsandog";
                string[] wordDict = {"cats", "dog", "sand", "and", "cat"};

                // Act
                var result = new Solution().WordBreak(s, wordDict);

                // Assert
                Assert.IsEmpty(result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                string[] wordDict = {"a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa", "aaaaaaaa", "aaaaaaaaa", "aaaaaaaaaa"};

                // Act
                var result = new Solution().WordBreak(s, wordDict);

                // Assert
                Assert.IsEmpty(result);
            }

            [Test]
            public void Test12()
            {
                // Arrange
                var s = "catsanddog";
                string[] wordDict = {"cat", "cats", "and", "sand", "dog"};

                // Act
                var result = new Solution().CountSentences(s, wordDict);

                // Assert
                Assert.AreEqual(2, result);
            }

            [Test]
            public void Test22()
            {
                // Arrange
                var s = "pineapplepenapple";
                string[] wordDict = {"apple", "pen", "applepen", "pine", "pineapple"};

                // Act
                var result = new Solution().CountSentences(s, wordDict);

                // Assert
                Assert.AreEqual(3, result);
            }

            [Test]
            public void Test32()
            {
                // Arrange
                var s = "catsandog";
                string[] wordDict = {"cats", "dog", "sand", "and", "cat"};

                // Act
                var result = new Solution().CountSentences(s, wordDict);

                // Assert
                Assert.AreEqual(0, result);
            }

            [Test]
            public void Test42()
            {
                // Arrange
                var s = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaabaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                string[] wordDict = {"a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa", "aaaaaaaa", "aaaaaaaaa", "aaaaaaaaaa"};

                // Act
                var result = new Solution().CountSentences(s, wordDict);

                // Assert
                Assert.AreEqual(0, result);
            }
        }
    }
}