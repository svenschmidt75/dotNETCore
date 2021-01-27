#region

using System;
using NUnit.Framework;

#endregion

// Problem: 111. Minimum Depth of Binary Tree
// URL: https://leetcode.com/problems/minimum-depth-of-binary-tree/

namespace LeetCode
{
    public class Solution
    {
        public int MinDepth(TreeNode root)
        {
            // SS: runtime complexity: O(V)
            // space complexity: O(log V), avg., O(V) worst-case
            
            if (root == null)
            {
                return 0;
            }

            int DFS(TreeNode node)
            {
                if (node.left == null && node.right == null)
                {
                    // SS: leaf node
                    return 1;
                }

                var minHeightLeft = int.MaxValue;
                if (node.left != null)
                {
                    minHeightLeft = DFS(node.left);
                }

                var minHeightRight = int.MaxValue;
                if (node.right != null)
                {
                    minHeightRight = DFS(node.right);
                }

                var minHeight = 1 + Math.Min(minHeightLeft, minHeightRight);
                return minHeight;
            }

            return DFS(root);
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