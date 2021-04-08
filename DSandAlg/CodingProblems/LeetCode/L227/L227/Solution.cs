#region

using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: https://leetcode.com/problems/basic-calculator-ii/
// URL: 227. Basic Calculator II

namespace LeetCode
{
    public class Solution
    {
        private static (int n, int sIdx) ParseNumber(string s, int sIdx)
        {
            var n = 0;
            while (sIdx < s.Length && char.IsDigit(s[sIdx]))
            {
                n *= 10;
                n += s[sIdx] - '0';
                sIdx++;
            }

            return (n, sIdx);
        }

        private static (int op, int sIdx) ParseOperator(string s, int sIdx)
        {
            var op = s[sIdx];
            if (op == '+')
            {
                return (0, sIdx + 1);
            }

            if (op == '-')
            {
                return (1, sIdx + 1);
            }

            if (op == '*')
            {
                return (2, sIdx + 1);
            }

            return (3, sIdx + 1);
        }

        private static int Evaluate(IList<(int type, int n)> stream)
        {
            var stack = new List<(int type, int n)>();

            var idx = 0;

            // SS: stream always starts with a number
            var v1 = stream[idx++].n;

            while (idx < stream.Count)
            {
                // SS: read operator
                var op = stream[idx++].n;

                if (op == 2 || op == 3)
                {
                    var v2 = stream[idx++].n;
                    int tmp;
                    if (op == 2)
                    {
                        tmp = v1 * v2;
                    }
                    else
                    {
                        tmp = v1 / v2;
                    }

                    v1 = tmp;
                }
                else
                {
                    stack.Add((0, v1));
                    stack.Add((1, op));
                    v1 = stream[idx++].n;
                }
            }

            stack.Add((0, v1));

            // SS: only + and - are left
            var result = stack[0].n;
            for (var i = 1; i < stack.Count; i += 2)
            {
                var op = stack[i].n;
                var v2 = stack[i + 1].n;

                int tmp;
                if (op == 0)
                {
                    tmp = result + v2;
                }
                else
                {
                    tmp = result - v2;
                }

                result = tmp;
            }

            return result;
        }

        public int Calculate(string s)
        {
            var stream = new List<(int type, int n)>();

            // SS: tokenize
            var sIdx = 0;
            while (sIdx < s.Length)
            {
                if (char.IsWhiteSpace(s[sIdx]))
                {
                    sIdx++;
                    continue;
                }

                // SS: parse number or operator
                if (char.IsDigit(s[sIdx]) == false)
                {
                    int op;
                    (op, sIdx) = ParseOperator(s, sIdx);
                    stream.Add((1, op));
                }
                else
                {
                    int n;
                    (n, sIdx) = ParseNumber(s, sIdx);
                    stream.Add((0, n));
                }
            }

            // SS: evaluate
            var result = Evaluate(stream);
            return result;
        }

        [TestFixture]
        public class Tests
        {
            [TestCase("3+2*2", 7)]
            [TestCase(" 3/2 ", 1)]
            [TestCase(" 3+5 / 2 ", 5)]
            [TestCase(" 34+47 / 2 *63", 1483)]
            [TestCase(" 34+47 / 2 *63 +  19", 1502)]
            [TestCase(" 3478653+4785 / 2 *6324", 18605661)]
            [TestCase("1-1+1", 1)]
            [TestCase("0-0", 0)]
            [TestCase("1*2-3/4+5*6-7*8+9/10", -24)]
            public void Test(string s, int expectedResult)
            {
                // Arrange

                // Act
                var result = new Solution().Calculate(s);

                // Assert
                Assert.AreEqual(expectedResult, result);
            }
        }
    }
}