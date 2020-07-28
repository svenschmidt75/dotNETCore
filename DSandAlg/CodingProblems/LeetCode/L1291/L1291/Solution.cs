#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// 1291. Sequential Digits
// https://leetcode.com/problems/sequential-digits/

namespace L1291
{
    public class Solution
    {
        public IList<int> SequentialDigits(int low, int high)
        {
            var result = new List<int>();

            if (low >= high)
            {
                return result;
            }

            var nDigitsLow = (int) Math.Log10(low);
            var nDigitsHigh = (int) Math.Log10(high);
            var nDigits = nDigitsLow;

            while (nDigits <= nDigitsHigh)
            {
                for (var i = 1; i <= 9; i++)
                {
                    if (i + nDigits > 9)
                    {
                        // SS: invalid digit, skip
                        break;
                    }

                    var currPosition = nDigits;
                    var value = 0;
                    var currDigit = i;
                    while (currDigit <= 9 && currPosition >= 0)
                    {
                        var s = (int) Math.Pow(10, currPosition) * currDigit;
                        value += s;
                        currPosition--;
                        currDigit++;
                    }

                    if (value < low)
                    {
                        continue;
                    }

                    if (value <= high)
                    {
                        result.Add(value);
                    }
                    else
                    {
                        break;
                    }
                }

                nDigits++;
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var low = 100;
                var high = 300;

                // Act
                var result = new Solution().SequentialDigits(low, high);

                // Assert
                CollectionAssert.AreEqual(result, new[] {123, 234});
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var low = 1000;
                var high = 13000;

                // Act
                var result = new Solution().SequentialDigits(low, high);

                // Assert
                CollectionAssert.AreEquivalent(result, new[] {1234, 2345, 3456, 4567, 5678, 6789, 12345});
            }
        }
    }
}