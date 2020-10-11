#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// https://leetcode.com/problems/critical-connections-in-a-network/

namespace L1192
{
    public class Solution
    {
        private int _time;

        public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            // SS: runtime complexity: O(E + V)
            
            // SS: create graph
            var g = new Graph();
            for (var i = 0; i < n; i++)
            {
                g.AddNode(i);
            }

            for (var i = 0; i < connections.Count; i++)
            {
                var (node1, node2) = (connections[i][0], connections[i][1]);
                g.AddUndirectedEdge(node1, node2);
            }

            var bridges = FindBridges(g);

            IList<IList<int>> result = new List<IList<int>>();
            for (var i = 0; i < bridges.Count; i++)
            {
                var (node1, node2) = bridges[i];
                result.Add(new List<int> {node1, node2});
            }

            return result;
        }

        public List<(int u, int v)> FindBridges(Graph g)
        {
            var visited = new HashSet<int>();
            var disc = new int[g.AdjacencyList.Count];
            var low = new int[g.AdjacencyList.Count];

            var bridges = new List<(int u, int v)>();

            var parent = new int[g.AdjacencyList.Count];
            for (var i = 0; i < parent.Length; i++)
            {
                parent[i] = -1;
            }

            // SS: discovery time for the first vertex
            _time = 1;

            for (var i = 0; i < g.AdjacencyList.Count; i++)
            {
                if (visited.Contains(i))
                {
                    continue;
                }

                BridgeDFS(g, i, visited, disc, low, parent, bridges);
            }

            return bridges;
        }

        private void BridgeDFS(Graph g, int vertex, HashSet<int> visited, int[] disc, int[] low, int[] parent, List<(int u, int v)> bridges)
        {
            visited.Add(vertex);

            // SS: record discovery time
            disc[vertex] = _time;
            low[vertex] = _time;

            _time++;

            var neighbors = g.AdjacencyList[vertex];
            for (var i = 0; i < neighbors.Count; i++)
            {
                var toVertex = neighbors[i];
                if (visited.Contains(toVertex))
                {
                    // SS: If we have a connection to the parent, we don't want to
                    // update the low value...
                    if (parent[vertex] != toVertex)
                    {
                        // SS: record the earliest discovered vertex we can reach from 'vertex'
                        low[vertex] = Math.Min(low[vertex], disc[toVertex]);
                    }
                }
                else
                {
                    parent[toVertex] = vertex;

                    BridgeDFS(g, toVertex, visited, disc, low, parent, bridges);

                    // SS: the parent's low value cannot be larger than the minimum low value
                    // off all its children
                    low[vertex] = Math.Min(low[vertex], low[toVertex]);

                    if (low[toVertex] > disc[vertex])
                    {
                        // SS: toVertex does not have a path to an either vertex or an ancestor of vertex,
                        // so removing edge (toVertex, vertex) would increase the number of connected components.
                        // Hence, edge (vertex, toVertex) is a bridge.
                        bridges.Add((vertex, toVertex));
                    }
                }
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var n = 4;
                var connections = new List<IList<int>>
                {
                    new List<int> {0, 1}
                    , new List<int> {1, 2}
                    , new List<int> {2, 0}
                    , new List<int> {1, 3}
                };

                // Act
                var bridges = new Solution().CriticalConnections(n, connections);

                // Assert
                Assert.AreEqual(1, bridges.Count);
                Assert.AreEqual(1, bridges[0][0]);
                Assert.AreEqual(3, bridges[0][1]);
            }
        }
    }
}