#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace DetectCycleDirectedGraph
{
    public class DetectCycleProblem
    {
        public static bool DetectCycle(Graph g)
        {
            // unvisited vertices
            var whiteSet = new HashSet<int>();

            // vertices that are currently being explored
            var graySet = new HashSet<int>();

            // vertices who have been visited, including its children
            var blackSet = new HashSet<int>();

            // add vertices to white set
            for (var i = 0; i < g.AdjacencyList.Count; i++)
            {
                whiteSet.Add(i);
            }

            var parentMap = new Dictionary<int, int>();

            while (whiteSet.Any())
            {
                var vertex = whiteSet.First();
                var cycle = FindCycle(g, vertex, whiteSet, graySet, blackSet, parentMap);
                if (cycle)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool FindCycle(Graph graph, int vertex, HashSet<int> whiteSet, HashSet<int> graySet
            , HashSet<int> blackSet, Dictionary<int, int> parentMap)
        {
            // DFS
            whiteSet.Remove(vertex);
            graySet.Add(vertex);

            var neighbors = graph.AdjacencyList[vertex];
            foreach (var neighbor in neighbors)
            {
                parentMap[neighbor] = vertex;

                if (blackSet.Contains(neighbor))
                {
                    // already explored
                    continue;
                }

                if (graySet.Contains(neighbor))
                {
                    // we have a cycle
                    return true;
                }

                if (FindCycle(graph, neighbor, whiteSet, graySet, blackSet, parentMap))
                {
                    return true;
                }
            }

            // all neighbors explored
            graySet.Remove(vertex);
            blackSet.Add(vertex);

            return false;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var graph = new Graph();
            graph.AddVertex(0);
            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);

            graph.AddDirectedEdge(0, 1);
            graph.AddDirectedEdge(0, 2);

            graph.AddDirectedEdge(1, 2);

            // cycle: 4 - 5 - 6 - 4
            graph.AddDirectedEdge(3, 0);
            graph.AddDirectedEdge(3, 4);

            graph.AddDirectedEdge(4, 5);

            graph.AddDirectedEdge(5, 3);

            // Act
            var hasCycle = DetectCycleProblem.DetectCycle(graph);

            // Assert
            Assert.IsTrue(hasCycle);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var graph = new Graph();
            graph.AddVertex(0);
            graph.AddVertex(1);

            graph.AddDirectedEdge(0, 1);
            graph.AddDirectedEdge(1, 0);

            // Act
            var hasCycle = DetectCycleProblem.DetectCycle(graph);

            // Assert
            Assert.IsTrue(hasCycle);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var graph = new Graph();
            graph.AddVertex(0);
            graph.AddVertex(1);

            graph.AddDirectedEdge(1, 0);

            // Act
            var hasCycle = DetectCycleProblem.DetectCycle(graph);

            // Assert
            Assert.IsFalse(hasCycle);
        }
    }
}