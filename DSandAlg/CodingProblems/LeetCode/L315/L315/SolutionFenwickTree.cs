#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace L315
{
    public class SolutionFenwickTree
    {
        public IList<int> CountSmaller(int[] nums)
        {
            if (nums.Length == 0)
            {
                return nums;
            }

            var result = new int[nums.Length];
            var fenwickTreeLength = nums.Length + 1;

            var fenwickTree = new FenwickTree(nums.Length);

            // SS: sort numbers, O(N log N)
            var enumerated = nums.Select((i, idx) => (i, idx)).OrderBy(t => t.i).ToArray();

            for (var i = 0; i < enumerated.Length; i++)
            {
                var (value, idx) = enumerated[i];

                var idx2 = fenwickTreeLength - (idx + 1);

                var c = fenwickTree.Sum(idx2);
                result[idx] = c;

                fenwickTree.Add(idx2, 1);
            }

            return result;
        }

        private class FenwickTree
        {
            private readonly int[] _data;

            public FenwickTree(int size)
            {
                _data = new int[size + 1];
            }

            private int Lsb(int idx)
            {
                return idx & -idx;
            }

            public void Add(int idx, int k)
            {
                // SS: add k to element with index 'idx'
                while (idx < _data.Length)
                {
                    _data[idx] += k;
                    idx += Lsb(idx);
                }
            }

            public int Sum(int idx)
            {
                var sum = 0;
                while (idx > 0)
                {
                    sum += _data[idx];
                    idx -= Lsb(idx);
                }

                return sum;
            }
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test17()
            {
                // Arrange
                int[] nums = {3, 7, 5, 2, 4, 1};

                // Act
                var result = new SolutionFenwickTree().CountSmaller(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 4, 3, 1, 1, 0}, result);
            }

            [Test]
            public void Test23()
            {
                // Arrange
                int[] nums = {3, 5, 7, 2, 4, 1};

                // Act
                var result = new SolutionFenwickTree().CountSmaller(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 3, 3, 1, 1, 0}, result);
            }

            [Test]
            public void Test35()
            {
                // Arrange
                int[] nums = {5, 2, 6, 1};

                // Act
                var result = new SolutionFenwickTree().CountSmaller(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 1, 1, 0}, result);
            }

            [Test]
            public void Test41()
            {
                // Arrange
                int[] nums = {2, 0, 1};

                // Act
                var result = new SolutionFenwickTree().CountSmaller(nums);

                // Assert
                CollectionAssert.AreEqual(new[] {2, 0, 0}, result);
            }
        }
    }
}