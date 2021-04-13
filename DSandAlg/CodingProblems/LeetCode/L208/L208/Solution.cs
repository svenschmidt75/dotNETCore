#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 208. Implement Trie (Prefix Tree)
// URL: https://leetcode.com/problems/implement-trie-prefix-tree/

namespace LeetCode
{
    public class Trie
    {
        private readonly TrieNode _root = new TrieNode();

        /**
         * Inserts a word into the trie.
         */
        public void Insert(string word)
        {
            var node = _root;
            var idx = 0;
            while (idx < word.Length)
            {
                var c = word[idx++];
                if (node._nodes.ContainsKey(c) == false)
                {
                    node._nodes[c] = new TrieNode();
                }

                node = node._nodes[c];
            }

            node.WordEnd = true;
        }

        /**
         * Returns if the word is in the trie.
         */
        public bool Search(string word)
        {
            var node = ContainsPrefix(word);
            return node != null && node.WordEnd;
        }

        /**
         * Returns if there is any word in the trie that starts with the given prefix.
         */
        public bool StartsWith(string prefix)
        {
            var node = ContainsPrefix(prefix);
            return node != null;
        }

        private TrieNode ContainsPrefix(string prefix)
        {
            var node = _root;
            var idx = 0;
            while (idx < prefix.Length)
            {
                var c = prefix[idx++];
                if (node._nodes.ContainsKey(c) == false)
                {
                    return null;
                }

                node = node._nodes[c];
            }

            return node;
        }

        private class TrieNode
        {
            internal readonly IDictionary<char, TrieNode> _nodes = new Dictionary<char, TrieNode>();
            internal bool WordEnd { get; set; }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var trie = new Trie();

                // Act
                // Assert
                trie.Insert("apple");

                Assert.True(trie.Search("apple"));
                Assert.False(trie.Search("app"));
                Assert.True(trie.StartsWith("app"));

                trie.Insert("app");
                Assert.True(trie.Search("app"));
            }
        }
    }
}