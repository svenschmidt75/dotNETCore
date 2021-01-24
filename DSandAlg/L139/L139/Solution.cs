#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 139. Word Break
// URL: https://leetcode.com/problems/word-break/

namespace LeetCode
{
    public class Solution
    {
        public bool WordBreak(string s, IList<string> wordDict)
        {
            return WordBreak3(s, wordDict);
        }

        private bool WordBreak1(string s, IList<string> wordDict)
        {
            // SS: runtime complexity: O(s^2) in the worst-case
            // space complexity: O(wordDict) 

            var hash = new HashSet<string>(wordDict);

            bool Solve(int idx)
            {
                // SS: recursion base case
                if (idx == s.Length)
                {
                    return true;
                }

                var startIdx = idx;
                var endIdx = idx;
                while (endIdx < s.Length)
                {
                    var word = s[startIdx..(endIdx + 1)];

                    // SS: O(1)
                    if (hash.Contains(word))
                    {
                        // SS: use backtracking
                        if (Solve(endIdx + 1))
                        {
                            return true;
                        }
                    }

                    endIdx++;
                }

                return false;
            }

            var found = Solve(0);
            return found;
        }

        class TrieNode
        {
            public bool IsWordEnd { get; set; }

            public IDictionary<char, TrieNode> Nodes { get; set; } = new Dictionary<char, TrieNode>();
        }

        class Trie
        {
            public TrieNode Root { get; set; } = new TrieNode();

            public void Add(string word)
            {
                TrieNode currentNode = Root;
                int idx = 0;

                while (true)
                {
                    if (idx == word.Length)
                    {
                        currentNode.IsWordEnd = true;
                        break;
                    }

                    char c = word[idx];
                    idx++;

                    if (currentNode.Nodes.ContainsKey(c))
                    {
                        currentNode = currentNode.Nodes[c];
                    }
                    else
                    {
                        var node = new TrieNode();
                        currentNode.Nodes[c] = node;
                        currentNode = node;
                    }
                }
            }
            
        } 
        
        private bool WordBreak2(string s, IList<string> wordDict)
        {
            // SS: build trie
            var trie = new Trie();
            foreach (var word in wordDict)
            {
                trie.Add(word);
            }

            bool Solve(int idx)
            {
                // SS: recursion base case
                if (idx == s.Length)
                {
                    return true;
                }
                
                // SS: check for new word
                TrieNode root = trie.Root;
                
                int i = idx;
                TrieNode node = root;
                while (i < s.Length)
                {
                    char c = s[i];
                    if (node.Nodes.ContainsKey(c) == false)
                    {
                        return false;
                    }

                    node = node.Nodes[c];

                    // SS: use backtracking
                    if (node.IsWordEnd)
                    {
                        if (Solve(i + 1))
                        {
                            return true;
                        }
                    }

                    i++;
                }

                return false;
            }

            var found = Solve(0);
            return found;
        }
        
        private bool WordBreak3(string s, IList<string> wordDict)
        {
            // SS: Using DP, top-down
            // runtime complexity: O(N), each position is done once
            // space complexity: O(N)
            
            var hash = new HashSet<string>(wordDict);

            var dp = new int[s.Length];

            bool Solve(int idx)
            {
                // SS: recursion base case
                if (idx == s.Length)
                {
                    return true;
                }

                if (dp[idx] > 0)
                {
                    return dp[idx] != 1;
                }
                
                // SS: check for new word
                var startIdx = idx;
                var endIdx = idx;
                while (endIdx < s.Length)
                {
                    var word = s[startIdx..(endIdx + 1)];

                    // SS: O(1)
                    if (hash.Contains(word))
                    {
                        // SS: use backtracking
                        if (Solve(endIdx + 1))
                        {
                            dp[idx] = 2;
                            return true;
                        }
                    }

                    endIdx++;
                }

                dp[idx] = 1;
                return false;
            }

            var found = Solve(0);
            return found;
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "leetcode";
                var wordDict = new List<string> {"leet", "code"};

                // Act
                var isSplit = new Solution().WordBreak(s, wordDict);

                // Assert
                Assert.True(isSplit);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "applepenapple";
                var wordDict = new List<string> {"apple", "pen"};

                // Act
                var isSplit = new Solution().WordBreak(s, wordDict);

                // Assert
                Assert.True(isSplit);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "catsandog";
                var wordDict = new List<string> {"cats", "dog", "sand", "and", "cat"};

                // Act
                var isSplit = new Solution().WordBreak(s, wordDict);

                // Assert
                Assert.False(isSplit);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
                var wordDict = new List<string> {"a", "aa", "aaa", "aaaa", "aaaaa", "aaaaaa", "aaaaaaa", "aaaaaaaa", "aaaaaaaaa", "aaaaaaaaaa"};

                // Act
                var isSplit = new Solution().WordBreak(s, wordDict);

                // Assert
                Assert.False(isSplit);
            }
        }
    }
}