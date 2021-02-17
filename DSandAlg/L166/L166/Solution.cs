using System;
using System.Collections.Generic;
using NUnit.Framework;

// Problem: 166. Fraction to Recurring Decimal
// URL: https://leetcode.com/problems/fraction-to-recurring-decimal/

namespace LeetCode
{
    public class Solution
    {
        public string FractionToDecimal(int numerator, int denominator)
        {
            string result = "";

            int num = numerator;
            int den = denominator;
            if (numerator / denominator < 0)
            {
                num = num < 0 ? -num : num;
                den = den < 0 ? -den : den;
            }

            var digits = new List<byte>();

            // SS: deal with the integer part here
            if (numerator / denominator < 0)
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
                        
            if (num / den < 1)
            {
                result += "0.";
            }
            else
            {
                int d = num / den;
                int rem = num - d * den;

                var di = new List<char>();
                while (d > 0)
                {
                    char c = (char) ('0' + (d % 10));
                    di.Add(c);
                    d /= 10;
                }

                for (int j = di.Count - 1; j >= 0; j--)
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
            
            int fac = 1;
            if (num / den < 1)
            {
                fac = 10;
            }
            
            int i = 0;

            var map = new Dictionary<int, int>();
            
            while (num > 0 && i < 10000)
            {
                byte d = (byte)((num * fac) / den);

//                Console.Write(d);

                if (d > 0)
                {
                    var r = num * fac;
                    num = num * fac - d * den;
                    
//                    Console.WriteLine($"idx: {i}: {num}");

                    if (map.TryGetValue(r, out int idx))
                    {
                        // SS: we found the repeating part

                        // SS: non-repeating fraction
                        for (int j = 0; j < idx; j++)
                        {
                            byte d2 = digits[j];
                            char c = (char)('0' + d2);
                            result += c;
                        }

                        // SS: repeating fraction
                        result += "(";
                        for (int j = idx; j < digits.Count; j++)
                        {
                            byte d2 = digits[j];
                            char c = (char)('0' + d2);
                            result += c;
                        }
                        result += ")";

                        break;
                    }

                    // SS: remember this "remainder" for detecting the
                    // repeating fractional part
                    map[r] = i;
                }

                digits.Add(d);

                if (d == 0)
                {
                    fac *= 10;
                }
                else
                {
                    fac = 10;
                }

                i++;
            }

            // SS: deal with no repeating fractions
            
            return result;
        }
    
        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                int numerator = 15;
                int denominator = 188;

                // Act
                string result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("0.07(9787234042553191489361702127659574468085106382)", result);
            }
            
            [Test]
            public void Test2()
            {
                // Arrange
                int numerator = 17;
                int denominator = 19;

                // Act
                string result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("0.(894736842105263157)", result);
            }
            
            [Test]
            public void Test3()
            {
                // Arrange
                int numerator = 17;
                int denominator = 19;

                // Act
                string result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("0.(894736842105263157)", result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                int numerator = 150;
                int denominator = 7;

                // Act
                string result = new Solution().FractionToDecimal(numerator, denominator);

                // Assert
                Assert.AreEqual("21.(428571)", result);
            }

        }
    }
}