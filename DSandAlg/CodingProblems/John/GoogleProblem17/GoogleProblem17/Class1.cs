#region

using System;
using System.Linq;
using NUnit.Framework;

#endregion

// LeetCode 269. Alien Dictionary
// https://leetcode.com/problems/alien-dictionary/

namespace GoogleProblem17
{
    public class Solution
    {
        public string Solve(string[] words)
        {
            var g = new Graph();

            for (var i = 0; i <= words.Length - 2; i++)
            {
                var w1 = words[i];
                var w2 = words[i + 1];

                var length = Math.Min(w1.Length, w2.Length);
                int j;
                for (j = 0; j < length; j++)
                {
                    var c1 = w1[j];
                    var v1 = c1 - 'a';
                    g.AddVertex(v1);

                    var c2 = w2[j];
                    var v2 = c2 - 'a';
                    g.AddVertex(v2);

                    if (c1 != c2)
                    {
                        if (g.ContainsEdge(v1, v2))
                        {
                            // Ordering cannot be satisfied due to conflicting ordering.
                            // Graph for topological sort would have a cycle...
                            return string.Empty;
                        }

                        g.AddDirectedEdge(v2, v1);
                        break;
                    }
                }

                // simply add remaining characters, since we have no
                // order information about them
                while (j < w1.Length)
                {
                    var c = w1[j];
                    var v = c - 'a';
                    g.AddVertex(v);
                    j++;
                }

                while (j < w2.Length)
                {
                    var c = w2[j];
                    var v = c - 'a';
                    g.AddVertex(v);
                    j++;
                }
            }

            // generate unordered alphabet
            var sortedVertices = Topo.TopologicalSort(g);

            var sortedChars = sortedVertices.Select(vertex => (char) ('a' + vertex)).ToArray();
            return new string(sortedChars);
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var words = new[] {"wrt", "wrf", "er", "ett", "rftt"};

            // Act
            var alphabet = new Solution().Solve(words);

            // Assert
            Assert.AreEqual("wertf", alphabet);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var words = new[] {"z", "x"};

            // Act
            var alphabet = new Solution().Solve(words);

            // Assert
            Assert.AreEqual("zx", alphabet);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var words = new[] {"z", "x", "z"};

            // Act
            var alphabet = new Solution().Solve(words);

            // Assert
            Assert.AreEqual("", alphabet);
        }
    }
}