#region

using System;
using NUnit.Framework;

#endregion

// Problem: 164. Maximum Gap
// URL: https://leetcode.com/problems/maximum-gap/

namespace LeetCode
{
    public class Solution
    {
        public int MaximumGap(int[] nums)
        {
            // SS: use a variant of Bucket Sort
            // runtime complexity: O(n)
            // space complexity: O(n)
            
            if (nums.Length < 2)
            {
                return 0;
            }

            // SS: find min and max value
            var min = int.MaxValue;
            var max = int.MinValue;
            for (var i = 0; i < nums.Length; i++)
            {
                var v = nums[i];
                min = Math.Min(min, v);
                max = Math.Max(max, v);
            }

            // SS: initialize buckets
            var b = Math.Max(1, (int) Math.Ceiling((max - min) / (double) nums.Length));
            var b2 = (max - min) / b + 1;
            (int min, int max)[] buckets = new (int, int )[b2];
            for (var i = 0; i < b2; i++)
            {
                buckets[i] = (int.MaxValue, int.MinValue);
            }

            // SS: put elements into buckets
            for (var i = 0; i < nums.Length; i++)
            {
                var v = nums[i];
                var bucketIdx = (v - min) / b;

                var bucketMin = Math.Min(buckets[bucketIdx].min, v);
                var bucketMax = Math.Max(buckets[bucketIdx].max, v);
                buckets[bucketIdx] = (bucketMin, bucketMax);
            }

            // SS: calc. distance between buckets
            int maxDst;
            if (b2 == 1)
            {
                maxDst = buckets[0].max - buckets[0].min;
            }
            else
            {
                maxDst = 0;
                var j = 0;
                while (j < b2)
                {
                    if (buckets[j].max != int.MinValue)
                    {
                        break;
                    }

                    j++;
                }

                for (var i = 1; i < b2; i++)
                {
                    if (buckets[i].min == int.MaxValue)
                    {
                        // SS: skip bucket
                        continue;
                    }

                    maxDst = Math.Max(maxDst, buckets[i].min - buckets[j].max);
                    j = i;
                }
            }

            return maxDst;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {3, 6, 9, 1};

                // Act
                var maxDst = new Solution().MaximumGap(nums);

                // Assert
                Assert.AreEqual(3, maxDst);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {21, 10, 5, 3, 16, 1};

                // Act
                var maxDst = new Solution().MaximumGap(nums);

                // Assert
                Assert.AreEqual(6, maxDst);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {1, 4, 5, 11, 14, 17, 20};

                // Act
                var maxDst = new Solution().MaximumGap(nums);

                // Assert
                Assert.AreEqual(6, maxDst);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] nums = {1, 1, 1, 1};

                // Act
                var maxDst = new Solution().MaximumGap(nums);

                // Assert
                Assert.AreEqual(0, maxDst);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] nums =
                {
                    15252, 16764, 27963, 7817, 26155, 20757, 3478, 22602, 20404, 6739, 16790, 10588, 16521, 6644, 20880, 15632, 27078, 25463, 20124, 15728, 30042, 16604, 17223, 4388, 23646, 32683
                    , 23688, 12439, 30630, 3895, 7926, 22101, 32406, 21540, 31799, 3768, 26679, 21799, 23740
                };

                // Act
                var maxDst = new Solution().MaximumGap(nums);

                // Assert
                Assert.AreEqual(2901, maxDst);
            }
        }
    }
}