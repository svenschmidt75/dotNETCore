#region

using System;
using NUnit.Framework;

#endregion

// Problem: 27. Remove Element 
// URL: https://leetcode.com/problems/remove-element/

namespace LeetCode27
{
    public class Solution
    {
        public int RemoveElement(int[] nums, int val)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(n)
            
            var n = nums.Length;

            if (n == 0)
            {
                return 0;
            }

            var i = 0;

            for (var j = 0; j < n; j++)
            {
                if (nums[j] != val)
                {
                    nums[i++] = nums[j];
                }
            }

            return i;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {3, 2, 2, 3};
                var val = 3;

                // Act
                var length = new Solution().RemoveElement(nums, val);

                // Assert
                Assert.AreEqual(2, length);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {0, 1, 2, 2, 3, 0, 4, 2};
                var val = 2;

                // Act
                var length = new Solution().RemoveElement(nums, val);

                // Assert
                Assert.AreEqual(5, length);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var nums = Array.Empty<int>();
                var val = 2;

                // Act
                var length = new Solution().RemoveElement(nums, val);

                // Assert
                Assert.AreEqual(0, length);
            }
        }
    }
}