#region

using System;
using NUnit.Framework;

#endregion

// Problem: 162. Find Peak Element
// URL: https://leetcode.com/problems/find-peak-element/

namespace LeetCode
{
    public class Solution
    {
        public int FindPeakElement(int[] nums)
        {
            // return FindPeakElementLinear(nums);
            return FindPeakElementLog(nums);
        }

        private int FindPeakElementLog(int[] nums)
        {
            // SS: Binary Search, O(log N)

            if (nums.Length == 1)
            {
                return 0;
            }

            var min = 0;
            var max = nums.Length - 1;

            while (true)
            {
                // SS: base case
                if (min == max)
                {
                    return min;
                }

                var mid = (min + max) / 2;

                if (mid == nums.Length - 1)
                {
                    return mid;
                }

                if (mid == 0)
                {
                    return nums[0] < nums[1] ? 1 : 0;
                }

                // SS: we follow the bigger number, as the one past the last
                // is neg. infinity, so smaller
                if (nums[mid - 1] < nums[mid] && nums[mid] > nums[mid + 1])
                {
                    return mid;
                }

                if (nums[mid] < nums[mid + 1])
                {
                    min = mid + 1;
                }
                else
                {
                    max = mid - 1;
                }
            }
        }

        private static int FindPeakElementLinear(int[] nums)
        {
            for (var i = 0; i < nums.Length; i++)
            {
                var prevValue = i > 0 ? nums[i - 1] : long.MinValue;
                long value = nums[i];
                var nextValue = i <= nums.Length - 2 ? nums[i + 1] : long.MinValue;

                if (prevValue < value && value > nextValue)
                {
                    return i;
                }
            }

            throw new InvalidOperationException();
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {1, 2, 3, 1};

                // Act
                var peakIndex = new Solution().FindPeakElement(nums);

                // Assert
                Assert.AreEqual(2, peakIndex);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {1, 2, 1, 3, 5, 6, 4};

                // Act
                var peakIndex = new Solution().FindPeakElement(nums);

                // Assert
                Assert.AreEqual(5, peakIndex);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {1};

                // Act
                var peakIndex = new Solution().FindPeakElement(nums);

                // Assert
                Assert.AreEqual(0, peakIndex);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {1, 2, 3};

                // Act
                var peakIndex = new Solution().FindPeakElement(nums);

                // Assert
                Assert.AreEqual(2, peakIndex);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {3, 2, 1};

                // Act
                var peakIndex = new Solution().FindPeakElement(nums);

                // Assert
                Assert.AreEqual(0, peakIndex);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] nums = {-2147483647, -2147483648};

                // Act
                var peakIndex = new Solution().FindPeakElement(nums);

                // Assert
                Assert.AreEqual(0, peakIndex);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[] nums = {4, 6, 5, 3, 1, 2, 1};

                // Act
                var peakIndex = new Solution().FindPeakElement(nums);

                // Assert
                Assert.AreEqual(1, peakIndex);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                int[] nums = {9, 10, -2147483647, -2147483648};

                // Act
                var peakIndex = new Solution().FindPeakElement(nums);

                // Assert
                Assert.AreEqual(1, peakIndex);
            }

            [Test]
            public void Test9()
            {
                // Arrange
                int[] nums = {1, 2};

                // Act
                var peakIndex = new Solution().FindPeakElement(nums);

                // Assert
                Assert.AreEqual(1, peakIndex);
            }

            [Test]
            public void Test10()
            {
                // Arrange
                int[] nums = {3, 4, 5};

                // Act
                var peakIndex = new Solution().FindPeakElement(nums);

                // Assert
                Assert.AreEqual(2, peakIndex);
            }
        }
    }
}