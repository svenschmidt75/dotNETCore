#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 114. Flatten Binary Tree to Linked List
// URL: https://leetcode.com/problems/flatten-binary-tree-to-linked-list/

namespace LeetCode
{
    public class Solution
    {
        public void Flatten(TreeNode root)
        {
            // SS: do reverse-order postorder traversal, i.e. visit right
            // child, then left child.
            // runtime complexity: O(N)
            // space complexity: O(1) (implicit stack-space used for recursion)
            
            if (root == null)
            {
                return;
            }

            TreeNode PreOrder(TreeNode node, TreeNode pivotNode)
            {
                var p = pivotNode;

                if (node.right != null)
                {
                    p = PreOrder(node.right, pivotNode);
                }

                if (node.left != null)
                {
                    p = PreOrder(node.left, p);
                }

                if (node.left == null && node.right == null)
                {
                    // SS: leaf node
                    if (p != null)
                    {
                        node.right = p;
                    }

                    return node;
                }

                if (node.left != null)
                {
                    node.right = node.left;
                    node.left = null;
                }

                return node;
            }

            PreOrder(root, null);
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
                var root = new TreeNode
                {
                    val = 1
                    , left = new TreeNode
                    {
                        val = 2
                        , left = new TreeNode
                        {
                            val = 4
                        }
                        , right = new TreeNode
                        {
                            val = 5
                        }
                    }
                    , right = new TreeNode
                    {
                        val = 3
                        , left = new TreeNode
                        {
                            val = 6
                            , left = new TreeNode
                            {
                                val = 8
                            }
                        }
                        , right = new TreeNode
                        {
                            val = 7
                        }
                    }
                };

                // Act
                new Solution().Flatten(root);

                // Assert
                var vals = new List<int>();
                var node = root;
                while (node != null)
                {
                    Assert.IsNull(node.left);
                    vals.Add(node.val);
                    node = node.right;
                }

                CollectionAssert.AreEqual(new[] {1, 2, 4, 5, 3, 6, 8, 7}, vals);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 1
                    , left = new TreeNode
                    {
                        val = 2
                        , left = new TreeNode
                        {
                            val = 3
                        }
                        , right = new TreeNode
                        {
                            val = 4
                        }
                    }
                    , right = new TreeNode
                    {
                        val = 5
                        , right = new TreeNode
                        {
                            val = 6
                        }
                    }
                };

                // Act
                new Solution().Flatten(root);

                // Assert
                var vals = new List<int>();
                var node = root;
                while (node != null)
                {
                    Assert.IsNull(node.left);
                    vals.Add(node.val);
                    node = node.right;
                }

                CollectionAssert.AreEqual(new[] {1, 2, 3, 4, 5, 6}, vals);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 1
                };

                // Act
                new Solution().Flatten(root);

                // Assert
                var vals = new List<int>();
                var node = root;
                while (node != null)
                {
                    Assert.IsNull(node.left);
                    vals.Add(node.val);
                    node = node.right;
                }

                CollectionAssert.AreEqual(new[] {1}, vals);
            }
        }
    }
}