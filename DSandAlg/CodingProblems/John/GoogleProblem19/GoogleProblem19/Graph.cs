#region

using System.Collections.Generic;

#endregion

namespace GoogleProblem19
{
    public class Graph
    {
        public IDictionary<int, List<int>> AdjacencyList { get; } = new Dictionary<int, List<int>>();

        public void AddVertex(int vertexId)
        {
            AdjacencyList[vertexId] = new List<int>();
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
    }
}