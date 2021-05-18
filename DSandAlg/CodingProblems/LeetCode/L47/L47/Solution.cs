#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 47. Permutations II
// URL: https://leetcode.com/problems/permutations-ii/

namespace LeetCode
{
    public class Solution
    {
        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            // SS: using backtracking
            // runtime complexity: O(n!)

            // SS: using dict here since we may have duplicates
            var multiset = new Dictionary<int, int>();
            for (var i = 0; i < nums.Length; i++)
            {
                var c = nums[i];
                if (multiset.ContainsKey(c) == false)
                {
                    multiset[c] = 0;
                }

                multiset[c]++;
            }

            // SS: sort, so we can handle duplicates
            Array.Sort(nums);

            var result = new List<IList<int>>();

            void Backtracking(List<int> currentPermutation, IDictionary<int, int> currentMultiset)
            {
                // SS: base case
                if (currentPermutation.Count == nums.Length)
                {
                    result.Add(new List<int>(currentPermutation));
                    return;
                }

                for (var i = 0; i < nums.Length; i++)
                {
                    if (i > 0 && nums[i - 1] == nums[i])
                    {
                        // SS: skip duplicates
                        continue;
                    }

                    var v = nums[i];
                    if (currentMultiset.ContainsKey(v) && currentMultiset[v] > 0)
                    {
                        currentMultiset[v]--;
                        currentPermutation.Add(v);
                        Backtracking(currentPermutation, currentMultiset);
                        currentPermutation.RemoveAt(currentPermutation.Count - 1);
                        currentMultiset[v]++;
                    }
                }
            }

            Backtracking(new List<int>(), multiset);
            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var result = new Solution().PermuteUnique(new[] {1, 1, 2});

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {1, 1, 2}, new[] {1, 2, 1}, new[] {2, 1, 1}}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var result = new Solution().PermuteUnique(new[] {1, 2, 3});

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {1, 2, 3}, new[] {1, 3, 2}, new[] {2, 1, 3}, new[] {2, 3, 1}, new[] {3, 1, 2}, new[] {3, 2, 1}}, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange

                // Act
                var result = new Solution().PermuteUnique(new[] {5, 4, 3, 5, 4});

                // Assert
                CollectionAssert.AreEquivalent(
                    new[]
                    {
                        new[] {3, 4, 4, 5, 5}, new[] {3, 4, 5, 4, 5}, new[] {3, 4, 5, 5, 4}, new[] {3, 5, 4, 4, 5}, new[] {3, 5, 4, 5, 4}, new[] {3, 5, 5, 4, 4}, new[] {4, 3, 4, 5, 5}
                        , new[] {4, 3, 5, 4, 5}, new[] {4, 3, 5, 5, 4}, new[] {4, 4, 3, 5, 5}, new[] {4, 4, 5, 3, 5}, new[] {4, 4, 5, 5, 3}, new[] {4, 5, 3, 4, 5}, new[] {4, 5, 3, 5, 4}
                        , new[] {4, 5, 4, 3, 5}, new[] {4, 5, 4, 5, 3}, new[] {4, 5, 5, 3, 4}, new[] {4, 5, 5, 4, 3}, new[] {5, 3, 4, 4, 5}, new[] {5, 3, 4, 5, 4}, new[] {5, 3, 5, 4, 4}
                        , new[] {5, 4, 3, 4, 5}, new[] {5, 4, 3, 5, 4}, new[] {5, 4, 4, 3, 5}, new[] {5, 4, 4, 5, 3}, new[] {5, 4, 5, 3, 4}, new[] {5, 4, 5, 4, 3}, new[] {5, 5, 3, 4, 4}
                        , new[] {5, 5, 4, 3, 4}, new[] {5, 5, 4, 4, 3}
                    }, result);
            }
        }
    }
}