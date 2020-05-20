using System;

namespace BinarySearch
{
    public static class BinarySearch
    {
        public static (bool, int) Search<T>(T[] array, T item) where T: IComparable
        {
            if (array.Length == 0)
                return (false, 0);
            return Search(array, 0, array.Length - 1, item, 0);
        }

        // Divide & Conquer
        private static (bool, int) Search<T>(T[] array, int min, int max, T item, int n) where T: IComparable
        {
            if (min > max)
                return (false, n);
            int mid = (min + max) / 2;
            if (array[mid].CompareTo(item) < 0)
            {
                return Search(array, mid + 1, max, item, n + 1);
            }
            if (array[mid].CompareTo(item) > 0)
            {
                return Search(array, min, mid - 1, item, n + 1);
            }
            // Sinplest case for arrays: only one element
            // base case for recursive scheme
            return (array[mid].CompareTo(item) == 0, n + 1);
        }

    }
}