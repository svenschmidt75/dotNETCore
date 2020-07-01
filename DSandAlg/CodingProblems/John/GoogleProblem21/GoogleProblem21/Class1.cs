#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace GoogleProblem21
{
    public class Node
    {
        public int Value { get; set; }
        public Node Parent { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public class GP21
    {
        /// <summary>
        ///     Given a binary tree, paint each node using only 2 colors (vertex coloring) according to the following rules:
        ///     - 2 players take alternating turns
        ///     - for each turn, a player can paint unpainted nodes using "her" color
        ///     The game ends when all nodes have been colored.
        ///     The player with the most colored nodes wins.
        ///     Return the node that gives player 1 the most nodes.
        /// </summary>
        public (Node player1StartNode, int count) Solve(Node root, Node player2StartNode)
        {
            var (player1StartNode, count) = Traverse(root, player2StartNode);
            return (player1StartNode, count);
        }

        private (Node player1StartNode, int count) Traverse(Node node, Node player2StartNode)
        {
            // SS: post-order traversal to visit each possible start node
            // for player 1
            // Traversal: O(n)

            if (node == null)
            {
                return (null, 0);
            }

            Node bestPlayer1StartNode = null;
            var maxCount = 0;

            if (node.Left != null)
            {
                (bestPlayer1StartNode, maxCount) = Traverse(node.Left, player2StartNode);
            }

            if (node.Right != null)
            {
                var (rightPlayer1StartNode, rightCount) = Traverse(node.Right, player2StartNode);
                if (rightCount > maxCount)
                {
                    bestPlayer1StartNode = rightPlayer1StartNode;
                    maxCount = rightCount;
                }
            }

            if (node == player2StartNode)
            {
                return (null, 0);
            }

            var (nodePlayer1StartNode, nodeCount) = RunGame(node, player2StartNode);
            if (nodeCount > maxCount)
            {
                bestPlayer1StartNode = nodePlayer1StartNode;
                maxCount = nodeCount;
            }

            return (bestPlayer1StartNode, maxCount);
        }

        private static (Node player1StartNode, int count) RunGame(Node player1StartNode, Node player2StartNode)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(n)

            var player1Set = new HashSet<Node> {player1StartNode};
            var player1Queue = new Queue<Node>();
            player1Queue.Enqueue(player1StartNode);

            var player2Set = new HashSet<Node> {player2StartNode};
            var player2Queue = new Queue<Node>();
            player2Queue.Enqueue(player2StartNode);

            var turnCnt = 0;

            while (player1Queue.Any() || player2Queue.Any())
            {
                if (turnCnt % 2 == 0)
                {
                    // SS: player 1 move
                    player1Queue = ProcessQueue(player1Set, player2Set, player1Queue);
                }
                else
                {
                    // SS: player 2 move
                    player2Queue = ProcessQueue(player2Set, player1Set, player2Queue);
                }

                turnCnt++;
            }

            return (player1StartNode, player1Set.Count);
        }

        private static Queue<Node> ProcessQueue(HashSet<Node> player1Set, HashSet<Node> player2Set
            , Queue<Node> playerQueue)
        {
            var q = new Queue<Node>();

            while (playerQueue.Any())
            {
                var node = playerQueue.Dequeue();

                var n = node.Parent;
                if (n != null && player1Set.Contains(n) == false && player2Set.Contains(n) == false)
                {
                    player1Set.Add(n);
                    q.Enqueue(n);
                }

                n = node.Left;
                if (n != null && player1Set.Contains(n) == false && player2Set.Contains(n) == false)
                {
                    player1Set.Add(n);
                    q.Enqueue(n);
                }

                n = node.Right;
                if (n != null && player1Set.Contains(n) == false && player2Set.Contains(n) == false)
                {
                    player1Set.Add(n);
                    q.Enqueue(n);
                }
            }

            return q;
        }
    }

    [TestFixture]
    public class Tests
    {
        private static Node CreateTree()
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

            node8.Parent = node4;
            node4.Left = node8;

            node9.Parent = node5;
            node5.Right = node9;

            node10.Parent = node7;
            node11.Parent = node7;
            node7.Left = node10;
            node7.Right = node11;

            node4.Parent = node2;
            node5.Parent = node2;
            node2.Left = node4;
            node2.Right = node5;

            node6.Parent = node3;
            node7.Parent = node3;
            node3.Left = node6;
            node3.Right = node7;

            node2.Parent = node1;
            node3.Parent = node1;
            node1.Left = node2;
            node1.Right = node3;

            return node1;
        }

        [Test]
        public void Test1()
        {
            // Arrange
            var root = CreateTree();

            // Act
            var node7 = root.Right.Right;
            var (node, count) = new GP21().Solve(root, node7);

            // Assert
            Assert.AreEqual(8, count);
            Assert.AreEqual(6, node.Value);
        }
    }
}