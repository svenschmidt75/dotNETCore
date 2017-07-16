using System;
using System.Collections;
using System.Collections.Generic;

namespace Set
{
    public class Set<T> : ICollection<T>
    {
        // SS: inefficient implementation to start with
        private readonly List<T> _data = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (Exists(item))
                return;
            _data.Add(item);
        }

        public void Clear()
        {
            _data.Clear();
        }

        public bool Contains(T item)
        {
            return Exists(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            return Exists(item) && _data.Remove(item);
        }

        public int Count => _data.Count;

        public bool IsReadOnly => false;

        private bool Exists(T item)
        {
            return _data.IndexOf(item) != -1;
        }
    }
}