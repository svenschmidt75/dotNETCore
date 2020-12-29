#region

using System;
using NUnit.Framework;

#endregion

// Problem: 67. Add Binary
// URL: https://leetcode.com/problems/add-binary/

namespace LeetCode
{
    public class Solution
    {
        public string AddBinary(string a, string b)
        {
            var result = new char[Math.Max(a.Length, b.Length) + 1];

            var aIdx = a.Length - 1;
            var bIdx = b.Length - 1;

            var carry = 0;
            var resultIdx = result.Length - 1;

            while (aIdx >= 0 && bIdx >= 0)
            {
                var ca = a[aIdx] - '0';
                var cb = b[bIdx] - '0';
                var sum = ca + cb + carry;
                if (sum > 1)
                {
                    result[resultIdx] = (char) (sum - 2 + '0');
                    carry = 1;
                }
                else
                {
                    result[resultIdx] = (char) (sum + '0');
                    carry = 0;
                }

                aIdx--;
                bIdx--;
                resultIdx--;
            }

            while (aIdx >= 0)
            {
                var ca = a[aIdx] - '0';
                var sum = ca + carry;
                if (sum > 1)
                {
                    result[resultIdx] = (char) (sum - 2 + '0');
                    carry = 1;
                }
                else
                {
                    result[resultIdx] = (char) (sum + '0');
                    carry = 0;
                }

                aIdx--;
                resultIdx--;
            }

            while (bIdx >= 0)
            {
                var cb = b[bIdx] - '0';
                var sum = cb + carry;
                if (sum > 1)
                {
                    result[resultIdx] = (char) (sum - 2 + '0');
                    carry = 1;
                }
                else
                {
                    result[resultIdx] = (char) (sum + '0');
                    carry = 0;
                }

                bIdx--;
                resultIdx--;
            }

            if (carry == 1)
            {
                result[resultIdx] = '1';
                resultIdx--;
            }

            var resultString = new string(result[(resultIdx + 1)..]);
            return resultString;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var a = "11";
                var b = "1";

                // Act
                var result = new Solution().AddBinary(a, b);

                // Assert
                Assert.AreEqual("100", result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var a = "1010";
                var b = "1011";

                // Act
                var result = new Solution().AddBinary(a, b);

                // Assert
                Assert.AreEqual("10101", result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var a = "0";
                var b = "1";

                // Act
                var result = new Solution().AddBinary(a, b);

                // Assert
                Assert.AreEqual("1", result);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var a = "1";
                var b = "111";

                // Act
                var result = new Solution().AddBinary(a, b);

                // Assert
                Assert.AreEqual("1000", result);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var a = "1111";
                var b = "1111";

                // Act
                var result = new Solution().AddBinary(a, b);

                // Assert
                Assert.AreEqual("11110", result);
            }
        }
    }
}