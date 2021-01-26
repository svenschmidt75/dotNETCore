#region

using NUnit.Framework;

#endregion

// Problem: 108. Convert Sorted Array to Binary Search Tree
// URL: https://leetcode.com/problems/convert-sorted-array-to-binary-search-tree/

namespace LeetCode
{
    public class Solution
    {
        public TreeNode SortedArrayToBST(int[] nums)
        {
            // SS: The idea is that inorder traversal of a BST results
            // in a sorted array.
            // runtime complexity: O(N)
            // space complexity: O(log N) avg.
            
            if (nums.Length == 0)
            {
                return null;
            }

            TreeNode Solve(int min, int max)
            {
                if (min == max)
                {
                    return null;
                }

                var mid = (min + max) / 2;

                var node = new TreeNode
                {
                    val = nums[mid]
                    , left = Solve(min, mid)
                    , right = Solve(mid + 1, max)
                };
                return node;
            }

            return Solve(0, nums.Length);
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
                int[] nums = {-10, -3, 0, 5, 9};

                // Act
                var node = new Solution().SortedArrayToBST(nums);

                // Assert
                Assert.AreEqual(0, node.val);

                Assert.AreEqual(-3, node.left.val);
                Assert.AreEqual(-10, node.left.left.val);
                Assert.IsNull(node.left.right);

                Assert.AreEqual(9, node.right.val);
                Assert.AreEqual(5, node.right.left.val);
                Assert.IsNull(node.right.right);
            }
        }
    }
}