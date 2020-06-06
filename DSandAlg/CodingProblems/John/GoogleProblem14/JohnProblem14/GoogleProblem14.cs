#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace JohnProblem14
{
    /// <summary>
    /// Given a bench, place people on the bench such that their distance is max
    /// compared to any other person on the bench already.
    /// </summary>
    public class GoogleProblem14
    {
        private readonly bool[] _input;
        private PriorityQueue<(int start, int end)> _intervals = PriorityQueue<(int start, int end)>.CreateMaxPriorityQueue();

        public GoogleProblem14(bool[] input)
        {
            _input = input;
            CreateIntervals(input);
        }

        public static int FindOneTimeIndex(bool[] input)
        {
            // SS: extract intervals
            var bestPriority = 0;
            var index = -1;

            // SS: O(n)
            var i = 0;
            while (i < input.Length)
            {
                if (input[i] == false)
                {
                    var j = i + 1;
                    while (j < input.Length && input[j] == false)
                    {
                        j++;
                    }

                    int priority;

                    // SS: special case when interval start at 0 or ends at end
                    if (j == input.Length)
                    {
                        priority = j - i;
                        if (priority > bestPriority)
                        {
                            bestPriority = priority;
                            index = j - 1;
                        }
                    }
                    else if (i == 0)
                    {
                        priority = j;
                        if (priority > bestPriority)
                        {
                            bestPriority = priority;
                            index = i;
                        }
                    }
                    else
                    {
                        priority = Math.Max(1, (j - i) / 2);
                        if (priority > bestPriority)
                        {
                            bestPriority = priority;
                            index = i + priority;
                        }
                    }

                    // SS: a true follows after this false
                    i = j + 1;
                }
                else
                {
                    i++;
                }
            }

            return index;
        }

        private void CreateIntervals(bool[] input)
        {
            // SS: O(n) interval generation, and O(n log n) for sorting intervals
            // With PQ: O(n), constructing max heap: O(n)
            var i = 0;
            while (i < input.Length)
            {
                if (input[i] == false)
                {
                    var j = i + 1;
                    while (j < input.Length && input[j] == false)
                    {
                        j++;
                    }

                    int priority;

                    // SS: special case when interval start at 0 or ends at end
                    if (j == input.Length)
                    {
                        priority = j - i;
                    }
                    else if (i == 0)
                    {
                        priority = j;
                    }
                    else
                    {
                        priority = Math.Max(1, (j - i) / 2);
                    }

                    var interval = (i, j);

                    // SS: O(log n)
                    _intervals.Enqueue(interval, priority);

                    // SS: a true follows after this false
                    i = j + 1;
                }
                else
                {
                    i++;
                }
            }
        }

        public int FindIndex()
        {
            if (_intervals.IsEmpty)
            {
                // SS: bench is full, no more people can sit
                return -1;
            }

            int position;

            // SS: find interval with highest priority
            // With PQ: O(log n)
            var (priority, (start, end)) = _intervals.Dequeue();

            if (start + 1 == end)
            {
                // SS: there is only space for one person
                position = start;

                Console.WriteLine($"{position}");

                return position;
            }

            // SS: split interval and insert
            if (start == 0)
            {
                // SS: place person on first seat
                position = start;
                _intervals.Enqueue((1, end), priority - 1);
            }
            else if (end == _input.Length)
            {
                // SS: place person on last seat
                position = end - 1;
                _intervals.Enqueue((start, end - 1), priority - 1);
            }
            else
            {
                position = (start + end) / 2;

                // SS: 1st half of the interval
                if (position > start)
                {
                    var p = Math.Max(1, (position - start) / 2);
                    _intervals.Enqueue((start, position), p);
                }

                // SS: 2nd half of the interval
                if (position + 1 < end)
                {
                    var p = Math.Max(1, (end - position) / 2);
                    _intervals.Enqueue((position + 1, end), p);
                }
            }

            Console.WriteLine($"{position}");

            return position;
        }
    }

    [TestFixture]
    public class GoogleProblem14Test
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var input = new[]
                {true, false, false, true, false, false, false, false, true, false, true, false, false, false, false};
            var problem = new GoogleProblem14(input);

            // Act
            problem.FindIndex();
            problem.FindIndex();
            var index = problem.FindIndex();

            // Assert
            Assert.AreEqual(6, index);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var input = new[]
                {true, false, false, true, false, false, false, false, true, false, true, false, false, false, false};
            var problem = new GoogleProblem14(input);

            // Act
            for (var i = 0; i < 10; i++)
            {
                problem.FindIndex();
            }

            // Assert
            Assert.True(problem.FindIndex() > -1);
            Assert.True(problem.FindIndex() == -1);
        }

        [Test]
        public void TestOneTimeEnd()
        {
            // Arrange
            var input = new[]
                {true, false, false, true, false, false, false, false, true, false, true, false, false, false, false};

            // Act
            var index = GoogleProblem14.FindOneTimeIndex(input);

            // Assert
            Assert.AreEqual(14, index);
        }

        [Test]
        public void TestOneTimeNotStartNotEnd()
        {
            // Arrange
            var input = new[]
                {false, true, false, true, false, false, false, false, true, false, true, false, false, true, false};

            // Act
            var index = GoogleProblem14.FindOneTimeIndex(input);

            // Assert
            Assert.AreEqual(6, index);
        }

        [Test]
        public void TestOneTimeStart()
        {
            // Arrange
            var input = new[]
                {false, false, false, true, false, false, false, false, true, false, true, false, false, true, false};

            // Act
            var index = GoogleProblem14.FindOneTimeIndex(input);

            // Assert
            Assert.AreEqual(0, index);
        }
    }
}