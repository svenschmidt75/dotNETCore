#region

using System.Collections.Generic;

#endregion

namespace GoogleProblem23
{
    public class Graph
    {
        public Dictionary<int, List<int>> AdjacencyList { get; } = new Dictionary<int, List<int>>();

        public void AddVertex(int node)
        {
            AdjacencyList[node] = new List<int>();
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