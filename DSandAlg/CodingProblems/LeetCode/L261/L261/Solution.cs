#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 261. Graph Valid Tree
// URL: https://leetcode.com/problems/graph-valid-tree/

namespace LeetCode
{
    public class Solution
    {
        public bool IsValidTree(int n, IList<IList<int>> edges)
        {
            return IsValidTree1(n, edges);
        }

        private bool IsValidTree1(int n, IList<IList<int>> edges)
        {
            // SS: check whether the graph is a tree
            // - no cycles
            // - connected
            // runtime complexity: O(V + E), DFS
            // https://algomonster.medium.com/leetcode-261-graph-valid-tree-f27c212c1db1

            // SS: a tree satisfies this constraint
            if (edges.Count != n - 1)
            {
                return false;
            }

            // SS: create graph
            var adjList = new Dictionary<int, List<int>>();
            foreach (var edge in edges)
            {
                var n1 = edge[0];
                var n2 = edge[1];

                if (adjList.TryGetValue(n1, out var neighbors) == false)
                {
                    neighbors = new List<int>();
                    adjList[n1] = neighbors;
                }

                neighbors.Add(n2);

                if (adjList.TryGetValue(n2, out neighbors) == false)
                {
                    neighbors = new List<int>();
                    adjList[n2] = neighbors;
                }

                neighbors.Add(n1);
            }


            // SS: check whether the graph is connected
            // and has no cycles
            var blackSet = new HashSet<int>();
            var graySet = new HashSet<int>();

            bool DFS(int node, int parent)
            {
                graySet.Add(node);

                var neighbors = adjList[node];
                foreach (var n2 in neighbors)
                {
                    if (n2 == parent)
                    {
                        // SS: ignore, since we came from this node, so
                        // this is not a cycle!
                        continue;
                    }

                    if (blackSet.Contains(n2))
                    {
                        // SS: already visited
                        continue;
                    }

                    if (graySet.Contains(n2))
                    {
                        // SS: we have a cycle
                        return false;
                    }

                    if (DFS(n2, node) == false)
                    {
                        return false;
                    }
                }

                graySet.Remove(node);
                blackSet.Add(node);

                return true;
            }

            // SS: if the graph is connected, and since it is undirected,
            // we can reach every node from any other node...
            if (DFS(0, -1) == false)
            {
                return false;
            }

            return blackSet.Count == n;
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
                    , new List<int> { 0, 2 }
                    , new List<int> { 0, 3 }
                    , new List<int> { 1, 4 }
                };

                // Act
                var result = new Solution().IsValidTree(5, edges);

                // Assert
                Assert.True(result);
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
                    , new List<int> { 1, 3 }
                    , new List<int> { 1, 4 }
                };

                // Act
                var result = new Solution().IsValidTree(5, edges);

                // Assert
                Assert.False(result);
            }
        }
    }
}