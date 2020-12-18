#region

using NUnit.Framework;

#endregion

// Problem: 61. Rotate List
// URL: https://leetcode.com/problems/rotate-list/

namespace LeetCode
{
    public class Solution
    {
        public ListNode RotateRight(ListNode head, int k)
        {
            // SS: runtime complexity: O(n)
            
            if (head?.next == null)
            {
                return head;
            }

            // SS: length of list
            var n = 0;
            var current = head;
            ListNode tail = null;
            while (current != null)
            {
                n++;
                tail = current;
                current = current.next;
            }

            var k2 = k % n;

            if (k2 == 0)
            {
                return head;
            }

            // SS: find new tail
            var i = 0;
            current = head;
            ListNode newTail = null;
            while (current != null)
            {
                if (i == n - k2 - 1)
                {
                    newTail = current;
                    break;
                }

                i++;
                current = current.next;
            }

            var newHead = newTail.next;
            tail.next = head;
            newTail.next = null;
            return newHead;
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
                var head = CreateList();

                // Act
                var node = new Solution().RotateRight(head, 1);

                // Assert
                Assert.AreEqual(5, node.val);
                Assert.AreEqual(1, node.next.val);
                Assert.AreEqual(2, node.next.next.val);
                Assert.AreEqual(3, node.next.next.next.val);
                Assert.AreEqual(4, node.next.next.next.next.val);
                Assert.Null(node.next.next.next.next.next);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var head = CreateList();

                // Act
                var node = new Solution().RotateRight(head, 2);

                // Assert
                Assert.AreEqual(4, node.val);
                Assert.AreEqual(5, node.next.val);
                Assert.AreEqual(1, node.next.next.val);
                Assert.AreEqual(2, node.next.next.next.val);
                Assert.AreEqual(3, node.next.next.next.next.val);
                Assert.Null(node.next.next.next.next.next);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var head = CreateList();

                // Act
                var node = new Solution().RotateRight(head, 5);

                // Assert
                Assert.AreEqual(1, node.val);
                Assert.AreEqual(2, node.next.val);
                Assert.AreEqual(3, node.next.next.val);
                Assert.AreEqual(4, node.next.next.next.val);
                Assert.AreEqual(5, node.next.next.next.next.val);
                Assert.Null(node.next.next.next.next.next);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var head = CreateList();

                // Act
                var node = new Solution().RotateRight(head, 7);

                // Assert
                Assert.AreEqual(4, node.val);
                Assert.AreEqual(5, node.next.val);
                Assert.AreEqual(1, node.next.next.val);
                Assert.AreEqual(2, node.next.next.next.val);
                Assert.AreEqual(3, node.next.next.next.next.val);
                Assert.Null(node.next.next.next.next.next);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                ListNode head = null;

                // Act
                var node = new Solution().RotateRight(head, 1);

                // Assert
                Assert.Null(node);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var head = new ListNode {val = 1};

                // Act
                var node = new Solution().RotateRight(head, 3);

                // Assert
                Assert.AreEqual(1, node.val);
                Assert.Null(node.next);
            }

            private static ListNode CreateList()
            {
                var head = new ListNode
                {
                    val = 1
                    , next = new ListNode
                    {
                        val = 2
                        , next = new ListNode
                        {
                            val = 3
                            , next = new ListNode
                            {
                                val = 4
                                , next = new ListNode
                                {
                                    val = 5
                                }
                            }
                        }
                    }
                };
                return head;
            }
        }
    }
}