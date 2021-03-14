#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 206. Reverse Linked List
// URL: https://leetcode.com/problems/reverse-linked-list/

namespace LeetCode
{
    public class Solution
    {
        public ListNode ReverseList(ListNode head)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(1)
            
            if (head == null)
            {
                return null;
            }

            var h = head;
            var p1 = head;
            var p2 = head.next;
            while (p2 != null)
            {
                var p3 = p2.next;
                p1.next = p3;
                p2.next = h;
                h = p2;
                p2 = p1.next;
            }

            return h;
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
                ListNode root = new(1, new(2, new(3, new(4, new(5)))));

                // Act
                var reversed = new Solution().ReverseList(root);

                // Assert
                var values = new List<int>();
                var n = reversed;
                while (n != null)
                {
                    values.Add(n.val);
                    n = n.next;
                }

                CollectionAssert.AreEqual(new[] {5, 4, 3, 2, 1}, values);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                ListNode root = new(1);

                // Act
                var reversed = new Solution().ReverseList(root);

                // Assert
                var values = new List<int>();
                var n = reversed;
                while (n != null)
                {
                    values.Add(n.val);
                    n = n.next;
                }

                CollectionAssert.AreEqual(new[] {1}, values);
            }
            
        }
    }
}