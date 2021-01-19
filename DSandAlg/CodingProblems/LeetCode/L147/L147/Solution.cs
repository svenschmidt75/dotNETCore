#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 147. Insertion Sort List
// URL: https://leetcode.com/problems/insertion-sort-list/

namespace LeetCode
{
    public class Solution
    {
        public ListNode InsertionSortList(ListNode head)
        {
            if (head == null)
            {
                return null;
            }

            // SS: add a sentinel node
            var sortedHead = new ListNode(0, head);
            var pivotNode = head;

            while (true)
            {
                var next = pivotNode.next;
                if (next == null)
                {
                    break;
                }

                if (next.val < pivotNode.val)
                {
                    // SS: from the sortedHead, find the insertion node
                    var node = sortedHead.next;
                    var prev = sortedHead;
                    while (node.val < next.val)
                    {
                        prev = node;
                        node = node.next;
                    }

                    pivotNode.next = next.next;
                    prev.next = next;
                    next.next = node;
                }
                else
                {
                    pivotNode = next;
                }
            }

            return sortedHead.next;
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
                var head = new ListNode(1, new ListNode(3, new ListNode(5, new ListNode(6, new ListNode(7, new ListNode(8, new ListNode(2, new ListNode(4))))))));

                // Act
                var sortedHead = new Solution().InsertionSortList(head);

                // Assert
                var values = new List<int>();
                var node = sortedHead;
                while (node != null)
                {
                    values.Add(node.val);
                    node = node.next;
                }

                CollectionAssert.AreEqual(new[] {1, 2, 3, 4, 5, 6, 7, 8}, values);
            }
        }
    }
}