#region

using System;
using NUnit.Framework;

#endregion

// Problem: 110. Balanced Binary Tree
// URL: https://leetcode.com/problems/balanced-binary-tree/

namespace LeetCode
{
    public class Solution
    {
        public bool IsBalanced(TreeNode root)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(log N) avg.

            if (root == null)
            {
                return true;
            }

            (bool, int) DFS(TreeNode node)
            {
                if (node == null)
                {
                    return (true, 0);
                }

                (var isLeftBalanced, var leftHeight) = DFS(node.left);
                (var isRightBalanced, var rightHeight) = DFS(node.right);

                var deltaHeight = Math.Abs(leftHeight - rightHeight);

                return (isLeftBalanced && isRightBalanced && deltaHeight < 2, 1 + Math.Max(leftHeight, rightHeight));
            }

            var (isBalanced, _) = DFS(root);
            return isBalanced;
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
                var root = new TreeNode(0, new TreeNode(1, new TreeNode(3), new TreeNode(4)), new TreeNode(2, null, new TreeNode(5, new TreeNode(6))));

                // Act
                var isBalanced = new Solution().IsBalanced(root);

                // Assert
                Assert.IsFalse(isBalanced);
            }
        }
    }
}