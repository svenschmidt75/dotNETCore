#region

using NUnit.Framework;

#endregion

// Problem: 26. Remove Duplicates from Sorted Array
// URL: https://leetcode.com/problems/remove-duplicates-from-sorted-array/

namespace LeetCode26
{
    public class Solution
    {
        public int RemoveDuplicates(int[] nums)
        {
            // SS: sliding window approach
            // runtime complexity: O(n)

            if (nums.Length < 2)
            {
                return nums.Length;
            }

            var i = 0;
            var j = 1;

            while (j < nums.Length)
            {
                var a = nums[i];

                // skip duplicates
                while (j < nums.Length && a == nums[j])
                {
                    j++;
                }

                if (j == nums.Length)
                {
                    break;
                }

                nums[i + 1] = nums[j];
                j++;
                i++;
            }

            return i + 1;
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {1, 1, 2};

                // Act
                var newLength = new Solution().RemoveDuplicates(nums);

                // Assert
                Assert.AreEqual(2, newLength);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {0, 0, 1, 1, 1, 2, 2, 3, 3, 4};

                // Act
                var newLength = new Solution().RemoveDuplicates(nums);

                // Assert
                Assert.AreEqual(5, newLength);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {1, 1};

                // Act
                var newLength = new Solution().RemoveDuplicates(nums);

                // Assert
                Assert.AreEqual(1, newLength);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {1};

                // Act
                var newLength = new Solution().RemoveDuplicates(nums);

                // Assert
                Assert.AreEqual(1, newLength);
            }
        }
    }
}