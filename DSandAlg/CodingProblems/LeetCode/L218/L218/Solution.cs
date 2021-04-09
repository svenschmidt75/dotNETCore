#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 218. The Skyline Problem
// URL: https://leetcode.com/problems/the-skyline-problem/

namespace LeetCode
{
    public class Solution
    {
        public IList<IList<int>> GetSkyline(int[][] buildings)
        {
            return GetSkyLine1(buildings);
        }

        private static IList<IList<int>> GetSkyLine1(int[][] buildings)
        {
            var result = new List<IList<int>>();

            var maxHeap = BinaryHeap<int>.CreateHeap((b1Idx, b2Idx) =>
            {
                var b1 = buildings[b1Idx];
                var b2 = buildings[b2Idx];

                if (b1[2] < b2[2])
                {
                    return true;
                }

                if (b1[2] > b2[2])
                {
                    return false;
                }

                // SS: tie, prioritize the one with larger endX
                return b1[1] >= b2[1];
            });

            int minX;
            int maxX;

            var idx = 0;
            while (true)
            {
                if (idx == buildings.Length && maxHeap.IsEmpty)
                {
                    break;
                }

                if (idx == buildings.Length)
                {
                    break;
                }

                var b1 = buildings[idx];
                minX = b1[0];
                maxX = b1[1];

                result.Add(new[] {minX, b1[2]});

                maxHeap.Push(idx);
                idx++;

                while (idx < buildings.Length)
                {
                    var tIdx = maxHeap.Peek();
                    var tallest = buildings[tIdx];

                    var b2 = buildings[idx];
                    if (b2[0] == tallest[0])
                    {
                        // SS: use the latter one
                        result[^1][1] = Math.Max(result[^1][1], b2[2]);
                        maxX = Math.Max(maxX, b2[1]);
                        maxHeap.Push(idx);
                        idx++;
                    }
                    else if (b2[0] > tallest[1])
                    {
                        // SS: if the next building starts after the tallest ends,
                        // we need to first process all buildings before it.
                        while (maxHeap.IsEmpty == false)
                        {
                            // SS: check whether the next building overlaps with this one
                            tIdx = maxHeap.Peek();
                            tallest = buildings[tIdx];

                            // SS: if the buildings overlap, exit
                            if (tallest[1] >= b2[0])
                            {
                                break;
                            }

                            maxHeap.Pop();

                            if (maxHeap.IsEmpty)
                            {
                                break;
                            }

                            var b3Idx = maxHeap.Pop();
                            var b3 = buildings[b3Idx];

                            if (tallest[1] >= b3[0] && tallest[1] < b3[1] && tallest[2] != b3[2])
                            {
                                result.Add(new[] {tallest[1], b3[2]});
                                maxHeap.Push(b3Idx);
                            }
                            else if (b3[1] < tallest[1])
                            {
                                // SS: must check tallest against others
                                maxHeap.Push(tIdx);
                            }
                            else
                            {
                                maxHeap.Push(b3Idx);
                                break;
                            }
                        }

                        if (maxHeap.IsEmpty)
                        {
                            break;
                        }
                    }
                    else if (b2[2] > tallest[2])
                    {
                        // SS: new buildings is taller
                        result.Add(new[] {b2[0], b2[2]});
                        maxX = Math.Max(maxX, b2[1]);
                        maxHeap.Push(idx);
                        idx++;
                    }
                    else
                    {
                        // SS: new buildings is smaller
                        maxX = Math.Max(maxX, b2[1]);
                        maxHeap.Push(idx);
                        idx++;
                    }
                }

                // SS: process buildings until...
                // SS: if the next building starts after the tallest ends,
                // we need to first process all buildings before it.
                while (maxHeap.IsEmpty == false)
                {
                    var tIdx = maxHeap.Pop();
                    var tallest = buildings[tIdx];

                    if (maxHeap.IsEmpty)
                    {
                        break;
                    }

                    var b2Idx = maxHeap.Pop();
                    var b2 = buildings[b2Idx];

                    if (tallest[1] >= b2[0] && tallest[1] < b2[1] && tallest[2] != b2[2])
                    {
                        result.Add(new[] {tallest[1], b2[2]});
                        maxHeap.Push(b2Idx);
                    }
                    else if (b2[1] < tallest[1])
                    {
                        // SS: must check tallest against others
                        maxHeap.Push(tIdx);
                    }
                    else
                    {
                        maxHeap.Push(b2Idx);
                        break;
                    }
                }

                // SS: insert maxX
                result.Add(new[] {maxX, 0});
            }

            return result;
        }

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

            internal List<T> Data { get; }

            public bool IsEmpty => Data.Any() == false;

            public static BinaryHeap<T> CreateHeap(T[] input, Func<T, T, bool> comparer)
            {
                return new BinaryHeap<T>(input, comparer);
            }

            public static BinaryHeap<T> CreateMinHeap(T[] input)
            {
                return new BinaryHeap<T>(input, (i1, i2) => i1.CompareTo(i2) > 0);
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

            public T Peek()
            {
                return Data[0];
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[][] buildings = {new[] {2, 9, 10}, new[] {3, 7, 15}, new[] {5, 12, 12}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {3, 15}, new[] {7, 12}, new[] {12, 0}}, results);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[][] buildings = {new[] {2, 9, 10}, new[] {2, 12, 10}, new[] {10, 20, 12}, new[] {14, 18, 14}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {10, 12}, new[] {14, 14}, new[] {18, 12}, new[] {20, 0}}, results);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[][] buildings = {new[] {2, 9, 10}, new[] {2, 12, 10}, new[] {10, 15, 12}, new[] {14, 18, 14}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {10, 12}, new[] {14, 14}, new[] {18, 0}}, results);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[][] buildings = {new[] {2, 6, 10}, new[] {4, 12, 12}, new[] {8, 10, 8}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {4, 12}, new[] {12, 0}}, results);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[][] buildings = {new[] {2, 8, 10}, new[] {4, 10, 14}, new[] {8, 12, 12}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {4, 14}, new[] {10, 12}, new[] {12, 0}}, results);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[][] buildings = {new[] {2, 14, 10}, new[] {4, 8, 16}, new[] {6, 10, 14}, new[] {6, 12, 12}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {4, 16}, new[] {8, 14}, new[] {10, 12}, new[] {12, 10}, new[] {14, 0}}, results);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[][] buildings = {new[] {2, 14, 10}, new[] {4, 8, 16}, new[] {6, 7, 14}, new[] {6, 12, 12}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {4, 16}, new[] {8, 12}, new[] {12, 10}, new[] {14, 0}}, results);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                int[][] buildings = {new[] {2, 14, 10}, new[] {4, 8, 16}, new[] {6, 14, 12}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {4, 16}, new[] {8, 12}, new[] {14, 0}}, results);
            }

            [Test]
            public void Test9()
            {
                // Arrange
                int[][] buildings = {new[] {2, 14, 10}, new[] {4, 8, 16}, new[] {6, 11, 12}, new[] {10, 12, 16}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {4, 16}, new[] {8, 12}, new[] {10, 16}, new[] {12, 10}, new[] {14, 0}}, results);
            }

            [Test]
            public void Test10()
            {
                // Arrange
                int[][] buildings = {new[] {2, 9, 10}, new[] {5, 7, 12}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {5, 12}, new[] {7, 10}, new[] {9, 0}}, results);
            }

            [Test]
            public void Test11()
            {
                // Arrange
                int[][] buildings = {new[] {2, 12, 10}, new[] {8, 18, 12}, new[] {13, 14, 16}, new[] {16, 20, 14}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {8, 12}, new[] {13, 16}, new[] {14, 12}, new[] {16, 14}, new[] {20, 0}}, results);
            }

            [Test]
            public void Test12()
            {
                // Arrange
                int[][] buildings = {new[] {2, 12, 10}, new[] {8, 22, 12}, new[] {13, 14, 16}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {8, 12}, new[] {13, 16}, new[] {14, 12}, new[] {22, 0}}, results);
            }

            [Test]
            public void Test13()
            {
                // Arrange
                int[][] buildings = {new[] {2, 12, 10}, new[] {8, 18, 12}, new[] {13, 16, 16}, new[] {15, 20, 14}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {8, 12}, new[] {13, 16}, new[] {16, 14}, new[] {20, 0}}, results);
            }

            [Test]
            public void Test14()
            {
                // Arrange
                int[][] buildings = {new[] {2, 14, 10}, new[] {4, 8, 16}, new[] {6, 12, 12}, new[] {7, 10, 14}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {4, 16}, new[] {8, 14}, new[] {10, 12}, new[] {12, 10}, new[] {14, 0}}, results);
            }

            [Test]
            public void Test15()
            {
                // Arrange
                int[][] buildings = {new[] {2, 12, 10}, new[] {4, 10, 16}, new[] {6, 16, 12}, new[] {8, 14, 14}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {4, 16}, new[] {10, 14}, new[] {14, 12}, new[] {16, 0}}, results);
            }

            [Test]
            public void Test16()
            {
                // Arrange
                int[][] buildings = {new[] {2, 12, 10}, new[] {4, 8, 16}, new[] {6, 14, 12}, new[] {10, 16, 14}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {4, 16}, new[] {8, 12}, new[] {10, 14}, new[] {16, 0}}, results);
            }

            [Test]
            public void Test17()
            {
                // Arrange
                int[][] buildings = {new[] {2, 9, 10}, new[] {3, 7, 15}, new[] {5, 12, 12}, new[] {15, 20, 10}, new[] {19, 24, 8}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {3, 15}, new[] {7, 12}, new[] {12, 0}, new[] {15, 10}, new[] {20, 8}, new[] {24, 0}}, results);
            }

            [Test]
            public void Test18()
            {
                // Arrange
                int[][] buildings = {new[] {2, 10, 10}, new[] {10, 12, 10}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {12, 0}}, results);
            }

            [Test]
            public void Test19()
            {
                // Arrange
                int[][] buildings = {new[] {2, 10, 10}, new[] {10, 12, 12}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {10, 12}, new[] {12, 0}}, results);
            }

            [Test]
            public void Test20()
            {
                // Arrange
                int[][] buildings = {new[] {2, 10, 10}, new[] {2, 6, 12}, new[] {2, 8, 14}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 14}, new[] {8, 10}, new[] {10, 0}}, results);
            }

            [Test]
            public void Test21()
            {
                // Arrange
                int[][] buildings = {new[] {2, 10, 10}, new[] {4, 8, 14}, new[] {4, 9, 12}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {2, 10}, new[] {4, 14}, new[] {8, 12}, new[] {9, 10}, new[] {10, 0}}, results);
            }
        }
    }
}