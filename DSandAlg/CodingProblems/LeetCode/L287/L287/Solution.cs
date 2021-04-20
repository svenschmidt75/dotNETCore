#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 287. Find the Duplicate Number
// URL: https://leetcode.com/problems/find-the-duplicate-number/

namespace LeetCode
{
    public class Solution
    {
        public int FindDuplicate(int[] nums)
        {
//            return FindDuplicate1(nums);
//            return FindDuplicate2(nums);
//            return FindDuplicate3(nums);
            return FindDuplicate4(nums);
        }

        private int FindDuplicate4(int[] nums)
        {
            // Larry: https://www.youtube.com/watch?v=r7iCVhlLZgQ&t=26s
            // Binary search to probe the element that is smaller,
            // and O(N) loop to check whether the duplicate element
            // is smaller or larger than the probe.

            var min = 0;
            var max = nums.Length - 1;

            bool IsDupe(int x)
            {
                /* Idea: All elements are in the range [1..n]. If we pick an index,
                 * which is between 0 and n - 1, and count the number of elements
                 * smaller or equal to the index, we have two cases:
                 * 1. count <= index => duplicate element is > index
                 * 2. count > index: => duplicate element is <= index
                 */
                var c = 0;
                foreach (var num in nums)
                {
                    if (num <= x)
                    {
                        c++;
                    }
                }

                return c > x;
            }

            while (min < max)
            {
                var mid = (min + max) / 2;

                if (IsDupe(mid))
                {
                    max = mid;
                }
                else
                {
                    min = mid + 1;
                }
            }

            return min;
        }

        //
        // private int FindDuplicate3(int[] nums)
        // {
        //     // SS: This only works if each [1, n] appears in nums[i]
        //     // (except for one). But we can have the duplicate appear
        //     // more than twice...
        //     
        //     // int xor = 0;
        //     //
        //     // for (int i = 0; i < nums.Length; i++)
        //     // {
        //     //     if (i + 1 <= nums.Length - 1)
        //     //     {
        //     //         xor ^= i + 1;
        //     //     }
        //     //
        //     //     xor ^= nums[i];
        //     // }
        //     //
        //     // return xor;
        // }

        private int FindDuplicate2(int[] nums)
        {
            // SS: runtime complexity: O(N log N)
            // space complexity: O(1)
            Array.Sort(nums);

            for (var i = 1; i < nums.Length; i++)
            {
                if (nums[i] == nums[i - 1])
                {
                    return nums[i];
                }
            }

            throw new InvalidOperationException();
        }

        private int FindDuplicate1(int[] nums)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(N)

            var set = new HashSet<int>();
            foreach (var num in nums)
            {
                if (set.Contains(num))
                {
                    return num;
                }

                set.Add(num);
            }

            throw new InvalidOperationException();
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {1, 3, 4, 2, 2};

                // Act
                var num = new Solution().FindDuplicate(nums);

                // Assert
                Assert.AreEqual(2, num);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {3, 1, 3, 4, 2};

                // Act
                var num = new Solution().FindDuplicate(nums);

                // Assert
                Assert.AreEqual(3, num);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {1, 1};

                // Act
                var num = new Solution().FindDuplicate(nums);

                // Assert
                Assert.AreEqual(1, num);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {1, 1, 2};

                // Act
                var num = new Solution().FindDuplicate(nums);

                // Assert
                Assert.AreEqual(1, num);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums = {2, 2, 2, 2, 2};

                // Act
                var num = new Solution().FindDuplicate(nums);

                // Assert
                Assert.AreEqual(2, num);
            }
        }
    }
}