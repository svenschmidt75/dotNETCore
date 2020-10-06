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

                visited.Add(i);

                var stack = new Stack<int>();
                stack.Push(i);
                var stackSet = new HashSet<int> {i};

                DFS(g, i, visited, stack, stackSet, vertex2Scc);
            }
        }

        private void DFS(Graph g, int vertex, HashSet<int> visited, Stack<int> stack, HashSet<int> stackSet, int[] vertex2Scc)
        {
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
                        // SS: we found a SCC

                        // propagate smallest low-link value
                        var lowLink = vertex2Scc[vertex];
                        var otherLowLink = vertex2Scc[toVertex];
                        vertex2Scc[vertex] = Math.Min(lowLink, otherLowLink);
                        break;
                    }

                    continue;
                }

                visited.Add(toVertex);

                stack.Push(toVertex);
                stackSet.Add(toVertex);

                DFS(g, toVertex, visited, stack, stackSet, vertex2Scc);

                var lowLink2 = vertex2Scc[vertex];
                var otherLowLink2 = vertex2Scc[toVertex];
                vertex2Scc[vertex] = Math.Min(lowLink2, otherLowLink2);
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
            private Graph Create()
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
                var g = Create();

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
        }
    }
}