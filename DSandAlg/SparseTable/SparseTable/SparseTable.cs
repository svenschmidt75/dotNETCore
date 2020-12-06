#region

using System;
using NUnit.Framework;

#endregion

namespace SparseTable
{
    public class SparseTable
    {
        private readonly int[][] _dp;
        private readonly Func<int, int, int> _func;
        private readonly int _p;

        public SparseTable(int[] input, Func<int, int, int> func)
        {
            // SS: construct the sparse table
            // runtime complexity: O(N log N)

            _func = func;

            // SS: number of columns in dp array
            _p = (int) Math.Log2(input.Length);

            _dp = new int[_p + 1][];
            _dp[0] = new int[input.Length];

            // first row is input array
            Array.Copy(input, _dp[0], input.Length);

            var k = 1;

            for (var i = 1; i <= _p; i++)
            {
                _dp[i] = new int[input.Length];

                for (var j = 0; j < input.Length; j++)
                {
                    if (j + 2 * k - 1 >= input.Length)
                    {
                        break;
                    }

                    var v1 = _dp[i - 1][j];
                    var v2 = _dp[i - 1][j + k];
                    var r = func(v1, v2);
                    _dp[i][j] = r;
                }

                k <<= 1;
            }
        }

        public int QueryOverlapFriendly(int min, int max)
        {
            // SS: interval [min, max]
            // runtime complexity: O(1)

            var length = max - min + 1;
            var w = (int) Math.Log2(length);
            var v1 = _dp[w][min];

            var rangeWidth = (int) Math.Pow(2, w);
            var v2 = _dp[w][max - rangeWidth + 1];

            var r = _func(v1, v2);
            return r;
        }

        public int QueryNonOverlapFriendly(int min, int max)
        {
            // SS: interval [min, max]
            // runtime complexity: O(log N)

            var length = max - min + 1;
            var w = (int) Math.Log2(length);
            var v1 = _dp[w][min];

            var rangeWidth = (int) Math.Pow(2, w);
            length -= rangeWidth;
            var offset = rangeWidth;

            while (length > 0)
            {
                w = (int) Math.Log2(length);
                var v2 = _dp[w][min + offset];
                v1 = _func(v1, v2);

                rangeWidth = (int) Math.Pow(2, w);
                length -= rangeWidth;
                offset += rangeWidth;
            }

            return v1;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(1, 11, -1)]
            [TestCase(2, 7, 1)]
            [TestCase(10, 10, 7)]
            public void Test1(int min, int max, int expectedMin)
            {
                // Arrange
                int[] input = {4, 2, 3, 7, 1, 5, 3, 3, 9, 6, 7, -1, 4};

                // Act
                var st = new SparseTable(input, Math.Min);

                // Assert
                Assert.AreEqual(expectedMin, st.QueryOverlapFriendly(min, max));
            }

            [TestCase(1, 11, -1)]
            [TestCase(2, 7, 1)]
            [TestCase(10, 10, 7)]
            public void Test2(int min, int max, int expectedMin)
            {
                // Arrange
                int[] input = {4, 2, 3, 7, 1, 5, 3, 3, 9, 6, 7, -1, 4};

                // Act
                var st = new SparseTable(input, Math.Min);

                // Assert
                Assert.AreEqual(expectedMin, st.QueryNonOverlapFriendly(min, max));
            }
        }
    }
}