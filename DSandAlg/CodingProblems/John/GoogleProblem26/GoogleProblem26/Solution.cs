#region

using System;
using System.Linq;
using NUnit.Framework;

#endregion

namespace GoogleProblem26
{
    public class Solution
    {
        private int[] Solve1(int[][] input, int k)
        {
            // SS: Given an array with m sorted lists each with n integers, return
            // the k smallest elements.

            // SS: sort, then extract k smallest elements
            // runtime complexity: O(m n log (m n))

            var m = input.Length;

            if (m < 1)
            {
                return new int[0];
            }

            var n = input[0].Length;

            if (k > m * n)
            {
                return new int[0];
            }

            var array = new int[m * n];
            for (var i = 0; i < m; i++)
            {
                var sortedList = input[i];
                Array.Copy(sortedList, 0, array, i * n, n);
            }

            Array.Sort(array);

            return array.Take(k).ToArray();
        }

        private int[] Solve2(int[][] input, int k)
        {
            // SS: Given an array with m sorted lists each with n integers, return
            // the k smallest elements.

            // SS: sort in steps, runtime complexity: O(k * m * log m)
            // space complexity: O(m)
            
            // Note: Since we are sorting integers, we could use radix sort for O(k * n * p), where p
            // is the number with the most digits... 

            var m = input.Length;

            if (m < 1)
            {
                return new int[0];
            }

            var n = input[0].Length;

            if (k > m * n)
            {
                return new int[0];
            }

            var result = new int[k];
            var count = 0;

            var positionArray = new int[m];
            var sortArray = new (int rowIdx, int value)[m];

            // SS: extract 1st column
            for (var j = 0; j < m; j++)
            {
                var value = input[j][0];
                sortArray[j] = (j, value);
            }

            sortArray = sortArray.OrderBy(t => t.value).ToArray();

            while (count < k)
            {
                var minElement = sortArray[0];
                result[count++] = minElement.value;

                // SS: move pointer for that row to next element
                positionArray[minElement.rowIdx]++;

                var pos = positionArray[minElement.rowIdx];
                if (pos == n)
                {
                    // SS: end of sorted array reached
                    sortArray[0] = (minElement.rowIdx, int.MaxValue);
                }
                else
                {
                    sortArray[0] = (minElement.rowIdx, input[minElement.rowIdx][positionArray[minElement.rowIdx]]);
                }

                sortArray = sortArray.OrderBy(t => t.value).ToArray();
            }

            return result;
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test11()
            {
                // Arrange
                var input = new[]
                {
                    new[] {1, 2, 3, 4, 5}
                    , new[] {19, 20, 21, 22, 23}
                    , new[] {-7, -6, -5, -4, -3}
                };

                var k = 3;

                // Act
                var result = new Solution().Solve1(input, k);

                // Assert
                CollectionAssert.AreEqual(new[] {-7, -6, -5}, result);
            }

            [Test]
            public void Test12()
            {
                // Arrange
                var input = new[]
                {
                    new[] {1, 2, 3, 4, 5}
                    , new[] {19, 20, 21, 22, 23}
                    , new[] {-7, -6, -5, -4, -3}
                };

                var k = 3;

                // Act
                var result = new Solution().Solve2(input, k);

                // Assert
                CollectionAssert.AreEqual(new[] {-7, -6, -5}, result);
            }

            [Test]
            public void Test21()
            {
                // Arrange
                var input = new[]
                {
                    new[] {1, 100, 112, 121, 145}
                    , new[] {19, 20, 25, 117, 132}
                    , new[] {-7, 18, 31, 52, 151}
                };

                var k = 5;

                // Act
                var result = new Solution().Solve1(input, k);

                // Assert
                CollectionAssert.AreEqual(new[] {-7, 1, 18, 19, 20}, result);
            }

            [Test]
            public void Test22()
            {
                // Arrange
                var input = new[]
                {
                    new[] {1, 100, 112, 121, 145}
                    , new[] {19, 20, 25, 117, 132}
                    , new[] {-7, 18, 31, 52, 151}
                };

                var k = 5;

                // Act
                var result = new Solution().Solve2(input, k);

                // Assert
                CollectionAssert.AreEqual(new[] {-7, 1, 18, 19, 20}, result);
            }

            [TestCase(3)]
            [TestCase(10)]
            [TestCase(15)]
            public void Test3(int k)
            {
                // Arrange
                var input = new[]
                {
                    new[] {1, 100, 112, 121, 145}
                    , new[] {19, 20, 25, 117, 132}
                    , new[] {-7, 18, 31, 52, 151}
                };

                // Act
                var result = new Solution().Solve2(input, k);
                var expectedResult = new Solution().Solve1(input, k);

                // Assert
                CollectionAssert.AreEqual(expectedResult, result);
            }
        }
    }
}