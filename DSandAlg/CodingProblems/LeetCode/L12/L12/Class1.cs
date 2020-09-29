#region

using System;
using NUnit.Framework;

#endregion

namespace L12
{
    public class Solution
    {
        public string IntToRoman(int num)
        {
            var nDigits = (int) Math.Log10(num) + 1;
            var divider = (int) Math.Pow(10, nDigits - 1);

            var roman = "";

            var number = num;
            var n = 0;
            while (n < nDigits)
            {
                var remainder = number / divider;
                if (remainder > 0)
                {
                    var pos = nDigits - 1 - n;
                    if (pos == 3)
                    {
                        // M
                        roman += new string('M', remainder);
                    }
                    else if (pos == 2)
                    {
                        if (remainder < 4)
                        {
                            roman += new string('C', remainder);
                        }
                        else if (remainder == 4)
                        {
                            roman += "CD";
                        }
                        else if (remainder < 9)
                        {
                            roman += "D";
                            roman += new string('C', remainder - 5);
                        }
                        else
                        {
                            // remainder == 9
                            roman += "CM";
                        }
                    }
                    else if (pos == 1)
                    {
                        if (remainder < 4)
                        {
                            roman += new string('X', remainder);
                        }
                        else if (remainder == 4)
                        {
                            roman += "XL";
                        }
                        else if (remainder < 9)
                        {
                            roman += "L";
                            roman += new string('X', remainder - 5);
                        }
                        else
                        {
                            // remainder == 9
                            roman += "XC";
                        }
                    }
                    else
                    {
                        if (remainder < 4)
                        {
                            roman += new string('I', remainder);
                        }
                        else if (remainder == 4)
                        {
                            roman += "IV";
                        }
                        else if (remainder < 9)
                        {
                            roman += "V";
                            roman += new string('I', remainder - 5);
                        }
                        else
                        {
                            // remainder == 9
                            roman += "IX";
                        }
                    }
                }

                number -= remainder * divider;
                divider /= 10;
                n++;
            }

            return roman;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var input = 3;

                // Act
                var result = new Solution().IntToRoman(input);

                // Assert
                Assert.AreEqual("III", result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var input = 4;

                // Act
                var result = new Solution().IntToRoman(input);

                // Assert
                Assert.AreEqual("IV", result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var input = 9;

                // Act
                var result = new Solution().IntToRoman(input);

                // Assert
                Assert.AreEqual("IX", result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var input = 58;

                // Act
                var result = new Solution().IntToRoman(input);

                // Assert
                Assert.AreEqual("LVIII", result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var input = 1994;

                // Act
                var result = new Solution().IntToRoman(input);

                // Assert
                Assert.AreEqual("MCMXCIV", result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var input = 309;

                // Act
                var result = new Solution().IntToRoman(input);

                // Assert
                Assert.AreEqual("CCCIX", result);
            }
        }
    }
}