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
    }
}