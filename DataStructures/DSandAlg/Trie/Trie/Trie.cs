using System;
using System.Collections.Generic;

namespace Trie
{
    public class Trie
    {
        private readonly Node _root = new Node
        {
            Children = new Dictionary<char, Node>()
            , IsWordBoundary = false
            , Value = ' '
        };
        
        public void Insert(string word)
        {
            Insert(word, _root);
        }

        private void Insert(string value, Node node)
        {
            // SS: test for zero-length?
            if (String.IsNullOrWhiteSpace(value))
            {
                return;
            }

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
                    Children = new Dictionary<char, Node>()
                    , IsWordBoundary = false
                    , Value = key
                };
                node.Children[key] = child;
            }

            // SS: the child already exist
            if (value.Length == 1)
            {
                child.IsWordBoundary = true;
            }
            else
            {
                // SS: go deeper
                Insert(value.Substring(1), child);
            }
        }

        public bool Find(string word)
        {
            if (String.IsNullOrWhiteSpace(word))
            {
                return false;
            }

            var node = _root;
            int idx = 0;
            while (true)
            {
                Node child = null;
                if (node.Children.ContainsKey(word[idx]))
                {
                    child = node.Children[word[idx]];
                }

                if (child == null)
                {
                    return false;
                }

                if (idx == word.Length - 1)
                {
                    return child.IsWordBoundary;
                }

                node = child;
                idx++;
            }
        }
        
    }
}
