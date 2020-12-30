#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 99. Recover Binary Search Tree
// URL: https://leetcode.com/problems/recover-binary-search-tree/

namespace LeetCode
{
    public class Solution
    {
        // 1. Brute-force: swap every node with every other node and check
        // whether BST property is satisfied

        public void RecoverTree(TreeNode root)
        {
            var minByNode = new Dictionary<TreeNode, TreeNode>();
            var maxByNode = new Dictionary<TreeNode, TreeNode>();

            (TreeNode n1, TreeNode n2) swap = (null, null);

            void DFS(TreeNode node)
            {
                if (node == null)
                {
                    return;
                }

                DFS(node.left);
                DFS(node.right);

                minByNode[node] = node;
                maxByNode[node] = node;

                if (node.left != null)
                {
                    // SS: propagate node with min value up
                    if (minByNode[node].val > minByNode[node.left].val)
                    {
                        minByNode[node] = minByNode[node.left];
                    }

                    // SS: propagate node with max value up
                    if (maxByNode[node].val < maxByNode[node.left].val)
                    {
                        maxByNode[node] = maxByNode[node.left];
                    }
                }

                if (node.right != null)
                {
                    // SS: propagate node with min value up
                    if (minByNode[node].val > minByNode[node.right].val)
                    {
                        minByNode[node] = minByNode[node.right];
                    }

                    // SS: propagate node with max value up
                    if (maxByNode[node].val < maxByNode[node.right].val)
                    {
                        maxByNode[node] = maxByNode[node.right];
                    }
                }

                // SS: now we have the min/max nodes for each subtree,
                // check for violations
                if (node.left != null)
                {
                    if (maxByNode[node.left].val > node.val)
                    {
                        swap = (node, maxByNode[node.left]);
                    }
                }

                if (node.right != null)
                {
                    if (minByNode[node.right].val < node.val)
                    {
                        swap = (node, minByNode[node.right]);
                    }
                }

                if (node.left != null && node.right != null)
                {
                    if (minByNode[node.right].val < maxByNode[node.left].val)
                    {
                        swap = (minByNode[node.right], maxByNode[node.left]);
                    }
                }
            }

            DFS(root);

            // SS: swap node values
            var tmp = swap.n1.val;
            swap.n1.val = swap.n2.val;
            swap.n2.val = tmp;
        }

        public class TreeNode
        {
            public TreeNode left;
            public TreeNode right;
            public int val;

            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                TreeNode root = new() {val = 1, left = new() {val = 3, right = new() {val = 2}}};

                // Act
                new Solution().RecoverTree(root);

                // Assert
                Assert.AreEqual(3, root.val);
                Assert.AreEqual(1, root.left.val);
                Assert.Null(root.right);
                Assert.Null(root.left.left);
                Assert.AreEqual(2, root.left.right.val);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                TreeNode root = new() {val = 3, left = new() {val = 1}, right = new() {val = 4, left = new() {val = 2}}};

                // Act
                new Solution().RecoverTree(root);

                // Assert
                Assert.AreEqual(2, root.val);
                Assert.AreEqual(1, root.left.val);
                Assert.AreEqual(4, root.right.val);
                Assert.AreEqual(3, root.right.left.val);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                TreeNode root = new() {val = 2, left = new() {val = 3}, right = new() {val = 1}};

                // Act
                new Solution().RecoverTree(root);

                // Assert
                Assert.AreEqual(2, root.val);
                Assert.AreEqual(1, root.left.val);
                Assert.AreEqual(3, root.right.val);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                TreeNode root = new() {val = 3, right = new() {val = 2, right = new() {val = 1}}};

                // Act
                new Solution().RecoverTree(root);

                // Assert
                Assert.AreEqual(1, root.val);
                Assert.AreEqual(2, root.right.val);
                Assert.AreEqual(3, root.right.right.val);
            }
        }
    }
}