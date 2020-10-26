#region

using System;
using NUnit.Framework;

#endregion


// 2. Add Two Numbers
// https://leetcode.com/problems/add-two-numbers/

namespace L2
{
    public class Solution
    {
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var node1 = l1;
            var node2 = l2;

            ListNode result = null;
            ListNode node = null;

            var carry = 0;
            var sum = 0;

            while (node1 != null || node2 != null)
            {
                var n1 = 0;
                if (node1 != null)
                {
                    n1 = node1.val;
                    node1 = node1.next;
                }

                var n2 = 0;
                if (node2 != null)
                {
                    n2 = node2.val;
                    node2 = node2.next;
                }

                var s = n1 + n2 + carry;
                carry = 0;
                if (s > 9)
                {
                    s -= 10;
                    carry = 1;
                }

                sum += s;

                var newNode = new ListNode {val = s};
                if (result == null)
                {
                    result = newNode;
                    node = newNode;
                }
                else
                {
                    node.next = newNode;
                    node = newNode;
                }
            }

            // SS: add carry
            if (carry > 0)
            {
                node.next = new ListNode {val = 1};
            }

            return result;
        }

        public ListNode AddTwoNumbers2(ListNode l1, ListNode l2)
        {
            var arr = new int[100];

            var node = l1;
            var n = 0;
            while (node != null)
            {
                arr[n++] = node.val;
                node = node.next;
            }

            var number1 = Convert2Int(arr, n);

            node = l2;
            n = 0;
            while (node != null)
            {
                arr[n++] = node.val;
                node = node.next;
            }

            var number2 = Convert2Int(arr, n);

            var sum = number1 + number2;
            if (sum > 0)
            {
                var nDigits = (int) Math.Log10(sum);
                var exp = (long) Math.Pow(10, nDigits);
                var i = 0;
                node = null;
                while (i <= nDigits)
                {
                    var digit = (int) (sum / exp);
                    var newNode = new ListNode(digit);
                    if (node == null)
                    {
                        node = newNode;
                    }
                    else
                    {
                        newNode.next = node;
                        node = newNode;
                    }

                    sum -= digit * exp;
                    exp /= 10;
                    i++;
                }
            }
            else
            {
                node = new ListNode {val = 0};
            }

            return node;
        }

        private long Convert2Int(int[] arr, int n)
        {
            long number = 0;

            var exp = (long) Math.Pow(10, n - 1);
            while (n > 0)
            {
                var digit = arr[n - 1];
                number += digit * exp;
                n--;
                exp /= 10;
            }

            return number;
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
                var l1 = new ListNode {val = 2, next = new ListNode {val = 4, next = new ListNode {val = 3}}};
                var l2 = new ListNode {val = 5, next = new ListNode {val = 6, next = new ListNode {val = 4}}};

                // Act
                var sum = new Solution().AddTwoNumbers(l1, l2);

                // Assert
                Assert.AreEqual(7, sum.val);
                Assert.AreEqual(0, sum.next.val);
                Assert.AreEqual(8, sum.next.next.val);
                Assert.IsNull(sum.next.next.next);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var l1 = new ListNode {val = 0};
                var l2 = new ListNode {val = 0};

                // Act
                var sum = new Solution().AddTwoNumbers(l1, l2);

                // Assert
                Assert.AreEqual(0, sum.val);
                Assert.IsNull(sum.next);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var l1 = new ListNode
                {
                    val = 9
                    , next = new ListNode
                        {val = 9, next = new ListNode {val = 9, next = new ListNode {val = 9, next = new ListNode {val = 9, next = new ListNode {val = 9, next = new ListNode {val = 9}}}}}}
                };
                var l2 = new ListNode {val = 9, next = new ListNode {val = 9, next = new ListNode {val = 9, next = new ListNode {val = 9}}}};

                // Act
                var sum = new Solution().AddTwoNumbers(l1, l2);

                // Assert
                Assert.AreEqual(8, sum.val);
                Assert.AreEqual(9, sum.next.val);
                Assert.AreEqual(9, sum.next.next.val);
                Assert.AreEqual(9, sum.next.next.next.val);
                Assert.AreEqual(0, sum.next.next.next.next.val);
                Assert.AreEqual(0, sum.next.next.next.next.next.val);
                Assert.AreEqual(0, sum.next.next.next.next.next.next.val);
                Assert.AreEqual(1, sum.next.next.next.next.next.next.next.val);
                Assert.IsNull(sum.next.next.next.next.next.next.next.next);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var l1 = new ListNode {val = 9};
                var l2 = new ListNode
                {
                    val = 1
                    , next = new ListNode
                    {
                        val = 9
                        , next = new ListNode
                        {
                            val = 9, next = new ListNode
                            {
                                val = 9
                                , next = new ListNode
                                {
                                    val = 9
                                    , next = new ListNode
                                    {
                                        val = 9
                                        , next = new ListNode
                                        {
                                            val = 9
                                            , next = new ListNode
                                            {
                                                val = 9
                                                , next = new ListNode
                                                {
                                                    val = 9
                                                    , next = new ListNode
                                                    {
                                                        val = 9
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };

                // Act
                var sum = new Solution().AddTwoNumbers(l1, l2);

                // Assert
                Assert.AreEqual(0, sum.val);
                Assert.AreEqual(0, sum.next.val);
                Assert.AreEqual(0, sum.next.next.val);
                Assert.AreEqual(0, sum.next.next.next.val);
                Assert.AreEqual(0, sum.next.next.next.next.val);
                Assert.AreEqual(0, sum.next.next.next.next.next.val);
                Assert.AreEqual(0, sum.next.next.next.next.next.next.val);
                Assert.AreEqual(0, sum.next.next.next.next.next.next.next.val);
                Assert.AreEqual(0, sum.next.next.next.next.next.next.next.next.val);
                Assert.AreEqual(0, sum.next.next.next.next.next.next.next.next.next.val);
                Assert.AreEqual(1, sum.next.next.next.next.next.next.next.next.next.next.val);
                Assert.IsNull(sum.next.next.next.next.next.next.next.next.next.next.next);
            }
        }
    }
}