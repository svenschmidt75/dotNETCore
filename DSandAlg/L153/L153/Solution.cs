#region

using System;
using NUnit.Framework;

#endregion

// Problem: 153. Find Minimum in Rotated Sorted Array
// URL: https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/

namespace LeetCode
{
    public class Solution
    {
        public int FindMin(int[] nums)
        {
            // SS: runtime complexity: O(log N)
            // space complexity: O(1)
            
            if (nums.Length == 1)
            {
                return nums[0];
            }

            var minElement = int.MaxValue;

            var min = 0;
            var max = nums.Length - 1;

            while (true)
            {
                if (min == max)
                {
                    minElement = Math.Min(minElement, nums[min]);
                    break;
                }

                minElement = Math.Min(minElement, nums[min]);
                minElement = Math.Min(minElement, nums[max]);

                var mid = (min + max) / 2;

                if (nums[min] < nums[mid])
                {
                    min = mid + 1;
                }
                else
                {
                    max = mid;
                }
            }

            return minElement;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {3, 4, 5, 1, 2};

                // Act
                var minElement = new Solution().FindMin(nums);

                // Assert
                Assert.AreEqual(1, minElement);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {5, 1, 2, 3, 4};

                // Act
                var minElement = new Solution().FindMin(nums);

                // Assert
                Assert.AreEqual(1, minElement);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {4, 5, 6, 7, 0, 1, 2};

                // Act
                var minElement = new Solution().FindMin(nums);

                // Assert
                Assert.AreEqual(0, minElement);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {11, 13, 15, 17};

                // Act
                var minElement = new Solution().FindMin(nums);

                // Assert
                Assert.AreEqual(11, minElement);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {3, 1, 2};

                // Act
                var minElement = new Solution().FindMin(nums);

                // Assert
                Assert.AreEqual(1, minElement);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] nums = {3, 1};

                // Act
                var minElement = new Solution().FindMin(nums);

                // Assert
                Assert.AreEqual(1, minElement);
            }
            
        }
    }
}