#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 39. Combination Sum
// URL: https://leetcode.com/problems/combination-sum/

namespace LeetCode39
{
    public class Solution
    {
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            IList<IList<int>> results = new List<IList<int>>();
            CombinationSumDQ(candidates, target, 0, results, new List<int>());
            return results;
        }

        private void CombinationSumDQ(int[] candidates, int target, int pos, IList<IList<int>> results, List<int> result)
        {
            // SS: Divide and Conquer solution
            // runtime complexity: O(2^N), space complexity: O(N)
            
            if (pos == candidates.Length)
            {
                if (target == 0)
                {
                    results.Add(result);
                }

                return;
            }

            if (target == 0)
            {
                results.Add(result);
                return;
            }

            if (target < 0)
            {
                return;
            }

            var c = candidates[pos];

            // SS: use the same number again
            var r1 = new List<int>(result);
            r1.Add(c);
            CombinationSumDQ(candidates, target - c, pos, results, r1);

            // skip this position
            CombinationSumDQ(candidates, target, pos + 1, results, result);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] candidates = {2, 3, 6, 7};
                var target = 7;

                // Act
                var result = new Solution().CombinationSum(candidates, target);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {2, 2, 3}, new[] {7}}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] candidates = {2, 3, 5};
                var target = 8;

                // Act
                var result = new Solution().CombinationSum(candidates, target);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {2, 2, 2, 2}, new[] {2, 3, 3}, new[] {3, 5}}, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] candidates = {2};
                var target = 3;

                // Act
                var result = new Solution().CombinationSum(candidates, target);

                // Assert
                Assert.False(result.Any());
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] candidates = {1};
                var target = 1;

                // Act
                var result = new Solution().CombinationSum(candidates, target);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {1}}, result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] candidates = {1};
                var target = 2;

                // Act
                var result = new Solution().CombinationSum(candidates, target);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {1, 1}}, result);
            }
        }
    }
}