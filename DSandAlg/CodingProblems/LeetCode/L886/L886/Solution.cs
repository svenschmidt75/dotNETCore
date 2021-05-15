#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 886. Possible Bipartition
// URL: https://leetcode.com/problems/possible-bipartition/

namespace LeetCode
{
    public class Solution
    {
        public bool PossibleBipartition(int n, int[][] dislikes)
        {
            return PossibleBipartition1(n, dislikes);
            // return PossibleBipartition2(n, dislikes);
        }

        private bool PossibleBipartition2(int n, int[][] dislikes)
        {
            // SS: runtime complexity: O(#dislikes^2)
            // This approach does not work. We have to process the vertices in
            // an order that propagates the coloring properly...

            var sets = new List<HashSet<int>>
            {
                new HashSet<int>()
            };

            // SS: add each vertex into the same set
            for (var i = 1; i <= n; i++)
            {
                sets[0].Add(i);
            }

            Array.Sort(dislikes, (d1, d2) =>
            {
                if (d1[0] == d2[0])
                {
                    // SS: resolve tie
                    return d1[1].CompareTo(d2[1]);
                }

                return d1[0].CompareTo(d2[0]);
            });

            var setIdx = 0;
            while (true)
            {
                var currentSet = sets[setIdx];
                var newSet = new HashSet<int>();

                for (var j = 0; j < dislikes.Length; j++)
                {
                    var v1 = dislikes[j][0];
                    var v2 = dislikes[j][1];

                    if (currentSet.Contains(v1) && currentSet.Contains(v2))
                    {
                        currentSet.Remove(v2);
                        newSet.Add(v2);
                    }
                }

                if (newSet.Any())
                {
                    sets.Add(newSet);
                    setIdx++;
                }
                else
                {
                    break;
                }

                if (sets.Count > 2)
                {
                    return false;
                }
            }

            return sets.Count <= 2;
        }

        private bool PossibleBipartition1(int n, int[][] dislikes)
        {
            // SS: 2-color the graph if possible
            // We are doing DFS, so runtime complexity O(V + E)
            // space complexity: O(V)

            var adjList = new Dictionary<int, IList<int>>();
            for (var i = 1; i <= n; i++)
            {
                adjList[i] = new List<int>();
            }

            // SS: connect nodes that dislike themselves, as those will
            // get different colors, i.e. end up being in different groups
            for (var i = 0; i < dislikes.Length; i++)
            {
                var v1 = dislikes[i][0];
                var v2 = dislikes[i][1];

                adjList[v1].Add(v2);
                adjList[v2].Add(v1);
            }

            var visited = new int[n + 1];
            var color = new int[n + 1];

            for (var i = 1; i <= n; i++)
            {
                if (visited[i] == 1)
                {
                    continue;
                }

                visited[i] = 1;
                color[i] = 1;
                if (DFS(i, adjList, visited, color) == false)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool DFS(int vertex, Dictionary<int, IList<int>> adjList, int[] visited, int[] color)
        {
            var neighborColor = color[vertex] == 1 ? 2 : 1;

            var neighbors = adjList[vertex];
            foreach (var neighbor in neighbors)
            {
                if (color[neighbor] > 0 && color[neighbor] != neighborColor)
                {
                    // SS: graph not 2-colorable
                    return false;
                }

                if (visited[neighbor] == 1)
                {
                    continue;
                }

                visited[neighbor] = 1;
                color[neighbor] = neighborColor;
                if (DFS(neighbor, adjList, visited, color) == false)
                {
                    return false;
                }
            }

            return true;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var dislikes = new[] {new[] {1, 2}, new[] {1, 3}, new[] {2, 4}};

                // Act
                var result = new Solution().PossibleBipartition(4, dislikes);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var dislikes = new[] {new[] {1, 2}, new[] {1, 3}, new[] {2, 3}};

                // Act
                var result = new Solution().PossibleBipartition(3, dislikes);

                // Assert
                Assert.False(result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var dislikes = new[] {new[] {1, 2}, new[] {2, 3}, new[] {3, 4}, new[] {4, 5}, new[] {1, 5}};

                // Act
                var result = new Solution().PossibleBipartition(5, dislikes);

                // Assert
                Assert.False(result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var dislikes = new int[0][];

                // Act
                var result = new Solution().PossibleBipartition(1, dislikes);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var dislikes = new[] {new[] {1, 2}, new[] {3, 4}, new[] {5, 6}, new[] {6, 7}, new[] {8, 9}, new[] {7, 8}};

                // Act
                var result = new Solution().PossibleBipartition(10, dislikes);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var dislikes = new[]
                {
                    new[] {21, 47}, new[] {4, 41}, new[] {2, 41}, new[] {36, 42}, new[] {32, 45}, new[] {26, 28}, new[] {32, 44}, new[] {5, 41}, new[] {29, 44}, new[] {10, 46}, new[] {1, 6}
                    , new[] {7, 42}, new[] {46, 49}, new[] {17, 46}, new[] {32, 35}, new[] {11, 48}, new[] {37, 48}, new[] {37, 43}, new[] {8, 41}, new[] {16, 22}, new[] {41, 43}, new[] {11, 27}
                    , new[] {22, 44}, new[] {22, 28}, new[] {18, 37}, new[] {5, 11}, new[] {18, 46}, new[] {22, 48}, new[] {1, 17}, new[] {2, 32}, new[] {21, 37}, new[] {7, 22}, new[] {23, 41}
                    , new[] {30, 39}, new[] {6, 41}, new[] {10, 22}, new[] {36, 41}, new[] {22, 25}, new[] {1, 12}, new[] {2, 11}, new[] {45, 46}, new[] {2, 22}, new[] {1, 38}, new[] {47, 50}
                    , new[] {11, 15}, new[] {2, 37}, new[] {1, 43}, new[] {30, 45}, new[] {4, 32}, new[] {28, 37}, new[] {1, 21}, new[] {23, 37}, new[] {5, 37}, new[] {29, 40}, new[] {6, 42}
                    , new[] {3, 11}, new[] {40, 42}, new[] {26, 49}, new[] {41, 50}, new[] {13, 41}, new[] {20, 47}, new[] {15, 26}, new[] {47, 49}, new[] {5, 30}, new[] {4, 42}, new[] {10, 30}
                    , new[] {6, 29}, new[] {20, 42}, new[] {4, 37}, new[] {28, 42}, new[] {1, 16}, new[] {8, 32}, new[] {16, 29}, new[] {31, 47}, new[] {15, 47}, new[] {1, 5}, new[] {7, 37}
                    , new[] {14, 47}, new[] {30, 48}, new[] {1, 10}, new[] {26, 43}, new[] {15, 46}, new[] {42, 45}, new[] {18, 42}, new[] {25, 42}, new[] {38, 41}, new[] {32, 39}, new[] {6, 30}
                    , new[] {29, 33}, new[] {34, 37}, new[] {26, 38}, new[] {3, 22}, new[] {18, 47}, new[] {42, 48}, new[] {22, 49}, new[] {26, 34}, new[] {22, 36}, new[] {29, 36}, new[] {11, 25}
                    , new[] {41, 44}, new[] {6, 46}, new[] {13, 22}, new[] {11, 16}, new[] {10, 37}, new[] {42, 43}, new[] {12, 32}, new[] {1, 48}, new[] {26, 40}, new[] {22, 50}, new[] {17, 26}
                    , new[] {4, 22}, new[] {11, 14}, new[] {26, 39}, new[] {7, 11}, new[] {23, 26}, new[] {1, 20}, new[] {32, 33}, new[] {30, 33}, new[] {1, 25}, new[] {2, 30}, new[] {2, 46}
                    , new[] {26, 45}, new[] {47, 48}, new[] {5, 29}, new[] {3, 37}, new[] {22, 34}, new[] {20, 22}, new[] {9, 47}, new[] {1, 4}, new[] {36, 46}, new[] {30, 49}, new[] {1, 9}
                    , new[] {3, 26}, new[] {25, 41}, new[] {14, 29}, new[] {1, 35}, new[] {23, 42}, new[] {21, 32}, new[] {24, 46}, new[] {3, 32}, new[] {9, 42}, new[] {33, 37}, new[] {7, 30}
                    , new[] {29, 45}, new[] {27, 30}, new[] {1, 7}, new[] {33, 42}, new[] {17, 47}, new[] {12, 47}, new[] {19, 41}, new[] {3, 42}, new[] {24, 26}, new[] {20, 29}, new[] {11, 23}
                    , new[] {22, 40}, new[] {9, 37}, new[] {31, 32}, new[] {23, 46}, new[] {11, 38}, new[] {27, 29}, new[] {17, 37}, new[] {23, 30}, new[] {14, 42}, new[] {28, 30}, new[] {29, 31}
                    , new[] {1, 8}, new[] {1, 36}, new[] {42, 50}, new[] {21, 41}, new[] {11, 18}, new[] {39, 41}, new[] {32, 34}, new[] {6, 37}, new[] {30, 38}, new[] {21, 46}, new[] {16, 37}
                    , new[] {22, 24}, new[] {17, 32}, new[] {23, 29}, new[] {3, 30}, new[] {8, 30}, new[] {41, 48}, new[] {1, 39}, new[] {8, 47}, new[] {30, 44}, new[] {9, 46}, new[] {22, 45}
                    , new[] {7, 26}, new[] {35, 42}, new[] {1, 27}, new[] {17, 30}, new[] {20, 46}, new[] {18, 29}, new[] {3, 29}, new[] {4, 30}, new[] {3, 46}
                };

                // Act
                var result = new Solution().PossibleBipartition(50, dislikes);

                // Assert
                Assert.True(result);
            }
        }
    }
}