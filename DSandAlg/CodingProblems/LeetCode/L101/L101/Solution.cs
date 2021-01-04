#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 101. Symmetric Tree
// URL: https://leetcode.com/problems/symmetric-tree/

namespace LeetCode
{
    public class Solution
    {
        public bool IsSymmetric(TreeNode root)
        {
            return IsSymmetricDFS(root);
        }

        private bool IsSymmetricIterative(TreeNode root)
        {
            if (root == null)
            {
                return true;
            }

            var stack = new Stack<(TreeNode, TreeNode)>();
            stack.Push((root.left, root.right));

            while (stack.Any())
            {
                (var n1, var n2) = stack.Pop();

                if (n1 == null)
                {
                    if (n2 != null)
                    {
                        return false;
                    }
                }
                else
                {
                    if (n2 == null)
                    {
                        return false;
                    }

                    if (n1.val != n2.val)
                    {
                        return false;
                    }

                    stack.Push((n1.left, n2.right));
                    stack.Push((n1.right, n2.left));
                }
            }

            return true;
        }

        private bool IsSymmetricDFS(TreeNode root)
        {
            // SS: runtime complexity: O(V)
            if (root == null)
            {
                return true;
            }

            bool DFS(TreeNode n1, TreeNode n2)
            {
                // SS: preorder traversal
                if (n1 == null)
                {
                    return n2 == null;
                }

                if (n2 == null)
                {
                    return false;
                }

                if (n1.val != n2.val)
                {
                    return false;
                }

                return DFS(n1.left, n2.right) && DFS(n1.right, n2.left);
            }

            return DFS(root.left, root.right);
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