#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 295. Find Median from Data Stream
// URL: https://leetcode.com/problems/find-median-from-data-stream/

namespace LeetCode
{
    public class Solution1
    {
        public class MedianFinder
        {
            private readonly BinaryHeap<int> _leftOfMedianHeap;
            private readonly BinaryHeap<int> _rightOfMedianHeap;
            private double _median = double.MinValue;

            /**
             * initialize your data structure here.
             */
            public MedianFinder()
            {
                _leftOfMedianHeap = BinaryHeap<int>.CreateMaxHeap();
                _rightOfMedianHeap = BinaryHeap<int>.CreateMinHeap();
            }

            public void AddNum(int num)
            {
                // SS: The idea is to split the stream up into elements <= median and
                // > median. We use a max heap for the <= elements and a min heap for
                // the > elements. For example:
                // nums = [9, 4, 6, 1, 8, 5]
                // left: [1, 4, 5] right: [6, 8, 9]
                // Since the range of numbers is -10^5 <= num <= 10^5, we have to keep
                // all numbers seen.
                // 
                // Follow up 1: If all integer numbers from the stream are in the range [0, 100], how would you optimize your solution?
                // In this case, 
                
                var leftLength = _leftOfMedianHeap.Length;
                var rightLength = _rightOfMedianHeap.Length;
                
                if (leftLength == 0)
                {
                    _leftOfMedianHeap.Push(num);
                    leftLength++;
                }
                else if (rightLength == 0)
                {
                    _rightOfMedianHeap.Push(num);
                    rightLength++;
                }
                else
                {
                    if (num < _rightOfMedianHeap.Peek())
                    {
                        _leftOfMedianHeap.Push(num);
                        leftLength++;
                    }
                    else
                    {
                        _rightOfMedianHeap.Push(num);
                        rightLength++;
                    }
                }
                
                // SS: left and right must not differ by more than 1
                while (leftLength - 2 >= rightLength)
                {
                    var item = _leftOfMedianHeap.Pop();
                    _rightOfMedianHeap.Push(item);

                    leftLength--;
                    rightLength++;
                }

                while (rightLength - 2 >= leftLength)
                {
                    var item = _rightOfMedianHeap.Pop();
                    _leftOfMedianHeap.Push(item);

                    rightLength--;
                    leftLength++;
                }

                // SS: calculate median
                double median;

                if (leftLength == rightLength)
                {
                    var leftPeek = _leftOfMedianHeap.Peek();
                    var rightPeek = _rightOfMedianHeap.Peek();
                    median = (leftPeek + rightPeek) / 2.0;
                }
                else if (leftLength < rightLength)
                {
                    median = _rightOfMedianHeap.Peek();
                }
                else
                {
                    median = _leftOfMedianHeap.Peek();
                }

                _median = median;

                // SS: move elements from left to right and vice versa
                // depending on the new median
                while (_leftOfMedianHeap.IsEmpty == false && _leftOfMedianHeap.Peek() > median)
                {
                    var item = _leftOfMedianHeap.Pop();
                    _rightOfMedianHeap.Push(item);
                }

                while (_rightOfMedianHeap.IsEmpty == false && _rightOfMedianHeap.Peek() <= median)
                {
                    var item = _rightOfMedianHeap.Pop();
                    _leftOfMedianHeap.Push(item);
                }
            }

            public double FindMedian()
            {
                return _median;
            }
        }


        /// <summary>
        ///     Binary heap (max or min).
        ///     Uses array as underlying DS. Remember, the binary tree is assumed
        ///     to be complete.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class BinaryHeap<T> where T : IComparable
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

            public int Length => Data.Count;

            internal List<T> Data { get; }

            public bool IsEmpty => Data.Any() == false;

            public static BinaryHeap<T> CreateHeap(T[] input, Func<T, T, bool> comparer)
            {
                return new BinaryHeap<T>(input, comparer);
            }

            public static BinaryHeap<T> CreateMinHeap()
            {
                return new BinaryHeap<T>((i1, i2) => i1.CompareTo(i2) > 0);
            }

            public static BinaryHeap<T> CreateMinHeap(T[] input)
            {
                return new BinaryHeap<T>(input, (i1, i2) => i1.CompareTo(i2) > 0);
            }

            public static BinaryHeap<T> CreateMaxHeap()
            {
                return new BinaryHeap<T>((i1, i2) => i1.CompareTo(i2) < 0);
            }

            public static BinaryHeap<T> CreateMaxHeap(T[] input)
            {
                return new BinaryHeap<T>(input, (i1, i2) => i1.CompareTo(i2) < 0);
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
                var startNode = (1 << depth) - 1;

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

            private static int GetParentNode(int nodeIndex)
            {
                return (nodeIndex - 1) / 2;
            }


            private static int GetRightChild(int parentNode)
            {
                return 2 * parentNode + 2;
            }

            private void Swap(int index1, int index2)
            {
                var tmp = Data[index1];
                Data[index1] = Data[index2];
                Data[index2] = tmp;
            }


            private static int GetLeftChild(int parentNode)
            {
                return 2 * parentNode + 1;
            }

            public void Push(T value)
            {
                Data.Add(value);
                UpHeapify(Data.Count - 1);
            }

            public T Peek()
            {
                return Data[0];
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
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var medianFinder = new MedianFinder();

                // Act
                // Assert
                medianFinder.AddNum(1);
                medianFinder.AddNum(2);
                Assert.AreEqual(1.5, medianFinder.FindMedian());

                medianFinder.AddNum(3);
                Assert.AreEqual(2, medianFinder.FindMedian());
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var medianFinder = new MedianFinder();

                // Act
                // Assert
                medianFinder.AddNum(2);
                Assert.AreEqual(2, medianFinder.FindMedian());

                medianFinder.AddNum(7);
                Assert.AreEqual(4.5, medianFinder.FindMedian());

                medianFinder.AddNum(3);
                Assert.AreEqual(3, medianFinder.FindMedian());

                medianFinder.AddNum(5);
                Assert.AreEqual(4, medianFinder.FindMedian());

                medianFinder.AddNum(6);
                Assert.AreEqual(5, medianFinder.FindMedian());
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var medianFinder = new MedianFinder();

                // Act
                // Assert
                medianFinder.AddNum(2);
                Assert.AreEqual(2, medianFinder.FindMedian());

                medianFinder.AddNum(3);
                Assert.AreEqual(2.5, medianFinder.FindMedian());

                medianFinder.AddNum(5);
                Assert.AreEqual(3, medianFinder.FindMedian());

                medianFinder.AddNum(6);
                Assert.AreEqual(4, medianFinder.FindMedian());

                medianFinder.AddNum(7);
                Assert.AreEqual(5, medianFinder.FindMedian());
            }

            [TestCase(-10000, 10000, 1000)]
            public void Test4(int min, int max, int count)
            {
                // Arrange
                var medianFinder = new MedianFinder();

                var seed = DateTime.Now.Millisecond;
                var rnd = new Random(seed);

                Console.WriteLine($"Seed: {seed}");

                var list = new List<int>();

                for (var i = 0; i < count; i++)
                {
                    var v = rnd.Next(min, max);
                    list.Add(v);
                    medianFinder.AddNum(v);
                }

                // Act
                list.Sort();

                double median;

                if (list.Count % 2 == 0)
                {
                    median = (list[list.Count / 2 - 1] + list[list.Count / 2]) / 2.0;
                }
                else
                {
                    median = list[list.Count / 2 - 1];
                }

                // Assert
                Assert.AreEqual(median, medianFinder.FindMedian());
            }

            [TestCase(0, 10, 20)]
            public void Test5(int min, int max, int count)
            {
                // Arrange
                var medianFinder = new MedianFinder();

                var seed = DateTime.Now.Millisecond;
                var rnd = new Random(seed);

                Console.WriteLine($"Seed: {seed}");

                var list = new List<int>();

                for (var i = 0; i < count; i++)
                {
                    var v = rnd.Next(min, max);
                    list.Add(v);
                    medianFinder.AddNum(v);
                }

                // Act
                // list.Sort();
                //
                // double median;
                //
                // if (list.Count % 2 == 0)
                // {
                //     median = (list[list.Count / 2 - 1] + list[list.Count / 2]) / 2.0;
                // }
                // else
                // {
                //     median = list[list.Count / 2 - 1];
                // }
                //
                // // Assert
                // Assert.AreEqual(median, medianFinder.FindMedian());
            }
            
        }
    }
}