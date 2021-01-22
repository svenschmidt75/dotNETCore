#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 138. Copy List with Random Pointer
// URL: https://leetcode.com/problems/copy-list-with-random-pointer/

namespace LeetCode
{
    public class Solution
    {
        public Node CopyRandomList(Node head)
        {
            if (head == null)
            {
                return null;
            }

            // SS: add a sentinel node
            var dcHead = new Node(0);

            var current = head;
            var dcCurrent = dcHead;

            var map = new Dictionary<Node, Node>();

            while (current != null)
            {
                var dcNode = new Node(current.val);

                map[current] = dcNode;

                dcCurrent.next = dcNode;
                dcCurrent = dcCurrent.next;
                current = current.next;
            }

            // SS: fix up random pointer
            current = head;
            dcCurrent = dcHead.next;

            while (current != null)
            {
                Node dcNode = null;
                if (current.random != null)
                {
                    dcNode = map[current.random];
                }

                dcCurrent.random = dcNode;

                dcCurrent = dcCurrent.next;
                current = current.next;
            }

            return dcHead.next;
        }

        public class Node
        {
            public Node next;
            public Node random;
            public int val;

            public Node(int _val)
            {
                val = _val;
                next = null;
                random = null;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var node7 = new Node(7);
                var node13 = new Node(13);
                var node11 = new Node(11);
                var node10 = new Node(10);
                var node1 = new Node(1);

                node7.next = node13;
                node13.next = node11;
                node11.next = node10;
                node10.next = node1;

                node7.random = null;
                node13.random = node7;
                node11.random = node1;
                node10.random = node11;
                node1.random = node7;

                // Act
                var clone = new Solution().CopyRandomList(node7);

                // Assert
                var node7Clone = clone;
                var node13Clone = node7Clone.next;
                var node11Clone = node13Clone.next;
                var node10Clone = node11Clone.next;
                var node1Clone = node10Clone.next;

                Assert.Null(node1Clone.next);
                Assert.AreNotSame(node7, node7Clone);
                Assert.AreNotSame(node13, node13Clone);
                Assert.AreNotSame(node11, node11Clone);
                Assert.AreNotSame(node10, node10Clone);
                Assert.AreNotSame(node1, node1Clone);

                Assert.Null(node7Clone.random);
                Assert.AreSame(node13Clone.random, node7Clone);
                Assert.AreSame(node11Clone.random, node1Clone);
                Assert.AreSame(node10Clone.random, node11Clone);
                Assert.AreSame(node1Clone.random, node7Clone);
            }
        }
    }
}