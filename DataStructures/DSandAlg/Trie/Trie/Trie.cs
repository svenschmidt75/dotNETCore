using System.Collections.Generic;
using System.Linq;

namespace Trie
{
    public class Trie
    {
        private readonly Node _root = new Node
        {
            Children = new Dictionary<char, Node>(), IsWordBoundary = false, Value = ' '
        };

        public void Insert(string word)
        {
            Insert(word, _root);
        }

        private void Insert(string value, Node node)
        {
            // SS: test for zero-length?
            if (string.IsNullOrWhiteSpace(value)) return;

            var key = value[0];
            Node child;
            if (node.Children.ContainsKey(key))
            {
                child = node.Children[key];
            }
            else
            {
                child = new Node
                {
                    Children = new Dictionary<char, Node>(), IsWordBoundary = false, Value = key
                };
                node.Children[key] = child;
            }

            // SS: the child already exist
            if (value.Length == 1)
                child.IsWordBoundary = true;
            else
                // SS: go deeper
                Insert(value.Substring(1), child);
        }

        public bool Find(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return false;

            var tailNode = GetTailNode(word);
            return tailNode?.IsWordBoundary ?? false;
        }

        private Node GetTailNode(string value)
        {
            var node = _root;
            var idx = 0;
            Node startNode = null;
            while (true)
            {
                Node child = null;
                if (node.Children.ContainsKey(value[idx])) child = node.Children[value[idx]];

                if (child == null) return null;

                if (idx == value.Length - 1)
                {
                    startNode = child;
                    break;
                }

                node = child;
                idx++;
            }

            return startNode;
        }

        public IEnumerable<string> GetWords(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix)) return Enumerable.Empty<string>();

            // SS: from tailNode, find all words
            var tailNode = GetTailNode(prefix);
            if (tailNode == null) return Enumerable.Empty<string>();

            var words = new List<string>();
            GetWordsFromNode(tailNode, prefix, words);
            return words;
        }

        private void GetWordsFromNode(Node node, string prefix, List<string> words)
        {
            // SS: DFS, pre-order traversal
            if (node.IsWordBoundary) words.Add(prefix);

            foreach (var child in node.Children.Values)
            {
                var word = $"{prefix}{child.Value}";
                GetWordsFromNode(child, word, words);
            }
        }
    }
}