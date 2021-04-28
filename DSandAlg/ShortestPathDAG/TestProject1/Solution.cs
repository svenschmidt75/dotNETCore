#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: Shortest Path for a DAG
// URL:

namespace LeetCode
{
    public class Solution
    {
        public int ShortestPath(IDictionary<int, IList<(int v, int w)>> adjList, int from, int to)
        {
            // SS: calculate the shortest path in a DAG.
            // We do not construct the path itself, just the distance.

            var topoOrdering = TopologicalSort(adjList);

            var dist = new long[adjList.Count];
            for (var i = 0; i < dist.Length; i++)
            {
                dist[i] = int.MaxValue;
            }

            // SS: distance from from to itself is 0
            dist[from] = 0;

            for (var i = 0; i < topoOrdering.Length; i++)
            {
                var node = topoOrdering[i];

                // SS: relax edges for node i
                var neighbors = adjList[node];
                foreach (var neighbor in neighbors)
                {
                    var d = dist[node] + neighbor.w;
                    if (d < dist[neighbor.v])
                    {
                        dist[neighbor.v] = d;
                    }
                }
            }

            return (int) dist[to];
        }

        private static int[] TopologicalSort(IDictionary<int, IList<(int v, int w)>> adjList)
        {
            // SS: topological sort
            var stack = new Stack<int>();
            var visited = new HashSet<int>();

            void DFS(int node)
            {
                var neighbors = adjList[node];
                foreach (var neighbor in neighbors)
                {
                    if (visited.Contains(neighbor.v))
                    {
                        continue;
                    }

                    visited.Add(neighbor.v);
                    DFS(neighbor.v);
                }

                // SS: all neighbors have been visited
                stack.Push(node);
            }

            for (var i = 0; i < adjList.Count; i++)
            {
                if (visited.Contains(i))
                {
                    continue;
                }

                visited.Add(i);
                DFS(i);
            }

            var ordering = new int[stack.Count];
            var j = 0;
            while (stack.Any())
            {
                ordering[j++] = stack.Pop();
            }

            return ordering;
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var adjList = new Dictionary<int, IList<(int v, int w)>>
                {
                    {0, new List<(int v, int w)> {(1, 3), (2, 6)}} // A
                    , {1, new List<(int v, int w)> {(4, 11), (3, 4), (2, 4)}} // B
                    , {2, new List<(int v, int w)> {(3, 8), (6, 11)}} // C
                    , {3, new List<(int v, int w)> {(4, -4), (5, 5), (6, 2)}} // D
                    , {4, new List<(int v, int w)> {(7, 9)}} // E
                    , {5, new List<(int v, int w)> {(7, 1)}} // F
                    , {6, new List<(int v, int w)> {(7, 2)}} // G
                    , {7, new List<(int v, int w)>()} // H
                };

                // Act
                var dist = new Solution().ShortestPath(adjList, 0, 7);

                // Assert
                Assert.AreEqual(11, dist);
            }

            [TestCase(0, int.MaxValue)]
            [TestCase(1, 0)]
            [TestCase(2, 2)]
            [TestCase(3, 6)]
            [TestCase(4, 5)]
            [TestCase(5, 3)]
            public void Test2(int to, int expected)
            {
                // Arrange
                var adjList = new Dictionary<int, IList<(int v, int w)>>
                {
                    {0, new List<(int v, int w)> {(1, 5), (2, 3)}} // r
                    , {1, new List<(int v, int w)> {(3, 6), (2, 2)}} // s
                    , {2, new List<(int v, int w)> {(4, 4), (5, 2), (3, 7)}} // t
                    , {3, new List<(int v, int w)> {(4, -1)}} // x
                    , {4, new List<(int v, int w)> {(5, -2)}} // y
                    , {5, new List<(int v, int w)>()} // z
                };

                // Act
                var dist = new Solution().ShortestPath(adjList, 1, to);

                // Assert
                Assert.AreEqual(expected, dist);
            }
        }
    }
}