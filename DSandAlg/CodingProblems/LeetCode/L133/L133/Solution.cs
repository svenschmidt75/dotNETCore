#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 133. Clone Graph
// URL: https://leetcode.com/problems/clone-graph/

namespace LeetCode
{
    public class Solution
    {
        public Node CloneGraphDFS2(Node node)
        {
            // SS: DF traversal with runtime complexity O(V + E)
            // space complexity: O(V) 
            // Larry's solution: https://www.youtube.com/watch?v=LqCqHbR80io&t=1s
            if (node == null)
            {
                return null;
            }

            var nodeHash = new Dictionary<Node, Node>();

            Node DFS(Node n)
            {
                if (nodeHash.ContainsKey(n))
                {
                    // SS: node already processed
                    return nodeHash[n];
                }

                var clone = new Node(n.val, new List<Node>(n.neighbors));
                nodeHash[n] = clone;

                for (var i = 0; i < n.neighbors.Count; i++)
                {
                    var neighbor = n.neighbors[i];
                    clone.neighbors[i] = DFS(neighbor);
                }

                return clone;
            }

            return DFS(node);
        }

        public Node CloneGraphBFS(Node node)
        {
            // SS: BF traversal with runtime complexity O(V + E)
            // space complexity: O(V) 
            if (node == null)
            {
                return null;
            }

            var nodeMap = new Dictionary<Node, Node>
            {
                {node, new Node(node.val, new List<Node>(node.neighbors))}
            };

            var visited = new HashSet<int> {node.val};

            var queue = new Queue<Node>();
            queue.Enqueue(node);

            while (queue.Any())
            {
                var n = queue.Dequeue();

                var adjList = n.neighbors;
                for (var i = 0; i < adjList.Count; i++)
                {
                    var neighbor = adjList[i];

                    if (nodeMap.ContainsKey(neighbor) == false)
                    {
                        var clonedNode = new Node(neighbor.val, new List<Node>(neighbor.neighbors));
                        nodeMap[neighbor] = clonedNode;
                    }

                    if (visited.Contains(neighbor.val))
                    {
                        nodeMap[n].neighbors[i] = nodeMap[neighbor];
                        continue;
                    }

                    visited.Add(neighbor.val);

                    nodeMap[n].neighbors[i] = nodeMap[neighbor];

                    queue.Enqueue(neighbor);
                }
            }

            return nodeMap[node];
        }

        public Node CloneGraphDFS(Node node)
        {
            // SS: DFS with runtime complexity O(V + E)
            // space complexity: O(V) 
            if (node == null)
            {
                return null;
            }

            var visited = new HashSet<int>();

            var nodeMap = new Dictionary<Node, Node>();

            Node DFS(Node node)
            {
                var clonedNode = new Node(node.val, new List<Node>(node.neighbors));
                nodeMap[node] = clonedNode;

                var adjList = node.neighbors;
                for (var i = 0; i < adjList.Count; i++)
                {
                    var neighbor = adjList[i];

                    if (visited.Contains(neighbor.val))
                    {
                        clonedNode.neighbors[i] = nodeMap[neighbor];
                        continue;
                    }

                    visited.Add(neighbor.val);

                    DFS(neighbor);

                    clonedNode.neighbors[i] = nodeMap[neighbor];
                }

                return clonedNode;
            }

            visited.Add(node.val);

            var clonedNode = DFS(node);

            return clonedNode;
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