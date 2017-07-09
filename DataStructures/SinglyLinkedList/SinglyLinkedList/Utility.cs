using System.Linq;

namespace SinglyLinkedList
{
    public static class Utility
    {
        public static (bool, SinglyLinkedList<T>) Find<T>(this SinglyLinkedList<T> list, T value)
        {
            var found = list.FirstOrDefault(item => item.Value.Equals(value));
            return (found != null, found);
        }

        public static SinglyLinkedList<T> Remove<T>(this SinglyLinkedList<T> list, T value)
        {
            // O(N)
            var prev = default(SinglyLinkedList<T>);
            var current = list;
            do
            {
                if (current.Value.Equals(value))
                {
                    if (prev == null)
                    {
                        // this node gets removed, return the next one
                        return current.Next;
                    }
                    
                    // prev needs to point to next
                    prev.Next = current.Next;
                    return list;
                }
                prev = current;
                current = current.Next;

            } while (current != null);
            return list;
        }
    }
}