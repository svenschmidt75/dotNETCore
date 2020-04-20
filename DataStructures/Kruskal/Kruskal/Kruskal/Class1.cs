using System;
using System.Collections.Generic;

namespace Kruskal
{
    public class Graph
    {
        private Dictionary<int, List<(int, int)>> _adjacencyList = new Dictionary<int, List<(int, int)>>();
        
        public void AddVertex(int vertex)
        {
            _adjacencyList[vertex] = new List<(int, int)>();
        }

        public void AddDirectedEdge(int vertex1, int vertex2, int weight)
        {
            _adjacencyList[vertex1].Add((vertex2, weight));
        }

        public void AddUndirectedEdge(int vertex1, int vertex2, int weight)
        {
            AddDirectedEdge(vertex1, vertex2, weight);
            AddDirectedEdge(vertex2, vertex1, weight);
        }

        public void Kruskal()
        {
            
            
            
        }
        
        
    }
}