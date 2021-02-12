#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 169. Majority Element
// URL: https://leetcode.com/problems/majority-element/

namespace LeetCode
{
    public class Solution
    {
        public int MajorityElement(int[] nums)
        {
            // return MajorityElementObvious(nums);
            return MajorityElementMoore(nums);
        }

        private static int MajorityElementMoore(int[] nums)
        {
            // SS: Moore's Majority Element Algorithm,
            // based on balancing elements
            // SS: runtime complexity: O(N)
            // space complexity: O(1)

            var candidate = nums[0];
            var counter = 1;

            for (var i = 1; i < nums.Length; i++)
            {
                var v = nums[i];
                if (v != candidate)
                {
                    counter--;

                    if (counter == 0)
                    {
                        candidate = v;
                        counter = 1;
                    }
                }
                else
                {
                    counter++;
                }
            }

            return candidate;
        }

        private static int MajorityElementObvious(int[] nums)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(N)

            var map = new Dictionary<int, int>();
            var max = 0;
            var element = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                var elem = nums[i];
                if (map.TryGetValue(elem, out var freq))
                {
                    map[elem]++;
                }
                else
                {
                    map[elem] = 1;
                }

                if (map[elem] > max)
                {
                    max = map[elem];
                    element = elem;
                }
            }

            return element;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act

                // Assert
            }
        }
    }
}