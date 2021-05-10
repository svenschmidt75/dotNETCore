#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 442. Find All Duplicates in an Array
// URL: https://leetcode.com/problems/find-all-duplicates-in-an-array/

namespace LeetCode
{
    public class Solution
    {
        public IList<int> FindDuplicates(int[] nums)
        {
            // return FindDuplicates1(nums);
            // return FindDuplicates2(nums);
            return FindDuplicates3(nums);
        }

        private IList<int> FindDuplicates3(int[] nums)
        {
            // SS: Since 1 <= nums[i] <= n, we can use the idea from cycle sort
            // to put each element in its place.
            // runtime complexity: O(n)
            // space complexity: O(1)
            var result = new List<int>();

            var i = 0;
            while (i < nums.Length)
            {
                var v = nums[i];

                if (i == v - 1)
                {
                    // SS: number is at its correct position
                    i++;
                    continue;
                }

                if (v == -1)
                {
                    // SS: number is marked as duplicate
                    i++;
                    continue;
                }

                if (nums[v - 1] == v)
                {
                    // SS: is a duplicate
                    result.Add(v);
                    nums[i] = -1;
                    i++;
                    continue;
                }

                // SS: swap
                // after a swap, the destination, i.e. nums[v - 1], is at its correct
                // spot
                nums[i] = nums[v - 1];
                nums[v - 1] = v;
            }

            return result;
        }

        private IList<int> FindDuplicates2(int[] nums)
        {
            // SS: Since 1 <= nums[i] <= n, we can use the idea from cycle sort
            // to put each element in its place.
            // We use a set, because we may end up adding a duplicate item more
            // than once.
            // runtime complexity: O(n)
            // space complexity: O(1)

            var result = new HashSet<int>();

            var i = 0;
            while (i < nums.Length)
            {
                var v = nums[i];

                if (i == v - 1)
                {
                    // SS: number is at its correct position
                    i++;
                    continue;
                }

                if (nums[v - 1] == v)
                {
                    // SS: is a duplicate
                    result.Add(v);
                    i++;
                    continue;
                }

                // SS: swap
                nums[i] = nums[v - 1];
                nums[v - 1] = v;
            }

            return result.ToList();
        }

        private IList<int> FindDuplicates1(int[] nums)
        {
            // SS: runtime complexity: O(n log n)
            // space complexity: O(1)
            Array.Sort(nums);

            var result = new List<int>();
            for (var i = 1; i < nums.Length; i++)
            {
                if (nums[i - 1] == nums[i])
                {
                    result.Add(nums[i]);
                }
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {4, 3, 2, 7, 8, 2, 3, 1}, new[] {2, 3})]
            [TestCase(new[] {1, 1, 2}, new[] {1})]
            [TestCase(new[] {1}, new int[0])]
            public void Test1(int[] nums, int[] expected)
            {
                // Arrange

                // Act
                var result = new Solution().FindDuplicates(nums);

                // Assert
                CollectionAssert.AreEquivalent(expected, result);
            }
        }
    }
}