#region

using NUnit.Framework;

#endregion

namespace L328
{
    public class Solution
    {
        public ListNode OddEvenList(ListNode head)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(1)
            
            if (head == null)
            {
                return null;
            }

            var oddHead = head;
            var evenHead = head.next;

            var oddNode = oddHead;
            var evenNode = evenHead;

            ListNode prev = null;

            while (oddNode != null)
            {
                oddNode.next = oddNode.next?.next;
                prev = oddNode;
                oddNode = oddNode.next;

                if (evenNode != null)
                {
                    evenNode.next = evenNode.next?.next;
                    evenNode = evenNode.next;
                }
            }

            // SS: stitch both together
            prev.next = evenHead;

            return oddHead;
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
                var head = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));

                // Act
                var newHead = new Solution().OddEvenList(head);

                // Assert
                Assert.AreEqual(1, newHead.val);
                Assert.AreEqual(3, newHead.next.val);
                Assert.AreEqual(5, newHead.next.next.val);
                Assert.AreEqual(2, newHead.next.next.next.val);
                Assert.AreEqual(4, newHead.next.next.next.next.val);
                Assert.Null(newHead.next.next.next.next.next);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var head = new ListNode(2, new ListNode(1, new ListNode(3, new ListNode(5, new ListNode(6, new ListNode(4, new ListNode(7)))))));

                // Act
                var newHead = new Solution().OddEvenList(head);

                // Assert
                var n = newHead;
                Assert.AreEqual(2, n.val);

                n = n.next;
                Assert.AreEqual(3, n.val);

                n = n.next;
                Assert.AreEqual(6, n.val);

                n = n.next;
                Assert.AreEqual(7, n.val);

                n = n.next;
                Assert.AreEqual(1, n.val);

                n = n.next;
                Assert.AreEqual(5, n.val);

                n = n.next;
                Assert.AreEqual(4, n.val);

                n = n.next;
                Assert.Null(n);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var head = new ListNode(1);

                // Act
                var newHead = new Solution().OddEvenList(head);

                // Assert
                Assert.AreEqual(1, newHead.val);
                Assert.Null(newHead.next);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var head = new ListNode(1, new ListNode(2));

                // Act
                var newHead = new Solution().OddEvenList(head);

                // Assert
                Assert.AreEqual(1, newHead.val);
                Assert.AreEqual(2, newHead.next.val);
                Assert.Null(newHead.next.next);
            }
        }
    }
}