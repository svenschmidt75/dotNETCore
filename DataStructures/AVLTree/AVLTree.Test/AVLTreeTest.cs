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

            var node6 = avlTree.Root.Right;
            Assert.AreEqual(2, node6.Height);

            var root = avlTree.Root;
            Assert.AreEqual(3, root.Height);
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
    }
}