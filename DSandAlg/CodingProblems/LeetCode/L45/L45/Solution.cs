#region

using System;
using NUnit.Framework;

#endregion

// Problem: 45. Jump Game II
// URL: https://leetcode.com/problems/jump-game-ii/

namespace LeetCode45
{
    public class Solution
    {
        public int Jump(int[] nums)
        {
            // SS: runtime complexity: O(N log N)
            
            // SS: initialize a segment tree for fast min range queries
            var st = new SegmentTree(nums.Length);
            
            int[] dp = new int[nums.Length];
            
            // SS: dp[nums.Length] = 0, i.e. once we are at the last position,
            // the jump is 0

            for (int i = nums.Length - 2; i >= 0; i--)
            {
                int nJumps = nums[i];

                int v;
                
                if (nJumps > 0)
                {
                    int minIdx = i + 1;
                    int maxIdx = i + nJumps;

                    // SS: O(log N)
                    int min = st.Query(minIdx, maxIdx);
                    v = min == int.MaxValue ? min : 1 + min;
                }
                else
                {
                    v = int.MaxValue;
                }

                dp[i] = v;

                // SS: O(log N)
                st.Set(i, v);
            }
            
            return dp[0];
        }

        private class SegmentTree
        {
            internal readonly int[] _data;
            private readonly int _n;

            public SegmentTree(int n)
            {
                _n = n;
                var exp = (int) Math.Ceiling(Math.Log10(n) / Math.Log10(2));
                var size = (int) (Math.Pow(2, exp + 1) - 1);
                _data = new int[size];
            }

            public void Set(int idx, int value)
            {
                // SS: runtime complexity: O(log N), N = _data.Length

                // SS: determine node index
                var nodeIdx = NodeIndexFromArrayIndex(idx);
                _data[nodeIdx] = value;

                // SS: propagate up
                var parent = (nodeIdx - 1) / 2;
                while (parent >= 0)
                {
                    _data[parent] = Math.Min(_data[2 * parent + 1], _data[2 * parent + 2]);
                    if (parent == 0)
                    {
                        break;
                    }

                    parent = (parent - 1) / 2;
                }
            }

            public int NodeIndexFromArrayIndex(int idx)
            {
                // SS: runtime complexity: O(log N)
                var nodeIdx = 0;

                var minRange = 0;
                var maxRange = _n - 1;

                while (minRange < maxRange)
                {
                    var mid = minRange + (maxRange - minRange) / 2;
                    if (idx <= mid)
                    {
                        maxRange = mid;
                        nodeIdx = 2 * nodeIdx + 1;
                    }
                    else
                    {
                        minRange = mid + 1;
                        nodeIdx = 2 * nodeIdx + 2;
                    }
                }

                return nodeIdx;
            }

            public int Query(int minIdx, int maxIdx)
            {
                // SS: [minIdx, maxIdx]
                // runtime complexity: O(log N)

                int Query(int node, int minRange, int maxRange)
                {
                    if (minIdx <= minRange && maxIdx >= maxRange)
                    {
                        return _data[node];
                    }

                    if (minIdx > maxRange || maxIdx < minRange)
                    {
                        return int.MaxValue;
                    }

                    // SS: we need to check both subtrees
                    var leftMaxRange = minRange + (maxRange - minRange) / 2;
                    var leftMin = Query(2 * node + 1, minRange, leftMaxRange);
                    var rightMin = Query(2 * node + 2, leftMaxRange + 1, maxRange);
                    return Math.Min(leftMin, rightMin);
                }

                return Query(0, 0, _n - 1);
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void TestJump1()
            {
                // Arrange
                int[] nums = {2, 3, 1, 1, 4};
                
                // Act
                int minJumps = new Solution().Jump(nums);
                
                // Assert
                Assert.AreEqual(2, minJumps);
            }
            
            [Test]
            public void TestJump2()
            {
                // Arrange
                int[] nums = {2, 3, 0, 1, 4};
                
                // Act
                int minJumps = new Solution().Jump(nums);
                
                // Assert
                Assert.AreEqual(2, minJumps);
            }

            [Test]
            public void TestJump3()
            {
                // Arrange
                int[] nums = {2, 2, 0, 1, 7};
                
                // Act
                int minJumps = new Solution().Jump(nums);
                
                // Assert
                Assert.AreEqual(3, minJumps);
            }

            [Test]
            public void TestJump4()
            {
                // Arrange
                int[] nums = {2};
                
                // Act
                int minJumps = new Solution().Jump(nums);
                
                // Assert
                Assert.AreEqual(0, minJumps);
            }

            [Test]
            public void TestJump5()
            {
                // Arrange
                int[] nums = {5, 0, 0, 0, 0, 9};
                
                // Act
                int minJumps = new Solution().Jump(nums);
                
                // Assert
                Assert.AreEqual(1, minJumps);
            }

            [Test]
            public void TestJump6()
            {
                // Arrange
                int[] nums = {5,9,3,2,1,0,2,3,3,1,0,0};
                
                // Act
                int minJumps = new Solution().Jump(nums);
                
                // Assert
                Assert.AreEqual(3, minJumps);
            }

            [TestCase(2, 4)]
            [TestCase(4, 6)]
            [TestCase(1, 8)]
            public void TestArrayToNodeIndex(int arrayIndex, int expectedNodeIndex)
            {
                // Arrange
                var st = new SegmentTree(5);

                // Act
                var nodeIndex = st.NodeIndexFromArrayIndex(arrayIndex);

                // Assert
                Assert.AreEqual(expectedNodeIndex, nodeIndex);
            }

            [Test]
            public void TestAddSegmentTree1()
            {
                // Arrange
                var st = new SegmentTree(5);

                // Act
                st.Set(4, 1);
                st.Set(3, 2);

                // Assert
                Assert.AreEqual(1, st._data[6]);
                Assert.AreEqual(2, st._data[5]);

                Assert.AreEqual(1, st._data[2]);
                Assert.AreEqual(0, st._data[0]);
            }

            [Test]
            public void TestAddSegmentTree2()
            {
                // Arrange
                var st = new SegmentTree(5);

                // Act
                st.Set(4, 0);
                st.Set(3, 1);
                st.Set(2, 2);
                st.Set(1, 1);
                st.Set(0, 2);

                // Assert
                Assert.AreEqual(0, st._data[6]);
                Assert.AreEqual(1, st._data[5]);
                Assert.AreEqual(2, st._data[4]);
                Assert.AreEqual(1, st._data[8]);
                Assert.AreEqual(2, st._data[7]);

                Assert.AreEqual(0, st._data[2]);
                Assert.AreEqual(1, st._data[3]);

                Assert.AreEqual(1, st._data[1]);

                Assert.AreEqual(0, st._data[0]);
            }

            [TestCase(0, 4, 0)]
            [TestCase(2, 2, 2)]
            [TestCase(3, 4, 0)]
            [TestCase(0, 2, 1)]
            [TestCase(1, 3, 1)]
            public void TestQuerySegmentTree(int minIdx, int maxIdx, int expected)
            {
                // Arrange
                var st = new SegmentTree(5);
                st.Set(4, 0);
                st.Set(3, 1);
                st.Set(2, 2);
                st.Set(1, 1);
                st.Set(0, 2);

                // Act // Assert
                Assert.AreEqual(expected, st.Query(minIdx, maxIdx));
            }
        }
    }
}