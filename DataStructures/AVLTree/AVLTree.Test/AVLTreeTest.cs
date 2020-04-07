using NUnit.Framework;

namespace AVLTree.Test
{
    [TestFixture]
    public class AVLTreeTest
    {
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