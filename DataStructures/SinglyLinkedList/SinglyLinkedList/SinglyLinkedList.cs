using System.Collections;
using System.Collections.Generic;

namespace SinglyLinkedList
{
    public class SinglyLinkedList<T> : IEnumerable<SinglyLinkedList<T>>
    {
        public T Value { get; }
        public SinglyLinkedList<T> Next { get; set; } = null;

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
            // O(1)
            return new SinglyLinkedList<T>(value, this);
        }

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
    }
}
