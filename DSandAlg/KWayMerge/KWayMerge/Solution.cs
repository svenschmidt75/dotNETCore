#region

using System;
using NUnit.Framework;

#endregion

namespace LeetCode
{
    public class TournamentTree
    {
        private readonly int _k;
        private readonly int[][] _nums;
        private readonly (int kIdx, int idx, int leafNodeIndex)[] _tournamentTree;

        public TournamentTree(int[][] nums)
        {
            _nums = nums;

            _k = nums.Length;
            var treeSize = 1 << ((int) Math.Ceiling(Math.Log2(_k)) + 1);
            _tournamentTree = new (int, int, int)[treeSize];

            Initialize(0, _k - 1, 0);
        }

        private void Initialize(int min, int max, int nodeIndex)
        {
            // SS: base case is leaf node
            if (min == max)
            {
                // SS: initialize with kth array and 1st element
                _tournamentTree[nodeIndex] = (min, 0, nodeIndex);
                return;
            }

            var leftIndex = 2 * nodeIndex + 1;
            var rightIndex = leftIndex + 1;

            var mid = (min + max) / 2;
            Initialize(min, mid, leftIndex);
            Initialize(mid + 1, max, rightIndex);

            // SS: propagate the smaller children to this level
            var lv = _tournamentTree[leftIndex];
            var rv = _tournamentTree[rightIndex];

            if (_nums[lv.kIdx][lv.idx] <= _nums[rv.kIdx][rv.idx])
            {
                _tournamentTree[nodeIndex] = lv;
            }
            else
            {
                _tournamentTree[nodeIndex] = rv;
            }
        }

        private void Next()
        {
            // SS: runtime complexity: O(log k)

            // SS: advance to next index in array
            var root = _tournamentTree[0];
            if (_tournamentTree[root.leafNodeIndex].idx == _nums[root.kIdx].Length - 1)
            {
                _tournamentTree[root.leafNodeIndex] = (-1, -1, root.leafNodeIndex);
            }
            else
            {
                _tournamentTree[root.leafNodeIndex].idx++;
            }

            // SS: propagate the tournament winner up
            var parentNode = (root.leafNodeIndex - 1) / 2;
            while (true)
            {
                // SS: propagate the smaller children to this level
                var lv = _tournamentTree[2 * parentNode + 1];
                var rv = _tournamentTree[2 * parentNode + 2];

                if (lv.kIdx == -1 && rv.kIdx >= 0)
                {
                    _tournamentTree[parentNode] = rv;
                }
                else if (lv.kIdx >= 0 && rv.kIdx == -1)
                {
                    _tournamentTree[parentNode] = lv;
                }
                else if (lv.kIdx == -1 && rv.kIdx == -1)
                {
                    _tournamentTree[parentNode] = lv;
                }
                else if (lv.kIdx >= 0 && rv.kIdx >= 0)
                {
                    if (_nums[lv.kIdx][lv.idx] <= _nums[rv.kIdx][rv.idx])
                    {
                        _tournamentTree[parentNode] = lv;
                    }
                    else
                    {
                        _tournamentTree[parentNode] = rv;
                    }
                }

                if (parentNode == 0)
                {
                    break;
                }

                parentNode = (parentNode - 1) / 2;
            }
        }

        public int[] KWayMerge(int[][] nums)
        {
            if (nums.Length == 0)
            {
                return new int[0];
            }

            if (nums.Length == 1)
            {
                return nums[0];
            }

            var n = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                n += nums[i].Length;
            }

            var merged = new int[n];

            // SS: merge
            var c = 0;
            while (true)
            {
                var root = _tournamentTree[0];
                if (root.kIdx == -1)
                {
                    break;
                }

                merged[c++] = nums[root.kIdx][root.idx];

                Next();
            }

            return merged;
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] nums1 = {1, 3, 4};
                int[] nums2 = {2, 5, 7};
                int[][] nums = {nums1, nums2};

                // Act
                var merged = new TournamentTree(nums).KWayMerge(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 2, 3, 4, 5, 7}, merged);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] nums1 = {1, 7, 12};
                int[] nums2 = {3, 13, 15};
                int[] nums3 = {4, 14, 18};
                int[] nums4 = {2, 10, 17};
                int[] nums5 = {5, 9, 11};
                int[] nums6 = {6, 8, 16};
                int[][] nums = {nums1, nums2, nums3, nums4, nums5, nums6};

                // Act
                var merged = new TournamentTree(nums).KWayMerge(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18}, merged);
            }
        }
    }
}