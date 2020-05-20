
using System;
using NUnit.Framework;

namespace ClassLibrary1
{
    public struct Interval
    {
        public int Start { get; set; }
        public int End { get; set; }

        public bool Overlap(Interval other)
        {
            return Start <= other.End && other.Start <= End;
        }
    }

    public class Node
    {
        public Interval Interval { get; set; }
        public int Max { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public static Node Insert(Node node, Interval interval)
        {
            // SS: We use the min of the interval as the BST property...
            if (node == null)
            {
                return new Node
                {
                    Interval = interval
                    , Max = interval.End
                };
            }

            if (interval.Start < node.Interval.Start)
            {
                node.Left = Insert(node.Left, interval);
                node.Max = Math.Max(node.Max, node.Left.Max);
            }
            else
            {
                node.Right = Insert(node.Right, interval);
                node.Max = Math.Max(node.Max, node.Right.Max);
            }

            return node;
        }
        
        public static bool Overlap(Node node, Interval interval)
        {
            if (node == null)
            {
                return false;
            }

            if (node.Interval.Overlap(interval))
            {
                return true;
            }

            // SS: We use the max property of each node to divide the tree
            // into left and right subtree. That is why lookup is efficient... 
            if (node.Left != null && node.Left.Max >= interval.Start)
            {
                return Overlap(node.Left, interval);
            }

            return Overlap(node.Right, interval);
        }
        
    }

    [TestFixture]
    public class IntervalTreeTest
    {
        [Test]
        public void TestCreateRoot()
        {
            // Arrange
            
            // Act
            var root = Node.Insert(null, new Interval{Start = 15, End = 20});
            
            // Assert
            Assert.AreEqual(20, root.Max);
        }
        
        [Test]
        public void TestCreateLeftChild()
        {
            // Arrange
            var root = Node.Insert(null, new Interval{Start = 15, End = 20});
            
            // Act
            Node.Insert(root, new Interval {Start = 10, End = 30});
            
            // Assert
            Assert.AreEqual(10, root.Left.Interval.Start);
            Assert.AreEqual(30, root.Left.Interval.End);
            Assert.AreEqual(30, root.Left.Max);
            Assert.AreEqual(30, root.Max);
        }
        
        [Test]
        public void TestCreateTree()
        {
            // Arrange
            
            // Act
            var root = Node.Insert(null, new Interval{Start = 15, End = 20});
            Node.Insert(root, new Interval {Start = 10, End = 30});
            Node.Insert(root, new Interval {Start = 5, End = 20});
            Node.Insert(root, new Interval {Start = 12, End = 15});
            Node.Insert(root, new Interval {Start = 17, End = 19});
            Node.Insert(root, new Interval {Start = 30, End = 40});
            
            // Assert
            Assert.AreEqual(40, root.Max);
            Assert.AreEqual(30, root.Left.Max);
        }

        [Test]
        public void TestOverlapTrue()
        {
            // Arrange
            var root = Node.Insert(null, new Interval{Start = 15, End = 20});
            Node.Insert(root, new Interval {Start = 10, End = 30});
            Node.Insert(root, new Interval {Start = 5, End = 20});
            Node.Insert(root, new Interval {Start = 12, End = 15});
            Node.Insert(root, new Interval {Start = 17, End = 19});
            Node.Insert(root, new Interval {Start = 30, End = 40});
            
            // Act
            var isOverlap = Node.Overlap(root, new Interval { Start = 4, End = 5});
            
            // Assert
            Assert.True(isOverlap);
        }

        [Test]
        public void TestOverlapFalse()
        {
            // Arrange
            var root = Node.Insert(null, new Interval{Start = 15, End = 20});
            Node.Insert(root, new Interval {Start = 10, End = 30});
            Node.Insert(root, new Interval {Start = 5, End = 20});
            Node.Insert(root, new Interval {Start = 12, End = 15});
            Node.Insert(root, new Interval {Start = 17, End = 19});
            Node.Insert(root, new Interval {Start = 30, End = 40});
            
            // Act
            var isOverlap = Node.Overlap(root, new Interval { Start = 1, End = 4});
            
            // Assert
            Assert.False(isOverlap);
        }

    }
}