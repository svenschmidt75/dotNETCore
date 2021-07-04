using System;
using System.Collections.Generic;
using NUnit.Framework;

// Problem: 435. Non-overlapping Intervals
// URL: https://leetcode.com/problems/non-overlapping-intervals/

namespace LeetCode
{
    public class Solution
    {
        public int EraseOverlapIntervals(int[][] intervals)
        {
            // return EraseOverlapIntervals1(intervals);
            // return EraseOverlapIntervals2(intervals);
            // return EraseOverlapIntervals3(intervals);
            // return EraseOverlapIntervals4(intervals);
            return EraseOverlapIntervals5(intervals);
        }

        private int EraseOverlapIntervals5(int[][] intervals)
        {
            // SS: Bottom-Up Dynamic Programming
            // runtime complexity: O(n^2)
            // space complexity: O(n)
            
            // SS: O(n log n)
            Array.Sort(intervals, (a1, a2) => a1[0].CompareTo(a2[0]));

            int[] dp = new int[intervals.Length];

            // SS: boundary condition
            // SS: dp[intervals.Length - 1] = 0, as one interval does not intersect any other one
            
            for (int i = intervals.Length - 2; i >= 0 ; i--)
            {
                int[] i1 = intervals[i];

                // SS: include the ith interval
                // skip while interval j is intersecting interval i 
                int skipped = 0;
                int j = i + 1;
                while (j < intervals.Length)
                {
                    int[] i2 = intervals[j];
                    if (i2[0] < i1[1])
                    {
                        // SS intervals intersect, skip
                        skipped++;
                        j++;
                    }
                    else
                    {
                        break;
                    }
                }

                // SS: min skips when interval i is included
                int m1 = skipped;
                if (j < intervals.Length)
                {
                    m1 += dp[j];
                }

                // SS: min skips when interval i is not included
                int m2 = 1 + dp[i + 1];

                int m = Math.Min(m1, m2);
                dp[i] = m;
            }

            return dp[0];
        }

        private int EraseOverlapIntervals4(int[][] intervals)
        {
            // SS: times out...
            
            // SS: O(n log n)
            Array.Sort(intervals, (a1, a2) => a1[0].CompareTo(a2[0]));

            var dp = new Dictionary<int, int>();
            
            int DFS(int idx)
            {
                // SS: base case
                if (idx + 1 >= intervals.Length)
                {
                    return 0;
                }

                if (dp.ContainsKey(idx))
                {
                    return dp[idx];
                }
                
                int min = int.MaxValue;
                int skipped = 0;

                for (int i = idx + 1; i < intervals.Length; i++)
                {
                    // SS: no overlap?
                    if (intervals[i][0] >= intervals[idx][1])
                    {
                        int m = DFS(i);
                        min = Math.Min(min, m + skipped);
                    }

                    skipped++;
                }

                var result = Math.Min(min, skipped);

                dp[idx] = result;
                
                return result;
            }

            int min = int.MaxValue;
            for (int i = 0; i < intervals.Length; i++)
            {
                if (dp.ContainsKey(i))
                {
                    continue;
                }
                
                int m = DFS(i);
                min = Math.Min(min, i + m);

                dp[i] = min;
            }

            return min;
        }

        private int EraseOverlapIntervals3(int[][] intervals)
        {
            // SS: O(n log n)
            Array.Sort(intervals, (a1, a2) => a1[0].CompareTo(a2[0]));

            int DFS(int idx)
            {
                // SS: base case
                if (idx + 1 >= intervals.Length)
                {
                    return 0;
                }

                int min = int.MaxValue;
                int skipped = 0;

                for (int i = idx + 1; i < intervals.Length; i++)
                {
                    // SS: no overlap?
                    if (intervals[i][0] >= intervals[idx][1])
                    {
                        int m = DFS(i);
                        min = Math.Min(min, m + skipped);
                    }

                    skipped++;
                }

                return Math.Min(min, skipped);
            }

            int min = int.MaxValue;
            for (int i = 0; i < intervals.Length; i++)
            {
                int m = DFS(i);
                min = Math.Min(min, i + m);
            }

            return min;
        }

        private int EraseOverlapIntervals2(int[][] intervals)
        {
            // SS: Top-Down Dynamic Programming
            // runtime complexity: O(n log n)

            // SS: O(n log n)
            Array.Sort(intervals, (a1, a2) => a1[0].CompareTo(a2[0]));

            var dp = new Dictionary<(int idx, int end), int>();

            int DFS(int idx, int end)
            {
                // SS: base condition
                if (idx == intervals.Length)
                {
                    // SS: no more intervals to skip
                    return 0;
                }

                // SS: either skip or do not skip

                if (dp.ContainsKey((idx, end)))
                {
                    return dp[(idx, end)];
                }

                // SS: skip this interval
                int skip = 1 + DFS(idx + 1, end);

                // SS: do not skip this interval
                int noSkip = int.MaxValue;
                int[] interval = intervals[idx];
                if (interval[0] >= end)
                {
                    // SS: no overlap.
                    // If there is overlap, this branch does not apply...
                    noSkip = DFS(idx + 1, interval[1]);
                }

                int m = Math.Min(skip, noSkip);

                Console.WriteLine($"{idx} {end}");
                dp[(idx, end)] = m;

                return m;
            }

            return DFS(0, int.MinValue);
        }

        private int EraseOverlapIntervals1(int[][] intervals)
        {
            // SS: DFS and backtracking
            // runtime complexity: O(2^n)

            // SS: O(n log n)
            Array.Sort(intervals, (a1, a2) => a1[0].CompareTo(a2[0]));

            int DFS(int idx, int end)
            {
                // SS: base condition
                if (idx == intervals.Length)
                {
                    // SS: no more intervals to skip
                    return 0;
                }

                // SS: either skip or do not skip

                // SS: skip this interval
                int skip = 1 + DFS(idx + 1, end);

                // SS: do not skip this interval
                int noSkip = int.MaxValue;
                int[] interval = intervals[idx];
                if (interval[0] >= end)
                {
                    // SS: no overlap.
                    // If there is overlap, this branch does not apply...
                    noSkip = DFS(idx + 1, interval[1]);
                }

                int m = Math.Min(skip, noSkip);

                Console.WriteLine($"{idx} {end}");

                return m;
            }

            return DFS(0, int.MinValue);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] intervals = { new[] { 1, 2 }, new[] { 2, 3 }, new[] { 3, 4 }, new[] { 1, 3 } };

                // Act
                int result = new Solution().EraseOverlapIntervals(intervals);

                // Assert
                Assert.AreEqual(1, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] intervals = { new[] { 1, 2 }, new[] { 1, 2 }, new[] { 1, 2 } };

                // Act
                int result = new Solution().EraseOverlapIntervals(intervals);

                // Assert
                Assert.AreEqual(2, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[][] intervals = { new[] { 1, 2 }, new[] { 2, 3 } };

                // Act
                int result = new Solution().EraseOverlapIntervals(intervals);

                // Assert
                Assert.AreEqual(0, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[][] intervals = { new[] { 3, 4 }, new[] { 1, 3 } };

                // Act
                int result = new Solution().EraseOverlapIntervals(intervals);

                // Assert
                Assert.AreEqual(0, result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[][] intervals = { new[] { 2, 3 }, new[] { 3, 4 }, new[] { 1, 3 } };

                // Act
                int result = new Solution().EraseOverlapIntervals(intervals);

                // Assert
                Assert.AreEqual(1, result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[][] intervals = { new[] { 2, 3 } };

                // Act
                int result = new Solution().EraseOverlapIntervals(intervals);

                // Assert
                Assert.AreEqual(0, result);
            }
            
        }
    }
}