#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// from Lowest Common Ancestor (LCA) Problem | Eulerian path method
// https://www.youtube.com/watch?v=sD1IoalFomA&list=PLDV1Zeh2NRsDGO4--qE8yH72HFL1Km93P&index=13

namespace BinarySearchTree
{
    public class LCA
    {
        public Node<int> Solve(Node<int> root, Node<int> node1, Node<int> node2)
        {
            var helper = new List<(Node<int> node, int depth)>();

            // SS: build Euler tour
            // SS: runtime complexity: O(N) 
            Traverse(root, 0, helper);

            var hashMap = new Dictionary<Node<int>, int>();
            for (var i = 0; i < helper.Count; i++)
            {
                var node = helper[i].node;
                hashMap[node] = i;
            }

            // SS: find node1's index, O(1)
            var idx1 = Math.Min(hashMap[node1], hashMap[node2]);
            var idx2 = Math.Max(hashMap[node1], hashMap[node2]);

            // SS: find minimum depth/node in between
            // this can be done more efficiently using a sparse table to do 
            // this Range Minimum Query RMQ
            // SS: runtime complexity: O(N)
            var minDepth = int.MaxValue;
            var lca = root;
            for (var i = idx1; i <= idx2; i++)
            {
                (var node, var depth) = helper[i];
                if (depth < minDepth)
                {
                    minDepth = depth;
                    lca = node;
                }
            }

            return lca;
        }

        private void Traverse(Node<int> node, int depth, List<(Node<int> node, int depth)> helper)
        {
            if (node == null)
            {
                return;
            }

            // SS: pre-order traversal
            helper.Add((node, depth));

            if (node.Left != null)
            {
                Traverse(node.Left, depth + 1, helper);
                helper.Add((node, depth));
            }

            if (node.Right != null)
            {
                Traverse(node.Right, depth + 1, helper);
                helper.Add((node, depth));
            }
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test71()
        {
            // SS: from https://www.youtube.com/watch?v=sD1IoalFomA&list=PLDV1Zeh2NRsDGO4--qE8yH72HFL1Km93P&index=13

            // Arrange
            var node5 = new Node<int> {Value = 5};
            var node6 = new Node<int> {Value = 6};

            var node4 = new Node<int> {Value = 4, Left = node6};

            var node2 = new Node<int> {Value = 2, Left = node4, Right = node5};

            var node3 = new Node<int> {Value = 3};
            var node1 = new Node<int> {Value = 1, Left = node3};
            var node0 = new Node<int> {Value = 0, Left = node1, Right = node2};

            // Act
            var parent = new LCA().Solve(node0, node6, node5);

            // Assert
            Assert.AreEqual(2, parent.Value);
        }

        [Test]
        public void Test72()
        {
            // SS: from https://www.youtube.com/watch?v=sD1IoalFomA&list=PLDV1Zeh2NRsDGO4--qE8yH72HFL1Km93P&index=13

            // Arrange
            var node5 = new Node<int> {Value = 5};
            var node6 = new Node<int> {Value = 6};

            var node4 = new Node<int> {Value = 4, Left = node6};

            var node2 = new Node<int> {Value = 2, Left = node4, Right = node5};

            var node3 = new Node<int> {Value = 3};
            var node1 = new Node<int> {Value = 1, Left = node3};
            var node0 = new Node<int> {Value = 0, Left = node1, Right = node2};

            // Act
            var parent = new LCA().Solve(node0, node3, node6);

            // Assert
            Assert.AreEqual(0, parent.Value);
        }
    }
}