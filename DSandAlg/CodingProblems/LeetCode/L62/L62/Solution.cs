#region

using NUnit.Framework;

#endregion

// Problem: 62. Unique Paths
// URL: https://leetcode.com/problems/unique-paths/

namespace LeetCode62
{
    public class Solution
    {
        public int UniquePaths(int m, int n)
        {
            var dp = new int[m][];
            for (var i = 0; i < m; i++)
            {
                dp[i] = new int[n];
            }

            // SS: dp[m - 1][n - 1] = 1 is boundary condition
            dp[m - 1][n - 1] = 1;

            for (var i = m - 1; i >= 0; i--)
            {
                for (var j = n - 1; j >= 0; j--)
                {
                    // SS: end point?
                    if (i == m - 1 && j == n - 1)
                    {
                        continue;
                    }

                    var nPaths = 0;

                    if (i <= m - 2)
                    {
                        nPaths += dp[i + 1][j];
                    }

                    if (j <= n - 2)
                    {
                        nPaths += dp[i][j + 1];
                    }

                    dp[i][j] = nPaths;
                }
            }

            return dp[0][0];
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var n = new Solution().UniquePaths(3, 7);

                // Assert
                Assert.AreEqual(28, n);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var n = new Solution().UniquePaths(3, 2);

                // Assert
                Assert.AreEqual(3, n);
            }

            [Test]
            public void Test3()
            {
                // Arrange

                // Act
                var n = new Solution().UniquePaths(7, 3);

                // Assert
                Assert.AreEqual(28, n);
            }

            [Test]
            public void Test4()
            {
                // Arrange

                // Act
                var n = new Solution().UniquePaths(3, 3);

                // Assert
                Assert.AreEqual(6, n);
            }

            [Test]
            public void Test5()
            {
                // Arrange

                // Act
                var n = new Solution().UniquePaths(1, 1);

                // Assert
                Assert.AreEqual(1, n);
            }
        }
    }
}