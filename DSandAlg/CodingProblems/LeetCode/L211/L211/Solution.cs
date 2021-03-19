#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 211. Design Add and Search Words Data Structure
// URL: https://leetcode.com/problems/design-add-and-search-words-data-structure/

namespace LeetCode
{
    public class Solution
    {
        public class WordDictionary
        {
            private readonly TrieNode _root = new TrieNode();

            public void AddWord(string word)
            {
                var node = _root;
                for (var i = 0; i < word.Length; i++)
                {
                    var c = word[i];

                    if (node.Node.TryGetValue(c, out var childNode) == false)
                    {
                        childNode = new TrieNode();
                        node.Node[c] = childNode;
                    }

                    if (i == word.Length - 1)
                    {
                        // SS: mark end-of-word
                        childNode.IsEnd = true;
                    }

                    node = childNode;
                }
            }

            public bool Search(string word)
            {
                bool Found(TrieNode node, int i)
                {
                    // SS: check base case -> end-of-word
                    if (i == word.Length)
                    {
                        return node.IsEnd;
                    }

                    var c = word[i];

                    if (c == '.')
                    {
                        // SS: check all paths
                        foreach (var item in node.Node)
                        {
                            var p = item.Key;
                            if (node.Node.TryGetValue(p, out var childNode))
                            {
                                if (Found(childNode, i + 1))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (node.Node.TryGetValue(c, out var childNode))
                        {
                            return Found(childNode, i + 1);
                        }
                    }

                    // SS: word not found
                    return false;
                }

                return Found(_root, 0);
            }

            private class TrieNode
            {
                public IDictionary<char, TrieNode> Node { get; } = new Dictionary<char, TrieNode>();
                public bool IsEnd { get; set; }
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var wordDictionary = new WordDictionary();
                wordDictionary.AddWord("bad");
                wordDictionary.AddWord("dad");
                wordDictionary.AddWord("mad");

                // Act
                // Assert
                Assert.False(wordDictionary.Search("pad"));
                Assert.True(wordDictionary.Search("bad"));
                Assert.True(wordDictionary.Search(".ad"));
                Assert.True(wordDictionary.Search("b.."));
            }
        }
    }
}