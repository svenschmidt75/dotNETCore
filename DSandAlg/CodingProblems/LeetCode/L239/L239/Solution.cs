#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 239. Sliding Window Maximum
// URL: https://leetcode.com/problems/sliding-window-maximum/

namespace LeetCode
{
    public class Solution
    {
        public int[] MaxSlidingWindow(int[] nums, int k)
        {
//            return MaxSlidingWindow1(nums, k);
            return MaxSlidingWindow2(nums, k);
        }

        private int[] MaxSlidingWindow2(int[] nums, int k)
        {
            // SS: Max Monotonic Queue approach,
            // https://www.youtube.com/c/Algorithmist/search?query=Monoqueues
            // runtime complexity: O(n), amortized, i.e. single operations may take longer,
            // but on avg. each operation takes O(1)

            if (k == 1)
            {
                return nums;
            }

            var result = new int[nums.Length - k + 1];
            var resultIdx = 0;

            var deque = new LinkedList<int>();

            var i = 0;
            var j = 0;

            while (j < nums.Length)
            {
                // SS: remove elements that are smaller than the current element nums[j],
                // as they cannot possibly be the max element
                while (deque.Any() && deque.Last.Value < nums[j])
                {
                    deque.RemoveLast();
                }

                deque.AddLast(nums[j]);

                if (j - i + 1 == k)
                {
                    result[resultIdx++] = deque.First.Value;
                    if (deque.First.Value == nums[i])
                    {
                        deque.RemoveFirst();
                    }

                    i++;
                }

                j++;
            }

            return result;
        }

        public int[] MaxSlidingWindow1(int[] nums, int k)
        {
            // SS: runtime complexity: O(n + n * log k) = O(n * log k)
            if (k == 1)
            {
                return nums;
            }

            var result = new int[nums.Length - k + 1];
            var resultIdx = 0;

            // SS: O(n)
            var maxHeap = new ModifiedMaxHeap(k);
            for (var i = 0; i < nums.Length; i++)
            {
                // SS: O(log k)
                maxHeap.Insert(nums[i]);

                if (maxHeap.Size() == k)
                {
                    result[resultIdx++] = maxHeap.Top();

                    // SS: O(log k)
                    maxHeap.Remove(nums[i - k + 1]);
                }
            }

            return result;
        }

        private class ModifiedMaxHeap
        {
            private readonly int[] _data;
            private readonly IDictionary<int, HashSet<int>> _dict = new Dictionary<int, HashSet<int>>();
            private readonly int _firstNodeIndex;
            private int _leafNodeIndex;

            public ModifiedMaxHeap(int k)
            {
                var nLeafNodes = (int) Math.Ceiling(Math.Log(k) / Math.Log(2));
                var nNodes = (1 << (nLeafNodes + 1)) - 1;

                _firstNodeIndex = (1 << nLeafNodes) - 1;
                _leafNodeIndex = _firstNodeIndex;

                _data = new int[nNodes];

                for (var i = 0; i < nNodes; i++)
                {
                    _data[i] = int.MinValue;
                }
            }

            public void Insert(int v)
            {
                var nodeIndex = _leafNodeIndex++;
                _data[nodeIndex] = v;

                if (_dict.TryGetValue(v, out var nodeIndices))
                {
                    nodeIndices.Add(nodeIndex);
                }
                else
                {
                    _dict[v] = new HashSet<int> {nodeIndex};
                }

                Upheapify(nodeIndex);
            }

            private void Upheapify(int startIndex)
            {
                // SS: runtime complexity: O(log n)
                var childIndex = startIndex;
                while (childIndex > 0)
                {
                    var parentIndex = (childIndex - 1) / 2;
                    var leftChild = 2 * parentIndex + 1;
                    var rightChild = 2 * parentIndex + 2;
                    _data[parentIndex] = Math.Max(_data[leftChild], _data[rightChild]);
                    childIndex = parentIndex;
                }
            }

            private void Swap(int idx1, int idx2)
            {
                var tmp = _data[idx1];
                _data[idx1] = _data[idx2];
                _data[idx2] = tmp;
            }

            public int Top()
            {
                return _data[0];
            }

            public void Remove(int v)
            {
                // SS: get leaf node index of value
                var nodeIndex1 = _dict[v].First();
                var nodeIndex2 = _leafNodeIndex - 1;

                if (nodeIndex1 < nodeIndex2)
                {
                    // SS: swap with latest element 
                    var v1 = _data[nodeIndex1];
                    _dict[v1].Remove(nodeIndex1);

                    var v2 = _data[nodeIndex2];
                    _dict[v2].Remove(nodeIndex2);
                    _dict[v2].Add(nodeIndex1);

                    if (_dict[v].Any() == false)
                    {
                        _dict.Remove(v);
                    }

                    Swap(nodeIndex1, nodeIndex2);

                    // SS: reset
                    _data[nodeIndex2] = int.MinValue;

                    Upheapify(nodeIndex1);
                    Upheapify(nodeIndex2);

                    // SS: shrink data
                    _leafNodeIndex--;
                }
                else
                {
                    // SS: we are removing the last index
                    _dict[v].Remove(nodeIndex1);
                    if (_dict[v].Any() == false)
                    {
                        _dict.Remove(v);
                    }

                    // SS: reset
                    _data[nodeIndex1] = int.MinValue;

                    Upheapify(nodeIndex1);

                    // SS: shrink data
                    _leafNodeIndex--;
                }
            }

            public int Size()
            {
                return _leafNodeIndex - _firstNodeIndex;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void TestHeap1()
            {
                // Arrange
                var maxHeap = new ModifiedMaxHeap(3);

                // Act
                maxHeap.Insert(1);

                // Assert
                Assert.AreEqual(1, maxHeap.Top());
                Assert.AreEqual(1, maxHeap.Size());
            }

            [Test]
            public void TestHeap2()
            {
                // Arrange
                var maxHeap = new ModifiedMaxHeap(5);
                maxHeap.Insert(1);
                maxHeap.Insert(2);
                maxHeap.Insert(3);

                // Act
                // Assert
                maxHeap.Remove(2);
                Assert.AreEqual(3, maxHeap.Top());

                maxHeap.Remove(3);
                Assert.AreEqual(1, maxHeap.Size());
            }

            [TestCase(new[] {1, 3, -1, -3, 5, 3, 6, 7}, 3, new[] {3, 3, 5, 5, 6, 7})]
            [TestCase(new[] {1}, 1, new[] {1})]
            [TestCase(new[] {1, -1}, 1, new[] {1, -1})]
            [TestCase(new[] {9, 11}, 2, new[] {11})]
            [TestCase(new[] {4, -2}, 2, new[] {4})]
            public void Test1(int[] nums, int k, int[] expected)
            {
                // Arrange

                // Act
                var result = new Solution().MaxSlidingWindow(nums, k);

                // Assert
                CollectionAssert.AreEqual(expected, result);
            }
        }
    }
}