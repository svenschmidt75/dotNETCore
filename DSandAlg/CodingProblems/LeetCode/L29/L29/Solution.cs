#region

using System;
using NUnit.Framework;

#endregion

// Problem: 29. Divide Two Integers, worst problem ever...
// URL: https://leetcode.com/problems/divide-two-integers/

namespace LeetCode29
{
    public class Solution
    {
        public int Divide(int dividend, int divisor)
        {
            if (dividend == int.MinValue && divisor == -1)
            {
                return int.MaxValue;
            }

            if (dividend == int.MinValue && divisor == 1)
            {
                return int.MinValue;
            }

            if (dividend == int.MaxValue && divisor == -1)
            {
                return int.MinValue;
            }

            if (dividend == int.MaxValue && divisor == 1)
            {
                return int.MaxValue;
            }

            if (divisor == int.MinValue)
            {
                if (dividend == int.MinValue)
                {
                    return 1;
                }

                return 0;
            }

            if (divisor == int.MaxValue)
            {
                if (dividend == int.MinValue)
                {
                    return -1;
                }

                if (dividend == int.MaxValue)
                {
                    return 1;
                }

                return 0;
            }

            var dividend2 = dividend;
            var divisor2 = divisor;

            var sign = 1;

            if (dividend2 < 0)
            {
                sign = -1;
            }

            if (divisor < 0)
            {
                divisor2 = -divisor2;
                sign *= -1;
            }

            var quotient = Subtract(dividend2, divisor2);
            return sign * quotient;
        }

        private int Subtract(int dividend, int divisor)
        {
            var quotient = 0;

            if (dividend == int.MinValue)
            {
                var e = 9;
                var exp = (int) Math.Pow(10, e);

                while (exp > 0 && - divisor < int.MinValue / exp)
                {
                    exp /= 10;
                }
                
                var div = -divisor * exp;
                var divid = dividend;

                while (e >= 0 &&  div < 0)
                {
                    while (div < divid)
                    {
                        e--;
                        div /= 10;
                        exp /= 10;
                    }

                    while (divid <= div && e >= 0 && div < 0)
                    {
                        while (divid <= div)
                        {
                            divid -= div;
                            quotient += exp;
                        }

                        div /= 10;
                        exp /= 10;
                        e--;
                    }
                }
            }
            else
            {
                if (dividend < 0)
                {
                    dividend = -dividend;
                }

                var e = (int) Math.Log10(dividend);
                var exp = (int) Math.Pow(10, e);

                while (exp > 0 && divisor > int.MaxValue / exp)
                {
                    exp /= 10;
                }
                
                var div = divisor * exp;
                var divid = dividend;

                while (e >= 0 && div > 0)
                {
                    while (div > divid)
                    {
                        e--;
                        div /= 10;
                        exp /= 10;
                    }

                    while (divid >= div && e >= 0 && div > 0)
                    {
                        while (divid >= div)
                        {
                            divid -= div;
                            quotient += exp;
                        }

                        div /= 10;
                        exp /= 10;
                        e--;
                    }
                }
            }

            return quotient;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var dividend = 10;
                var divisor = 3;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(3, quotient);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var dividend = 7;
                var divisor = -3;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(-2, quotient);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var dividend = 0;
                var divisor = 1;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(0, quotient);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var dividend = 1;
                var divisor = 1;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(1, quotient);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var dividend = -1;
                var divisor = 1;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(-1, quotient);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var dividend = -1;
                var divisor = -1;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(1, quotient);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var dividend = int.MaxValue;
                var divisor = -1;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(int.MinValue, quotient);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                var dividend = int.MinValue;
                var divisor = -1;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(int.MaxValue, quotient);
            }

            [Test]
            public void Test9()
            {
                // Arrange
                var dividend = int.MaxValue;
                var divisor = 2;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(1073741823, quotient);
            }

            [Test]
            public void Test10()
            {
                // Arrange
                var dividend = int.MaxValue;
                var divisor = 2;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(1073741823, quotient);
            }

            [Test]
            public void Test11()
            {
                // Arrange
                var dividend = int.MaxValue;
                var divisor = -2;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(-1073741823, quotient);
            }

            [Test]
            public void Test12()
            {
                // Arrange
                var dividend = int.MinValue;
                var divisor = 2;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(-1073741824, quotient);
            }

            [Test]
            public void Test13()
            {
                // Arrange
                var dividend = int.MinValue;
                var divisor = -2;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(1073741824, quotient);
            }

            [Test]
            public void Test14()
            {
                // Arrange
                var dividend = 1000;
                var divisor = 15;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(66, quotient);
            }

            [Test]
            public void Test15()
            {
                // Arrange
                var dividend = 1000;
                var divisor = int.MinValue;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(0, quotient);
            }

            [Test]
            public void Test16()
            {
                // Arrange
                var dividend = 1000;
                var divisor = int.MaxValue;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(0, quotient);
            }

            [Test]
            public void Test17()
            {
                // Arrange
                var dividend = int.MinValue;
                var divisor = int.MaxValue;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(-1, quotient);
            }

            [Test]
            public void Test18()
            {
                // Arrange
                var dividend = int.MaxValue;
                var divisor = int.MaxValue;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(1, quotient);
            }

            [Test]
            public void Test19()
            {
                // Arrange
                var dividend = int.MaxValue;
                var divisor = int.MaxValue;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(1, quotient);
            }

            [Test]
            public void Test20()
            {
                // Arrange
                var dividend = int.MaxValue;
                var divisor = int.MinValue;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(0, quotient);
            }

            [Test]
            public void Test21()
            {
                // Arrange
                var dividend = int.MaxValue;
                var divisor = 3;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(715827882, quotient);
            }
            
            [Test]
            public void Test22()
            {
                // Arrange
                var dividend = int.MinValue;
                var divisor = -3;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(715827882, quotient);
            }

            [Test]
            public void Test23()
            {
                // Arrange
                var dividend = int.MinValue;
                var divisor = 2;

                // Act
                var quotient = new Solution().Divide(dividend, divisor);

                // Assert
                Assert.AreEqual(-1073741824, quotient);
            }

        }
    }
}