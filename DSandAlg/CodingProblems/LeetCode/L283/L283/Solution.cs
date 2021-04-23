#region

using NUnit.Framework;

#endregion

// Problem: 283. Move Zeroes
// URL: https://leetcode.com/problems/move-zeroes/

namespace LeetCode
{
    public class Solution
    {
        public void MoveZeroes(int[] nums)
        {
//            MoveZeroes1(nums);
            MoveZeroes2(nums);
        }

        private void MoveZeroes2(int[] nums)
        {
            // SS: runtime performance: O(n)
            // space complexity: O(1)

            var n = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                if (nums[i] != 0)
                {
                    nums[n] = nums[i];
                    n++;
                }
            }

            for (; n < nums.Length; n++)
            {
                nums[n] = 0;
            }
        }

        private static void MoveZeroes1(int[] nums)
        {
            // SS: runtime performance: O(n)
            // space complexity: O(1)

            var i = 0;
            var j = 0;

            while (j < nums.Length)
            {
                while (i < nums.Length && nums[i] != 0)
                {
                    i++;
                }

                // SS: i points to a 0
                if (j < i)
                {
                    j = i;
                }

                // SS: find first non-0
                while (j < nums.Length && nums[j] == 0)
                {
                    j++;
                }

                if (j == nums.Length)
                {
                    break;
                }

                nums[i] = nums[j];
                nums[j] = 0;

                i++;
                j++;
            }
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {0, 1, 0, 3, 12}, new[] {1, 3, 12, 0, 0})]
            [TestCase(new[] {0}, new[] {0})]
            [TestCase(new[] {1, 2, 3, 0, 4, 0, 5, 0, 0, 7, 0, 0}, new[] {1, 2, 3, 4, 5, 7, 0, 0, 0, 0, 0, 0})]
            public void Test(int[] nums, int[] expected)
            {
                // Arrange

                // Act
                new Solution().MoveZeroes(nums);

                // Assert
                CollectionAssert.AreEqual(expected, nums);
            }
        }
    }
}