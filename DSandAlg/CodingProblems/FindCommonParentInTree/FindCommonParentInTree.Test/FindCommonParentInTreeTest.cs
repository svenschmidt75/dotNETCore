#region

using BinarySearchTree;
using NUnit.Framework;

#endregion

namespace FindCommonParentInTree.Test
{
    [TestFixture]
    public class FindCommonParentInTreeTest
    {
        [Test]
        public void Test11()
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
        public void Test12()
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

            // Act
            var parent = binarySearchTree.FindCommonParent2(72, 99);

            // Assert
            Assert.AreEqual(91, parent.Value);
        }

        [Test]
        public void Test21()
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
        public void Test22()
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

            // Act
            var parent = binarySearchTree.FindCommonParent2(50, 99);

            // Assert
            Assert.AreEqual(65, parent.Value);
        }

        [Test]
        public void Test31()
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

        [Test]
        public void Test32()
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

            // Act
            var parent = binarySearchTree.FindCommonParent2(32, 72);

            // Assert
            Assert.AreEqual(41, parent.Value);
        }

        [Test]
        public void Test42()
        {
            // Arrange
            var node1 = new Node<int> {Value = 1};
            var node5 = new Node<int> {Value = 5};
            var node4 = new Node<int> {Value = 4};
            var node3 = new Node<int> {Value = 3, Left = node5};
            var node2 = new Node<int> {Value = 2, Left = node3, Right = node4};
            var node0 = new Node<int> {Value = 0, Left = node1, Right = node2};

            var bt = new BinarySearchTree<int> {Root = node0};

            // Act
            var parent = bt.FindCommonParent2(5, 4);

            // Assert
            Assert.AreEqual(2, parent.Value);
        }

        [Test]
        public void Test52()
        {
            // Arrange
            var node1 = new Node<int> {Value = 1};
            var node5 = new Node<int> {Value = 5};
            var node4 = new Node<int> {Value = 4};
            var node3 = new Node<int> {Value = 3, Left = node5};
            var node2 = new Node<int> {Value = 2, Left = node3, Right = node4};
            var node0 = new Node<int> {Value = 0, Left = node1, Right = node2};

            var bt = new BinarySearchTree<int> {Root = node0};

            // Act
            var parent = bt.FindCommonParent2(3, 5);

            // Assert
            Assert.AreEqual(3, parent.Value);
        }
        
        [Test]
        public void Test62()
        {
            // Arrange
            var node1 = new Node<int> {Value = 1};
            var node5 = new Node<int> {Value = 5};
            var node4 = new Node<int> {Value = 4};
            var node3 = new Node<int> {Value = 3, Left = node5};
            var node2 = new Node<int> {Value = 2, Left = node3, Right = node4};
            var node0 = new Node<int> {Value = 0, Left = node1, Right = node2};

            var bt = new BinarySearchTree<int> {Root = node0};

            // Act
            var parent = bt.FindCommonParent2(3, 3);

            // Assert
            Assert.AreEqual(3, parent.Value);
        }

    }
}