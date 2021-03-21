#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 232. Implement Queue using Stacks
// URL: https://leetcode.com/problems/implement-queue-using-stacks/

namespace LeetCode
{
    public class Solution
    {
        public class MyQueue
        {
            private readonly Stack<int> _popStack = new Stack<int>();
            private readonly Stack<int> _pushStack = new Stack<int>();

            /**
             * Push element x to the back of queue.
             */
            public void Push(int x)
            {
                _pushStack.Push(x);
            }

            /**
             * Removes the element from in front of queue and returns that element.
             */
            public int Pop()
            {
                // SS: this is amortized time O(1), because for each element,
                // we push it into the pushStack, pop it from it and push it
                // onto the pop stack and pop it from there, hence ~ O(4) = O(1)
                if (_popStack.Any() == false)
                {
                    while (_pushStack.Any())
                    {
                        var x = _pushStack.Pop();
                        _popStack.Push(x);
                    }
                }

                return _popStack.Pop();
            }

            /**
             * Get the front element.
             */
            public int Peek()
            {
                if (_popStack.Any() == false)
                {
                    while (_pushStack.Any())
                    {
                        var x = _pushStack.Pop();
                        _popStack.Push(x);
                    }
                }

                return _popStack.Peek();
            }

            /**
             * Returns whether the queue is empty.
             */
            public bool Empty()
            {
                return _pushStack.Any() == false && _popStack.Any() == false;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var myQueue = new MyQueue();
                myQueue.Push(1);
                myQueue.Push(2);

                // Act
                // Assert
                Assert.AreEqual(1, myQueue.Peek());
                Assert.AreEqual(1, myQueue.Pop());
                Assert.False(myQueue.Empty());
            }
        }
    }
}