#region

using NUnit.Framework;

#endregion

// Problem: 112. Path Sum
// URL: https://leetcode.com/problems/path-sum/

namespace LeetCode
{
    public class Solution
    {
        public bool HasPathSum(TreeNode root, int targetSum)
        {
            if (root == null)
            {
                return false;
            }

            bool PreOrder(TreeNode node, int partialSum)
            {
                if (node.left == null && node.right == null)
                {
                    return partialSum + node.val == targetSum;
                }

                var val = partialSum + node.val;
                return node.left != null && PreOrder(node.left, val) || node.right != null && PreOrder(node.right, val);
            }

            return PreOrder(root, 0);
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