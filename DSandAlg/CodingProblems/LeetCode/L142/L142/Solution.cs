#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem:
// URL:

namespace LeetCode
{
    public class Solution
    {
        public ListNode DetectCycle(ListNode head)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(1)
            // Uses slow pointer that moves at 1 node at a time, and
            // a fast pointer that moves at 2 nodes at a time...

            if (head == null)
            {
                return null;
            }

            var foundCycle = false;

            var slow = head;
            var fast = head;

            while (slow != null && fast?.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;

                if (slow == fast)
                {
                    // SS: we found a cycle
                    foundCycle = true;
                    break;
                }
            }

            if (foundCycle == false)
            {
                return null;
            }

            // SS: advance both until they meet
            slow = head;
            while (slow != fast)
            {
                slow = slow.next;
                fast = fast.next;
            }

            // SS: the node they meet is the start of the cycle
            return slow;
        }

        public ListNode DetectCycle2(ListNode head)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(1)
            // Uses slow pointer that moves at 1 node at a time, and
            // a fast pointer that moves at 2 nodes at a time...

            if (head == null)
            {
                return null;
            }

            var foundCycle = false;

            var slow = head;
            var fast = slow.next?.next;

            while (slow != null && fast != null)
            {
                if (slow == fast)
                {
                    // SS: we found a cycle
                    foundCycle = true;
                    break;
                }

                slow = slow.next;
                fast = fast.next?.next;
            }

            if (foundCycle == false)
            {
                return null;
            }

            // SS: determine length of cycle
            var m = 1;
            slow = slow.next;
            while (slow != fast)
            {
                m++;
                slow = slow.next;
            }

            // SS: move fast to mth node
            fast = head;
            for (var i = 0; i < m; i++)
            {
                fast = fast.next;
            }

            // SS: advance both until they meet
            slow = head;
            while (slow != fast)
            {
                slow = slow.next;
                fast = fast.next;
            }

            // SS: the node they meet is the start of the cycle
            return slow;
        }

        public ListNode DetectCycleLinearMemory(ListNode head)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(N)

            if (head == null)
            {
                return null;
            }

            var freqMap = new HashSet<ListNode>();

            var current = head;
            while (current != null)
            {
                if (freqMap.Contains(current))
                {
                    return current;
                }

                freqMap.Add(current);

                current = current.next;
            }

            return null;
        }

        public class ListNode
        {
            public ListNode next;
            public int val;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var head = new ListNode {val = 3};
                var startCycle = new ListNode {val = 2};
                head.next = startCycle;
                startCycle.next = new ListNode {val = 0, next = new ListNode {val = -4, next = startCycle}};

                // Act
                var node = new Solution().DetectCycle(head);

                // Assert
                Assert.AreEqual(2, node.val);
            }
        }
    }
}