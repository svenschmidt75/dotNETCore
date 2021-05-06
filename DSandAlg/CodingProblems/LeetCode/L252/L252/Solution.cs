#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: Meeting Rooms
// URL: https://leetcode.com/problems/meeting-rooms/

namespace LeetCode
{
    public class Solution
    {
        public int MeetingRooms(IList<int[]> schedule)
        {
            // SS: runtime complexity: O(n log n)
            // space complexity: O(n)

            // SS: sort at O(n log n)
            schedule = schedule.OrderBy(s => s[0]).ToList();

            var minHeap = BinaryHeap<int>.CreateMinHeap();

            var nRooms = 0;

            var idx = 0;
            while (idx < schedule.Count)
            {
                var course = schedule[idx];
                idx++;

                // SS: remove the courses that end before this one starts
                // as those will not cause conflicts
                while (minHeap.IsEmpty == false && course[0] > minHeap.Peek())
                {
                    // SS: new course starts after last ends
                    minHeap.Pop();
                }

                minHeap.Push(course[1]);
                nRooms = Math.Max(nRooms, minHeap.Length);
            }

            return nRooms;
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
                int[][] schedule = {new[] {0, 30}, new[] {5, 10}, new[] {15, 20}};

                // Act
                var result = new Solution().MeetingRooms(schedule);

                // Assert
                Assert.AreEqual(2, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] schedule = {new[] {5, 8}, new[] {9, 15}};

                // Act
                var result = new Solution().MeetingRooms(schedule);

                // Assert
                Assert.AreEqual(1, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[][] schedule = {new[] {0, 5}, new[] {0, 10}, new[] {0, 20}, new[] {15, 25}};

                // Act
                var result = new Solution().MeetingRooms(schedule);

                // Assert
                Assert.AreEqual(3, result);
            }
        }
    }
}