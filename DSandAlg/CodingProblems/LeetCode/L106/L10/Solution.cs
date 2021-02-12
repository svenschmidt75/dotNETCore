#region

using System;
using NUnit.Framework;

#endregion

// Problem: 106. Construct Binary Tree from Inorder and Postorder Traversal
// URL: https://leetcode.com/problems/construct-binary-tree-from-inorder-and-postorder-traversal/

namespace LeetCode
{
    public class Solution
    {
        public TreeNode BuildTree(int[] inorder, int[] postorder)
        {
            if (inorder.Length == 0 || inorder.Length != postorder.Length)
            {
                return null;
            }

            TreeNode Build(int ioMin, int ioMax, int poMin, int poMax)
            {
                var root = postorder[poMax];
                var node = new TreeNode(root);

                if (poMin == poMax)
                {
                    return node;
                }

                // SS: determine left and right subtrees
                var idx = Array.FindIndex(inorder, x => x == root);

                var leftSize = idx - ioMin;
                if (leftSize > 0)
                {
                    node.left = Build(ioMin, idx - 1, poMin, poMin + leftSize - 1);
                }

                var rightSize = ioMax - idx;
                if (rightSize > 0)
                {
                    node.right = Build(idx + 1, idx + rightSize, poMin + leftSize, poMin + leftSize + rightSize - 1);
                }

                return node;
            }

            return Build(0, inorder.Length - 1, 0, postorder.Length - 1);
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
                int[] inorder = {9, 3, 15, 20, 7};
                int[] postorder = {9, 15, 7, 20, 3};

                // Act
                var root = new Solution().BuildTree(inorder, postorder);

                // Assert
                Assert.AreEqual(3, root.val);
                Assert.AreEqual(9, root.left.val);
                Assert.AreEqual(20, root.right.val);
                Assert.AreEqual(15, root.right.left.val);
                Assert.AreEqual(7, root.right.right.val);
            }
            
            [Test]
            public void Test2()
            {
                // Arrange
                int[] inorder = {9};
                int[] postorder = {9};

                // Act
                var root = new Solution().BuildTree(inorder, postorder);

                // Assert
                Assert.AreEqual(9, root.val);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] inorder = {2, 1};
                int[] postorder = {2, 1};

                // Act
                var root = new Solution().BuildTree(inorder, postorder);

                // Assert
                Assert.AreEqual(1, root.val);
                Assert.AreEqual(2, root.left.val);
            }
            
            [Test]
            public void Test4()
            {
                // Arrange
                int[] inorder = {1, 2};
                int[] postorder = {2, 1};

                // Act
                var root = new Solution().BuildTree(inorder, postorder);

                // Assert
                Assert.AreEqual(1, root.val);
                Assert.AreEqual(2, root.right.val);
            }
            
        }
    }
}