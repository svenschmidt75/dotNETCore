#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 42. Trapping Rain Water
// URL: https://leetcode.com/problems/trapping-rain-water/

namespace LeetCode42
{
    public class Solution
    {
        public int Trap(int[] height)
        {
            if (height.Length < 2)
            {
                return 0;
            }

            var water = 0;

            var stack = new Stack<int>();

            var i = 0;
            var leftHeight = 0;

            while (i <= height.Length - 2)
            {
                stack.Push(i);
                leftHeight = Math.Max(leftHeight, height[i]);

                if (height[i] < height[i + 1])
                {
                    var rightHeight = height[i + 1];
                    var maxHeight = Math.Min(leftHeight, rightHeight);

                    var rightIndex = i + 1;

                    // SS: compute trapped water
                    var idx1 = stack.Pop();
                    var h1 = height[idx1];

                    while (stack.Any())
                    {
                        var idx2 = stack.Peek();
                        var h2 = height[idx2];

                        if (h2 <= maxHeight)
                        {
                            stack.Pop();
                        }

                        var w = rightIndex - idx2 - 1;
                        var h = Math.Min(h2, maxHeight);
                        var waterLevel = h - h1;

                        var localWater = waterLevel * w;

                        if (localWater >= 0)
                        {
                            water += localWater;
                            h1 = h;
                        }

                        if (h2 >= maxHeight)
                        {
                            break;
                        }
                    }
                }

                i++;
            }

            return water;
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] heights = {0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1};

                // Act
                var water = new Solution().Trap(heights);

                // Assert
                Assert.AreEqual(6, water);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] heights = {0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1, 5};

                // Act
                var water = new Solution().Trap(heights);

                // Assert
                Assert.AreEqual(11, water);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] heights = {4, 2, 0, 3, 2, 5};

                // Act
                var water = new Solution().Trap(heights);

                // Assert
                Assert.AreEqual(9, water);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var heights = Array.Empty<int>();

                // Act
                var water = new Solution().Trap(heights);

                // Assert
                Assert.AreEqual(0, water);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] heights = {1};

                // Act
                var water = new Solution().Trap(heights);

                // Assert
                Assert.AreEqual(0, water);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] heights = {1, 1};

                // Act
                var water = new Solution().Trap(heights);

                // Assert
                Assert.AreEqual(0, water);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[] heights = {3, 2, 1, 0, 2};

                // Act
                var water = new Solution().Trap(heights);

                // Assert
                Assert.AreEqual(3, water);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                int[] heights = {3, 2, 0, 1, 2};

                // Act
                var water = new Solution().Trap(heights);

                // Assert
                Assert.AreEqual(3, water);
            }

            [Test]
            public void Test9()
            {
                // Arrange
                int[] heights = {5, 5, 4, 7, 8, 2, 6, 9, 4, 5};

                // Act
                var water = new Solution().Trap(heights);

                // Assert
                Assert.AreEqual(10, water);
            }
        }
    }
}