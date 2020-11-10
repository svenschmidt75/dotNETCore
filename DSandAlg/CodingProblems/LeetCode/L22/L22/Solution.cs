#region

using System;
using System.Collections.Generic;
using NUnit.Framework;

#endregion

// Problem: 22. Generate Parentheses
// URL: https://leetcode.com/problems/generate-parentheses/

namespace LeetCode22
{
    public class Solution
    {
        public IList<string> GenerateParenthesis(int n)
        {
            return GenerateParenthesisDQ(n);
        }

        private IList<string> GenerateParenthesisDQ(int n)
        {
            // SS: runtime complexity: O(2^N)
            var result = new List<string>();
            var current = new char[n * 2];
            GenerateParenthesisDQ(n, n, current, 0, result);
            return result;
        }

        private void GenerateParenthesisDQ(int nOpen, int nClose, char[] current, int pos, List<string> result)
        {
            // SS: I don't think this is a DP problem, since there can be no overlapping
            // subproblems (I think). This is because the parenthesis cannot be arranged
            // arbitrarily...

            if (nOpen == 0 && nClose == 0)
            {
                var r = new string(current);
                result.Add(r);
                return;
            }

            if (nOpen > 0)
            {
                // SS: not necessary to new up a new array here,
                // sufficient to only do so when doing closing
                // parenthesis...
                var c = new char[current.Length];
                Array.Copy(current, c, pos);
                c[pos] = '(';
                GenerateParenthesisDQ(nOpen - 1, nClose, c, pos + 1, result);
            }

            if (nClose > 0 && nClose > nOpen)
            {
                var c = new char[current.Length];
                Array.Copy(current, c, pos);
                c[pos] = ')';
                GenerateParenthesisDQ(nOpen, nClose - 1, c, pos + 1, result);
            }
        }


        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                // Arrange
                var n = 3;

                // Act
                var result = new Solution().GenerateParenthesis(n);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"((()))", "(()())", "(())()", "()(())", "()()()"}, result);
            }

            [Test]
            public void Test2()
            {
                // Arrange
                var n = 1;

                // Act
                var result = new Solution().GenerateParenthesis(n);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"()"}, result);
            }
        }
    }
}