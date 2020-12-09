#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 56. Merge Intervals
// URL: https://leetcode.com/problems/merge-intervals/

namespace LeetCode
{
    public class Solution
    {
        public int[][] Merge(int[][] intervals)
        {
            // SS:: runtime complexity: O(N log N)

            if (intervals.Length <= 1)
            {
                return intervals;
            }

            var comparer = Comparer<int>.Default;
            Array.Sort(intervals, (x, y) => comparer.Compare(x[0], y[0]));

            var result = new List<int[]>();

            var i = 0;
            while (i < intervals.Length)
            {
                var k = i;
                var j = i + 1;

                var max = intervals[i][1];

                while (j < intervals.Length && intervals[j][0] <= max)
                {
                    max = Math.Max(max, intervals[j][1]);
                    i++;
                    j++;
                }

                var merged = new int[2];

                if (k == i)
                {
                    // SS: no merge
                    merged[0] = intervals[k][0];
                    merged[1] = max;
                }
                else
                {
                    merged[0] = intervals[k][0];
                    merged[1] = max;
                }

                result.Add(merged);
                i = j;
            }

            return result.ToArray();
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] intervals = {new[] {1, 2}, new[] {2, 6}, new[] {3, 4}};

                // Act
                var merged = new Solution().Merge(intervals);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {1, 6}}, merged);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] intervals = {new[] {1, 3}, new[] {2, 6}, new[] {8, 10}, new[] {15, 18}};

                // Act
                var merged = new Solution().Merge(intervals);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {1, 6}, new[] {8, 10}, new[] {15, 18}}, merged);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[][] intervals = {new[] {2, 3}, new[] {4, 5}, new[] {6, 7}, new[] {8, 9}, new[] {1, 10}};

                // Act
                var merged = new Solution().Merge(intervals);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {1, 10}}, merged);
            }
        }
    }
}