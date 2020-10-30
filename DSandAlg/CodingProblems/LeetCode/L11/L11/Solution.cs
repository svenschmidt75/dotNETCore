#region

using System;
using NUnit.Framework;

#endregion

// 11. Container With Most Water
// https://leetcode.com/problems/container-with-most-water/

namespace L11
{
    public class Solution
    {
        public int MaxArea(int[] height)
        {
//            return MaxAreaNaive(height);
            return MaxAreaBetter(height);
        }

        public int MaxAreaNaive(int[] height)
        {
            var maxArea = 0;

            for (var i = 0; i < height.Length; i++)
            {
                var height1 = height[i];

                for (var j = i + 1; j < height.Length; j++)
                {
                    var height2 = height[j];
                    var area = (j - i) * Math.Min(height1, height2);
                    maxArea = Math.Max(maxArea, area);
                }
            }

            return maxArea;
        }

        public int MaxAreaBetter(int[] height)
        {
            // SS: runtime complexity: O(n)!

            // SS: height has at least 2 elements

            // SS: we assume we have no trailing 0
            var i = 0;
            var j = height.Length - 1;
            var maxArea = 0;

            while (i < j)
            {
                // advance the smaller height to the next taller one
                var h1 = height[i];
                var h2 = height[j];
                var area = Math.Min(height[i], height[j]) * (j - i);
                maxArea = Math.Max(maxArea, area);

                if (h1 <= h2)
                {
                    var k = i + 1;
                    while (k < j && height[k] < h1)
                    {
                        k++;
                    }

                    i = k;
                }
                else
                {
                    var k = j - 1;
                    while (k > i && height[k] < h2)
                    {
                        k--;
                    }

                    j = k;
                }
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
                int[] height = {1, 8, 6, 2, 5, 4, 8, 3, 7};

                // Act
                var maxArea = new Solution().MaxArea(height);

                // Assert
                Assert.AreEqual(49, maxArea);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] height = {1, 1};

                // Act
                var maxArea = new Solution().MaxArea(height);

                // Assert
                Assert.AreEqual(1, maxArea);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                int[] height = {4, 3, 2, 1, 4};

                // Act
                var maxArea = new Solution().MaxArea(height);

                // Assert
                Assert.AreEqual(16, maxArea);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int[] height = {1, 2, 1};

                // Act
                var maxArea = new Solution().MaxArea(height);

                // Assert
                Assert.AreEqual(2, maxArea);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                int[] height = {3, 4, 3, 9, 7, 2, 12, 6};

                // Act
                var maxArea = new Solution().MaxArea(height);

                // Assert
                Assert.AreEqual(27, maxArea);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                int[] height = {1, 8, 4, 2, 1, 0, 0, 0, 0, 1};

                // Act
                var maxArea = new Solution().MaxArea(height);

                // Assert
                Assert.AreEqual(9, maxArea);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                int[] height = {1, 8, 4, 2, 8, 0, 0, 0, 0, 1};

                // Act
                var maxArea = new Solution().MaxArea(height);

                // Assert
                Assert.AreEqual(24, maxArea);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                int[] height = {1, 3, 2, 5, 25, 24, 5};

                // Act
                var maxArea = new Solution().MaxArea(height);

                // Assert
                Assert.AreEqual(24, maxArea);
            }
        }
    }
}