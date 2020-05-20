#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace Kruskal
{
    public class Graph
    {
        private readonly Dictionary<int, List<(int, int)>> _adjacencyList = new Dictionary<int, List<(int, int)>>();
        private readonly List<(int vertex1, int vertex2, int weight)> _edgeList = new List<(int, int, int)>();

        public void AddVertex(int vertex)
        {
            _adjacencyList[vertex] = new List<(int, int)>();
        }

        public void AddUndirectedEdge(int vertex1, int vertex2, int weight)
        {
            // SS: only undirected graphs...
            _adjacencyList[vertex1].Add((vertex2, weight));
            _adjacencyList[vertex1].Add((vertex2, weight));

            _edgeList.Add((vertex1, vertex2, weight));
        }

        public (List<(int vertex1, int vertex2)> mst, int mstCost) Kruskal()
        {
            // SS: initialize the union-find DS with each vertex
            var unionFind = UnionFindDS.MakeSet(_adjacencyList.Count);

            // SS: O(E log E)
            var sortedEdges = _edgeList.OrderBy(edge => edge.weight).ToArray();

            var mst = new List<(int vertex1, int vertex2)>();
            var mstCost = 0;

            // SS: O(E)
            foreach (var edge in sortedEdges)
            {
                // SS: O(log V) 
                if (unionFind.Find(edge.vertex1) != unionFind.Find(edge.vertex2))
                {
                    // SS: no cycle, add edge and merge vertices

                    // SS: O(log V)
                    unionFind.Merge(edge.vertex1, edge.vertex2);
                    mst.Add((edge.vertex1, edge.vertex2));
                    mstCost += edge.weight;
                }
            }

            return (mst, mstCost);
        }
    }

    [TestFixture]
    public class KruskalTest
    {
        private static Graph CreateGraph()
        {
            var graph = new Graph();

            graph.AddVertex(0); // A
            graph.AddVertex(1); // B
            graph.AddVertex(2); // C
            graph.AddVertex(3); // D
            graph.AddVertex(4); // E
            graph.AddVertex(5); // F
            graph.AddVertex(6); // G

            // (A, B, 2)
            // (A, C, 6)
            // (A, E, 5)
            // (A, F, 10)
            graph.AddUndirectedEdge(0, 1, 2);
            graph.AddUndirectedEdge(0, 2, 6);
            graph.AddUndirectedEdge(0, 4, 5);
            graph.AddUndirectedEdge(0, 5, 10);

            // (B, D, 3)
            // (B, E, 3)
            graph.AddUndirectedEdge(1, 3, 3);
            graph.AddUndirectedEdge(1, 4, 3);

            // (C, D, 1)
            // (C, F, 5)
            graph.AddUndirectedEdge(2, 3, 1);
            graph.AddUndirectedEdge(2, 5, 2);

            // (D, E, 4)
            // (D, G, 5)
            graph.AddUndirectedEdge(3, 4, 4);
            graph.AddUndirectedEdge(3, 6, 5);

            // (F, G, 5)
            graph.AddUndirectedEdge(5, 6, 5);

            return graph;
        }

        [Test]
        public void TestUdemyGraph()
        {
            // Arrange
            var graph = CreateGraph();

            // Act
            var (mst, mstCost) = graph.Kruskal();

            // Assert
            Assert.AreEqual(16, mstCost);

            // (A, B), (B, E), (C, D), (C, F), (B, D), (D, G)
            CollectionAssert.AreEquivalent(mst, new[] {(0, 1), (2, 3), (2, 5), (1, 4), (1, 3), (3, 6)});
        }
    }
}