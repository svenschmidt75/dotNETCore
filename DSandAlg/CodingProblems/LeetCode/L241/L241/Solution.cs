#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 241. Different Ways to Add Parentheses
// URL: https://leetcode.com/problems/different-ways-to-add-parentheses/

namespace LeetCode
{
    public class Solution
    {
        public IList<int> DiffWaysToCompute(string expression)
        {
            // SS: runtime complexity: O(2^expr)

            IList<int> Solve(string expr)
            {
                var localResult = new List<int>();

                // SS: base case
                if (string.IsNullOrEmpty(expr))
                {
                    return localResult;
                }

                var i = 0;
                while (i < expr.Length)
                {
                    // SS: skip to next operator
                    while (i < expr.Length && char.IsDigit(expr[i]))
                    {
                        i++;
                    }

                    if (i == expr.Length)
                    {
                        // SS: there was no operator, must be a number
                        var number = ReadNumber(expr);
                        localResult.Add(number);
                        break;
                    }

                    // SS: left expr
                    var leftExpr = expr[..i];
                    var leftValues = Solve(leftExpr);

                    // SS: read operator
                    var op = expr[i++];

                    // SS: right expr
                    var rightExpr = expr[i..];
                    var rightValues = Solve(rightExpr);

                    // SS: calculate values
                    foreach (var leftValue in leftValues)
                    {
                        foreach (var rightValue in rightValues)
                        {
                            var value = leftValue;

                            switch (op)
                            {
                                case '+':
                                    value += rightValue;
                                    break;

                                case '-':
                                    value -= rightValue;
                                    break;

                                case '*':
                                    value *= rightValue;
                                    break;
                            }

                            localResult.Add(value);
                        }
                    }

                    // SS: skip until next operator
                    while (i < expr.Length && char.IsDigit(expr[i]))
                    {
                        i++;
                    }
                }

                return localResult;
            }

            var result = Solve(expression);
            return result;
        }

        private static int ReadNumber(string expr)
        {
            var n = 0;
            var i = 0;
            while (i < expr.Length && char.IsDigit(expr[i]))
            {
                // SS: shift 1 digit to right
                n *= 10;
                n += expr[i++] - '0';
            }

            return n;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("1", new[] {1})]
            [TestCase("26", new[] {26})]
            [TestCase("1+2", new[] {3})]
            [TestCase("1+2*3", new[] {7, 9})]
            [TestCase("2-1-1", new[] {0, 2})]
            [TestCase("2*3-4*5", new[] {-34, -14, -10, -10, 10})]
            public void Test1(string expr, IList<int> expected)
            {
                // Arrange

                // Act
                var results = new Solution().DiffWaysToCompute(expr);

                // Assert
                CollectionAssert.AreEquivalent(expected, results);
            }
        }
    }
}