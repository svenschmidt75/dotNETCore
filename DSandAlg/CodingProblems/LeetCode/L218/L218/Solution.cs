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
                if (idx == buildings.Length)
                {
                    break;
                }

                var b = buildings[idx];
                minX = b[0];
                maxX = b[1];

                result.Add(new[] {minX, b[2]});

                maxHeap.Push(idx);

                idx++;

                while (idx < buildings.Length && maxHeap.IsEmpty == false)
                {
                    // SS: get tallest building
                    var tIdx = maxHeap.Peek();
                    var tallest = buildings[tIdx];

                    b = buildings[idx];
                    if (b[0] == tallest[0])
                    {
                        // SS: both buildings start at the same point, use the higher one
                        result[^1][1] = Math.Max(result[^1][1], b[2]);
                        maxX = Math.Max(maxX, b[1]);
                        maxHeap.Push(idx);
                        idx++;
                    }
                    else if (b[0] > tallest[1])
                    {
                        // SS: the next building starts after the tallest ends,
                        // so create points for the tallest building before adding
                        // the new building
                        while (true)
                        {
                            // SS: pop off the tallest building
                            tIdx = maxHeap.Peek();
                            tallest = buildings[tIdx];

                            // SS: if the current tallest building overlaps with the next unprocessed one,
                            // we are done
                            if (tallest[1] >= b[0])
                            {
                                break;
                            }

                            // SS: pop off the tallest building
                            maxHeap.Pop();

                            if (maxHeap.IsEmpty)
                            {
                                break;
                            }

                            // SS: next tallest building
                            var b2Idx = maxHeap.Pop();
                            var b2 = buildings[b2Idx];

                            if (tallest[1] >= b2[0] && tallest[1] < b2[1] && tallest[2] > b2[2])
                            {
                                // SS: there is an intersection point
                                result.Add(new[] {tallest[1], b2[2]});
                                maxHeap.Push(b2Idx);
                            }
                            else if (b2[1] < tallest[1])
                            {
                                // SS: no intersection point, drop smaller building
                                maxHeap.Push(tIdx);
                            }
                            else
                            {
                                // SS: no intersection point, drop tallest building
                                maxHeap.Push(b2Idx);
                                break;
                            }
                        }
                    }
                    else if (b[2] > tallest[2])
                    {
                        // SS: new buildings is taller
                        result.Add(new[] {b[0], b[2]});
                        maxX = Math.Max(maxX, b[1]);
                        maxHeap.Push(idx);
                        idx++;
                    }
                    else
                    {
                        // SS: new buildings is smaller
                        maxX = Math.Max(maxX, b[1]);
                        maxHeap.Push(idx);
                        idx++;
                    }
                }

                // SS: process remaining buildings
                while (maxHeap.IsEmpty == false)
                {
                    // SS: pop off the tallest building
                    var tIdx = maxHeap.Pop();
                    var tallest = buildings[tIdx];

                    if (maxHeap.IsEmpty)
                    {
                        break;
                    }

                    // SS: next tallest building
                    var b2Idx = maxHeap.Pop();
                    var b2 = buildings[b2Idx];

                    if (tallest[1] >= b2[0] && tallest[1] < b2[1] && tallest[2] > b2[2])
                    {
                        // SS: there is an intersection point
                        result.Add(new[] {tallest[1], b2[2]});
                        maxHeap.Push(b2Idx);
                    }
                    else if (b2[1] < tallest[1])
                    {
                        // SS: no intersection point, drop smaller building
                        maxHeap.Push(tIdx);
                    }
                    else
                    {
                        // SS: no intersection point, drop tallest building
                        maxHeap.Push(b2Idx);
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

//                var depth = (int) Math.Log2(Data.Count) - 1;
                var depth = (int) (Math.Log(Data.Count) / Math.Log(2)) - 1;
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

            [Test]
            public void Test22()
            {
                // Arrange
                int[][] buildings = {new[] {9, 100, 74}, new[] {47, 99, 152}, new[] {74, 99, 145}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {9, 74}, new[] {47, 152}, new[] {99, 74}, new[] {100, 0}}, results);
            }


            [Test]
            public void Test23()
            {
                // Arrange
                int[][] buildings = {new[] {9, 100, 74}, new[] {11, 30, 179}, new[] {12, 32, 215}, new[] {74, 99, 145}};

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {9, 74}, new[] {11, 179}, new[] {12, 215}, new[] {32, 74}, new[] {74, 145}, new[] {99, 74}, new[] {100, 0}}, results);
            }

            [Test]
            public void Test24()
            {
                // Arrange
                int[][] buildings =
                {
                    new[] {1, 38, 219}, new[] {2, 19, 228}, new[] {2, 64, 106}, new[] {3, 80, 65}, new[] {3, 84, 8}, new[] {4, 12, 8}, new[] {4, 25, 14}, new[] {4, 46, 225}, new[] {4, 67, 187}
                    , new[] {5, 36, 118}, new[] {5, 48, 211}, new[] {5, 55, 97}, new[] {6, 42, 92}, new[] {6, 56, 188}, new[] {7, 37, 42}, new[] {7, 49, 78}, new[] {7, 84, 163}, new[] {8, 44, 212}
                    , new[] {9, 42, 125}, new[] {9, 85, 200}, new[] {9, 100, 74}, new[] {10, 13, 58}, new[] {11, 30, 179}, new[] {12, 32, 215}, new[] {12, 33, 161}, new[] {12, 61, 198}
                    , new[] {13, 38, 48}, new[] {13, 65, 222}, new[] {14, 22, 1}, new[] {15, 70, 222}, new[] {16, 19, 196}, new[] {16, 24, 142}, new[] {16, 25, 176}, new[] {16, 57, 114}
                    , new[] {18, 45, 1}, new[] {19, 79, 149}, new[] {20, 33, 53}, new[] {21, 29, 41}, new[] {23, 77, 43}, new[] {24, 41, 75}, new[] {24, 94, 20}, new[] {27, 63, 2}, new[] {31, 69, 58}
                    , new[] {31, 88, 123}, new[] {31, 88, 146}, new[] {33, 61, 27}, new[] {35, 62, 190}, new[] {35, 81, 116}, new[] {37, 97, 81}, new[] {38, 78, 99}, new[] {39, 51, 125}
                    , new[] {39, 98, 144}, new[] {40, 95, 4}, new[] {45, 89, 229}, new[] {47, 49, 10}, new[] {47, 99, 152}, new[] {48, 67, 69}, new[] {48, 72, 1}, new[] {49, 73, 204}
                    , new[] {49, 77, 117}, new[] {50, 61, 174}, new[] {50, 76, 147}, new[] {52, 64, 4}, new[] {52, 89, 84}, new[] {54, 70, 201}, new[] {57, 76, 47}, new[] {58, 61, 215}
                    , new[] {58, 98, 57}, new[] {61, 95, 190}, new[] {66, 71, 34}, new[] {66, 99, 53}, new[] {67, 74, 9}, new[] {68, 97, 175}, new[] {70, 88, 131}, new[] {74, 77, 155}
                    , new[] {74, 99, 145}, new[] {76, 88, 26}, new[] {82, 87, 40}, new[] {83, 84, 132}, new[] {88, 99, 99}
                };

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {1, 219}, new[] {2, 228}, new[] {19, 225}, new[] {45, 229}, new[] {89, 190}, new[] {95, 175}, new[] {97, 152}, new[] {99, 74}, new[] {100, 0}}
                    , results);
            }

            [Test]
            public void Test25()
            {
                // Arrange
                int[][] buildings =
                {
                    new[] {9, 100, 74}, new[] {15, 70, 222}, new[] {16, 25, 176}, new[] {16, 57, 114}, new[] {18, 45, 1}, new[] {19, 79, 149}, new[] {20, 33, 53}, new[] {21, 29, 41}
                    , new[] {23, 77, 43}, new[] {57, 76, 47}, new[] {58, 61, 215}, new[] {58, 98, 57}, new[] {61, 95, 190}, new[] {74, 99, 145}
                };
                ;

                // Act
                var results = new Solution().GetSkyline(buildings);

                // Assert
                CollectionAssert.AreEqual(new[] {new[] {9, 74}, new[] {15, 222}, new[] {70, 190}, new[] {95, 145}, new[] {99, 74}, new[] {100, 0}}, results);
            }
        }
    }
}