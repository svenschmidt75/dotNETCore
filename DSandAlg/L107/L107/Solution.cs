#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 107. Binary Tree Level Order Traversal II
// URL: https://leetcode.com/problems/binary-tree-level-order-traversal-ii/

namespace LeetCode
{
    public class Solution
    {
        public IList<IList<int>> LevelOrderBottom(TreeNode root)
        {
            // SS: BF traversal

            var results = new List<IList<int>>();

            if (root == null)
            {
                return results;
            }

            var queue = new Queue<(int, TreeNode)>();
            queue.Enqueue((0, root));

            var currentLevel = 0;
            var levelValues = new List<int>();

            var stack = new Stack<IList<int>>();

            while (queue.Any())
            {
                var (level, node) = queue.Dequeue();

                if (level > currentLevel)
                {
                    currentLevel = level;
                    stack.Push(levelValues);
                    levelValues = new List<int>();
                }

                levelValues.Add(node.val);

                if (node.left != null)
                {
                    queue.Enqueue((level + 1, node.left));
                }

                if (node.right != null)
                {
                    queue.Enqueue((level + 1, node.right));
                }
            }

            stack.Push(levelValues);

            // SS: reverse
            while (stack.Any())
            {
                results.Add(stack.Pop());
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
                var root = new TreeNode(3, new TreeNode(9), new TreeNode(20, new TreeNode(15), new TreeNode(7)));

                // Act
                var results = new Solution().LevelOrderBottom(root);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {15, 7}, new[] {9, 20}, new[] {3}}, results);
            }
        }
    }
}