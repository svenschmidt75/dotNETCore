#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 41. First Missing Positive
// URL: https://leetcode.com/problems/first-missing-positive/

namespace LeetCode41
{
    public class Solution
    {
        public int FirstMissingPositive(int[] nums)
        {
            return FirstMissingPositive3(nums);
        }

        public int FirstMissingPositive2(int[] nums)
        {
            // SS: runtime complexity: O(N log N)
            // space complexity: O(N)

            if (nums.Length == 0)
            {
                return 1;
            }

            Array.Sort(nums);

            // SS: skip all negative and 0 numbers
            var i = 0;
            while (i < nums.Length && nums[i] <= 0)
            {
                i++;
            }

            if (i == nums.Length || nums[i] > 1)
            {
                return 1;
            }

            // nums[i] == 1
            i++;

            // SS: skip if either same numbers or consecutive
            while (i < nums.Length && nums[i - 1] + 1 >= nums[i])
            {
                i++;
            }

            return i == nums.Length ? nums[^1] + 1 : nums[i - 1] + 1;
        }

        public int FirstMissingPositive3(int[] nums)
        {
            // SS: O(N) runtime complexity, O(N) space complexity
            
            if (nums.Length == 0)
            {
                return 1;
            }

            var hash = new Dictionary<int, int> {{1, 1}};

            var i = 0;
            while (i < nums.Length)
            {
                if (nums[i] <= 0)
                {
                    if (hash.ContainsKey(1) == false)
                    {
                        hash[1] = 1;
                    }
                }
                else
                {
                    if (nums[i] > 1 && nums[i] > int.MinValue)
                    {
                        if (hash.ContainsKey(nums[i] - 1) == false)
                        {
                            hash[nums[i] - 1] = 1;
                        }
                    }

                    if (nums[i] < int.MaxValue && hash.ContainsKey(nums[i] + 1) == false)
                    {
                        hash[nums[i] + 1] = 1;
                    }

                    hash[nums[i]] = -1;
                }

                i++;
            }

            var min = int.MaxValue;
            foreach (var item in hash)
            {
                if (item.Value != -1)
                {
                    min = Math.Min(min, item.Key);
                }
            }

            return min;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {1, 2, 0};

                // Act
                var result = new Solution().FirstMissingPositive(nums);

                // Assert
                Assert.AreEqual(3, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {0, 2, 2, 1, 1};

                // Act
                var result = new Solution().FirstMissingPositive(nums);

                // Assert
                Assert.AreEqual(3, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var nums = new int[0];

                // Act
                var result = new Solution().FirstMissingPositive(nums);

                // Assert
                Assert.AreEqual(1, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {3, 4, -1, 1};

                // Act
                var result = new Solution().FirstMissingPositive(nums);

                // Assert
                Assert.AreEqual(2, result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {7, 8, 9, 11, 12};

                // Act
                var result = new Solution().FirstMissingPositive(nums);

                // Assert
                Assert.AreEqual(1, result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] nums = {int.MaxValue};

                // Act
                var result = new Solution().FirstMissingPositive(nums);

                // Assert
                Assert.AreEqual(1, result);
            }
        }
    }
}