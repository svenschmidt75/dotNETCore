#region

using System;
using System.Collections.Generic;
using System.Linq;
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
                // SS: O(log n) stack space and O(n) minheap
                return value;
            }

            private void DFS(TreeNode node, BinaryHeap<int> minHeap)
            {
                if (node == null)
                {
                    return;
                }

                // SS: pre-order
                minHeap.Push(node.val);

                TestContext.Progress.WriteLine(node.val);

                DFS(node.left, minHeap);
                DFS(node.right, minHeap);
            }

            public int KthSmallest2(TreeNode root, int k)
            {
                // SS: faster solution, O(n), but more complicated.
                // Still O(log n) stack size
                var (nk, val) = KthSmallest2Recursive(root, k, 0);
                return val;
            }

            private (int k, int val) KthSmallest2Recursive(TreeNode node, int k, int i)
            {
                if (node == null)
                {
                    return (i, -1);
                }

                var val = node.val;

                Console.WriteLine(val);

                if (node.left == null && node.right != null)
                {
                    // increase
                    var currentK = i + 1;
                    if (currentK == k)
                    {
                        return (k, node.val);
                    }

                    var (rightK, rightVal) = KthSmallest2Recursive(node.right, k, currentK);
                    if (rightK == k)
                    {
                        return (k, rightVal);
                    }

                    return (rightK, -1);
                }

                if (node.left != null)
                {
                    var (leftK, leftVal) = KthSmallest2Recursive(node.left, k, i);
                    if (leftK == k)
                    {
                        return (k, leftVal);
                    }

                    var currentK = leftK + 1;
                    if (currentK == k)
                    {
                        return (k, node.val);
                    }

                    var maxK = currentK;

                    if (node.right != null)
                    {
                        var (rightK, rightVal) = KthSmallest2Recursive(node.right, k, currentK);
                        if (rightK == k)
                        {
                            return (k, rightVal);
                        }

                        maxK = rightK;
                    }

                    return (maxK, -1);
                }

                if (node.left == null && node.right == null)
                {
                    // increase
                    var currentK = i + 1;
                    if (currentK == k)
                    {
                        return (k, node.val);
                    }

                    return (currentK, -1);
                }

                return (i, k);
            }

            public int KthSmallest3(TreeNode root, int k)
            {
                // SS: iterative inorder traversal
                var stack = new Stack<TreeNode>();

                var node = root;

                while (true)
                {
                    // SS: find the next smallest node
                    while (node != null)
                    {
                        stack.Push(node);
                        node = node.left;
                    }

                    if (stack.Any() == false)
                    {
                        break;
                    }

                    node = stack.Pop();

                    if (--k == 0)
                    {
                        TestContext.WriteLine($"kth element: {node.val}");
                        return node.val;
                    }

                    node = node.right;
                }

                return -1;
            }
        }
    }


    [TestFixture]
    public class Tests
    {
        private TreeNode CreateTree(int[] vals)
        {
            var root = new TreeNode {val = vals[0]};

            var q = new Queue<TreeNode>();
            q.Enqueue(root);

            var i = 1;

            while (q.Any())
            {
                var p = q.Dequeue();

                if (i < vals.Length)
                {
                    var leftVal = vals[i];
                    if (leftVal != 1000)
                    {
                        var leftChild = new TreeNode {val = leftVal};
                        p.left = leftChild;
                        q.Enqueue(leftChild);
                    }

                    i++;
                }
                else
                {
                    break;
                }

                if (i < vals.Length)
                {
                    var rightVal = vals[i];
                    if (rightVal != 1000)
                    {
                        var rightChild = new TreeNode {val = rightVal};
                        p.right = rightChild;
                        q.Enqueue(rightChild);
                    }

                    i++;
                }
                else
                {
                    break;
                }
            }

            return root;
        }

        [Test]
        public void Test11()
        {
            // Arrange
            var tree = CreateTree(new[] {3, 1, 4, 1000, 2});

            // Act
            var value = new TreeNode.Solution().KthSmallest1(tree, 1);

            // Assert
            Assert.AreEqual(1, value);
        }

        [Test]
        public void Test12()
        {
            // Arrange
            var tree = CreateTree(new[] {3, 1, 4, 1000, 2});

            // Act
            var value = new TreeNode.Solution().KthSmallest2(tree, 1);

            // Assert
            Assert.AreEqual(1, value);
        }

        [Test]
        public void Test13()
        {
            // Arrange
            var tree = CreateTree(new[] {3, 1, 4, 1000, 2});

            // Act
            var value = new TreeNode.Solution().KthSmallest3(tree, 1);

            // Assert
            Assert.AreEqual(1, value);
        }

        [Test]
        public void Test21()
        {
            // Arrange
            var tree = CreateTree(new[] {5, 3, 6, 2, 4, 1000, 1000, 1});

            // Act
            var value = new TreeNode.Solution().KthSmallest1(tree, 3);

            // Assert
            Assert.AreEqual(3, value);
        }

        [Test]
        public void Test22()
        {
            // Arrange
            var tree = CreateTree(new[] {5, 3, 6, 2, 4, 1000, 1000, 1});

            // Act
            var value = new TreeNode.Solution().KthSmallest2(tree, 3);

            // Assert
            Assert.AreEqual(3, value);
        }

        [Test]
        public void Test23()
        {
            // Arrange
            var tree = CreateTree(new[] {5, 3, 6, 2, 4, 1000, 1000, 1});

            // Act
            var value = new TreeNode.Solution().KthSmallest3(tree, 6);

            // Assert
            Assert.AreEqual(6, value);
        }

        [Test]
        public void Test31()
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

        [Test]
        public void Test32()
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
            var value = new TreeNode.Solution().KthSmallest2(tree, 32);

            // Assert
            Assert.AreEqual(31, value);
        }

        [Test]
        public void Test33()
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
            var value = new TreeNode.Solution().KthSmallest3(tree, 32);

            // Assert
            Assert.AreEqual(31, value);
        }

        [Test]
        public void Test42()
        {
            // Arrange
            var tree = CreateTree(new[]
            {
                0, 1000, 1, 1000, 3, 2
            });

            // Act
            var value = new TreeNode.Solution().KthSmallest2(tree, 3);

            // Assert
            Assert.AreEqual(2, value);
        }

        [Test]
        public void Test43()
        {
            // Arrange
            var tree = CreateTree(new[]
            {
                0, 1000, 1, 1000, 3, 2
            });

            // Act
            var value = new TreeNode.Solution().KthSmallest3(tree, 3);

            // Assert
            Assert.AreEqual(2, value);
        }
    }
}