using System;
using NUnit.Framework;

namespace MergeTwoSortedLists
{
    public class Node
    {
        public int Value { get; set; }
        public Node Next { get; set; }
    }

    public static class MergeList
    {
        public static Node Merge(Node node1, Node node2)
        {
            if (node1 == null || node2 == null)
            {
                return null;
            }

            Node cn1 = node1;
            Node cn2 = node2;

            Node currentNode = new Node();
            if (cn1.Value > cn2.Value)
            {
                currentNode.Value = cn2.Value;
                cn2 = cn2.Next;
            }
            else
            {
                currentNode.Value = cn1.Value;
                cn1 = cn1.Next;
            }

            var head = currentNode;
            
            while (cn1 != null && cn2 != null)
            {
                if (cn1.Value > cn2.Value)
                {
                    var n = new Node{Value = cn2.Value};
                    currentNode.Next = n;
                    cn2 = cn2.Next;
                }
                else
                {
                    var n = new Node{Value = cn1.Value};
                    currentNode.Next = n;
                    cn1 = cn1.Next;
                }

                currentNode = currentNode.Next;
            }

            while (cn1 != null)
            {
                var n = new Node{Value = cn1.Value};
                currentNode.Next = n;
                cn1 = cn1.Next;
                currentNode = currentNode.Next;
            }
            
            while (cn2 != null)
            {
                var n = new Node{Value = cn2.Value};
                currentNode.Next = n;
                cn2 = cn2.Next;
                currentNode = currentNode.Next;
            }

            return head;
        }
    }

    [TestFixture]
    public class MergeTest
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var node11 = new Node{Value = 1};
            var node12 = new Node{Value = 2};
            var node13 = new Node{Value = 4};

            node11.Next = node12;
            node12.Next = node13;


            var node21 = new Node{Value = 1};
            var node22 = new Node{Value = 3};
            var node23 = new Node{Value = 4};

            node21.Next = node22;
            node22.Next = node23;

            // Act
            var n = MergeList.Merge(node11, node21);
            
            // Assert
            var prev = n.Value;
            var currentNode = n.Next;
            while (currentNode != null)
            {
                Assert.True(prev <= currentNode.Value);
                prev = currentNode.Value;
                currentNode = currentNode.Next;
            }
        }
    }
    
}