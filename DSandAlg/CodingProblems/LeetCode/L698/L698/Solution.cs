#region

using System;
using NUnit.Framework;

#endregion

// Problem: 698. Partition to K Equal Sum Subsets
// URL: https://leetcode.com/problems/partition-to-k-equal-sum-subsets/

namespace LeetCode
{
    public class Solution
    {
        public bool CanPartitionKSubsets(int[] nums, int k)
        {
//            return CanPartitionKSubsets1(nums, k);
            // return CanPartitionKSubsets2(nums, k);
            return CanPartitionKSubsets3(nums, k);
        }

        private bool CanPartitionKSubsets3(int[] nums, int k)
        {
            // SS: DFS with backtracking
            // runtime complexity: O(n * (n - 1) * (n - 2) * ... * 2 * 1) = O(n!)
            // space complexity: O(n)
            
            // SS: check whether k evenly divides the sum of all elements
            var sum = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
            }

            if (sum % k > 0)
            {
                return false;
            }

            // SS: length of each bin
            var binSize = sum / k;

            // SS: find k bins of length binSize
            Array.Sort(nums);

            bool DFS(int numsIdx, bool[] numsUsed, int setIdx, int setSum)
            {
                // SS: base case
                if (setIdx == k)
                {
                    return true;
                }

                for (var i = numsIdx; i < nums.Length; i++)
                {
                    if (numsUsed[i])
                    {
                        continue;
                    }

                    numsUsed[i] = true;

                    var v = nums[i];
                    if (setSum + v == binSize)
                    {
                        // SS: finished current set, process next set
                        if (DFS(0, numsUsed, setIdx + 1, 0))
                        {
                            // SS: as soon as we have a solution, we are done
                            return true;
                        }
                    }
                    else if (setSum + v < binSize)
                    {
                        // SS: add to current set
                        if (DFS(i + 1, numsUsed, setIdx, setSum + v))
                        {
                            // SS: as soon as we have a solution, we are done
                            return true;
                        }
                    }

                    // SS: backtrack
                    numsUsed[i] = false;
                }

                return false;
            }

            return DFS(0, new bool[nums.Length], 0, 0);
        }

        private bool CanPartitionKSubsets2(int[] nums, int k)
        {
            // SS: using Divide & Conquer, runtime complexity: O(k^N)
            // space complexity: O(k + N) 

            // SS: check whether k evenly divides the sum of all elements
            var sum = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
            }

            if (sum % k > 0)
            {
                return false;
            }

            // SS: length of each bin
            var binSize = sum / k;

            var bins = new int[k];

            bool DFS(int idx)
            {
                if (idx == nums.Length)
                {
                    // SS: done, check if all bins have the correct size
                    for (var i = 0; i < bins.Length; i++)
                    {
                        if (bins[i] != binSize)
                        {
                            return false;
                        }
                    }

                    return true;
                }

                var v = nums[idx];

                // SS: put each nums[idx] into each of the k buckets
                // and check whether it leads to a valid solution...
                for (var i = 0; i < k; i++)
                {
                    var binLength = bins[i];
                    if (binLength + v <= binSize)
                    {
                        bins[i] += v;
                        if (DFS(idx + 1))
                        {
                            return true;
                        }

                        // SS: backtrack
                        bins[i] -= v;
                    }
                }

                return false;
            }

            return DFS(0);
        }

        private bool CanPartitionKSubsets1(int[] nums, int k)
        {
            // SS: greedy, i.e. sort and always use the max. element.
            // Does not work...

            // SS: check whether k evenly divides the sum of all elements
            var sum = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
            }

            if (sum % k > 0)
            {
                return false;
            }

            // SS: length of each bin
            var binSize = sum / k;

            // SS: find k bins of length binSize
            Array.Sort(nums);

            var bins = new int[k];

            for (var i = nums.Length - 1; i >= 0; i--)
            {
                var v = nums[i];

                var j = 0;
                while (j < bins.Length)
                {
                    var binLength = bins[j];
                    if (binLength + v <= binSize)
                    {
                        bins[j] += v;
                        break;
                    }

                    j++;
                }

                if (j == bins.Length)
                {
                    // SS: no bin had enough capacity, error
                    return false;
                }
            }

            for (var i = 0; i < bins.Length; i++)
            {
                if (bins[i] != binSize)
                {
                    return false;
                }
            }

            return true;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] { 4, 3, 2, 3, 5, 2, 1 }, 4, true)]
            [TestCase(new[] { 1, 2, 3, 4 }, 3, false)]
            [TestCase(new[] { 3522, 181, 521, 515, 304, 123, 2512, 312, 922, 407, 146, 1932, 4037, 2646, 3871, 269 }, 5, true)]
            [TestCase(new[] { 7628, 3147, 7137, 2578, 7742, 2746, 4264, 7704, 9532, 9679, 8963, 3223, 2133, 7792, 5911, 3979 }, 6, false)]
            public void Test(int[] nums, int k, bool expected)
            {
                // Arrange

                // Act
                var result = new Solution().CanPartitionKSubsets(nums, k);

                // Assert
                Assert.AreEqual(expected, result);
            }
        }
    }
}