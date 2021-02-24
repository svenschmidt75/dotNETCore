#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace LeetCode
{
    public class BSTIterator
    {
        private readonly Dictionary<TreeNode, TreeNode> _pred = new Dictionary<TreeNode, TreeNode>();
        private TreeNode _current;

        public BSTIterator(TreeNode root)
        {
            _current = root;
        }

        public int Next()
        {
            // SS: Morris inorder traversal
            // runtime complexity: O(1) avg.

            while (_current != null)
            {
                if (_current.left == null)
                {
                    var n = _current;

                    var next = _current.right;
                    if (next == null)
                    {
                        _pred.TryGetValue(_current, out next);
                    }

                    _current = next;

                    return n.val;
                }

                var p = FindPred(_current);

                if (_pred.ContainsKey(p) == false)
                {
                    // SS: left subtree not yet explored
                    _pred[p] = _current;
                    _current = _current.left;
                }
                else
                {
                    // SS: left subtree already explored
                    _pred.Remove(p);

                    var n = _current;

                    var next = _current.right;
                    if (next == null)
                    {
                        _pred.TryGetValue(_current, out next);
                    }

                    _current = next;

                    return n.val;
                }
            }

            throw new InvalidOperationException();
        }

        public bool HasNext()
        {
            return _current != null;
        }

        private static TreeNode FindPred(TreeNode node)
        {
            // SS: find right-most node in left subtree
            var n = node.left;
            while (n.right != null)
            {
                n = n.right;
            }

            return n;
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
                    val = 7
                    , left = new TreeNode
                    {
                        val = 3
                    }
                    , right = new TreeNode
                    {
                        val = 15
                        , left = new TreeNode
                        {
                            val = 9
                        }
                        , right = new TreeNode
                        {
                            val = 20
                        }
                    }
                };

                // Act
                // Assert
                var it = new BSTIterator(root);
                Assert.AreEqual(3, it.Next());
                Assert.AreEqual(7, it.Next());
                Assert.True(it.HasNext());
                Assert.AreEqual(9, it.Next());
                Assert.True(it.HasNext());
                Assert.AreEqual(15, it.Next());
                Assert.True(it.HasNext());
                Assert.AreEqual(20, it.Next());
                Assert.False(it.HasNext());
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var root = new TreeNode
                {
                    val = 10
                    , left = new TreeNode
                    {
                        val = 5
                        , left = new TreeNode
                        {
                            val = -2
                            , right = new TreeNode
                            {
                                val = 2
                                , left = new TreeNode
                                {
                                    val = -1
                                }
                            }
                        }
                        , right = new TreeNode
                        {
                            val = 6
                            , right = new TreeNode
                            {
                                val = 8
                            }
                        }
                    }
                    , right = new TreeNode
                    {
                        val = 30
                        , right = new TreeNode
                        {
                            val = 40
                        }
                    }
                };

                // Act
                // Assert
                var it = new BSTIterator(root);
                Assert.AreEqual(-2, it.Next());
                Assert.AreEqual(-1, it.Next());
                Assert.True(it.HasNext());
                Assert.AreEqual(2, it.Next());
                Assert.AreEqual(5, it.Next());
                Assert.AreEqual(6, it.Next());
                Assert.AreEqual(8, it.Next());
                Assert.AreEqual(10, it.Next());
                Assert.AreEqual(30, it.Next());
                Assert.True(it.HasNext());
                Assert.AreEqual(40, it.Next());
                Assert.False(it.HasNext());
            }
        }
    }
}