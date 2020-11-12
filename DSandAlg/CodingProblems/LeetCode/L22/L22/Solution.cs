#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
            return GenerateParenthesisLarry(n);
        }

        private IList<string> GenerateParenthesisLarry(int n)
        {
            // SS: Larry's approach: https://www.youtube.com/watch?v=hvdeiy6SPKY
            var results = new HashSet<string>();
            
            if (n == 1)
            {
                results.Add("()");
            }
            else
            {
                var r = GenerateParenthesisLarry(n - 1);
                foreach (var item in r)
                {
                    results.Add("(" + item + ")");
                    results.Add("()" + item);
                    results.Add(item + "()");
                }

                for (int i = 2; i < n - 2 + 1; i++)
                {
                    // SS: generate of length i (say n = 8) (i.e. 2, 3, 4, 5, 6)
                    var rLeft = GenerateParenthesisLarry(i);

                    // SS: generate of length n - 1 (i.e. 6, 5, 4, 3, 2)
                    var rRight = GenerateParenthesisLarry(n - i);

                    //  length i (say n = 8)  (i.e. 2, 3, 4, 5, 6)
                    // length n - 1           (i.e. 6, 5, 4, 3, 2)
                    //                              8, 8, 8, 8, 8
                    foreach (var leftItem in rLeft)
                    {
                        foreach (var rightItem in rRight)
                        {
                            results.Add(leftItem + rightItem);
                        }
                    }
                }
            }

            var res = results.ToList();
            res.Sort();
            return res;
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

            [Test]
            public void Test3()
            {
                // Arrange
                var n = 4;

                // Act
                var result = new Solution().GenerateParenthesis(n);

                // Assert
                CollectionAssert.AreEquivalent(new[] {"(((())))","((()()))","((())())","((()))()","(()(()))","(()()())","(()())()","(())(())","(())()()","()((()))","()(()())","()(())()","()()(())","()()()()"}, result);
            }
        }
    }
}