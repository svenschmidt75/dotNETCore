#region

using NUnit.Framework;

#endregion

// Problem: 83. Remove Duplicates from Sorted List
// URL: https://leetcode.com/problems/remove-duplicates-from-sorted-list/

namespace LeetCode
{
    public class Solution
    {
        public ListNode DeleteDuplicates(ListNode head)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(1)

            if (head == null)
            {
                return null;
            }

            var p1 = head;
            while (p1 != null)
            {
                var p2 = p1;
                while (p2 != null && p2.val == p1.val)
                {
                    p2 = p2.next;
                }

                p1.next = p2;
                p1 = p1.next;
            }

            return head;
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

                // Act

                // Assert
            }
        }
    }
}