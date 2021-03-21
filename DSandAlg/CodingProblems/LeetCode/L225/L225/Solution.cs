#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 225. Implement Stack using Queues
// URL: https://leetcode.com/problems/implement-stack-using-queues/

namespace LeetCode
{
    public class Solution
    {
        public class MyStackAmortized
        {
            private const int N = 10;
            private readonly List<Queue<int>> _queues = new List<Queue<int>>();

            public int Pop()
            {
                // SS: Each queue contains at most N elements, so
                // runtime operation is O(10), or constant time
                // per element

                var q = _queues[^1];

                var prev = 0;
                var q2 = new Queue<int>();
                while (q.Any())
                {
                    var x = q.Dequeue();
                    prev = x;

                    if (q.Count >= 1)
                    {
                        q2.Enqueue(x);
                    }
                }

                // SS: remove queue if empty
                if (q2.Count == 0)
                {
                    _queues.RemoveAt(_queues.Count - 1);
                }
                else
                {
                    _queues[^1] = q2;
                }

                return prev;
            }

            public void Push(int x)
            {
                if (_queues.Any() == false)
                {
                    _queues.Add(new Queue<int>());
                }

                var q = _queues[^1];
                if (q.Count == N)
                {
                    // SS: last queue full, create new one
                    q = new Queue<int>();
                    _queues.Add(q);
                }

                q.Enqueue(x);
            }

            public int Top()
            {
                // SS: Each queue contains at most N elements, so
                // runtime operation is O(10), or constant time
                // per element

                var q = _queues[^1];

                var prev = 0;
                var q2 = new Queue<int>();
                while (q.Any())
                {
                    var x = q.Dequeue();
                    prev = x;
                    q2.Enqueue(x);
                }

                _queues[^1] = q2;

                return prev;
            }

            public bool Empty()
            {
                return _queues.Any() == false;
            }
        }

        public class MyStack
        {
            private Queue<int> _q1 = new Queue<int>();

            /**
             * Push element x onto stack.
             */
            public void Push(int x)
            {
                _q1.Enqueue(x);
            }

            /**
             * Removes the element on top of the stack and returns that element.
             */
            public int Pop()
            {
                var prev = 0;
                var q2 = new Queue<int>();
                while (_q1.Any())
                {
                    var x = _q1.Dequeue();
                    prev = x;

                    if (_q1.Count >= 1)
                    {
                        q2.Enqueue(x);
                    }
                }

                _q1 = q2;

                return prev;
            }

            /**
             * Get the top element.
             */
            public int Top()
            {
                var q2 = new Queue<int>();
                var prev = 0;
                while (_q1.Any())
                {
                    var x = _q1.Dequeue();
                    prev = x;
                    q2.Enqueue(x);
                }

                _q1 = q2;

                return prev;
            }

            /**
             * Returns whether the stack is empty.
             */
            public bool Empty()
            {
                return _q1.Any() == false;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var stack = new MyStackAmortized();

                // Act
                stack.Push(1);
                stack.Push(2);

                // Assert
                Assert.AreEqual(2, stack.Top());
                Assert.AreEqual(2, stack.Pop());
                Assert.False(stack.Empty());
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var stack = new MyStackAmortized();

                // Act
                // Assert
                stack.Push(1);
                stack.Push(2);
                stack.Push(3);
                Assert.AreEqual(3, stack.Top());
                stack.Push(4);
                Assert.AreEqual(4, stack.Pop());
                stack.Pop();
                stack.Pop();
                stack.Pop();

                stack.Push(8);
                Assert.AreEqual(8, stack.Pop());

                Assert.True(stack.Empty());
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var stack = new MyStackAmortized();

                // Act
                // Assert
                for (var i = 0; i < 100; i++)
                {
                    stack.Push(i);
                }

                Assert.AreEqual(99, stack.Top());

                for (var i = 0; i < 100; i++)
                {
                    stack.Pop();
                }

                Assert.True(stack.Empty());
            }
        }
    }
}