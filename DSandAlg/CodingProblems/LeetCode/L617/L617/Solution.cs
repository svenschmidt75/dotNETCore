#region

using NUnit.Framework;

#endregion

// Problem: 617. Merge Two Binary Trees
// URL: https://leetcode.com/problems/merge-two-binary-trees/

namespace LeetCode
{
    public class Solution
    {
        public TreeNode MergeTrees(TreeNode root1, TreeNode root2)
        {
            // SS: post-order traversal, O(max(n, m))

            TreeNode DFS(TreeNode left, TreeNode right)
            {
                if (left == null)
                {
                    return right;
                }

                if (right == null)
                {
                    return left;
                }

                left.left = DFS(left.left, right.left);
                left.right = DFS(left.right, right.right);
                left.val += right.val;
                return left;
            }

            return DFS(root1, root2);
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