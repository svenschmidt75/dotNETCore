#region

using System;
using NUnit.Framework;

#endregion

// https://leetcode.com/discuss/interview-question/398023/Microsoft-Online-Assessment-Questions

namespace Insert5LargestPossibleNumber
{
    public class Solution
    {
        public int GetLargest(int input)
        {
            if (input == 0)
            {
                return 5;
            }

            var nDigits = (int) Math.Log10(Math.Abs(input));
            var currentExp = nDigits + 1;

            var haveInserted = false;

            var result = 0;

            var num = Math.Abs(input);
            var i = nDigits;
            while (i >= 0)
            {
                var exp = (int) Math.Pow(10, i);
                var digit = num / exp;

                if (input > 0 && (digit > 5 || haveInserted) || input < 0 && (digit < 5 || haveInserted))
                {
                    num -= digit * exp;
                    exp = (int) Math.Pow(10, currentExp);
                    result += digit * exp;
                    currentExp--;
                    i--;
                }
                else
                {
                    // SS: insert 5 at this position
                    haveInserted = true;
                    exp = (int) Math.Pow(10, currentExp);
                    result += 5 * exp;
                    currentExp--;
                }
            }

            if (input < 0)
            {
                result *= -1;
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

                // Act
                var result = new Solution().GetLargest(93);

                // Assert
                Assert.AreEqual(953, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange

                // Act
                var result = new Solution().GetLargest(-76);

                // Assert
                Assert.AreEqual(-576, result);
            }
        }
    }
}