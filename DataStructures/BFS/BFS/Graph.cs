using System.Collections.Generic;

namespace Djikstra
{
    public class Graph
    {
        private readonly Dictionary<Node, List<Edge>> _nodes = new Dictionary<Node, List<Edge>>();

        public IDictionary<Node, List<Edge>> Nodes => _nodes;

        public IList<Edge> Add(Node node)
        {
            _nodes[node] = new List<Edge>();
            return _nodes[node];
        }

        public IReadOnlyList<Edge> this[Node node] => _nodes[node];
    }
}