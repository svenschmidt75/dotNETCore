#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 90. Subsets II
// URL: https://leetcode.com/problems/subsets-ii/

namespace LeetCode
{
    public class Solution
    {
        public IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            // SS: generate power set, so runtime complexity O(2^n)

            var subsets = new List<IList<int>>
            {
                // SS: empty subset
                new List<int>()
            };

            // SS: order, O(n log n)
            Array.Sort(nums);

            void Backtracking(int numIdx, List<int> subset)
            {
                for (var i = numIdx; i < nums.Length; i++)
                {
                    var v = nums[i];

                    // SS: ignore duplicates
                    // (this is why we sort nums...)
                    if (i > numIdx && nums[i - 1] == nums[i])
                    {
                        continue;
                    }

                    var s = new List<int>(subset) {v};
                    subsets.Add(s);
                    Backtracking(i + 1, s);
                }
            }

            Backtracking(0, new List<int>());

            return subsets;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums = {1, 2, 2};

                // Act
                var subsets = new Solution().SubsetsWithDup(nums);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new int[0], new[] {1}, new[] {2}, new[] {1, 2}, new[] {2, 2}, new[] {1, 2, 2}}, subsets);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {0};

                // Act
                var subsets = new Solution().SubsetsWithDup(nums);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new int[0], new[] {0}}, subsets);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {2, 1, 6, 3, 4, 6, 2, 2};

                // Act
                var subsets = new Solution().SubsetsWithDup(nums);

                // Assert
                CollectionAssert.AreEquivalent(
                    new[]
                    {
                        new int[0], new[] {1}, new[] {1, 2}, new[] {1, 2, 2}, new[] {1, 2, 2, 2}, new[] {1, 2, 2, 2, 3}, new[] {1, 2, 2, 2, 3, 4}, new[] {1, 2, 2, 2, 3, 4, 6}
                        , new[] {1, 2, 2, 2, 3, 4, 6, 6}, new[] {1, 2, 2, 2, 3, 6}, new[] {1, 2, 2, 2, 3, 6, 6}, new[] {1, 2, 2, 2, 4}, new[] {1, 2, 2, 2, 4, 6}, new[] {1, 2, 2, 2, 4, 6, 6}
                        , new[] {1, 2, 2, 2, 6}, new[] {1, 2, 2, 2, 6, 6}, new[] {1, 2, 2, 3}, new[] {1, 2, 2, 3, 4}, new[] {1, 2, 2, 3, 4, 6}, new[] {1, 2, 2, 3, 4, 6, 6}, new[] {1, 2, 2, 3, 6}
                        , new[] {1, 2, 2, 3, 6, 6}, new[] {1, 2, 2, 4}, new[] {1, 2, 2, 4, 6}, new[] {1, 2, 2, 4, 6, 6}, new[] {1, 2, 2, 6}, new[] {1, 2, 2, 6, 6}, new[] {1, 2, 3}, new[] {1, 2, 3, 4}
                        , new[] {1, 2, 3, 4, 6}, new[] {1, 2, 3, 4, 6, 6}, new[] {1, 2, 3, 6}, new[] {1, 2, 3, 6, 6}, new[] {1, 2, 4}, new[] {1, 2, 4, 6}, new[] {1, 2, 4, 6, 6}, new[] {1, 2, 6}
                        , new[] {1, 2, 6, 6}, new[] {1, 3}, new[] {1, 3, 4}, new[] {1, 3, 4, 6}, new[] {1, 3, 4, 6, 6}, new[] {1, 3, 6}, new[] {1, 3, 6, 6}, new[] {1, 4}, new[] {1, 4, 6}
                        , new[] {1, 4, 6, 6}, new[] {1, 6}, new[] {1, 6, 6}, new[] {2}, new[] {2, 2}, new[] {2, 2, 2}, new[] {2, 2, 2, 3}, new[] {2, 2, 2, 3, 4}, new[] {2, 2, 2, 3, 4, 6}
                        , new[] {2, 2, 2, 3, 4, 6, 6}, new[] {2, 2, 2, 3, 6}, new[] {2, 2, 2, 3, 6, 6}, new[] {2, 2, 2, 4}, new[] {2, 2, 2, 4, 6}, new[] {2, 2, 2, 4, 6, 6}, new[] {2, 2, 2, 6}
                        , new[] {2, 2, 2, 6, 6}, new[] {2, 2, 3}, new[] {2, 2, 3, 4}, new[] {2, 2, 3, 4, 6}, new[] {2, 2, 3, 4, 6, 6}, new[] {2, 2, 3, 6}, new[] {2, 2, 3, 6, 6}, new[] {2, 2, 4}
                        , new[] {2, 2, 4, 6}, new[] {2, 2, 4, 6, 6}, new[] {2, 2, 6}, new[] {2, 2, 6, 6}, new[] {2, 3}, new[] {2, 3, 4}, new[] {2, 3, 4, 6}, new[] {2, 3, 4, 6, 6}, new[] {2, 3, 6}
                        , new[] {2, 3, 6, 6}, new[] {2, 4}, new[] {2, 4, 6}, new[] {2, 4, 6, 6}, new[] {2, 6}, new[] {2, 6, 6}, new[] {3}, new[] {3, 4}, new[] {3, 4, 6}, new[] {3, 4, 6, 6}
                        , new[] {3, 6}, new[] {3, 6, 6}, new[] {4}, new[] {4, 6}, new[] {4, 6, 6}, new[] {6}, new[] {6, 6}
                    }, subsets);
            }
        }
    }
}