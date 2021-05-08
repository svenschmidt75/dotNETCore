#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 310. Minimum Height Trees
// URL: https://leetcode.com/problems/minimum-height-trees/

namespace LeetCode
{
    public class Solution
    {
        public IList<int> FindMinHeightTrees(int n, int[][] edges)
        {
//            return FindMinHeightTrees1(n, edges);
            return FindMinHeightTrees2(n, edges);
        }

        private IList<int> FindMinHeightTrees2(int n, int[][] edges)
        {
            var result = new List<int>();

            // SS: create adj. list
            var adjList = new Dictionary<int, HashSet<int>>();
            for (var i = 0; i < n; i++)
            {
                adjList[i] = new HashSet<int>();
            }

            // SS: indegree
            int[] indegree = new int[n];
            
            for (var i = 0; i < edges.Length; i++)
            {
                var edge = edges[i];
                var n1 = edge[0];
                var n2 = edge[1];

                indegree[n1]++;
                indegree[n2]++;
                
                adjList[n1].Add(n2);
                adjList[n2].Add(n1);
            }

            // SS: do BFS starting from the leaf nodes
            var q = new Queue<int>();

            var visited = new int[n];
            int nLeaves = 0;

            for (var i = 0; i < n; i++)
            {
                // SS: Outdegree and indegree are only defined for directed graphs, so we cannot
                // use this here. Instead, a leaf node is one that has only one connection.
                if (indegree[i] == 1)
                {
                    visited[i] = 1;
                    nLeaves++;
                    q.Enqueue(i);
                }
            }

            // SS: peel away the outer leaves and add new leaves
            while (nLeaves < n)
            {
                var leaf = q.Dequeue();
                var neighbors = adjList[leaf];
                foreach (var neighbor in neighbors)
                {
                    if (visited[neighbor] == 1)
                    {
                        // SS: already processed
                        continue;
                    }
                    
                    indegree[neighbor]--;
                    if (indegree[neighbor] == 1)
                    {
                        // SS: is now a leaf node
                        q.Enqueue(neighbor);
                        nLeaves++;
                    }
                }

                visited[leaf] = 1;
            }

            result = q.ToList();
            return result;
        }

        private static IList<int> FindMinHeightTrees1(int n, int[][] edges)
        {
            // SS: from https://www.youtube.com/watch?v=a4hXpeHZ_-c
            // modifies tree
            var result = new List<int>();

            if (edges.Length == 0)
            {
                result.Add(0);
                return result;
            }

            // SS: create adj. list
            var adjList = new Dictionary<int, HashSet<int>>();
            for (var i = 0; i < n; i++)
            {
                adjList[i] = new HashSet<int>();
            }

            for (var i = 0; i < edges.Length; i++)
            {
                var edge = edges[i];
                var n1 = edge[0];
                var n2 = edge[1];

                adjList[n1].Add(n2);
                adjList[n2].Add(n1);
            }

            // SS: do BFS starting from the leaf nodes
            var leaves = new List<int>();

            for (var i = 0; i < n; i++)
            {
                // SS: Outdegree and indegree are only defined for directed graphs, so we cannot
                // use this here. Instead, a leaf node is one that has only one connection.
                if (adjList[i].Count == 1)
                {
                    leaves.Add(i);
                }
            }

            // SS: peel away the outer leaves and add new leaves
            var nodesLeft = n;
            while (nodesLeft > 2)
            {
                nodesLeft -= leaves.Count;

                var newLeaves = new List<int>();

                foreach (var leaf in leaves)
                {
                    var neighbors = adjList[leaf];
                    foreach (var neighbor in neighbors)
                    {
                        // SS: remove edge (neighbor, leaf)
                        adjList[neighbor].Remove(leaf);

                        if (adjList[neighbor].Count == 1)
                        {
                            newLeaves.Add(neighbor);
                        }
                    }
                }

                leaves = newLeaves;
            }

            return leaves;
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] edges = {new[] {1, 0}, new[] {1, 2}, new[] {1, 3}};

                // Act
                var result = new Solution().FindMinHeightTrees(4, edges);

                // Assert
                CollectionAssert.AreEquivalent(new[] {1}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] edges = {new[] {3, 0}, new[] {3, 1}, new[] {3, 2}, new[] {3, 4}, new[] {5, 4}};

                // Act
                var result = new Solution().FindMinHeightTrees(6, edges);

                // Assert
                CollectionAssert.AreEquivalent(new[] {3, 4}, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var edges = new int[0][];

                // Act
                var result = new Solution().FindMinHeightTrees(1, edges);

                // Assert
                CollectionAssert.AreEquivalent(new[] {0}, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[][] edges = {new[] {0, 1}};

                // Act
                var result = new Solution().FindMinHeightTrees(2, edges);

                // Assert
                CollectionAssert.AreEquivalent(new[] {0, 1}, result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[][] edges = {new[] {0, 1}, new[] {1, 2}, new[] {1, 3}, new[] {2, 4}, new[] {3, 5}, new[] {4, 6}};

                // Act
                var result = new Solution().FindMinHeightTrees(7, edges);

                // Assert
                CollectionAssert.AreEquivalent(new[] {1, 2}, result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[][] edges = {new[] {0, 1}, new[] {0, 2}};

                // Act
                var result = new Solution().FindMinHeightTrees(3, edges);

                // Assert
                CollectionAssert.AreEquivalent(new[] {0}, result);
            }
            
        }
    }
}