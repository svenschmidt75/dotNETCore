#region

using NUnit.Framework;

#endregion

// Problem: 21. Merge Two Sorted Lists
// URL: https://leetcode.com/problems/merge-two-sorted-lists/

namespace LeetCode21
{
    public class Solution
    {
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null)
            {
                return l2;
            }

            if (l2 == null)
            {
                return l1;
            }

            ListNode root = null;

            var currentL1 = l1;
            var currentL2 = l2;

            // SS: We know that l1 != null and l2 != null
            if (currentL1.val <= currentL2.val)
            {
                root = currentL1;
                currentL1 = currentL1.next;
            }
            else
            {
                root = currentL2;
                currentL2 = currentL2.next;
            }

            var current = root;

            while (currentL1 != null && currentL2 != null)
            {
                if (currentL1.val <= currentL2.val)
                {
                    // SS: store next pointer, because we'll overwrite it
                    current.next = currentL1;
                    current = current.next;
                    currentL1 = currentL1.next;
                }
                else
                {
                    current.next = currentL2;
                    current = current.next;
                    currentL2 = currentL2.next;
                }
            }

            while (currentL1 != null)
            {
                current.next = currentL1;
                current = current.next;
                currentL1 = currentL1.next;
            }

            while (currentL2 != null)
            {
                current.next = currentL2;
                current = current.next;
                currentL2 = currentL2.next;
            }

            return root;
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
                var l1 = new ListNode(1, new ListNode(2, new ListNode(4)));
                var l2 = new ListNode(1, new ListNode(3, new ListNode(4)));

                // Act
                var mergedRoot = new Solution().MergeTwoLists(l1, l2);

                // Assert
                Assert.AreEqual(1, mergedRoot.val);
                Assert.AreEqual(1, mergedRoot.next.val);
                Assert.AreEqual(2, mergedRoot.next.next.val);
                Assert.AreEqual(3, mergedRoot.next.next.next.val);
                Assert.AreEqual(4, mergedRoot.next.next.next.next.val);
                Assert.AreEqual(4, mergedRoot.next.next.next.next.next.val);
                Assert.Null(mergedRoot.next.next.next.next.next.next);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                ListNode l1 = null;
                ListNode l2 = null;

                // Act
                var mergedRoot = new Solution().MergeTwoLists(l1, l2);

                // Assert
                Assert.Null(mergedRoot);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                ListNode l1 = null;
                var l2 = new ListNode();

                // Act
                var mergedRoot = new Solution().MergeTwoLists(l1, l2);

                // Assert
                Assert.AreEqual(0, mergedRoot.val);
                Assert.Null(mergedRoot.next);
            }
        }
    }
}