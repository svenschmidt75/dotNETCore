#region

using System;
using NUnit.Framework;

#endregion

// Problem: 75. Sort Colors
// URL: https://leetcode.com/problems/sort-colors/

namespace LeetCode
{
    public class Solution
    {
        public void SortColors(int[] nums)
        {
            // SS: The idea is to move 2's to the end and 0's to the beginning
            // using a 3-pointer sliding window.
            // runtime complexity: O(n)
            // A clever solution counts the occurrence of each colors and overwrites
            // the input array accordingly. But that would be a 2-pass solution, but
            // the problem statement asks for a 1-pass solution...

            var i = 0;
            var j = 0;
            var k = nums.Length - 1;

            while (j <= k)
            {
                if (nums[j] == 0)
                {
                    Swap(nums, i, j);
                    i++;
                    j = Math.Max(i, j);
                }
                else if (nums[j] == 2)
                {
                    Swap(nums, j, k);
                    k--;
                }
                else if (nums[k] == 0)
                {
                    Swap(nums, i, k);
                    i++;
                    j = Math.Max(i, j);
                }
                else
                {
                    j++;
                }
            }
        }

        private static void Swap(int[] nums, int i, int j)
        {
            var tmp = nums[j];
            nums[j] = nums[i];
            nums[i] = tmp;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {2, 0, 2, 1, 1, 0};

                // Act
                new Solution().SortColors(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {0, 0, 1, 1, 2, 2}, nums);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {2, 0, 1};

                // Act
                new Solution().SortColors(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {0, 1, 2}, nums);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {0};

                // Act
                new Solution().SortColors(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {0}, nums);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {1};

                // Act
                new Solution().SortColors(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {1}, nums);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {0, 0, 1, 2, 0, 1, 2};

                // Act
                new Solution().SortColors(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {0, 0, 0, 1, 1, 2, 2}, nums);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] nums = {2, 0, 2, 0, 2, 1, 1, 0, 2, 0, 2, 1, 1, 0, 2, 1, 1, 0};

                // Act
                new Solution().SortColors(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2}, nums);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[] nums = {0, 2, 2, 2, 0, 2, 1, 1};

                // Act
                new Solution().SortColors(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {0, 0, 1, 1, 2, 2, 2, 2}, nums);
            }
        }
    }
}