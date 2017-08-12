using System.Collections.Generic;

namespace MazeSolver
{
    public class Node
    {
        public string Name { get; }
        public List<Node> Neighbors { get; } = new List<Node>();
        public int DistanceToEnd { get; }

        public Node(string name, int distanceToEnd)
        {
            Name = name;
            DistanceToEnd = distanceToEnd;
        }
    }
}