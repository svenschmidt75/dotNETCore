#region

using System.Collections.Generic;

#endregion

namespace GoogleProblem17
{
    public class Graph
    {
        public IDictionary<int, HashSet<int>> AdjacencyList { get; } = new Dictionary<int, HashSet<int>>();

        public void AddVertex(int vertexId)
        {
            if (AdjacencyList.ContainsKey(vertexId) == false)
            {
                AdjacencyList[vertexId] = new HashSet<int>();
            }
        }

        public void AddDirectedEdge(int fromVertexId, int toVertexId)
        {
            AdjacencyList[fromVertexId].Add(toVertexId);
        }


        public void AddUndirectedEdge(int fromVertexId, int toVertexId)
        {
            AddDirectedEdge(fromVertexId, toVertexId);
            AddDirectedEdge(toVertexId, fromVertexId);
        }

        public bool ContainsEdge(int v1, int v2)
        {
            if (AdjacencyList.ContainsKey(v1))
            {
                return AdjacencyList[v1].Contains(v2);
            }

            return false;
        }
    }
}