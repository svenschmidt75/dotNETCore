#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 166. Fraction to Recurring Decimal
// URL: https://leetcode.com/problems/fraction-to-recurring-decimal/

namespace LeetCode
{
    public class Solution
    {
        public string FractionToDecimal(int numerator, int denominator)
        {
            // SS: runtime complexity:
            // space complexity:
            
            var result = "";

            // SS: remove sign
            long num = numerator < 0 ? -(long)numerator : numerator;
            long den = denominator < 0 ? -(long)denominator : denominator;

            if ((float)numerator / denominator < 0)
            {
                result += "-";
            }

            if (num == 0)
            {
                return "0";
            }

            if (num / den == 1)
            {
                result += "1";
                return result;
            }

            if ((float)num / den < 1)
            {
                result += "0.";
            }
            else
            {
                // SS: integer part
                var d = num / den;
                var rem = num - d * den;

                var di = new List<char>();
                while (d > 0)
                {
                    var c = (char) ('0' + d % 10);
                    di.Add(c);
                    d /= 10;
                }

                for (var j = di.Count - 1; j >= 0; j--)
                {
                    result += di[j];
                }

                if (rem == 0)
                {
                    return result;
                }

                num = rem;
                result += ".";
            }

            var fac = 1;
            if (num / den < 1)
            {
                fac = 10;
            }

            var i = 0;

            var map = new Dictionary<long, int>();

            var digits = new List<byte>();

            while (num > 0 && i < 10000)
            {
                // SS: shift digits by fac/10 places to left
                long r = num * fac;

                // SS: extract digit
                byte d = (byte) (r / den);

                // SS: remainder
                num = r % den;

                if (map.TryGetValue(r, out var idx))
                {
                    // SS: we found the repeating part

                    // SS: non-repeating fraction
                    for (var j = 0; j < idx; j++)
                    {
                        var d2 = digits[j];
                        var c = (char) ('0' + d2);
                        result += c;
                    }

                    // SS: repeating fraction
                    result += "(";
                    for (var j = idx; j < digits.Count; j++)
                    {
                        var d2 = digits[j];
                        var c = (char) ('0' + d2);
                        result += c;
                    }

                    result += ")";

                    return result;
                }

                // SS: remember this "remainder" for detecting the
                // repeating fractional part
                map[r] = i;

                digits.Add(d);

                if (d > 0)
                {
                    fac = 10;
                }

                i++;
            }

            // SS: deal with no repeating fractions
            for (var j = 0; j < digits.Count; j++)
            {
                var d2 = digits[j];
                var c = (char) ('0' + d2);
                result += c;
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
                var numerator = 15;
                var denominator = 188;

                // Act
                var result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("0.07(9787234042553191489361702127659574468085106382)", result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var numerator = 17;
                var denominator = 19;

                // Act
                var result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("0.(894736842105263157)", result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var numerator = 17;
                var denominator = 19;

                // Act
                var result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("0.(894736842105263157)", result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var numerator = 150;
                var denominator = 7;

                // Act
                var result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("21.(428571)", result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var numerator = int.MinValue;
                var denominator = int.MaxValue;

                // Act
                var result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("-1", result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var numerator = 1;
                var denominator = 5;

                // Act
                var result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("0.2", result);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var numerator = 1;
                var denominator = 2;

                // Act
                var result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("0.5", result);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                var numerator = 2;
                var denominator = 1;

                // Act
                var result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("2", result);
            }

            [Test]
            public void Test9()
            {
                // Arrange
                var numerator = 2;
                var denominator = 3;

                // Act
                var result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("0.(6)", result);
            }

            [Test]
            public void Test10()
            {
                // Arrange
                var numerator = 4;
                var denominator = 333;

                // Act
                var result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("0.(012)", result);
            }
            
            [Test]
            public void Test11()
            {
                // Arrange
                var numerator = -2;
                var denominator = 3;

                // Act
                var result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("-0.(6)", result);
            }

            [Test]
            public void Test12()
            {
                // Arrange
                var numerator = -2;
                var denominator = -3;

                // Act
                var result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("0.(6)", result);
            }
            
        }
    }
}