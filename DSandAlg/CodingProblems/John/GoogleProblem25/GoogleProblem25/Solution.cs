#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace GoogleProblem25
{
    /// <summary>
    ///     Given a histogram, calculate the volume of water that would be trapped after
    ///     rain shower.
    /// </summary>
    public class Solution
    {
        public int Solve1(int[] histogram)
        {
            if (histogram.Length < 2)
            {
                return 0;
            }

            var stack = new Stack<(int, int)>();

            var i = 0;

            var waterVolume = 0;

            while (i < histogram.Length)
            {
                var waterLevel = histogram[i];

                if (stack.Any() && stack.Peek().Item2 <= waterLevel)
                {
                    // SS: calculate water volume
                    var (_, minWaterLevel) = stack.Pop();

                    while (stack.Any() && stack.Peek().Item2 >= minWaterLevel)
                    {
                        var (idx, wl) = stack.Peek();

                        var maxWaterLevel = Math.Min(waterLevel, wl);

                        var width = i - idx - 1;
                        var waterColumn = maxWaterLevel - minWaterLevel;
                        var vl = width * waterColumn;
                        waterVolume += vl;

                        // SS: pop off item from stack?
                        if (wl < waterLevel)
                        {
                            stack.Pop();
                            
                            // SS: adjust water level
                            minWaterLevel = Math.Max(minWaterLevel, wl);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                stack.Push((i, waterLevel));
                i++;
            }

            return waterVolume;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                /*
                 * 4             **
                 * 3    ** 00 00 **
                 * 2    ** ** 00 **
                 * 1 ** ** ** ** **
                 */

                // Arrange
                var histogram = new[] {1, 3, 2, 1, 4};

                // Act
                var result = new Solution().Solve1(histogram);

                // Assert
                Assert.AreEqual(3, result);
            }

            [Test]
            public void Test2()
            {
                /*
                 * 4             **
                 * 3    ** ** ** **
                 * 2    ** ** ** **
                 * 1 ** ** ** ** **
                 */

                // Arrange
                var histogram = new[] {1, 3, 3, 3, 4};

                // Act
                var result = new Solution().Solve1(histogram);

                // Assert
                Assert.AreEqual(0, result);
            }

            [Test]
            public void Test3()
            {
                /*
                 * 4 **
                 * 3 ** ** ** **
                 * 2 ** ** ** **
                 * 1 ** ** ** ** **
                 */

                // Arrange
                var histogram = new[] {4, 3, 3, 3, 1};

                // Act
                var result = new Solution().Solve1(histogram);

                // Assert
                Assert.AreEqual(0, result);
            }

            [Test]
            public void Test4()
            {
                /*
                 * 4 **
                 * 3 ** ** 00 00 00 **
                 * 2 ** ** 00 ** 00 ** 00 **
                 * 1 ** ** ** ** ** ** ** **
                 */

                // Arrange
                var histogram = new[] {4, 3, 1, 2, 1, 3, 1, 2};

                // Act
                var result = new Solution().Solve1(histogram);

                // Assert
                Assert.AreEqual(6, result);
            }

            [Test]
            public void Test5()
            {
                /*
                 * 3       **
                 * 2 ** 00 **
                 * 1 ** ** **
                 */

                // Arrange
                var histogram = new[] {2, 1, 3};

                // Act
                var result = new Solution().Solve1(histogram);

                // Assert
                Assert.AreEqual(1, result);
            }

            [Test]
            public void Test6()
            {
                /*
                 * 4 **
                 * 3 ** ** 00 00 **
                 * 2 ** ** ** 00 **
                 * 1 ** ** ** ** **
                 */

                // Arrange
                var histogram = new[] {4, 3, 2, 1, 3};

                // Act
                var result = new Solution().Solve1(histogram);

                // Assert
                Assert.AreEqual(3, result);
            }

            [Test]
            public void Test7()
            {
                /*
                 * 4 ** 00 00 00 00 00 00 00 **
                 * 3 ** ** 00 00 00 ** 00 00 **
                 * 2 ** ** 00 ** 00 ** 00 ** **
                 * 1 ** ** ** ** ** ** ** ** **
                 */

                // Arrange
                var histogram = new[] {4, 3, 1, 2, 1, 3, 1, 2, 4};

                // Act
                var result = new Solution().Solve1(histogram);

                // Assert
                Assert.AreEqual(15, result);
            }
        }
    }
}