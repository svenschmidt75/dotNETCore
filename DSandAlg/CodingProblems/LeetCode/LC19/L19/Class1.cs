#region

using NUnit.Framework;

#endregion

namespace L19
{
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

    public class Solution
    {
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            var p1 = head;
            var p2 = head;
            var p3 = head;

            while (p1.next != null)
            {
                p1 = p1.next;

                // SS: advance p2 by n+-1 to find the element before n-th
                for (var i = 0; i < n + 1; i++)
                {
                    p2 = p2.next;
                    if (p2 == null)
                    {
                        p2 = head;
                    }
                }

                // SS: advance p3 by n
                for (var i = 0; i < n; i++)
                {
                    p3 = p3.next;
                    if (p3 == null)
                    {
                        p3 = head;
                    }
                }
            }

            // p2 points to (n-1)th from end
            // p3 points to n-th from end
            if (head == p3)
            {
                return head.next;
            }

            p2.next = p3.next;
            return head;
        }
    }

    [TestFixture]
    public class SolutionTest
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var node5 = new ListNode(5);
            var node4 = new ListNode(4, node5);
            var node3 = new ListNode(3, node4);
            var node2 = new ListNode(2, node3);
            var node1 = new ListNode(1, node2);

            // Act
            var head = new Solution().RemoveNthFromEnd(node1, 2);

            // Assert
            Assert.AreSame(node1, head);
            Assert.AreSame(node2, head.next);
            Assert.AreSame(node3, head.next.next);
            Assert.AreSame(node5, head.next.next.next);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var node1 = new ListNode(1);

            // Act
            var head = new Solution().RemoveNthFromEnd(node1, 1);

            // Assert
            Assert.IsNull(head);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var node3 = new ListNode(3);
            var node2 = new ListNode(2, node3);
            var node1 = new ListNode(1, node2);

            // Act
            var head = new Solution().RemoveNthFromEnd(node1, 3);

            // Assert
            Assert.AreSame(node2, head);
            Assert.AreSame(node3, head.next);
        }
    }
}