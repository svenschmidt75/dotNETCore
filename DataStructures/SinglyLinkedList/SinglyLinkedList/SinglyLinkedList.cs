using System.Collections;
using System.Collections.Generic;

namespace SinglyLinkedList
{
    /// <summary>
    ///     Singly linked list, immutable.
    ///     Modeled accoring to Haskell's list type.
    /// </summary>
    public class SinglyLinkedList<T> : IEnumerable<SinglyLinkedList<T>>
    {
        public SinglyLinkedList(T value)
        {
            Value = value;
            Length = 1;
        }

        private SinglyLinkedList(T value, SinglyLinkedList<T> next)
        {
            Value = value;
            Next = next;
            Length = 1 + next.Length;
        }

        public T Value { get; }
        public SinglyLinkedList<T> Next { get; private set; }
        public int Length { get; private set; }

        public IEnumerator<SinglyLinkedList<T>> GetEnumerator()
        {
            // O(N)
            var list = this;
            do
            {
                yield return list;
                list = list.Next;
            } while (list.Next != null);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public SinglyLinkedList<T> Cons(T value)
        {
            // O(1)
            return new SinglyLinkedList<T>(value, this);
        }

        internal void Append(SinglyLinkedList<T> other)
        {
            if (other == null)
                return;
            Next = other.Next;
            Length += other.Length;
        }
    }
}