#region

using System;
using NUnit.Framework;

#endregion

// Problem: 543. Diameter of Binary Tree
// URL: https://leetcode.com/problems/diameter-of-binary-tree/

namespace LeetCode
{
    public class Solution
    {
        public int DiameterOfBinaryTree(TreeNode root)
        {
            // SS: post-order traversal, O(n)
            
            int maxDiameter = 0;

            int DFS(TreeNode node)
            {
                if (node == null)
                {
                    return 0;
                }

                int maxLeftHeight = DFS(node.left);
                int maxRightHeight = DFS(node.right);
                int diameter = maxLeftHeight + maxRightHeight;
                maxDiameter = Math.Max(maxDiameter, diameter);
                return 1 + Math.Max(maxLeftHeight, maxRightHeight);
            }

            DFS(root);
            return maxDiameter;
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