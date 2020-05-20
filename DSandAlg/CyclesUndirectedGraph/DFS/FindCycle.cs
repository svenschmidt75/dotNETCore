#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shared;

#endregion

namespace DFS
{
    public static class FindCycle
    {
        public static bool HasCycle(Graph graph)
        {
            var visited = new HashSet<int>();

            var startVertex = graph.AdjacencyList.First().Key;
            visited.Add(startVertex);

            var hasCycle = FindCycleDFS(graph, startVertex, startVertex, visited);

            return hasCycle;
        }

        private static bool FindCycleDFS(Graph graph, int vertex, int fromVertex, HashSet<int> visited)
        {
            var destVertices = graph.AdjacencyList[vertex];
            foreach (var toVertex in destVertices)
            {
                if (visited.Contains(toVertex))
                {
                    // SS: ignore the vertex we came from in cycle detection
                    if (toVertex != fromVertex)
                    {
                        // SS: found cycle
                        return true;
                    }

                    continue;
                }

                visited.Add(toVertex);
                if (FindCycleDFS(graph, toVertex, vertex, visited))
                {
                    // SS: found cycle
                    return true;
                }
            }

            return false;
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