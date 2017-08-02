using System;
using System.Linq;

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
            int length = endIndex - startIndex + 1;
            if (length < 2)
            {
                return;
            }
            if (length == 2)
            {
                if (input[startIndex].CompareTo(input[endIndex]) == 1)
                {
                    T tmp = input[startIndex];
                    input[startIndex] = input[endIndex];
                    input[endIndex] = tmp;
                }
                return;
            }

            // select a pivot element
            T pivot = input[startIndex];

            // partition the array
            var partitioned = new T[endIndex - startIndex + 1];

            int j = 0;
            int lowerPivotIndex = 0;
            int upperPivotIndex = 0;
            for (int i = startIndex; i <= endIndex; i++)
            {
                var elem = input[i];
                if (elem.CompareTo(pivot) == -1)
                {
                    partitioned[j++] = elem;
                }
            }
            lowerPivotIndex = j;
            for (int i = startIndex; i <= endIndex; i++)
            {
                var elem = input[i];
                if (elem.CompareTo(pivot) == 0)
                {
                    partitioned[j++] = elem;
                }
            }
            upperPivotIndex = j - 1;

            // TODO SS: do the ones larger and start from the end backwards...
            for (int i = startIndex; i <= endIndex; i++)
            {
                var elem = input[i];
                if (elem.CompareTo(pivot) == 1)
                {
                    partitioned[j++] = elem;
                }
            }
            // The pivot element is the last one in the 1st half,
            // everything before it is smaller or equal.
            if (lowerPivotIndex > 0)
            {
                Sort(partitioned, startIndex, lowerPivotIndex - 1);
            }
            if (upperPivotIndex + 1 < endIndex)
            {
                Sort(partitioned, upperPivotIndex + 1, endIndex);
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                input[i] = partitioned[i - startIndex];
            }
        }

    }
}