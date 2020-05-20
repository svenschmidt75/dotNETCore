namespace DoubleLinkedList
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Prev { get; set; }
        public Node<T> Next { get; set; }
    }
}