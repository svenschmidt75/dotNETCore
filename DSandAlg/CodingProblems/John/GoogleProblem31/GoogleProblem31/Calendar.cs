#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace GoogleProblem31
{
    public class Calendar
    {
        private readonly IntervalSearchTree _intervalSearchTree;

        public Calendar(List<(int start, int end)> events)
        {
            // SS: preprocessing stage
            // find overlaps of existing events
            // precondition: no triple-overlaps
            var a = new (int eventIdx, int idx, int value)[events.Count * 2];

            // SS: O(n)
            for (var i = 0; i < events.Count; i++)
            {
                var (start, end) = events[i];
                a[2 * i] = (i, 0, start);
                a[2 * i + 1] = (i, 1, end);
            }

            // SS: O(n log n)
            Array.Sort(a, new EventComparer());

            // SS: extract overlaps
            var overlaps = new List<(int start, int end)>();
            for (var i = 1; i < a.Length; i++)
            {
                var prev = a[i - 1];
                var current = a[i];
                if (prev.idx != current.idx && prev.eventIdx > current.eventIdx)
                {
                    // SS: overlap
                    overlaps.Add((prev.value, current.value));
                }
            }

            // SS: insert overlap intervals into interval tree
            // construction of interval tree: O(n log n)
            _intervalSearchTree = new IntervalSearchTree();
            for (var i = 0; i < overlaps.Count; i++)
            {
                var (start, end) = overlaps[i];
                _intervalSearchTree.Insert(start, end);
            }
        }

        public bool IsTripleBooking((int start, int end) evnt)
        {
            // SS: O(log n) avg. (depending on how well the interval search tree is balanced...)
            return _intervalSearchTree.Overlap(evnt.start, evnt.end);
        }

        public class EventComparer : IComparer<(int, int, int)>
        {
            public int Compare((int, int, int) x, (int, int, int) y)
            {
                return x.Item3.CompareTo(y.Item3);
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var appointments = new List<(int, int)>
                {
                    (0, 5)
                    , (3, 10)
                };
                var calendar = new Calendar(appointments);

                // Act
                var result = calendar.IsTripleBooking((3, 4));

                // Assert
                Assert.IsTrue(result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var appointments = new List<(int, int)>
                {
                    (0, 5)
                    , (3, 10)
                };
                var calendar = new Calendar(appointments);

                // Act
                var result = calendar.IsTripleBooking((0, 2));

                // Assert
                Assert.IsFalse(result);
            }
        }
    }
}