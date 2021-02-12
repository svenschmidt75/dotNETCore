#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 143. Reorder List
// URL: https://leetcode.com/problems/reorder-list/

namespace LeetCode
{
    public class Solution
    {
        public void ReorderList(ListNode head)
        {
            // SS: runtime complexity O(n)
            // space complexity: O(1)
            // Find the middle node and reverse the right half of the
            // list. Then stitch together alternating nodes.
            
            if (head == null || head.next == null)
            {
                return;
            }

            ListNode slow = head;
            ListNode fast = head;
            ListNode prev = null;
            while (fast?.next != null)
            {
                prev = slow;
                slow = slow.next;
                fast = fast.next.next;
            }

            prev.next = null;
            
            // SS: slow points to the middle node
            ListNode newHead = slow;
            ListNode p1 = slow;
            ListNode p2 = slow.next;
            while (p2 != null)
            {
                p1.next = p2.next;
                p2.next = newHead;
                newHead = p2;
                p2 = p1.next;
            }
            
            ListNode current = new ListNode();
            ListNode l1 = head;
            ListNode l2 = newHead;
            while (l1 != null && l2 != null)
            {
                ListNode n1 = l1.next;
                ListNode n2 = l2.next;
                current.next = l1;
                l1.next = l2;
                current = l2;
                l1 = n1;
                l2 = n2;
            }
        }

        public void ReorderList2(ListNode head)
        {
            // SS: runtime complexity O(n)
            // space complexity: O(n)
            if (head == null)
            {
                return;
            }

            var stack = new Stack<ListNode>();

            // SS: put all nodes on the stack
            ListNode current = head;
            while (current != null)
            {
                stack.Push(current);
                current = current.next;
            }

            int n = stack.Count;
            
            current = head;
            ListNode next;
            int c = 1;
            while (current != null && c < n)
            {
                next = current.next;
                ListNode node = stack.Pop();
                current.next = node;
                node.next = next;
                current = next;

                c += 2;
            }

            current.next = null;
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
                ListNode head = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));

                // Act
                new Solution().ReorderList(head);

                // Assert
                Assert.AreEqual(1, head.val);
                Assert.AreEqual(5, head.next.val);
                Assert.AreEqual(2, head.next.next.val);
                Assert.AreEqual(4, head.next.next.next.val);
                Assert.AreEqual(3, head.next.next.next.next.val);
                Assert.IsNull(head.next.next.next.next.next);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                ListNode head = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4))));

                // Act
                new Solution().ReorderList(head);

                // Assert
                Assert.AreEqual(1, head.val);
                Assert.AreEqual(4, head.next.val);
                Assert.AreEqual(2, head.next.next.val);
                Assert.AreEqual(3, head.next.next.next.val);
                Assert.IsNull(head.next.next.next.next);
            }
            
            [Test]
            public void Test3()
            {
                // Arrange
                ListNode head = new ListNode(1);

                // Act
                new Solution().ReorderList(head);

                // Assert
                Assert.AreEqual(1, head.val);
                Assert.IsNull(head.next);
            }

        }
    }
}