using NUnit.Framework;

namespace BinarySearchTree.Test
{
    [TestFixture]
    public class BinarySearchTreeTest
    {
        [Test]
        public void TestInsert()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            bst.Insert(1);

            // Act
            bst.Insert(2);

            // Assert
            Assert.True(bst.Search(2));
        }

        [Test]
        public void TestInsertEmptyRoot()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();

            // Act
            bst.Insert(1);

            // Assert
            Assert.True(bst.Search(1));
        }

        [Test]
        public void TestValidate()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();

            // Act
            bst.Insert(101);
            bst.Insert(105);
            bst.Insert(102);
            bst.Insert(144);
            bst.Insert(231);

            // Assert
            var flattened = bst.TraverseInOrder();
            CollectionAssert.AreEqual(new[] {101, 102, 105, 144, 231}, flattened);
        }
    }
}