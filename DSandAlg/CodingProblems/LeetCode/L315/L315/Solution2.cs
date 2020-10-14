#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace L315
{
    public class Solution2
    {
        public IList<int> CountSmaller(int[] nums)
        {
            if (nums.Length == 0)
            {
                return nums;
            }

            var result = new int[nums.Length];

            // SS: sort results, O(N log N)
            var enumerated = nums.Select((i, idx) => (i, idx)).OrderBy(t => t.i).ToArray();

            var segmentTree = new SegmentTree(nums.Length);

            for (var i = 0; i < enumerated.Length; i++)
            {
                var (value, idx) = enumerated[i];
                var cnt = segmentTree.Insert(idx);
                result[idx] = cnt - 1;
            }

            return result;
        }

        private class SegmentTree
        {
            private readonly int[] _data;
            private readonly int _n;

            public SegmentTree(int n)
            {
                _n = n;
                var log = Math.Log10(n) / Math.Log10(2.0);
                var exp = (int) Math.Ceiling(log) + 1;
                var treeSize = (int) Math.Pow(2, exp);
                _data = new int[treeSize];
            }

            public int Insert(int position)
            {
                // SS: insert into position. Assumed we call this with values in
                // ascending order.
                var min = 0;
                var max = _n - 1;
                var value = Insert(position, 0, 0, min, max);
                return value;
            }

            private int Insert(int position, int value, int nodeIdx, int min, int max)
            {
                if (min >= _n)
                {
                    return 0;
                }

                if (min == max)
                {
                    // SS: leaf node, insert
                    if (position == min)
                    {
                        // SS: leaf node, insert
                        _data[nodeIdx] = value + 1;
                        return _data[nodeIdx];
                    }

                    // SS: return the larger of the values
                    var v1 = Math.Max(_data[nodeIdx], value);
                    return v1;
                }

                if (position < min)
                {
                    // SS: return node value
                    return _data[nodeIdx];
                }

                // SS: can this case happen?
                if (position > max)
                {
                    return value;
                }

                // SS we have overlap and need to test both
                var mid = (min + max) / 2;

                var rightNodeIdx = 2 * nodeIdx + 2;
                var r = Insert(position, value, rightNodeIdx, mid + 1, max);

                var leftNodeIdx = 2 * nodeIdx + 1;
                var l = Insert(position, r, leftNodeIdx, min, mid);

                _data[nodeIdx]++;

                return l;
            }
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test11()
            {
                // Arrange
                var segmentTree = new SegmentTree(6);

                // Act
                var value = segmentTree.Insert(5);

                // Assert
                Assert.AreEqual(1, value);
            }

            [Test]
            public void Test12()
            {
                // Arrange
                var segmentTree = new SegmentTree(6);
                segmentTree.Insert(5);

                // Act
                var value = segmentTree.Insert(3);

                // Assert
                Assert.AreEqual(2, value);
            }

            [Test]
            public void Test13()
            {
                // Arrange
                var segmentTree = new SegmentTree(6);
                segmentTree.Insert(5);
                segmentTree.Insert(3);

                // Act
                var value = segmentTree.Insert(0);

                // Assert
                Assert.AreEqual(3, value);
            }

            [Test]
            public void Test14()
            {
                // Arrange
                var segmentTree = new SegmentTree(6);
                segmentTree.Insert(5);
                segmentTree.Insert(3);
                segmentTree.Insert(0);

                // Act
                var value = segmentTree.Insert(4);

                // Assert
                Assert.AreEqual(2, value);
            }

            [Test]
            public void Test15()
            {
                // Arrange
                var segmentTree = new SegmentTree(6);
                segmentTree.Insert(5);
                segmentTree.Insert(3);
                segmentTree.Insert(0);
                segmentTree.Insert(4);

                // Act
                var value = segmentTree.Insert(2);

                // Assert
                Assert.AreEqual(4, value);
            }

            [Test]
            public void Test16()
            {
                // Arrange
                var segmentTree = new SegmentTree(6);
                segmentTree.Insert(5);
                segmentTree.Insert(3);
                segmentTree.Insert(0);
                segmentTree.Insert(4);
                segmentTree.Insert(2);

                // Act
                var value = segmentTree.Insert(1);

                // Assert
                Assert.AreEqual(5, value);
            }

            [Test]
            public void Test17()
            {
                // Arrange
                int[] nums = {3, 7, 5, 2, 4, 1};

                // Act
                var result = new Solution2().CountSmaller(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 4, 3, 1, 1, 0}, result);
            }

            [Test]
            public void Test21()
            {
                // Arrange
                var segmentTree = new SegmentTree(6);
                segmentTree.Insert(5);
                segmentTree.Insert(3);
                segmentTree.Insert(0);
                segmentTree.Insert(4);

                // Act
                var value = segmentTree.Insert(1);

                // Assert
                Assert.AreEqual(4, value);
            }

            [Test]
            public void Test22()
            {
                // Arrange
                var segmentTree = new SegmentTree(6);
                segmentTree.Insert(5);
                segmentTree.Insert(3);
                segmentTree.Insert(0);
                segmentTree.Insert(4);
                segmentTree.Insert(1);

                // Act
                var value = segmentTree.Insert(2);

                // Assert
                Assert.AreEqual(4, value);
            }

            [Test]
            public void Test23()
            {
                // Arrange
                int[] nums = {3, 5, 7, 2, 4, 1};

                // Act
                var result = new Solution2().CountSmaller(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 3, 3, 1, 1, 0}, result);
            }

            [Test]
            public void Test31()
            {
                // Arrange
                var segmentTree = new SegmentTree(4);

                // Act
                var value = segmentTree.Insert(3);

                // Assert
                Assert.AreEqual(1, value);
            }

            [Test]
            public void Test32()
            {
                // Arrange
                var segmentTree = new SegmentTree(4);
                segmentTree.Insert(3);

                // Act
                var value = segmentTree.Insert(1);

                // Assert
                Assert.AreEqual(2, value);
            }

            [Test]
            public void Test33()
            {
                // Arrange
                var segmentTree = new SegmentTree(4);
                segmentTree.Insert(3);
                segmentTree.Insert(1);

                // Act
                var value = segmentTree.Insert(0);

                // Assert
                Assert.AreEqual(3, value);
            }

            [Test]
            public void Test34()
            {
                // Arrange
                var segmentTree = new SegmentTree(4);
                segmentTree.Insert(3);
                segmentTree.Insert(1);
                segmentTree.Insert(0);

                // Act
                var value = segmentTree.Insert(2);

                // Assert
                Assert.AreEqual(2, value);
            }

            [Test]
            public void Test35()
            {
                // Arrange
                int[] nums = {5, 2, 6, 1};

                // Act
                var result = new Solution2().CountSmaller(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 1, 1, 0}, result);
            }

            [Test]
            public void Test41()
            {
                // Arrange
                int[] nums = {2, 0, 1};

                // Act
                var result = new Solution2().CountSmaller(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 0, 0}, result);
            }
            
        }
    }
}