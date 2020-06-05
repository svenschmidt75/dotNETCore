#region

using System;
using NUnit.Framework;

#endregion

namespace JohnProblem14
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
        private readonly T[] _input;

        private BinaryHeap(T[] input, Func<T, T, bool> comparer)
        {
            _input = input;
            _comparer = comparer;
            MakeHeap();
        }

        private BinaryHeap(Func<T, T, bool> comparer)
        {
            _comparer = comparer;
            _input = new T[0];
        }

        public static BinaryHeap<T> CreateMinHeap(T[] input, Func<T, T, bool> comparer)
        {
            return new BinaryHeap<T>(input, comparer);
        }

        public static BinaryHeap<T> CreateMinHeap(Func<T, T, bool> comparer)
        {
            return new BinaryHeap<T>(comparer);
        }

        public static BinaryHeap<T> CreateMaxHeap(T[] input, Func<T, T, bool> comparer)
        {
            return new BinaryHeap<T>(input, comparer);
        }

        public static BinaryHeap<T> CreateMaxHeap(Func<T, T, bool> comparer)
        {
            return new BinaryHeap<T>(comparer);
        }

        private void MakeHeap()
        {
            // SS: use Floyd's algorithm 
            if (_input.Length == 0)
            {
                return;
            }

            var depth = (int) Math.Log2(_input.Length) - 1;
            var nNodesAtDepth = 1 << depth;
            var startNode = 1 << depth - 1;

            while (depth >= 0)
            {
                for (var i = 0; i < nNodesAtDepth; i++)
                {
                    var parentNode = startNode + i;

                    var swapIndex = -1;

                    var leftChild = GetLeftChild(parentNode);
                    if (leftChild < _input.Length)
                    {
                        if (_comparer(_input[parentNode], _input[leftChild]))
                        {
                            swapIndex = leftChild;
                        }
                    }

                    var rightChild = GetRightChild(parentNode);
                    if (rightChild < _input.Length)
                    {
                        if (_comparer(_input[parentNode], _input[rightChild]))
                        {
                            if (leftChild == -1 || _comparer(_input[leftChild], _input[rightChild]))
                            {
                                swapIndex = rightChild;
                            }
                        }
                    }

                    if (swapIndex != -1)
                    {
                        Swap(parentNode, swapIndex);
                    }
                }

                startNode >>= 1;
                nNodesAtDepth >>= 1;
                
                depth--;
            }
        }

        private int GetRightChild(int parentNode)
        {
            return 2 * parentNode + 2;
        }

        private void Swap(int index1, int index2)
        {
            var tmp = _input[index1];
            _input[index1] = _input[index2];
            _input[index2] = tmp;
        }


        private int GetLeftChild(int parentNode)
        {
            return 2 * parentNode + 1;
        }
    }

    [TestFixture]
    public class BinaryHeapTest
    {
        [Test]
        public void TestCreateMaxHeap()
        {
            // Arrange
            var input = new[] { 8, 9, 12, 13, 15, 3, 17};
            
            // Act
            var maxHeap = BinaryHeap<int>.CreateMaxHeap(input, (i1, i2) => i1 < i2);

            // Assert
        }
        
    }
}