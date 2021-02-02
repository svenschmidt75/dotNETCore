#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 146. LRU Cache
// URL: https://leetcode.com/problems/lru-cache/

namespace LeetCode
{
    public class Node
    {
        public int Value { get; set; }
        public int Key { get; set; }
        public Node Prev { get; set; }
        public Node Next { get; set; }
    }

    public class LRUCache
    {
        private readonly int _capacity;

        private readonly Dictionary<int, Node> _hashMap = new Dictionary<int, Node>();

        // SS: head points to most recently used element
        // The head node is actually a sentinel node, i.e.
        // the "real" head is head.next!
        private readonly Node _head = new Node();

        // SS: tail points to least recently used element
        // The tail node is actually a sentinel node, i.e.
        // the "real" tail is tail.prev!
        private readonly Node _tail = new Node();
        private int _count;

        public LRUCache(int capacity)
        {
            _capacity = capacity;

            _head.Next = _tail;
            _tail.Prev = _head;
        }

        public int Get(int key)
        {
            // SS: O(1) runtime complexity
            if (_hashMap.TryGetValue(key, out var node))
            {
                Update(key);
                return node.Value;
            }

            return -1;
        }

        private void Update(int key)
        {
            // SS: move node with key to head

            var node = _hashMap[key];
            if (_head.Next == node)
            {
                return;
            }

            // SS: disconnect node from doubly-linked list
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;

            // SS: make node the head (head.next)
            node.Prev = _head;
            node.Next = _head.Next;

            _head.Next.Prev = node;
            _head.Next = node;
        }

        private void Evict()
        {
            // SS: evict least recently used node from cache
            var node = _tail.Prev;
            _hashMap.Remove(node.Key);

            // SS: make previous node the tail
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;

            _count--;
        }

        public void Put(int key, int value)
        {
            // SS: O(1) runtime complexity

            // SS: is the element already in the cache?
            if (_hashMap.TryGetValue(key, out var node))
            {
                Update(key);
                node.Value = value;
            }
            else
            {
                // SS: element is not in cache

                // SS: do we need to evict?
                if (_count == _capacity)
                {
                    Evict();
                }

                // SS: add element to head of cache
                var n = new Node
                {
                    Key = key
                    , Value = value
                    , Next = _head.Next
                    , Prev = _head
                };
                _hashMap[key] = n;

                _head.Next.Prev = n;
                _head.Next = n;

                _count++;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var lru = new LRUCache(4);
                lru.Put(1, 1);
                lru.Put(2, 2);
                lru.Put(3, 3);
                lru.Put(4, 4);

                // Act
                lru.Put(5, 5);

                // Assert
                Assert.AreEqual(-1, lru.Get(1));
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var lru = new LRUCache(2);
                lru.Put(1, 1);
                lru.Put(2, 2);

                Assert.AreEqual(1, lru.Get(1));

                lru.Put(3, 3);

                Assert.AreEqual(-1, lru.Get(2));

                lru.Put(4, 4);

                Assert.AreEqual(-1, lru.Get(1));
                Assert.AreEqual(3, lru.Get(3));
                Assert.AreEqual(4, lru.Get(4));
            }
        }
    }
}