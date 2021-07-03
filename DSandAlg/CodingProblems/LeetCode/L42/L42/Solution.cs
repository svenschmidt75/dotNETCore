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
            // return Trap3(height);
            return Trap4(height);
        }

        private int Trap4(int[] height)
        {
            // SS: find tallest peak
            int maxHeight = 0;
            int maxHeightIdx = 0;

            for (int i = 0; i < height.Length; i++)
            {
                if (height[i] > maxHeight)
                {
                    maxHeight = height[i];
                    maxHeightIdx = i;
                }
            }

            int water = 0;

            int maxLeftHeight = 0;
            
            int idx = 1;
            int ph = height[0];
            while (idx < height.Length && idx < maxHeightIdx)
            {
                maxLeftHeight = Math.Max(maxLeftHeight, ph);

                int h = height[idx];
                idx++;

                if (h  - maxLeftHeight <= 0)
                {
                    ph = h;
                    continue;
                }

                int dh = Math.Min(maxHeight, maxLeftHeight - h);
                water += dh;
                
                ph = h;
            }

            return water;
        }

        public int Trap3(int[] height)
        {
            // SS: Larry's solution, https://www.youtube.com/watch?v=PlYQ5LAZDtI&t=5s
            // runtime complexity: O(N)
            // space complexity: O(N) due to array reverse (we could do in-place...)

            int SolveIncreasing(int[] height)
            {
                var peak = 0;
                var water = 0;

                for (var i = 0; i < height.Length; i++)
                {
                    var h = height[i];
                    if (h >= peak)
                    {
                        peak = h;
                    }
                    else
                    {
                        water += peak - h;
                    }
                }

                return water;
            }

            if (height.Length == 0)
            {
                return 0;
            }

            // SS: The solution idea is to find the highest peak and split the problem into two
            // smaller sub problems. One to the left of the peak, the other to it's right.
            // At each position, we calculate the entire water column.

            var peakIndex = 0;
            for (var i = 0; i < height.Length; i++)
            {
                var h = height[i];
                if (h > height[peakIndex])
                {
                    peakIndex = i;
                }
            }

            // solve the problem from 0 to peakIndex (excluding the peak itself)
            return SolveIncreasing(height[..peakIndex]) + SolveIncreasing(height[(peakIndex + 1)..].Reverse().ToArray());
        }

        public int Trap2(int[] height)
        {
            // SS: runtime complexity: O(n)
            // space complexity: O(n)

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