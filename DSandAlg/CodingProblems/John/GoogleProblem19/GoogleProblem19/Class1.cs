#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace GoogleProblem19
{
    /// <summary>
    ///     Given a 1d array with unique integers. Apply compression with these rules:
    ///     1. smallest value is array is 1 (after compression)
    ///     2. largest value is as small as possible
    ///     3. preserve ordering, i.e. if x_i < x_j before sort, then c_i
    ///     < c_j after compression
    ///         Example:
    ///         Input: [10, 20, 30]
    ///         Output: [1, 2, 3]
    ///         Follow-up: Similar, but given 2 d array. The above rules apply by column and row.
    ///         Example:
    ///         Input: [[20, 30, 40],
    ///         [50, 60, 10]]
    ///         Output: [[1, 2, 3]
    ///     ,[2, 3, 1]]
    /// </summary>
    public class Solution
    {
        public static int[] Solve1D(int[] input)
        {
            var result = new int[input.Length];

            // sort at O(n log n)
            var sorted = input.Select((v, idx) => (idx, v)).OrderBy(t => t.v).ToArray();

            for (var i = 0; i < sorted.Length; i++)
            {
                var idx = sorted[i].idx;
                result[idx] = i + 1;
            }

            return result;
        }

        public static int[][] Solve2D(int[][] input)
        {
            // SS: runtime complexity: O(#rows * #cols * log #cols)
            // space complexity: O(n)

            var result = new int[input.Length][];

            for (var i = 0; i < input.Length; i++)
            {
                result[i] = new int[input[i].Length];
            }

            var hash = new HashSet<int>[input[0].Length];
            for (var i = 0; i < input[0].Length; i++)
            {
                hash[i] = new HashSet<int>();
            }

            for (var row = 0; row < input.Length; row++)
            {
                var r = input[row];

                // sort at O(n log n)
                var sorted = r.Select((v, idx) => (idx, v)).OrderBy(t => t.v).ToArray();

                var j = 1;
                for (var i = 0; i < sorted.Length; i++)
                {
                    var col = sorted[i].idx;
                    while (hash[col].Contains(j))
                    {
                        j++;
                    }

                    hash[col].Add(j);

                    result[row][col] = j;
                    j++;
                }
            }

            return result;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var input = new[] {10, 20, 30};

            // Act
            var result = Solution.Solve1D(input);

            // Act
            CollectionAssert.AreEqual(new[] {1, 2, 3}, result);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var input = new[] {5, 7, 9, 3, 6, 1, 2};

            // Act
            var result = Solution.Solve1D(input);

            // Act
            CollectionAssert.AreEqual(new[] {4, 6, 7, 3, 5, 1, 2}, result);
        }

        [Test]
        public void Test2D_1()
        {
            // Arrange
            var input = new[] {new[] {20, 30, 40}, new[] {50, 60, 10}};

            // Act
            var result = Solution.Solve2D(input);

            // Act
            CollectionAssert.AreEqual(new[] {new[] {1, 2, 3}, new[] {2, 3, 1}}, result);
        }

        [Test]
        public void Test2D_2()
        {
            // Arrange
            var input = new[] {new[] {20, 30, 40}, new[] {60, 50, 10}};

            // Act
            var result = Solution.Solve2D(input);

            // Act
            CollectionAssert.AreEqual(new[] {new[] {1, 2, 3}, new[] {4, 3, 1}}, result);
        }
    }
}