using System.Collections.Generic;

namespace Graph
{
    public class Graph
    {
        private readonly Dictionary<int, List<(int, int)>> _adjacencyList = new Dictionary<int, List<(int, int)>>();
        private int _nNodes = 0;

        public void AddNode(int node)
        {
            _adjacencyList[node] = new List<(int, int)>();
            _nNodes++;
        }

        public void AddUndirectedEdge(int fromNode, int toNode, int weight = 0)
        {
            AddEdge(fromNode, toNode, weight);
            AddEdge(toNode, fromNode, weight);
        }

        private void AddEdge(int fromNode, int toNode, int weight)
        {
            _adjacencyList[fromNode].Add((toNode, weight));
        }

        public void AddDirectedEdge(int fromNode, int toNode, int weight)
        {
            AddEdge(fromNode, toNode, weight);
        }
    }
}