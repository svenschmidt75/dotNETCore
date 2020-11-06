#region

using System;
using NUnit.Framework;

#endregion

// Problem: 88. Merge Sorted Array
// URL: https://leetcode.com/problems/merge-sorted-array/

namespace LeetCode88
{
    public class Solution
    {
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            var n1Idx = m - 1;
            var n2Idx = n - 1;

            var i = nums1.Length - 1;

            while (n1Idx >= 0 && n2Idx >= 0)
            {
                var n1 = nums1[n1Idx];
                var n2 = nums2[n2Idx];

                if (n1 <= n2)
                {
                    nums1[i] = n2;
                    n2Idx--;
                }
                else
                {
                    nums1[i] = n1;
                    n1Idx--;
                }

                i--;
            }

            Array.Copy(nums2, 0, nums1, 0, n2Idx + 1);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums1 = {1, 2, 3, 0, 0, 0};
                var m = 3;

                int[] nums2 = {2, 5, 6};
                var n = 3;

                // Act
                new Solution().Merge(nums1, m, nums2, n);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 2, 2, 3, 5, 6}, nums1);
            }
        }
    }
}