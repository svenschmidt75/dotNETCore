#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 323: Number of Connected Components in an Undirected Graph
// URL: https://leetcode.com/problems/number-of-connected-components-in-an-undirected-graph/

namespace LeetCode
{
    public class Solution
    {
        public int CountConnectedComponents(int n, IList<IList<int>> edges)
        {
            return CountConnectedComponents1(n, edges);
        }

        private int CountConnectedComponents1(int n, IList<IList<int>> edges)
        {
            // SS: DFS and mark all nodes that can be reached,
            // do until all nodes are marked.
            // runtime complexity: O(V + E)
            // space complexity: O(V) (both visited set and stack)
            
            // SS: create graph
            var adjList = new Dictionary<int, List<int>>();
            for (var i = 0; i < edges.Count; i++)
            {
                var n1 = edges[i][0];
                var n2 = edges[i][1];

                if (adjList.TryGetValue(n1, out var es) == false)
                {
                    es = new List<int>();
                    adjList[n1] = es;
                }

                es.Add(n2);

                if (adjList.TryGetValue(n2, out es) == false)
                {
                    es = new List<int>();
                    adjList[n2] = es;
                }

                es.Add(n1);
            }

            var visited = new HashSet<int>();

            void DFS(int node)
            {
                visited.Add(node);

                // SS: check that the node has neighbors...
                if (adjList.ContainsKey(node) == false)
                {
                    // SS: isolated node
                    return;
                }
                
                var neighbors = adjList[node];
                foreach (var neighbor in neighbors)
                {
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }

                    DFS(neighbor);
                }
            }

            var components = 0;
            for (var i = 0; i < n; i++)
            {
                if (visited.Contains(i))
                {
                    continue;
                }

                components++;
                DFS(i);
            }

            return components;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var edges = new List<IList<int>>
                {
                    new List<int> { 0, 1 }
                    , new List<int> { 1, 2 }
                    , new List<int> { 3, 4 }
                };

                // Act
                var result = new Solution().CountConnectedComponents(5, edges);

                // Assert
                Assert.AreEqual(2, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var edges = new List<IList<int>>
                {
                    new List<int> { 0, 1 }
                    , new List<int> { 1, 2 }
                    , new List<int> { 2, 3 }
                    , new List<int> { 3, 4 }
                };

                // Act
                var result = new Solution().CountConnectedComponents(5, edges);

                // Assert
                Assert.AreEqual(1, result);
            }
        }
    }
}