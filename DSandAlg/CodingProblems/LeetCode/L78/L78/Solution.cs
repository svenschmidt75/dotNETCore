#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 78. Subsets
// URL: https://leetcode.com/problems/subsets/

namespace LeetCode
{
    public class Solution
    {
        public IList<IList<int>> Subsets(int[] nums)
        {
            // SS: generate the power set, which has 2^n elements, hence
            // the runtime complexity is O(2^n).
            // Using backtracking...
            var results = new List<IList<int>>();

            void Solve(int idx, List<int> r)
            {
                for (var i = idx; i < nums.Length; i++)
                {
                    r.Add(nums[i]);
                    Solve(i + 1, r);

                    // backtracking
                    r.RemoveAt(r.Count - 1);
                }

                results.Add(new List<int>(r));
            }

            Solve(0, new List<int>());

            return results;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {1, 2, 3};

                // Act
                var results = new Solution().Subsets(nums);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new int[0], new[] {1}, new[] {1, 2}, new[] {1, 3}, new[] {1, 2, 3}, new[] {2}, new[] {2, 3}, new[] {3}}, results);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {0};

                // Act
                var results = new Solution().Subsets(nums);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new int[0], new[] {0}}, results);
            }
        }
    }
}