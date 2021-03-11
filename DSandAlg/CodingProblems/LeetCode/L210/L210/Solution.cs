#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 210. Course Schedule II
// URL: https://leetcode.com/problems/course-schedule-ii/

namespace LeetCode
{
    public class Solution
    {
        public int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            // SS: topological sort, i.e. O(V + E) runtime complexity
            // space complexity: adjacency list, depends on density of graph. Worst case: O(V^2)

            // SS: create adjacency list
            var adjList = new Dictionary<int, IList<int>>();
            for (var i = 0; i < numCourses; i++)
            {
                adjList[i] = new List<int>();
            }

            // SS: add directed edges
            for (var i = 0; i < prerequisites.Length; i++)
            {
                var edge = prerequisites[i];
                var fromVertex = edge[0];
                var toVertex = edge[1];
                adjList[fromVertex].Add(toVertex);
            }

            if (HasCycle(adjList))
            {
                // SS: no solution is possible
                return new int[0];
            }

            var stack = new Stack<int>();

            var visited = new HashSet<int>();

            void TopologicalSort(int vertex)
            {
                var edges = adjList[vertex];
                foreach (var targetVertex in edges)
                {
                    if (visited.Contains(targetVertex))
                    {
                        continue;
                    }

                    visited.Add(targetVertex);
                    TopologicalSort(targetVertex);
                }

                // SS: all dependencies of vertex have been satisfied
                stack.Push(vertex);
            }

            // SS: loop over all courses, so we add those that
            // have no dependencies
            for (var i = 0; i < numCourses; i++)
            {
                if (visited.Contains(i))
                {
                    continue;
                }

                visited.Add(i);

                TopologicalSort(i);
            }

            var orderedCourses = new int[numCourses];
            var j = numCourses - 1;
            while (stack.Any())
            {
                orderedCourses[j--] = stack.Pop();
            }

            return orderedCourses;
        }

        private static bool HasCycle2(Dictionary<int, IList<int>> adjList)
        {
            // SS: detect cycle in directed graph, https://www.youtube.com/watch?v=rKQaZuoUR4M
            var white = new int[adjList.Count];
            for (var i = 0; i < white.Length; i++)
            {
                white[i] = 1;
            }

            var gray = new int[white.Length];
            var black = new int[white.Length];

            bool DFS(int vertex)
            {
                gray[vertex] = 1;
                white[vertex] = 0;

                var edges = adjList[vertex];
                foreach (var targetVertex in edges)
                {
                    if (black[targetVertex] > 0)
                    {
                        continue;
                    }

                    if (gray[targetVertex] > 0)
                    {
                        // SS: we found a cycle
                        return true;
                    }

                    var hasCycle = DFS(targetVertex);
                    if (hasCycle)
                    {
                        return true;
                    }
                }

                gray[vertex] = 0;
                black[vertex] = 1;

                return false;
            }

            for (var vertex = 0; vertex < white.Length; vertex++)
            {
                if (white[vertex] == 0)
                {
                    continue;
                }

                var hashCycle = DFS(vertex);
                if (hashCycle)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool HasCycle(Dictionary<int, IList<int>> adjList)
        {
            // SS: check whether the graph is a DAG
            var visited = new int[adjList.Count];

            var inDegree = new int[adjList.Count];
            foreach (var item in adjList)
            {
                foreach (var v in item.Value)
                {
                    inDegree[v]++;
                }
            }

            var queue = new Queue<int>();

            for (var i = 0; i < inDegree.Length; i++)
            {
                if (inDegree[i] == 0)
                {
                    queue.Enqueue(i);
                }
            }

            while (queue.Any())
            {
                var vertex = queue.Dequeue();

                if (visited[vertex] != 0)
                {
                    continue;
                }

                visited[vertex] = 1;

                // SS: check all outgoing edges
                var neighbors = adjList[vertex];
                for (var i = 0; i < neighbors.Count; i++)
                {
                    var v = neighbors[i];
                    inDegree[v]--;
                    if (inDegree[v] == 0)
                    {
                        queue.Enqueue(v);
                    }
                }
            }

            return inDegree.Any(x => x != 0);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] prerequisites = {new[] {1, 0}};

                // Act
                var orderedCourses = new Solution().FindOrder(2, prerequisites);

                // Assert
                CollectionAssert.AreEqual(new[] {0, 1}, orderedCourses);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] prerequisites = {new[] {1, 0}, new[] {2, 0}, new[] {3, 1}, new[] {3, 2}};

                // Act
                var orderedCourses = new Solution().FindOrder(4, prerequisites);

                // Assert
                CollectionAssert.AreEqual(new[] {0, 1, 2, 3}, orderedCourses);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var prerequisites = new int[0][];

                // Act
                var orderedCourses = new Solution().FindOrder(1, prerequisites);

                // Assert
                CollectionAssert.AreEqual(new[] {0}, orderedCourses);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var prerequisites = new int[0][];

                // Act
                var orderedCourses = new Solution().FindOrder(3, prerequisites);

                // Assert
                CollectionAssert.AreEqual(new[] {0, 1, 2}, orderedCourses);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[][] prerequisites = {new[] {2, 1}, new[] {2, 0}};

                // Act
                var orderedCourses = new Solution().FindOrder(4, prerequisites);

                // Assert
                CollectionAssert.AreEqual(new[] {0, 1, 2, 3}, orderedCourses);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[][] prerequisites = {new[] {1, 0}, new[] {0, 1}};

                // Act
                var orderedCourses = new Solution().FindOrder(2, prerequisites);

                // Assert
                Assert.IsEmpty(orderedCourses);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[][] prerequisites = {new[] {0, 1}, new[] {0, 3}, new[] {3, 5}, new[] {5, 4}, new[] {4, 3}, new[] {1, 2}};

                // Act
                var orderedCourses = new Solution().FindOrder(6, prerequisites);

                // Assert
                Assert.IsEmpty(orderedCourses);
            }
        }
    }
}