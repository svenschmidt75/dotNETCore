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
            // SS: runtime complexity: O(n)
            // space complexity: O(1)
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
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {1, 2, 3, 1};

                // Act
                var result = new Solution().ContainsDuplicate(nums);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {1, 2, 3, 4};

                // Act
                var result = new Solution().ContainsDuplicate(nums);

                // Assert
                Assert.False(result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {1, 1, 1, 3, 3, 4, 3, 2, 4, 2};

                // Act
                var result = new Solution().ContainsDuplicate(nums);

                // Assert
                Assert.True(result);
            }
        }
    }
}