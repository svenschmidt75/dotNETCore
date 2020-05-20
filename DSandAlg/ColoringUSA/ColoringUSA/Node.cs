using System.Collections.Generic;

namespace Djikstra
{
    public class Node
    {
        public string Name { get; }
        public List<Node> Neighbors { get; } = new List<Node>();
        public int Color { get; set; }

        public Node(string name)
        {
            Name = name;
            Color = -1;
        }

        public void Add(Node neighbor)
        {
            Neighbors.Add(neighbor);
        }
    }
}