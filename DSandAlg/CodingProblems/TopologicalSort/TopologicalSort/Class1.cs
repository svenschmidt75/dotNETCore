#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace TopologicalSort
{
    public static class Topo
    {
        public static int[] TopologicalSort(Graph g)
        {
            var verticesToVisit = new HashSet<int>();
            var q = new Queue<int>();
            foreach (var p in g.AdjacencyList)
            {
                verticesToVisit.Add(p.Key);
                q.Enqueue(p.Key);
            }

            var stack = new Stack<int>();

            while (q.Any())
            {
                var vertex = q.Dequeue();
                if (verticesToVisit.Contains(vertex) == false)
                {
                    continue;
                }

                Sort(g, vertex, verticesToVisit, stack);
            }

            var orderedVertices = new int[g.AdjacencyList.Count];
            var i = g.AdjacencyList.Count - 1;
            while (stack.Any())
            {
                var vertex = stack.Pop();
                orderedVertices[i] = vertex;
                i--;
            }

            return orderedVertices;
        }

        private static void Sort(Graph graph, int vertex, HashSet<int> verticesToVisit, Stack<int> stack)
        {
            verticesToVisit.Remove(vertex);

            var neighbors = graph.AdjacencyList[vertex];
            foreach (var neighbor in neighbors)
            {
                if (verticesToVisit.Contains(neighbor) == false)
                {
                    continue;
                }

                Sort(graph, neighbor, verticesToVisit, stack);
            }

            // SS: once all outgoing edges have been processes, add vertex to stack
            stack.Push(vertex);
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
            graph.AddVertex(0); // a
            graph.AddVertex(1); // b
            graph.AddVertex(2); // c
            graph.AddVertex(3); // d

            // (a, b)
            // (a, c)
            graph.AddDirectedEdge(0, 1);
            graph.AddDirectedEdge(0, 2);

            // (b, c)
            graph.AddDirectedEdge(1, 2);

            // (c, d)
            graph.AddDirectedEdge(2, 3);

            // Act
            var sortedVertices = Topo.TopologicalSort(graph);

            // Assert
            CollectionAssert.AreEqual(new[] {3, 2, 1, 0}, sortedVertices);
        }
    }
}