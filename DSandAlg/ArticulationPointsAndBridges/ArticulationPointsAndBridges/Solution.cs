using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ArticulationPointsAndBridges
{
    public class Solution
    {
        private int _time;
        
        public int[] GetArticulationPoints(Graph g)
        {
            var visited = new HashSet<int>();
            int[] disc = new int[g.AdjacencyList.Count];
            int[] low = new int[g.AdjacencyList.Count];
            int[] ap = new int[g.AdjacencyList.Count];

            int[] parent = new int[g.AdjacencyList.Count];
            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = -1;
            }

            // SS: discovery time for the first vertex
            _time = 1;
            
            for (int i = 0; i < g.AdjacencyList.Count; i++)
            {
                if (visited.Contains(i))
                {
                    continue;
                }
                
                DFS(g, i, visited, disc, low, parent, ap);
            }
            
            return ap;
        }

        private void DFS(Graph g, int vertex, HashSet<int> visited, int[] disc, int[] low, int[] parent, int[] ap)
        {
            visited.Add(vertex);

            // SS: record discovery time
            disc[vertex] = _time;
            low[vertex] = _time;

            _time++;

            var neighbors = g.AdjacencyList[vertex];
            for (int i = 0; i < neighbors.Count; i++)
            {
                var toVertex = neighbors[i];
                if (visited.Contains(toVertex))
                {
                    if (g.AdjacencyList[vertex].Contains(toVertex) == false)
                    {
                        // SS: record the earliest time we can reach from vertex
                        low[vertex] = Math.Min(low[vertex], disc[toVertex]);
                    }
                    continue;
                }

                parent[toVertex] = vertex;
                
                DFS(g, toVertex, visited, disc, low, parent, ap);
                if (low[toVertex] >= disc[vertex])
                {
                    // SS: toVertex does not have a path to an ancestor of vertex,
                    // so removing vertex would increase the number of connected
                    // components. Hence, vertex is an articulation point.
                    ap[vertex] = 1;
                }
            }
        }

        [TestFixture]
        public class Tests
        {
            private static Graph CreateGraph1()
            {
                // SS: from https://www.hackerearth.com/practice/algorithms/graphs/articulation-points-and-bridges/tutorial/
                
                var g = new Graph();
                g.AddNode(0);
                g.AddNode(1);
                g.AddNode(2);
                g.AddNode(3);
                g.AddNode(4);
                g.AddNode(5);
                
                g.AddUndirectedEdge(0, 1);
                g.AddUndirectedEdge(0, 5);
                
                g.AddUndirectedEdge(1, 2);
                g.AddUndirectedEdge(1, 3);
                
                g.AddUndirectedEdge(2, 3);
                g.AddUndirectedEdge(2, 4);

                g.AddUndirectedEdge(3, 4);

                return g;
            }

            [Test]
            public void Test1()
            {
                // Arrange
                var g = CreateGraph1();
                
                // Act
                var ap = new Solution().GetArticulationPoints(g);
                
                // Assert
            }
        }
    }
}