#region

using System.Collections.Generic;
using System.Linq;
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
            // return WordBreakDFS(s, wordDict);
            return WordBreak2(s, wordDict);
        }

        public IList<string> WordBreak2(string s, IList<string> wordDict)
        {
            // SS: Larry's solution, https://www.youtube.com/watch?v=JJ574R_w5ro

            // SS: the indices into s are the vertices
            var graph = new Dictionary<int, IList<int>>();
            for (var i = 0; i <= s.Length; i++)
            {
                graph[i] = new List<int>();
            }

            // SS: add edges
            for (var i = 0; i < wordDict.Count; i++)
            {
                var word = wordDict[i];

                for (var j = 0; j < s.Length; j++)
                {
                    if (j + word.Length <= s.Length && s[j..(j + word.Length)] == word)
                    {
                        // SS: add edge, in reverse direction
                        graph[j + word.Length].Add(j);
                    }
                }
            }

            // SS: check whether we can reach the last index
            var reachable = new int[s.Length + 1];
            reachable[0] = 1;
            for (var i = 0; i <= s.Length; i++)
            {
                foreach (var previousVertex in graph[i])
                {
                    reachable[i] |= reachable[previousVertex];
                }
            }

            if (reachable[^1] == 0)
            {
                // SS: the last vertex cannot be reached
                return new List<string>();
            }

            // SS: keep track of the sentences at each vertex
            var sentences = new List<string>[s.Length + 1];
            for (var i = 0; i <= s.Length; i++)
            {
                sentences[i] = new List<string>();
            }

            sentences[0].Add("");

            // SS: for each vertex, check all edges and add their sentences
            for (var i = 0; i <= s.Length; i++)
            {
                foreach (var previousIndex in graph[i])
                {
                    foreach (var sentence in sentences[previousIndex])
                    {
                        sentences[i].Add(sentence + " " + s[previousIndex..i]);
                    }
                }
            }

            return sentences[^1].Select(x => x.TrimStart()).ToList();
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

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "aaaaaaa";
                string[] wordDict = {"aaaa", "aaa"};

                // Act
                var result = new Solution().WordBreak(s, wordDict);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"aaaa aaa", "aaa aaaa"}, result);
            }
        }
    }
}