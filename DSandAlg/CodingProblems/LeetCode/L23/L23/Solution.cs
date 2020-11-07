#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 23. Merge k Sorted Lists
// URL: https://leetcode.com/problems/merge-k-sorted-lists/

namespace LeetCode23
{
    public class Solution
    {
        public ListNode MergeKLists(ListNode[] lists)
        {
            if (lists.Length == 0)
            {
                return null;
            }

            var k = lists.Length;

            var root = lists[0];

            for (var i = 1; i < k; i++)
            {
                root = MergeTwoLists(root, lists[i]);
            }

            return root;
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

        public ListNode MergeKLists4(ListNode[] lists)
        {
            // SS: runtime complexity: O(#maxLength * k)

            if (lists.Length == 0)
            {
                return null;
            }

            var k = lists.Length;

            ListNode root = null;

            var minValue = int.MaxValue;
            var minIdx = -1;

            for (var i = 0; i < lists.Length; i++)
            {
                var n = lists[i];
                if (n == null)
                {
                    continue;
                }

                if (n.val < minValue)
                {
                    minValue = n.val;
                    root = n;
                    minIdx = i;
                }
            }

            if (root == null)
            {
                return null;
            }

            // advance the minimum index
            lists[minIdx] = lists[minIdx].next;

            var current = root;

            while (true)
            {
                minIdx = -1;
                minValue = int.MaxValue;
                ListNode node = null;

                for (var i = 0; i < lists.Length; i++)
                {
                    var n = lists[i];
                    if (n == null)
                    {
                        continue;
                    }

                    if (n.val < minValue)
                    {
                        minValue = n.val;
                        node = n;
                        minIdx = i;
                    }
                }

                if (node == null)
                {
                    return root;
                }

                current.next = node;
                current = current.next;

                // advance the minimum index
                lists[minIdx] = node.next;
            }
        }

        public ListNode MergeKLists3(ListNode[] lists)
        {
            // SS: runtime complexity: O(#maxLength * k)
            // sorting is not necessary, as we are only interested in the smallest
            // element, which we can find in O(n) time

            if (lists.Length == 0)
            {
                return null;
            }

            var k = lists.Length;

            (int val, ListNode node)[] currentNodes = new (int, ListNode)[k];
            for (var i = 0; i < k; i++)
            {
                var node = lists[i];

                // we need to deal with empty lists
                currentNodes[i] = node != null ? (node.val, node) : (int.MaxValue, null);
            }

            // find index of min value
            var minIdx = FindMinimum(currentNodes);
            if (minIdx == -1)
            {
                return null;
            }

            var root = currentNodes[minIdx].node;

            // advance to next number in linked list
            var nextNode = root.next;
            currentNodes[minIdx] = nextNode == null ? (int.MaxValue, null) : (nextNode.val, nextNode);

            var current = root;

            while (true)
            {
                minIdx = FindMinimum(currentNodes);
                if (minIdx == -1)
                {
                    return root;
                }

                var node = currentNodes[minIdx].node;

                current.next = node;
                current = current.next;

                // advance to next number in linked list
                nextNode = node.next;
                currentNodes[minIdx] = nextNode == null ? (int.MaxValue, null) : (nextNode.val, nextNode);
            }
        }

        private int FindMinimum((int val, ListNode node)[] currentNodes)
        {
            var minIdx = -1;
            var minVal = int.MaxValue;

            for (var i = 0; i < currentNodes.Length; i++)
            {
                if (currentNodes[i].val < minVal)
                {
                    minIdx = i;
                    minVal = currentNodes[i].val;
                }
            }

            return minIdx;
        }

        public ListNode MergeKLists2(ListNode[] lists)
        {
            // SS: runtime complexity is O(#maxLength * k * log k)
            if (lists.Length == 0)
            {
                return null;
            }

            var k = lists.Length;

            (int val, ListNode node)[] currentNodes = new (int, ListNode)[k];
            for (var i = 0; i < k; i++)
            {
                var node = lists[i];

                // we need to deal with empty lists
                currentNodes[i] = node != null ? (node.val, node) : (int.MaxValue, null);
            }

            // sort at O(K log k)
            currentNodes = currentNodes.OrderBy(n => n.val).ToArray();

            var root = currentNodes[0].node;
            if (root == null)
            {
                return null;
            }

            // advance to next number in linked list
            var nextNode = currentNodes[0].node.next;
            currentNodes[0] = nextNode == null ? (int.MaxValue, null) : (nextNode.val, nextNode);

            var current = root;

            while (true)
            {
                // sort at O(K log k)
                currentNodes = currentNodes.OrderBy(n => n.val).ToArray();

                var node = currentNodes[0].node;
                if (node == null)
                {
                    return root;
                }

                current.next = node;
                current = current.next;

                // advance to next number in linked list
                nextNode = currentNodes[0].node.next;
                currentNodes[0] = nextNode == null ? (int.MaxValue, null) : (nextNode.val, nextNode);
            }
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