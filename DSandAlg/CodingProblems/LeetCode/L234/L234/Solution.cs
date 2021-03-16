#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 234. Palindrome Linked List
// URL: https://leetcode.com/problems/palindrome-linked-list/

namespace LeetCode
{
    public class Solution
    {
        public bool IsPalindrome(ListNode head)
        {
            // return IsPalindrome1(head);
            return IsPalindrome2(head);
        }

        private bool IsPalindrome2(ListNode head)
        {
            // SS: Pass 1: find length of linked list
            // Pass 2: reverse list the 1st n/2 items, them
            // compare reversed half with remaining half.
            // runtime complexity: O(n)
            // space complexity: O(1)

            var n = 0;
            var current = head;
            while (current != null)
            {
                n++;
                current = current.next;
            }

            if (n == 1)
            {
                return true;
            }

            // SS: reverse 1st half of list list
            var i = 0;
            var newHead = head;
            var p1 = head;
            var p2 = head.next;
            while (i < n / 2 - 1)
            {
                p1.next = p2.next;
                p2.next = newHead;
                newHead = p2;
                p2 = p1.next;
                i++;
            }

            // SS: skip 1st node if odd
            if (n % 2 == 1)
            {
                p2 = p2.next;
            }

            // SS: Pass 2 through reversed list
            current = newHead;
            for (var j = 0; j <= n / 2 - 1; j++)
            {
                if (current.val != p2.val)
                {
                    return false;
                }

                current = current.next;
                p2 = p2.next;
            }

            return true;
        }

        private bool IsPalindrome1(ListNode head)
        {
            // SS: Pass 1: extract values
            // Pass 2: reverse list
            // compare n/2 items
            // runtime complexity: O(n)
            // space complexity: O(n)

            var values = new List<int>();

            var current = head;
            while (current != null)
            {
                values.Add(current.val);
                current = current.next;
            }

            // SS: reverse list
            var newHead = head;
            var p1 = head;
            var p2 = head.next;
            while (p2 != null)
            {
                p1.next = p2.next;
                p2.next = newHead;
                newHead = p2;
                p2 = p1.next;
            }

            // SS: Pass 2 through reversed list
            var n = values.Count / 2;
            current = newHead;
            for (var i = 0; i < n; i++)
            {
                if (values[i] != current.val)
                {
                    return false;
                }

                current = current.next;
            }

            return true;
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
            [TestCase(new[] {1, 2, 2, 1}, true)]
            [TestCase(new[] {1, 2}, false)]
            [TestCase(new[] {1, 2, 1}, true)]
            [TestCase(new[] {1}, true)]
            [TestCase(new[] {1, 2, 6, 7, 2, 1}, false)]
            [TestCase(new[] {1, 2, 3, 2, 1}, true)]
            [TestCase(new[] {1, 2, 3, 3, 2, 1}, true)]
            public void Test1(int[] values, bool expectedIsPalindrome)
            {
                // Arrange
                var head = new ListNode(values[0]);
                var current = head;
                for (var i = 1; i < values.Length; i++)
                {
                    var node = new ListNode(values[i]);
                    current.next = node;
                    current = node;
                }

                // Act
                var isPalindrome = new Solution().IsPalindrome(head);

                // Assert
                Assert.AreEqual(expectedIsPalindrome, isPalindrome);
            }
        }
    }
}