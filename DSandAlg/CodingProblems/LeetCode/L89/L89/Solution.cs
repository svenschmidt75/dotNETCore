#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 89. Gray Code
// URL: https://leetcode.com/problems/gray-code/

namespace LeetCode
{
    public class Solution
    {
        public IList<int> GrayCode(int n)
        {
            // SS: runtime complexity: O(2^n)
            // space complexity: O(2^n)  
            var results = new List<int> {0};
            var map = new HashSet<int>();

            void Solve(int number)
            {
                for (var i = 0; i < n; i++)
                {
                    var n = FlipBit(number, i);
                    if (map.Contains(n))
                    {
                        continue;
                    }

                    map.Add(n);
                    results.Add(n);
                    Solve(n);
                }
            }

            map.Add(0);
            Solve(0);
            return results;
        }

        private static int FlipBit(int number, int bit)
        {
            return (1 << bit) ^ number;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var results = new Solution().GrayCode(2);

                // Assert
                CollectionAssert.AreEqual(new[] {0, 1, 3, 2}, results);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var results = new Solution().GrayCode(3);

                // Assert
                CollectionAssert.AreEquivalent(new[] {0, 1, 3, 2, 6, 7, 5, 4}, results);
            }
        }
    }
}