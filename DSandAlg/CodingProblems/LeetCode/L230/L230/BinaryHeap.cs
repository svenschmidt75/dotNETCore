#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace L230
{
    /// <summary>
    ///     Binary heap (max or min).
    ///     Uses array as underlying DS. Remember, the binary tree is assumed
    ///     to be complete.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryHeap<T>
    {
        private readonly Func<T, T, bool> _comparer;

        private BinaryHeap(T[] input, Func<T, T, bool> comparer)
        {
            Data = input.ToList();
            _comparer = comparer;
            MakeHeap();
        }

        private BinaryHeap(Func<T, T, bool> comparer)
        {
            _comparer = comparer;
            Data = new List<T>();
        }

        internal List<T> Data { get; }

        public bool IsEmpty => Data.Any() == false;

        public static BinaryHeap<T> CreateHeap(T[] input, Func<T, T, bool> comparer)
        {
            return new BinaryHeap<T>(input, comparer);
        }

        public static BinaryHeap<T> CreateHeap(Func<T, T, bool> comparer)
        {
            return new BinaryHeap<T>(comparer);
        }

        private void MakeHeap()
        {
            // SS: use Floyd's algorithm 
            if (Data.Any() == false)
            {
                return;
            }

            var depth = (int) Math.Log2(Data.Count) - 1;
            var nNodesAtDepth = 1 << depth;
            var startNode = 1 << (depth - 1);

            while (depth >= 0)
            {
                for (var i = 0; i < nNodesAtDepth; i++)
                {
                    var parentNode = startNode + i;
                    DownHeapify(parentNode);
                }

                startNode >>= 1;
                nNodesAtDepth >>= 1;

                depth--;
            }
        }

        private void UpHeapify(int startIndex)
        {
            var nodeIndex = startIndex;
            while (nodeIndex > 0)
            {
                var parentNode = GetParentNode(nodeIndex);
                if (_comparer(Data[parentNode], Data[nodeIndex]))
                {
                    Swap(parentNode, nodeIndex);
                    nodeIndex = parentNode;
                }
                else
                {
                    break;
                }
            }
        }

        private void DownHeapify(int startIndex)
        {
            var parentNode = startIndex;
            while (parentNode >= 0)
            {
                var swapIndex = -1;

                var leftChild = GetLeftChild(parentNode);
                if (leftChild < Data.Count)
                {
                    if (_comparer(Data[parentNode], Data[leftChild]))
                    {
                        swapIndex = leftChild;
                    }
                }

                var rightChild = GetRightChild(parentNode);
                if (rightChild < Data.Count)
                {
                    if (_comparer(Data[parentNode], Data[rightChild]))
                    {
                        if (leftChild == -1 || _comparer(Data[leftChild], Data[rightChild]))
                        {
                            swapIndex = rightChild;
                        }
                    }
                }

                if (swapIndex != -1)
                {
                    Swap(parentNode, swapIndex);
                    parentNode = swapIndex;
                }
                else
                {
                    break;
                }
            }
        }

        private int GetParentNode(int nodeIndex)
        {
            return (nodeIndex - 1) / 2;
        }


        private int GetRightChild(int parentNode)
        {
            return 2 * parentNode + 2;
        }

        private void Swap(int index1, int index2)
        {
            var tmp = Data[index1];
            Data[index1] = Data[index2];
            Data[index2] = tmp;
        }


        private int GetLeftChild(int parentNode)
        {
            return 2 * parentNode + 1;
        }

        public void Push(T value)
        {
            Data.Add(value);
            UpHeapify(Data.Count - 1);
        }

        public T Pop()
        {
            if (Data.Any() == false)
            {
                throw new ArgumentException();
            }

            var value = Data[0];
            if (Data.Count > 1)
            {
                Data[0] = Data[^1];
                DownHeapify(0);
            }

            Data.RemoveAt(Data.Count - 1);
            return value;
        }
    }

    [TestFixture]
    public class BinaryHeapTest
    {
        [Test]
        public void TestCreateMaxHeap()
        {
            // Arrange
            var input = new[] {8, 9, 12, 13, 15, 3, 17};

            // Act
            var maxHeap = BinaryHeap<int>.CreateHeap(input, (i1, i2) => i1 < i2);

            // Assert
            CollectionAssert.AreEqual(new[] {17, 15, 12, 13, 9, 3, 8}, maxHeap.Data);
        }

        [Test]
        public void TestCreateMinHeap()
        {
            // Arrange
            var input = new[] {8, 9, 12, 13, 15, 3, 17};

            // Act
            var maxHeap = BinaryHeap<int>.CreateHeap(input, (i1, i2) => i1 > i2);

            // Assert
            CollectionAssert.AreEqual(new[] {3, 9, 8, 13, 15, 12, 17}, maxHeap.Data);
        }

        [Test]
        public void TestPopMaxHeap()
        {
            // Arrange
            var input = new[] {8, 9, 12, 13, 15, 3, 17};
            var maxHeap = BinaryHeap<int>.CreateHeap(input, (i1, i2) => i1 < i2);
            maxHeap.Push(16);

            // Act / Assert
            var value = maxHeap.Pop();
            Assert.AreEqual(17, value);

            value = maxHeap.Pop();
            Assert.AreEqual(16, value);

            value = maxHeap.Pop();
            Assert.AreEqual(15, value);

            value = maxHeap.Pop();
            Assert.AreEqual(13, value);

            value = maxHeap.Pop();
            Assert.AreEqual(12, value);

            value = maxHeap.Pop();
            Assert.AreEqual(9, value);

            value = maxHeap.Pop();
            Assert.AreEqual(8, value);

            value = maxHeap.Pop();
            Assert.AreEqual(3, value);

            Assert.True(maxHeap.IsEmpty);
        }

        [Test]
        public void TestPopMinHeap()
        {
            // Arrange
            var input = new[] {8, 9, 12, 13, 15, 3, 17};
            var minHeap = BinaryHeap<int>.CreateHeap(input, (i1, i2) => i1 > i2);
            minHeap.Push(16);

            // Act / Assert
            var value = minHeap.Pop();
            Assert.AreEqual(3, value);

            value = minHeap.Pop();
            Assert.AreEqual(8, value);

            value = minHeap.Pop();
            Assert.AreEqual(9, value);

            value = minHeap.Pop();
            Assert.AreEqual(12, value);

            value = minHeap.Pop();
            Assert.AreEqual(13, value);

            value = minHeap.Pop();
            Assert.AreEqual(15, value);

            value = minHeap.Pop();
            Assert.AreEqual(16, value);

            value = minHeap.Pop();
            Assert.AreEqual(17, value);

            Assert.True(minHeap.IsEmpty);
        }

        [Test]
        public void TestPushMaxHeap()
        {
            // Arrange
            var input = new[] {8, 9, 12, 13, 15, 3, 17};
            var maxHeap = BinaryHeap<int>.CreateHeap(input, (i1, i2) => i1 < i2);

            // Act
            maxHeap.Push(16);

            // Assert
            CollectionAssert.AreEqual(new[] {17, 16, 12, 15, 9, 3, 8, 13}, maxHeap.Data);
        }
    }
}