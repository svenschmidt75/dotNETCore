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
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var array = new[] {34, 8, 10, 3, 2, 80, 30, 33, 1};

            // Act
            var maxDiff = Class1.Solve(array);

            // Assert
            Assert.AreEqual(6, maxDiff);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var array = new[] {9, 2, 3, 4, 5, 6, 7, 8, 18, 0};

            // Act
            var maxDiff = Class1.Solve(array);

            // Assert
            Assert.AreEqual(8, maxDiff);
        }
    }
}