using NUnit.Framework;

namespace BinarySearchTree.Test
{
    [TestFixture]
    public class BinarySearchTreeTest
    {
        [Test]
        public void TestFindNode()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            bst.Insert(41);
            bst.Insert(65);
            bst.Insert(50);
            bst.Insert(91);
            bst.Insert(99);

            // Act
            var (found, parent, child) = bst.FindNode(91);

            // Assert
            Assert.True(found);
            Assert.AreEqual(65, parent.Value);
            Assert.AreEqual(91, child.Value);
        }

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
        public void TestMinimumNode()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            bst.Insert(41);
            bst.Insert(65);
            bst.Insert(50);
            bst.Insert(91);
            bst.Insert(99);
            bst.Insert(72);
            bst.Insert(68);
            bst.Insert(70);
            var (_, parent, child) = bst.FindNode(91);

            // Act
            var (p, c) = bst.FindSmallestNode(parent, child);

            // Assert
            Assert.AreEqual(72, p.Value);
            Assert.AreEqual(68, c.Value);
        }

        [Test]
        public void TestMinimumNodeNoLeftSubtree()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            bst.Insert(41);
            bst.Insert(65);
            bst.Insert(50);
            bst.Insert(91);
            bst.Insert(99);
            var (_, parent, child) = bst.FindNode(91);

            // Act
            var (p, c) = bst.FindSmallestNode(parent, child);

            // Assert
            Assert.AreEqual(65, parent.Value);
            Assert.AreEqual(91, child.Value);
        }

        [Test]
        public void TestRemove()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            bst.Insert(41);
            bst.Insert(65);
            bst.Insert(50);
            bst.Insert(91);
            bst.Insert(99);
            bst.Insert(72);
            bst.Insert(68);
            bst.Insert(70);

            // Act
            bst.Remove(65);

            // Assert
            var (_, parent, child) = bst.FindNode(68);
            Assert.AreEqual(41, parent.Value);
            Assert.AreEqual(68, child.Value);
            Assert.AreEqual(50, child.Left.Value);
            Assert.AreEqual(91, child.Right.Value);
        }

        [Test]
        public void TestRemoveHead()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            bst.Insert(41);
            bst.Insert(65);
            bst.Insert(50);
            bst.Insert(91);
            bst.Insert(99);
            bst.Insert(72);
            bst.Insert(68);
            bst.Insert(70);
            bst.Insert(10);

            // Act
            bst.Remove(41);

            // Assert
            var (_, parent, child) = bst.FindNode(50);
            Assert.AreSame(parent, child);
            Assert.AreEqual(50, child.Value);
            Assert.AreEqual(10, child.Left.Value);
            Assert.AreEqual(65, child.Right.Value);
        }

        [Test]
        public void TestRemoveNodeNoLeftSubtree()
        {
            // Arrange
            var bst = new BinarySearchTree<int>();
            bst.Insert(41);
            bst.Insert(65);
            bst.Insert(50);
            bst.Insert(91);
            bst.Insert(99);

            // Act
            bst.Remove(65);

            // Assert
            var (_, parent, child) = bst.FindNode(91);
            Assert.AreEqual(41, parent.Value);
            Assert.AreEqual(91, child.Value);
            Assert.AreEqual(50, child.Left.Value);
            Assert.AreEqual(99, child.Right.Value);
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