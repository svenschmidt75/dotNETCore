#region

using NUnit.Framework;

#endregion

// Problem: 117. Populating Next Right Pointers in Each Node II
// URL: https://leetcode.com/problems/populating-next-right-pointers-in-each-node-ii/

namespace LeetCode
{
    public class Solution
    {
        public Node Connect(Node root)
        {
            if (root == null)
            {
                return null;
            }

            var parent = root;
            Node levelStart = null;
            Node prev = null;
            while (true)
            {
                var node = parent.left;
                if (node != null)
                {
                    if (levelStart == null)
                    {
                        levelStart = node;
                    }

                    if (prev == null)
                    {
                        prev = node;
                    }
                    else
                    {
                        prev.next = node;
                        prev = node;
                    }
                }

                node = parent.right;
                if (node != null)
                {
                    if (levelStart == null)
                    {
                        levelStart = node;
                    }

                    if (prev == null)
                    {
                        prev = node;
                    }
                    else
                    {
                        prev.next = node;
                        prev = node;
                    }
                }

                // next parent
                var nextParent = parent.next;
                if (nextParent == null)
                {
                    parent = levelStart;
                    levelStart = null;
                    prev = null;
                }
                else
                {
                    parent = nextParent;
                }

                if (parent == null)
                {
                    break;
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
                var root = new Node
                {
                    val = 1
                    , left = new Node
                    {
                        val = 2
                        , left = new Node(4)
                        , right = new Node(5)
                    }
                    , right = new Node
                    {
                        val = 3
                        , right = new Node(7)
                    }
                };

                // Act
                var node = new Solution().Connect(root);

                // Assert
                Assert.IsNull(node.next);
                Assert.AreEqual(3, node.left.next.val);
                Assert.IsNull(node.right.next);
                Assert.AreEqual(5, node.left.left.next.val);
                Assert.AreEqual(7, node.left.right.next.val);
                Assert.IsNull(node.right.right.next);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var root = new Node
                {
                    val = 1
                    , left = new Node
                    {
                        val = 2
                        , left = new Node(4)
                        , right = new Node(5)
                    }
                    , right = new Node
                    {
                        val = 3
                        , left = new Node(6)
                        , right = new Node(7)
                    }
                };

                // Act
                var node = new Solution().Connect(root);

                // Assert
                Assert.IsNull(node.next);

                Assert.AreEqual(3, node.left.next.val);
                Assert.IsNull(node.right.next);

                Assert.AreEqual(5, node.left.left.next.val);
                Assert.AreEqual(6, node.left.right.next.val);
                Assert.AreEqual(7, node.right.left.next.val);
                Assert.IsNull(node.right.right.next);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var root = new Node
                {
                    val = 1
                    , left = new Node
                    {
                        val = 2
                        , left = new Node
                        {
                            val = 3
                            , right = new Node
                            {
                                val = 5
                                , left = new Node
                                {
                                    val = 7
                                }
                            }
                        }
                    }
                    , right = new Node
                    {
                        val = 10
                        , left = new Node
                        {
                            val = 11
                            , right = new Node
                            {
                                val = 12
                                , left = new Node
                                {
                                    val = 13
                                }
                                , right = new Node
                                {
                                    val = 14
                                    , right = new Node
                                    {
                                        val = 15
                                    }
                                }
                            }
                        }
                    }
                };

                // Act
                var node = new Solution().Connect(root);

                // Assert
                Assert.IsNull(node.next);

                Assert.AreEqual(10, node.left.next.val);
                Assert.IsNull(node.right.next);

                Assert.AreEqual(11, node.left.left.next.val);
                Assert.IsNull(node.right.left.next);

                Assert.AreEqual(12, node.left.left.right.next.val);
                Assert.IsNull(node.right.left.right.next);

                Assert.AreEqual(13, node.left.left.right.left.next.val);
                Assert.AreEqual(14, node.right.left.right.left.next.val);
                Assert.IsNull(node.right.left.right.right.next);

                Assert.IsNull(node.right.left.right.right.right.next);
            }
        }
    }
}