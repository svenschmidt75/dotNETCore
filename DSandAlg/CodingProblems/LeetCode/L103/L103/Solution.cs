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
            // SS: Larry's solution using 2 queues
            // https://www.youtube.com/watch?v=m3k1tpmaOYc

            var results = new List<IList<int>>();

            if (root == null)
            {
                return results;
            }

            var queue1 = new Queue<TreeNode>();
            queue1.Enqueue(root);

            var queue2 = new Queue<TreeNode>();

            while (true)
            {
                results.Add(new List<int>());

                while (queue1.Any())
                {
                    var node = queue1.Dequeue();

                    results[^1].Add(node.val);

                    if (node.left != null)
                    {
                        queue2.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        queue2.Enqueue(node.right);
                    }
                }

                if (queue2.Any() == false)
                {
                    break;
                }

                queue1 = queue2;
                queue2 = new Queue<TreeNode>();
            }

            // SS: reverse every other
            for (var i = 1; i < results.Count; i += 2)
            {
                var l = (List<int>) results[i];
                l.Reverse();
            }

            return results;
        }

        public IList<IList<int>> ZigzagLevelOrder2(TreeNode root)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(n)

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
                var (node, level) = queue.Dequeue();

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