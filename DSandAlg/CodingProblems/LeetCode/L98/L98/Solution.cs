#region

using NUnit.Framework;

#endregion

// Problem: 98. Validate Binary Search Tree
// URL: https://leetcode.com/problems/validate-binary-search-tree/

namespace LeetCode
{
    public class Solution
    {
        public bool IsValidBST(TreeNode root)
        {
            bool IsValid(TreeNode node, int min, int max)
            {
                if (node == null)
                {
                    // SS: the empty tree is a valid BST
                    return true;
                }

                if (node.val <= min || node.val >= max)
                {
                    return false;
                }

                return IsValid(node.left, min, node.val) && IsValid(node.right, node.val, max);
            }

            return IsValid(root, int.MinValue, int.MaxValue);
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
                var root = new TreeNode {val = 2, left = new TreeNode {val = 1}, right = new TreeNode {val = 3}};

                // Act
                var isValid = new Solution().IsValidBST(root);

                // Assert
                Assert.True(isValid);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 5
                    , left = new TreeNode {val = 1}
                    , right = new TreeNode
                    {
                        val = 4
                        , left = new TreeNode {val = 3}
                        , right = new TreeNode {val = 6}
                    }
                };

                // Act
                var isValid = new Solution().IsValidBST(root);

                // Assert
                Assert.False(isValid);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 5
                    , left = new TreeNode {val = 4}
                    , right = new TreeNode
                    {
                        val = 6
                        , left = new TreeNode {val = 3}
                        , right = new TreeNode {val = 7}
                    }
                };

                // Act
                var isValid = new Solution().IsValidBST(root);

                // Assert
                Assert.False(isValid);
            }
        }
    }
}