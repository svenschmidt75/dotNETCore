#region

using System;
using NUnit.Framework;

#endregion

// Problem: 16. 3Sum Closest
// URL: https://leetcode.com/problems/3sum-closest/

namespace LeetCode16
{
    public class Solution
    {
        public int ThreeSumClosest(int[] nums, int target)
        {
            // SS: total runtime complexity: O(N^2), using sorting and sliding window

            // O(N log N)
            Array.Sort(nums);

            var closestSum = int.MaxValue;
            var minDistance = int.MaxValue;

            for (var i = 0; i < nums.Length; i++)
            {
                // prevent doing work we have already done
                if (i > 0 && nums[i] == nums[i - 1])
                {
                    continue;
                }

                var a = nums[i];

                var j = i + 1;
                var k = nums.Length - 1;
                while (j < k)
                {
                    var b = nums[j];
                    var c = nums[k];

                    var s = a + b + c;

                    var distance = Math.Abs(s - target);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestSum = s;
                    }

                    if (s < target)
                    {
                        // increase j to make sum bigger
                        j++;

                        while (j < k && nums[j] == nums[j - 1])
                        {
                            j++;
                        }
                    }
                    else if (s > target)
                    {
                        // decrease k so make sum smaller
                        k--;

                        while (j > k && nums[k] == nums[k + 1])
                        {
                            k--;
                        }
                    }
                    else
                    {
                        // sum == target

                        // increase j to make sum bigger
                        j++;

                        while (j < k && nums[j] == nums[j - 1])
                        {
                            j++;
                        }

                        // decrease k so make sum smaller
                        k--;

                        while (j < k && nums[k] == nums[k + 1])
                        {
                            k--;
                        }
                    }
                }
            }

            return closestSum;
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {-1, 2, 1, -4};
                var target = 1;

                // Act
                var result = new Solution().ThreeSumClosest(nums, target);

                // Assert
                Assert.AreEqual(2, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {1, 1, 1, 1};
                var target = 3;

                // Act
                var result = new Solution().ThreeSumClosest(nums, target);

                // Assert
                Assert.AreEqual(3, result);
            }
        }
    }
}