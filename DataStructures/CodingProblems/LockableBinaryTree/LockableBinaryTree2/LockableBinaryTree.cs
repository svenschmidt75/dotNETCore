#region

using System;
using NUnit.Framework;

#endregion

namespace LockableBinaryTree2
{
    /// <summary>
    /// SS: To get O(h) guaranteed, where h = height of tree, we only ever must go up
    /// the tree, from child to parents, because the longest path is of length h.
    /// We introduce a parent pointer on each node, and also a field IsAnyDescendantLocked,
    /// which we set on a node's parents when locking it.
    /// This allows O(h) lock/unlock operations.
    ///
    /// Here, we only allow for one node in the tree to be locked. This rule can be relaxed.
    /// Locking/unlocking would work almost the same, but instead of IsAnyDescendantLocked,
    /// we'd have a counter that counts how many descendants are locked.
    /// The crucial "trick" is to store this field on each node, so we can make lock/unlock
    /// operate in O(h) (because we only ever traverse the tree up).
    /// </summary>
    public static class LockableBinaryTree
    {
        public static bool Lock(Node node)
        {
            // SS: If none of node's descendant are locked and none it its parents',
            // lock node.
            if (node == null)
            {
                throw new ArgumentException();
            }

            // SS: O(h) worst-case
            var validateLocked = ValidateLocked(node);
            if (validateLocked)
            {
                return false;
            }

            node.IsLocked = true;

            // SS: O(h) worst-case
            LockAncestors(node.Parent);

            return true;
        }

        public static bool Unlock(Node node)
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

            // SS: O(h) worst-case
            UnLockAncestors(node.Parent);

            return true;
        }

        private static void LockAncestors(Node node)
        {
            if (node == null)
            {
                return;
            }

            node.IsAnyDescendantLocked = true;
            LockAncestors(node.Parent);
        }

        private static void UnLockAncestors(Node node)
        {
            if (node == null)
            {
                return;
            }

            node.IsAnyDescendantLocked = false;
            LockAncestors(node.Parent);
        }

        private static bool ValidateLocked(Node node)
        {
            // SS: if a descendant on this node is locked, or any of it's parents,
            // we cannot lock this node.
            if (node == null)
            {
                return false;
            }

            return node.IsAnyDescendantLocked || ValidateLocked(node.Parent);
        }
    }

    [TestFixture]
    public class LockableBinaryTreeTest
    {
        public Node CreateTree()
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

            return node1;
        }

        [Test]
        public void TestAncestorLocked()
        {
            // Arrange
            var root = CreateTree();
            root.IsLocked = true;
            root.IsAnyDescendantLocked = true;

            // Act
            var nodeLocked = LockableBinaryTree.Lock(root.Left);

            // Assert
            Assert.AreEqual(nodeLocked, false);
        }

        [Test]
        public void TestDescendantLocked()
        {
            // Arrange
            var root = CreateTree();
            root.Left.Right.IsLocked = true;
            root.IsAnyDescendantLocked = true;
            root.Left.IsAnyDescendantLocked = true;

            // Act
            var nodeLocked = LockableBinaryTree.Lock(root.Left);

            // Assert
            Assert.AreEqual(nodeLocked, false);
        }

        [Test]
        public void TestLocked()
        {
            // Arrange
            var root = CreateTree();

            // Act
            var nodeLocked = LockableBinaryTree.Lock(root.Left);

            // Assert
            Assert.AreEqual(nodeLocked, true);
            Assert.AreEqual(root.Left.IsAnyDescendantLocked, false);
            Assert.AreEqual(root.IsAnyDescendantLocked, true);
        }
    }
}