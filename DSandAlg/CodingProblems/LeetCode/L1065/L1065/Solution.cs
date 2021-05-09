#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: Index Pairs of a String
// URL: https://leetcode.com/problems/index-pairs-of-a-string/

namespace LeetCode
{
    public class Solution
    {
        public IList<int[]> Find(string text, string[] words)
        {
            // return Find1(text, words);
            return Find2(text, words);
        }

        private IList<int[]> Find2(string text, string[] words)
        {
            // SS: runtime complexity: O(#words * w) to construct true
            // search: O(text * w)
            // O(#words * w + text * w)
            // space complexity: O(#words * w)

            // SS: construct trie
            var root = new TrieNode();

            // SS: insert all words into trie
            for (var i = 0; i < words.Length; i++)
            {
                var word = words[i];
                var current = root;
                for (var j = 0; j < word.Length; j++)
                {
                    var c = word[j];
                    if (current.Nodes.TryGetValue(c, out var child))
                    {
                        current = child;
                    }
                    else
                    {
                        var node = new TrieNode();
                        current.Nodes[c] = node;
                        current = node;
                    }
                }

                current.IsWordEnd = true;
            }

            var result = new List<int[]>();

            // SS: search for words
            for (var i = 0; i < text.Length; i++)
            {
                var j = i;
                var current = root;
                while (j < text.Length)
                {
                    var c = text[j];
                    if (current.Nodes.TryGetValue(c, out var child))
                    {
                        if (current.IsWordEnd)
                        {
                            result.Add(new[] {i, j - 1});
                        }

                        current = child;
                        j++;
                    }
                    else
                    {
                        if (current.IsWordEnd)
                        {
                            result.Add(new[] {i, j - 1});
                        }

                        break;
                    }
                }

                if (j == text.Length && current.IsWordEnd)
                {
                    result.Add(new[] {i, j - 1});
                }
            }

            // SS: O(w log w)
            result.Sort((idx1, idx2) =>
            {
                if (idx1[0] == idx2[0])
                {
                    // SS: trie
                    return idx1[1].CompareTo(idx2[1]);
                }

                return idx1[0].CompareTo(idx2[0]);
            });

            return result;
        }

        private IList<int[]> Find1(string text, string[] words)
        {
            // SS: runtime complexity: O(t * w^2 + w log w)
            // space complexity: O(w)
            var result = new List<int[]>();

            // SS: O(w)
            for (var i = 0; i < words.Length; i++)
            {
                var word = words[i];

                // SS: O(t * w)
                var startIdx = 0;
                while (startIdx < word.Length)
                {
                    var idx = text[startIdx..].IndexOf(word);
                    if (idx > -1)
                    {
                        idx += startIdx;
                        result.Add(new[] {idx, idx + word.Length - 1});
                        startIdx = idx + 1;
                    }
                    else
                    {
                        startIdx = word.Length;
                    }
                }
            }

            // SS: O(w log w)
            result.Sort((idx1, idx2) =>
            {
                if (idx1[0] == idx2[0])
                {
                    // SS: trie
                    return idx1[1].CompareTo(idx2[1]);
                }

                return idx1[0].CompareTo(idx2[0]);
            });

            return result;
        }

        private class TrieNode
        {
            public IDictionary<char, TrieNode> Nodes { get; } = new Dictionary<char, TrieNode>();
            public bool IsWordEnd { get; set; }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var results = new Solution().Find("thestoryofleetcodeandme", new[] {"story", "fleet", "leetcode"});

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {3, 7}, new[] {9, 13}, new[] {10, 17}}, results);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var results = new Solution().Find("ababa", new[] {"aba", "ab"});

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {0, 1}, new[] {0, 2}, new[] {2, 3}, new[] {2, 4}}, results);
            }
        }
    }
}