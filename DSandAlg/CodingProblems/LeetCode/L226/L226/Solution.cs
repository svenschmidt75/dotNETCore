#region

using NUnit.Framework;

#endregion

// Problem: 226. Invert Binary Tree
// URL: https://leetcode.com/problems/invert-binary-tree/

namespace LeetCode
{
    public class Solution
    {
        public TreeNode InvertTree(TreeNode root)
        {
            // SS: runtime complexity of DFS: O(V)
            // space complexity: O(depth of tree)
            
            TreeNode DFS(TreeNode original)
            {
                if (original == null)
                {
                    return null;
                }

                TreeNode dup = new TreeNode(original.val)
                {
                    left = DFS(original.right)
                    , right = DFS(original.left)
                };

                return dup;
            }

            TreeNode inverted = DFS(root);
            return inverted;
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
                TreeNode root = new TreeNode
                {
                    val = 4
                    , left = new TreeNode
                    {
                        val = 2
                        , left = new TreeNode(1)
                        , right = new TreeNode(3)
                    }
                    , right = new TreeNode
                    {
                        val = 7
                        , left = new TreeNode(6)
                        , right = new TreeNode(9)
                    }
                };

                // Act
                TreeNode inverted = new Solution().InvertTree(root);

                // Assert
                Assert.AreEqual(root.val, inverted.val);

                Assert.AreEqual(root.left.val, inverted.right.val);
                Assert.AreEqual(root.left.left.val, inverted.right.right.val);
                Assert.AreEqual(root.left.right.val, inverted.right.left.val);
                
                Assert.AreEqual(root.right.val, inverted.left.val);
                Assert.AreEqual(root.right.right.val, inverted.left.left.val);
                Assert.AreEqual(root.right.left.val, inverted.left.right.val);
            }
        }
    }
}