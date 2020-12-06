#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace Kosaraju
{
    public class Solution
    {
        public static int[] Kosaraju(Graph graph)
        {
            // SS: 2-pass algorithm. We first create the transposed graph, that we need for the
            // 2nd pass. We essentially collapse all cycles (strongly connected components)
            // into a node by assigning each nodes in a strongly connected component a representative.
            // The compressed graph is a DAG!
            // Note: A single note can and often is a component in itself! (for example, it has no outgoing connections)

            var adjList = graph.AdjacencyList;

            // create transpose graph
            var transposedGraph = CreateTransposedGraph(graph);


            // 1st pass
            // Note how this step is similar to topological sort... (which doesn't work when
            // the graph has cycles...)
            var visited = new HashSet<int>();
            var stack = new Stack<int>();

            foreach (var vertex in adjList.Keys)
            {
                if (visited.Contains(vertex))
                {
                    continue;
                }

                visited.Add(vertex);
                DFS1(vertex, graph, stack, visited);
            }

            // 2nd pass
            visited = new HashSet<int>();
            var who = new int[adjList.Count];
            while (stack.Any())
            {
                var vertex = stack.Pop();
                if (visited.Contains(vertex))
                {
                    continue;
                }

                visited.Add(vertex);
                DFS2(vertex, vertex, transposedGraph, visited, who);
            }

            // SS: the array who maps each vertex of the graph to it's representative in
            // a strongly connected component...
            return who;
        }

        private static void DFS2(int vertex, int representative, Graph graph, HashSet<int> visited, int[] who)
        {
            who[vertex] = representative;

            var neighbors = graph.AdjacencyList[vertex];
            for (var i = 0; i < neighbors.Count; i++)
            {
                var neighbor = neighbors[i];
                if (visited.Contains(neighbor))
                {
                    continue;
                }

                visited.Add(neighbor);
                DFS2(neighbor, representative, graph, visited, who);
            }
        }

        private static void DFS1(int vertex, Graph graph, Stack<int> stack, HashSet<int> visited)
        {
            var neighbors = graph.AdjacencyList[vertex];
            for (var i = 0; i < neighbors.Count; i++)
            {
                var neighbor = neighbors[i];
                if (visited.Contains(neighbor))
                {
                    continue;
                }

                visited.Add(neighbor);
                DFS1(neighbor, graph, stack, visited);
            }

            stack.Push(vertex);
        }

        private static Graph CreateTransposedGraph(Graph graph)
        {
            // create transpose graph
            var adjList = graph.AdjacencyList;

            var transposedGraph = new Graph();
            foreach (var vertex in adjList.Keys)
            {
                transposedGraph.AddVertex(vertex);
            }

            foreach (var vertex in adjList.Keys)
            {
                var neighbors = adjList[vertex];

                for (var i = 0; i < neighbors.Count; i++)
                {
                    var neighbor = neighbors[i];
                    transposedGraph.AddDirectedEdge(neighbor, vertex);
                }
            }

            return transposedGraph;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var graph1 = new Graph();
            graph1.AddVertex(0);
            graph1.AddVertex(1);
            graph1.AddVertex(2);
            graph1.AddVertex(3);
            graph1.AddVertex(4);
            graph1.AddVertex(5);
            graph1.AddVertex(6);
            graph1.AddVertex(7);
            graph1.AddVertex(8);
            graph1.AddVertex(9);
            graph1.AddVertex(10);

            graph1.AddDirectedEdge(0, 1);

            graph1.AddDirectedEdge(1, 2);
            graph1.AddDirectedEdge(1, 3);

            graph1.AddDirectedEdge(2, 0);

            graph1.AddDirectedEdge(3, 4);

            graph1.AddDirectedEdge(4, 5);

            graph1.AddDirectedEdge(5, 3);

            graph1.AddDirectedEdge(6, 5);
            graph1.AddDirectedEdge(6, 7);

            graph1.AddDirectedEdge(7, 8);

            graph1.AddDirectedEdge(8, 9);

            graph1.AddDirectedEdge(9, 6);
            graph1.AddDirectedEdge(9, 10);

            // Act
            var who = Solution.Kosaraju(graph1);

            // Assert
            Assert.True(who[0] == who[1]);
            Assert.True(who[0] == who[2]);

            Assert.True(who[3] == who[4]);
            Assert.True(who[3] == who[5]);

            Assert.True(who[6] == who[7]);
            Assert.True(who[6] == who[8]);
            Assert.True(who[6] == who[9]);

            Assert.AreEqual(10, who[10]);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var graph2 = new Graph();
            graph2.AddVertex(0);
            graph2.AddVertex(1);
            graph2.AddVertex(2);
            graph2.AddVertex(3);
            graph2.AddVertex(4);
            graph2.AddVertex(5);
            graph2.AddVertex(6);
            graph2.AddVertex(7);
            graph2.AddVertex(8);
            graph2.AddVertex(9);
            graph2.AddVertex(10);
            graph2.AddVertex(11);
            graph2.AddVertex(12);

            graph2.AddDirectedEdge(0, 1);
            graph2.AddDirectedEdge(0, 6);

            graph2.AddDirectedEdge(1, 5);
            graph2.AddDirectedEdge(1, 6);

            graph2.AddDirectedEdge(2, 3);
            graph2.AddDirectedEdge(2, 4);

            graph2.AddDirectedEdge(3, 11);

            graph2.AddDirectedEdge(4, 8);
            graph2.AddDirectedEdge(4, 10);

            graph2.AddDirectedEdge(6, 7);

            graph2.AddDirectedEdge(7, 9);

            graph2.AddDirectedEdge(8, 6);

            graph2.AddDirectedEdge(9, 8);
            graph2.AddDirectedEdge(9, 12);

            graph2.AddDirectedEdge(12, 10);

            // Act
            var who = Solution.Kosaraju(graph2);

            // Assert
            Assert.AreEqual(0, who[0]);
            Assert.AreEqual(1, who[1]);
            Assert.AreEqual(2, who[2]);
            Assert.AreEqual(3, who[3]);
            Assert.AreEqual(4, who[4]);
            Assert.AreEqual(5, who[5]);
            Assert.AreEqual(10, who[10]);
            Assert.AreEqual(11, who[11]);
            Assert.AreEqual(12, who[12]);

            Assert.True(who[6] == who[7]);
            Assert.True(who[6] == who[9]);
            Assert.True(who[6] == who[8]);
        }
    }
}