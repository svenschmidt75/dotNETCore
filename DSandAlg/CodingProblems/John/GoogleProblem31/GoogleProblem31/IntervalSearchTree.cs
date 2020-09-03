#region

using System;
using NUnit.Framework;

#endregion

namespace GoogleProblem31
{
    // https://www.youtube.com/watch?v=q0QOYtSsTg4&t=637s
    public class IntervalSearchTree
    {
        public Node Root { get; set; }

        public void Insert(int min, int max)
        {
            var node = new Node
            {
                c1 = min
                , c2 = max
                , Max = max
            };

            if (Root == null)
            {
                Root = node;
                return;
            }

            Root = Insert(Root, node);
        }

        private Node Insert(Node node, Node n)
        {
            if (node == null)
            {
                return n;
            }

            // SS: the start of the interval is the BST key!
            if (n.c1 <= node.c1)
            {
                node.Left = Insert(node.Left, n);
                node.Max = Math.Max(node.Max, node.Left.Max);
            }
            else
            {
                node.Right = Insert(node.Right, n);
                node.Max = Math.Max(node.Max, node.Right.Max);
            }

            return node;
        }

        public bool Overlap(int c1, int c2)
        {
            // SS: check whether interval [c1, c2] intersects any of the existing intervals
            var node = Root;
            while (node != null)
            {
                if (node.Intersects(c1, c2))
                {
                    return true;
                }

                if (node.Left == null)
                {
                    node = node.Right;
                }
                else if (node.Left.Max < c1)
                {
                    node = node.Right;
                }
                else
                {
                    node = node.Left;
                }
            }

            return false;
        }

        public class Node
        {
            public int c1 { get; set; }
            public int c2 { get; set; }
            public int Max { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public bool Intersects(int b1, int b2)
            {
                return c1 < b2 && b1 < c2;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var intervalTree = new IntervalSearchTree();

                // Act
                intervalTree.Insert(1, 5);

                // Assert
                Assert.AreEqual(1, intervalTree.Root.c1);
                Assert.AreEqual(5, intervalTree.Root.c2);
                Assert.AreEqual(5, intervalTree.Root.Max);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var intervalTree = new IntervalSearchTree();

                // Act
                intervalTree.Insert(1, 5);
                intervalTree.Insert(3, 10);

                // Assert
                Assert.AreEqual(1, intervalTree.Root.c1);
                Assert.AreEqual(5, intervalTree.Root.c2);
                Assert.AreEqual(10, intervalTree.Root.Max);

                Assert.AreEqual(3, intervalTree.Root.Right.c1);
                Assert.AreEqual(10, intervalTree.Root.Right.c2);
                Assert.AreEqual(10, intervalTree.Root.Right.Max);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var intervalTree = new IntervalSearchTree();

                // Act
                intervalTree.Insert(1, 5);
                intervalTree.Insert(3, 10);
                intervalTree.Insert(0, 20);

                // Assert
                Assert.AreEqual(1, intervalTree.Root.c1);
                Assert.AreEqual(5, intervalTree.Root.c2);
                Assert.AreEqual(20, intervalTree.Root.Max);

                Assert.AreEqual(0, intervalTree.Root.Left.c1);
                Assert.AreEqual(20, intervalTree.Root.Left.c2);
                Assert.AreEqual(20, intervalTree.Root.Left.Max);

                Assert.AreEqual(3, intervalTree.Root.Right.c1);
                Assert.AreEqual(10, intervalTree.Root.Right.c2);
                Assert.AreEqual(10, intervalTree.Root.Right.Max);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var intervalTree = new IntervalSearchTree();
                intervalTree.Insert(2, 5);
                intervalTree.Insert(3, 10);
                intervalTree.Insert(7, 13);

                // Act
                // Assert
                Assert.IsFalse(intervalTree.Overlap(0, 1));
                Assert.IsTrue(intervalTree.Overlap(2, 3));
                Assert.IsTrue(intervalTree.Overlap(6, 12));
                Assert.IsFalse(intervalTree.Overlap(15, 21));
            }
        }
    }
}