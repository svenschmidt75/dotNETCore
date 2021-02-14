#region

using System;
using NUnit.Framework;

#endregion

namespace LeetCode
{
    public class TournamentTree
    {
        public static int[] KWayMerge(int[][] nums)
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

            var k = nums.Length;

            var merged = new int[n];

            // SS: k-way merge using tournament trees
            var treeSize = 1 << ((int) Math.Ceiling(Math.Log2(k)) + 1);
            (int kIdx, int idx)[] tournamentTree = new (int, int)[treeSize];

            void InitializeTournamentTree(int min, int max, int nodeIndex)
            {
                // SS: base case is leaf node
                if (min == max)
                {
                    // SS: initialize with kth array and 1st element
                    tournamentTree[nodeIndex] = (min, 0);
                    return;
                }

                var leftIndex = 2 * nodeIndex + 1;
                var rightIndex = leftIndex + 1;

                var mid = (min + max) / 2;
                InitializeTournamentTree(min, mid, leftIndex);
                InitializeTournamentTree(mid + 1, max, rightIndex);

                // SS: propagate the smaller children to this level
                var lv = tournamentTree[leftIndex];
                var rv = tournamentTree[rightIndex];

                if (nums[lv.kIdx][lv.idx] <= nums[rv.kIdx][rv.idx])
                {
                    tournamentTree[nodeIndex] = lv;
                }
                else
                {
                    tournamentTree[nodeIndex] = rv;
                }
            }

            InitializeTournamentTree(0, k - 1, 0);

            // SS: merge
            var c = 0;
            while (true)
            {
                var root = tournamentTree[0];
                if (root.kIdx == -1)
                {
                    break;
                }

                merged[c++] = nums[root.kIdx][root.idx];

                // SS: replace branch with winner
                void Next(int min, int max, int nodeIndex)
                {
                    // SS: base case
                    if (min == max)
                    {
                        // SS: leaf node
                        var node = tournamentTree[nodeIndex];

                        if (node.idx == nums[node.kIdx].Length - 1)
                        {
                            tournamentTree[nodeIndex] = (-1, -1);
                        }
                        else
                        {
                            tournamentTree[nodeIndex] = (node.kIdx, node.idx + 1);
                        }
                    }
                    else
                    {
                        // SS: inner node
                        var leftIndex = 2 * nodeIndex + 1;
                        var rightIndex = leftIndex + 1;

                        var mid = (min + max) / 2;
                        if (tournamentTree[leftIndex] == root)
                        {
                            Next(min, mid, leftIndex);
                        }
                        else
                        {
                            Next(mid + 1, max, rightIndex);
                        }

                        // SS: propagate the smaller children to this level
                        var lv = tournamentTree[leftIndex];
                        var rv = tournamentTree[rightIndex];

                        if (lv.kIdx == -1 && rv.kIdx >= 0)
                        {
                            tournamentTree[nodeIndex] = rv;
                        }
                        else if (lv.kIdx >= 0 && rv.kIdx == -1)
                        {
                            tournamentTree[nodeIndex] = lv;
                        }
                        else if (lv.kIdx == -1 && rv.kIdx == -1)
                        {
                            tournamentTree[nodeIndex] = lv;
                        }
                        else if (lv.kIdx >= 0 && rv.kIdx >= 0)
                        {
                            if (nums[lv.kIdx][lv.idx] <= nums[rv.kIdx][rv.idx])
                            {
                                tournamentTree[nodeIndex] = lv;
                            }
                            else
                            {
                                tournamentTree[nodeIndex] = rv;
                            }
                        }
                    }
                }

                Next(0, k - 1, 0);
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
                var merged = new Solution().KWayMerge(nums);

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
                var merged = new Solution().KWayMerge(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18}, merged);
            }
        }
    }
}