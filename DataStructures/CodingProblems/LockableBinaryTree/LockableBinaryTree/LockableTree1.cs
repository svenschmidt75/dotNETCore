#region

using System;
using NUnit.Framework;

#endregion

namespace LockableBinaryTree
{
    /// <summary>
    ///     SS: We add a parent link to each node, effectively turning this tree into a
    ///     graph.
    ///     To check ancestors for locked is O(h) worst-case, where h is the height of the tree.
    ///     To check descendants is O(h) avg. case, but O(n) worst-case.
    /// </summary>
    public class LockableTree1
    {
        public LockableTree1(Node root)
        {
            Root = root;
        }

        public Node Root { get; }

        public bool Lock(Node node)
        {
            if (node == null)
            {
                throw new ArgumentException();
            }

            var anyAncestorLocked = AnyAncestorLocked(node);
            if (anyAncestorLocked)
            {
                return false;
            }

            var anyDescendantLocked = AnyDescendantLocked(node);
            if (anyDescendantLocked)
            {
                return false;
            }

            node.IsLocked = true;
            return true;
        }

        private bool AnyDescendantLocked(Node node)
        {
            // SS: O(h) avg. case performance (I think), but O(n) worst-case (i.e. call lock on root node)
            if (node == null)
            {
                return false;
            }

            return node.IsLocked || AnyDescendantLocked(node.Left) || AnyDescendantLocked(node.Right);
        }

        private bool AnyAncestorLocked(Node node)
        {
            // SS: O(h) worst-case
            if (node == null)
            {
                return false;
            }

            return node.IsLocked || AnyAncestorLocked(node.Parent);
        }

        public bool UnLock(Node node)
        {
            if (node == null)
            {
                throw new ArgumentException();
            }

            if (node.IsLocked == false)
            {
                return false;
            }

            node.IsLocked = false;
            return true;
        }
    }

    [TestFixture]
    public class LockableBinaryTreeTest
    {
        public LockableTree1 CreateTree()
        {
            var node4 = new Node {Value = 4};
            var node5 = new Node {Value = 5};
            var node2 = new Node {Value = 2, Left = node4, Right = node5};
            node4.Parent = node2;
            node5.Parent = node2;

            var node6 = new Node {Value = 6};
            var node3 = new Node {Value = 3, Left = node6};
            node6.Parent = node3;

            var node1 = new Node {Value = 1, Left = node2, Right = node3};
            node2.Parent = node1;
            node3.Parent = node1;

            return new LockableTree1(node1);
        }

        [Test]
        public void TestAncestorLocked()
        {
            // Arrange
            var tree = CreateTree();
            tree.Root.IsLocked = true;

            // Act
            var nodeLocked = tree.Lock(tree.Root.Left);

            // Assert
            Assert.AreEqual(nodeLocked, false);
        }

        [Test]
        public void TestDescendantLocked()
        {
            // Arrange
            var tree = CreateTree();
            tree.Root.Left.Right.IsLocked = true;

            // Act
            var nodeLocked = tree.Lock(tree.Root.Left);

            // Assert
            Assert.AreEqual(nodeLocked, false);
        }

        [Test]
        public void TestLocked()
        {
            // Arrange
            var tree = CreateTree();

            // Act
            var nodeLocked = tree.Lock(tree.Root.Left);

            // Assert
            Assert.AreEqual(nodeLocked, true);
        }
    }
}