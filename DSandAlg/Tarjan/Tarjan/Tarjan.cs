#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace Tarjan
{
    public class Tarjan
    {
        public int[] FindSCCs(Graph g)
        {
            // SS: find SCCs (Strongly Connected Components) in (directed) graph
            var vertex2Scc = new int[g.AdjacencyList.Count];
            FindSCCs(g, vertex2Scc, new HashSet<int>());
            return vertex2Scc;
        }

        private void FindSCCs(Graph g, int[] vertex2Scc, HashSet<int> visited)
        {
            for (var i = 0; i < g.AdjacencyList.Count; i++)
            {
                if (visited.Contains(i))
                {
                    // vertex has already been visited
                    continue;
                }

                var stack = new Stack<int>();
                var stackSet = new HashSet<int>();
                DFS(g, i, visited, stack, stackSet, vertex2Scc);
            }
        }

        private void DFS(Graph g, int vertex, HashSet<int> visited, Stack<int> stack, HashSet<int> stackSet, int[] vertex2Scc)
        {
            visited.Add(vertex);
            stack.Push(vertex);
            stackSet.Add(vertex);

            // SS: set initial low-link value
            vertex2Scc[vertex] = vertex;

            var neighbors = g.AdjacencyList[vertex];
            for (var i = 0; i < neighbors.Count; i++)
            {
                var toVertex = neighbors[i];

                if (visited.Contains(toVertex))
                {
                    // SS: update low-link value?
                    if (stackSet.Contains(toVertex))
                    {
                        // SS: we found an SCC

                        // propagate smallest low-link value
                        vertex2Scc[vertex] = Math.Min(vertex2Scc[vertex], vertex2Scc[toVertex]);
                        break;
                    }

                    continue;
                }

                DFS(g, toVertex, visited, stack, stackSet, vertex2Scc);

                vertex2Scc[vertex] = Math.Min(vertex2Scc[vertex], vertex2Scc[toVertex]);
            }

            // SS: beginning of SCC?
            if (vertex2Scc[vertex] == vertex)
            {
                // SS: remove all vertices that belong to the SCC
                while (stack.Any())
                {
                    var v = stack.Peek();
                    if (vertex2Scc[v] == vertex)
                    {
                        stack.Pop();
                        stackSet.Remove(v);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }


        [TestFixture]
        public class Tests
        {
            private Graph Create1()
            {
                // SS: from Tarjan's Strongly Connected Component (SCC) Algorithm (UPDATED) | Graph Theory
                // https://www.youtube.com/watch?v=wUgWX0nc4NY&list=PLDV1Zeh2NRsDGO4--qE8yH72HFL1Km93P&index=23
                // 7:57 into video

                var g = new Graph();
                g.AddNode(0);
                g.AddNode(1);
                g.AddNode(2);
                g.AddNode(3);
                g.AddNode(4);
                g.AddNode(5);
                g.AddNode(6);
                g.AddNode(7);

                g.AddDirectedEdge(0, 1);

                g.AddDirectedEdge(1, 2);

                g.AddDirectedEdge(2, 0);

                g.AddDirectedEdge(3, 4);
                g.AddDirectedEdge(3, 7);

                g.AddDirectedEdge(4, 5);

                g.AddDirectedEdge(5, 0);
                g.AddDirectedEdge(5, 6);

                g.AddDirectedEdge(6, 0);
                g.AddDirectedEdge(6, 2);
                g.AddDirectedEdge(6, 4);

                g.AddDirectedEdge(7, 3);
                g.AddDirectedEdge(7, 5);

                return g;
            }

            [Test]
            public void Test1()
            {
                // Arrange
                var g = Create1();

                // Act
                var sccs = new Tarjan().FindSCCs(g);

                // Assert
                Assert.AreEqual(0, sccs[0]);
                Assert.AreEqual(0, sccs[1]);
                Assert.AreEqual(0, sccs[2]);

                Assert.AreEqual(4, sccs[4]);
                Assert.AreEqual(4, sccs[5]);
                Assert.AreEqual(4, sccs[6]);

                Assert.AreEqual(3, sccs[3]);
                Assert.AreEqual(3, sccs[7]);
            }

            [Test]
            public void TwoCircles()
            {
                // Arrange
                var g = new Graph();
                g.AddNode(0);
                g.AddNode(1);
                g.AddNode(2);
                g.AddNode(3);
                g.AddNode(4);
                g.AddNode(5);

                g.AddDirectedEdge(0, 1);

                g.AddDirectedEdge(1, 2);
                g.AddDirectedEdge(1, 4);

                g.AddDirectedEdge(2, 3);

                g.AddDirectedEdge(3, 0);

                g.AddDirectedEdge(4, 5);

                g.AddDirectedEdge(5, 0);

                // Act
                var sccs = new Tarjan().FindSCCs(g);

                // Assert
                Assert.AreEqual(0, sccs[0]);
                Assert.AreEqual(0, sccs[1]);
                Assert.AreEqual(0, sccs[2]);
                Assert.AreEqual(0, sccs[3]);
                Assert.AreEqual(0, sccs[4]);
                Assert.AreEqual(0, sccs[5]);
            }
        }
    }
}