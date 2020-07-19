#region

using System;
using NUnit.Framework;

#endregion

namespace GoogleProblem24
{
    public class Solution
    {
        /// <summary>
        ///  Find the longest subarray of houses within our budget such that
        /// the length of plots of land is maximized.
        /// </summary>
        /// <param name="price"></param>
        /// <param name="plotLength"></param>
        /// <param name="budget"></param>
        /// <returns></returns>
        public int Solve(int[] price, int[] plotLength, int budget)
        {
            // SS: runtime complexity: O(N), using sliding window technique
            if (budget == 0)
            {
                return 0;
            }

            var remainingBudget = budget;
            var i = 0;
            var j = 0;
            var segmentLength = 0;
            var longestSegmentLength = 0;

            while (j < price.Length)
            {
                var p = price[j];
                if (remainingBudget >= p)
                {
                    // SS: grow sliding window, i.e. we can afford to buy this
                    remainingBudget -= p;
                    segmentLength += plotLength[j];
                    longestSegmentLength = Math.Max(longestSegmentLength, segmentLength);
                    j++;
                }
                else
                {
                    // SS: shrink window, i.e. we cannot afford to buy this
                    if (i == j)
                    {
                        // SS: skip this house as we cannot afford it
                        i++;
                        j++;
                    }
                    else
                    {
                        remainingBudget += price[i];
                        segmentLength -= plotLength[i];
                        i++;
                    }
                }
            }

            return longestSegmentLength;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var price = new[] {10, 20, 15, 35, 70};
                var plotLength = new[] {3, 7, 5, 12, 1};
                var budget = 35;

                // Act
                var longestPlotLength = new Solution().Solve(price, plotLength, budget);

                // Assert
                Assert.AreEqual(12, longestPlotLength);
            }
        }
    }
}