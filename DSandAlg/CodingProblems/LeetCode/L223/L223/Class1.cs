// LeetCode 223. Rectangle Area
// https://leetcode.com/problems/rectangle-area/

#region

using System;
using NUnit.Framework;

#endregion

namespace L223
{
    public class Solution
    {
        public int ComputeArea(int A, int B, int C, int D, int E, int F, int G, int H)
        {
            var x11 = A;
            var y11 = B;
            var x12 = C;
            var y12 = D;

            var x21 = E;
            var y21 = F;
            var x22 = G;
            var y22 = H;

            var area = (x12 - x11) * (y12 - y11) + (x22 - x21) * (y22 - y21);

            var overlapX = x11 < x22 && x21 < x12;
            var overlapY = y11 < y22 && y21 < y12;
            if (overlapX && overlapY)
            {
                var xOverlap = Math.Min(x12, x22) - Math.Max(x11, x21);
                var yOverlap = Math.Min(y12, y22) - Math.Max(y11, y21);
                var overlapArea = xOverlap * yOverlap;
                area -= overlapArea;
            }

            return area;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var rect1 = new[] {-3, 0, 3, 4};
                var rect2 = new[] {0, -1, 9, 2};

                // Act
                var result = new Solution().ComputeArea(rect1[0], rect1[1], rect1[2], rect1[3], rect2[0], rect2[1]
                    , rect2[2], rect2[3]);

                // Assert
                Assert.AreEqual(45, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var rect1 = new[] {0, 0, 0, 0};
                var rect2 = new[] {-1, -1, 1, 1};

                // Act
                var result = new Solution().ComputeArea(rect1[0], rect1[1], rect1[2], rect1[3], rect2[0], rect2[1]
                    , rect2[2], rect2[3]);

                // Assert
                Assert.AreEqual(4, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var rect1 = new[] {-2, -2, 2, 2};
                var rect2 = new[] {-1, -1, 1, 1};

                // Act
                var result = new Solution().ComputeArea(rect1[0], rect1[1], rect1[2], rect1[3], rect2[0], rect2[1]
                    , rect2[2], rect2[3]);

                // Assert
                Assert.AreEqual(16, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var rect1 = new[] {-2, -2, 2, 2};
                var rect2 = new[] {3, 3, 4, 4};

                // Act
                var result = new Solution().ComputeArea(rect1[0], rect1[1], rect1[2], rect1[3], rect2[0], rect2[1]
                    , rect2[2], rect2[3]);

                // Assert
                Assert.AreEqual(17, result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var rect1 = new[] {-2, -2, 2, 2};
                var rect2 = new[] {1, 1, 3, 3};

                // Act
                var result = new Solution().ComputeArea(rect1[0], rect1[1], rect1[2], rect1[3], rect2[0], rect2[1]
                    , rect2[2], rect2[3]);

                // Assert
                Assert.AreEqual(19, result);
            }
        }
    }
}