#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 145. Binary Tree Postorder Traversal
// URL: https://leetcode.com/problems/binary-tree-postorder-traversal/

namespace LeetCode
{
    public class Solution
    {
        public IList<int> PostorderTraversal(TreeNode root)
        {
            return Recursive(root);
        }

        private IList<int> Iterative(TreeNode root)
        {
            var result = new List<int>();

            if (root == null)
            {
                return result;
            }

            var stack = new Stack<TreeNode>();
            stack.Push(root);

            while (stack.Any())
            {
                var node = stack.Pop();

                if (node.left != null)
                {
                    stack.Push(node.left);
                }

                if (node.right != null)
                {
                    stack.Push(node.right);
                }

                result.Add(node.val);
            }

            // SS: postorder traversal, so reverse
            result.Reverse();

            return result;
        }

        private IList<int> Recursive(TreeNode root)
        {
            var result = new List<int>();

            if (root == null)
            {
                return result;
            }

            void DFS(TreeNode node)
            {
                if (node == null)
                {
                    return;
                }

                // SS: postorder traversal
                DFS(node.left);
                DFS(node.right);

                result.Add(node.val);
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