#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 116. Populating Next Right Pointers in Each Node
// URL: https://leetcode.com/problems/populating-next-right-pointers-in-each-node/

namespace LeetCode
{
    public class Solution
    {
        public Node Connect(Node root)
        {
            return ConnectConstantSpace(root);
        }

        private Node ConnectConstantSpace(Node root)
        {
            // SS: Larry's solution: https://www.youtube.com/watch?v=PPFvaY4IWoI
            // runtime complexity: O(N)
            // space complexity: O(1)

            if (root == null)
            {
                return null;
            }

            var prev = root;

            while (prev != null)
            {
                var startLevel = prev.left;
                var current = startLevel;

                if (current == null)
                {
                    break;
                }

                current.next = prev.right;
                current = current.next;

                prev = prev.next;

                while (prev != null)
                {
                    current.next = prev.left;
                    current = current.next;

                    current.next = prev.right;
                    current = current.next;

                    prev = prev.next;
                }

                prev = startLevel;
            }

            return root;
        }

        private Node ConnectRecursive(Node root)
        {
            if (root == null)
            {
                return null;
            }

            void DFS(Node node)
            {
                if (node == null)
                {
                    return;
                }

                // SS: postorder traversal
                DFS(node.left);
                DFS(node.right);

                // SS: leaf node?
                if (node.left == null)
                {
                    return;
                }

                node.left.next = node.right;

                // SS: connect the middle left to right
                ConnectMiddle(node.left.right, node.right.left);
            }

            DFS(root);

            return root;
        }

        private static void ConnectMiddle(Node left, Node right)
        {
            if (left == null)
            {
                return;
            }

            left.next = right;

            ConnectMiddle(left.right, right.left);
        }

        public Node ConnectQueue(Node root)
        {
            // SS: do BF traversal
            // runtime complexity: O(N)
            // space complexity: O(N) for queue

            if (root == null)
            {
                return null;
            }

            var queue = new Queue<(Node n, int level)>();
            queue.Enqueue((root, 0));

            var currentLevel = -1;

            Node prev = null;

            while (queue.Any())
            {
                var (n, level) = queue.Dequeue();

                if (currentLevel < level)
                {
                    // SS: change of levels
                    currentLevel = level;
                    prev = n;
                }
                else
                {
                    prev.next = n;
                    prev = n;
                }

                if (n.left != null)
                {
                    queue.Enqueue((n.left, currentLevel + 1));
                }

                if (n.right != null)
                {
                    queue.Enqueue((n.right, currentLevel + 1));
                }
            }

            return root;
        }

        public class Node
        {
            public Node left;
            public Node next;
            public Node right;
            public int val;

            public Node() { }

            public Node(int _val)
            {
                val = _val;
            }

            public Node(int _val, Node _left, Node _right, Node _next)
            {
                val = _val;
                left = _left;
                right = _right;
                next = _next;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                Node root = new()
                {
                    val = 1
                    , left = new()
                    {
                        val = 2
                        , left = new()
                        {
                            val = 4
                        }
                        , right = new()
                        {
                            val = 5
                        }
                    }
                    , right = new()
                    {
                        val = 3
                        , left = new()
                        {
                            val = 6
                        }
                        , right = new()
                        {
                            val = 7
                        }
                    }
                };

                // Act
                var n = new Solution().Connect(root);

                // Assert
                Assert.IsNull(n.next);
                Assert.AreSame(n.left.next, n.right);
                Assert.AreSame(n.left.left.next, n.left.right);
                Assert.AreSame(n.left.right.next, n.right.left);
                Assert.AreSame(n.right.left.next, n.right.right);
                Assert.IsNull(n.right.next);
                Assert.IsNull(n.right.right.next);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                Node root = new()
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
                var n = new Solution().Connect(root);

                // Assert
                Assert.IsNull(n.next);
                Assert.AreSame(n.left.next, n.right);
                Assert.AreSame(n.left.left.next, n.left.right);
                Assert.AreSame(n.left.right.next, n.right.left);
                Assert.AreSame(n.right.left.next, n.right.right);
                Assert.AreSame(n.left.right.right.next, n.right.left.left);
            }
        }
    }
}