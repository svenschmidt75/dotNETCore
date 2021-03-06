#region

using NUnit.Framework;

#endregion

// Problem: 268. Missing Number
// URL: https://leetcode.com/problems/missing-number/

namespace LeetCode
{
    public class Solution
    {
        public int MissingNumber(int[] nums)
        {
//            return MissingNumber1(nums);
            return MissingNumber2(nums);
        }

        private int MissingNumber2(int[] nums)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(1)
            
            var xor = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                xor ^= i;
                xor ^= nums[i];
            }

            xor ^= nums.Length;
            return xor;
        }

        private static int MissingNumber1(int[] nums)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(1)

            var sum = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
            }

            // SS: sum of all elements from 0 to nums.Length
            var sum2 = nums.Length * (nums.Length + 1) / 2;

            return sum2 - sum;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {3, 0, 1}, 2)]
            [TestCase(new[] {0, 1}, 2)]
            [TestCase(new[] {9, 6, 4, 2, 3, 5, 7, 0, 1}, 8)]
            [TestCase(new[] {0}, 1)]
            public void Test(int[] nums, int expected)
            {
                // Arrange

                // Act
                var n = new Solution().MissingNumber(nums);

                // Assert
                Assert.AreEqual(expected, n);
            }
        }
    }
}