#region

using System;
using NUnit.Framework;

#endregion

// Problem: 43. Multiply Strings
// URL: https://leetcode.com/problems/multiply-strings/

namespace LeetCode43
{
    public class Solution
    {
        public string Multiply(string num1, string num2)
        {
            var resultArray = new char[num1.Length + num2.Length];
            var resultIdx = 0;

            var maxIdx = num1.Length + num2.Length;
            var totalIdx = 0;

            var carry = 0;

            while (totalIdx <= maxIdx)
            {
                var idx1 = Math.Min(num1.Length - 1, totalIdx);
                var idx2 = totalIdx - idx1;
                if (idx2 >= num2.Length)
                {
                    if (carry > 0)
                    {
                        resultArray[resultIdx++] = (char) (carry + '0');
                    }

                    break;
                }

                var tempResult = carry;
                carry = 0;

                while (idx2 <= totalIdx && idx2 < num2.Length)
                {
                    var d1 = num1[num1.Length - 1 - idx1] - '0';
                    var d2 = num2[num2.Length - 1 - idx2] - '0';
                    tempResult += d1 * d2;
                    idx1--;
                    idx2++;
                }

                var r = tempResult;
                if (tempResult > 9)
                {
                    var d = tempResult / 10;
                    r -= d * 10;
                    carry = d;
                }

                resultArray[resultIdx++] = (char) (r + '0');
                totalIdx++;
            }

            Array.Reverse(resultArray, 0, resultIdx);

            // SS: skip leading 0
            var startIdx = 0;
            while (startIdx < resultIdx - 1 && resultArray[startIdx] == '0')
            {
                startIdx++;
            }

            var result = new string(resultArray, startIdx, resultIdx - startIdx);
            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var num1 = "2";
                var num2 = "3";

                // Act
                var result = new Solution().Multiply(num1, num2);

                // Assert
                Assert.AreEqual("6", result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var num1 = "123";
                var num2 = "456";

                // Act
                var result = new Solution().Multiply(num1, num2);

                // Assert
                Assert.AreEqual("56088", result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var num1 = "99";
                var num2 = "99";

                // Act
                var result = new Solution().Multiply(num1, num2);

                // Assert
                Assert.AreEqual("9801", result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var num1 = "999";
                var num2 = "0";

                // Act
                var result = new Solution().Multiply(num1, num2);

                // Assert
                Assert.AreEqual("0", result);
            }
        }
    }
}