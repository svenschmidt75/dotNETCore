#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 133. Clone Graph
// URL: https://leetcode.com/problems/clone-graph/

namespace LeetCode
{
    public class Solution
    {
        public Node CloneGraph(Node node)
        {
            if (node == null)
            {
                return null;
            }

            var nodeList = new List<Node>();
            var visited = new HashSet<int>();

            void DFS(Node node)
            {
                var adjList = node.neighbors;
                for (var i = 0; i < adjList.Count; i++)
                {
                    var neighbor = adjList[i];

                    if (visited.Contains(neighbor.val))
                    {
                        continue;
                    }

                    visited.Add(neighbor.val);
                    nodeList.Add(neighbor);

                    DFS(neighbor);
                }
            }

            visited.Add(node.val);
            nodeList.Add(node);
            DFS(node);

            // SS: clone graph

            var nodeHash = new Dictionary<Node, Node>();
            for (var i = 0; i < nodeList.Count; i++)
            {
                var n = nodeList[i];
                var deepCopy = new Node(n.val, new List<Node>(n.neighbors));
                nodeHash[n] = deepCopy;
            }

            for (var i = 0; i < nodeList.Count; i++)
            {
                var n = nodeList[i];
                var deepCopy = nodeHash[n];

                for (var j = 0; j < n.neighbors.Count; j++)
                {
                    var neighbor = n.neighbors[j];
                    deepCopy.neighbors[j] = nodeHash[neighbor];
                }
            }

            return nodeHash[node];
        }

        public class Node
        {
            public IList<Node> neighbors;
            public int val;

            public Node()
            {
                val = 0;
                neighbors = new List<Node>();
            }

            public Node(int _val)
            {
                val = _val;
                neighbors = new List<Node>();
            }

            public Node(int _val, List<Node> _neighbors)
            {
                val = _val;
                neighbors = _neighbors;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act

                // Assert
            }
        }
    }
}