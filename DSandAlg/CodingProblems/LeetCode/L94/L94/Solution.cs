#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 94. Binary Tree Inorder Traversal
// URL: https://leetcode.com/problems/binary-tree-inorder-traversal/

namespace LeetCode
{
    public class Solution
    {
        public IList<int> InorderTraversal(TreeNode root)
        {
            return InorderTraversalIterative(root);
        }

        public IList<int> InorderTraversalIterative(TreeNode root)
        {
            // SS: runtime complexity: O(n), n = #nodes
            // space complexity: O(log n)

            var result = new List<int>();
            if (root == null)
            {
                return result;
            }

            var stack = new Stack<TreeNode>();
            var node = root;

            while (true)
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.left;
                }
                else
                {
                    if (stack.Any() == false)
                    {
                        break;
                    }

                    node = stack.Pop();
                    result.Add(node.val);
                    node = node.right;
                }
            }

            return result;
        }

        public IList<int> InorderTraversalRecursive(TreeNode root)
        {
            // SS: runtime complexity: O(n), n = #nodes
            // space complexity: O(log n)

            var result = new List<int>();
            if (root == null)
            {
                return result;
            }

            void InOrder(TreeNode node)
            {
                if (node == null)
                {
                    return;
                }

                InOrder(node.left);
                result.Add(node.val);
                InOrder(node.right);
            }

            InOrder(root);

            return result;
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
                    , left = null
                    , right = new TreeNode
                    {
                        val = 2
                        , left = new TreeNode
                        {
                            val = 3
                        }
                    }
                };

                // Act
                var result = new Solution().InorderTraversal(root);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 3, 2}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var result = new Solution().InorderTraversal(null);

                // Assert
                Assert.IsEmpty(result);
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
                var result = new Solution().InorderTraversal(root);

                // Assert
                CollectionAssert.AreEqual(new[] {1}, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 1
                    , left = new TreeNode
                    {
                        val = 2
                    }
                };

                // Act
                var result = new Solution().InorderTraversal(root);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 1}, result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 1
                    , right = new TreeNode
                    {
                        val = 2
                    }
                };

                // Act
                var result = new Solution().InorderTraversal(root);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 2}, result);
            }
        }
    }
}