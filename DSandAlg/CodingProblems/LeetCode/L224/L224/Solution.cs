#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 224. Basic Calculator
// URL: https://leetcode.com/problems/basic-calculator/

namespace LeetCode
{
    public class Solution
    {
        /* The grammar is in BNF:
         *
         * expr -> expr '+' expr
         *      |  expr '-' expr
         *      | factor
         *
         * factor -> '(' expr ')'
         *        | '-' factor
         *        | number
         *
         * but this grammar is left-recursive, so a recursive decent parser
         * may not terminate and get stuck in an infinite loop.
         *
         * With left-recursion removed, we have
         *
         * expr -> term {op term}
         *
         * term -> '(' expr ')'
         *      |  '-' term
         *      |  number
         */

        public int Calculate(string s)
        {
            // return Calculate1(s);
            return Calculate2(s);
        }

        private int Calculate2(string s)
        {
            // SS: Solution from Leetcode...
            int len = s.Length, sign = 1, result = 0;
            var stack = new Stack<int>();
            for (var i = 0; i < len; i++)
            {
                if (char.IsDigit(s[i]))
                {
                    var sum = s[i] - '0';
                    while (i + 1 < len && char.IsDigit(s[i + 1]))
                    {
                        sum = sum * 10 + s[i + 1] - '0';
                        i++;
                    }

                    result += sum * sign;
                }
                else if (s[i] == '+')
                    sign = 1;
                else if (s[i] == '-')
                    sign = -1;
                else if (s[i] == '(')
                {
                    stack.Push(result);
                    stack.Push(sign);
                    result = 0;
                    sign = 1;
                }
                else if (s[i] == ')') result = result * stack.Pop() + stack.Pop();
            }

            return result;
        }

        private static int Calculate1(string s)
        {
            var (value, _) = Expr(s, 0);
            return value;
        }

        private static (int value, int pos) Expr(string s, int i)
        {
            int e1;

            (e1, i) = Term(s, i);

            i = SkipWhiteSpace(s, i);

            while (i < s.Length && (s[i] == '+' || s[i] == '-'))
            {
                var op = s[i++];

                i = SkipWhiteSpace(s, i);

                int e2;
                (e2, i) = Term(s, i);

                if (op == '+')
                {
                    e1 += e2;
                }
                else
                {
                    e1 -= e2;
                }

                i = SkipWhiteSpace(s, i);
            }

            return (e1, i);
        }

        private static (int value, int pos) Term(string s, int i)
        {
            int e;

            i = SkipWhiteSpace(s, i);

            if (s[i] == '(')
            {
                // SS: consume (
                i++;

                (e, i) = Expr(s, i);

                i = SkipWhiteSpace(s, i);

                // SS: consume ')'
                i++;

                return (e, i);
            }

            if (s[i] == '-')
            {
                // SS: consume unary minus
                i++;

                (e, i) = Term(s, i);

                return (-e, i);
            }

            (e, i) = ReadNumber(s, i);
            return (e, i);
        }

        private static (int value, int pos) ReadNumber(string s, int i)
        {
            var j = i;
            while (j < s.Length && char.IsDigit(s[j]))
            {
                j++;
            }

            var v = int.Parse(s[i..j]);
            return (v, j);
        }

        private static int SkipWhiteSpace(string s, int i)
        {
            var j = i;
            while (j < s.Length && char.IsWhiteSpace(s[j]))
            {
                j++;
            }

            return j;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("1 + 1", 2)]
            [TestCase("1 + (2 - 3 ) ", 0)]
            [TestCase("(2 - 3 ) - 1 ", -2)]
            [TestCase(" 2-1 + 2 ", 3)]
            [TestCase(" (1+(4+5+2)-3)+(6+8) ", 23)]
            [TestCase("5   ", 5)]
            [TestCase("-2+1", -1)]
            public void Test1(string s, int expected)
            {
                // Arrange

                // Act
                var e = new Solution().Calculate(s);

                // Assert
                Assert.AreEqual(expected, e);
            }
        }
    }
}