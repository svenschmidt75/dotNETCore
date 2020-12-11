#region

using System;
using NUnit.Framework;

#endregion

// Problem: 72. Edit Distance (Levenshtein Distance I think)
// URL: https://leetcode.com/problems/edit-distance/

namespace LeetCode
{
    public class Solution
    {
        public int MinDistance(string word1, string word2)
        {
            // SS: runtime complexity: O(word1.Length * word2.Length)
            // SS: memory complexity: O(word1.Length * word2.Length)

            var dp = new int[word2.Length + 1][];
            for (var i = 0; i <= word2.Length; i++)
            {
                dp[i] = new int[word1.Length + 1];
            }

            // SS: set boundary conditions

            // SS: set to remove, based on how many characters are left to remove
            for (var i = 0; i < word2.Length; i++)
            {
                dp[i][word1.Length] = word2.Length - i;
            }

            // SS: set to insert, based on how many characters are left to insert
            for (var i = 0; i < word1.Length; i++)
            {
                dp[word2.Length][i] = word1.Length - i;
            }

            // SS: fill-in grid
            for (var idx2 = word2.Length - 1; idx2 >= 0; idx2--)
            {
                for (var idx1 = word1.Length - 1; idx1 >= 0; idx1--)
                {
                    if (word1[idx1] == word2[idx2])
                    {
                        dp[idx2][idx1] = dp[idx2 + 1][idx1 + 1];
                    }
                    else
                    {
                        // SS: insert word[idx2] into position idx1
                        var c1 = 1 + dp[idx2 + 1][idx1];

                        // SS: remove word1[idx1]
                        var c2 = 1 + dp[idx2][idx1 + 1];

                        // SS: replace word1[idx1] with word2[idx2]
                        var c3 = 1 + dp[idx2 + 1][idx1 + 1];

                        var c = Math.Min(c1, Math.Min(c2, c3));
                        dp[idx2][idx1] = c;
                    }
                }
            }

            return dp[0][0];
        }

        public int MinDistance2(string word1, string word2)
        {
            // SS: Divide & Conquer, runtime complexity: O(3^N)

            int Solve(int idx1, int idx2)
            {
                if (idx1 == word1.Length && idx2 == word2.Length)
                {
                    return 0;
                }

                if (idx1 < word1.Length && idx2 < word2.Length && word1[idx1] == word2[idx2])
                {
                    // SS: nothing to do
                    return Solve(idx1 + 1, idx2 + 1);
                }

                // SS: replace character word1[idx1] with word2[idx2]
                var c1 = int.MaxValue;
                if (idx1 < word1.Length && idx2 < word2.Length)
                {
                    c1 = 1 + Solve(idx1 + 1, idx2 + 1);
                }

                // SS: remove character work1[idx1]
                var c2 = int.MaxValue;
                if (idx1 < word1.Length)
                {
                    c2 = 1 + Solve(idx1 + 1, idx2);
                }

                // SS: insert character word2[idx2]
                var c3 = int.MaxValue;
                if (idx2 < word2.Length)
                {
                    c3 = 1 + Solve(idx1, idx2 + 1);
                }

                var c = Math.Min(c1, Math.Min(c2, c3));
                return c;
            }

            var n = Solve(0, 0);
            return n;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var word1 = "horse";
                var word2 = "ros";

                // Act
                var n = new Solution().MinDistance(word1, word2);

                // Assert
                Assert.AreEqual(3, n);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var word1 = "ros";
                var word2 = "horse";

                // Act
                var n = new Solution().MinDistance(word1, word2);

                // Assert
                Assert.AreEqual(3, n);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var word1 = "intention";
                var word2 = "execution";

                // Act
                var n = new Solution().MinDistance(word1, word2);

                // Assert
                Assert.AreEqual(5, n);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var word1 = "ros";
                var word2 = "ros";

                // Act
                var n = new Solution().MinDistance(word1, word2);

                // Assert
                Assert.AreEqual(0, n);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var word1 = "";
                var word2 = "";

                // Act
                var n = new Solution().MinDistance(word1, word2);

                // Assert
                Assert.AreEqual(0, n);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var word1 = "e";
                var word2 = "";

                // Act
                var n = new Solution().MinDistance(word1, word2);

                // Assert
                Assert.AreEqual(1, n);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var word1 = "e";
                var word2 = "ee";

                // Act
                var n = new Solution().MinDistance(word1, word2);

                // Assert
                Assert.AreEqual(1, n);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                var word1 = "ef";
                var word2 = "eb";

                // Act
                var n = new Solution().MinDistance(word1, word2);

                // Assert
                Assert.AreEqual(1, n);
            }

            [Test]
            public void Test9()
            {
                // Arrange
                var word1 = "";
                var word2 = "e";

                // Act
                var n = new Solution().MinDistance(word1, word2);

                // Assert
                Assert.AreEqual(1, n);
            }

            [Test]
            public void Test10()
            {
                // Arrange
                var word1 = "gd";
                var word2 = "hjgsdf";

                // Act
                var n = new Solution().MinDistance(word1, word2);

                // Assert
                Assert.AreEqual(4, n);
            }
        }
    }
}