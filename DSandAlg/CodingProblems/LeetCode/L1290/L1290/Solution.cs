#region

using System;
using NUnit.Framework;

#endregion

namespace L1290
{
    public class Solution
    {
        public int GetDecimalValue(ListNode head)
        {
            // SS: determine number of elements
            var node = head;
            var n = 0;
            while (node != null)
            {
                n++;
                node = node.next;
            }

            if (n == 0)
            {
                return 0;
            }

            var power = n - 1;

            node = head;
            var value = 0;
            while (node != null)
            {
                var v = node.val;
                var s = v << power;
                value += s;
                power--;
                node = node.next;
            }

            return value;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var node = new ListNode {val = 1, next = new ListNode {val = 0, next = new ListNode {val = 1}}};

                // Act
                var value = new Solution().GetDecimalValue(node);

                // Assert
                Assert.AreEqual(5, value);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var node = new ListNode {val = 0};

                // Act
                var value = new Solution().GetDecimalValue(node);

                // Assert
                Assert.AreEqual(0, value);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var node = new ListNode {val = 1};

                // Act
                var value = new Solution().GetDecimalValue(node);

                // Assert
                Assert.AreEqual(1, value);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var binaryNumberString = Convert.ToString(18880, 2);
                ListNode head = null;
                ListNode node = null;

                for (var i = 0; i < binaryNumberString.Length; i++)
                {
                    var d = binaryNumberString[i];

                    var n = new ListNode
                    {
                        val = d == '1' ? 1 : 0
                    };

                    if (node == null)
                    {
                        head = n;
                        node = head;
                    }
                    else
                    {
                        node.next = n;
                        node = n;
                    }
                }

                // Act
                var value = new Solution().GetDecimalValue(head);

                // Assert
                Assert.AreEqual(18880, value);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var node = new ListNode
                {
                    val = 0
                    , next = new ListNode
                    {
                        val = 0
                    }
                };

                // Act
                var value = new Solution().GetDecimalValue(node);

                // Assert
                Assert.AreEqual(0, value);
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
    }
}