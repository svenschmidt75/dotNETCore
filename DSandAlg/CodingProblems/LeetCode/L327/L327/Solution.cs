using System;
using NUnit.Framework;

// 327. Count of Range Sum
// https://leetcode.com/problems/count-of-range-sum/

namespace L327
{
    public class Solution
    {
        public int CountRangeSum(int[] nums, int lower, int upper)
        {
            return CountRangeSum1(nums, lower, upper);
        }

        public int CountRangeSum1(int[] nums, int lower, int upper)
        {
            // SS: taken from https://leetcode.com/problems/count-of-range-sum/discuss/907027/Java-Count-while-Merge-Sort-O(nlogn)-Time-Clean-Concise-Solution
            // for me to understand

            int n = nums.Length;

            // SS: create prefix sum
            int[] sums = new int[n + 1];
            for(int i = 0; i < n; ++i) {
                sums[i + 1] = sums[i] + nums[i];
            }

            return CountWhileMergeSort(sums, 0, n + 1, lower, upper);
        }

        private int CountWhileMergeSort(int[] sums, int low, int high, int lower, int upper)
        {
            if (high - low <= 1) {
                return 0;
            }
            int mid = low + (high - low) / 2;
            int count = CountWhileMergeSort(sums, low, mid, lower, upper) + CountWhileMergeSort(sums, mid, high, lower, upper);
            count += CountWhileMerge(sums, low, mid, high, lower, upper);
            return count;
        }

        private int CountWhileMerge(int[] sums, int low, int mid, int high, int lower, int upper)
        {
            int j = mid, k = mid, t = mid, count = 0;
            int[] cache = new int[high - low];
            for (int i = low, r = 0; i < mid; ++i, ++r) {
                while (k < high && sums[k] - sums[i] < lower) {
                    k++;
                }
                while (j < high && sums[j] - sums[i] <= upper) {
                    j++;
                }
                while (t < high && sums[t] < sums[i]) {
                    cache[r++] = sums[t++];
                }
                cache[r] = sums[i];
                count += (j - k);
            }
            Array.Copy(cache, 0, sums, low, t - low);
            return count;
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {-2, 5, -1};
                int lower = -2;
                int upper = 2;
                
                // Act
                int nRangeSums = new Solution().CountRangeSum(nums, lower, upper);
                
                // Assert
                Assert.AreEqual(3, nRangeSums);
            }
            
            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = new int[0];
                int lower = 0;
                int upper = 0;
                
                // Act
                int nRangeSums = new Solution().CountRangeSum(nums, lower, upper);
                
                // Assert
                Assert.AreEqual(0, nRangeSums);
            }
            
            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = new [] {0, 2, 5, 3, 1, -3, 7, 1, 9, -3};
                int lower = 8;
                int upper = 13;
                
                // Act
                int nRangeSums = new Solution().CountRangeSum(nums, lower, upper);
                
                // Assert
                Assert.AreEqual(16, nRangeSums);
            }
            
        }
    }
}