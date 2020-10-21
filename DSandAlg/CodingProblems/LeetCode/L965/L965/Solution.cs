#region

using NUnit.Framework;

#endregion

namespace L965
{
    public class Solution
    {
        public bool IsUnivalTree(TreeNode root)
        {
            // SS: runtime complexity: O(N), where N is the number of nodes
            // Note: we could also extract the node values and check that they
            // are all the same, at the expense of increased memory, etc.

            if (root == null)
            {
                return false;
            }

            // SS: leaf?
            if (root.left == null && root.right == null)
            {
                return true;
            }

            if (root.left != null)
            {
                if (IsUnivalTree(root.left) == false || root.val != root.left.val)
                {
                    return false;
                }
            }

            if (root.right != null)
            {
                if (IsUnivalTree(root.right) == false || root.val != root.right.val)
                {
                    return false;
                }
            }

            return true;
        }

        // Definition for a binary tree node.
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
                var ll = new TreeNode(1);
                var lr = new TreeNode(1);
                var l = new TreeNode(1, ll, lr);

                var rr = new TreeNode(1);
                var r = new TreeNode(1, null, rr);

                var root = new TreeNode(1, l, r);

                // Act
                var isUniValued = new Solution().IsUnivalTree(root);

                // Assert
                Assert.AreEqual(true, isUniValued);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var ll = new TreeNode(5);
                var lr = new TreeNode(2);
                var l = new TreeNode(2, ll, lr);

                var r = new TreeNode(2);

                var root = new TreeNode(2, l, r);

                // Act
                var isUniValued = new Solution().IsUnivalTree(root);

                // Assert
                Assert.AreEqual(false, isUniValued);
            }
        }
    }
}