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
            if (list.Value.Equals(value))
            {
                return list.Next;
            }
            var current = list.Next;
            while (current != null)
            {
                if (current.Value.Equals(value))
                {
                    break;
                }
                current = current.Next;
            }
            if (current == null)
            {
                // item not found
                return list;
            }
            var newList = new SinglyLinkedList<T>(list.Value);
            var item = list.Next;
            while (item != current)
            {
                newList = newList.Cons(item.Value);
                item = item.Next;
            }
            // skip current, as that is the one to be deleted
            newList.Append(current.Next);
            return newList;
        }
    }
}