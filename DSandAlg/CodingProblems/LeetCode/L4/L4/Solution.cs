#region

using NUnit.Framework;

#endregion

// Problem: 4. Median of Two Sorted Arrays
// URL: https://leetcode.com/problems/median-of-two-sorted-arrays/

namespace LeetCode
{
    public class Solution
    {
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            return FindMedianSortedArrays1(nums1, nums2);
        }

        private double FindMedianSortedArrays1(int[] nums1, int[] nums2)
        {
            // SS: implement 2-way merge
            // runtime complexity: 1 comparison per element, hence O(2 * (nums1 + nums2))
            // space complexity: O(nums1 + nums2)

            var i = 0;
            var j = 0;

            var combined = new int[nums1.Length + nums2.Length];
            var c = 0;

            while (i < nums1.Length && j < nums2.Length)
            {
                var v = nums1[i] <= nums2[j] ? nums1[i++] : nums2[j++];
                combined[c++] = v;
            }

            while (i < nums1.Length)
            {
                combined[c++] = nums1[i++];
            }

            while (j < nums2.Length)
            {
                combined[c++] = nums2[j++];
            }

            double median;

            var length = combined.Length;
            var l2 = length / 2;
            if (length % 2 == 1)
            {
                // SS: odd length
                median = combined[l2];
            }
            else
            {
                median = (combined[l2 - 1] + combined[l2]) / 2.0;
            }

            return median;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums1 = {1, 3};
                int[] nums2 = {2};

                // Act
                var median = new Solution().FindMedianSortedArrays(nums1, nums2);

                // Assert
                Assert.That(median, Is.EqualTo(2));
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums1 = {1, 2};
                int[] nums2 = {3, 4};

                // Act
                var median = new Solution().FindMedianSortedArrays(nums1, nums2);

                // Assert
                Assert.That(median, Is.EqualTo((2 + 3) / 2.0));
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums1 = {0, 0};
                int[] nums2 = {0, 0};

                // Act
                var median = new Solution().FindMedianSortedArrays(nums1, nums2);

                // Assert
                Assert.That(median, Is.EqualTo(0));
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var nums1 = new int[0];
                int[] nums2 = {1};

                // Act
                var median = new Solution().FindMedianSortedArrays(nums1, nums2);

                // Assert
                Assert.That(median, Is.EqualTo(1));
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums1 = {2};
                var nums2 = new int[0];

                // Act
                var median = new Solution().FindMedianSortedArrays(nums1, nums2);

                // Assert
                Assert.That(median, Is.EqualTo(2));
            }
        }
    }
}