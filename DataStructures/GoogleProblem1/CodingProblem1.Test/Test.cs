using GoogleProblem1;
using NUnit.Framework;

namespace CodingProblem1.Test
{
    [TestFixture]
    public class Test
    {
        private static Node CreateBinaryTree()
        {
            //             1
            //       2           3
            //   4       5   6       7
            // 8  9   10

            var node8 = new Node {Value = "8"};
            var node9 = new Node {Value = "9"};
            var node4 = new Node {Value = "4"};
            node4.Left = node8;
            node4.Right = node9;


            var node10 = new Node {Value = "10"};
            var node5 = new Node {Value = "5"};
            node5.Left = node10;


            var node2 = new Node {Value = "2"};
            node2.Left = node4;
            node2.Right = node5;


            var node6 = new Node {Value = "6"};
            var node7 = new Node {Value = "7"};
            var node3 = new Node {Value = "3"};
            node3.Left = node6;
            node3.Right = node7;

            var node1 = new Node {Value = "1"};
            node1.Left = node2;
            node1.Right = node3;
            return node1;
        }

        [Test]
        public void TestFindNode10()
        {
            // Arrange
            var node1 = CreateBinaryTree();

            // Act
            var found = FindNodeByIndex.FindNodeInTree1(node1, 10);

            // Assert
            Assert.True(found);
        }


        [Test]
        public void TestFindNode10_2()
        {
            // Arrange
            var node1 = CreateBinaryTree();

            // Act
            var found = FindNodeByIndex.FindNodeInTree2(node1, 10);

            // Assert
            Assert.True(found);
        }

        [Test]
        public void TestFindNode11_1()
        {
            // Arrange
            var node1 = CreateBinaryTree();

            // Act
            var found = FindNodeByIndex.FindNodeInTree1(node1, 11);

            // Assert
            Assert.False(found);
        }

        [Test]
        public void TestFindNode11_2()
        {
            // Arrange
            var node1 = CreateBinaryTree();

            // Act
            var found = FindNodeByIndex.FindNodeInTree2(node1, 11);

            // Assert
            Assert.False(found);
        }

        [Test]
        public void TestFindRootNode_1()
        {
            // Arrange
            var node1 = CreateBinaryTree();

            // Act
            var found = FindNodeByIndex.FindNodeInTree1(node1, 1);

            // Assert
            Assert.True(found);
        }

        [Test]
        public void TestFindRootNode_2()
        {
            // Arrange
            var node1 = CreateBinaryTree();

            // Act
            var found = FindNodeByIndex.FindNodeInTree2(node1, 1);

            // Assert
            Assert.True(found);
        }

        [Test]
        public void TestGenerateIndices()
        {
            // Arrange

            // Act
            var indices = FindNodeByIndex.CalculateIndices(11);

            // Assert
            CollectionAssert.AreEqual(new[] {1, 2, 5, 11}, indices);
        }

        [Test]
        public void TestGenerateIndices_1()
        {
            // Arrange

            // Act
            var indices = FindNodeByIndex.CalculateIndices(11);

            // Assert
            CollectionAssert.AreEqual(new[] {1, 2, 5, 11}, indices);
        }

        [Test]
        public void TestNumberOfNodes1()
        {
            // Arrange
            var node1 = CreateBinaryTree();

            // Act
            var nNodes = FindNodeByIndex.NumberOfNodes(node1);

            // Assert
            Assert.AreEqual(10, nNodes);
        }

        [Test]
        public void TestNumberOfNodes2()
        {
            // Arrange
            var node1 = CreateBinaryTree();
            node1.Left.Right.Right = new Node{Value = "11"};

            // Act
            var nNodes = FindNodeByIndex.NumberOfNodes(node1);

            // Assert
            Assert.AreEqual(11, nNodes);
        }
        
        [Test]
        public void TestNumberOfNodesRoot()
        {
            // Arrange
            var node1 = CreateBinaryTree();
            node1.Left = null;
            node1.Right = null;

            // Act
            var nNodes = FindNodeByIndex.NumberOfNodes(node1);

            // Assert
            Assert.AreEqual(1, nNodes);
        }

    }
}