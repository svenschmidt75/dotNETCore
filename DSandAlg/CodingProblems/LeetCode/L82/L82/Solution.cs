#region

using NUnit.Framework;

#endregion

// Problem: 82. Remove Duplicates from Sorted List II
// URL: https://leetcode.com/problems/remove-duplicates-from-sorted-list-ii/

namespace LeetCode
{
    public class Solution
    {
        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null)
            {
                return head;
            }

            // SS: skip duplicates at the beginning
            ListNode newHead = null;
            var current = head;
            while (current != null)
            {
                var last = FindLastWithSameValue(current);
                if (current == last)
                {
                    // SS: not a duplicate node
                    newHead = current;
                    break;
                }

                current = last.next;
            }

            if (newHead == null)
            {
                return newHead;
            }

            // SS: newHead is not a duplicate node
            var prev = newHead;
            current = prev.next;
            while (true)
            {
                var last = FindLastWithSameValue(current);
                if (last == null)
                {
                    prev.next = null;
                    break;
                }

                if (last != current)
                {
                    current = last.next;
                }
                else
                {
                    // SS: not a duplicate node
                    prev.next = current;
                    prev = current;
                    current = current.next;
                }
            }

            return newHead;
        }

        private static ListNode FindLastWithSameValue(ListNode node)
        {
            if (node == null)
            {
                return null;
            }

            var prev = node;
            var next = node.next;
            while (next != null && node.val == next.val)
            {
                prev = next;
                next = next.next;
            }

            return prev;
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
                var head = new ListNode {val = 1, next = new ListNode {val = 1}};

                // Act
                var newHead = new Solution().DeleteDuplicates(head);

                // Assert
                Assert.Null(newHead);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var head = new ListNode {val = 1, next = new ListNode {val = 1, next = new ListNode {val = 2}}};

                // Act
                var newHead = new Solution().DeleteDuplicates(head);

                // Assert
                Assert.AreEqual(2, newHead.val);
                Assert.Null(newHead.next);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var head = new ListNode {val = 1, next = new ListNode {val = 1, next = new ListNode {val = 2, next = new ListNode {val = 2}}}};

                // Act
                var newHead = new Solution().DeleteDuplicates(head);

                // Assert
                Assert.Null(newHead);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var head = new ListNode
                    {val = 1, next = new ListNode {val = 1, next = new ListNode {val = 2, next = new ListNode {val = 3, next = new ListNode {val = 3, next = new ListNode {val = 4}}}}}};

                // Act
                var newHead = new Solution().DeleteDuplicates(head);

                // Assert
                Assert.AreEqual(2, newHead.val);
                Assert.AreEqual(4, newHead.next.val);
                Assert.Null(newHead.next.next);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var head = new ListNode
                {
                    val = 1
                    , next = new ListNode
                        {val = 1, next = new ListNode {val = 2, next = new ListNode {val = 3, next = new ListNode {val = 3, next = new ListNode {val = 4, next = new ListNode {val = 4}}}}}}
                };

                // Act
                var newHead = new Solution().DeleteDuplicates(head);

                // Assert
                Assert.AreEqual(2, newHead.val);
                Assert.Null(newHead.next);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var head = new ListNode
                {
                    val = 1
                    , next = new ListNode
                        {val = 2, next = new ListNode {val = 3, next = new ListNode {val = 3, next = new ListNode {val = 4, next = new ListNode {val = 4, next = new ListNode {val = 5}}}}}}
                };

                // Act
                var newHead = new Solution().DeleteDuplicates(head);

                // Assert
                Assert.AreEqual(1, newHead.val);
                Assert.AreEqual(2, newHead.next.val);
                Assert.AreEqual(5, newHead.next.next.val);
                Assert.Null(newHead.next.next.next);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var head = new ListNode {val = 1, next = new ListNode {val = 1, next = new ListNode {val = 1, next = new ListNode {val = 2, next = new ListNode {val = 3}}}}};

                // Act
                var newHead = new Solution().DeleteDuplicates(head);

                // Assert
                Assert.AreEqual(2, newHead.val);
                Assert.AreEqual(3, newHead.next.val);
                Assert.Null(newHead.next.next);
            }
        }
    }
}