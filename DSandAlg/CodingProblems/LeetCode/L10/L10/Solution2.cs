#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace L10
{
    public class Solution2
    {
        public bool IsMatch(string s, string p)
        {
            var g = BuildGraph(p);

            if (g.NVertices == 0)
            {
                return s.Length == 0;
            }

            var q = new Queue<(int vertex, int sPosition)>();
            q.Enqueue((0, 0));

            while (q.Any())
            {
                var (vertex, sPosition) = q.Dequeue();

                var transitions = g.AdjacencyList[vertex];

                if (transitions.Any() == false)
                {
                    // end state
                    if (sPosition == s.Length)
                    {
                        return true;
                    }

                    continue;
                }

                for (var i = 0; i < transitions.Count; i++)
                {
                    var transition = transitions[i];
                    if (transition.skip == 0)
                    {
                        // simply move to the next state
                        q.Enqueue((transition.targetVertex, sPosition));
                    }
                    else if (sPosition < s.Length)
                    {
                        var c = s[sPosition];
                        if (c == transition.c || transition.c == '.')
                        {
                            q.Enqueue((transition.targetVertex, sPosition + transition.skip));
                        }
                    }
                }
            }

            return false;
        }

        private Graph BuildGraph(string p)
        {
            var g = new Graph();

            var currentVertex = 0;
            var i = 0;
            while (i < p.Length)
            {
                g.AddVertex(currentVertex);

                var c = p[i];
                var offset = 1;

                if (i <= p.Length - 2 && p[i + 1] == '*')
                {
                    g.AddDirectedEdge(currentVertex, currentVertex, c, 1);
                    g.AddDirectedEdge(currentVertex, currentVertex + 1, (char) 0, 0);
                    offset++;
                }
                else
                {
                    g.AddDirectedEdge(currentVertex, currentVertex + 1, c, 1);
                }

                i += offset;
                currentVertex++;
            }

            // add end state
            g.AddVertex(currentVertex);

            return g;
        }

        public class Graph
        {
            public int NVertices { get; set; }

            public Dictionary<int, List<(int targetVertex, char c, int skip)>> AdjacencyList { get; } = new Dictionary<int, List<(int targetVertex, char c, int skip)>>();

            public void AddVertex(int node)
            {
                AdjacencyList[node] = new List<(int, char, int)>();
                NVertices++;
            }

            public void AddDirectedEdge(int fromNode, int toNode, char c, int skip)
            {
                AdjacencyList[fromNode].Add((toNode, c, skip));
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "aa";
                var p = "a";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.False(isMatch);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "aa";
                var p = "a*";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "ab";
                var p = ".*";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "aab";
                var p = "c*a*b";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "mississippi";
                var p = "mis*is*p*.";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.False(isMatch);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var s = "mississippi";
                var p = "mis*is*ip*.";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var s = "";
                var p = ".*";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.True(isMatch);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                var s = "a";
                var p = "";

                // Act
                var isMatch = new Solution().IsMatch(s, p);

                // Assert
                Assert.False(isMatch);
            }
        }
    }
}