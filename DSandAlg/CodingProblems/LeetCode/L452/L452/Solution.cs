#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 452. Minimum Number of Arrows to Burst Balloons
// URL: https://leetcode.com/problems/minimum-number-of-arrows-to-burst-balloons/

namespace LeetCode
{
    public class Solution
    {
        #region Public Methods

        public int FindMinArrowShots(int[][] points)
        {
            return FindMinArrowShots1(points);
        }

        #endregion

        #region Private Methods

        #region Event Handlers

        private int FindMinArrowShots1(int[][] points)
        {
            // SS: whenever we have an intersection, shrink the interval
            // runtime complexity: O(n log n) due to sorting
            // space complexity: O(1)

            // SS: sort intervals by start value
            Array.Sort(points, Comparer<int[]>.Create((p1, p2) => p1[0].CompareTo(p2[0])));

            int i11 = points[0][0];
            int i12 = points[0][1];

            int nArrows = 1;
            int j = 1;
            while (j < points.Length)
            {
                // SS: check if intervals intersect
                int i21 = points[j][0];
                int i22 = points[j][1];
                if (Intersects(i11, i12, i21, i22))
                {
                    // SS: shrink interval
                    i11 = Math.Max(i11, i21);
                    i12 = Math.Min(i12, i22);
                }
                else
                {
                    // SS: no overlap, so add an arrow
                    i11 = points[j][0];
                    i12 = points[j][1];
                    nArrows++;
                }

                j++;
            }

            return nArrows;
        }

        private static bool Intersects(int i11, int i12, int i21, int i22)
        {
            return i11 <= i22 && i21 <= i12;
        }

        #endregion

        #endregion

        [TestFixture]
        public class Tests
        {
            #region Public Methods

            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                int nArrows = new Solution().FindMinArrowShots(new[] {new[] {10, 16}, new[] {2, 8}, new[] {1, 6}, new[] {7, 12}});

                // Assert
                Assert.AreEqual(2, nArrows);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                int nArrows = new Solution().FindMinArrowShots(new[] {new[] {1, 2}, new[] {3, 4}, new[] {5, 6}, new[] {7, 8}});

                // Assert
                Assert.AreEqual(4, nArrows);
            }

            [Test]
            public void Test3()
            {
                // Arrange

                // Act
                int nArrows = new Solution().FindMinArrowShots(new[] {new[] {1, 2}, new[] {2, 3}, new[] {3, 4}, new[] {4, 5}});

                // Assert
                Assert.AreEqual(2, nArrows);
            }

            [Test]
            public void Test4()
            {
                // Arrange

                // Act
                int nArrows = new Solution().FindMinArrowShots(new[] { new[] { -1, 1 }, new[] { 0, 1 }, new[] { 2, 3 }, new[] { 1, 2 } });

                // Assert
                Assert.AreEqual(2, nArrows);
            }

            [Test]
            public void Test5()
            {
                // Arrange

                // Act
                int nArrows = new Solution().FindMinArrowShots(new[] { new[] { 3, 9 }, new[] { 7, 12 }, new[] { 3, 8 }, new[] { 6, 8 }, new[] { 9, 10 }, new[] { 2, 9 }, new[] { 0, 9 }, new[] { 3, 9 }, new[] { 0, 6 }, new[] { 2, 8 } });

                // Assert
                Assert.AreEqual(2, nArrows);
            }

            [Test]
            public void Test6()
            {
                // Arrange

                // Act
                int nArrows = new Solution().FindMinArrowShots(new[] { new[] { 9, 12 }, new[] { 1, 10 }, new[] { 4, 11 }, new[] { 8, 12 }, new[] { 3, 9 }, new[] { 6, 9 }, new[] { 6, 7 } });

                // Assert
                Assert.AreEqual(2, nArrows);
            }

            #endregion
        }
    }
}
