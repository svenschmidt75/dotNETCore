﻿#region

using NUnit.Framework;

#endregion

namespace AVLTree.Test
{
    [TestFixture]
    public class AVLTreeTest
    {
        [Test]
        public void TestDeleteNodeLeftRightChildren()
        {
            // Arrange
            var avlTree = AVLTree.Create();
            avlTree.Insert(5);
            avlTree.Insert(2);
            avlTree.Insert(8);
            avlTree.Insert(1);
            avlTree.Insert(6);
            avlTree.Insert(9);
            avlTree.Insert(7);

            // Act
            avlTree.Remove(8);

            // Assert
            Assert.AreEqual(7, avlTree.Root.Right.Value);
        }

        [Test]
        public void TestDeleteNodeNoChildren()
        {
            // Arrange
            /*
             *          5
             *      2       8
             *           6     9
             */
            
            var avlTree = AVLTree.Create();
            avlTree.Insert(5);
            avlTree.Insert(2);
            avlTree.Insert(8);
            avlTree.Insert(6);
            avlTree.Insert(9);

            // Act
            avlTree.Remove(2);

            // Assert
            /*
             *          8
             *     5       9
             *       6
             */
            Assert.AreEqual(5, avlTree.Root.Left.Value);
            Assert.AreEqual(6, avlTree.Root.Left.Right.Value);
        }

        [Test]
        public void TestDeleteNodeNoLeftChild()
        {
            // Arrange
            var avlTree = AVLTree.Create();
            avlTree.Insert(5);
            avlTree.Insert(2);
            avlTree.Insert(8);
            avlTree.Insert(9);

            // Act
            avlTree.Remove(8);

            // Assert
            Assert.AreEqual(9, avlTree.Root.Right.Value);
        }

        [Test]
        public void TestDeleteNodeNoRightChild()
        {
            // Arrange
            var avlTree = AVLTree.Create();
            avlTree.Insert(5);
            avlTree.Insert(2);
            avlTree.Insert(8);
            avlTree.Insert(6);

            // Act
            avlTree.Remove(8);

            // Assert
            Assert.AreEqual(6, avlTree.Root.Right.Value);
        }

        [Test]
        public void TestDoubleLeftHeavy()
        {
            // Arrange
            var avlTree = AVLTree.Create();

            // Act
            avlTree.Insert(3);
            avlTree.Insert(2);
            avlTree.Insert(1);

            // Assert
            Assert.AreEqual(2, avlTree.Root.Value);
            Assert.AreEqual(1, avlTree.Root.Left.Value);
            Assert.AreEqual(3, avlTree.Root.Right.Value);
        }

        [Test]
        public void TestDoubleRightHeavy()
        {
            // Arrange
            var avlTree = AVLTree.Create();

            // Act
            avlTree.Insert(3);
            avlTree.Insert(4);
            avlTree.Insert(5);

            // Assert
            Assert.AreEqual(4, avlTree.Root.Value);
            Assert.AreEqual(3, avlTree.Root.Left.Value);
            Assert.AreEqual(5, avlTree.Root.Right.Value);
        }

        [Test]
        public void TestHeight()
        {
            // Arrange
            var avlTree = AVLTree.Create();

            // Act
            avlTree.Insert(2);
            avlTree.Insert(1);
            avlTree.Insert(8);
            avlTree.Insert(7);
            avlTree.Insert(5);
            avlTree.Insert(9);

            // Assert
            var node9 = avlTree.Root.Right.Right;
            Assert.AreEqual(0, node9.Height);

            var node8 = avlTree.Root.Right;
            Assert.AreEqual(1, node8.Height);

            var root = avlTree.Root;
            Assert.AreEqual(2, root.Height);
        }

        [Test]
        public void TestInsert()
        {
            // Arrange
            var avlTree = AVLTree.Create();

            // Act
            avlTree.Insert(2);
            avlTree.Insert(1);
            avlTree.Insert(3);

            // Assert
            Assert.AreEqual(2, avlTree.Root.Value);
            Assert.AreEqual(1, avlTree.Root.Left.Value);
            Assert.AreEqual(3, avlTree.Root.Right.Value);
        }

        [Test]
        public void TestLeftRightHeavy()
        {
            // Arrange
            var avlTree = AVLTree.Create();

            // Act
            avlTree.Insert(5);
            avlTree.Insert(2);
            avlTree.Insert(4);

            // Assert
            Assert.AreEqual(4, avlTree.Root.Value);
            Assert.AreEqual(2, avlTree.Root.Left.Value);
            Assert.AreEqual(5, avlTree.Root.Right.Value);
        }

        [Test]
        public void TestRightLeftHeavy()
        {
            // Arrange
            var avlTree = AVLTree.Create();

            // Act
            avlTree.Insert(5);
            avlTree.Insert(8);
            avlTree.Insert(6);

            // Assert
            Assert.AreEqual(6, avlTree.Root.Value);
            Assert.AreEqual(5, avlTree.Root.Left.Value);
            Assert.AreEqual(8, avlTree.Root.Right.Value);
        }
    }
}