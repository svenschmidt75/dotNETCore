#region

using System;
using NUnit.Framework;

#endregion

// Problem: 154. Find Minimum in Rotated Sorted Array II
// URL: https://leetcode.com/problems/find-minimum-in-rotated-sorted-array-ii/

namespace LeetCode
{
    public class Solution
    {
        public int FindMin(int[] nums)
        {
//            return FindMinLinear(nums);
            return FindMinFast(nums);
        }

        private static int FindMinFast(int[] nums)
        {
            var minValue = int.MaxValue;

            void Solve(int min, int max)
            {
                var l = nums[min];
                minValue = Math.Min(minValue, l);

                var r = nums[max];
                minValue = Math.Min(minValue, r);

                // SS: base case
                if (min == max)
                {
                    return;
                }

                var mid = (min + max) / 2;
                var m = nums[mid];
                minValue = Math.Min(minValue, m);

                if (l > m)
                {
                    // SS: min on left side
                    Solve(min, mid);
                }
                else if (l < m)
                {
                    // SS: min on right side
                    Solve(mid + 1, max);
                }
                else
                {
                    // SS: undecidable, shrink window
                    Solve(min, max - 1);
                }
            }

            Solve(0, nums.Length - 1);
            return minValue;
        }


        private static int FindMinLinear(int[] nums)
        {
            var min = int.MaxValue;
            for (var i = 0; i < nums.Length; i++)
            {
                min = Math.Min(min, nums[i]);
            }

            return min;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {4, 5, 6, 7, 0, 1, 2};

                // Act
                var min = new Solution().FindMin(nums);

                // Assert
                Assert.AreEqual(0, min);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {4, 4, 5, 6, 7, 7, 0, 0, 1, 2, 2, 3, 3};

                // Act
                var min = new Solution().FindMin(nums);

                // Assert
                Assert.AreEqual(0, min);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {4, 5, 6, 7, 1, 1, 2, 3, 3, 3, 3, 3};

                // Act
                var min = new Solution().FindMin(nums);

                // Assert
                Assert.AreEqual(1, min);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {2, 2, 2, 0, 1};

                // Act
                var min = new Solution().FindMin(nums);

                // Assert
                Assert.AreEqual(0, min);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {10, 1, 10, 10, 10};

                // Act
                var min = new Solution().FindMin(nums);

                // Assert
                Assert.AreEqual(1, min);
            }
        }
    }
}