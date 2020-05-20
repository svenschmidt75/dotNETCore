using BinarySearchTree;
using NUnit.Framework;

namespace FindCommonParentInTree.Test
{
    [TestFixture]
    public class FindCommonParentInTreeTest
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var binarySearchTree = new BinarySearchTree<int>();
            binarySearchTree.Insert(41);
            binarySearchTree.Insert(20);
            binarySearchTree.Insert(11);
            binarySearchTree.Insert(29);
            binarySearchTree.Insert(32);
            binarySearchTree.Insert(65);
            binarySearchTree.Insert(50);
            binarySearchTree.Insert(91);
            binarySearchTree.Insert(72);
            binarySearchTree.Insert(99);

            var child1 = binarySearchTree.FindNode(72).child;
            var child2 = binarySearchTree.FindNode(99).child;
            
            // Act
            var parent = binarySearchTree.FindCommonParent(child1, child2);

            // Assert
            Assert.AreEqual(91, parent.Value);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var binarySearchTree = new BinarySearchTree<int>();
            binarySearchTree.Insert(41);
            binarySearchTree.Insert(20);
            binarySearchTree.Insert(11);
            binarySearchTree.Insert(29);
            binarySearchTree.Insert(32);
            binarySearchTree.Insert(65);
            binarySearchTree.Insert(50);
            binarySearchTree.Insert(91);
            binarySearchTree.Insert(72);
            binarySearchTree.Insert(99);

            var child1 = binarySearchTree.FindNode(50).child;
            var child2 = binarySearchTree.FindNode(99).child;
            
            // Act
            var parent = binarySearchTree.FindCommonParent(child1, child2);

            // Assert
            Assert.AreEqual(65, parent.Value);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var binarySearchTree = new BinarySearchTree<int>();
            binarySearchTree.Insert(41);
            binarySearchTree.Insert(20);
            binarySearchTree.Insert(11);
            binarySearchTree.Insert(29);
            binarySearchTree.Insert(32);
            binarySearchTree.Insert(65);
            binarySearchTree.Insert(50);
            binarySearchTree.Insert(91);
            binarySearchTree.Insert(72);
            binarySearchTree.Insert(99);

            var child1 = binarySearchTree.FindNode(32).child;
            var child2 = binarySearchTree.FindNode(72).child;
            
            // Act
            var parent = binarySearchTree.FindCommonParent(child1, child2);

            // Assert
            Assert.AreEqual(41, parent.Value);
        }
        
    }
}