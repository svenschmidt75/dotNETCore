#region

using System;
using NUnit.Framework;

#endregion

// Problem: 124. Binary Tree Maximum Path Sum
// URL: https://leetcode.com/problems/binary-tree-maximum-path-sum/

namespace LeetCode
{
    public class Solution
    {
        public int MaxPathSum(TreeNode root)
        {
            // SS: O(N) using postorder traversal

            var maxSumParent = int.MinValue;

            int DFS(TreeNode node)
            {
                if (node == null)
                {
                    return 0;
                }

                var leftSum = DFS(node.left);
                var rightSum = DFS(node.right);

                // SS: Possible paths:
                // 1. left child -> parent
                // 2. right child -> parent
                // 3. left child -> parent -> right child
                // 4. parent

                // SS: best path from child to parent
                var childParent = Math.Max(Math.Max(leftSum, rightSum), 0) + node.val;
                maxSumParent = Math.Max(maxSumParent, childParent);

                // SS: check path from left child to parent to right child
                maxSumParent = Math.Max(maxSumParent, leftSum + node.val + rightSum);

                return childParent;
            }

            var sum = DFS(root);
            var maxSum = Math.Max(sum, maxSumParent);
            return maxSum;
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
                var root = new TreeNode
                {
                    val = 1
                    , left = new TreeNode
                    {
                        val = 2
                    }
                    , right = new TreeNode
                    {
                        val = 3
                    }
                };

                // Act
                var maxSum = new Solution().MaxPathSum(root);

                // Assert
                Assert.AreEqual(6, maxSum);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = -10
                    , left = new TreeNode
                    {
                        val = 9
                    }
                    , right = new TreeNode
                    {
                        val = 20
                        , left = new TreeNode
                        {
                            val = 15
                        }
                        , right = new TreeNode
                        {
                            val = 7
                        }
                    }
                };

                // Act
                var maxSum = new Solution().MaxPathSum(root);

                // Assert
                Assert.AreEqual(42, maxSum);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                TreeNode root = new()
                {
                    val = -1
                    , left = new()
                    {
                        val = 0
                        , left = new()
                        {
                            val = 2
                            , left = new()
                            {
                                val = 6
                            }
                            , right = new()
                            {
                                val = 7
                            }
                        }
                        , right = new()
                        {
                            val = 3
                            , left = new()
                            {
                                val = 8
                            }
                            , right = new()
                            {
                                val = 9
                            }
                        }
                    }
                    , right = new()
                    {
                        val = 1
                        , left = new()
                        {
                            val = 4
                            , left = new()
                            {
                                val = 10
                            }
                            , right = new()
                            {
                                val = 11
                            }
                        }
                        , right = new()
                        {
                            val = 5
                            , left = new()
                            {
                                val = 12
                            }
                            , right = new()
                            {
                                val = 13
                            }
                        }
                    }
                };

                // Act
                var maxSum = new Solution().MaxPathSum(root);

                // Assert
                Assert.AreEqual(34, maxSum);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                TreeNode root = new()
                {
                    val = 7
                    , left = new()
                    {
                        val = 9
                    }
                    , right = new()
                    {
                        val = 3
                        , left = new()
                        {
                            val = 15
                        }
                        , right = new()
                        {
                            val = 7
                        }
                    }
                };

                // Act
                var maxSum = new Solution().MaxPathSum(root);

                // Assert
                Assert.AreEqual(34, maxSum);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 2
                    , left = new TreeNode
                    {
                        val = -1
                    }
                    , right = new TreeNode
                    {
                        val = -2
                    }
                };

                // Act
                var maxSum = new Solution().MaxPathSum(root);

                // Assert
                Assert.AreEqual(2, maxSum);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = -1
                    , left = new TreeNode
                    {
                        val = -2
                        , left = new TreeNode
                        {
                            val = -6
                        }
                    }
                    , right = new TreeNode
                    {
                        val = 10
                        , left = new TreeNode
                        {
                            val = -3
                        }
                        , right = new TreeNode
                        {
                            val = -6
                        }
                    }
                };

                // Act
                var maxSum = new Solution().MaxPathSum(root);

                // Assert
                Assert.AreEqual(10, maxSum);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = -1
                    , right = new TreeNode
                    {
                        val = 9
                        , left = new TreeNode
                        {
                            val = -6
                        }
                        , right = new TreeNode
                        {
                            val = 3
                            , right = new TreeNode
                            {
                                val = -2
                            }
                        }
                    }
                };

                // Act
                var maxSum = new Solution().MaxPathSum(root);

                // Assert
                Assert.AreEqual(12, maxSum);
            }
        }
    }
}