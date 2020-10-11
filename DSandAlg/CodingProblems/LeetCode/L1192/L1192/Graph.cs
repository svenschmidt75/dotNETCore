#region

using System.Collections.Generic;

#endregion

namespace L1192
{
    public class Graph
    {
        private int _nNodes;

        public Dictionary<int, List<int>> AdjacencyList { get; } = new Dictionary<int, List<int>>();

        public void AddNode(int node)
        {
            AdjacencyList[node] = new List<int>();
            _nNodes++;
        }

        public void AddUndirectedEdge(int fromNode, int toNode)
        {
            AddEdge(fromNode, toNode);
            AddEdge(toNode, fromNode);
        }

        private void AddEdge(int fromNode, int toNode)
        {
            AdjacencyList[fromNode].Add(toNode);
        }

        public void AddDirectedEdge(int fromNode, int toNode)
        {
            AddEdge(fromNode, toNode);
        }
    }
}