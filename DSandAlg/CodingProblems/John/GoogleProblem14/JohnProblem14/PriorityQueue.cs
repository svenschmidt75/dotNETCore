#region

using System;
using NUnit.Framework;

#endregion

namespace JohnProblem14
{
    public class PriorityQueue<T>
    {
        private readonly BinaryHeap<(int, T)> _binaryHeap;

        private PriorityQueue(Func<(int p, T d), (int p, T d), bool> comparer)
        {
            _binaryHeap = BinaryHeap<(int, T)>.CreateHeap(comparer);
        }

        public bool IsEmpty => _binaryHeap.IsEmpty;

        public static PriorityQueue<T> CreateMaxPriorityQueue()
        {
            return new PriorityQueue<T>((p1, p2) => p1.p < p2.p);
        }

        public static PriorityQueue<T> CreateMinPriorityQueue()
        {
            return new PriorityQueue<T>((p1, p2) => p1.p > p2.p);
        }

        public void Enqueue(T value, int priority)
        {
            _binaryHeap.Push((priority, value));
        }

        public (int priority, T value) Dequeue()
        {
            return _binaryHeap.Pop();
        }
    }

    [TestFixture]
    public class TestPriorityQueue
    {
        [Test]
        public void TestEnqueue()
        {
            // Arrange
            var pq = PriorityQueue<string>.CreateMaxPriorityQueue();

            // Act
            pq.Enqueue("a", 17);
            pq.Enqueue("b", 9);
            pq.Enqueue("c", 2);
            pq.Enqueue("d", 21);

            // Assert
            Assert.AreEqual("d", pq.Dequeue().value);
            Assert.AreEqual("a", pq.Dequeue().value);
            Assert.AreEqual("b", pq.Dequeue().value);
            Assert.AreEqual("c", pq.Dequeue().value);

            Assert.True(pq.IsEmpty);
        }
    }
}