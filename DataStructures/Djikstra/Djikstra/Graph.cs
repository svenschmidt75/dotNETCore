using System.Collections.Generic;
using System.Dynamic;

namespace Djikstra
{
    public class Graph
    {
        private readonly Dictionary<Node, List<Edge>> _nodes = new Dictionary<Node, List<Edge>>();

        public IDictionary<Node, List<Edge>> Nodes => _nodes;

        public Node Start { get; }

        public Node End { get; }

        public IList<Edge> Add(Node node)
        {
            _nodes[node] = new List<Edge>();
            return _nodes[node];
        }
    }
}