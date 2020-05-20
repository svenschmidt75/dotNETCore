#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shared;

#endregion

namespace DisjointSet
{
    public static class FindCycle
    {
        public static bool HasCycle(Graph graph)
        {
            var nVertices = graph.AdjacencyList.Count;

            // SS: create disjoint set DS
            var ds = DisjointSet.MakeSet(nVertices);

            // SS: let's extract edges, although this is not necessary...
            var edges = GetEdges(graph);

            foreach (var edge in edges)
            {
                var root1 = ds.Find(edge.Item1);
                var root2 = ds.Find(edge.Item2);
                if (root1 == root2)
                {
                    // SS: both vertices are in the same set, so adding this edge
                    // would form a cycle.
                    return true;
                }

                ds.Merge(root1, root2);
            }

            return false;
        }

        private static List<(int start, int end)> GetEdges(Graph graph)
        {
            var edges = new HashSet<(int, int)>();

            var adjacencyList = graph.AdjacencyList;
            foreach (var vertexItem in adjacencyList)
            {
                var vertex = vertexItem.Key;
                var es = vertexItem.Value;
                foreach (var edge in es)
                {
                    if (edges.Contains((vertex, edge)) || edges.Contains((edge, vertex)))
                    {
                        continue;
                    }

                    edges.Add((vertex, edge));
                }
            }

            return edges.ToList();
        }
    }


    [TestFixture]
    public class HasCycleTest
    {
        private static Graph CreateGraphWithCycle()
        {
            var graph = new Graph();
            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            // A, B
            // A, F
            graph.AddUndirectedEdge(0, 1);
            graph.AddUndirectedEdge(0, 5);

            // B, C
            // B, E
            graph.AddUndirectedEdge(1, 2);
            graph.AddUndirectedEdge(1, 4);

            // C, D
            graph.AddUndirectedEdge(2, 3);

            // D, E
            graph.AddUndirectedEdge(3, 4);

            return graph;
        }

        private static Graph CreateGraphWithoutCycle()
        {
            var graph = new Graph();
            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            // A, B
            // A, F
            graph.AddUndirectedEdge(0, 1);
            graph.AddUndirectedEdge(0, 5);

            // B, C
            // B, E
            graph.AddUndirectedEdge(1, 2);
            graph.AddUndirectedEdge(1, 4);

            // C, D
            graph.AddUndirectedEdge(2, 3);

            return graph;
        }

        [Test]
        public void TestWithCycle()
        {
            // Arrange
            var graph = CreateGraphWithCycle();

            // Act
            var hasCycle = FindCycle.HasCycle(graph);

            // Assert
            Assert.IsTrue(hasCycle);
        }

        [Test]
        public void TestWithoutCycle()
        {
            // Arrange
            var graph = CreateGraphWithoutCycle();

            // Act
            var hasCycle = FindCycle.HasCycle(graph);

            // Assert
            Assert.IsFalse(hasCycle);
        }
    }
}