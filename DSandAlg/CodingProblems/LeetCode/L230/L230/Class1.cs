#region

using System;
using NUnit.Framework;

#endregion

namespace L230
{
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

        public class Solution
        {
            public int KthSmallest1(TreeNode root, int k)
            {
                var minHeap = BinaryHeap<int>.CreateHeap((p1, p2) => p1 > p2);

                // SS: O(n * log n)
                // n vertices, each pushed into min heap at log n each
                DFS(root, minHeap);

                // SS: O(k log n)
                var value = -1;
                var n = 0;
                while (n < k)
                {
                    value = minHeap.Pop();
                    n++;
                }

                // SS: total O(n log n)
                // SS: O(log n) stack space
                return value;
            }

            private void DFS(TreeNode node, BinaryHeap<int> minHeap)
            {
                if (node == null)
                {
                    return;
                }

                minHeap.Push(node.val);

                Console.WriteLine(node.val);

                DFS(node.left, minHeap);
                DFS(node.right, minHeap);
            }
        }
    }


    [TestFixture]
    public class Tests
    {
        private TreeNode CreateTree(int[] vals)
        {
            var nodes = new TreeNode[vals.Length];

            for (var i = 0; i < vals.Length; i++)
            {
                var val = vals[i];
                if (val == -1)
                {
                    continue;
                }

                var node = new TreeNode {val = val};
                nodes[i] = node;
            }

            for (var i = 0; i < vals.Length; i++)
            {
                var parent = nodes[i];
                if (parent == null)
                {
                    continue;
                }

                var left = 2 * i + 1;
                if (left < vals.Length)
                {
                    parent.left = nodes[left];
                }

                var right = 2 * i + 2;
                if (right < vals.Length)
                {
                    parent.right = nodes[right];
                }
            }

            return nodes[0];
        }

        [Test]
        public void Test1()
        {
            // Arrange
            var tree = CreateTree(new[] {3, 1, 4, 1000, 2});

            // Act
            var value = new TreeNode.Solution().KthSmallest1(tree, 1);

            // Assert
            Assert.AreEqual(1, value);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var tree = CreateTree(new[] {5, 3, 6, 2, 4, 1000, 1000, 1});

            // Act
            var value = new TreeNode.Solution().KthSmallest1(tree, 3);

            // Assert
            Assert.AreEqual(3, value);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var tree = CreateTree(new[]
            {
                45, 30, 46, 10, 36, 1000, 49, 8, 24, 34, 42, 48, 1000, 4, 9, 14, 25, 31, 35, 41, 43, 47, 1000, 0, 6
                , 1000, 1000, 11, 20, 1000, 28, 1000, 33, 1000, 1000, 37, 1000, 1000, 44, 1000, 1000, 1000, 1, 5, 7
                , 1000, 12, 19, 21, 26, 29, 32, 1000, 1000, 38, 1000, 1000, 1000, 3, 1000, 1000, 1000, 1000, 1000, 13
                , 18, 1000, 1000, 22, 1000, 27, 1000, 1000, 1000, 1000, 1000, 39, 2, 1000, 1000, 1000, 15, 1000, 1000
                , 23, 1000, 1000, 1000, 40, 1000, 1000, 1000, 16, 1000, 1000, 1000, 1000, 1000, 17
            });

            // Act
            var value = new TreeNode.Solution().KthSmallest1(tree, 32);

            // Assert
            Assert.AreEqual(31, value);
        }
    }
}