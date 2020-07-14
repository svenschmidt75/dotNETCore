#region

using System.Collections.Generic;

#endregion

namespace AlgoExpert_AirportConnections
{
    public class Vertex
    {
        public int Index { get; set; }
        public string Name { get; set; }
    }

    public class Graph
    {
        private int _nNodes;

        public Dictionary<Vertex, List<Vertex>> AdjacencyList { get; } = new Dictionary<Vertex, List<Vertex>>();

        public void AddNode(Vertex vertex)
        {
            AdjacencyList[vertex] = new List<Vertex>();
            _nNodes++;
        }

        public void AddUndirectedEdge(Vertex fromNode, Vertex toNode)
        {
            AddEdge(fromNode, toNode);
            AddEdge(toNode, fromNode);
        }

        private void AddEdge(Vertex fromNode, Vertex toNode)
        {
            AdjacencyList[fromNode].Add(toNode);
        }

        public void AddDirectedEdge(Vertex fromNode, Vertex toNode)
        {
            AddEdge(fromNode, toNode);
        }
    }
}