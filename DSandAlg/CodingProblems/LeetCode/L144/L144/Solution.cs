#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 144. Binary Tree Preorder Traversal
// URL: https://leetcode.com/problems/binary-tree-preorder-traversal/

namespace LeetCode
{
    public class Solution
    {
        public IList<int> PreorderTraversal(TreeNode root)
        {
            return PreorderTraversalIterative2(root);
        }

        public IList<int> PreorderTraversalIterative2(TreeNode root)
        {
            var result = new List<int>();

            var stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Any())
            {
                var node = stack.Pop();
                if (node == null)
                {
                    break;
                }

                result.Add(node.val);

                if (node.right != null)
                {
                    stack.Push(node.right);
                }

                if (node.left != null)
                {
                    stack.Push(node.left);
                }
            }

            return result;
        }

        public IList<int> PreorderTraversalIterative(TreeNode root)
        {
            var result = new List<int>();

            if (root == null)
            {
                return null;
            }

            var stack = new Stack<TreeNode>();
            var node = root;
            while (true)
            {
                if (node == null)
                {
                    if (stack.Any() == false)
                    {
                        break;
                    }

                    node = stack.Pop();
                    node = node.right;
                }
                else
                {
                    result.Add(node.val);
                    stack.Push(node);
                    node = node.left;
                }
            }

            return result;
        }

        public IList<int> PreorderTraversalRecursive(TreeNode root)
        {
            var result = new List<int>();

            if (root == null)
            {
                return null;
            }

            void DFS(TreeNode node)
            {
                if (node == null)
                {
                    return;
                }

                result.Add(node.val);
                DFS(node.left);
                DFS(node.right);
            }

            DFS(root);

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

                // Act

                // Assert
            }
        }
    }
}