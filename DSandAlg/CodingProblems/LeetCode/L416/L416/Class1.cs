#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// LeetCode 416. Partition Equal Subset Sum
// https://leetcode.com/problems/partition-equal-subset-sum/


namespace L416
{
    public class Solution
    {
        private static int cnt = 1;

        public bool CanPartition(int[] nums)
        {
            // SS: basically, we want to do the same thing as for 0/1 knapsack.
            // We want to "steal" such that we end up with totalSum/2 "value".
            if (nums.Length < 2)
            {
                return false;
            }

            var memoization = new Dictionary<int, bool>[nums.Length];
            for (var i = 0; i < nums.Length; i++)
            {
                memoization[i] = new Dictionary<int, bool>();
            }

            var sum = nums.Sum();
            return SolveRec(nums, 0, 0, sum, memoization);
        }

        private bool SolveRec(int[] nums, int n, int leftSum, int totalSum, Dictionary<int, bool>[] memoization)
        {
            if (n == nums.Length)
            {
                return 2 * leftSum == totalSum;
            }

            var l = false;
            var r = false;

            var ls = leftSum + nums[n];
            if (memoization[n].TryGetValue(ls, out var v))
            {
                l = v;
            }
            else
            {
                l = SolveRec(nums, n + 1, ls, totalSum, memoization);
                memoization[n][ls] = l;
            }

            if (l)
            {
                return true;
            }

            ls = leftSum;
            if (memoization[n].TryGetValue(ls, out v))
            {
                r = v;
            }
            else
            {
                r = SolveRec(nums, n + 1, ls, totalSum, memoization);
                memoization[n][ls] = r;
            }

            if (r)
            {
                return true;
            }

            Console.WriteLine(cnt);
            cnt++;

            memoization[n][ls] = false;

            return false;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var nums = new[] {1, 5, 11, 5};

            // Act
            var result = new Solution().CanPartition(nums);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var nums = new[] {1, 2, 3, 5};

            // Act
            var result = new Solution().CanPartition(nums);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var nums = new[] {1, 2, 3, 4, 5, 6, 7};

            // Act
            var result = new Solution().CanPartition(nums);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Test4()
        {
            // Arrange
            var nums = new[] {1, 2, 3, 4, 5, 6, 7, 8};

            // Act
            var result = new Solution().CanPartition(nums);

            // Assert
            Assert.IsTrue(result);
        }
    }
}