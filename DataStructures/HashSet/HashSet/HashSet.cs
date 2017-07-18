using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HashSet
{
    public class HashSet<T> : ICollection<T>
    {
        public delegate int Hash(T value);

        private const int Size = 1876;
        private readonly Hash _hash;
        private readonly LinkedList<T>[] _data = new LinkedList<T>[Size];

        public HashSet() : this(value => value.GetHashCode())
        {
        }

        public HashSet(Hash hash)
        {
            _hash = hash;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _data.Where(x => x != null).SelectMany(bucket => bucket).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int GetHash(T item)
        {
            var index = _hash(item) % Size;
            if (index < 0 || index >= Size)
            {
                throw new InvalidOperationException("Hash index too large");
            }
            return index;
        }

        public void Add(T item)
        {
            var index = GetHash(item);
            if (Exists(item, index))
                return;
            var bucket = new LinkedList<T>();
            bucket.AddFirst(item);
            _data[index] = bucket;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            return Exists(item, GetHash(item));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int Count => _data.Where(x => x != null).SelectMany(bucket => bucket).Count();
        public bool IsReadOnly { get; }

        private bool Exists(T item, int index)
        {
            var bucket = _data[index];
            return bucket != null && bucket.Any(x => x.Equals(item));
        }
    }
}