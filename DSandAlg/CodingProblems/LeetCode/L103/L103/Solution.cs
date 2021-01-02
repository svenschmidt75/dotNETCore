#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 103. Binary Tree Zigzag Level Order Traversal
// URL: https://leetcode.com/problems/binary-tree-zigzag-level-order-traversal/

namespace LeetCode
{
    public class Solution
    {
        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            var results = new List<IList<int>>();

            if (root == null)
            {
                return results;
            }

            var levelValues = new List<int>();

            var currentLevel = -1;

            var queue = new Queue<(TreeNode node, int level)>();
            queue.Enqueue((root, 0));

            while (queue.Any())
            {
                (var node, var level) = queue.Dequeue();

                if (currentLevel < level)
                {
                    if (level % 2 == 0)
                    {
                        // SS: reverse on odd levels
                        levelValues.Reverse();
                    }

                    levelValues = new List<int>();
                    results.Add(levelValues);

                    currentLevel = level;
                }

                levelValues.Add(node.val);

                if (node.left != null)
                {
                    queue.Enqueue((node.left, currentLevel + 1));
                }

                if (node.right != null)
                {
                    queue.Enqueue((node.right, currentLevel + 1));
                }
            }

            if (currentLevel % 2 == 1)
            {
                // SS: reverse on odd levels
                levelValues.Reverse();
            }

            return results;
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
                TreeNode root = new()
                {
                    val = 3
                    , left = new()
                    {
                        val = 9
                    }
                    , right = new()
                    {
                        val = 20
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
                var results = new Solution().ZigzagLevelOrder(root);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {3}, new[] {20, 9}, new[] {15, 7}}, results);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                TreeNode root = new()
                {
                    val = 1
                    , left = new()
                    {
                        val = 2
                        , left = new()
                        {
                            val = 4
                            , left = new()
                            {
                                val = 7
                            }
                        }
                    }
                    , right = new()
                    {
                        val = 3
                        , right = new()
                        {
                            val = 5
                            , right = new()
                            {
                                val = 8
                            }
                        }
                    }
                };

                // Act
                var results = new Solution().ZigzagLevelOrder(root);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {1}, new[] {3, 2}, new[] {4, 5}, new[] {8, 7}}, results);
            }
        }
    }
}