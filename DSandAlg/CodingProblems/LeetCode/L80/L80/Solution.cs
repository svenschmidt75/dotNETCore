#region

using NUnit.Framework;

#endregion

// Problem: 80. Remove Duplicates from Sorted Array II
// URL: https://leetcode.com/problems/remove-duplicates-from-sorted-array-ii/

namespace LeetCode
{
    public class Solution
    {
        public int RemoveDuplicates(int[] nums)
        {
            // SS: we need to do two things:
            // copy elements and keep a window of 2 elements at most
            // runtime complexity: O(n)
            var i = 0;
            var j = 0;

            while (j < nums.Length)
            {
                var count = 0;
                var k = j;
                while (j < nums.Length && nums[j] == nums[k])
                {
                    if (count < 2)
                    {
                        nums[i] = nums[j];
                        count++;
                        i++;
                    }

                    j++;
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
                int[] nums = {1, 1, 1, 2, 2, 3};

                // Act
                var newLength = new Solution().RemoveDuplicates(nums);

                // Assert
                Assert.AreEqual(5, newLength);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {0, 0, 1, 1, 1, 1, 2, 3, 3};

                // Act
                var newLength = new Solution().RemoveDuplicates(nums);

                // Assert
                Assert.AreEqual(7, newLength);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {0};

                // Act
                var newLength = new Solution().RemoveDuplicates(nums);

                // Assert
                Assert.AreEqual(1, newLength);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {0, 0, 1, 1, 2, 3, 3};

                // Act
                var newLength = new Solution().RemoveDuplicates(nums);

                // Assert
                Assert.AreEqual(nums.Length, newLength);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {0, 0, 0};

                // Act
                var newLength = new Solution().RemoveDuplicates(nums);

                // Assert
                Assert.AreEqual(2, newLength);
            }
        }
    }
}