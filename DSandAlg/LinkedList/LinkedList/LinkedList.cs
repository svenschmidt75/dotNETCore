using System;

namespace LinkedList
{
    public class LinkedList<T>
    {
        public LinkedList()
        {
        }

        public LinkedList(Node<T> head, Node<T> tail)
        {
            Head = head;
            Tail = tail;
            // TODO SS: initialize Length
        }

        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }
        private int Length { get; set; }

        public void Prepend(T item)
        {
            var node = new Node<T> {Value = item};
            node.Next = Head;
            Head = node;
            Length++;
        }

        public void Append(T item)
        {
            var node = new Node<T> {Value = item};
            if (Head == null) Head = node;
            if (Tail != null) Tail.Next = node;
            Tail = node;
            Length++;
        }

        public void Insert(int index, T item)
        {
            var parent = At(index);
            var node = new Node<T> {Value = item, Next = parent.Next};
            parent.Next = node;
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
                Length--;
                if (Length == 0)
                {
                    Tail = null;
                }
            }
            else
            {
                var parent = At(index - 1);
                if (parent.Next == Tail)
                {
                    // SS: we are removing the tail
                    Tail = parent;
                }
                else
                {
                    parent.Next = parent.Next.Next;
                }
                Length--;
            }
        }
    }
}