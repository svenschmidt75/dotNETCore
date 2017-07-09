using System;
using System.Collections.Generic;
using System.Linq;

namespace Queue
{
    public class Queue<T>
    {
        // SS: Use 2 stacks later, one for Enqueue, one for Dequeue?
        private List<T> _queue = new List<T>();

        public void Enqueue(T value)
        {
            // add to front
            _queue = new[] {value}.Concat(_queue).ToList();
        }

        public T Dequeue()
        {
            var length = _queue.Count;
            if (length == 0)
                throw new InvalidOperationException("Dequeue: Empty queue");
            var item = _queue[length - 1];
            _queue.RemoveAt(length - 1);
            return item;
        }
    }
}