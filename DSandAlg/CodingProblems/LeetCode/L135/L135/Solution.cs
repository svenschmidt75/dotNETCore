#region

using System;
using NUnit.Framework;

#endregion

// Problem: 135. Candy
// URL: https://leetcode.com/problems/candy/

namespace LeetCode
{
    public class Solution
    {
        public int Candy(int[] ratings)
        {
            // SS: 2-pass algorithm, i.e. O(N) runtime complexity
            // O(N) space complexity

            if (ratings.Length == 0)
            {
                return 0;
            }

            var candy = new int[ratings.Length];

            candy[^1] = 1;

            // SS: 1st pass to correct from right to left
            for (var i = ratings.Length - 2; i >= 0; i--)
            {
                if (ratings[i] <= ratings[i + 1])
                {
                    candy[i] = Math.Min(1, candy[i + 1]);
                }
                else
                {
                    candy[i] = 1 + candy[i + 1];
                }
            }

            // SS: 2nd pass to correct from left to right
            for (var i = 1; i < candy.Length; i++)
            {
                if (ratings[i - 1] < ratings[i] && candy[i - 1] >= candy[i])
                {
                    candy[i] = candy[i - 1] + 1;
                }
            }

            // SS: sum candies
            var nCandy = 0;
            for (var i = 0; i < candy.Length; i++)
            {
                nCandy += candy[i];
            }

            return nCandy;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int[] ratings = {1, 0, 2};

                // Act
                var nCandy = new Solution().Candy(ratings);

                // Assert
                Assert.AreEqual(5, nCandy);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                int[] ratings = {1, 2, 2};

                // Act
                var nCandy = new Solution().Candy(ratings);

                // Assert
                Assert.AreEqual(4, nCandy);
            }

            [TestCase(new[] {5}, 1)]
            [TestCase(new[] {3, 5}, 3)]
            [TestCase(new[] {9, 3, 5}, 5)]
            [TestCase(new[] {2, 9, 3, 5}, 6)]
            [TestCase(new[] {1, 2, 9, 3, 5}, 9)]
            [TestCase(new[] {6, 1, 2, 9, 3, 5}, 11)]
            [TestCase(new[] {4, 6, 1, 2, 9, 3, 5}, 12)]
            [TestCase(new[] {0, 4, 6, 1, 2, 9, 3, 5}, 15)]
            [TestCase(new[] {1, 0, 4, 6, 1, 2, 9, 3, 5}, 17)]
            public void Test3(int[] ratings, int expectedCandy)
            {
                // Arrange

                // Act
                var nCandy = new Solution().Candy(ratings);

                // Assert
                Assert.AreEqual(expectedCandy, nCandy);
            }

            [TestCase(new[] {5}, 1)]
            [TestCase(new[] {6, 5}, 3)]
            [TestCase(new[] {7, 6, 5}, 6)]
            [TestCase(new[] {8, 7, 6, 5}, 10)]
            public void Test4(int[] ratings, int expectedCandy)
            {
                // Arrange

                // Act
                var nCandy = new Solution().Candy(ratings);

                // Assert
                Assert.AreEqual(expectedCandy, nCandy);
            }

            [TestCase(new[] {1, 2, 3}, 6)]
            public void Test5(int[] ratings, int expectedCandy)
            {
                // Arrange

                // Act
                var nCandy = new Solution().Candy(ratings);

                // Assert
                Assert.AreEqual(expectedCandy, nCandy);
            }

            [TestCase(new[] {2, 2, 1}, 4)]
            [TestCase(new[] {1, 2, 2, 1}, 6)]
            [TestCase(new[] {1, 3, 2, 2, 1}, 7)]
            [TestCase(new[] {1, 2, 8, 9, 3, 5}, 13)]
            [TestCase(new[] {5, 3, 9, 8, 2, 1}, 13)]
            public void Test6(int[] ratings, int expectedCandy)
            {
                // Arrange

                // Act
                var nCandy = new Solution().Candy(ratings);

                // Assert
                Assert.AreEqual(expectedCandy, nCandy);
            }
        }
    }
}