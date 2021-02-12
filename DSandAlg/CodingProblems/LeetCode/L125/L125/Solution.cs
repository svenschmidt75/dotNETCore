#region

using NUnit.Framework;

#endregion

// Problem: 125. Valid Palindrome
// URL: https://leetcode.com/problems/valid-palindrome/

namespace LeetCode
{
    public class Solution
    {
        public bool IsPalindrome(string s)
        {
            if (s == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(s))
            {
                return true;
            }

            var i = 0;
            var j = s.Length - 1;
            while (i < j)
            {
                // SS: skip non-alphanumeric chars
                while (i < j && char.IsLetterOrDigit(s[i]) == false)
                {
                    i++;
                }

                if (i == j)
                {
                    return true;
                }

                // SS: skip non-alphanumeric chars
                while (j > i && char.IsLetterOrDigit(s[j]) == false)
                {
                    j--;
                }

                if (i == j)
                {
                    return true;
                }

                if (char.ToLower(s[i]) != char.ToLower(s[j]))
                {
                    return false;
                }

                i++;
                j--;
            }

            return true;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "A man, a plan, a canal: Panama";

                // Act
                var isValid = new Solution().IsPalindrome(s);

                // Assert
                Assert.True(isValid);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "race a car";

                // Act
                var isValid = new Solution().IsPalindrome(s);

                // Assert
                Assert.False(isValid);
            }
        }
    }
}