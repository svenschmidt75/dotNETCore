#region

using NUnit.Framework;

#endregion

// Problem: 237. Delete Node in a Linked List
// URL: https://leetcode.com/problems/delete-node-in-a-linked-list/

namespace LeetCode
{
    public class Solution
    {
        public void DeleteNode(ListNode node)
        {
            node.val = node.next.val;
            node.next = node.next.next;
        }

        public void DeleteNode2(ListNode node)
        {
            if (node == null)
            {
                return;
            }

            while (node.next != null)
            {
                var current = node.next;
                node.val = current.val;
                node.next = current.next == null ? null : current;
                node = current;
            }
        }

        public class ListNode
        {
            public ListNode next;
            public int val;

            public ListNode(int x)
            {
                val = x;
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