#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 155. Min Stack
// URL: https://leetcode.com/problems/min-stack/

namespace LeetCode
{
    public class MinStack
    {
        private readonly Stack<int> _dataStack = new Stack<int>();
        private int _minElement = int.MaxValue;
        private readonly Stack<int> _minStack = new Stack<int>();

        public void Push(int x)
        {
            _minElement = Math.Min(_minElement, x);
            _dataStack.Push(x);
            _minStack.Push(_minElement);
        }

        public void Pop()
        {
            _minStack.Pop();
            _dataStack.Pop();

            if (_minStack.Any())
            {
                _minElement = _minStack.Peek();
            }
            else
            {
                _minElement = int.MaxValue;
            }
        }

        public int Top()
        {
            return _dataStack.Peek();
        }

        public int GetMin()
        {
            return _minStack.Peek();
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var minStack = new MinStack();
                minStack.Push(-2);
                minStack.Push(0);
                minStack.Push(-3);

                // Act
                // Assert
                Assert.AreEqual(-3, minStack.GetMin());
                minStack.Pop();
                Assert.AreEqual(0, minStack.Top());
                Assert.AreEqual(-2, minStack.GetMin());

                minStack.Pop();
                Assert.AreEqual(-2, minStack.GetMin());

                minStack.Pop();
                minStack.Push(1);
                Assert.AreEqual(1, minStack.GetMin());
            }
        }
    }
}