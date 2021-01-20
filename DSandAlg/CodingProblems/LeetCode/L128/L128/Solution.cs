#region

using System;
using NUnit.Framework;

#endregion

// Problem: 128. Longest Consecutive Sequence
// URL: https://leetcode.com/problems/longest-consecutive-sequence/

namespace LeetCode
{
    public class Solution
    {
        public int LongestConsecutive(int[] nums)
        {
            return LongestConsecutiveSlow(nums);
        }

        private int LongestConsecutiveSlow(int[] nums)
        {
            // SS: runtime complexity: O(N log N)

            if (nums.Length == 0)
            {
                return 0;
            }

            Array.Sort(nums);

            var maxLength = 0;
            var currentMaxLength = 1;
            for (var i = 1; i < nums.Length; i++)
            {
                // SS: if nums[i - 1] == nums[i], we do not change currentMaxLength
                if (nums[i - 1] + 1 == nums[i])
                {
                    currentMaxLength++;
                }
                else if (nums[i - 1] != nums[i])
                {
                    maxLength = Math.Max(maxLength, currentMaxLength);
                    currentMaxLength = 1;
                }
            }

            maxLength = Math.Max(maxLength, currentMaxLength);
            return maxLength;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {100, 4, 200, 1, 3, 2};

                // Act
                var longest = new Solution().LongestConsecutive(nums);

                // Assert
                Assert.AreEqual(4, longest);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {0, 3, 7, 2, 5, 8, 4, 6, 0, 1};

                // Act
                var longest = new Solution().LongestConsecutive(nums);

                // Assert
                Assert.AreEqual(9, longest);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {1, 2, 0, 1};

                // Act
                var longest = new Solution().LongestConsecutive(nums);

                // Assert
                Assert.AreEqual(3, longest);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {9, 1, 4, 7, 3, -1, 0, 5, 8, -1, 6};

                // Act
                var longest = new Solution().LongestConsecutive(nums);

                // Assert
                Assert.AreEqual(7, longest);
            }
        }
    }
}