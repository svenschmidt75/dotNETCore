#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 81. Search in Rotated Sorted Array II
// URL: https://leetcode.com/problems/search-in-rotated-sorted-array-ii/

namespace LeetCode
{
    public class Solution
    {
        public bool Search(int[] nums, int target)
        {
            return Search1(nums, target);
        }

        private bool Search1(int[] nums, int target)
        {
            bool Solve(int min, int max)
            {
                // SS: base case
                if (min == max)
                {
                    return false;
                }

                int mid = (min + max) / 2;

                int v1 = nums[min];
                int v2 = nums[mid];

                if (v2 == target)
                {
                    return true;
                }

                if (v1 > v2)
                {
                    bool result = false;

                    // SS: pivot is in left half
                    if (v1 >= target && v2 < target)
                    {
                        // SS: only right half
                        result = Solve(mid + 1, max);
                    }

                    return result || Solve(min, mid);
                }

                return Solve(min, mid) || Solve(mid + 1, max);
            }

            return Solve(0, nums.Length);
        }

        [TestFixture]
        public class Tests
        {
            #region Public Methods

            [TestCase(new[] { 2, 5, 6, 0, 0, 1, 2 }, 0, true)]
            [TestCase(new[] { 2, 5, 6, 0, 0, 1, 2 }, 4, false)]
            [TestCase(new[] { 2, 5, 6, 0, 0, 1, 2 }, 6, true)]
            [TestCase(new[] { 2, 5, 6, 0, 0, 1, 2 }, 1, true)]
            [TestCase(new[] { 2, 5, 6, 0, 0, 1, 2 }, 1, true)]
            [TestCase(new[] { 2, 5, 6, 0, 0, 1, 2 }, 3, false)]
            [TestCase(new[] { 1 }, 3, false)]
            [TestCase(new[] { 1, 0, 1, 1, 1 }, 0, true)]
            [TestCase(new[] { 1, 1, 1, 1, 3, 1 }, 3, true)]
            public void Test(int[] nums, int target, bool expected)
            {
                // Arrange

                // Act
                bool result = new Solution().Search(nums, target);

                // Assert
                Assert.AreEqual(expected, result);
            }


            #endregion
        }
    }
}
