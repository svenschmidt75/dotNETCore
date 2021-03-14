#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 203. Remove Linked List Elements
// URL: https://leetcode.com/problems/remove-linked-list-elements/

namespace LeetCode
{
    public class Solution
    {
        public ListNode RemoveElements(ListNode head, int val)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(1)
            
            if (head == null)
            {
                return null;
            }

            var sentinelHead = new ListNode(-1, head);

            var prev = sentinelHead;
            var current = head;
            while (current != null)
            {
                if (current.val == val)
                {
                    prev.next = current.next;
                }
                else
                {
                    prev = current;
                }

                current = current.next;
            }

            return sentinelHead.next;
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
                ListNode root = new(1, new(2, new(6, new(3, new(4, new(5, new(6)))))));

                // Act
                var newHead = new Solution().RemoveElements(root, 6);

                // Assert
                var values = new List<int>();
                var n = newHead;
                while (n != null)
                {
                    values.Add(n.val);
                    n = n.next;
                }

                CollectionAssert.AreEqual(new[] {1, 2, 3, 4, 5}, values);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                ListNode root = new(7, new(7, new(7, new(7))));

                // Act
                var newHead = new Solution().RemoveElements(root, 7);

                // Assert
                var values = new List<int>();
                var n = newHead;
                while (n != null)
                {
                    values.Add(n.val);
                    n = n.next;
                }

                Assert.IsEmpty(values);
            }
        }
    }
}