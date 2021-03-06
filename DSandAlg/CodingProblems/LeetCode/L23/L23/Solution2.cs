#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace LeetCode23
{
    public class Solution2
    {
        public ListNode MergeKLists(ListNode[] lists)
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
                var root = new Solution2().MergeKLists(lists);

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
                var root = new Solution2().MergeKLists(lists);

                // Assert
                Assert.Null(root);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                ListNode[] lists = {null};

                // Act
                var root = new Solution2().MergeKLists(lists);

                // Assert
                Assert.Null(root);
            }
        }
    }
}