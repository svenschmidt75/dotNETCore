#region

using NUnit.Framework;

#endregion

// Problem: 148. Sort List
// URL: https://leetcode.com/problems/sort-list/

namespace LeetCode
{
    public class Solution
    {
        public ListNode SortList(ListNode head)
        {
            if (head == null)
            {
                return null;
            }

            // SS: find the tail node and number of nodes in the range
            // and the middle node
            var tail = head;
            var current = head;
            var count = 0;
            while (current != null)
            {
                tail = current;
                current = current.next;
                count++;
            }

            return MergeSort(head, tail, count);
        }

        private ListNode MergeSort(ListNode left, ListNode right, int nNodes)
        {
            // SS: finding the middle element costs N, 2 * N/2, 4 * N/4, etc., so N per level
            // merging is the same, N per level
            // there are log2(N) levels, hence total runtime complexity is O(N log N)
            // since the merging happens in-place, it is O(1)
            
            if (left == right)
            {
                // SS: no more partitioning possible
                return left;
            }

            // SS: find the middle node
            var middle = left;
            var prev = left;
            var nMiddle = nNodes / 2;
            var count = 0;
            while (count < nMiddle)
            {
                prev = middle;
                middle = middle.next;
                count++;
            }

            var l = MergeSort(left, prev, nMiddle);
            var r = MergeSort(middle, right, nNodes - nMiddle);

            // SS: merge together
            var sortedHead = new ListNode();
            var node = sortedHead;
            var pl = l;
            var pr = r;
            var pil = 0;
            var pir = 0;
            while (pil < nMiddle && pir < nNodes - nMiddle)
            {
                if (pl.val < pr.val)
                {
                    node.next = pl;
                    node = node.next;
                    pl = pl.next;
                    pil++;
                }
                else
                {
                    node.next = pr;
                    node = node.next;
                    pr = pr.next;
                    pir++;
                }
            }

            while (pil < nMiddle)
            {
                node.next = pl;
                node = node.next;
                pl = pl.next;
                pil++;
            }

            while (pir < nNodes - nMiddle)
            {
                node.next = pr;
                node = node.next;
                pr = pr.next;
                pir++;
            }

            // SS: make sure we disconect the old tail from it's next node
            node.next = null;
            return sortedHead.next;
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
                var head = new ListNode(4, new ListNode(2, new ListNode(1, new ListNode(3, new ListNode(7)))));

                // Act
                var sortedHead = new Solution().SortList(head);

                // Assert
                Assert.AreEqual(1, sortedHead.val);
                Assert.AreEqual(2, sortedHead.next.val);
                Assert.AreEqual(3, sortedHead.next.next.val);
                Assert.AreEqual(4, sortedHead.next.next.next.val);
                Assert.AreEqual(7, sortedHead.next.next.next.next.val);
                Assert.IsNull(sortedHead.next.next.next.next.next);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var head = new ListNode(4, new ListNode(2, new ListNode(7, new ListNode(3, new ListNode(1)))));

                // Act
                var sortedHead = new Solution().SortList(head);

                // Assert
                Assert.AreEqual(1, sortedHead.val);
                Assert.AreEqual(2, sortedHead.next.val);
                Assert.AreEqual(3, sortedHead.next.next.val);
                Assert.AreEqual(4, sortedHead.next.next.next.val);
                Assert.AreEqual(7, sortedHead.next.next.next.next.val);
                Assert.IsNull(sortedHead.next.next.next.next.next);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var head = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));

                // Act
                var sortedHead = new Solution().SortList(head);

                // Assert
                Assert.AreEqual(1, sortedHead.val);
                Assert.AreEqual(2, sortedHead.next.val);
                Assert.AreEqual(3, sortedHead.next.next.val);
                Assert.AreEqual(4, sortedHead.next.next.next.val);
                Assert.AreEqual(5, sortedHead.next.next.next.next.val);
                Assert.IsNull(sortedHead.next.next.next.next.next);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var head = new ListNode(2, new ListNode(1));

                // Act
                var sortedHead = new Solution().SortList(head);

                // Assert
                Assert.AreEqual(1, sortedHead.val);
                Assert.AreEqual(2, sortedHead.next.val);
                Assert.IsNull(sortedHead.next.next);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var head = new ListNode(2);

                // Act
                var sortedHead = new Solution().SortList(head);

                // Assert
                Assert.AreEqual(2, sortedHead.val);
                Assert.IsNull(sortedHead.next);
            }
        }
    }
}