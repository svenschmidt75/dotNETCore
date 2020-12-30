#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 102. Binary Tree Level Order Traversal
// URL: https://leetcode.com/problems/binary-tree-level-order-traversal/

namespace LeetCode
{
    public class Solution
    {
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            var results = new List<IList<int>>();
            if (root == null)
            {
                return results;
            }

            var level = 0;
            var queue = new Queue<(TreeNode node, int level)>();
            queue.Enqueue((root, 0));

            var levelValues = new List<int>();
            results.Add(levelValues);

            while (queue.Any())
            {
                var (node, nodeLevel) = queue.Dequeue();

                if (nodeLevel > level)
                {
                    levelValues = new List<int>();
                    results.Add(levelValues);
                    level = nodeLevel;
                }

                levelValues.Add(node.val);

                if (node.left != null)
                {
                    queue.Enqueue((node.left, level + 1));
                }

                if (node.right != null)
                {
                    queue.Enqueue((node.right, level + 1));
                }
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
                TreeNode root = new() {val = 3, left = new() {val = 9}, right = new() {val = 20, left = new() {val = 15}, right = new() {val = 7}}};

                // Act
                var results = new Solution().LevelOrder(root);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {3}, new[] {9, 20}, new[] {15, 7}}, results);
            }
            
            [Test]
            public void Test2()
            {
                // Arrange
                TreeNode root = new() {val = 3, left = new() {val = 9}, right = new() {val = 20, left = new() {val = 15}}};

                // Act
                var results = new Solution().LevelOrder(root);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {3}, new[] {9, 20}, new[] {15}}, results);
            }
            
            [Test]
            public void Test3()
            {
                // Arrange
                TreeNode root = new() {val = 3, left = new() {val = 9}, right = new() {val = 20, right = new() {val = 7}}};

                // Act
                var results = new Solution().LevelOrder(root);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {3}, new[] {9, 20}, new[] {7}}, results);
            }
            
        }
    }
}