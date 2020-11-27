#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 46. Permutations
// URL: https://leetcode.com/problems/permutations/

namespace LeetCode46
{
    public class Solution
    {
        public IList<IList<int>> Permute(int[] nums)
        {
            // SS: runtime complexity: O(n!)
            // The solution idea is to recognize that the problem of computing all permutations of [1, 2, 3]
            // is the same as
            // 1 append the problem of computing all permutations of [2, 3]
            // 2 append the problem of computing all permutations of [1, 3]
            // 3 append the problem of computing all permutations of [1, 2]
            // i.e. a Divide and Conquer approach.
            // We are dividing the problem into smaller and smaller problems...
            
            var result = new List<IList<int>>();

            void Perm(int[] nums, List<int> v, HashSet<int> idx)
            {
                for (var i = 0; i < nums.Length; i++)
                {
                    if (idx.Contains(i))
                    {
                        continue;
                    }

                    idx.Add(i);

                    v.Add(nums[i]);
                    
                    Perm(nums, v, idx);

                    // SS: backtracking, i.e. remove
                    v.RemoveAt(v.Count - 1);
                    
                    idx.Remove(i);
                }

                if (v.Count == nums.Length)
                {
                    var c = new List<int>(v);
                    result.Add(c);
                }
            }

            Perm(nums, new List<int>(), new HashSet<int>());

            return result;
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
                var permutations = new Solution().Permute(nums);

                // Assert
                Assert.AreEqual(6, permutations.Count);
                CollectionAssert.AreEquivalent(new[] {new[] {1, 2, 3}, new[] {1, 3, 2}, new[] {2, 1, 3}, new[] {2, 3, 1}, new[] {3, 1, 2}, new[] {3, 2, 1}}, permutations);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums = {0, 1};

                // Act
                var permutations = new Solution().Permute(nums);

                // Assert
                Assert.AreEqual(2, permutations.Count);
                CollectionAssert.AreEquivalent(new[] {new[] {0, 1}, new[] {1, 0}}, permutations);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] nums = {0};

                // Act
                var permutations = new Solution().Permute(nums);

                // Assert
                Assert.AreEqual(1, permutations.Count);
                CollectionAssert.AreEquivalent(new[] {new[] {0}}, permutations);
            }
        }
    }
}