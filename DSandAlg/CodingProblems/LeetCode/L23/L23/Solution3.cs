#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace LeetCode23
{
    public class Solution3
    {
        public ListNode MergeKLists(ListNode[] lists)
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
                var root = new Solution3().MergeKLists(lists);

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
                var root = new Solution3().MergeKLists(lists);

                // Assert
                Assert.Null(root);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                ListNode[] lists = {null};

                // Act
                var root = new Solution3().MergeKLists(lists);

                // Assert
                Assert.Null(root);
            }
        }
    }
}