#region

using NUnit.Framework;

#endregion

// Problem: 852. Peak Index in a Mountain Array
// URL: https://leetcode.com/problems/peak-index-in-a-mountain-array/

namespace LeetCode
{
    public class Solution
    {
        public int PeakIndexInMountainArray(int[] arr)
        {
//            return PeakIndexInMountainArray1(arr);
            return PeakIndexInMountainArray2(arr);
        }

        private int PeakIndexInMountainArray2(int[] arr)
        {
            // SS: runtime complexity: O(log n)
            var min = 0;
            var max = arr.Length;

            while (true)
            {
                var mid = (min + max) / 2;

                if (arr[mid - 1] < arr[mid] && arr[mid] > arr[mid + 1])
                {
                    return mid;
                }

                if (arr[mid - 1] < arr[mid])
                {
                    min = mid + 1;
                }
                else
                {
                    max = mid;
                }
            }
        }

        private int PeakIndexInMountainArray1(int[] arr)
        {
            // SS: runtime complexity: O(n)
            var i = 1;
            while (arr[i - 1] < arr[i])
            {
                i++;
            }

            return i - 1;
        }


        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {0, 1, 0}, 1)]
            [TestCase(new[] {0, 2, 1, 0}, 1)]
            [TestCase(new[] {0, 10, 5, 2}, 1)]
            [TestCase(new[] {3, 4, 5, 1}, 2)]
            [TestCase(new[] {24, 69, 100, 99, 79, 78, 67, 36, 26, 19}, 2)]
            public void Test(int[] arr, int expected)
            {
                // Arrange

                // Act
                var idx = new Solution().PeakIndexInMountainArray(arr);

                // Assert
                Assert.AreEqual(expected, idx);
            }
        }
    }
}