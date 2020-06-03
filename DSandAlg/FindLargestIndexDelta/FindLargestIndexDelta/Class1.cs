#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Given an array arr[], find the maximum j – i such that arr[j] > arr[i]
// https://www.geeksforgeeks.org/given-an-array-arr-find-the-maximum-j-i-such-that-arrj-arri/

namespace FindLargestIndexDelta
{
    public static class Class1
    {
        public static int Solve(int[] array)
        {
            // SS: enable fast lookup of index of a value
            var valueToIndex = new Dictionary<int, List<int>>();
            for (var i = 0; i < array.Length; i++)
            {
                var value = array[i];
                if (valueToIndex.TryGetValue(value, out var vs) == false)
                {
                    vs = new List<int> {i};
                    valueToIndex.Add(value, vs);
                }
                else
                {
                    vs.Add(value);
                }
            }

            // SS: O(n log n)
            Array.Sort(array);

            var maxDiff = 0;
            var temp = array.Length;

            for (var i = 0; i < array.Length; i++)
            {
                var value = array[i];
                var idx = valueToIndex[value];

                // SS: index of smaller item > index of larger item?
                // If so, reset, because we need the larger item's index
                // to be larger than the smaller item's index...
                if (temp > idx[0])
                {
                    // SS: index of 1st occurence in unsorted array
                    temp = idx[0];
                }

                // SS: difference of larger item's index and smaller item's index
                maxDiff = Math.Max(maxDiff, idx[^1] - temp);
            }

            return maxDiff;
        }

        public static int Solve2(int[] array)
        {
            // SS: O(n log n)

            // SS: construct array with values such that at each index, we find
            // the smallest value
            var minArray = new int[array.Length];
            minArray[0] = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                minArray[i] = Math.Min(array[i], minArray[i - 1]);
            }

            // SS: construct array with values such that at each index, we find
            // the largest value
            var maxArray = new int[array.Length];
            minArray[^1] = array[^1];
            for (int i = array.Length - 2; i >= 0; i--)
            {
                maxArray[i] = Math.Max(array[i], maxArray[i + 1]);
            }

            // SS: example: minArray[5]: min element up to index 5

            int lIdx = 0;
            int rIdx = array.Length - 1;
            int maxDiff = 0;
            while (lIdx < array.Length - 1 && rIdx < array.Length - 1)
            {
                if (minArray[lIdx] < maxArray[rIdx])
                {
                    maxDiff = Math.Max(maxDiff, rIdx - lIdx);
                    rIdx -= 1;
                }
                else
                {
                    lIdx += 1;
                }
            }

            return maxDiff;
        }
    }
    

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test11()
        {
            // Arrange
            var array = new[] {34, 8, 10, 3, 2, 80, 30, 33, 1};

            // Act
            var maxDiff = Class1.Solve(array);

            // Assert
            Assert.AreEqual(6, maxDiff);
        }

        [Test]
        public void Test12()
        {
            // Arrange
            var array = new[] {9, 2, 3, 4, 5, 6, 7, 8, 18, 0};

            // Act
            var maxDiff = Class1.Solve(array);

            // Assert
            Assert.AreEqual(8, maxDiff);
        }
        
        [Test]
        public void Test21()
        {
            // Arrange
            var array = new[] {34, 8, 10, 3, 2, 80, 30, 33, 1};

            // Act
            var maxDiff = Class1.Solve2(array);

            // Assert
            Assert.AreEqual(6, maxDiff);
        }

        [Test]
        public void Test22()
        {
            // Arrange
            var array = new[] {9, 2, 3, 4, 5, 6, 7, 8, 18, 0};

            // Act
            var maxDiff = Class1.Solve2(array);

            // Assert
            Assert.AreEqual(8, maxDiff);
        }

    }
}