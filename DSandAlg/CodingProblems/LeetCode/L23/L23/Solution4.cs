#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace LeetCode23
{
    public class Solution4
    {
        public ListNode MergeKLists(ListNode[] lists)
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
                var root = new Solution4().MergeKLists(lists);

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
                var root = new Solution4().MergeKLists(lists);

                // Assert
                Assert.Null(root);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                ListNode[] lists = {null};

                // Act
                var root = new Solution4().MergeKLists(lists);

                // Assert
                Assert.Null(root);
            }
        }
    }
}