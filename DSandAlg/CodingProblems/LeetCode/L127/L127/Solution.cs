#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 127. Word Ladder
// URL: https://leetcode.com/problems/word-ladder/

namespace LeetCode
{
    public class Solution
    {
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
//            return LadderLengthBacktracking(beginWord, endWord, wordList);
            return LadderLengthShortestPath(beginWord, endWord, wordList);
        }

        private int LadderLengthShortestPath(string beginWord, string endWord, IList<string> wordList)
        {
            // SS: runtime complexity: O(N^2 * beginWord), N = wordList.Length
            // space complexity: O(N^2) (per vertex N, store up to N vertices)

            // SS: is the end word contained?
            if (wordList.IndexOf(endWord) == -1)
            {
                // SS: no solution
                return 0;
            }

            var endVertex = -1;

            // SS: generate graph with only valid transitions (i.e. two words are connected if
            // they differ by at most 1 character)
            var g = new Graph();
            for (var i = 0; i <= wordList.Count; i++)
            {
                g.AddVertex(i);
            }

            // SS: O(N * beginWord)
            for (var i = 0; i < wordList.Count; i++)
            {
                var w = wordList[i];
                if (w == endWord)
                {
                    endVertex = i + 1;
                }

                // SS: O(beginWord)
                if (Difference(beginWord, w))
                {
                    g.AddUndirectedEdge(0, i + 1);
                }
            }

            // SS: O(N^2 * beginWord)
            for (var i = 0; i < wordList.Count; i++)
            {
                var w1 = wordList[i];

                for (var j = i + 1; j < wordList.Count; j++)
                {
                    var w2 = wordList[j];

                    if (Difference(w1, w2))
                    {
                        g.AddUndirectedEdge(i + 1, j + 1);
                    }
                }
            }

            // SS: do BFS starting from vertex 0
            // O(N)
            var queue = new Queue<(int vertex, int pathLength)>();
            queue.Enqueue((0, 1));

            var visited = new HashSet<int> {0};

            while (queue.Any())
            {
                (var vertex, var pathLength) = queue.Dequeue();

                if (vertex == endVertex)
                {
                    return pathLength;
                }

                var adjList = g.AdjacencyList[vertex];
                for (var i = 0; i < adjList.Count; i++)
                {
                    var target = adjList[i];

                    if (visited.Contains(target))
                    {
                        continue;
                    }

                    visited.Add(target);

                    queue.Enqueue((target, pathLength + 1));
                }
            }

            return 0;
        }

        private int LadderLengthBacktracking(string beginWord, string endWord, IList<string> wordList)
        {
            // SS: check for valid input

            int Solve(IList<string> wordsLeft, IList<string> wordsSoFar)
            {
                var currentWord = wordsSoFar[^1];

                // SS: termination condition
                if (currentWord == endWord)
                {
                    return wordsSoFar.Count;
                }

                if (wordsLeft.Any() == false)
                {
                    // SS: no more words, so no path
                    return int.MaxValue;
                }

                var minCount = int.MaxValue;

                for (var i = 0; i < wordsLeft.Count; i++)
                {
                    var w = wordsLeft[i];

                    // SS: transition possible?
                    if (Difference(currentWord, w))
                    {
                        wordsSoFar.Add(w);
                        wordsLeft.Remove(w);
                        var c = Solve(wordsLeft, wordsSoFar);
                        minCount = Math.Min(minCount, c);

                        // SS: backtrack
                        wordsLeft.Add(w);
                        wordsSoFar.RemoveAt(wordsSoFar.Count - 1);
                    }
                }

                return minCount;
            }

            // SS: could check if end word is on wordList...

            var n = Solve(wordList, new List<string> {beginWord});
            return n == int.MaxValue ? 0 : n;
        }

        private static bool Difference(string w1, string w2)
        {
            var isDifferent = false;

            for (var i = 0; i < w1.Length; i++)
            {
                if (w1[i] != w2[i])
                {
                    if (isDifferent)
                    {
                        return false;
                    }

                    isDifferent = true;
                }
            }

            return true;
        }

        public class Graph
        {
            public IDictionary<int, List<int>> AdjacencyList { get; } = new Dictionary<int, List<int>>();

            public void AddVertex(int vertexId)
            {
                AdjacencyList[vertexId] = new List<int>();
            }

            public void AddDirectedEdge(int fromVertexId, int toVertexId)
            {
                AdjacencyList[fromVertexId].Add(toVertexId);
            }


            public void AddUndirectedEdge(int fromVertexId, int toVertexId)
            {
                AddDirectedEdge(fromVertexId, toVertexId);
                AddDirectedEdge(toVertexId, fromVertexId);
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var wordList = new List<string> {"hot", "dot", "dog", "lot", "log", "cog"};

                // Act
                var n = new Solution().LadderLength("hit", "cog", wordList);

                // Assert
                Assert.AreEqual(5, n);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var wordList = new List<string> {"hot", "dot", "dog", "lot", "log"};

                // Act
                var n = new Solution().LadderLength("hit", "cog", wordList);

                // Assert
                Assert.AreEqual(0, n);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var wordList = new List<string> {"hit"};

                // Act
                var n = new Solution().LadderLength("hot", "hit", wordList);

                // Assert
                Assert.AreEqual(2, n);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var wordList = new List<string> {"hot"};

                // Act
                var n = new Solution().LadderLength("hit", "cog", wordList);

                // Assert
                Assert.AreEqual(0, n);
            }
        }
    }
}