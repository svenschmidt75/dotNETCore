using NUnit.Framework;

// Problem: 704. Binary Search
// URL: https://leetcode.com/problems/binary-search/

namespace LeetCode
{
    public class Solution
    {
        public int Search(int[] nums, int target)
        {
            // SS: runtime complexity: O(log n)
            // space complexity: O(1)

            int min = 0;
            int max = nums.Length;

            while (min < max)
            {
                int mid = (min + max) / 2;
                var num = nums[mid];

                if (num == target)
                {
                    return mid;
                }

                if (num < target)
                {
                    min = mid + 1;
                }
                else
                {
                    max = mid;
                }
            }

            return -1;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[]{-1,0,3,5,9,12}, 9, 4)]
            [TestCase(new[]{-1,0,3,5,9,12}, 2, -1)]
            public void Test(int[] nums, int target, int expectedIndex)
            {
                // Arrange

                // Act
                int idx = new Solution().Search(nums, target);

                // Assert
                Assert.AreEqual(expectedIndex, idx);
            }
        }
    }
}