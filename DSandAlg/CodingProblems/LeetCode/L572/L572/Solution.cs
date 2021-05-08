#region

using NUnit.Framework;

#endregion

// Problem: 572. Subtree of Another Tree
// URL: https://leetcode.com/problems/subtree-of-another-tree/

namespace LeetCode
{
    public class Solution
    {
        public bool IsSubtree(TreeNode root, TreeNode subRoot)
        {
            return IsSubtree1(root, subRoot);
        }

        private static bool IsSubtree1(TreeNode root, TreeNode subRoot)
        {
            bool CheckSubTree(TreeNode n1, TreeNode n2)
            {
                if (n1 == null)
                {
                    return n2 == null;
                }

                if (n2 == null)
                {
                    return false;
                }

                return n1.val == n2.val && CheckSubTree(n1.left, n2.left) && CheckSubTree(n1.right, n2.right);
            }

            bool DFS(TreeNode mainNode, TreeNode subNode)
            {
                if (mainNode == null)
                {
                    return subNode == null;
                }

                if (subNode == null)
                {
                    return false;
                }

                return mainNode.val == subNode.val && CheckSubTree(mainNode, subNode) || DFS(mainNode.left, subRoot) || DFS(mainNode.right, subRoot);
            }

            return DFS(root, subRoot);
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
                var subTree = new TreeNode(4)
                {
                    left = new TreeNode(1)
                    , right = new TreeNode(2)
                };

                var root = new TreeNode(3)
                {
                    left = subTree
                    , right = new TreeNode(5)
                };

                // Act
                var result = new Solution().IsSubtree(root, subTree);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var subTree = new TreeNode(4)
                {
                    left = new TreeNode(1)
                    , right = new TreeNode(2)
                };

                var root = new TreeNode(4)
                {
                    left = new TreeNode(1)
                    {
                        left = subTree
                        , right = new TreeNode(7)
                    }
                    , right = new TreeNode(2)
                };

                // Act
                var result = new Solution().IsSubtree(root, subTree);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var subTree = new TreeNode(4)
                {
                    left = new TreeNode(1)
                    , right = new TreeNode(2)
                };

                var root = new TreeNode(3)
                {
                    left = new TreeNode(4)
                    {
                        left = new TreeNode(1)
                        , right = new TreeNode(2)
                        {
                            left = new TreeNode()
                        }
                    }
                    , right = new TreeNode(5)
                };

                // Act
                var result = new Solution().IsSubtree(root, subTree);

                // Assert
                Assert.False(result);
            }
        }
    }
}