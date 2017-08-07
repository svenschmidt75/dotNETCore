using System.Collections.Generic;

namespace MazeSolver
{
    public class Graph
    {
        private readonly Dictionary<Node, List<Edge>> _nodes = new Dictionary<Node, List<Edge>>();

        public Graph(Node startNode, Node endNode)
        {
            Start = startNode;
            End = endNode;
        }

        public IDictionary<Node, List<Edge>> Nodes => _nodes;

        public Node Start { get; }

        public Node End { get; }

        public IList<Edge> Add(Node node)
        {
            _nodes[node] = new List<Edge>();
            return _nodes[node];
        }

        public IReadOnlyList<Edge> this[Node node] => _nodes[node];
    }
}