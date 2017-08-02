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
                {
                    var tmp = input[startIndex];
                    input[startIndex] = input[endIndex];
                    input[endIndex] = tmp;
                }
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

        private static (T[], int, int) Partition<T>(IReadOnlyList<T> input, int startIndex, int endIndex) where T : IComparable<T>
        {
            // select a pivot element
            var pivot = input[startIndex];
            var partitioned = new T[endIndex - startIndex + 1];

            var j = 0;
            var lowerPivotIndex = 0;
            var upperPivotIndex = 0;
            for (var i = startIndex; i <= endIndex; i++)
            {
                var elem = input[i];
                if (elem.CompareTo(pivot) == -1)
                    partitioned[j++] = elem;
            }
            lowerPivotIndex = j;
            for (var i = startIndex; i <= endIndex; i++)
            {
                var elem = input[i];
                if (elem.CompareTo(pivot) == 0)
                    partitioned[j++] = elem;
            }
            upperPivotIndex = j - 1;

            // TODO SS: do the ones larger and start from the end backwards...
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