#region

using NUnit.Framework;

#endregion

// Problem: 24. Swap Nodes in Pairs
// URL: https://leetcode.com/problems/swap-nodes-in-pairs/

namespace LeetCode
{
    public class Solution
    {
        public ListNode SwapPairs(ListNode head)
        {
            // SS: runtime complexity: O(n)

            if (head == null)
            {
                return head;
            }

            var sentinel = new ListNode(-1, head);
            var prev = sentinel;

            var current = prev.next;
            var next = current.next;
            while (current != null && next != null)
            {
                var next2 = next.next;
                prev.next = next;
                next.next = current;
                current.next = next2;
                prev = current;

                current = next2;
                next = current?.next;
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
            public void TestEven()
            {
                // Arrange
                var head = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4))));

                // Act
                var newHead = new Solution().SwapPairs(head);

                // Assert
                Assert.AreEqual(2, newHead.val);
                Assert.AreEqual(1, newHead.next.val);
                Assert.AreEqual(4, newHead.next.next.val);
                Assert.AreEqual(3, newHead.next.next.next.val);
                Assert.Null(newHead.next.next.next.next);
            }

            [Test]
            public void TestOdd()
            {
                // Arrange
                var head = new ListNode(1, new ListNode(2, new ListNode(3)));

                // Act
                var newHead = new Solution().SwapPairs(head);

                // Assert
                Assert.AreEqual(2, newHead.val);
                Assert.AreEqual(1, newHead.next.val);
                Assert.AreEqual(3, newHead.next.next.val);
                Assert.Null(newHead.next.next.next);
            }

            [Test]
            public void TestOne()
            {
                // Arrange
                var head = new ListNode(1);

                // Act
                var newHead = new Solution().SwapPairs(head);

                // Assert
                Assert.AreEqual(1, newHead.val);
                Assert.Null(newHead.next);
            }
        }
    }
}