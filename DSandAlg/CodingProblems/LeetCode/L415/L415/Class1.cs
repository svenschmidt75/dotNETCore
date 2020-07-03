#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

namespace L415
{
    public class Solution
    {
        public string AddStrings(string num1, string num2)
        {
            var maxLength = Math.Min(num1.Length, num2.Length);

            var pos = 0;
            var carry = 0;

            var intResult = new Stack<char>();

            while (pos < maxLength)
            {
                var index1 = num1.Length - pos - 1;
                var c1 = num1[index1];
                var i1 = c1 - '0';

                var index2 = num2.Length - pos - 1;
                var c2 = num2[index2];
                var i2 = c2 - '0';

                var r = i1 + i2 + carry;
                if (r >= 10)
                {
                    r -= 10;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }

                intResult.Push((char) (r + '0'));

                pos++;
            }

            while (pos < num1.Length)
            {
                var index = num1.Length - pos - 1;
                var c = num1[index];
                var i = c - '0';

                var r = i + carry;
                if (r >= 10)
                {
                    r -= 10;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }

                intResult.Push((char) (r + '0'));

                pos++;
            }

            while (pos < num2.Length)
            {
                var index = num2.Length - pos - 1;
                var c = num2[index];
                var i = c - '0';

                var r = i + carry;
                if (r >= 10)
                {
                    r -= 10;
                    carry = 1;
                }
                else
                {
                    carry = 0;
                }

                intResult.Push((char) (r + '0'));

                pos++;
            }

            var result = new List<char>();
            if (carry == 1)
            {
                result.Add('1');
            }

            while (intResult.Any())
            {
                var c = intResult.Pop();
                result.Add(c);
            }

            return new string(result.ToArray());
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var num1 = "6";
            var num2 = "501";

            // Act
            var result = new Solution().AddStrings(num1, num2);

            // Assert
            Assert.AreEqual("507", result);
        }
    }
}