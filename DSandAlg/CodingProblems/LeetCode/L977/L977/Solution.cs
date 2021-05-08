#region

using NUnit.Framework;

#endregion

// Problem: 977. Squares of a Sorted Array
// URL: https://leetcode.com/problems/squares-of-a-sorted-array/

namespace LeetCode
{
    public class Solution
    {
        public int[] SortedSquares(int[] nums)
        {
            // SS: 2-pointer approach
            // runtime complexity: O(n)

            var result = new int[nums.Length];
            var k = 0;

            // SS: find element that divides the set
            var i = 0;
            while (i < nums.Length && nums[i] < 0)
            {
                i++;
            }

            var j = i - 1;
            while (j >= 0 && i < nums.Length)
            {
                var v1 = nums[j] * nums[j];
                var v2 = nums[i] * nums[i];

                if (v1 < v2)
                {
                    result[k++] = v1;
                    j--;
                }
                else
                {
                    result[k++] = v2;
                    i++;
                }
            }

            while (j >= 0)
            {
                var v = nums[j] * nums[j];
                result[k++] = v;
                j--;
            }

            while (i < nums.Length)
            {
                var v = nums[i] * nums[i];
                result[k++] = v;
                i++;
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {-4, -1, 0, 3, 10}, new[] {0, 1, 9, 16, 100})]
            [TestCase(new[] {-7, -3, 2, 3, 11}, new[] {4, 9, 9, 49, 121})]
            [TestCase(new[] {-7, -3, -2, -1}, new[] {1, 4, 9, 49})]
            [TestCase(new[] {1, 2, 3, 7}, new[] {1, 4, 9, 49})]
            public void Test(int[] nums, int[] expected)
            {
                // Arrange

                // Act
                var result = new Solution().SortedSquares(nums);

                // Assert
                CollectionAssert.AreEqual(expected, result);
            }
        }
    }
}