#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace LeetCode17
{
    public class Solution2
    {
        private readonly string[] _letters =
        {
            "" // 0
            , "" // 1
            , "abc" // 2
            , "def" // 3
            , "ghi" // 4
            , "jkl" // 5
            , "mno" // 6
            , "pqrs" // 7
            , "tuv" // 8
            , "wxyz" // 9
        };

        public IList<string> LetterCombinations(string digits)
        {
            if (string.IsNullOrEmpty(digits))
            {
                return new List<string>();
            }

            // construct trie
            var root = ConstructTrie(new TrieNode(), digits, 0);

            IList<string> result = new List<string>();

            var cArr = new char[digits.Length];
            WalkTrie(root, cArr, 0, result);

            return result;
        }

        private void WalkTrie(TrieNode node, char[] cArr, int cArrPos, IList<string> result)
        {
            if (node.Nodes.Any() == false)
            {
                // leaf node
                var s = new string(cArr);
                result.Add(s);
            }
            else
            {
                foreach (var trieNode in node.Nodes)
                {
                    var c = trieNode.Key;
                    cArr[cArrPos] = c;
                    WalkTrie(trieNode.Value, cArr, cArrPos + 1, result);
                }
            }
        }

        private TrieNode ConstructTrie(TrieNode node, string digits, int pos)
        {
            if (pos == digits.Length)
            {
                return node;
            }

            var d = digits[pos] - '0';
            var letters = _letters[d];

            for (var i = 0; i < letters.Length; i++)
            {
                var l = letters[i];
                node.Nodes[l] = ConstructTrie(new TrieNode(), digits, pos + 1);
            }

            return node;
        }

        private class TrieNode
        {
            public IDictionary<char, TrieNode> Nodes { get; } = new Dictionary<char, TrieNode>();
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var digits = "23";

                // Act
                var result = new Solution2().LetterCombinations(digits);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var digits = "";

                // Act
                var result = new Solution2().LetterCombinations(digits);

                // Assert
                Assert.IsEmpty(result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var digits = "2";

                // Act
                var result = new Solution2().LetterCombinations(digits);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"a", "b", "c"}, result);
            }
        }
    }
}