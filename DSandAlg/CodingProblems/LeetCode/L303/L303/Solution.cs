#region

using NUnit.Framework;

#endregion

// Problem: 303. Range Sum Query - Immutable
// URL: https://leetcode.com/problems/range-sum-query-immutable/

namespace LeetCode
{
    public class NumArray
    {
        private readonly int[] _fennwickTree;

        public NumArray(int[] nums)
        {
            _fennwickTree = new int[nums.Length + 1];

            // SS: construct Fenwick tree
            for (var i = 0; i < nums.Length; i++)
            {
                var idx = i + 1;
                var v = nums[i];

                while (idx < _fennwickTree.Length)
                {
                    _fennwickTree[idx] += v;
                    idx += Lsb(idx);
                }
            }
        }

        private int Lsb(int idx)
        {
            return idx & -idx;
        }

        private int Query(int idx)
        {
            var sum = 0;
            while (idx > 0)
            {
                sum += _fennwickTree[idx];
                idx -= Lsb(idx);
            }

            return sum;
        }

        public int SumRange(int left, int right)
        {
            var l = Query(left);
            var r = Query(right + 1);
            var d = r - l;
            return d;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var a = new NumArray(new[] {-2, 0, 3, -5, 2, -1});

                // Act
                // Assert
                Assert.AreEqual(1, a.SumRange(0, 2));
                Assert.AreEqual(-1, a.SumRange(2, 5));
                Assert.AreEqual(-3, a.SumRange(0, 5));
            }
        }
    }
}