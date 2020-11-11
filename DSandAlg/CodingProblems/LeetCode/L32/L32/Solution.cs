#region

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

#endregion

// Problem: 32. Longest Valid Parentheses
// URL: https://leetcode.com/problems/longest-valid-parentheses/

namespace LeetCode32
{
    public class Solution
    {
        public int LongestValidParentheses(string s)
        {
            // SS: runtime complexity: O(N)
            // space complexity: O(N)

            if (s.Length == 0)
            {
                return 0;
            }

            var maxLength = 0;
            var length = 0;

            var stack = new Stack<int>();

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];

                if (c == '(')
                {
                    stack.Push(length);
                    length = 0;
                }
                else
                {
                    if (stack.Any() == false)
                    {
                        length = 0;
                    }
                    else
                    {
                        length += stack.Pop() + 2;
                        maxLength = Math.Max(maxLength, length);
                    }
                }
            }

            return maxLength;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var s = "(()";

                // Act
                var maxLength = new Solution().LongestValidParentheses(s);

                // Assert
                Assert.AreEqual(2, maxLength);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var s = "())()";

                // Act
                var maxLength = new Solution().LongestValidParentheses(s);

                // Assert
                Assert.AreEqual(2, maxLength);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                var s = "())(())";

                // Act
                var maxLength = new Solution().LongestValidParentheses(s);

                // Assert
                Assert.AreEqual(4, maxLength);
            }

            [Test]
            public void Test4()
            {
                // Arrange
                var s = "())()(())";

                // Act
                var maxLength = new Solution().LongestValidParentheses(s);

                // Assert
                Assert.AreEqual(6, maxLength);
            }

            [Test]
            public void Test5()
            {
                // Arrange
                var s = "()(()(())";

                // Act
                var maxLength = new Solution().LongestValidParentheses(s);

                // Assert
                Assert.AreEqual(6, maxLength);
            }

            [Test]
            public void Test6()
            {
                // Arrange
                var s = "()((()))";

                // Act
                var maxLength = new Solution().LongestValidParentheses(s);

                // Assert
                Assert.AreEqual(8, maxLength);
            }

            [Test]
            public void Test7()
            {
                // Arrange
                var s = "()((())";

                // Act
                var maxLength = new Solution().LongestValidParentheses(s);

                // Assert
                Assert.AreEqual(4, maxLength);
            }

            [Test]
            public void Test8()
            {
                // Arrange
                var s = "()))(())";

                // Act
                var maxLength = new Solution().LongestValidParentheses(s);

                // Assert
                Assert.AreEqual(4, maxLength);
            }
        }
    }
}