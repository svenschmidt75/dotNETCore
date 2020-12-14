#region

using System;
using System.Collections.Generic;
using System.Linq;
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
            // SS: runtime ocmplexity: O(n), from Tushar, https://www.youtube.com/watch?v=ZmnqCZp9bBs
            
            var stack = new Stack<int>();

            var maxArea = 0;
            int area;

            for (var i = 0; i < heights.Length; i++)
            {
                if (stack.Any() == false || heights[i] >= heights[stack.Peek()])
                {
                    stack.Push(i);
                }
                else
                {
                    while (stack.Any())
                    {
                        var idx = stack.Pop();

                        if (stack.Any() == false)
                        {
                            area = heights[idx] * i;
                        }
                        else
                        {
                            area = heights[idx] * (i - stack.Peek() - 1);
                        }

                        maxArea = Math.Max(maxArea, area);

                        if (stack.Any() == false || heights[stack.Peek()] <= heights[i])
                        {
                            break;
                        }
                    }

                    stack.Push(i);
                }
            }

            while (stack.Any())
            {
                var idx = stack.Pop();

                if (stack.Any() == false)
                {
                    area = heights[idx] * heights.Length;
                }
                else
                {
                    area = heights[idx] * (heights.Length - stack.Peek() - 1);
                }

                maxArea = Math.Max(maxArea, area);
            }

            return maxArea;
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

            [Test]
            public void Test8()
            {
                // Arrange
                int[] heights = {1, 2, 2};

                // Act
                var maxArea = new Solution().LargestRectangleArea(heights);

                // Assert
                Assert.AreEqual(4, maxArea);
            }
        }
    }
}