#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 120. Triangle
// URL: https://leetcode.com/problems/triangle/

namespace LeetCode
{
    public class Solution
    {
        public int MinimumTotal(IList<IList<int>> triangle)
        {
            // return MinimumTotalBottomUpRightLeft(triangle);
            // return MinimumTotalDivideConquer(triangle);
            return MinimumTotalFast(triangle);
        }

        private int MinimumTotalDivideConquer(IList<IList<int>> triangle)
        {
            int Solve(int idx, int pos)
            {
                if (idx == triangle.Count)
                {
                    return 0;
                }

                // SS: at each position, there are two ways to consider, (pos, pos + 1)
                // hence O(2^N)

                var v1 = triangle[idx][pos];

                if (pos < triangle[idx].Count - 1)
                {
                    var v2 = triangle[idx][pos + 1];
                    return Math.Min(v1 + Solve(idx + 1, pos), v2 + Solve(idx + 1, pos + 1));
                }

                return v1 + Solve(idx + 1, pos);
            }

            return Solve(0, 0);
        }

        private int MinimumTotalBottomUpRightLeft(IList<IList<int>> triangle)
        {
            // SS: runtime performance: O(N^2), N = #triangle levels
            // space complexity: O(N)

            var dp1 = new int[triangle[^1].Count];
            var dp2 = new int[triangle[^1].Count];

            // SS: set boundary conditions
            for (var i = 0; i < triangle[^1].Count; i++)
            {
                dp2[i] = triangle[^1][i];
            }

            // SS: start at 2nd to last triangle
            for (var i = triangle.Count - 2; i >= 0; i--)
            {
                // SS: check every position
                for (var pos = 0; pos < triangle[i].Count; pos++)
                {
                    var v1 = dp2[pos] + triangle[i][pos];

                    if (pos < triangle[i + 1].Count - 1)
                    {
                        var v2 = dp2[pos + 1] + triangle[i][pos];
                        dp1[pos] = Math.Min(v1, v2);
                    }
                    else
                    {
                        dp1[pos] = v1;
                    }
                }

                var tmp = dp1;
                dp1 = dp2;
                dp2 = tmp;
            }

            return dp2[0];
        }

        private int MinimumTotalFast(IList<IList<int>> triangle)
        {
            // SS: runtime complexity: O(#triangle^2)
            // space complexity: O(1)

            for (var i = triangle.Count - 2; i >= 0; i--)
            {
                for (var pos = 0; pos < triangle[i].Count; pos++)
                {
                    triangle[i][pos] = triangle[i][pos] + Math.Min(triangle[i + 1][pos], triangle[i + 1][pos + 1]);
                }
            }

            return triangle[0][0];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                IList<IList<int>> triangle = new List<IList<int>>
                {
                    new List<int> {2}
                    , new List<int> {3, 4}
                    , new List<int> {6, 5, 7}
                    , new List<int> {4, 1, 8, 3}
                };

                // Act
                var minPathSum = new Solution().MinimumTotal(triangle);

                // Assert
                Assert.AreEqual(11, minPathSum);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                IList<IList<int>> triangle = new List<IList<int>>
                {
                    new List<int> {-10}
                };

                // Act
                var minPathSum = new Solution().MinimumTotal(triangle);

                // Assert
                Assert.AreEqual(-10, minPathSum);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                IList<IList<int>> triangle = new List<IList<int>>
                {
                    new List<int> {2}
                    , new List<int> {3, 4}
                    , new List<int> {6, 5, 1}
                    , new List<int> {2, 1, 8, 3}
                    , new List<int> {2, 1, 3, 3, 2}
                    , new List<int> {2, 1, 1, 1, 8, 3}
                    , new List<int> {2, 1, 8, 3, 2, 3, 4}
                };

                // Act
                var minPathSum = new Solution().MinimumTotal(triangle);

                // Assert
                Assert.AreEqual(14, minPathSum);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                IList<IList<int>> triangle = new List<IList<int>>
                {
                    new List<int> {-1}
                    , new List<int> {2, 3}
                    , new List<int> {1, -1, -3}
                };

                // Act
                var minPathSum = new Solution().MinimumTotal(triangle);

                // Assert
                Assert.AreEqual(-1, minPathSum);
            }
        }
    }
}