using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashSet
{
    public class HashSet<T> : ICollection<T>
    {
        public delegate uint Hash(T value);

        private const uint Size = 1876;
        private readonly LinkedList<T>[] _data = new LinkedList<T>[Size];
        private readonly Hash _hash;

        public HashSet() : this(value => (uint) value.GetHashCode())
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
            foreach (var bucket in _data)
                bucket?.Clear();
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
            var index = GetHash(item);
            if (Exists(item, index) == false)
                return false;
            var bucket = _data[index];
            return bucket != null && bucket.Remove(item);
        }

        public int Count => _data.Where(x => x != null).SelectMany(bucket => bucket).Count();
        public bool IsReadOnly { get; }

        private uint GetHash(T item)
        {
            var index = _hash(item) % Size;
            if (index >= Size)
                throw new InvalidOperationException("Hash index too large");
            return index;
        }

        private bool Exists(T item, uint index)
        {
            var bucket = _data[index];
            return bucket != null && bucket.Any(x => x.Equals(item));
        }
    }
}