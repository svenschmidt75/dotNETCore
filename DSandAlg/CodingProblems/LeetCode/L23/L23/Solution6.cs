#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace LeetCode23
{
    public class Solution6
    {
        public ListNode MergeKLists(ListNode[] lists)
        {
            // SS: runtime complexity: O(n * log k), n=list length
            // best runtime solution

            if (lists.Length == 0)
            {
                return null;
            }

            var k = lists.Length;
            var stride = 1;

            while (stride < k)
            {
                for (var i = 0; i < k - stride; i += stride * 2)
                {
                    lists[i] = MergeTwoLists(lists[i], lists[i + stride]);
                }

                stride *= 2;
            }

            return lists[0];
        }

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
                ListNode[] lists =
                {
                    new ListNode(1, new ListNode(4, new ListNode(5)))
                    , new ListNode(1, new ListNode(3, new ListNode(4)))
                    , new ListNode(2, new ListNode(6))
                };

                // Act
                var root = new Solution6().MergeKLists(lists);

                // Assert
                var vals = new List<int>();
                var current = root;
                while (current != null)
                {
                    vals.Add(current.val);
                    current = current.next;
                }

                CollectionAssert.AreEqual(new[] {1, 1, 2, 3, 4, 4, 5, 6}, vals);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var lists = new ListNode[0];

                // Act
                var root = new Solution6().MergeKLists(lists);

                // Assert
                Assert.Null(root);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                ListNode[] lists = {null};

                // Act
                var root = new Solution6().MergeKLists(lists);

                // Assert
                Assert.Null(root);
            }
        }
    }
}