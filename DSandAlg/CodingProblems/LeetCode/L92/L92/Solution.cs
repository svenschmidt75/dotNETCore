#region

using NUnit.Framework;

#endregion

// Problem: 92. Reverse Linked List II
// URL: https://leetcode.com/problems/reverse-linked-list-ii/

namespace LeetCode
{
    public class Solution
    {
        public ListNode ReverseBetween(ListNode head, int m, int n)
        {
            if (head == null)
            {
                return null;
            }

            // SS: add a sentinel node as the new head, so we don't have to treat
            // the case m = 0 any different
            var sentinel = new ListNode {val = 0, next = head};

            ListNode n1 = null;
            ListNode prev = null;
            ListNode newHead = null;
            ListNode newTail = null;
            var node = sentinel;
            var cnt = 0;

            // SS: find node right before the reverse
            while (node != null)
            {
                var next = node.next;

                if (cnt == m)
                {
                    n1 = prev;
                    newTail = node;
                    newHead = node;
                }
                else if (cnt > m && cnt < n)
                {
                    node.next = newHead;
                    newHead = node;
                }
                else if (cnt == n)
                {
                    node.next = newHead;
                    newHead = node;

                    n1.next = newHead;
                    newTail.next = next;
                    break;
                }

                cnt++;
                prev = node;
                node = next;
            }

            if (n > cnt)
            {
                return null;
            }

            return sentinel.next;
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
                var head = new ListNode {val = 1, next = new ListNode {val = 2, next = new ListNode {val = 3, next = new ListNode {val = 4, next = new ListNode {val = 5}}}}};

                // Act
                var node = new Solution().ReverseBetween(head, 2, 4);

                // Assert
                Assert.AreEqual(1, node.val);
                Assert.AreEqual(4, node.next.val);
                Assert.AreEqual(3, node.next.next.val);
                Assert.AreEqual(2, node.next.next.next.val);
                Assert.AreEqual(5, node.next.next.next.next.val);
                Assert.Null(node.next.next.next.next.next);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var head = new ListNode {val = 1, next = new ListNode {val = 2, next = new ListNode {val = 3, next = new ListNode {val = 4, next = new ListNode {val = 5}}}}};

                // Act
                var node = new Solution().ReverseBetween(head, 1, 2);

                // Assert
                Assert.AreEqual(2, node.val);
                Assert.AreEqual(1, node.next.val);
                Assert.AreEqual(3, node.next.next.val);
                Assert.AreEqual(4, node.next.next.next.val);
                Assert.AreEqual(5, node.next.next.next.next.val);
                Assert.Null(node.next.next.next.next.next);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var head = new ListNode {val = 1};

                // Act
                var node = new Solution().ReverseBetween(head, 1, 1);

                // Assert
                Assert.AreEqual(1, node.val);
                Assert.Null(node.next);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var head = new ListNode {val = 1, next = new ListNode {val = 2, next = new ListNode {val = 3, next = new ListNode {val = 4, next = new ListNode {val = 5}}}}};

                // Act
                var node = new Solution().ReverseBetween(head, 3, 5);

                // Assert
                Assert.AreEqual(1, node.val);
                Assert.AreEqual(2, node.next.val);
                Assert.AreEqual(5, node.next.next.val);
                Assert.AreEqual(4, node.next.next.next.val);
                Assert.AreEqual(3, node.next.next.next.next.val);
                Assert.Null(node.next.next.next.next.next);
            }
        }
    }
}