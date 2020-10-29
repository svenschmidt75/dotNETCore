#region

using System;
using NUnit.Framework;

#endregion

// 9. Palindrome Number
// https://leetcode.com/problems/palindrome-number/

namespace L9
{
    public class Solution
    {
        public bool IsPalindrome(int x)
        {
            var s1 = x.ToString();

            var array = s1.ToCharArray();
            Array.Reverse(array);
            var s2 = new string(array);

            return s1.Equals(s2);
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var x = 121;

                // Act
                var isPalindrome = new Solution().IsPalindrome(x);

                // Assert
                Assert.True(isPalindrome);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var x = -121;

                // Act
                var isPalindrome = new Solution().IsPalindrome(x);

                // Assert
                Assert.False(isPalindrome);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var x = 10;

                // Act
                var isPalindrome = new Solution().IsPalindrome(x);

                // Assert
                Assert.False(isPalindrome);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var x = -101;

                // Act
                var isPalindrome = new Solution().IsPalindrome(x);

                // Assert
                Assert.False(isPalindrome);
            }
        }
    }
}