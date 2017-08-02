using System;

namespace QuickSort
{
    public static class QuickSort
    {
        public static void Sort<T>(T[] input) where T: IComparable<T>
        {
            Sort(input, 0, input.Length - 1);
        }

        private static void Sort<T>(T[] input, int startIndex, int endIndex) where T: IComparable<T>
        {
            if (input.Length < 2)
            {
                return;
            }
            if (input.Length == 2)
            {
                if (input[0].CompareTo(input[1]) == 1)
                {
                    T tmp = input[1];
                    input[0] = input[1];
                    input[1] = tmp;
                    return;
                }
            }

            // select a pivot element
            T pivot = input[0];

            // partition the array
            var partitioned = new T[endIndex - startIndex + 1];

            int j = 0;
            int pivotIndex = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                var elem = input[i];
                if (elem.CompareTo(pivot) < 1)
                {
                    partitioned[j++] = elem;
                }
            }
            pivotIndex = j - 1;
            // TODO SS: do the ones larger and start from the end backwards...
            for (int i = startIndex; i <= endIndex; i++)
            {
                var elem = input[i];
                if (elem.CompareTo(pivot) == 1)
                {
                    partitioned[j++] = elem;
                }
            }
            Sort(input, startIndex, pivotIndex);
            Sort(input, pivotIndex + 1, endIndex);
        }

    }
}