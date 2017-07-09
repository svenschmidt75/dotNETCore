using System;
using System.Collections.Generic;

namespace Stack
{
    public class Stack<T>
    {
        private readonly List<T> _stack = new List<T>();

        public void Push(T value)
        {
            _stack.Add(value);
        }

        public T Pop()
        {
            var length = _stack.Count;
            if (length == 0)
            {
                throw new InvalidOperationException("Pop: Stack empty");
            }
            var lastItem = _stack[length - 1];
            _stack.RemoveAt(length - 1);
            return lastItem;
        }

    }
}