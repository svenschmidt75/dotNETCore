#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 448. Find All Numbers Disappeared in an Array
// URL: https://leetcode.com/problems/find-all-numbers-disappeared-in-an-array/

namespace LeetCode
{
    public class Solution
    {
        public IList<int> FindDisappearedNumbers(int[] nums)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(1)
            
            var i = 0;
            while (i < nums.Length)
            {
                var v = nums[i];

                if (v == -1)
                {
                    // SS: have we seen this number already
                    i++;
                    continue;
                }

                var tmp = nums[v - 1];
                if (tmp == -1)
                {
                    // SS: have we seen this number already
                    i++;
                    continue;
                }

                // SS: mark as seen
                nums[v - 1] = -1;

                if (v - 1 == i)
                {
                    // SS: This number is at its correct place already
                    i++;
                    continue;
                }

                nums[i] = tmp;
            }

            // SS: find missing numbers
            var result = new List<int>();
            for (var j = 0; j < nums.Length; j++)
            {
                if (nums[j] != -1)
                {
                    result.Add(j + 1);
                }
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {4, 3, 2, 7, 8, 2, 3, 1}, new[] {5, 6})]
            [TestCase(new[] {1, 1}, new[] {2})]
            [TestCase(new[] {1}, new int[0])]
            public void Test(int[] nums, int[] expected)
            {
                // Arrange

                // Act
                var result = new Solution().FindDisappearedNumbers(nums);

                // Assert
                CollectionAssert.AreEqual(expected, result);
            }
        }
    }
}