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

            if (intervals[i][0] <= newInterval[1] && intervals[i][1] >= newInterval[1])
            {
                results.Add(new[] {a, intervals[i][1]});
                i++;
            }
            else
            {
                results.Add(new[] {a, newInterval[1]});
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