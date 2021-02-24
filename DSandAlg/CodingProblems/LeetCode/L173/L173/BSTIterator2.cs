#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 173. Binary Search Tree Iterator
// URL: https://leetcode.com/problems/binary-search-tree-iterator/

namespace LeetCode
{
    public class BSTIterator2
    {
        private readonly Queue<TreeNode> _queue;

        public BSTIterator2(TreeNode root)
        {
            _queue = new Queue<TreeNode>();

            void DFS(TreeNode node)
            {
                if (node == null)
                {
                    return;
                }

                DFS(node.left);
                _queue.Enqueue(node);
                DFS(node.right);
            }

            DFS(root);
        }

        public int Next()
        {
            var node = _queue.Dequeue();
            return node.val;
        }

        public bool HasNext()
        {
            return _queue.Any();
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
                var it = new BSTIterator2(root);
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
        }
    }
}