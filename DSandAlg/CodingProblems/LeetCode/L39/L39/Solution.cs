#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 39. Combination Sum
// URL: https://leetcode.com/problems/combination-sum/

/* Note that this is essentially the coin change problem, https://www.geeksforgeeks.org/coin-change-dp-7/,
 * except that we have to actually generate the solutions in this problem, not just return how many
 * solutions there are.
 */

namespace LeetCode39
{
    public class Solution
    {
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
//            var results = CombinationSumDQ(candidates, target);

            var results = CombinationSumDQ2(candidates, target);
            return results;
        }

        private IList<IList<int>> CombinationSumDQ(int[] candidates, int target)
        {
            IList<IList<int>> results = new List<IList<int>>();
            CombinationSumDQ(candidates, target, 0, results, new List<int>());
            return results;
        }

        private void CombinationSumDQ(int[] candidates, int target, int pos, IList<IList<int>> results, List<int> result)
        {
            // SS: Divide and Conquer solution
            // runtime complexity: O(2^N)
            // space complexity: O(N) for call stack and O(N^2) for results

            if (target == 0)
            {
                results.Add(result);
                return;
            }

            if (pos == candidates.Length || target < 0)
            {
                return;
            }

            Console.WriteLine($"{target} {pos}");

            var c = candidates[pos];

            // SS: use the same number again
            var r1 = new List<int>(result) {c};
            CombinationSumDQ(candidates, target - c, pos, results, r1);

            // skip this position
            CombinationSumDQ(candidates, target, pos + 1, results, result);
        }

        private IList<IList<int>> CombinationSumDQ2(int[] candidates, int target)
        {
            // SS: Divide and Conquer solution
            // runtime complexity: O(2^N)
            // space complexity: O(N) for call stack and O(N^2) for results

            var sorted = candidates.OrderBy(x => x).ToArray();

            IList<int[]> results = new List<int[]>();
            CombinationSumDQ2(sorted, target, 0, results, new int[candidates.Length]);

            return results.Select(r =>
            {
                var tmp = new List<int>();
                for (var i = 0; i < r.Length; i++)
                {
                    for (var j = 0; j < r[i]; j++)
                    {
                        tmp.Add(sorted[i]);
                    }
                }

                return (IList<int>) tmp;
            }).ToList();
        }

        private void CombinationSumDQ2(int[] candidates, int target, int pos, IList<int[]> results, int[] result)
        {
            // SS: candidates is assumed to be sorted
            // runtime complexity: O( pi_{i=0}^{candidates.Length - 1} target/candidates[i])
            // = O( target^N * 1 / pi_{i=0}^{candidates.Length - 1} target/candidates[i]), i.e exponential
            // complexity.

            if (target == 0)
            {
                var r = new int[result.Length];
                Array.Copy(result, r, result.Length);
                results.Add(r);
                return;
            }

            if (pos == candidates.Length || target < 0)
            {
                return;
            }

//            Console.WriteLine($"{target} {pos}");

            var c = candidates[pos];

            if (c > target)
            {
                return;
            }

            var div = target / c;

            for (var i = 0; i <= div; i++)
            {
                if (i > 0)
                {
                    result[pos]++;
                }

                CombinationSumDQ2(candidates, target - i * c, pos + 1, results, result);
            }

            // SS: restore state (typical in backtracking)
            result[pos] = 0;
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

            [Test]
            public void Test6()
            {
                // Arrange
                int[] candidates = {3, 2};
                var target = 10;

                // Act
                var result = new Solution().CombinationSum(candidates, target);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new[] {2, 2, 2, 2, 2}, new[] {2, 2, 3, 3}}, result);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[] candidates = {1, 2, 3, 4, 5, 6, 7, 8, 9};
                var target = 23;

                // Act
                var result = new Solution().CombinationSum(candidates, target);

                // Assert
                Assert.AreEqual(887, result.Count);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                int[] candidates = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
                var target = 20;

                // Act
                var result = new Solution().CombinationSum(candidates, target);
                
                // Assert
                Assert.AreEqual(530, result.Count);
            }
        }
    }
}