#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace L713
{
    public class Solution
    {
        public int NumSubarrayProductLessThanK(int[] nums, int k)
        {
            var n = 0;

            var intervals = new List<(List<int> subarray, double max)>();

            for (var i = 0; i < nums.Length; i++)
            {
                var x = nums[i];
                if (x < k)
                {
                    Console.WriteLine($"[{x}]");
                    n++;
                }

                var newIntervals = new List<(List<int> subarray, double max)>();

                foreach (var tuple in intervals)
                {
                    if (x < tuple.max)
                    {
                        Console.Write("[");
                        for (var j = 0; j < tuple.subarray.Count; j++)
                        {
                            Console.Write($"{tuple.subarray[j]}, ");
                        }

                        Console.WriteLine($"{x}]");

                        n++;
                        tuple.subarray.Add(x);
                        var m = tuple.max / x;
                        newIntervals.Add((tuple.subarray, m));
                    }
                }

                intervals = newIntervals;

                if (x < k)
                {
                    intervals.Add((new List<int> {x}, k / (double) x));
                }
            }

            return n;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var input = new[] {10, 5, 2, 6};
            var k = 100;

            // Act
            var n = new Solution().NumSubarrayProductLessThanK(input, k);

            // Assert
            Assert.AreEqual(8, n);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var input = new[] {10, 9, 10, 4, 3, 8, 3, 3, 6, 2, 10, 10, 9, 3};
            var k = 19;

            // Act
            var n = new Solution().NumSubarrayProductLessThanK(input, k);

            // Assert
            Assert.AreEqual(18, n);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var input = Enumerable.Repeat(1, 48123).ToArray();
            var k = 5;

            // Act
            var n = new Solution().NumSubarrayProductLessThanK(input, k);

            // Assert
            Assert.AreEqual(8, n);
        }
    }
}