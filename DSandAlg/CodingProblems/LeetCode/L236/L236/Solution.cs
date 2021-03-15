#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 236. Lowest Common Ancestor of a Binary Tree
// URL: https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/

namespace LeetCode
{
    public class Solution
    {
        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            TreeNode DFS(TreeNode node, TreeNode n1, TreeNode n2)
            {
                // SS: base case
                if (node == null)
                {
                    return null;
                }

                if (node.left != null)
                {
                    var left = DFS(node.left, n1, n2);
                    if (left != null)
                    {
                        if (n1 == null)
                        {
                            n1 = left;
                        }
                        else if (n1 != left)
                        {
                            n2 = left;
                        }
                    }
                }

                if (node.right != null)
                {
                    var right = DFS(node.right, n1, n2);
                    if (right != null)
                    {
                        if (n1 == null)
                        {
                            n1 = right;
                        }
                        else if (n1 != right)
                        {
                            n2 = right;
                        }
                    }
                }

                if (node == p)
                {
                    if (n1 == null)
                    {
                        n1 = node;
                    }
                    else if (n2 == null)
                    {
                        n2 = node;
                    }
                }
                else if (node == q)
                {
                    if (n1 == null)
                    {
                        n1 = node;
                    }
                    else if (n2 == null)
                    {
                        n2 = node;
                    }
                }

                if (n1 != null && n2 != null)
                {
                    // SS: this is the lca
                    return node;
                }

                return n1;
            }

            var lca = DFS(root, null, null);
            return lca;
        }

        public TreeNode LowestCommonAncestor2(TreeNode root, TreeNode p, TreeNode q)
        {
            // SS: construct Eulerian tour
            // runtime complexity: O(N)
            // space complexity: O(n)

            var nodes = new List<TreeNode>();
            var depths = new List<int>();
            var dict = new Dictionary<TreeNode, int>();

            void DFS(TreeNode node, int depth)
            {
                // SS: base case
                if (node == null)
                {
                    return;
                }

                dict[node] = nodes.Count;
                nodes.Add(node);
                depths.Add(depth);

                if (node.left != null)
                {
                    DFS(node.left, depth + 1);

                    dict[node] = nodes.Count;
                    nodes.Add(node);
                    depths.Add(depth);
                }

                if (node.right != null)
                {
                    DFS(node.right, depth + 1);

                    dict[node] = nodes.Count;
                    nodes.Add(node);
                    depths.Add(depth);
                }
            }

            DFS(root, 0);

            // SS: find nodes p and q
            var node1Idx = dict[p];
            var node2Idx = dict[q];

            TreeNode lca = null;
            var depth = int.MaxValue;
            for (var i = Math.Min(node1Idx, node2Idx); i <= Math.Max(node1Idx, node2Idx); i++)
            {
                if (depths[i] < depth)
                {
                    lca = nodes[i];
                    depth = depths[i];
                }
            }

            return lca;
        }

        public class TreeNode
        {
            public TreeNode left;
            public TreeNode right;
            public int val;

            public TreeNode(int x)
            {
                val = x;
            }
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var node3 = new TreeNode(3);
                var node5 = new TreeNode(5);
                var node1 = new TreeNode(1);
                var node6 = new TreeNode(6);
                var node2 = new TreeNode(2);
                var node0 = new TreeNode(0);
                var node8 = new TreeNode(8);
                var node7 = new TreeNode(7);
                var node4 = new TreeNode(4);

                node2.left = node7;
                node2.right = node4;

                node5.left = node6;
                node5.right = node2;

                node3.left = node5;
                node3.right = node1;

                node1.left = node0;
                node1.right = node8;

                // Act
                var lca = new Solution().LowestCommonAncestor(node3, node5, node1);

                // Assert
                Assert.AreSame(node3, lca);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var node3 = new TreeNode(3);
                var node5 = new TreeNode(5);
                var node1 = new TreeNode(1);
                var node6 = new TreeNode(6);
                var node2 = new TreeNode(2);
                var node0 = new TreeNode(0);
                var node8 = new TreeNode(8);
                var node7 = new TreeNode(7);
                var node4 = new TreeNode(4);

                node2.left = node7;
                node2.right = node4;

                node5.left = node6;
                node5.right = node2;

                node3.left = node5;
                node3.right = node1;

                node1.left = node0;
                node1.right = node8;

                // Act
                var lca = new Solution().LowestCommonAncestor(node3, node5, node4);

                // Assert
                Assert.AreSame(node5, lca);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var node1 = new TreeNode(1);
                var node2 = new TreeNode(2);

                node1.left = node2;

                // Act
                var lca = new Solution().LowestCommonAncestor(node1, node1, node2);

                // Assert
                Assert.AreSame(node1, lca);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var node1 = new TreeNode(1);
                var node2 = new TreeNode(2);

                node1.right = node2;

                // Act
                var lca = new Solution().LowestCommonAncestor(node1, node1, node2);

                // Assert
                Assert.AreSame(node1, lca);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var node1 = new TreeNode(1);
                var node2 = new TreeNode(2);

                node1.right = node2;

                // Act
                var lca = new Solution().LowestCommonAncestor(node1, node1, node1);

                // Assert
                Assert.AreSame(node1, lca);
            }
            
        }
    }
}