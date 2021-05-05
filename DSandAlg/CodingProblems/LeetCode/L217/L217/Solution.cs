#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 217. Contains Duplicate
// URL: https://leetcode.com/problems/contains-duplicate/

namespace LeetCode
{
    public class Solution
    {
        public bool ContainsDuplicate(int[] nums)
        {
//            return ContainsDuplicate1(nums);
            return ContainsDuplicate2(nums);
        }

        private bool ContainsDuplicate2(int[] nums)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(1)

            const long bitOffset = (int) 1E9;

            var nBits = 2 * bitOffset + 1;
            var bitmap = new byte[(nBits + 7) / 8];

            for (var i = 0; i < nums.Length; i++)
            {
                var v = nums[i];

                var byteBucket = (int) ((bitOffset + v) / 8);
                var bit = (int) ((bitOffset + v) % 8);

                // SS: number already seen?
                if ((bitmap[byteBucket] & (1 << bit)) > 0)
                {
                    return true;
                }

                bitmap[byteBucket] |= (byte) (1 << bit);
            }

            return false;
        }

        private static bool ContainsDuplicate1(int[] nums)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(n)

            var dict = new HashSet<int>();

            for (var i = 0; i < nums.Length; i++)
            {
                if (dict.Contains(nums[i]))
                {
                    return true;
                }

                dict.Add(nums[i]);
            }

            return false;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {1, 2, 3, 1}, true)]
            [TestCase(new[] {1, 2, 3, 4}, false)]
            [TestCase(new[] {1, 1, 1, 3, 3, 4, 3, 2, 4, 2}, true)]
            [TestCase(new[] {(int) -1E9, 0, (int) 1E9}, false)]
            public void Test(int[] nums, bool expected)
            {
                // Arrange

                // Act
                var result = new Solution().ContainsDuplicate(nums);

                // Assert
                Assert.AreEqual(expected, result);
            }
        }
    }
}