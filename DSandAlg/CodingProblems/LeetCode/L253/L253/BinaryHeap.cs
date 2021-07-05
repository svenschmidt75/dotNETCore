using System;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode
{
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

            var depth = (int)Math.Log2(Data.Count) - 1;
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
}