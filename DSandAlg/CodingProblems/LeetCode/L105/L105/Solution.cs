#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 105. Construct Binary Tree from Preorder and Inorder Traversal
// URL: https://leetcode.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/

namespace LeetCode
{
    public class Solution
    {
        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            // SS: In preorder traversal, we first visit the current node, then left and
            // right subtree. Hence, the current root node is always given by the first node
            // in the preorder traversal. Once we have the root node, we need to determine
            // both left and right subtrees. Those are given by the inorder traversal, as
            // everything to the left of the root node is the left subtree and everything to
            // the right is the right subtree.

            // SS: runtime complexity: O(N) when using a hashmap to lookup inorder values,
            // otherwise search in linear, so total runtime complexity would be O(N^2) in the
            // worst case.

            if (preorder.Length == 0)
            {
                return null;
            }

            // SS: to speed up search, use a hash map for fast lookup
            // We assume that all node values are unique...
            var hashMap = new Dictionary<int, int>();
            for (var i = 0; i < inorder.Length; i++)
            {
                hashMap[inorder[i]] = i;
            }

            TreeNode Build(int preorderMin, int preorderMax, int inorderMin, int inorderMax)
            {
                // SS: termination condition for recursion
                if (preorderMin == preorderMax)
                {
                    return null;
                }

                var rootVal = preorder[preorderMin];
                var inorderIdx = hashMap[rootVal];

                var leftSubtreeLength = inorderIdx - inorderMin;

                var newLeftPreorderMax = preorderMin + 1 + leftSubtreeLength;
                var newLeftInorderMax = inorderMin + leftSubtreeLength;
                var newRightInorderMin = newLeftInorderMax + 1;

                var node = new TreeNode
                {
                    val = rootVal
                    , left = Build(preorderMin + 1, newLeftPreorderMax, inorderMin, newLeftInorderMax)
                    , right = Build(newLeftPreorderMax, preorderMax, newRightInorderMin, inorderMax)
                };

                return node;
            }

            var root = Build(0, preorder.Length, 0, inorder.Length);
            return root;
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
                int[] preorder = {3, 9, 20, 15, 7};
                int[] inorder = {9, 3, 15, 20, 7};

                // Act
                var root = new Solution().BuildTree(preorder, inorder);

                // Assert
                Assert.AreEqual(3, root.val);
                Assert.AreEqual(9, root.left.val);
                Assert.AreEqual(20, root.right.val);
                Assert.AreEqual(15, root.right.left.val);
                Assert.AreEqual(7, root.right.right.val);
            }
        }
    }
}