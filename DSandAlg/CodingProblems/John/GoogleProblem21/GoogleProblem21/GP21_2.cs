#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace GoogleProblem21
{
    public class GP21_2
    {
        public (Node player1StartNode, int count) Solve(Node root, Node player2StartNode)
        {
            // SS: pre-processing, at O(N) runtime cost, allows to check at O(1)
            var nodeAugmentation = new Dictionary<Node, NodeAugmentation>();
            Pass1(null, root, nodeAugmentation);
            Pass2(root, 0, nodeAugmentation);

            // SS: max. nodes player 1 can paint when start node player 1 is player 2's
            // parent node
            var nNodesPlayer2Parent = nodeAugmentation[player2StartNode].NodesAbove;

            // SS: max. nodes player 1 can paint when start node player 1 is player 2's
            // left child
            var nNodesPlayer2Left =
                player2StartNode.Left != null ? nodeAugmentation[player2StartNode.Left].NodesBelow : 0;

            // SS: max. nodes player 1 can paint when start node player 1 is player 2's
            // right child
            var nNodesPlayer2Right =
                player2StartNode.Right != null ? nodeAugmentation[player2StartNode.Right].NodesBelow : 0;

            var maxNodes = nNodesPlayer2Parent;
            var player1StartNode = nodeAugmentation[player2StartNode].Parent;
            if (nNodesPlayer2Left > maxNodes)
            {
                maxNodes = nNodesPlayer2Left;
                player1StartNode = player2StartNode.Left;
            }

            if (nNodesPlayer2Right > maxNodes)
            {
                maxNodes = nNodesPlayer2Right;
                player1StartNode = player2StartNode.Right;
            }

            return (player1StartNode, maxNodes);
        }

        internal static int Pass1(Node parent, Node node, IDictionary<Node, NodeAugmentation> nodeAugmentations)
        {
            // SS: determine nodes below a node and set it's parent using post-order traversal
            if (node == null)
            {
                return 0;
            }

            var nodeAug = new NodeAugmentation
            {
                Parent = parent
            };

            var nLeft = Pass1(node, node.Left, nodeAugmentations);
            var nRight = Pass1(node, node.Right, nodeAugmentations);
            var nodesBelow = 1 + nLeft + nRight;
            nodeAug.NodesBelow = nodesBelow;

            nodeAugmentations[node] = nodeAug;

            return nodesBelow;
        }

        internal static void Pass2(Node node, int nNodesAbove, IDictionary<Node, NodeAugmentation> nodeAugmentations)
        {
            // SS: determine nodes above a node
            if (node == null)
            {
                return;
            }

            nodeAugmentations[node].NodesAbove = nNodesAbove;

            var nNodesLeft = 0;
            if (node.Left != null)
            {
                nNodesLeft = nodeAugmentations[node.Left].NodesBelow;
            }

            var nNodesRight = 0;
            if (node.Right != null)
            {
                nNodesRight = nodeAugmentations[node.Right].NodesBelow;
            }

            Pass2(node.Left, nNodesAbove + nNodesRight + 1, nodeAugmentations);
            Pass2(node.Right, nNodesAbove + nNodesLeft + 1, nodeAugmentations);
        }

        internal class NodeAugmentation
        {
            public Node Parent { get; set; }
            public int NodesBelow { get; set; }
            public int NodesAbove { get; set; }
        }
    }

    [TestFixture]
    public class GP21_2_Tests
    {
        private static Node CreateTree1()
        {
            var node1 = new Node {Value = 1};
            var node2 = new Node {Value = 2};
            var node3 = new Node {Value = 3};
            var node4 = new Node {Value = 4};
            var node5 = new Node {Value = 5};
            var node6 = new Node {Value = 6};
            var node7 = new Node {Value = 7};
            var node8 = new Node {Value = 8};
            var node9 = new Node {Value = 9};
            var node10 = new Node {Value = 10};
            var node11 = new Node {Value = 11};

            node1.Left = node2;
            node1.Right = node3;

            node2.Left = node4;

            node3.Left = node5;
            node3.Right = node6;

            node5.Left = node7;

            node6.Left = node8;
            node6.Right = node9;

            node8.Left = node10;

            node9.Right = node11;

            return node1;
        }

        [Test]
        public void TestNodesBelow()
        {
            // Arrange
            var root = CreateTree1();
            var nodeAugmentations = new Dictionary<Node, GP21_2.NodeAugmentation>();

            // Act
            GP21_2.Pass1(null, root, nodeAugmentations);

            // Assert
            Assert.AreEqual(11, nodeAugmentations[root].NodesBelow);
            Assert.AreEqual(5, nodeAugmentations[root.Right.Right].NodesBelow);
        }

        [Test]
        public void TestNodesAbove()
        {
            // Arrange
            var root = CreateTree1();
            var nodeAugmentations = new Dictionary<Node, GP21_2.NodeAugmentation>();
            GP21_2.Pass1(null, root, nodeAugmentations);

            // Act
            GP21_2.Pass2(root, 0, nodeAugmentations);

            // Assert
            Assert.AreEqual(0, nodeAugmentations[root].NodesAbove);
            Assert.AreEqual(9, nodeAugmentations[root.Left].NodesAbove);
            Assert.AreEqual(3, nodeAugmentations[root.Right].NodesAbove);
            Assert.AreEqual(9, nodeAugmentations[root.Right.Left].NodesAbove);
        }

        [Test]
        public void Test1()
        {
            // Arrange
            var root = Tests.CreateTree();

            // Act
            var node7 = root.Right.Right;
            var (node, count) = new GP21_2().Solve(root, node7);

            // Assert
            Assert.AreEqual(8, count);
            Assert.AreEqual(3, node.Value);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var root = Tests.CreateTree();

            // Act
            var (node, count) = new GP21_2().Solve(root, root);

            // Assert
            Assert.AreEqual(5, count);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var root = Tests.CreateTree();

            // Act
            var node3 = root.Right;
            var (node, count) = new GP21_2().Solve(root, node3);

            // Assert
            Assert.AreEqual(6, count);
            Assert.AreEqual(1, node.Value);
        }
    }
}