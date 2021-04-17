#region

using NUnit.Framework;

#endregion

// Problem: 273. Integer to English Words
// URL: https://leetcode.com/problems/integer-to-english-words/

namespace LeetCode
{
    public class Solution
    {
        private static readonly string[] SingleDigitEncoded =
            {"", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thir", "Four", "Fif", "Six", "Seven", "Eigh", "Nine"};

        private static readonly string[] DoubleDigitEncoded = {"", "", "Twen", "Thir", "For", "Fif", "Six", "Seven", "Eigh", "Nine"};
        private static readonly string[] MagnitudeEncoded = {"", "Thousand", "Million", "Billion"};

        public string NumberToWords(int num)
        {
            // SS: special case
            if (num == 0)
            {
                return "Zero";
            }

            var result = "";
            var magnitude = 0;

            while (num > 0)
            {
                // SS: get the lowest-significant digit
                var d = num % 10;
                var number = d;
                num /= 10;

                d = num % 10;
                num /= 10;
                if (d > 0)
                {
                    number += d * 10;
                }

                // SS: encode 2 digits
                var d2 = Encode2Digits(number);
                var tmpStr = d2;

                // SS: encode Hundred
                d = num % 10;
                num /= 10;
                if (d > 0)
                {
                    var h = SingleDigitEncoded[d];
                    tmpStr = $"{h} Hundred";
                    if (string.IsNullOrWhiteSpace(d2) == false)
                    {
                        tmpStr += $" {d2}";
                    }
                }

                // SS: encode magnitude
                if (magnitude > 0 && string.IsNullOrWhiteSpace(tmpStr) == false)
                {
                    tmpStr += $" {MagnitudeEncoded[magnitude]}";
                }

                if (string.IsNullOrWhiteSpace(result))
                {
                    result = tmpStr;
                }
                else if (string.IsNullOrWhiteSpace(tmpStr) == false)
                {
                    result = tmpStr + " " + result;
                }

                magnitude++;
            }

            return result;
        }

        private static string Encode2Digits(int d)
        {
            if (d == 0)
            {
                return "";
            }

            var result = "";
            if (d < 20)
            {
                result = SingleDigitEncoded[d];
                if (d > 12)
                {
                    result += "teen";
                }
            }
            else
            {
                var d1 = d % 10;
                var d2 = d / 10 % 10;

                result = $"{DoubleDigitEncoded[d2]}ty";

                if (d1 > 0)
                {
                    result += $" {SingleDigitEncoded[d1]}";
                }
            }

            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase(123, "One Hundred Twenty Three")]
            [TestCase(12345, "Twelve Thousand Three Hundred Forty Five")]
            [TestCase(1234567, "One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven")]
            [TestCase(1234567891, "One Billion Two Hundred Thirty Four Million Five Hundred Sixty Seven Thousand Eight Hundred Ninety One")]
            [TestCase(0, "Zero")]
            [TestCase(7, "Seven")]
            [TestCase(10, "Ten")]
            [TestCase(11, "Eleven")]
            [TestCase(19, "Nineteen")]
            [TestCase(59, "Fifty Nine")]
            [TestCase(100, "One Hundred")]
            [TestCase(1012, "One Thousand Twelve")]
            [TestCase(237846, "Two Hundred Thirty Seven Thousand Eight Hundred Forty Six")]
            [TestCase(2147483647, "Two Billion One Hundred Forty Seven Million Four Hundred Eighty Three Thousand Six Hundred Forty Seven")]
            [TestCase(777777777, "Seven Hundred Seventy Seven Million Seven Hundred Seventy Seven Thousand Seven Hundred Seventy Seven")]
            [TestCase(1000000001, "One Billion One")]
            public void TestCase(int num, string expected)
            {
                // Arrange

                // Act
                var result = new Solution().NumberToWords(num);

                // Assert
                Assert.AreEqual(expected, result);
            }
        }
    }
}