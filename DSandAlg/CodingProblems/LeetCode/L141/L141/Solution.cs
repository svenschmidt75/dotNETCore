#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 141. Linked List Cycle
// URL: https://leetcode.com/problems/linked-list-cycle/

namespace LeetCode
{
    public class Solution
    {
        public bool HasCycle(ListNode head)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(1)
            // Uses slow pointer that moves at 1 node at a time, and
            // a fast pointer that moves at 2 nodes at a time...

            if (head == null)
            {
                return false;
            }

            var slow = head;
            var fast = slow.next?.next;

            while (slow != null && fast != null)
            {
                if (slow == fast)
                {
                    return true;
                }

                slow = slow.next;
                fast = fast.next?.next;
            }

            return false;
        }

        public bool HasCycleLinearMemory(ListNode head)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(N)

            if (head == null)
            {
                return false;
            }

            var map = new HashSet<ListNode>();

            var current = head;
            while (current != null)
            {
                if (map.Contains(current))
                {
                    return true;
                }

                map.Add(current);
                current = current.next;
            }

            return false;
        }

        public class ListNode
        {
            public ListNode next;
            public int val;

            public ListNode(int x)
            {
                val = x;
                next = null;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act

                // Assert
            }
        }
    }
}