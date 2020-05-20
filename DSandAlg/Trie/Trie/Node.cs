using System.Collections.Generic;

namespace Trie
{
    public class Node
    {
        public char Value { get; set; }
        public bool IsWordBoundary { get; set; }
        public Dictionary<char, Node> Children { get; set; }
    }
}