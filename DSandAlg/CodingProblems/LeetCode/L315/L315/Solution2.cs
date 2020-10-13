#region

using System;
using NUnit.Framework;

#endregion

namespace L315
{
    public class Solution2
    {
        private class SegmentTree
        {
            private readonly int _n;
            private readonly int[] _data;

            public SegmentTree(int n)
            {
                _n = n;
                var exp = (int) Math.Ceiling(Math.Log2(n)) + 1;
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
        }
    }
}