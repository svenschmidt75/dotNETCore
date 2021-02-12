#region

using System;
using NUnit.Framework;

#endregion

// Problem: 104. Maximum Depth of Binary Tree
// URL: https://leetcode.com/problems/maximum-depth-of-binary-tree/

namespace LeetCode
{
    public class Solution
    {
        public int MaxDepth(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            // SS: preorder traversal
            int Solve(TreeNode node)
            {
                if (node == null)
                {
                    return 0;
                }

                return Math.Max(1 + Solve(node.left), 1 + Solve(node.right));
            }

            return Solve(root);
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