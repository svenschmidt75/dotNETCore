#region

using NUnit.Framework;

#endregion

// 8. String to Integer (atoi)
// https://leetcode.com/problems/string-to-integer-atoi/

namespace L8
{
    public class Solution
    {
        public int MyAtoi(string s)
        {
            // skip white-space
            var i = 0;
            while (i < s.Length && s[i] == ' ')
            {
                i++;
            }

            // string all white-space?
            if (i == s.Length)
            {
                return 0;
            }

            // implicitly assume positive sign
            var sign = 1;
            if (s[i] == '-')
            {
                sign = -1;
                i++;
            }
            else if (s[i] == '+')
            {
                i++;
            }

            // skip leading 0
            while (i < s.Length && s[i] == '0')
            {
                i++;
            }

            // count number of digits
            var digits = new int[200];
            var nDigits = 0;
            while (i < s.Length && char.IsDigit(s[i]))
            {
                var digit = s[i] - '0';
                digits[nDigits++] = digit;
                i++;
            }

            if (nDigits == 0)
            {
                return 0;
            }

            var result = 0;

            var k = nDigits - 1;
            var exp = 1;
            while (k >= 0)
            {
                // would adding digits[k] * exp cause and overflow?
                if ((int.MaxValue - result) / exp < digits[k])
                {
                    return sign < 0 ? int.MinValue : int.MaxValue;
                }

                result += digits[k] * exp;

                // would multiplying exp by 10 cause an overflow?
                if (int.MaxValue / 10 - exp < 0)
                {
                    // we return the result of we have processed all numbers...
                    if (k == 0)
                    {
                        return sign * result;
                    }

                    return sign < 0 ? int.MinValue : int.MaxValue;
                }

                exp *= 10;
                k--;
            }

            return sign * result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "42";

                // Act
                var result = new Solution().MyAtoi(s);

                // Assert
                Assert.AreEqual(42, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "  -42";

                // Act
                var result = new Solution().MyAtoi(s);

                // Assert
                Assert.AreEqual(-42, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "4193 with words";

                // Act
                var result = new Solution().MyAtoi(s);

                // Assert
                Assert.AreEqual(4193, result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "words and 987";

                // Act
                var result = new Solution().MyAtoi(s);

                // Assert
                Assert.AreEqual(0, result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "-91283472332";

                // Act
                var result = new Solution().MyAtoi(s);

                // Assert
                Assert.AreEqual(int.MinValue, result);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var s = "-000000000000000000000000000000000000000000000000001";

                // Act
                var result = new Solution().MyAtoi(s);

                // Assert
                Assert.AreEqual(-1, result);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var s = "10000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000522545459";

                // Act
                var result = new Solution().MyAtoi(s);

                // Assert
                Assert.AreEqual(int.MaxValue, result);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                var s = "  0000000000012345678";

                // Act
                var result = new Solution().MyAtoi(s);

                // Assert
                Assert.AreEqual(12345678, result);
            }

            [Test]
            public void Test9()
            {
                // Arrange
                var s = "2147483646";

                // Act
                var result = new Solution().MyAtoi(s);

                // Assert
                Assert.AreEqual(2147483646, result);
            }

            [Test]
            public void Test10()
            {
                // Arrange
                var s = "-2147483647";

                // Act
                var result = new Solution().MyAtoi(s);

                // Assert
                Assert.AreEqual(-2147483647, result);
            }

            [Test]
            public void Test11()
            {
                // Arrange
                var s = "2147483648";

                // Act
                var result = new Solution().MyAtoi(s);

                // Assert
                Assert.AreEqual(2147483647, result);
            }

            [Test]
            public void Test12()
            {
                // Arrange
                var s = "-6147483648";

                // Act
                var result = new Solution().MyAtoi(s);

                // Assert
                Assert.AreEqual(int.MinValue, result);
            }
        }
    }
}