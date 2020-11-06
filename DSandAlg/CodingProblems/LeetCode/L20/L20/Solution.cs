#region

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 20. Valid Parentheses
// URL: https://leetcode.com/problems/valid-parentheses/

namespace LeetCode20
{
    public class Solution
    {
        public bool IsValid(string s)
        {
            var stack = new Stack<char>();

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];

                if (c == '(' || c == '[' || c == '{')
                {
                    stack.Push(c);
                }
                else
                {
                    if (stack.Any() == false)
                    {
                        return false;
                    }

                    char p = stack.Pop();
                    if (c == ')' && p != '(')
                    {
                        return false;
                    }

                    if (c == ']' && p != '[')
                    {
                        return false;
                    }

                    if (c == '}' && p != '{')
                    {
                        return false;
                    }
                }
            }

            return stack.Any() == false;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "()";

                // Act
                var isValid = new Solution().IsValid(s);

                // Assert
                Assert.True(isValid);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "()[]{}";

                // Act
                var isValid = new Solution().IsValid(s);

                // Assert
                Assert.True(isValid);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "(]";

                // Act
                var isValid = new Solution().IsValid(s);

                // Assert
                Assert.False(isValid);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "([)]";

                // Act
                var isValid = new Solution().IsValid(s);

                // Assert
                Assert.False(isValid);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "{[]}";

                // Act
                var isValid = new Solution().IsValid(s);

                // Assert
                Assert.True(isValid);
            }
        }
    }
}