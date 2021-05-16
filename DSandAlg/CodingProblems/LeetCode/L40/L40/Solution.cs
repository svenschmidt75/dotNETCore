#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 40. Combination Sum II
// URL: https://leetcode.com/problems/combination-sum-ii/

namespace LeetCode
{
    public class Solution
    {
        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            return CombinationSum21(candidates, target);
        }

        private IList<IList<int>> CombinationSum21(int[] candidates, int target)
        {
            var results = new List<IList<int>>();

            // SS: order candidates now so we can avoid duplicate
            // entries
            Array.Sort(candidates);

            void DFS(int idx, int t, List<int> r)
            {
                // SS: base case
                if (t == target)
                {
                    // SS: solution found
                    results.Add(new List<int>(r));
                    return;
                }

                if (idx == candidates.Length || t > target)
                {
                    return;
                }

                // SS: We do a for loop here, because we want to process each candidate number
                // only once, so we skip.
                // We cannot do that if we just branch in 1. take candidate[idx] and 2. do not
                // take candidate[idx]...
                for (var i = idx; i < candidates.Length; i++)
                {
                    var v = candidates[i];
                    r.Add(v);
                    DFS(i + 1, t + v, r);
                    r.RemoveAt(r.Count - 1);

                    // SS: skip duplicates
                    var j = i + 1;
                    while (j < candidates.Length && candidates[j] == candidates[i])
                    {
                        j++;
                    }

                    i = j - 1;
                }
            }

            DFS(0, 0, new List<int>());
            return results;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var result = new Solution().CombinationSum2(new[] {10, 1, 2, 7, 6, 1, 5}, 8);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {1, 1, 6}, new[] {1, 2, 5}, new[] {1, 7}, new[] {2, 6}}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var result = new Solution().CombinationSum2(new[] {2, 5, 2, 1, 2}, 5);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {1, 2, 2}, new[] {5}}, result);
            }
        }
    }
}