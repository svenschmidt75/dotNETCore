#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace LevelOrderTraversalInSpiralOrder
{
    public class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    /// <summary>
    ///     Tree Traversal Spiral Order
    ///     https://www.youtube.com/watch?v=vjt5Y6-1KsQ&list=PLGZgSdLg2naQgkmK1XlKhC9BbXeuYcL4T&index=5
    /// </summary>
    public class Class1
    {
        public static void LevelOrderTraversalInSpiralOrder(Node root)
        {
            var level = 0;

            var queue = new Queue<Node>();
            var stack = new Stack<Node>();

            queue.Enqueue(root);

            while (queue.Any())
            {
                var node = queue.Dequeue();

                Console.Write($"{node.Value} ");

                if (level % 2 == 0)
                {
                    // SS: even level 
                    if (node.Left != null)
                    {
                        stack.Push(node.Left);
                    }

                    if (node.Right != null)
                    {
                        stack.Push(node.Right);
                    }
                }
                else
                {
                    // SS: odd level 
                    if (node.Right != null)
                    {
                        stack.Push(node.Right);
                    }

                    if (node.Left != null)
                    {
                        stack.Push(node.Left);
                    }
                }

                if (queue.Any() == false)
                {
                    while (stack.Any())
                    {
                        var n = stack.Pop();
                        queue.Enqueue(n);
                    }

                    level += 1;
                }
            }
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var node10 = new Node {Value = 10};
            var node9 = new Node {Value = 9};
            var node8 = new Node {Value = 8};
            var node7 = new Node {Value = 7};
            var node6 = new Node {Value = 6};
            var node5 = new Node {Value = 5};
            var node4 = new Node {Value = 4};
            var node3 = new Node {Value = 3};
            var node2 = new Node {Value = 2};
            var node1 = new Node {Value = 1};

            node1.Left = node2;
            node1.Right = node3;

            node2.Left = node4;
            node2.Right = node5;

            node3.Left = node6;
            node3.Right = node7;

            node4.Left = node8;
            node4.Right = node9;

            node7.Right = node10;

            // Act
            Class1.LevelOrderTraversalInSpiralOrder(node1);

            // Assert
        }
    }
}