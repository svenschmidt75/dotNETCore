#region

using System;
using NUnit.Framework;

#endregion

// Problem: 275. H-Index II
// URL: https://leetcode.com/problems/h-index-ii/

namespace LeetCode
{
    public class Solution
    {
        public int HIndex(int[] citations)
        {
            // SS: runtime complexity: O(log n)
            // space complexity: O(1)

            var maxH = 0;

            var min = 0;
            var max = citations.Length;

            while (min < max)
            {
                var mid = (min + max) / 2;

                var nCitations = citations[mid];
                var countPapers = citations.Length - mid;

                // SS: the h index cannot be larger than the number of citations
                // and it cannot be larger than the number of papers with at least
                // that number of citations.
                var h = Math.Min(countPapers, nCitations);
                maxH = Math.Max(maxH, h);

                // SS: we can only grow h if there are more than maxH papers left...
                if (countPapers > maxH)
                {
                    // SS: h index can only grow to the right
                    min = mid + 1;
                }
                else
                {
                    max = mid;
                }
            }

            return maxH;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(new[] {0, 1, 3, 5, 6}, 3)]
            [TestCase(new[] {0, 1, 1, 5, 6}, 2)]
            [TestCase(new[] {1, 2, 100}, 2)]
            [TestCase(new[] {1, 1, 1, 1, 1}, 1)]
            [TestCase(new[] {11, 15}, 2)]
            public void Test(int[] nums, int expected)
            {
                // Arrange

                // Act
                var hIndex = new Solution().HIndex(nums);

                // Assert
                Assert.AreEqual(expected, hIndex);
            }
        }
    }
}