#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

namespace L66
{
    public class Solution
    {
        public int[] PlusOne(int[] digits)
        {
            var result = new List<int>();
            Array.Reverse(digits);

            var carry = 0;
            for (var i = 0; i < digits.Length; i++)
            {
                var digit = digits[i] + carry;

                if (i == 0)
                {
                    digit++;
                }

                carry = 0;
                if (digit > 9)
                {
                    digit = 0;
                    carry = 1;
                }

                result.Add(digit);
            }

            if (carry == 1)
            {
                result.Add(1);
            }

            result.Reverse();

            return result.ToArray();
        }
    }


    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var input = new[] {1, 2, 3};

            // Act
            var result = new Solution().PlusOne(input);

            // Assert
            CollectionAssert.AreEqual(new[] {1, 2, 4}, result);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var input = new[] {4, 3, 2, 1};

            // Act
            var result = new Solution().PlusOne(input);

            // Assert
            CollectionAssert.AreEqual(new[] {4, 3, 2, 2}, result);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var input = new[] {0};

            // Act
            var result = new Solution().PlusOne(input);

            // Assert
            CollectionAssert.AreEqual(new[] {1}, result);
        }

        [Test]
        public void Test4()
        {
            // Arrange
            var input = new[] {9};

            // Act
            var result = new Solution().PlusOne(input);

            // Assert
            CollectionAssert.AreEqual(new[] {1, 0}, result);
        }

        [Test]
        public void Test5()
        {
            // Arrange
            var input = new[] {4, 3, 2, 9};

            // Act
            var result = new Solution().PlusOne(input);

            // Assert
            CollectionAssert.AreEqual(new[] {4, 3, 3, 0}, result);
        }
    }
}