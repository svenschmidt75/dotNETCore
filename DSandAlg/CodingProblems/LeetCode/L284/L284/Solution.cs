#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 284. Peeking Iterator
// URL: https://leetcode.com/problems/peeking-iterator/

namespace LeetCode
{
    internal class PeekingIterator
    {
        private readonly IEnumerator<int> _iterator;
        private bool _isEnd = true;

        // iterators refers to the first element of the array.
        public PeekingIterator(IEnumerator<int> iterator)
        {
            _iterator = iterator;
        }

        // Returns the next element in the iteration without advancing the iterator.
        public int Peek()
        {
            return _iterator.Current;
        }

        // Returns the next element in the iteration and advances the iterator.
        public int Next()
        {
            var current = _iterator.Current;
            _isEnd = _iterator.MoveNext();
            return current;
        }

        // Returns false if the iterator is refering to the end of the array of true otherwise.
        public bool HasNext()
        {
            return _isEnd;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act

                // Assert
            }
        }
    }
}