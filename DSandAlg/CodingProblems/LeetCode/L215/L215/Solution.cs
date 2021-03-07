#region

using System;
using NUnit.Framework;

#endregion

// Problem: 215. Kth Largest Element in an Array
// URL: https://leetcode.com/problems/kth-largest-element-in-an-array/

namespace LeetCode
{
    public class Solution
    {
        public int FindKthLargest(int[] nums, int k)
        {
            /* Approach 1: sort at O(N log N)
             * Approach 2: create Min Heap at O(N), then pop off k elements at O(N + k log N) 
             * Approach 3: QuickSelect at O(N) avg., O(N^2) worst-case
             */

            // SS: 1 ≤ k ≤ array's length

            var min = 0;
            var max = nums.Length - 1;
            var targetIndex = nums.Length - k;

            while (true)
            {
                // SS: pick 1st as pivot
                var pivot = nums[min];

                // SS: partition
                // var pi = min;
                // for (var j = min + 1; j <= max; j++)
                // {
                //     if (nums[j] < pivot)
                //     {
                //         Swap(nums, pi, pi + 1);
                //         if (j > pi + 1)
                //         {
                //             Swap(nums, pi, j);
                //         }
                //
                //         pi++;
                //     }
                // }

                // SS: partition
                // Move partition element to last position,
                // then swap last with 1st that is >= than
                // partition element
                Swap(nums, min, max);
                var pi = min;
                for (var j = min; j < max; j++)
                {
                    if (nums[j] < pivot)
                    {
                        Swap(nums, pi, j);
                        pi++;
                    }
                }
                Swap(nums, max, pi);

                if (pi == targetIndex)
                {
                    return nums[pi];
                }

                if (pi < targetIndex)
                {
                    min = pi + 1;
                }
                else
                {
                    max = pi - 1;
                }
            }
        }

        private static void Swap(int[] nums, int i, int j)
        {
            var tmp = nums[i];
            nums[i] = nums[j];
            nums[j] = tmp;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {3, 2, 1, 5, 6, 4};

                // Act
                var v = new Solution().FindKthLargest(nums, 2);

                // Assert
                Assert.AreEqual(5, v);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {3, 2, 3, 1, 2, 4, 5, 5, 6};

                // Act
                var v = new Solution().FindKthLargest(nums, 4);

                // Assert
                Assert.AreEqual(4, v);
            }

            [TestCase(1)]
            [TestCase(3)]
            public void Test3(int k)
            {
                // Arrange
                int[] nums = {3145, 9, 3455, 34, 345, 567, 9, 345, 67, 89, 2345, 34, 567, 567, 78, 9};

                // Act
                var v = new Solution().FindKthLargest(nums, k);

                // Assert
                Array.Sort(nums);
                Assert.AreEqual(nums[^k], v);
            }
        }
    }
}