using System;
using System.Collections.Generic;
using NUnit.Framework;

// Problem: 621. Task Scheduler
// URL: https://leetcode.com/problems/task-scheduler/

namespace LeetCode
{
    public class Solution
    {
        public int LeastInterval(char[] tasks, int n)
        {
            // return LeastInterval1(tasks, n);
            // return LeastInterval2(tasks, n);
            return LeastInterval3(tasks, n);
        }

        private int LeastInterval3(char[] tasks, int n)
        {
            // SS: runtime complexity: O(t log t)
            
            // SS: count number of tasks
            int[] taskBuckets = new int[26];
            for (int i = 0; i < tasks.Length; i++)
            {
                var task = tasks[i];
                int taskIdx = task - 'A';
                taskBuckets[taskIdx]++;
            }

            // SS: sort in descending order
            Array.Sort(taskBuckets, Comparer<int>.Create((i1, i2) => i2 - i1));

            // SS: number of gaps
            int nGaps = 0;
            if (n > 0)
            {
                nGaps = taskBuckets[0] - 1;
            }

            // SS: positions to fill with tasks
            int nGapCount = nGaps * n;

            int j = 1;
            while (j < 26 && taskBuckets[j] > 0)
            {
                // SS: this task can at most fill nGaps slots
                var nTasks = taskBuckets[j];
                nGapCount -= Math.Min(nTasks, nGaps);
                j++;
            }

            // SS: left-over is either 0 or positive
            nGapCount = Math.Max(0, nGapCount);
            
            return nGapCount + tasks.Length;
        }

        private int LeastInterval1(char[] tasks, int n)
        {
            // SS: runtime complexity: O(t * n log t )
            // space complexity: O(t)

            var maxHeap1 = BinaryHeap<int>.CreateMaxHeap();
            var maxHeap2 = BinaryHeap<int>.CreateMaxHeap();

            // SS: task buckets
            int[] taskBuckets = new int[26];
            for (int i = 0; i < tasks.Length; i++)
            {
                var task = tasks[i];
                int taskIdx = task - 'A';
                taskBuckets[taskIdx]++;
            }

            for (int i = 0; i < 26; i++)
            {
                var nTasks = taskBuckets[i];
                if (nTasks > 0)
                {
                    maxHeap1.Push(nTasks);
                }
            }

            int totalTimeUnits = 0;
            while (maxHeap1.IsEmpty == false)
            {
                // SS: number of tasks
                int s1 = maxHeap1.Pop();

                if (s1 > 1)
                {
                    maxHeap2.Push(s1 - 1);
                }

                int timeUnits = 1;

                int distance = n;

                while (distance > 0 && maxHeap1.Length + maxHeap2.Length > 0)
                {
                    if (maxHeap1.IsEmpty == false)
                    {
                        // SS: get next task that is not in conflict
                        int s2 = maxHeap1.Pop();
                        if (s2 > 1)
                        {
                            maxHeap2.Push(s2 - 1);
                        }
                    }

                    timeUnits++;
                    distance--;
                }

                totalTimeUnits += timeUnits;

                while (maxHeap2.IsEmpty == false)
                {
                    int t = maxHeap2.Pop();
                    maxHeap1.Push(t);
                }
            }

            return totalTimeUnits;
        }

        private int LeastInterval2(char[] tasks, int n)
        {
            // SS: runtime complexity: O(t * n log t )
            // space complexity: O(t)

            var maxHeap = BinaryHeap<int>.CreateMaxHeap();

            // SS: task buckets
            int[] taskBuckets = new int[26];
            for (int i = 0; i < tasks.Length; i++)
            {
                var task = tasks[i];
                int taskIdx = task - 'A';
                taskBuckets[taskIdx]++;
            }

            for (int i = 0; i < 26; i++)
            {
                var nTasks = taskBuckets[i];
                if (nTasks > 0)
                {
                    maxHeap.Push(nTasks);
                }
            }

            int[] bucket = new int[maxHeap.Length];

            int totalTimeUnits = 0;
            while (maxHeap.IsEmpty == false)
            {
                int bucketIdx = 0;

                // SS: number of tasks
                int s1 = maxHeap.Pop();

                if (s1 > 1)
                {
                    bucket[bucketIdx++] = s1 - 1;
                }

                int timeUnits = 1;

                int distance = n;

                while (distance > 0 && maxHeap.Length + bucketIdx > 0)
                {
                    if (maxHeap.IsEmpty == false)
                    {
                        // SS: get next task that is not in conflict
                        int s2 = maxHeap.Pop();
                        if (s2 > 1)
                        {
                            bucket[bucketIdx++] = s2 - 1;
                        }
                    }

                    timeUnits++;
                    distance--;
                }

                totalTimeUnits += timeUnits;

                while (bucketIdx > 0)
                {
                    int t = bucket[bucketIdx-- - 1];
                    maxHeap.Push(t);
                }
            }

            return totalTimeUnits;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                int timeUnits = new Solution().LeastInterval(new[] { 'A', 'A', 'A', 'B', 'B', 'B' }, 2);

                // Assert
                Assert.AreEqual(8, timeUnits);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                int timeUnits = new Solution().LeastInterval(new[] { 'A', 'A', 'A', 'B', 'B', 'B' }, 0);

                // Assert
                Assert.AreEqual(6, timeUnits);
            }

            [Test]
            public void Test3()
            {
                // Arrange

                // Act
                int timeUnits = new Solution().LeastInterval(new[] { 'A', 'A', 'A', 'A', 'A', 'A', 'B', 'C', 'D', 'E', 'F', 'G' }, 2);

                // Assert
                Assert.AreEqual(16, timeUnits);
            }

            [Test]
            public void Test4()
            {
                // Arrange

                // Act
                int timeUnits = new Solution().LeastInterval(new[] { 'A' }, 2);

                // Assert
                Assert.AreEqual(1, timeUnits);
            }

            [Test]
            public void Test5()
            {
                // Arrange

                // Act
                int timeUnits = new Solution().LeastInterval(new[] { 'A', 'B' }, 3);

                // Assert
                Assert.AreEqual(2, timeUnits);
            }

            [Test]
            public void Test6()
            {
                // Arrange

                // Act
                int timeUnits = new Solution().LeastInterval(new[] { 'A', 'A', 'B' }, 1);

                // Assert
                Assert.AreEqual(3, timeUnits);
            }

            [Test]
            public void Test7()
            {
                // Arrange

                // Act
                int timeUnits = new Solution().LeastInterval(new[] { 'A', 'A', 'B' }, 2);

                // Assert
                Assert.AreEqual(4, timeUnits);
            }

            [Test]
            public void Test8()
            {
                // Arrange

                // Act
                int timeUnits = new Solution().LeastInterval(new[] { 'A', 'A', 'A', 'B', 'B', 'B', 'C', 'C', 'C', 'D', 'D', 'E' }, 2);
                    
                // Assert
                Assert.AreEqual(12, timeUnits);
            }
            
        }
    }
}