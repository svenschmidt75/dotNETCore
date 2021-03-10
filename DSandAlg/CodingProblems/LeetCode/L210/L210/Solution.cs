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
            // SS: topological sort

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

        private static bool HasCycle(Dictionary<int, IList<int>> adjList)
        {
            // SS: detect cycle in directed graph, https://www.youtube.com/watch?v=rKQaZuoUR4M
            var white = new HashSet<int>();
            foreach (var item in adjList)
            {
                white.Add(item.Key);
            }

            var black = new HashSet<int>();

            bool DFS(int vertex, HashSet<int> gray)
            {
                var edges = adjList[vertex];
                foreach (var targetVertex in edges)
                {
                    if (gray.Contains(targetVertex))
                    {
                        return true;
                    }

                    gray.Add(targetVertex);
                    black.Add(targetVertex);

                    var hasCycle = DFS(targetVertex, gray);
                    if (hasCycle)
                    {
                        return true;
                    }

                    gray.Remove(targetVertex);
                }

                return false;
            }

            foreach (var vertex in white)
            {
                if (black.Contains(vertex))
                {
                    continue;
                }

                var gray = new HashSet<int>();
                gray.Add(vertex);
                black.Add(vertex);

                var hashCycle = DFS(vertex, gray);
                if (hashCycle)
                {
                    return true;
                }
            }

            return false;
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
        }
    }
}