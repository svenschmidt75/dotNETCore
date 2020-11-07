#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// 25. Reverse Nodes in k-Group
// https://leetcode.com/problems/reverse-nodes-in-k-group/

namespace LeetCode25
{
    public class Solution
    {
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            var currentNode = head;

            ListNode newRoot = null;
            ListNode n = null;
            ListNode next;

            var stack = new Stack<ListNode>();

            while (currentNode != null)
            {
                stack.Push(currentNode);

                next = currentNode.next;

                if (stack.Count == k)
                {
                    while (stack.Any())
                    {
                        var p = stack.Pop();

                        if (n == null)
                        {
                            newRoot = p;
                            n = p;
                        }
                        else
                        {
                            n.next = p;
                            n = p;
                        }
                    }
                }

                currentNode = next;
            }

            next = null;
            while (stack.Any())
            {
                var p = stack.Pop();

                if (next == null)
                {
                    next = p;
                }
                else
                {
                    p.next = next;
                    next = p;
                }
            }

            n.next = next;

            return newRoot;
        }

        public class ListNode
        {
            public ListNode next;
            public int val;

            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var head = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));

                // Act
                var root = new Solution().ReverseKGroup(head, 2);

                // Assert
                var vals = new List<int>();
                var current = root;
                while (current != null)
                {
                    vals.Add(current.val);
                    current = current.next;
                }

                CollectionAssert.AreEqual(new[] {2, 1, 4, 3, 5}, vals);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var head = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));

                // Act
                var root = new Solution().ReverseKGroup(head, 3);

                // Assert
                var vals = new List<int>();
                var current = root;
                while (current != null)
                {
                    vals.Add(current.val);
                    current = current.next;
                }

                CollectionAssert.AreEqual(new[] {3, 2, 1, 4, 5}, vals);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var head = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));

                // Act
                var root = new Solution().ReverseKGroup(head, 1);

                // Assert
                var vals = new List<int>();
                var current = root;
                while (current != null)
                {
                    vals.Add(current.val);
                    current = current.next;
                }

                CollectionAssert.AreEqual(new[] {1, 2, 3, 4, 5}, vals);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var head = new ListNode(1);

                // Act
                var root = new Solution().ReverseKGroup(head, 1);

                // Assert
                var vals = new List<int>();
                var current = root;
                while (current != null)
                {
                    vals.Add(current.val);
                    current = current.next;
                }

                CollectionAssert.AreEqual(new[] {1}, vals);
            }
        }
    }
}