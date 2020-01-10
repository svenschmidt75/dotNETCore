using System;

namespace DoubleLinkedList
{
    public class DoublyLinkedList<T>
    {
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }
        private int Length { get; set; }

        public void Prepend(T item)
        {
            var node = new Node<T> {Value = item};
            if (Head != null) Head.Prev = node;
            node.Next = Head;
            Head = node;
            Length++;
        }

        public void Append(T item)
        {
            var node = new Node<T> {Value = item};
            if (Head == null) Head = node;
            if (Tail != null)
            {
                Tail.Next = node;
                node.Prev = Tail;
            }

            Tail = node;
            Length++;
        }

        public void Insert(int index, T item)
        {
            var parent = At(index);
            var next = parent.Next;
            var node = new Node<T> {Value = item, Prev = parent, Next = next};
            if (parent == Tail) Tail = node;
            parent.Next = node;
            if (next != null) next.Prev = node;
            Length++;
        }

        public Node<T> At(int index)
        {
            if (index < 0 || index >= Length) throw new ArgumentException($"Index {index} out of range");
            var currentNode = Head;
            var nodeIndex = 0;
            while (nodeIndex < index)
            {
                currentNode = currentNode.Next;
                nodeIndex++;
            }

            return currentNode;
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= Length) throw new ArgumentException($"Index {index} out of range");
            if (index == 0)
            {
                Head = Head.Next;
                Head.Prev = null;
                Length--;
                if (Length == 0) Tail = null;
            }
            else
            {
                var parent = At(index - 1);
                if (parent.Next == Tail)
                {
                    // SS: we are removing the tail
                    Tail.Prev = null;
                    Tail = parent;
                    Tail.Next = null;
                }
                else
                {
                    var next = parent.Next;
                    parent.Next = next.Next;
                    parent.Next.Prev = parent;
                    next.Prev = null;
                    next.Next = null;
                }

                Length--;
            }
        }
    }
}