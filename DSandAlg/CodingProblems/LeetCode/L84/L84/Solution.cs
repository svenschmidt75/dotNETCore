#region

using System;
using NUnit.Framework;

#endregion

// Problem: 84. Largest Rectangle in Histogram
// URL: https://leetcode.com/problems/largest-rectangle-in-histogram/

namespace Leetcode
{
    public class Solution
    {
        public int LargestRectangleArea(int[] heights)
        {
            if (heights.Length == 1)
            {
                return heights[0];
            }

            var dp = new int[heights.Length];

            var maxHeight = 0;

            var j = 0;
            var k = 0;
            while (j < heights.Length)
            {
                while (j < heights.Length && heights[j] == 0)
                {
                    j++;
                }

                // SS: find next peak
                while (j <= heights.Length - 2 && heights[j] > 0 && heights[j] < heights[j + 1])
                {
                    j++;
                }

                if (j == heights.Length)
                {
                    break;
                }

                // SS: find left min
                var left = j;
                while (left > k && heights[left - 1] > 0 && heights[left - 1] < heights[left])
                {
                    left--;
                }

                var right = j;
                while (right <= heights.Length - 2 && heights[right + 1] > 0 && heights[right + 1] < heights[right])
                {
                    right++;
                }

                // SS: analyze between left and right
                var i1 = j;
                var i2 = j;
                var h = j;
                while (true)
                {
                    var w = i2 - i1 + 1;
                    var h2 = heights[h] + dp[h];
                    var area = w * h2;
                    maxHeight = Math.Max(maxHeight, area);

                    if (i1 > left && i2 < right)
                    {
                        if (heights[i1 - 1] >= heights[i2 + 1])
                        {
                            h = i1 - 1;
                            i1--;
                        }
                        else
                        {
                            h = i2 + 1;
                            i2++;
                        }
                    }
                    else if (i1 > left)
                    {
                        h = i1 - 1;
                        i1--;
                    }
                    else if (i2 < right)
                    {
                        h = i2 + 1;
                        i2++;
                    }
                    else
                    {
                        // extend to right
                        if (right == heights.Length - 1)
                        {
                            return maxHeight;
                        }

                        var p = right + 1;
                        while (p < heights.Length && heights[p] - dp[p] > 0 && heights[p] >= heights[right])
                        {
                            p++;
                        }

                        w = p - left;
                        h2 = heights[right] + dp[right];
                        area = w * h2;
                        maxHeight = Math.Max(maxHeight, area);

                        h = heights[right];
                        for (var i = right; i < p; i++)
                        {
                            dp[i] += h;
                            heights[i] -= h;
                        }

                        // set k
                        k = right + 1;
                        j = k;
                        break;
                    }
                }
            }

            return maxHeight;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] heights = {2, 1, 5, 6, 2, 3};

                // Act
                var maxArea = new Solution().LargestRectangleArea(heights);

                // Assert
                Assert.AreEqual(10, maxArea);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] heights = {3, 2, 6, 5, 1, 2};

                // Act
                var maxArea = new Solution().LargestRectangleArea(heights);

                // Assert
                Assert.AreEqual(10, maxArea);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] heights = {5, 11, 5, 4};

                // Act
                var maxArea = new Solution().LargestRectangleArea(heights);

                // Assert
                Assert.AreEqual(16, maxArea);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] heights = {5, 8, 9, 7, 6};

                // Act
                var maxArea = new Solution().LargestRectangleArea(heights);

                // Assert
                Assert.AreEqual(25, maxArea);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] heights = {5, 8, 9};

                // Act
                var maxArea = new Solution().LargestRectangleArea(heights);

                // Assert
                Assert.AreEqual(16, maxArea);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] heights = {2, 1, 2, 1, 2, 1, 2, 3, 2};

                // Act
                var maxArea = new Solution().LargestRectangleArea(heights);

                // Assert
                Assert.AreEqual(9, maxArea);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[] heights = {2, 0, 4, 5, 1, 2};

                // Act
                var maxArea = new Solution().LargestRectangleArea(heights);

                // Assert
                Assert.AreEqual(8, maxArea);
            }
        }
    }
}