#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 150. Evaluate Reverse Polish Notation
// URL: https://leetcode.com/problems/evaluate-reverse-polish-notation/

namespace LeetCode
{
    public class Solution
    {
        public int EvalRPN(string[] tokens)
        {
            var stack = new Stack<int>();

            for (var i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];

                Func<int, int, int> binOp = null;

                if (token == "+")
                {
                    binOp = (a, b) => a + b;
                }
                else if (token == "-")
                {
                    binOp = (a, b) => a - b;
                }
                else if (token == "*")
                {
                    binOp = (a, b) => a * b;
                }
                else if (token == "/")
                {
                    binOp = (a, b) => a / b;
                }
                else
                {
                    // SS: operand is integer
                    var a = int.Parse(token);
                    stack.Push(a);
                }

                if (binOp != null)
                {
                    var b = stack.Pop();
                    var a = stack.Pop();
                    var c = binOp(a, b);
                    stack.Push(c);
                }
            }

            var result = stack.Pop();
            return result;
        }

        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                string[] tokens = {"2", "1", "+", "3", "*"};

                // Act
                var result = new Solution().EvalRPN(tokens);

                // Assert
                Assert.AreEqual(9, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                string[] tokens = {"4", "13", "5", "/", "+"};

                // Act
                var result = new Solution().EvalRPN(tokens);

                // Assert
                Assert.AreEqual(6, result);
            }

            [Test]
            public void Test3()
            {
                // Arrange
                string[] tokens = {"10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+"};

                // Act
                var result = new Solution().EvalRPN(tokens);

                // Assert
                Assert.AreEqual(22, result);
            }
        }
    }
}