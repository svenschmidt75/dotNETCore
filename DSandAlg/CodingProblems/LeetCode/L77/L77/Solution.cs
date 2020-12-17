#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 77. Combinations
// URL: https://leetcode.com/problems/combinations/

namespace LeetCode
{
    public class Solution
    {
        public IList<IList<int>> Combine(int n, int k)
        {
            var combinations = new List<IList<int>>();

            if (n < k)
            {
                return combinations;
            }

            void Solve(int nIdx, int kIdx, int[] r)
            {
                if (kIdx == k)
                {
                    combinations.Add(r.ToList());
                    return;
                }

                if (nIdx == n)
                {
                    return;
                }

                for (var i = nIdx + 1; i <= n; i++)
                {
                    r[kIdx] = i;
                    Solve(i, kIdx + 1, r);
                }
            }

            var r = new int[k];
            Solve(0, 0, r);

            return combinations;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var combinations = new Solution().Combine(4, 2);

                // Assert
                CollectionAssert.AreEquivalent(new[] {new List<int> {2, 4}, new List<int> {3, 4}, new List<int> {2, 3}, new List<int> {1, 2}, new List<int> {1, 3}, new List<int> {1, 4}}
                    , combinations);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var combinations = new Solution().Combine(5, 3);

                // Assert
                CollectionAssert.AreEquivalent(new[]
                {
                    new List<int> {1, 2, 3}
                    , new List<int> {1, 2, 4}
                    , new List<int> {1, 2, 5}
                    , new List<int> {1, 3, 4}
                    , new List<int> {1, 3, 5}
                    , new List<int> {1, 4, 5}
                    , new List<int> {2, 3, 4}
                    , new List<int> {2, 3, 5}
                    , new List<int> {2, 4, 5}
                    , new List<int> {3, 4, 5}
                }, combinations);
            }
        }
    }
}