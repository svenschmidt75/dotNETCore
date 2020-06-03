#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// 138. Copy List with Random Pointer
// https://leetcode.com/problems/copy-list-with-random-pointer/

namespace CopyLinkedListWithRandomPointer
{
    public class Node
    {
        public int Value { get; set; }
        public Node Next { get; set; }
        public Node Random { get; set; }
    }

    public static class DeepCopyHelper
    {
        public static Node DeepCopy(Node head)
        {
            // SS: runtime is O(n), memory complexity is also O(n)
            if (head == null)
            {
                return null;
            }

            // SS: 1st pass to map Node to index
            var node2Index = new Dictionary<Node, int>();

            var currentIndex = 0;
            var currentNode = head;
            while (currentNode != null)
            {
                node2Index.Add(currentNode, currentIndex);
                currentNode = currentNode.Next;
                currentIndex++;
            }

            // SS: 2nd pass to copy linked list, ignoring random pointer
            var copyNode2Index = new Dictionary<int, Node>();

            var headCopy = new Node {Value = head.Value};
            currentIndex = 0;
            var currentNode1 = head;
            var currentNode2 = headCopy;
            while (currentNode1 != null)
            {
                copyNode2Index.Add(currentIndex, currentNode2);
                currentIndex++;

                if (currentNode1.Next != null)
                {
                    var next = new Node {Value = currentNode1.Next.Value};
                    currentNode2.Next = next;
                }

                currentNode1 = currentNode1.Next;
                currentNode2 = currentNode2.Next;
            }

            // SS: 3rd pass to fill-in random pointer
            currentIndex = 0;
            currentNode1 = head;
            currentNode2 = headCopy;
            while (currentNode1 != null)
            {
                if (currentNode1.Random != null)
                {
                    var randomIndex = node2Index[currentNode1.Random];
                    currentNode2.Random = copyNode2Index[randomIndex];
                }

                currentNode1 = currentNode1.Next;
                currentNode2 = currentNode2.Next;
                currentIndex++;
            }

            return headCopy;
        }
    }

    [TestFixture]
    public class DeepCopyTest
    {
        private static void IsDeepCopy(Node node1, Node deepCopy)
        {
            var currentNode1 = node1;
            var currentNode2 = deepCopy;
            while (currentNode1 != null)
            {
                Assert.AreEqual(currentNode1.Value, currentNode2.Value);

                if (currentNode1.Random == null)
                {
                    Assert.IsNull(currentNode2.Random);
                }
                else
                {
                    Assert.AreEqual(currentNode1.Random.Value, currentNode2.Random.Value);
                }

                currentNode1 = currentNode1.Next;
                currentNode2 = currentNode2.Next;
            }
        }

        [Test]
        public void Test1()
        {
            // Arrange
            var node1 = new Node {Value = 7};
            var node2 = new Node {Value = 13};
            var node3 = new Node {Value = 11};
            var node4 = new Node {Value = 10};
            var node5 = new Node {Value = 1};

            node1.Next = node2;
            node2.Next = node3;
            node3.Next = node4;
            node4.Next = node5;

            node1.Random = null;
            node2.Random = node1;
            node3.Random = node5;
            node4.Random = node3;
            node5.Random = node1;

            // Act
            var deepCopy = DeepCopyHelper.DeepCopy(node1);

            // Assert
            IsDeepCopy(node1, deepCopy);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var node1 = new Node {Value = 1};
            var node2 = new Node {Value = 2};

            node1.Next = node2;
            node2.Next = null;

            node1.Random = node2;
            node2.Random = null;

            // Act
            var deepCopy = DeepCopyHelper.DeepCopy(node1);

            // Assert
            IsDeepCopy(node1, deepCopy);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var node1 = new Node {Value = 3};
            var node2 = new Node {Value = 3};
            var node3 = new Node {Value = 3};

            node1.Next = node2;
            node2.Next = node3;
            node3.Next = null;

            node1.Random = null;
            node2.Random = node1;
            node3.Random = null;

            // Act
            var deepCopy = DeepCopyHelper.DeepCopy(node1);

            // Assert
            IsDeepCopy(node1, deepCopy);
        }
    }
}