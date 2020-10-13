#region

using System;
using NUnit.Framework;

#endregion

// 1051. Height Checker
// https://leetcode.com/problems/height-checker/

namespace L1051
{
    public class Solution
    {
        public int HeightChecker(int[] heights)
        {
            var sorted = new int[heights.Length];
            Array.Copy(heights, sorted, heights.Length);
            Array.Sort(sorted);

            var diff = 0;
            for (var i = 0; i < heights.Length; i++)
            {
                if (heights[i] != sorted[i])
                {
                    diff++;
                }
            }

            return diff;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var heights = new[] {1, 1, 4, 2, 1, 3};

                // Act
                var result = new Solution().HeightChecker(heights);

                // Assert
                Assert.AreEqual(3, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var heights = new[] {5, 1, 2, 3, 4};

                // Act
                var result = new Solution().HeightChecker(heights);

                // Assert
                Assert.AreEqual(5, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var heights = new[] {1, 2, 3, 4, 5};

                // Act
                var result = new Solution().HeightChecker(heights);

                // Assert
                Assert.AreEqual(0, result);
            }
        }
    }
}