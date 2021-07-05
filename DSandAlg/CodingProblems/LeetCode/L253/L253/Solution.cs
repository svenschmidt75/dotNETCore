using System;
using NUnit.Framework;

// Problem: Meeting Rooms II
// URL: https://leetcode.com/problems/meeting-rooms-ii/

namespace LeetCode
{
    public class Solution
    {
        public int MeetingRooms(int[][] intervals)
        {
            // return MeetingRooms1(intervals);
            return MeetingRooms2(intervals);
        }

        private int MeetingRooms2(int[][] intervals)
        {
            // SS: sort start and end times together with whether it is open
            // or close interval. Then, check for overlaps
            // SS: runtime complexity: O(n log n) due to sorting
            // space complexity: O(n)

            (int type, int value)[] arr = new (int type, int value)[intervals.Length * 2];
            int idx = 0;
            for (int i = 0; i < intervals.Length; i++)
            {
                arr[idx++] = (1, intervals[i][0]);
                arr[idx++] = (-1, intervals[i][1]);
            }

            // SS: sort
            Array.Sort(arr, (i1, i2) => i1.value.CompareTo(i2.value));

            int minMeetingRooms = 0;
            int overlapCount = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                var (type, _) = arr[i];
                overlapCount += type;
                minMeetingRooms = Math.Max(minMeetingRooms, overlapCount);
            }

            return minMeetingRooms;
        }

        private int MeetingRooms1(int[][] intervals)
        {
            // SS: Use min heap with end interval value as key
            // runtime complexity: O(n log n) (push each interval into the min heap)
            // space complexity: O(n)

            if (intervals.Length == 0)
            {
                return 0;
            }

            // SS: sort by start interval time
            // SS: O(n log n)
            Array.Sort(intervals, (i1, i2) => i1[0].CompareTo(i2[0]));

            var minHeap = BinaryHeap<int>.CreateHeap((i1, i2) =>
            {
                var interval1 = intervals[i1];
                var interval2 = intervals[i2];
                return interval1[1].CompareTo(interval2[1]) > 0;
            });

            int minMeetingRooms = 1;

            int idx = 0;
            while (idx < intervals.Length)
            {
                if (minHeap.IsEmpty)
                {
                    minHeap.Push(idx);
                    idx++;
                }
                else
                {
                    int heapIntervalIdx = minHeap.Peek();
                    var heapInterval = intervals[heapIntervalIdx];

                    var interval = intervals[idx];

                    // SS: how to handle end_{i} == start_{i + 1}?
                    if (heapInterval[1] < interval[0])
                    {
                        // SS: previous interval stops before this starts, so remove
                        // as no conflict
                        minHeap.Pop();
                    }
                    else
                    {
                        // SS: intervals overlap, so conflict
                        minHeap.Push(idx);
                        idx++;
                    }
                }

                minMeetingRooms = Math.Max(minMeetingRooms, minHeap.Length);
            }

            return minMeetingRooms;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] intervals = { new[] { 0, 30 }, new[] { 5, 10 }, new[] { 15, 20 } };

                // Act
                int result = new Solution().MeetingRooms(intervals);

                // Assert
                Assert.AreEqual(2, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] intervals = { new[] { 7, 10 }, new[] { 2, 4 } };

                // Act
                int result = new Solution().MeetingRooms(intervals);

                // Assert
                Assert.AreEqual(1, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[][] intervals = { new[] { 2, 7 }, new[] { 12, 20 }, new[] { 8, 15 }, new[] { 5, 10 }, new[] { 0, 30 } };

                // Act
                int result = new Solution().MeetingRooms(intervals);

                // Assert
                Assert.AreEqual(3, result);
            }
        }
    }
}