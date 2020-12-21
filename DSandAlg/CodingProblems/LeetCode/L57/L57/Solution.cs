#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 57. Insert Interval
// URL: https://leetcode.com/problems/insert-interval/

namespace LeetCode
{
    public class Solution
    {
        public int[][] Insert(int[][] intervals, int[] newInterval)
        {
            return InsertEventBased(intervals, newInterval);
        }

        private static int Compare((int a, int b) x, (int a, int b) y)
        {
            if (x.a != y.a)
            {
                return x.a.CompareTo(y.a);
            }

            // SS: is an interval starts where another ends, we return the opening part
            // first, because we want to merge them later.
            return (-x.b).CompareTo(-y.b);
        }

        public int[][] InsertEventBased(int[][] intervals, int[] newInterval)
        {
            // SS: event-based approach (generalizes easily to more than one interval to insert)
            // We map the beginning of an interval to 1, the end to -1. After sorting, we generate
            // the new intervals. Runtime complexity is O(N log N).
            // Larry's solution, https://www.youtube.com/watch?v=C5gsLIBktwA

            var events = new List<(int value, int offset)>();
            foreach (var interval in intervals)
            {
                // SS: opening interval value is event +1
                events.Add((interval[0], 1));

                // SS: closing interval value is event -1
                events.Add((interval[1], -1));
            }

            events.Add((newInterval[0], 1));
            events.Add((newInterval[1], -1));

            // SS: O(N log N)
            events.Sort(Compare);

            // SS: reconstruct the intervals
            var results = new List<int[]>();
            var v = 0;
            var start = -1;

            foreach (var item in events)
            {
                v += item.offset;

                if (v == 1 && item.offset == 1)
                {
                    start = item.value;
                }
                else if (v == 0 && item.offset == -1)
                {
                    results.Add(new[] {start, item.value});
                }
            }

            return results.ToArray();
        }

        public int[][] InsertLinear(int[][] intervals, int[] newInterval)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(N)

            var results = new List<int[]>();

            var i = 0;
            while (i < intervals.Length && intervals[i][1] < newInterval[0])
            {
                results.Add(intervals[i]);
                i++;
            }

            if (i == intervals.Length)
            {
                results.Add(newInterval);
                return results.ToArray();
            }

            var a = Math.Min(intervals[i][0], newInterval[0]);
            while (i < intervals.Length && intervals[i][1] <= newInterval[1])
            {
                i++;
            }

            if (i == intervals.Length)
            {
                results.Add(new[] {a, newInterval[1]});
                return results.ToArray();
            }

            if (newInterval[1] < intervals[i][0])
            {
                results.Add(new[] {a, newInterval[1]});
            }
            else
            {
                results.Add(new[] {a, intervals[i][1]});
                i++;
            }

            while (i < intervals.Length)
            {
                results.Add(intervals[i]);
                i++;
            }

            return results.ToArray();
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] intervals = {new[] {0, 10}, new[] {20, 30}, new[] {40, 50}};
                int[] newInterval = {12, 18};

                // Act
                var results = new Solution().Insert(intervals, newInterval);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {0, 10}, new[] {12, 18}, new[] {20, 30}, new[] {40, 50}}, results);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] intervals = {new[] {0, 10}, new[] {20, 30}, new[] {40, 50}};
                int[] newInterval = {10, 20};

                // Act
                var results = new Solution().Insert(intervals, newInterval);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {0, 30}, new[] {40, 50}}, results);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[][] intervals = {new[] {0, 10}, new[] {20, 30}, new[] {40, 50}};
                int[] newInterval = {-10, 11};

                // Act
                var results = new Solution().Insert(intervals, newInterval);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {-10, 11}, new[] {20, 30}, new[] {40, 50}}, results);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[][] intervals = {new[] {0, 10}, new[] {20, 30}, new[] {40, 50}};
                int[] newInterval = {-10, 100};

                // Act
                var results = new Solution().Insert(intervals, newInterval);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {-10, 100}}, results);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[][] intervals = {new[] {0, 10}, new[] {20, 30}, new[] {40, 50}};
                int[] newInterval = {90, 100};

                // Act
                var results = new Solution().Insert(intervals, newInterval);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {0, 10}, new[] {20, 30}, new[] {40, 50}, new[] {90, 100}}, results);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var intervals = new int[0][];
                int[] newInterval = {90, 100};

                // Act
                var results = new Solution().Insert(intervals, newInterval);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {90, 100}}, results);
            }
        }
    }
}