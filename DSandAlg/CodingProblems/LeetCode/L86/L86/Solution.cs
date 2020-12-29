#region

using NUnit.Framework;

#endregion

// Problem: 86. Partition List
// URL: https://leetcode.com/problems/partition-list/

namespace LeetCode
{
    public class Solution
    {
        public ListNode Partition(ListNode head, int x)
        {
            ListNode head1 = null;
            ListNode tail1 = null;

            ListNode head2 = null;
            ListNode tail2 = null;

            var node = head;
            while (node != null)
            {
                if (node.val < x)
                {
                    if (head1 == null)
                    {
                        head1 = node;
                        tail1 = node;
                    }
                    else
                    {
                        tail1.next = node;
                        tail1 = node;
                    }
                }
                else
                {
                    if (head2 == null)
                    {
                        head2 = node;
                        tail2 = node;
                    }
                    else
                    {
                        tail2.next = node;
                        tail2 = node;
                    }
                }

                node = node.next;
            }

            if (head1 == null)
            {
                return head2;
            }

            tail1.next = head2;

            if (tail2 != null)
            {
                tail2.next = null;
            }

            return head1;
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
                ListNode head = new() {val = 1, next = new() {val = 4, next = new() {val = 3, next = new() {val = 2, next = new() {val = 5, next = new() {val = 2}}}}}};

                // Act
                var newHead = new Solution().Partition(head, 3);

                // Assert
                Assert.AreEqual(1, newHead.val);
                Assert.AreEqual(2, newHead.next.val);
                Assert.AreEqual(2, newHead.next.next.val);
                Assert.AreEqual(4, newHead.next.next.next.val);
                Assert.AreEqual(3, newHead.next.next.next.next.val);
                Assert.AreEqual(5, newHead.next.next.next.next.next.val);
                Assert.Null(newHead.next.next.next.next.next.next);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                ListNode head = new() {val = 2, next = new() {val = 3, next = new() {val = 4, next = new() {val = 5, next = new() {val = 6, next = new() {val = 7}}}}}};

                // Act
                var newHead = new Solution().Partition(head, 1);

                // Assert
                Assert.AreEqual(2, newHead.val);
                Assert.AreEqual(3, newHead.next.val);
                Assert.AreEqual(4, newHead.next.next.val);
                Assert.AreEqual(5, newHead.next.next.next.val);
                Assert.AreEqual(6, newHead.next.next.next.next.val);
                Assert.AreEqual(7, newHead.next.next.next.next.next.val);
                Assert.Null(newHead.next.next.next.next.next.next);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                ListNode head = new() {val = 2, next = new() {val = 3, next = new() {val = 4, next = new() {val = 5, next = new() {val = 6, next = new() {val = 7}}}}}};

                // Act
                var newHead = new Solution().Partition(head, 10);

                // Assert
                Assert.AreEqual(2, newHead.val);
                Assert.AreEqual(3, newHead.next.val);
                Assert.AreEqual(4, newHead.next.next.val);
                Assert.AreEqual(5, newHead.next.next.next.val);
                Assert.AreEqual(6, newHead.next.next.next.next.val);
                Assert.AreEqual(7, newHead.next.next.next.next.next.val);
                Assert.Null(newHead.next.next.next.next.next.next);
            }
        }
    }
}