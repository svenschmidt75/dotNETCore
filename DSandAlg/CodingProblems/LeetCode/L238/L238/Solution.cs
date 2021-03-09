#region

using NUnit.Framework;

#endregion

// Problem: 238. Product of Array Except Self
// URL: https://leetcode.com/problems/product-of-array-except-self/

namespace LeetCode
{
    public class Solution
    {
        public int[] ProductExceptSelf(int[] nums)
        {
            // return ProductExceptSelf1(nums);
            return ProductExceptSelf3(nums);
        }

        private int[] ProductExceptSelf3(int[] nums)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(1)

            var prefix = new int[nums.Length];
            prefix[0] = 1;
            for (var i = 1; i < nums.Length; i++)
            {
                prefix[i] = prefix[i - 1] * nums[i - 1];
            }

            for (var i = nums.Length - 2; i >= 0; i--)
            {
                prefix[i] *= nums[i + 1];
                nums[i] *= nums[i + 1];
            }

            return prefix;
        }

        private int[] ProductExceptSelf1(int[] nums)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(n)

            var output = new int[nums.Length];

            var prefix = new int[nums.Length];
            prefix[0] = 1;

            var suffix = new int[nums.Length];
            suffix[^1] = 1;

            for (var i = 1; i < nums.Length; i++)
            {
                prefix[i] = prefix[i - 1] * nums[i - 1];

                var j = nums.Length - 1 - i;
                suffix[j] = suffix[j + 1] * nums[j + 1];
            }

            for (var i = 0; i < nums.Length; i++)
            {
                output[i] = prefix[i] * suffix[i];
            }

            return output;
        }

        private int[] ProductExceptSelf2(int[] nums)
        {
            // SS: runtime complexity: O(n)

            var output = new int[nums.Length];

            var prefix = new int[nums.Length];
            prefix[0] = 1;
            for (var i = 1; i < nums.Length; i++)
            {
                prefix[i] = prefix[i - 1] * nums[i - 1];
            }

            var suffix = new int[nums.Length];
            suffix[^1] = 1;
            for (var i = nums.Length - 2; i >= 0; i--)
            {
                suffix[i] = suffix[i + 1] * nums[i + 1];
            }

            for (var i = 0; i < nums.Length; i++)
            {
                output[i] = prefix[i] * suffix[i];
            }

            return output;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {1, 2, 3, 4};

                // Act
                var output = new Solution().ProductExceptSelf(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {24, 12, 8, 6}, output);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {2, 5, 8, 2, 5, 7};

                // Act
                var output = new Solution().ProductExceptSelf(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {2800, 1120, 700, 2800, 1120, 800}, output);
            }

            [Test]
            public void Test9()
            {
                // Arrange
                int[] nums = {2, 5};

                // Act
                var output = new Solution().ProductExceptSelf(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {5, 2}, output);
            }
        }
    }
}