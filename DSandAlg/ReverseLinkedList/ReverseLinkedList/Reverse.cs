using System;
using LinkedList;

namespace ReverseLinkedList
{
    public static class Reverse
    {
        public static LinkedList<T> ReverseLinkedList<T>(this LinkedList<T> master)
        {
            var (head, tail) = Reverse_internal(master.Head);
            return new LinkedList<T>(head, tail);
        }

        // SS: A recursive approach to reversing a linked list.
        // Alternatively, use a stack...
        private static (Node<T>, Node<T>) Reverse_internal<T>(Node<T> node)
        {
            var n = new Node<T>{Value = node.Value};

            // SS: base case
            if (node.Next == null)
            {
                return (n, n);
            }

            // SS: recursive case
            var (head, currentNode) = Reverse_internal(node.Next);
            currentNode.Next = n;
            return (head, n);
        }

        public static void ReverseNonRecursion<T>(this LinkedList<T> master)
        {
            var first = master.Head;
            if (first.Next == null)
            {
                // SS: only one element in linked list
                return;
            }
            master.Tail = first;
            var second = first.Next;
            while (second != null)
            {
                var third = second.Next;
                second.Next = first;
                first = second;
                second = third;
            }

            master.Head = first;
            master.Tail.Next = null;
        }
        
    }
}