// Problem: 23. Merge k Sorted Lists
// URL: https://leetcode.com/problems/merge-k-sorted-lists/

#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace LeetCode23
{
    public class Solution
    {
        public ListNode MergeKLists(ListNode[] lists)
        {
            // SS: runtime complexity: O(k * log k + n log k) ~ O(n log k), n=list length
            var minHeap = BinaryHeap<(int val, ListNode node)>.CreateHeap((t1, t2) => t1.val > t2.val);

            // O(k log k)
            for (var i = 0; i < lists.Length; i++)
            {
                var r = lists[i];
                if (r == null)
                {
                    continue;
                }

                minHeap.Push((r.val, r));
            }

            ListNode root = null;
            ListNode current = null;

            // O(n log k)
            while (minHeap.IsEmpty == false)
            {
                (_, var node) = minHeap.Pop();

                if (root == null)
                {
                    root = node;
                    current = node;
                }
                else
                {
                    current.next = node;
                    current = node;
                }

                if (node.next != null)
                {
                    minHeap.Push((node.next.val, node.next));
                }
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
                var root = new Solution().MergeKLists(lists);

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
                var root = new Solution().MergeKLists(lists);

                // Assert
                Assert.Null(root);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                ListNode[] lists = {null};

                // Act
                var root = new Solution().MergeKLists(lists);

                // Assert
                Assert.Null(root);
            }
        }
    }
}