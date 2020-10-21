#region

using NUnit.Framework;

#endregion

// Google Coding Interview - Universal Value Tree Problem
// https://www.youtube.com/watch?v=7HgsS8bRvjo&list=PLGZgSdLg2naSIMoYuyUY86j8LqMS2fnw_&index=15

namespace UniversalValueTreeProblem
{
    public class Solution
    {
        public int NumberOfUnivalTrees(TreeNode root)
        {
            // SS: runtime complexity: O(N), N = #nodes, as we are doing a post-order traversal
            if (root == null)
            {
                // SS: empty tree is by definition a univalued tree
                return 1;
            }

            if (root.left == null && root.right == null)
            {
                // SS: a leaf is by definition univalued
                return 1;
            }

            var nLeft = root.left != null ? NumberOfUnivalTrees(root.left) : 0;
            var nRight = root.right != null ? NumberOfUnivalTrees(root.right) : 0;
            var nTotal = nLeft + nRight;

            if (root.left != null)
            {
                if (root.left.val != root.val)
                {
                    return nTotal;
                }

                if (root.right != null && root.right.val != root.val)
                {
                    return nTotal;
                }

                return nTotal + 1;
            }

            if (root.right.val == root.val)
            {
                nTotal++;
            }

            return nTotal;
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
                var l = new TreeNode(1);

                var rll = new TreeNode(1);
                var rlr = new TreeNode(1);
                var rl = new TreeNode(1, rll, rlr);

                var rr = new TreeNode();
                var r = new TreeNode(0, rl, rr);

                var root = new TreeNode(0, l, r);

                // Act
                var nUniValuedTrees = new Solution().NumberOfUnivalTrees(root);

                // Assert
                Assert.AreEqual(5, nUniValuedTrees);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var l = new TreeNode();

                var rll = new TreeNode(1);
                var rlr = new TreeNode(1);
                var rl = new TreeNode(1, rll, rlr);

                var rr = new TreeNode(1);
                var r = new TreeNode(1, rl, rr);

                var root = new TreeNode(0, l, r);

                // Act
                var nUniValuedTrees = new Solution().NumberOfUnivalTrees(root);

                // Assert
                Assert.AreEqual(6, nUniValuedTrees);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var ll = new TreeNode(1);
                var l = new TreeNode(1, ll);

                var r = new TreeNode(1);

                var root = new TreeNode(0, l, r);

                // Act
                var nUniValuedTrees = new Solution().NumberOfUnivalTrees(root);

                // Assert
                Assert.AreEqual(3, nUniValuedTrees);
            }

            [Test]
            public void Test4()
            {
                // Arrange

                // Act
                var nUniValuedTrees = new Solution().NumberOfUnivalTrees(null);

                // Assert
                Assert.AreEqual(1, nUniValuedTrees);
            }
        }
    }
}