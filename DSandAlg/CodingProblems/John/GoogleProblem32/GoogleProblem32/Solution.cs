#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace GoogleProblem32
{
    public class Solution
    {
        public static int Solve(Node[] nodes)
        {
            if (nodes.Length == 0)
            {
                return 0;
            }

            // SS: O(n) runtime, O(n) space complexity
            var hashSet = new HashSet<Node>();
            for (var i = 0; i < nodes.Length; i++)
            {
                hashSet.Add(nodes[i]);
            }

            // SS: O(n)
            var nComponents = 0;
            for (var i = 0; i < nodes.Length; i++)
            {
                var baseNode = nodes[i];

                if (hashSet.Contains(baseNode) == false)
                {
                    continue;
                }

                hashSet.Remove(baseNode);

                nComponents++;

                var node = baseNode.Next;
                while (hashSet.Contains(node))
                {
                    hashSet.Remove(node);
                    node = node.Next;
                }

                node = baseNode.Prev;
                while (hashSet.Contains(node))
                {
                    hashSet.Remove(node);
                    node = node.Prev;
                }
            }

            return nComponents;
        }

        public class Node
        {
            public int Value { get; set; }
            public Node Prev { get; set; }
            public Node Next { get; set; }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
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

                node1.Next = node2;
                node2.Prev = node1;

                node2.Next = node3;
                node3.Prev = node2;

                node3.Next = node4;
                node4.Prev = node3;

                node4.Next = node5;
                node5.Prev = node4;

                node5.Next = node6;
                node6.Prev = node5;

                node6.Next = node7;
                node7.Prev = node6;

                node7.Next = node8;
                node8.Prev = node7;

                node8.Next = node9;
                node9.Prev = node8;

                node9.Next = node10;
                node10.Prev = node9;

                // Act
                var result = Solve(new[] {node2, node3, node4, node7, node8, node10});

                // Assert
                Assert.AreEqual(3, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
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

                node1.Next = node2;
                node2.Prev = node1;

                node2.Next = node3;
                node3.Prev = node2;

                node3.Next = node4;
                node4.Prev = node3;

                node4.Next = node5;
                node5.Prev = node4;

                node5.Next = node6;
                node6.Prev = node5;

                node6.Next = node7;
                node7.Prev = node6;

                node7.Next = node8;
                node8.Prev = node7;

                node8.Next = node9;
                node9.Prev = node8;

                node9.Next = node10;
                node10.Prev = node9;

                // Act
                var result = Solve(new[] {node10, node8, node7, node4, node3, node2});

                // Assert
                Assert.AreEqual(3, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
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

                node1.Next = node2;
                node2.Prev = node1;

                node2.Next = node3;
                node3.Prev = node2;

                node3.Next = node4;
                node4.Prev = node3;

                node4.Next = node5;
                node5.Prev = node4;

                node5.Next = node6;
                node6.Prev = node5;

                node6.Next = node7;
                node7.Prev = node6;

                node7.Next = node8;
                node8.Prev = node7;

                node8.Next = node9;
                node9.Prev = node8;

                node9.Next = node10;
                node10.Prev = node9;

                // Act
                var result = Solve(new[] {node4, node8, node3, node10, node7, node2});

                // Assert
                Assert.AreEqual(3, result);
            }
        }
    }
}