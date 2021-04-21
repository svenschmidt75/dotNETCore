#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 274. H-Index
// URL: https://leetcode.com/problems/h-index/

namespace LeetCode
{
    public class Solution
    {
        public int HIndex(int[] citations)
        {
//            return HIndex1(citations);
            return HIndex2(citations);
        }

        public int HIndex2(int[] citations)
        {
            // SS: Solution from LeetCode

            var max = int.MinValue;
            var count = new Dictionary<int, int>();

            foreach (var n in citations)
            {
                count[n] = count.ContainsKey(n) ? count[n] + 1 : 1;
                max = Math.Max(max, n);
            }

            var prev = 0;
            for (var n = max; n > 0; n--)
            {
                count[n] = count.ContainsKey(n) ? count[n] + prev : prev;
                prev = count[n];
                if (count[n] >= n) return n;
            }

            return 0;
        }

        private int HIndex1(int[] citations)
        {
            // SS: runtime complexity: O(n log n)
            // space complexity: O(1)
            Array.Sort(citations);

            var n = citations.Length;

            var maxH = 0;

            for (var i = 0; i < n; i++)
            {
                var h = n - i;
                if (h < maxH)
                {
                    break;
                }

                var nc = citations[i];
                if (nc >= h)
                {
                    maxH = Math.Max(maxH, h);
                }
            }

            return maxH;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {3, 0, 6, 1, 5}, 3)]
            [TestCase(new[] {1, 3, 1}, 1)]
            [TestCase(new int[0], 0)]
            [TestCase(new[] {9, 3, 56, 3, 7, 9, 23, 5, 7}, 6)]
            public void Test(int[] citations, int expectedH)
            {
                // Arrange

                // Act
                var h = new Solution().HIndex(citations);

                // Assert
                Assert.AreEqual(expectedH, h);
            }
        }
    }
}