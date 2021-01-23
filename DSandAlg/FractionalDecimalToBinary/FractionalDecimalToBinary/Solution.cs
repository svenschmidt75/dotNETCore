#region

using NUnit.Framework;

#endregion

namespace Problem
{
    public class Solution
    {
        public static string Convert(float fractional)
        {
            // SS: Input: positive fractional decimal number, for example 0.7392
            // Output: binary representation

            const int maxDigits = 23;
            var binaryDigits = new char[maxDigits];

            // SS: the leading digit in front of the radix point is 0, because
            // the number is 0 < fractional < 1
            var subtract = 0.5f;
            for (var i = 0; i < maxDigits; i++)
            {
                // SS: shift to the left, i.e. multiply by 2
                if (fractional - subtract >= 0)
                {
                    binaryDigits[i] = '1';
                    fractional -= subtract;
                }
                else
                {
                    binaryDigits[i] = '0';
                }

                subtract /= 2;
            }

            var result = new string(binaryDigits);
            return result;
        }

        public static float Convert(string binaryRespresentation)
        {
            float number = 0;

            var value = 0.5f;
            for (var i = 0; i < binaryRespresentation.Length; i++)
            {
                var digit = (byte) (binaryRespresentation[i] - '0');
                number += digit * value;
                value /= 2;
            }

            return number;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange

                // Act
                var result = Convert(0.7392f);
                var value = Convert(result);

                // Assert
            }
        }
    }
}