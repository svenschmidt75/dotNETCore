#region

using NUnit.Framework;

#endregion

// Problem: 160. Intersection of Two Linked Lists
// URL: https://leetcode.com/problems/intersection-of-two-linked-lists/

namespace LeetCode
{
    public class Solution
    {
        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            return GetIntersectionNodeReverse(headA, headB);
        }

        public ListNode GetIntersectionNodeSimple(ListNode headA, ListNode headB)
        {
            if (headA == null || headB == null)
            {
                return null;
            }    
            
            // SS: all values are > 0, reverse sign
            ListNode current = headA;
            while (current != null)
            {
                current.val *= -1;
                current = current.next;
            }

            ListNode intersectionNode = null;
            current = headB;
            while (current != null)
            {
                if (current.val < 0)
                {
                    intersectionNode = current;
                    break;
                }

                current = current.next;
            }
            
            // SS: reverse sign
            current = headA;
            while (current != null)
            {
                current.val *= -1;
                current = current.next;
            }

            return intersectionNode;
        }

        public ListNode GetIntersectionNodeReverse(ListNode headA, ListNode headB)
        {
            // SS: reverse headA. Connect headA to headB, and find a cycle
            // and the start node.
            if (headA == null || headB == null)
            {
                return null;
            }

            if (headA == headB)
            {
                return headA;
            }
            
            // SS: reverse headA
            ListNode headAReversed = Reverse(headA);

            // SS: connect headA to headB
            headA.next = headB;
            
            // SS: find if cycle
            ListNode slow = headAReversed;
            ListNode fast = headAReversed;
            while (fast?.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;

                if (slow == fast)
                {
                    break;
                }
            }

            if (fast?.next == null)
            {
                // SS: no cycle
                headA.next = null;
                Reverse(headAReversed);
                return null;
            }

            // SS: find start of cycle node
            ListNode node = headAReversed;
            while (node != fast)
            {
                node = node.next;
                fast = fast.next;
            }

            headA.next = null;
            Reverse(headAReversed);
            
            return node;
        }

        private static ListNode Reverse(ListNode node)
        {
            ListNode head = node;
            ListNode tail = head;
            while (tail.next != null)
            {
                var next = tail.next;
                var next2 = tail.next.next;
                tail.next = next2;
                next.next = head;
                head = next;
            }

            return head;
        }

        public class ListNode
        {
            public ListNode next;
            public int val;

            public ListNode(int x)
            {
                val = x;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var node1 = new ListNode(4);
                var node2 = new ListNode(1);
                var node3 = new ListNode(8);
                var node4 = new ListNode(4);
                var node5 = new ListNode(5);

                node1.next = node2;
                node2.next = node3;
                node3.next = node4;
                node4.next = node5;

                var node6 = new ListNode(5);
                var node7 = new ListNode(6);
                var node8 = new ListNode(1);

                node6.next = node7;
                node7.next = node8;
                node8.next = node3;
                
                // Act
                var bifurcationNode = new Solution().GetIntersectionNode(node1, node6);

                // Assert
                Assert.AreEqual(8, bifurcationNode.val);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var node1 = new ListNode(1);
                var node2 = new ListNode(1);

                node1.next = node2;
                
                // Act
                var bifurcationNode = new Solution().GetIntersectionNode(node1, node2);

                // Assert
                Assert.AreEqual(1, bifurcationNode.val);
            }
 
            [Test]
            public void Test3()
            {
                // Arrange
                var node1 = new ListNode(3);

                var node2 = new ListNode(2);
                node2.next = node1;
                
                // Act
                var bifurcationNode = new Solution().GetIntersectionNode(node1, node2);

                // Assert
                Assert.AreEqual(3, bifurcationNode.val);
            }
        }
        
    }
}