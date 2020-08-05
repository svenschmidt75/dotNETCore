#region

using NUnit.Framework;

#endregion

// LeetCode 836. Rectangle Overlap
// https://leetcode.com/problems/rectangle-overlap/

namespace L836
{
    public class Solution
    {
        public bool IsRectangleOverlap(int[] rec1, int[] rec2)
        {
            var overlapX = rec1[0] < rec2[2] && rec2[0] < rec1[2];
            var overlapY = rec1[1] < rec2[3] && rec2[1] < rec1[3];
            return overlapX && overlapY;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var rect1 = new[] {0, 0, 2, 2};
                var rect2 = new[] {1, 1, 3, 3};

                // Act
                var result = new Solution().IsRectangleOverlap(rect1, rect2);

                // Assert
                Assert.True(result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var rect1 = new[] {0, 0, 1, 1};
                var rect2 = new[] {1, 0, 2, 1};

                // Act
                var result = new Solution().IsRectangleOverlap(rect1, rect2);

                // Assert
                Assert.False(result);
            }
        }
    }
}