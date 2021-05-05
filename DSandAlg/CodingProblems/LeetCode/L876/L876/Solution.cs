using NUnit.Framework;

// Problem: 876. Middle of the Linked List
// URL: https://leetcode.com/problems/middle-of-the-linked-list/

namespace LeetCode
{
    public class Solution
    {
        public class ListNode
        {
            public int val;
            public ListNode next;

            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        public ListNode MiddleNode(ListNode head)
        {
            // SS: runtime complexity: O(n), slow and fast pointer
            // space complexity: O(1)
            
            ListNode slow = head;
            ListNode fast = head;

            while (fast != null)
            {
                slow = slow.next;
                fast = fast.next?.next;
            }

            return slow;
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