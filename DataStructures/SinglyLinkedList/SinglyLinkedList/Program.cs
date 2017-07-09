using System;

namespace SinglyLinkedList
{
    public class SinglyLinkedList<T>
    {
        public T Value { get; }
        public SinglyLinkedList<T> Next { get; } = null;

        public SinglyLinkedList(T value)
        {
            Value = value;
        }

        private SinglyLinkedList(T value, SinglyLinkedList<T> next)
        {
            Value = value;
            Next = next;
        }

        public SinglyLinkedList<T> Cons(T value)
        {
            return new SinglyLinkedList<T>(value, this);
        }
    }
}
