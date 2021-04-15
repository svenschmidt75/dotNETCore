#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 229. Majority Element II
// URL: https://leetcode.com/problems/majority-element-ii/

namespace LeetCode
{
    public class Solution
    {
        public IList<int> MajorityElement(int[] nums)
        {
            // return MajorityElement1(nums);
            return MajorityElement2(nums);
        }

        private IList<int> MajorityElement2(int[] nums)
        {
            // SS: Since there can be at most 2 majority elements (n/3 <= 2 always),
            // we use the Boyer-Moore majority algorithm for two different elements.
            // Runtime complexity: O(n)
            // Space complexity: O(1)
            if (nums.Length < 2)
            {
                return nums;
            }

            var candidate1 = int.MaxValue;
            var count1 = 0;

            var candidate2 = int.MaxValue;
            var count2 = 0;

            for (var i = 0; i < nums.Length; i++)
            {
                if (nums[i] == candidate1)
                {
                    count1++;
                    continue;
                }

                if (nums[i] == candidate2)
                {
                    count2++;
                    continue;
                }

                if (count1 == 0)
                {
                    candidate1 = nums[i];
                    count1 = 1;
                    continue;
                }

                if (count2 == 0)
                {
                    candidate2 = nums[i];
                    count2 = 1;
                    continue;
                }

                // SS: this only happens half the time at most...
                count1--;
                count2--;
            }

            // SS: verify the majority element exists
            var result = new List<int>();
            count1 = 0;
            count2 = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == candidate1)
                {
                    count1++;
                }
                if (nums[i] == candidate2)
                {
                    count2++;
                }
            }
            
            if (count1 > nums.Length / 3)
            {
                result.Add(candidate1);
            }

            if (count2 > nums.Length / 3)
            {
                result.Add(candidate2);
            }

            return result;
        }

        private IList<int> MajorityElement1(int[] nums)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(n)

            var dict = new Dictionary<int, int>();

            // SS: frequency for each element
            for (var i = 0; i < nums.Length; i++)
            {
                if (dict.ContainsKey(nums[i]))
                {
                    dict[nums[i]]++;
                }
                else
                {
                    dict[nums[i]] = 1;
                }
            }

            var result = new List<int>();
            foreach (var item in dict)
            {
                if (item.Value > nums.Length / 3)
                {
                    result.Add(item.Key);
                }
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {3, 2, 3}, new[] {3})]
            [TestCase(new[] {1}, new[] {1})]
            [TestCase(new[] {1, 2}, new[] {1, 2})]
            [TestCase(new[] {1, 2, 3, 4, 5, 6, 7}, new int[0])]
            [TestCase(new[] {1, 2, 3, 4, 1, 6, 1}, new[] {1})]
            public void Test1(int[] nums, int[] expected)
            {
                // Arrange

                // Act
                var result = new Solution().MajorityElement(nums);

                // Assert
                CollectionAssert.AreEquivalent(expected, result);
            }
        }
    }
}