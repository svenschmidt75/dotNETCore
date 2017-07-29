using System.Collections.Generic;

namespace Djikstra
{
    public class Node
    {
        public string Name { get; }
        public List<Node> Neighbors { get; } = new List<Node>();

        public Node(string name)
        {
            Name = name;
        }

        public void Add(Node neighbor)
        {
            Neighbors.Add(neighbor);
        }
    }
}