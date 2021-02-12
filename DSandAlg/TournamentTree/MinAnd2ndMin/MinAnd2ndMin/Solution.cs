#region

using System;
using NUnit.Framework;

#endregion

namespace TournementTree
{
    public class MinTournamentTree
    {
        private readonly int[] _binaryTree;
        private int _leafStartIndex;

        public MinTournamentTree(int[] data)
        {
            _binaryTree = new int[2 * data.Length - 1];
            _leafStartIndex = data.Length;
            Build(data);
        }

        public (int min1, int min2) FindMinimum()
        {
            // SS: runtime complexity: O(log n) since tree is height-balanced

            // SS: assumption: array data has at least 3 elements

            var min1 = _binaryTree[0];
            var min2 = int.MaxValue;

            // SS: find second minimum
            var nodeIndex = 0;
            while (true)
            {
                var leftChild = 2 * nodeIndex + 1;
                var rightChild = 2 * nodeIndex + 2;

                // SS: a tournament tree is complete, so either a node has 2 children
                // or none
                if (leftChild >= _binaryTree.Length)
                {
                    break;
                }

                var left = _binaryTree[leftChild];
                var right = _binaryTree[rightChild];

                if (left == min1)
                {
                    min2 = Math.Min(min2, right);

                    // SS: follow the min element
                    nodeIndex = nodeIndex * 2 + 1;
                }
                else
                {
                    min2 = Math.Min(min2, left);

                    // SS: follow the min element
                    nodeIndex = nodeIndex * 2 + 2;
                }
            }

            return (min1, min2);
        }

        private void Build(int[] data)
        {
            // SS: fill-in the leaf nodes
            // runtime complexity: O(n) to fill in leaf nodes + log n
            // levels, with a total of n comparisons, so total O(2n)
            
            int DFS(int minIdx, int maxIdx, int nodeIndex)
            {
                // SS: post-order traversal

                // SS: leaf node?
                if (minIdx == maxIdx)
                {
                    _binaryTree[nodeIndex] = data[minIdx];
                    return data[minIdx];
                }

                var mid = (minIdx + maxIdx) / 2;
                var leftMin = DFS(minIdx, mid, 2 * nodeIndex + 1);
                var rightMin = DFS(mid + 1, maxIdx, 2 * nodeIndex + 2);
                var minElement = Math.Min(leftMin, rightMin);
                _binaryTree[nodeIndex] = minElement;
                return minElement;
            }

            DFS(0, data.Length - 1, 0);
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] data = {1, 2, 3, 4, 5, 6, 7};
                var tree = new MinTournamentTree(data);

                // Act
                (var min1, var min2) = tree.FindMinimum();

                // Assert
                Assert.AreEqual(1, min1);
                Assert.AreEqual(2, min2);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] data = {61, 6, 100, 9, 10, 12, 17};
                var tree = new MinTournamentTree(data);

                // Act
                (var min1, var min2) = tree.FindMinimum();

                // Assert
                Assert.AreEqual(6, min1);
                Assert.AreEqual(9, min2);
            }
        }
    }
}