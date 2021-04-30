#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: Evaluate a simple equation of the form (a + b) * c = 8 * (r - p)
// Return true if they both sides are equal to one another.

namespace LeetCode
{
    public class Solution
    {
        private static readonly IDictionary<char, int> _charToNumber = new Dictionary<char, int>
        {
            {'a', 1}
            , {'b', 2}
            , {'c', 3}
            , {'d', 4}
            , {'e', 5}
            , {'f', 6}
            , {'g', 7}
            , {'h', 8}
            , {'i', 9}
            , {'j', 10}
            , {'k', 11}
            , {'l', 12}
            , {'m', 13}
            , {'n', 14}
            , {'o', 15}
            , {'p', 16}
            , {'q', 17}
            , {'r', 18}
            , {'s', 19}
            , {'t', 20}
            , {'u', 21}
            , {'v', 22}
            , {'w', 23}
            , {'x', 24}
            , {'y', 25}
            , {'z', 26}
        };

        public bool Evaluate(string equation)
        {
            var pos = 0;
            while (pos < equation.Length && equation[pos] != '=')
            {
                pos++;
            }

            var left = equation[..pos];
            var right = equation[(pos + 1)..];
            return EvaluateExpr(left) == EvaluateExpr(right);
        }

        public int EvaluateExpr(string equation)
        {
            // SS: allowed literals: a-z, (, ), +, -, *, /
            var sumStack = new Stack<int>();
            var signStack = new Stack<int>();
            var opStack = new Stack<int>();
            var prevStack = new Stack<int>();

            var sum = int.MinValue;
            var sign = 1;
            var op = 0;

            // SS: for evaluation of * and /, which have higher precedence
            var prev = 0;

            var pos = 0;
            while (pos < equation.Length)
            {
                pos = SkipWhiteSpace(equation, pos);
                if (pos == equation.Length)
                {
                    break;
                }

                var c = equation[pos];

                if (char.IsLetter(c))
                {
                    var d = _charToNumber[c];

                    // SS: sum uninitialized?
                    if (sum == int.MinValue)
                    {
                        sum = 0;
                    }

                    (sum, prev) = Eval(sum, prev, sign, op, d);
                }
                else if (c == '+')
                {
                    sign = 1;
                    op = 0;
                }
                else if (c == '-')
                {
                    sign = -1;
                    op = 0;
                }
                else if (c == '*')
                {
                    op = 1;
                }
                else if (c == '/')
                {
                    op = 2;
                }
                else if (c == '(')
                {
                    // SS: sum uninitialized?
                    if (sum == int.MinValue)
                    {
                        sum = 0;
                    }

                    sumStack.Push(sum);
                    prevStack.Push(prev);
                    opStack.Push(op);
                    signStack.Push(sign);

                    sum = int.MinValue;
                    prev = 0;
                    op = 0;
                    sign = 1;
                }
                else if (c == ')')
                {
                    (sum, prev) = Eval(sumStack.Pop(), prevStack.Pop(), signStack.Pop(), opStack.Pop(), sum);
                }

                pos++;
            }

            return sum;
        }

        private static (int sum, int prev) Eval(int sum, int prev, int sign, int op, int d)
        {
            if (op == 0)
            {
                prev = sign * d;
                return (sum + prev, prev);
            }

            if (op == 1)
            {
                sum -= prev;
                prev = prev * d;
                return (sum + prev, prev);
            }

            if (op == 2)
            {
                sum -= prev;
                prev = prev / (sign * d);
                return (sum + prev, prev);
            }

            throw new InvalidOperationException();
        }

        private static int SkipWhiteSpace(string s, int pos)
        {
            while (pos < s.Length && char.IsWhiteSpace(s[pos]))
            {
                pos++;
            }

            return pos;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("a + a", 2)]
            [TestCase("a + b * c", 7)]
            [TestCase("a + b * c + d", 7 + 4)]
            [TestCase("a + f / c", 1 + 2)]
            [TestCase("a + b * (b + c)", 1 + 2 * (2 + 3))]
            [TestCase("a - b * (b + c)", 1 - 2 * (2 + 3))]
            [TestCase("a - b * (b + c) * f", 1 - 2 * (2 + 3) * 6)]
            [TestCase("a + (f / b) - g", 1 + 6 / 2 - 7)]
            [TestCase("-c", -3)]
            [TestCase("a + (f / (c - a)) - g", 1 + 6 / (3 - 1) - 7)]
            [TestCase("(a + (f / (c - a)) - g)", 1 + 6 / (3 - 1) - 7)]
            public void Test1(string equation, int expected)
            {
                // Arrange

                // Act
                var value = new Solution().EvaluateExpr(equation);

                // Assert
                Assert.AreEqual(expected, value);
            }

            [TestCase("a = a", true)]
            [TestCase("a + (g / b) - g = -c", true)]
            public void Test2(string equation, bool expected)
            {
                // Arrange

                // Act
                var isEqual = new Solution().Evaluate(equation);

                // Assert
                Assert.AreEqual(expected, isEqual);
            }
        }
    }
}