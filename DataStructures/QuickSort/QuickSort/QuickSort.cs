using System;
using System.Collections.Generic;

namespace QuickSort
{
    public static class QuickSort
    {
        public static void Sort<T>(T[] input) where T : IComparable<T>
        {
            Sort(input, 0, input.Length - 1);
        }

        private static void Sort<T>(T[] input, int startIndex, int endIndex) where T : IComparable<T>
        {
            var length = endIndex - startIndex + 1;

            // base cases for recursive scheme
            if (length < 2)
                return;
            if (length == 2)
            {
                if (input[startIndex].CompareTo(input[endIndex]) == 1)
                    Swap(input, startIndex, endIndex);
                return;
            }

            // partition the array
            (T[] partitioned, int lowerPivotIndex, int upperPivotIndex) = Partition(input, startIndex, endIndex);
            if (lowerPivotIndex > startIndex)
                Sort(partitioned, startIndex, lowerPivotIndex - 1);
            if (upperPivotIndex + 1 < endIndex)
                Sort(partitioned, upperPivotIndex + 1, endIndex);

            for (var i = startIndex; i <= endIndex; i++)
                input[i] = partitioned[i - startIndex];
        }

        private static void Swap<T>(IList<T> input, int startIndex, int endIndex) where T : IComparable<T>
        {
            var tmp = input[startIndex];
            input[startIndex] = input[endIndex];
            input[endIndex] = tmp;
        }

        private static (T[], int, int) Partition<T>(IReadOnlyList<T> input, int startIndex, int endIndex)
            where T : IComparable<T>
        {
            // Currently, due to the nature of this partition scheme newing up
            // a separate array, this qsort implementation is O (N ln N) in
            // memory complexity.
            // With an in-memory partition scheme, we'd have O (N) instead.

            // select a pivot element
            var pivot = input[startIndex];
            var partitioned = new T[endIndex - startIndex + 1];

            var j = 0;
            var lowerPivotIndex = 0;
            var upperPivotIndex = 0;

            // all elements smaller than the pivot
            for (var i = startIndex; i <= endIndex; i++)
            {
                var elem = input[i];
                if (elem.CompareTo(pivot) == -1)
                    partitioned[j++] = elem;
            }
            lowerPivotIndex = j;

            // all elements equal to the pivot (can be more than one)
            for (var i = startIndex; i <= endIndex; i++)
            {
                var elem = input[i];
                if (elem.CompareTo(pivot) == 0)
                    partitioned[j++] = elem;
            }
            upperPivotIndex = j - 1;

            // all elements bigger than the pivot
            for (var i = startIndex; i <= endIndex; i++)
            {
                var elem = input[i];
                if (elem.CompareTo(pivot) == 1)
                    partitioned[j++] = elem;
            }

            return (partitioned, lowerPivotIndex, upperPivotIndex);
        }
    }
}