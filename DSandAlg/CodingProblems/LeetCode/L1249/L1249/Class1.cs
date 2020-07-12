#region

using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion

// 1249. Minimum Remove to Make Valid Parentheses
// https://leetcode.com/problems/minimum-remove-to-make-valid-parentheses/

namespace L1249
{
    public class Solution
    {
        public string MinRemoveToMakeValid(string s)
        {
            var stack = new Stack<int>();
            var map = new HashSet<int>();

            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];

                if (c == '(')
                {
                    stack.Push(i);

                    // add unbalanced
                    map.Add(i);
                }
                else if (c == ')')
                {
                    if (stack.Any())
                    {
                        var idx = stack.Pop();

                        // parenthesis are balanced
                        map.Remove(idx);
                    }
                    else
                    {
                        // add unbalanced
                        map.Add(i);
                    }
                }
            }

            var result = new StringBuilder();
            for (var i = 0; i < s.Length; i++)
            {
                var c = s[i];

                if (c == '(')
                {
                    if (map.Contains(i) == false)
                    {
                        result.Append(c);
                    }
                }
                else if (c == ')')
                {
                    if (map.Contains(i) == false)
                    {
                        result.Append(c);
                    }
                }
                else
                {
                    result.Append(c);
                }
            }

            var r = result.ToString();
            return r;
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            // Arrange
            var input = "lee(t(c)o)de)";

            // Act
            var result = new Solution().MinRemoveToMakeValid(input);

            // Assert
            Assert.AreEqual("lee(t(c)o)de", result);
        }

        [Test]
        public void Test2()
        {
            // Arrange
            var input = "a)b(c)d";

            // Act
            var result = new Solution().MinRemoveToMakeValid(input);

            // Assert
            Assert.AreEqual("ab(c)d", result);
        }

        [Test]
        public void Test3()
        {
            // Arrange
            var input = "))((";

            // Act
            var result = new Solution().MinRemoveToMakeValid(input);

            // Assert
            Assert.AreEqual("", result);
        }

        [Test]
        public void Test4()
        {
            // Arrange
            var input = "(a(b(c)d)";

            // Act
            var result = new Solution().MinRemoveToMakeValid(input);

            // Assert
            Assert.AreEqual("a(b(c)d)", result);
        }

        [Test]
        public void Test5()
        {
            // Arrange
            var input = "l(e(e(t))";

            // Act
            var result = new Solution().MinRemoveToMakeValid(input);

            // Assert
            Assert.AreEqual("le(e(t))", result);
        }

        [Test]
        public void Test6()
        {
            // Arrange
            var input = "abc(d)";

            // Act
            var result = new Solution().MinRemoveToMakeValid(input);

            // Assert
            Assert.AreEqual("abc(d)", result);
        }
    }
}