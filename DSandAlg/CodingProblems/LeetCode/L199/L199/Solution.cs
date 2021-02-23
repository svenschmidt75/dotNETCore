#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 199. Binary Tree Right Side View
// URL: https://leetcode.com/problems/binary-tree-right-side-view/

namespace LeetCode
{
    public class Solution
    {
        public IList<int> RightSideView(TreeNode root)
        {
            // SS: runtime complexity: O(n), due to DF traversal
            // space complexity: O(n) (hash map, call stack less)

            var result = new List<int>();

            if (root == null)
            {
                return result;
            }

            var depth = -1;

            var map = new Dictionary<int, TreeNode>();

            void DFS(TreeNode node, int level)
            {
                if (node == null)
                {
                    return;
                }

                // SS: keep track of the depth of the tree
                depth = Math.Max(depth, level);

                if (map.ContainsKey(level) == false)
                {
                    map[level] = node;
                }

                // SS: visit right child first
                DFS(node.right, level + 1);
                DFS(node.left, level + 1);
            }

            DFS(root, 0);

            // SS: extract the nodes...
            for (var i = 0; i <= depth; i++)
            {
                var node = map[i];
                result.Add(node.val);
            }

            return result;
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
                        , right = new TreeNode
                        {
                            val = 5
                        }
                    }
                    , right = new TreeNode
                    {
                        val = 3
                        , right = new TreeNode
                        {
                            val = 4
                        }
                    }
                };

                // Act
                var results = new Solution().RightSideView(root);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 3, 4}, results);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 1
                    , right = new TreeNode
                    {
                        val = 3
                    }
                };

                // Act
                var results = new Solution().RightSideView(root);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 3}, results);
            }

            [Test]
            public void Test3()
            {
                // Arrange

                // Act
                var results = new Solution().RightSideView(null);

                // Assert
                Assert.IsEmpty(results);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 1
                    , left = new TreeNode
                    {
                        val = 2
                        , left = new TreeNode
                        {
                            val = 4
                        }
                        , right = new TreeNode
                        {
                            val = 5
                            , left = new TreeNode
                            {
                                val = 7
                            }
                            , right = new TreeNode
                            {
                                val = 8
                            }
                        }
                    }
                    , right = new TreeNode
                    {
                        val = 3
                        , left = new TreeNode
                        {
                            val = 6
                        }
                    }
                };

                // Act
                var results = new Solution().RightSideView(root);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 3, 6, 8}, results);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 1
                    , left = new TreeNode
                    {
                        val = 2
                        , right = new TreeNode
                        {
                            val = 5
                            , left = new TreeNode
                            {
                                val = 6
                            }
                        }
                    }
                    , right = new TreeNode
                    {
                        val = 3
                        , right = new TreeNode
                        {
                            val = 4
                        }
                    }
                };

                // Act
                var results = new Solution().RightSideView(root);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 3, 4, 6}, results);
            }
        }
    }
}